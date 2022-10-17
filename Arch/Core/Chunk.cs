using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core.Utils;

namespace Arch.Core; 

/// <summary>
/// Represents a chunk which stores <see cref="Entities"/> and their Components.
/// Has a size of <see cref="TOTAL_CAPACITY"/>, about 16KB memory and uses multiple arrays since low level memory allocation doesnt allow managed structs.
/// Should nevertheless be cache friendly since the arrays fit perfectly into the cache upon iteration.
/// /// <example>
/// [
///     [Health, Health, Health]
///     [Transform, Transform, Transform]
///     ...
/// ]
/// </example>
/// </summary>
public struct Chunk{

    public Chunk(int capacity, params Type[] types) {

        // Calculate capacity & init arrays
        Capacity = capacity;
        Entities = new Entity[Capacity];     
        Components = new Array[types.Length];
        
        // Init mapping
        ComponentIdToArrayIndex = new Dictionary<int, int>(types.Length);
        EntityIdToIndex = new Dictionary<int, int>(Capacity);

        // Allocate arrays and map 
        for (var index = 0; index < types.Length; index++) {

            var type = types[index];
            var componentId = Component.Id(type);
            
            ComponentIdToArrayIndex[componentId] = index;
            Components[index] = Array.CreateInstance(type, Capacity);
        }
        
        Size = 0;
    }

    /// <summary>
    /// Adds an <see cref="Entity"/> to this chunk.
    /// Increases the <see cref="Size"/>. 
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(in Entity entity) {

        if(Size >= Capacity)
            return;

        EntityIdToIndex[entity.EntityId] = Size;
        Entities[Size] = entity;
        Size++;
    }
    
    /// <summary>
    /// Sets an component into the fitting component array at an index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="cmp">The component</param>
    /// <typeparam name="T">The type</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in int index, in T cmp) {

        var array = GetArray<T>();
        var entityIndex = EntityIdToIndex[index];
        array[entityIndex] = cmp;
    }

    /// <summary>
    /// Checks wether this chunk contains an array of the type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>() {
        
        var id = Component<T>.Id;
        return ComponentIdToArrayIndex.ContainsKey(id);
    }
    
    /// <summary>
    /// Checks wether this chunk contains an certain entity.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(in Entity entity) {
        return EntityIdToIndex.ContainsKey(entity.EntityId);
    }
    
    /// <summary>
    /// Returns an component from the fitting component array by its index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>The component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(in int index) {

        var array = GetArray<T>();
        var entityIndex = EntityIdToIndex[index];
        return ref array[entityIndex];
    }
    
    /// <summary>
    /// Removes an <see cref="Entity"/> from this chunk and all its components. 
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(in Entity entity) {

        // Current entity
        var entityID = entity.EntityId;
        var index = EntityIdToIndex[entityID];
            
        // Last entity in archetype. 
        var lastIndex = Size-1;
        var lastEntity = Entities[lastIndex];
        var lastEntityId =  lastEntity.EntityId;
            
        // Copy last entity to replace the removed one
        Entities[index] = lastEntity;
        for (var i = 0; i < Components.Length; i++) {

            var array = Components[i];
            Array.Copy(array, lastIndex, array, index, 1);
        }
            
        // Update the mapping
        EntityIdToIndex[lastEntityId] = index;
        EntityIdToIndex.Remove(entityID);
        Size--;
    }
    
    /// <summary>
    /// Moves the last entity from the chunk into the current chunk and fills/replaces an index. 
    /// </summary>
    /// <param name="chunk"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int ReplaceIndexWithLastEntityFrom(int index, ref Chunk chunk) {

        // Get last entity
        var lastIndex = chunk.Size-1;
        var lastEntity = chunk.Entities[lastIndex];

        // Replace index entity with the last entity from 
        Entities[index] = lastEntity;
        EntityIdToIndex[lastEntity.EntityId] = index;

        // Move/Copy components to the new chunk
        for (var i = 0; i < Components.Length; i++) {

            var sourceArray = chunk.Components[i];
            var desArray = Components[i];
            Array.Copy(sourceArray, lastIndex, desArray, index, 1);
        }
        
        // Remove last entity from this chunk
        chunk.Remove(lastEntity);
        return lastEntity.EntityId;
    }
    
    /// <summary>
    /// Returns the index of the component array inside the structure of arrays. 
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int Index<T>() {

        var id = Component<T>.Id;
        if (ComponentIdToArrayIndex.TryGetValue(id, out var index))
            return index;
        
        return -1;
    }
    
    /// <summary>
    /// Returns the internal array for the passed component
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns>The array of the certain component stored in the <see cref="Archetype"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] GetArray<T>() {

        var index = Index<T>();
        return Components[index] as T[];
    }

    /// <summary>
    /// The entities in this chunk.
    /// </summary>
    public Entity[] Entities { get; set; }
    
    /// <summary>
    /// The entity components in this chunk.
    /// </summary>
    public Array[] Components { get; set; }
    
    /// <summary>
    /// A map to get the index of a component array inside <see cref="Components"/>.
    /// </summary>
    public Dictionary<int, int> ComponentIdToArrayIndex { get; set; }
    
    /// <summary>
    /// A map used to get the array indexes of a certain <see cref="Entity"/>.
    /// </summary>
    public Dictionary<int, int> EntityIdToIndex { get; set; }
    
    /// <summary>
    /// The current size/occupation of this chunk.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// The total capacity, how many entities fit in here.
    /// </summary>
    public int Capacity { get; private set; }
}