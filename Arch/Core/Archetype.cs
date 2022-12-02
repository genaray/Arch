using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Text;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core;

/// <summary>
///     An archetype, stores all <see cref="Entity" />'s with the same set of components, tightly packed in <see cref="Chunks" />.
/// </summary>
public sealed unsafe partial class Archetype
{
    public const int TotalCapacity = 16000; // 16KB, fits perfectly into one L1 Cache

    internal Archetype(params Type[] types)
    {
        Types = types;
        EntitiesPerChunk = CalculateEntitiesPerChunk(types);

        // The bitmask/set 
        BitSet = types.ToBitSet();
        ComponentIdToArrayIndex = types.ToLookupArray();
        
        EntityIdToChunkIndex = new PooledDictionary<int, int>(EntitiesPerChunk);
        Chunks = Array.Empty<Chunk>();
    }

    /// <summary>
    ///     The types with which the <see cref="BitSet" /> was created.
    /// </summary>
    public Type[] Types { get; }

    /// <summary>
    ///     The bitmask for querying, contains the component flags set for this archetype.
    /// </summary>
    public BitSet BitSet { get; }

    /// <summary>
    /// A lookup table which maps a component-id to its array index in the <see cref="Chunk"/>.
    /// Stored here since it reduces the amount of memory, better than every chunk having one of these. 
    /// </summary>
    public int[] ComponentIdToArrayIndex { get; }

    /// <summary>
    ///     For mapping the entity id to the chunk it is in.
    /// </summary>
    public PooledDictionary<int, int> EntityIdToChunkIndex { get; }

    /// <summary>
    ///     A array of active chunks within this archetype.
    /// </summary>
    public Chunk[] Chunks { get; private set; }

    /// <summary>
    ///     Returns the last chunk from the <see cref="Chunks" />
    /// </summary>
    public ref Chunk LastChunk => ref Chunks[Size - 1];

    /// <summary>
    ///     The chunk capacity, how many chunks are there in total.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    ///     Indicates how many full chunks are currently being used.
    ///     Partial empty chunks do not count.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     The amount of entities fitting in each chunk.
    /// </summary>
    public int EntitiesPerChunk { get; }

    /// <summary>
    /// Searches for the next free <see cref="Chunk"/> to insert an <see cref="Entity"/> and returns its index. 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int NextChunkIndex()
    {
        
        // If there reserved chunks, but no used yet... fill them 
        if (Capacity <= 0 || Capacity < Size) return -1;
        
        ref var lastChunk = ref LastChunk;
        if (lastChunk.Size < lastChunk.Capacity) return Size-1;
            
        // Last chunk is already full and capaccity still available, move to next chunk
        if (Capacity <= Size) return -1;
        Size++;
        return NextChunkIndex();
    }
    
    /// <summary>
    ///     Adds an <see cref="Entity" /> to this chunk.
    ///     Will fill existing allocated <see cref="Chunk" />'s till all of them are full, then it will start to allocate a new chunk and fill that one again.
    /// </summary>
    /// <param name="entity">The entity to add to this archetype</param>
    /// <returns>True if a new chunk was created</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Add(in Entity entity)
    {

        // Get next available chunk which has free space... 
        var nextChunkIndex = NextChunkIndex();
        if (nextChunkIndex != -1)
        {
            ref var lastChunk = ref Chunks[nextChunkIndex];
            lastChunk.Add(in entity);
            EntityIdToChunkIndex[entity.EntityId] = Size - 1;

            // Last chunk was filled, still capacity, increase size to make next entity fill reserved chunk 
            if (lastChunk.Size == EntitiesPerChunk && Capacity >= Size + 1)
            {
                Size++;
                return false;
            }
            return false;
        }

        // Create new chunk
        var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
        newChunk.Add(in entity);

        // Resize chunks
        EnsureOrTrimCapacity(Size + 1);

        // Add chunk & map entity
        Chunks[Size] = newChunk;
        EntityIdToChunkIndex[entity.EntityId] = Size;
        Capacity++;
        Size++;

        return true;
    }
    
    /// <summary>
    ///     Removes an <see cref="Entity" /> from this chunk and all its components.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if a chunk was destroyed</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(in Entity entity)
    {
        var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
        ref var chunk = ref Chunks[chunkIndex];

        // If its the last chunk, simply remove the entity
        if (chunkIndex == Size - 1)
        {
            chunk.Remove(entity);
            EntityIdToChunkIndex.Remove(entity.EntityId);

            if (chunk.Size != 0) return false;

            // Remove last unused chunk & resize to free memory
            EnsureOrTrimCapacity(Size - 1);
            Capacity--;
            Size--;
            return true;
        }

        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        var index = chunk.EntityIdToIndex[entity.EntityId];
        var movedEntityId = chunk.ReplaceIndexWithLastEntityFrom(index, ref LastChunk);
        EntityIdToChunkIndex.Remove(entity.EntityId);
        EntityIdToChunkIndex[movedEntityId] = chunkIndex;

        if (LastChunk.Size != 0) return false;

        // Remove last unused chunk & resize to free memory
        EnsureOrTrimCapacity(Size - 1);
        Capacity--;
        Size--;
        return true;
    }
    
    /// <summary>
    ///     Sets an component into the fitting component array for an entity.
    /// </summary>
    /// <param name="entity">The index</param>
    /// <param name="cmp">The component</param>
    /// <typeparam name="T">The type</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T cmp)
    { 
        var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
        ref var chunk = ref Chunks[chunkIndex];
        chunk.Set(in entity, in cmp);
    }

    /// <summary>
    ///     Checks wether this chunk contains an array of the type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>()
    { 
        var id = ComponentMeta<T>.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Returns an component from the fitting component array for its entity.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>The component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(in Entity entity)
    {
        var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
        ref var chunk = ref Chunks[chunkIndex];
        return ref chunk.Get<T>(in entity);
    }

    /// <summary>
    /// Returns the <see cref="Chunk"/> for the passed <see cref="Entity"/> within this archetype.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A referecene to the chunk in which the passed entity is stored in.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Chunk GetChunk(in Entity entity)
    {
        var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
        return ref Chunks[chunkIndex];
    }

    /// <summary>
    ///     Returns a <see cref="Enumerator{T}" /> for <see cref="Chunks" /> to iterate over all chunks in this archetype.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator<Chunk> GetEnumerator()
    {
        return new Enumerator<Chunk>(Chunks.AsSpan(), Size);
    }

    /// <summary>
    ///     Calculates how many entities with the types fit into one chunk.
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static int CalculateEntitiesPerChunk(Type[] types)
    {
        return TotalCapacity / (sizeof(Entity) + types.ToByteSize());
    }
}


/// <summary>
///     Adds capacity related methods.
/// </summary>
public sealed partial class Archetype
{
    /// <summary>
    ///     Sets the capacity and either makes the internal <see cref="Chunks" /> and <see cref="EntityIdToChunkIndex" /> arrays bigger or smaller.
    /// </summary>
    /// <param name="newCapacity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureOrTrimCapacity(int newCapacity)
    {
        // More size needed
        if (newCapacity > Capacity)
        {
            // Increase chunk array size
            var newChunks = ArrayPool<Chunk>.Shared.Rent(newCapacity);
            Array.Copy(Chunks, newChunks, Size);
            ArrayPool<Chunk>.Shared.Return(Chunks, true);
            Chunks = newChunks;

            // Increase mapping
            EntityIdToChunkIndex.EnsureCapacity(newCapacity * EntitiesPerChunk);
        }
        else
        {
            // Always keep capacity for atleast one chunk
            if (newCapacity <= 0) newCapacity = 1;

            // Decrease chunk size
            var newChunks = ArrayPool<Chunk>.Shared.Rent(newCapacity);
            Array.Copy(Chunks, newChunks, Size - 1);
            ArrayPool<Chunk>.Shared.Return(Chunks, true);
            Chunks = newChunks;

            // Decrease mapping
            EntityIdToChunkIndex.TrimExcess(newCapacity * EntitiesPerChunk);
        }
    }

    /// <summary>
    ///     Reserves memory for a specific amount of <see cref="Entity" />'s.
    ///     Highly efficient bulk adding. Once allocated, you can traverse over the arch to fill it.
    ///     Allocates on top of the existing <see cref="Capacity" />.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if a new chunk was created</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(in int amount)
    {
        // Put into the last partial empty chunk 
        if (Size > 0)
        {
            // Calculate amount of required chunks
            ref var lastChunk = ref LastChunk;
            var freeSpots = lastChunk.Capacity - lastChunk.Size;
            var neededSpots = amount - freeSpots;
            var neededChunks = neededSpots / EntitiesPerChunk;

            // Set capacity and insert new empty chunks
            EnsureOrTrimCapacity(Capacity + neededChunks);

            for (var index = 0; index < neededChunks; index++)
            {
                var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            Capacity += neededChunks;
        }
        else
        {
            // Allocate new chunks in one go
            var neededChunks = (int)Math.Ceiling((float)amount / EntitiesPerChunk);
            EnsureOrTrimCapacity(Capacity + neededChunks);

            for (var index = 0; index < neededChunks; index++)
            {
                var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            Capacity += neededChunks; // So many chunks are allocated
            Size = 1; // Since no other chunks are allocated... 
        }
    }
}


/// <summary>
///     Adds structural change related methods. 
/// </summary>
public sealed partial class Archetype
{
    /// <summary>
    /// Moves an <see cref="Entity"/> from this <see cref="Archetype"/> into a different one.
    /// Removes it from the current one. 
    /// </summary>
    /// <param name="entity">The entity to move.</param>
    /// <param name="toArchetype">The archetype it should move into.</param>
    /// <param name="created">If there was a new chunk created in the <see cref="toArchetype"/>.</param>
    /// <param name="destroyed">If there was a chunk destroyed in this archetype.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Move(in Entity entity, Archetype toArchetype, out bool created, out bool destroyed)
    {
        created = toArchetype.Add(in entity);
        
        var oldChunkIndex = EntityIdToChunkIndex[entity.EntityId];
        var newChunkIndex = toArchetype.EntityIdToChunkIndex[entity.EntityId];

        ref var oldChunk = ref Chunks[oldChunkIndex];
        ref var newChunk = ref toArchetype.Chunks[newChunkIndex];

        var oldIndex = oldChunk.EntityIdToIndex[entity.EntityId];
        var newIndex = newChunk.EntityIdToIndex[entity.EntityId];
        oldChunk.CopyToDifferent(oldIndex, ref newChunk, newIndex);
        
        destroyed = Remove(in entity);
    }
}
