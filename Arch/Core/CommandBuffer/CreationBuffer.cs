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
    internal Type[] _types;
    
    internal int _size;
    internal int _capacity;
    
    internal Array[] _components;
    internal int[] _lookupArray; 

    internal LinearArchetype(Type[] types, int capacity = 64)
    {
        _types = types;
        
        // Allocate arrays and map 
        _lookupArray = types.ToLookupArray();
        _components = new Array[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            _components[index] = Array.CreateInstance(type, capacity);
        }

        _capacity = capacity;
    }

    /// <summary>
    /// Adds an row/entity to the archetype.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add()
    {
        // Increase internal capacities when full
        if (_size >= _capacity)
        {
            for (var i = 0; i < _components.Length; i++)
            {
                var type = _types[i];
                var components = _components[i];
                var newArray = Array.CreateInstance(type, _capacity * 2);
                components.CopyTo(newArray, 0);
                _components[i] = newArray;
            }
        }
        
        var index = _size;
        _size++;
        return index;
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
        var array = Unsafe.As<T[]>(_components[arrayIndex]);

        lock (array)
        { 
            array[index] = component;
        }
    }
}

/// <summary>
/// A buffer used to buffer the creation of entities. 
/// </summary>
public struct CreationBuffer
{
    
    /// <summary>
    /// Represents a created entity. 
    /// </summary>
    public readonly ref struct CreatedEntity
    {
        internal readonly int _index;
        internal readonly LinearArchetype _archetype;

        internal CreatedEntity(int index, LinearArchetype archetype)
        {
            _index = index;
            _archetype = archetype;
        }
    }
    
    internal World _world;
    internal Dictionary<int, LinearArchetype> _archetypes;
    
    internal CreationBuffer(World world)
    {
        _world = world;
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
        var hash = ComponentMeta.GetHashCode(types);
        if (_archetypes.TryGetValue(hash, out archetype)) return;
        
        archetype = new LinearArchetype(types);
        _archetypes[hash] = archetype;
    }
    
    /// <summary>
    /// Creates an entity by a set of types. 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CreatedEntity Create(Type[] types)
    {
        
        GetOrCreateArchetype(types, out var archetype);
        var archetypeIndex = archetype.Add();
        return new CreatedEntity(archetypeIndex, archetype);
    }

    /// <summary>
    /// Sets a component for a created entity. 
    /// </summary>
    /// <param name="createdEntity"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in CreatedEntity createdEntity, in T component)
    {
        var archetype = createdEntity._archetype;
        archetype.Set(createdEntity._index, in component);
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
            var components = commandBufferArchetype._components;
            
            var archetype = _world.GetOrCreate(commandBufferArchetype._types);
            _world.Reserve(commandBufferArchetype._types, commandBufferArchetype._size);
            
            // Create buffered entities into the real world and copy their components. 
            for (var i = 0; i < commandBufferArchetype._size; i++)
            {
                var entity = _world.Create(commandBufferArchetype._types);
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