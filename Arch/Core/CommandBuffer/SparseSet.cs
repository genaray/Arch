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
    public ComponentType Type { get; }

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
    public ComponentArray<IComponent> Components { get; private set; }

    public SparseArray(ComponentType type, int capacity = 64)
    {
        Type = type;

        Capacity = capacity;
        Size = 0;
        Entities = new int[Capacity];
        Array.Fill(Entities, -1);
        Components = new ComponentArray<IComponent>(type, Capacity);
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
                var length = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, length, index-length);
            }

            Entities[index] = Size;
            Size++;

            // Resize components
            if (Size < Components.Array.Length) return;

            Capacity = Capacity <= 0 ? 1 : Capacity;
            var array = new ComponentArray<IComponent>(Type, Capacity * 2);
            Components.Array.CopyTo(array.Array, 0);
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
        return Unsafe.As<T[]>(Components.Array);
    }

    /// <summary>
    /// Sets a component for the entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(int index, in T component)
    { 
        lock (this)
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
        var id = Component<T>.ComponentType.Id;
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
        var id = Component<T>.ComponentType.Id;
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
        var id = Component<T>.ComponentType.Id;
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
            sparset?.Dispose();
    }
}