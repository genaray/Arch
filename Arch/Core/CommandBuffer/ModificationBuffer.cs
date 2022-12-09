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

    /// <summary>
    /// The type this sparsearray stores as an contignous array.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// The capacity.
    /// </summary>
    public int Capacity { get; private set; }
    
    /// <summary>
    /// The current size.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// The stored entities / indexes.
    /// </summary>
    public int[] Entities;
    
    /// <summary>
    /// The component array of <see cref="Type"/>. 
    /// </summary>
    public Array Components { get; private set; }

    public SparseArray(Type type, int capacity = 64)
    {
        Type = type;

        Capacity = capacity;
        Size = 0;
        Entities = new int[Capacity];
        Array.Fill(Entities, -1);
        Components = Array.CreateInstance(type, Capacity);
    }

    /// <summary>
    /// Adds an entity to this sparse array.
    /// </summary>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int index)
    {
        lock (Type)
        {
            // Resize entities
            if (index >= Entities.Length)
            {
                var length = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, length, index-length);
            }

            Entities[index] = Size;
            Size++;

            // Resize components
            if (Size < Components.Length) return;

            Capacity = Capacity <= 0 ? 1 : Capacity;
            var array = Array.CreateInstance(Type, Capacity * 2);
            Components.CopyTo(array, 0);
            Components = array;
            Capacity *= 2;
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
    /// Returns the sparsearray casted to a generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T[] GetArray<T>()
    {
        return Unsafe.As<T[]>(Components);
    }

    /// <summary>
    /// Sets a component for the entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(int index, in T component)
    { 
        lock (Type)
        {
            GetArray<T>()[Entities[index]] = component;   
        }
    }

    /// <summary>
    /// Returns a component for the entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ref T Get<T>(int index)
    {
        return ref GetArray<T>()[Entities[index]];
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
/// TODO : Tight array like in the structural sparset to avoid uncessecary iterations !! 
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

    /// <summary>
    /// The initial capacity of this set. 
    /// </summary>
    public int InitialCapacity { get; }

    /// <summary>
    /// The size, how many entities are stored in this set.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// A list of all entities. 
    /// </summary>
    public List<WrappedEntity> Entities { get; }

    /// <summary>
    /// The amount of sparse arrays in this set.
    /// </summary>
    public int UsedSize { get; private set; }

    /// <summary>
    /// Tight packed array pointing to used sparse arrays for iteration
    /// </summary>
    public int[] Used;
    
    /// <summary>
    /// The sparse arrays. 
    /// </summary>
    public SparseArray[] Components; 

    private readonly object _createLock = new();              // Lock for create operations
    private readonly object _setLock = new();                 // Lock for set operations

    public SparseSet(int capacity = 64)
    {
        InitialCapacity = capacity;
        Entities = new List<WrappedEntity>();

        UsedSize = 0;
        Used = Array.Empty<int>();
        Components = Array.Empty<SparseArray>();
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
            Entities.Add(new WrappedEntity(entity, id));
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
    public void Set<T>(int index, in T component)
    {
        var id = ComponentMeta<T>.Id;
        lock (_setLock)
        {
            // Allocate new sparsearray for new component type 
            if (id >= Components.Length)
            {
                Array.Resize(ref Components, id + 1);
                Components[id] = new SparseArray(typeof(T), InitialCapacity);

                Array.Resize(ref Used, UsedSize + 1);
                Used[UsedSize] = id;
                UsedSize++;
            }
        }

        // Add and set to sparsearray
        var array = Components[id];
        lock (array) { if (!array.Has(index)) array.Add(index); }
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
        var array = Components[id];
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
        var array = Components[id];
        return array.Get<T>(index);
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
            sparset.Dispose();
    }
}

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

/// <summary>
/// A buffer which stores modification requests of existing entities. 
/// </summary>
public struct ModificationBuffer : IDisposable
{

    /// <summary>
    /// The world this buffer playbacks to.
    /// </summary>
    public World World { get; }
    internal SparseSet _sparseSet;

    /// <summary>
    /// Creates a modification buffer.
    /// </summary>
    /// <param name="world">The world this buffer playbacks to.</param>
    /// <param name="capacity">The initial capcity, grows once it was reached.</param>
    public ModificationBuffer(World world, int capacity = 64)
    {
        World = world;
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
        for (var index = 0; index < _sparseSet.Size; index++)
        {
            // Get wrapped entity
            var wrappedEntity = _sparseSet.Entities[index];
            ref readonly var entity = ref wrappedEntity._entity;
            ref readonly var id = ref wrappedEntity._index;
            
            // Get entity chunk
            ref readonly var chunk = ref World.GetChunk(in entity);
            var chunkIndex = chunk.EntityIdToIndex[entity.EntityId];
            
            // Loop over all sparset component arrays and if our entity is in one, copy the set component to its chunk 
            for (var i = 0; i < _sparseSet.UsedSize; i++)
            {
                var used = _sparseSet.Used[i];
                var sparseArray = _sparseSet.Components[used];
                if(!sparseArray.Has(id)) continue;

                var chunkArray = chunk.GetArray(sparseArray.Type);
                Array.Copy(sparseArray.Components, id, chunkArray, chunkIndex, 1);
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