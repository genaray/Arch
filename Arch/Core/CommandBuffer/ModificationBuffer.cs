using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core.CommandBuffer;

/// <summary>
/// A sparse array, an alternative to archetypes.
/// </summary>
internal class SparseArray : IDisposable
{

    internal Type _type;
    internal int _size;
    internal int[] _entities;
    internal Array _components;

    public SparseArray(Type type)
    {
        _type = type;
        _size = 0;
        _entities = Array.Empty<int>();
        _components = Array.CreateInstance(type, 1);
    }

    /// <summary>
    /// Adds an entity to this sparse array.
    /// </summary>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int index)
    {
        
        // Resize entities
        if (index >= _entities.Length)
        {
            Array.Resize(ref _entities, index+1);
            Array.Fill(_entities, -1, _size, index);
        }

        _entities[index] = _size;
        _size++;

        // Resize components
        if (_size < _components.Length) return;
        
        var array = Array.CreateInstance(_type, _components.Length*2);
        _components.CopyTo(array, 0);
        _components = array;
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
    /// Returns the sparsearray casted to a generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T[] GetArray<T>()
    {
        return Unsafe.As<T[]>(_components);
    }

    /// <summary>
    /// Sets a component for the entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(int index, in T component)
    {
        GetArray<T>()[_entities[index]] = component;
    }

    /// <summary>
    /// Returns a component for the entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref T Get<T>(int index)
    {
        return ref GetArray<T>()[_entities[index]];
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
internal class SparseSet : IDisposable
{

    /// <summary>
    /// Represents an entity, combined to a internal sparset index id for fast lookups.
    /// </summary>
    internal readonly struct WrappedEntity
    {
        internal readonly Entity _entity;
        internal readonly int _index;

        public WrappedEntity(Entity entity, int index)
        {
            _entity = entity;
            this._index = index;
        }
    }
    
    internal int _size;
    internal List<WrappedEntity> _entities;
    internal SparseArray[] _components;  // Blocking Collections

    public SparseSet(int capacity = 64)
    {
        _entities = new List<WrappedEntity>();
        _components = new SparseArray[ComponentRegistry.Size];
    }

    /// <summary>
    /// Creates an entity inside this sparset. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        var id = _size;
        _entities.Add(new WrappedEntity(entity, id));
        _size++;
        return id;
    }

    /// <summary>
    /// Sets a component for an index. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index, in T component)
    {
        var id = ComponentMeta<T>.Id;
        if (id >= _components.Length)
        {
            Array.Resize(ref _components, id+1);
            _components[id] = new SparseArray(typeof(T));
        }
        
        var array = _components[id];
        if(!array.Has(index)) array.Add(index);
        array.Set(index, in component);
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
    /// Returns a component for the index. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>(int index)
    {
        var id = ComponentMeta<T>.Id;
        var array = _components[id];
        return array.Get<T>(index);
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
            sparset.Dispose();
    }
}

/// <summary>
/// A buffer which stores modification requests of existing entities. 
/// </summary>
public struct ModificationBuffer : IDisposable
{
    
    /// <summary>
    /// Represents a modificated entity, basically a real world entity which got a newly reassigned id. 
    /// </summary>
    public readonly ref struct ModificatedEntity
    {
        internal readonly int _index;
        internal ModificatedEntity(int index)
        {
            _index = index;
        }
    }
    
    internal World _world;
    internal SparseSet _sparseSet;

    /// <summary>
    /// Creates a modification buffer.
    /// </summary>
    /// <param name="world">The world this buffer playbacks to.</param>
    /// <param name="capacity">The initial capcity, grows once it was reached.</param>
    internal ModificationBuffer(World world, int capacity = 64)
    {
        _world = world;
        _sparseSet = new SparseSet(capacity);
    }

    /// <summary>
    /// Modifies an existing entity and returns its wrapped form to operate on.
    /// </summary>
    /// <param name="existingEntity"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ModificatedEntity Modify(in Entity existingEntity)
    {
        var index = _sparseSet.Create(existingEntity);
        return new ModificatedEntity(index);
    }
    
    /// <summary>
    /// Sets an component for a wrapped entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in ModificatedEntity entity, in T component)
    {
        _sparseSet.Set(entity._index, component);
    }

    /// <summary>
    /// Plays back all recorded operations. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {

        // Loop over all sparset entities
        for (var index = 0; index < _sparseSet._size; index++)
        {
            // Get wrapped entity
            var wrappedEntity = _sparseSet._entities[index];
            ref readonly var entity = ref wrappedEntity._entity;
            ref readonly var id = ref wrappedEntity._index;
            
            // Get entity chunk
            ref readonly var chunk = ref _world.GetChunk(in entity);
            var chunkIndex = chunk.EntityIdToIndex[entity.EntityId];
            
            // Loop over all sparset component arrays and if our entity is in one, copy the set component to its chunk 
            for (var i = 0; i < _sparseSet._components.Length; i++)
            {
                var sparseArray = _sparseSet._components[i];
                if(!sparseArray.Has(id)) continue;

                var chunkArray = chunk.GetArray(sparseArray._type);
                Array.Copy(sparseArray._components, id, chunkArray, chunkIndex, 1);
            }
        }
        
        Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _sparseSet.Dispose();
    }
}