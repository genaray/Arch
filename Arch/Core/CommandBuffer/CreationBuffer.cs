using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Core.CommandBuffer;

/// <summary>
/// A archetype variation used to store buffered entities and their components.
/// Its more lightweight and faster for this purpose. 
/// Does not use chunks, neither has a real representation of entities... basically a plain multiple array storage. 
/// </summary>
internal class LinearArchetype
{
    
    /// <summary>
    /// The types this archetype stores. 
    /// </summary>
    public Type[] Types { get; }
    
    /// <summary>
    /// The size, how many entities this archetype stores. 
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// The total current capacity of this archetype.
    /// </summary>
    public int Capacity { get; private set; }
    
    /// <summary>
    /// The component arrays, where the components are stored. 
    /// </summary>
    public Array[] Components { get; }
    
    /// <summary>
    /// The lookup array for instant acess of arrays. 
    /// </summary>
    private readonly int[] _lookupArray; 

    internal LinearArchetype(Type[] types, int capacity = 64)
    {
        Types = types;
        
        // Allocate arrays and map 
        _lookupArray = types.ToLookupArray();
        Components = new Array[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            Components[index] = Array.CreateInstance(type, capacity);
        }

        Capacity = capacity;
    }

    /// <summary>
    /// Adds an row/entity to the archetype.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add()
    {
        lock (Components)
        {
            // Increase internal capacities when full
            if (Size >= Capacity-1)
            {
                // Set capacity to 1 if its zero, resize all arrays to a bigger size
                Capacity = Capacity <= 0 ? 1 : Capacity;
                for (var i = 0; i < Components.Length; i++)
                {
                    var type = Types[i];
                    var components = Components[i];
                    var newArray = Array.CreateInstance(type, Capacity * 2);

                    // Lock components since another thread might just modify it, only one at a time !
                    lock (components)
                    {
                        components.CopyTo(newArray, 0);
                        Components[i] = newArray;   
                    }
                }
                Capacity *= 2;
            }

            var index = Size;
            Size++;
            return index;
        }
    }

    /// <summary>
    /// Sets an component for a row/entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index, in T component)
    {
        var id = ComponentMeta<T>.Id;
        var arrayIndex = _lookupArray[id];
        var components = Components[arrayIndex];
        var array = Unsafe.As<T[]>(components);

        lock (components)
        { 
            array[index] = component;
        }
    }
}

/// <summary>
/// Represents a created entity. 
/// </summary>
public readonly ref struct BufferedEntity
{
    internal readonly int _index;
    internal readonly LinearArchetype _archetype;

    internal BufferedEntity(int index, LinearArchetype archetype)
    {
        _index = index;
        _archetype = archetype;
    }
}

/// <summary>
/// A buffer used to buffer the creation of entities. 
/// </summary>
public struct CreationBuffer
{

    /// <summary>
    /// The world this buffer playbacks to.
    /// </summary>
    public World World { get; }
    
    /// <summary>
    /// The initial capacity. 
    /// </summary>
    public int InitialCapacity { get; }
    
    /// <summary>
    /// The archetypes stored in this buffer. 
    /// </summary>
    private readonly Dictionary<int, LinearArchetype> _archetypes;
    
    /// <summary>
    /// Creates a creation buffer.
    /// </summary>
    /// <param name="world">The world this playbacks to.</param>
    /// <param name="initialCapacity">The initial capacity, grows once it was reached.</param>
    internal CreationBuffer(World world, int initialCapacity = 64)
    {
        World = world;
        InitialCapacity = initialCapacity;
        _archetypes = new Dictionary<int, LinearArchetype>(8);
    }

    /// <summary>
    /// Gets or creates an archetype. 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="archetype"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void GetOrCreateArchetype(Type[] types, out LinearArchetype archetype)
    {
        // Create new archetype, requires a lock. 
        lock (_archetypes)
        {
            var hash = ComponentMeta.GetHashCode(types);
            if (_archetypes.TryGetValue(hash, out archetype)) return;

            archetype = new LinearArchetype(types, InitialCapacity);
            _archetypes[hash] = archetype;
        }
    }
    
    /// <summary>
    /// Creates an entity by a set of types. 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BufferedEntity Create(Type[] types)
    {
        GetOrCreateArchetype(types, out var archetype);
        var archetypeIndex = archetype.Add();
        return new BufferedEntity(archetypeIndex, archetype);
    }

    /// <summary>
    /// Sets a component for a created entity. 
    /// </summary>
    /// <param name="bufferedEntity"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in BufferedEntity bufferedEntity, in T component)
    {
        var archetype = bufferedEntity._archetype;
        archetype.Set(bufferedEntity._index, in component);
    }

    /// <summary>
    /// Plays back all buffered operations to create the entities into the real world. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        foreach (var kvp in _archetypes)
        {
            var commandBufferArchetype = kvp.Value;
            var components = commandBufferArchetype.Components;
            
            var archetype = World.GetOrCreate(commandBufferArchetype.Types);
            World.Reserve(commandBufferArchetype.Types, commandBufferArchetype.Size);
            
            // Create buffered entities into the real world and copy their components. 
            for (var i = 0; i < commandBufferArchetype.Size; i++)
            {
                var entity = World.Create(commandBufferArchetype.Types);
                ref var chunk = ref archetype.GetChunk(in entity);
                var entityIndex = chunk.EntityIdToIndex[entity.EntityId];
                
                // Move/Copy components to the new chunk
                for (var j = 0; j < components.Length; j++)
                {
                    var sourceArray = components[j];
                    var desArray = chunk.Components[j];
                    Array.Copy(sourceArray, i, desArray, entityIndex, 1);
                }
            }
        } 
    }
}