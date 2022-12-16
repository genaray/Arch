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

    /// <summary>
    /// The type this array stores.
    /// </summary>
    public ComponentType Type { get; }
    
    /// <summary>
    /// The total size.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// The entities / indexes. 
    /// </summary>
    public int[] Entities;

    public StructuralSparseArray(ComponentType type, int capacity = 64)
    {
        Type = type;
        Size = 0;
        Entities = new int[capacity];
        Array.Fill(Entities, -1);
    }

    /// <summary>
    /// Adds an entity to this sparse array.
    /// </summary>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int index)
    {
        lock (this)
        {
            // Resize entities
            if (index >= Entities.Length)
            {
                var lenght = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, lenght, index-lenght);
            }

            Entities[index] = Size;
            Size++;
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
        return index < Entities.Length && Entities[index] != -1;
    }
    
    /// <summary>
    /// Disposes this array. 
    /// </summary>
    public void Dispose()
    {
        Size = 0;
    }
}

/// <summary>
/// A sparset which is an alternative to an archetype. Has some advantages, for example less copy around stuff and easier archetype changes. 
/// </summary>
internal class StructuralSparseSet : IDisposable
{
    /// <summary>
    /// The initial capacity of this set. 
    /// </summary>
    public int InitialCapacity { get; }
    
    /// <summary>
    /// The amount of entities.
    /// </summary>
    public int Size { get; private set; }         
    
    /// <summary>
    /// All entities within this set. 
    /// </summary>
    public List<StructuralEntity> Entities { get; private set; }
    
    /// <summary>
    /// Stores all used component indexes in a tightly packed array [5,1,10]
    /// </summary>
    public int[] Used;      
    
    /// <summary>
    /// How many sparse arrays are actually in here. 
    /// </summary>
    public int UsedSize { get; private set; }  
    
    /// <summary>
    /// The components / sparse arrays. 
    /// </summary>
    public StructuralSparseArray[] Components;  // The components as a sparset so we can easily acess them via component ids

    private readonly object _createLock = new();
    private readonly object _setLock = new();
    
    public StructuralSparseSet(int capacity = 64)
    {
        InitialCapacity = capacity;
        Entities = new List<StructuralEntity>(capacity);
        Components = Array.Empty<StructuralSparseArray>();
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
            var id = Size;
            Entities.Add(new StructuralEntity(entity, id));
            Size++;
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
        var id = Component<T>.ComponentType.Id;

        lock (_setLock)
        {
            // Allocate new sparsearray for component and resize arrays 
            if (id >= Components.Length)
            {
                Array.Resize(ref Used, UsedSize + 1);
                Array.Resize(ref Components, id + 1);

                Components[id] = new StructuralSparseArray(typeof(T), InitialCapacity);
                Used[UsedSize] = id;
                UsedSize++;
            }
        }

        var array = Components[id];
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
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];
        return array.Has(index);
    }
    
    /// <summary>
    /// Disposes this set. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        Size = 0;
        Entities.Clear();
        foreach (var sparset in Components)
            sparset?.Dispose();
    }
}

/// <summary>
/// A buffer to buffer structural changes for entities.
/// </summary>
public struct StructuralBuffer : IDisposable
{

    /// <summary>
    /// The world this buffer playbacks to. 
    /// </summary>
    public World World { get; }

    private StructuralSparseSet _adds;
    private StructuralSparseSet _removes;
    
    private PooledList<ComponentType> _addTypes;
    private PooledList<ComponentType> _removeTypes;

    /// <summary>
    /// Creates a structural buffer.
    /// </summary>
    /// <param name="world">The world this buffer playbacks to.</param>
    /// <param name="capacity">The initial capacity, grows if it was filled.</param>
    public StructuralBuffer(World world, int capacity = 64)
    {
        World = world;
        _adds = new StructuralSparseSet(capacity);
        _removes = new StructuralSparseSet(capacity);
        _addTypes = new PooledList<ComponentType>(8);
        _removeTypes = new PooledList<ComponentType>(8);
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
        for (var index = 0; index < _adds.Size; index++)
        {
            var wrappedEntity = _adds.Entities[index];
            for (var i = 0; i < _adds.UsedSize; i++)
            {
                ref var usedIndex = ref _adds.Used[i];
                ref var sparseSet = ref _adds.Components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _addTypes.Add(sparseSet.Type);
            }
            World.Add(in wrappedEntity._entity, (IList<ComponentType>)_addTypes);
            _addTypes.Clear();
        }
        
        // Playback removes 
        for (var index = 0; index < _removes.Size; index++)
        {
            var wrappedEntity = _removes.Entities[index];
            for (var i = 0; i < _removes.UsedSize; i++)
            {
                ref var usedIndex = ref _removes.Used[i];
                ref var sparseSet = ref _removes.Components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _removeTypes.Add(sparseSet.Type);
            }
            World.Remove(in wrappedEntity._entity, _removeTypes);
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