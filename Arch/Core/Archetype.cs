using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Arch.Test;

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
        array[index] = cmp;
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
        return ref array[index];
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
        if (index < 0) return null;
        return (T[])Components[index];
    }
        
    /// <summary>
    /// Returns a span pointing to the internal stored array 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<T> GetSpan<T>() {
            
        var index = Index<T>();
        if (index < 0) return null;
        return (T[])Components[index];
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

/// <summary>
/// An archetype, stores entities with the same components, tightly packed in <see cref="Chunks"/>
/// </summary>
public sealed unsafe class Archetype {
    
    public const int TOTAL_CAPACITY = 16000; // 16KB, fits perfectly into one L1 Cache
    
    public Archetype(params Type[] types) {

        Types = types;
        EntitiesPerChunk = CalculateEntitiesPerChunk(types);
        
        // The bitmask/set 
        BitSet = BitSetExtensions.From(types);
        EntityIdToChunkIndex = new Dictionary<int, int>(EntitiesPerChunk);
        Chunks = Array.Empty<Chunk>();
    }

        /// <summary>
    /// Adds an <see cref="Entity"/> to this chunk.
    /// Increases the <see cref="Size"/>. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if a new chunk was created</returns>
    public bool Add(in Entity entity) {
            
        // Put into the last partial empty chunk 
        if (Size > 0) {
            
            ref var lastChunk = ref LastChunk;
            if (lastChunk.Size < lastChunk.Capacity) {

                lastChunk.Add(in entity);
                EntityIdToChunkIndex[entity.EntityId] = Size - 1;
                return false;
            }
        }

        // Create new chunk
        var newChunk = new Chunk(EntitiesPerChunk, Types);
        newChunk.Add(in entity);
            
        // Resize chunks
        var chunks = Chunks;
        Array.Resize(ref chunks, Size+1);
        
        // Add new chunk
        Chunks = chunks;
        Chunks[Size-1] = newChunk;

        // Resize & Map entity
        EntityIdToChunkIndex.EnsureCapacity(Size * EntitiesPerChunk);
        EntityIdToChunkIndex[entity.EntityId] = Size - 1;

        return true;
    }
    
    /// <summary>
    /// Sets an component into the fitting component array at an index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <param name="cmp">The component</param>
    /// <typeparam name="T">The type</typeparam>
    public void Set<T>(in int index, in T cmp) {

        var chunkIndex = EntityIdToChunkIndex[index];
        ref var chunk = ref Chunks[chunkIndex];
        chunk.Set(in index, in cmp);
    }

    /// <summary>
    /// Checks wether this chunk contains an array of the type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    public bool Has<T>() {

        var id = Component<T>.Id;
        return BitSet.IsSet(id);
    }
    
    /// <summary>
    /// Returns an component from the fitting component array by its index.
    /// </summary>
    /// <param name="index">The index</param>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>The component</returns>
    public ref T Get<T>(in int index) {
        
        var chunkIndex = EntityIdToChunkIndex[index];
        ref var chunk = ref Chunks[chunkIndex];
        return ref chunk.Get<T>(in index);
    }
    
    /// <summary>
    /// Removes an <see cref="Entity"/> from this chunk and all its components. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if a chunk was destroyed</returns>
    public bool Remove(in Entity entity) {
        
        var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
        ref var chunk = ref Chunks[chunkIndex];
        
        // If its the last chunk, simply remove the entity
        if (chunkIndex == Size - 1) {
            chunk.Remove(entity);
            EntityIdToChunkIndex.Remove(entity.EntityId);
            return false;
        }
        
        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        var movedEntityId = chunk.ReplaceIndexWithLastEntityFrom(entity.EntityId, ref LastChunk);
        EntityIdToChunkIndex.Remove(entity.EntityId);
        EntityIdToChunkIndex[movedEntityId] = chunkIndex;
        
        if (LastChunk.Size != 0) return false;
        
        // Remove last unused chunk & resize to free memory
        var chunks = Chunks;
        Array.Resize(ref chunks, chunks.Length - 1);
        Chunks = chunks;
        EntityIdToChunkIndex.TrimExcess(Size*EntitiesPerChunk);

        return true;
    }
    
    /// <summary>
    /// The types with which the <see cref="BitSet"/> was created.
    /// </summary>
    public Type[] Types { get; set; }
    
    /// <summary>
    /// The bitmask for querying, contains the component flags set for this archetype.
    /// </summary>
    public BitSet BitSet { get; set; }

    /// <summary>
    /// For mapping the entity id to the chunk it is in. 
    /// </summary>
    public Dictionary<int, int> EntityIdToChunkIndex { get; set; }
    
    /// <summary>
    /// A array of active chunks within this archetype. 
    /// </summary>
    public Chunk[] Chunks { get; private set; }
    
    /// <summary>
    /// Returns the last chunk from the <see cref="Chunks"/>
    /// </summary>
    public ref Chunk LastChunk => ref Chunks[Size - 1];
 
    /// <summary>
    /// The chunk size. 
    /// </summary>
    public int Size => Chunks.Length;
    
    /// <summary>
    /// The amount of entities fitting in each chunk. 
    /// </summary>
    public int EntitiesPerChunk { get; private set; }

    /// <summary>
    /// Calculates how many entities with the types fit into one chunk. 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static int CalculateEntitiesPerChunk(Type[] types) {
        return TOTAL_CAPACITY/(sizeof(Entity)+types.ToByteSize());
    }
}
