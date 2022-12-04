using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core.CommandBuffer;


/// <summary>
/// Represents an entity, combined to a internal sparset index id for fast lookups.
/// </summary>
public readonly struct StructuralEntity
{
    internal readonly Entity _entity;
    internal readonly int _index;

    public StructuralEntity(Entity entity, int index)
    {
        _entity = entity;
        this._index = index;
    }
}

/// <summary>
/// A sparse array, an alternative to archetypes.
/// </summary>
internal class StructuralSparseArray : IDisposable
{

    internal Type _type;
    internal int _size;
    internal int[] _entities;

    public StructuralSparseArray(Type type, int capacity = 64)
    {
        _type = type;
        _size = 0;
        _entities = new int[capacity];
        Array.Fill(_entities, -1);
    }

    /// <summary>
    /// Adds an entity to this sparse array.
    /// </summary>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int index)
    {
        lock (_type)
        {
            // Resize entities
            if (index >= _entities.Length)
            {
                var lenght = _entities.Length;
                Array.Resize(ref _entities, index + 1);
                Array.Fill(_entities, -1, lenght, index-lenght);
            }

            _entities[index] = _size;
            _size++;
        }
    }
    
    /// <summary>
    /// Returns true if this sparsearray contains an entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int index)
    {
        return index < _entities.Length && _entities[index] != -1;
    }
    
    /// <summary>
    /// Disposes this array. 
    /// </summary>
    public void Dispose()
    {
        _size = 0;
    }
}

/// <summary>
/// A sparset which is an alternative to an archetype. Has some advantages, for example less copy around stuff and easier archetype changes. 
/// </summary>
internal class StructuralSparseSet : IDisposable
{

    internal int _initialCapacity;
    
    internal int _size;                            // Amount of entities
    internal List<StructuralEntity> _entities;
    
    internal int[] _used;                          // Stores all used component indexes in a tightly packed array [5,1,10]
    internal int _usedSize;                        // Amount of different components
    internal StructuralSparseArray[] _components;  // The components as a sparset so we can easily acess them via component ids

    internal object _createLock = new();
    internal object _setLock = new();
    
    public StructuralSparseSet(int capacity = 64)
    {
        _initialCapacity = capacity;
        _entities = new List<StructuralEntity>(capacity);
        _components = Array.Empty<StructuralSparseArray>();
    }

    /// <summary>
    /// Creates an entity inside this sparset. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        lock (_createLock)
        {
            var id = _size;
            _entities.Add(new StructuralEntity(entity, id));
            _size++;
            return id;
        }
    }

    /// <summary>
    /// Sets a component for an index. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index)
    {
        var id = ComponentMeta<T>.Id;

        lock (_setLock)
        {
            // Allocate new sparsearray for component and resize arrays 
            if (id >= _components.Length)
            {
                Array.Resize(ref _used, _usedSize + 1);
                Array.Resize(ref _components, id + 1);

                _components[id] = new StructuralSparseArray(typeof(T), _initialCapacity);
                _used[_usedSize] = id;
                _usedSize++;
            }
        }

        var array = _components[id];
        lock(array){ if (!array.Has(index)) array.Add(index); }
    }

    
    /// <summary>
    /// Returns whether this index has a certain component or not. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(int index)
    {
        var id = ComponentMeta<T>.Id;
        var array = _components[id];
        return array.Has(index);
    }
    
    /// <summary>
    /// Disposes this set. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _size = 0;
        _entities.Clear();
        foreach (var sparset in _components)
            sparset?.Dispose();
    }
}

/// <summary>
/// A buffer to buffer structural changes for entities.
/// </summary>
public struct StructuralBuffer : IDisposable
{

    internal World _world;
    
    internal StructuralSparseSet _adds;
    internal StructuralSparseSet _removes;
    
    internal PooledList<Type> _addTypes;
    internal PooledList<Type> _removeTypes;

    /// <summary>
    /// Creates a structural buffer.
    /// </summary>
    /// <param name="world">The world this buffer playbacks to.</param>
    /// <param name="capacity">The initial capacity, grows if it was filled.</param>
    public StructuralBuffer(World world, int capacity = 64)
    {
        _world = world;
        _adds = new StructuralSparseSet(capacity);
        _removes = new StructuralSparseSet(capacity);
        _addTypes = new PooledList<Type>(8);
        _removeTypes = new PooledList<Type>(8);
    }

    /// <summary>
    /// Creates a add operation for an passed existing entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>A wrapped entity which can be used in further add operations. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public StructuralEntity BatchAdd(in Entity entity)
    {
        var id = _adds.Create(in entity);
        return new StructuralEntity(entity, id);
    }
    
    /// <summary>
    /// Creates a remove operation for an passed existing entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>A wrapped entity which can be used in further remove operations.</returns>
    public StructuralEntity BatchRemove(in Entity entity)
    {
        var id = _removes.Create(in entity);
        return new StructuralEntity(entity, id);
    }
    
    /// <summary>
    /// Adds a component for a wrapped entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in StructuralEntity entity)
    {
        _adds.Set<T>(entity._index);
    }

    /// <summary>
    /// Removes an component for a wrapped entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in StructuralEntity entity)
    {
        _removes.Set<T>(entity._index);
    }
    
    /// <summary>
    /// Plays back all recorded operations. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        // Playback adds
        for (var index = 0; index < _adds._size; index++)
        {
            var wrappedEntity = _adds._entities[index];
            for (var i = 0; i < _adds._usedSize; i++)
            {
                ref var usedIndex = ref _adds._used[i];
                ref var sparseSet = ref _adds._components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _addTypes.Add(sparseSet._type);
            }
            _world.Add(in wrappedEntity._entity, (IList<Type>)_addTypes);
            _addTypes.Clear();
        }
        
        // Playback removes 
        for (var index = 0; index < _removes._size; index++)
        {
            var wrappedEntity = _removes._entities[index];
            for (var i = 0; i < _removes._usedSize; i++)
            {
                ref var usedIndex = ref _removes._used[i];
                ref var sparseSet = ref _removes._components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _removeTypes.Add(sparseSet._type);
            }
            _world.Remove(in wrappedEntity._entity, _removeTypes);
            _removeTypes.Clear();
        }
        Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _adds.Dispose();
        _removes.Dispose();
        _addTypes.Clear();
        _removeTypes.Clear();
    }
}