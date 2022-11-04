using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;

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
public partial struct Chunk {

    /// <summary>
    /// Allocates enough space for the passed amount of entities with all its components. 
    /// </summary>
    /// <param name="capacity"></param>
    /// <param name="types"></param>
    internal Chunk(int capacity, params Type[] types) {

        // Calculate capacity & init arrays
        Size = 0;
        Capacity = capacity;
        
        Entities = new Entity[Capacity];     
        Components = new Array[types.Length];
        
        // Init mapping
        ComponentIdToArrayIndex = new Dictionary<int, int>(types.Length);
        EntityIdToIndex = new Dictionary<int, int>(Capacity);

        // Allocate arrays and map 
        for (var index = 0; index < types.Length; index++) {

            var type = types[index];
            var componentId = ComponentMeta.Id(type);
            
            ComponentIdToArrayIndex[componentId] = index;
            Components[index] = Array.CreateInstance(type, Capacity);
        }
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

        var array = GetSpan<T>();
        array[index] = cmp;
    }
    
    /// <summary>
    /// Sets an component into the fitting component array for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <param name="cmp">The component</param>
    /// <typeparam name="T">The type</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T cmp) {

        var array = GetSpan<T>();
        var entityIndex = EntityIdToIndex[entity.EntityId];
        array[entityIndex] = cmp;
    }

    /// <summary>
    /// Checks wether this chunk contains an array of the type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool Has<T>() {
        
        var id = ComponentMeta<T>.Id;
        return ComponentIdToArrayIndex.ContainsKey(id);
    }
    
    /// <summary>
    /// Checks wether this chunk contains an certain entity.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
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
    [Pure]
    public ref T Get<T>(in int index) {

        var array = GetSpan<T>();
        return ref array[index];
    }
    
    /// <summary>
    /// Returns an component from the fitting component array for an entity
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>The component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T Get<T>(in Entity entity) {

        var array = GetSpan<T>();
        var entityIndex = EntityIdToIndex[entity.EntityId];
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
    /// The entities in this chunk.
    /// </summary>
    public readonly Entity[] Entities { [Pure] get; }
    
    /// <summary>
    /// The entity components in this chunk.
    /// </summary>
    public readonly Array[] Components { [Pure] get; }
    
    /// <summary>
    /// A map to get the index of a component array inside <see cref="Components"/>.
    /// </summary>
    public readonly Dictionary<int, int> ComponentIdToArrayIndex { [Pure] get; }
    
    /// <summary>
    /// A map used to get the array indexes of a certain <see cref="Entity"/>.
    /// </summary>
    public readonly Dictionary<int, int> EntityIdToIndex { [Pure] get; }

    /// <summary>
    /// The current size/occupation of this chunk.
    /// </summary>
    public int Size { [Pure] get; private set; }
    
    /// <summary>
    /// The total capacity, how many entities fit in here.
    /// </summary>
    public int Capacity { [Pure] get; }
}

/// <summary>
/// Adds various utility methods to the chunk. 
/// </summary>
public partial struct Chunk {

    /// <summary>
    /// Returns the index of the component array inside the structure of arrays. 
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    private int Index<T>() {

        var id = ComponentMeta<T>.Id;
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
    [Pure]
    public T[] GetArray<T>() {

        var index = Index<T>();
        return Components[index] as T[];
    }
    
    /// <summary>
    /// Returns an span of the internal array for the passed component
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns>The array of the certain component stored in the <see cref="Archetype"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpan<T>() {
        return new Span<T>(GetArray<T>(), 0, Size);
    }

    /// <summary>
    /// Returns a ref to the first element of the component array in an safe way.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirst<T>() {
        return ref GetSpan<T>()[0];  // Span to avoid bound checking for the [] operation
    }
    
    /// <summary>
    /// Returns an the internal array for the passed component.
    /// Uses unsafe operations to avoid bound checks. 
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns>The array of the certain component stored in the <see cref="Archetype"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public T[] GetArrayUnsafe<T>() {
      
        var index = Index<T>();
        ref var array = ref ArrayExtensions.DangerousGetReferenceAt(Components, index);
        return Unsafe.As<T[]>(array);
    }
    
    /// <summary>
    /// Returns an span to the internal array for the passed component.
    /// Uses unsafe operations to avoid bound checks. 
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns>The array of the certain component stored in the <see cref="Archetype"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpanUnsafe<T>() {
        return new Span<T>(GetArrayUnsafe<T>());
    }
    
    /// <summary>
    /// Returns a ref to the first element of the component array in an sunsafe way.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirstUnsafe<T>() {
        return ref ArrayExtensions.DangerousGetReference(GetArrayUnsafe<T>());
    }
    
    /// <summary>
    /// Moves the last entity from the chunk into the current chunk and fills/replaces an index. 
    /// </summary>
    /// <param name="chunk"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
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
}