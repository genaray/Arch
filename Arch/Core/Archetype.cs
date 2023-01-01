using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core;

/// <summary>
/// A entity slot inside the archetype.
/// </summary>
internal struct Slot
{
    public int Index;        // The entity index inside the chunk
    public int ChunkIndex;   // The chunk index inside the archetype 
    public Slot(int index, int chunkIndex)
    {
        Index = index;
        ChunkIndex = chunkIndex;
    }
}

/// <summary>
///     An archetype, stores all <see cref="Entity" />'s with the same set of components, tightly packed in <see cref="Chunks" />.
/// </summary>
public sealed unsafe partial class Archetype
{
    internal const int BaseSize = 16000; // 16KB Chunk size

    internal Archetype(params ComponentType[] types)
    {
        Types = types;

        // Calculations
        ChunkSize = MinimumRequiredChunkSize(types);
        EntitiesPerChunk = CalculateEntitiesPerChunk(types);

        // The bitmask/set 
        BitSet = types.ToBitSet();
        ComponentIdToArrayIndex = types.ToLookupArray();
        
        // Setup arrays and mappings
        Chunks = ArrayPool<Chunk>.Shared.Rent(1);
        Chunks[0] = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, types);

        Size = 1;
        Capacity = 1;
    }

    /// <summary>
    ///     The types with which the <see cref="BitSet" /> was created.
    /// </summary>
    public ComponentType[] Types { get; }

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
    ///     A array of active chunks within this archetype.
    /// </summary>
    public Chunk[] Chunks { get; private set; }

    /// <summary>
    ///     Returns the last chunk from the <see cref="Chunks" />
    /// </summary>
    public ref Chunk LastChunk => ref Chunks[Size-1];

    /// <summary>
    ///     The chunk capacity, how many chunks are there in total.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    ///     Indicates how many full chunks are currently being used.
    ///     Partial empty chunks do not count.
    /// </summary>
    public int Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] 
        get; 
        private set;
    }

    /// <summary>
    ///     The amount of entities fitting in each chunk.
    /// </summary>
    public int EntitiesPerChunk { get; }

    /// <summary>
    /// The actual chunk size of each chunk of this archetype, based on <see cref="MinimumRequiredChunkSize"/>
    /// </summary>
    public int ChunkSize { get; } = BaseSize;
    
    /// <summary>
    /// The minimum amount of entities per chunk in this archetype.
    /// 
    /// </summary>
    public int MinimumAmountOfEntitiesPerChunk { get; } = 100;

    /// <summary>
    ///     Adds an <see cref="Entity" /> to this chunk.
    ///     Will fill existing allocated <see cref="Chunk" />'s till all of them are full, then it will start to allocate a new chunk and fill that one again.
    /// </summary>
    /// <param name="entity">The entity to add to this archetype</param>
    /// <returns>True if a new chunk was created</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Add(in Entity entity, out Slot slot)
    {
        // Fill existing chunk
        ref var lastChunk = ref LastChunk;
        if (lastChunk.Size != lastChunk.Capacity)
        {
            slot.Index = lastChunk.Add(in entity);
            slot.ChunkIndex = Size - 1;

            // Chunk full, use existing capacity 
            if (lastChunk.Size == lastChunk.Capacity && Size < Capacity) Size++;
            return false;
        }
        
        // Create new chunk
        var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
        slot.Index = newChunk.Add(in entity);
        slot.ChunkIndex = Size;
        
        // Resize chunks & map entity
        EnsureOrTrimCapacity(Size + 1);
        Chunks[Size] = newChunk;

        // Increase capacity
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
    internal bool Remove(ref Slot slot, out int movedEntityId)
    {
        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        ref var chunk = ref Chunks[slot.ChunkIndex];
        movedEntityId = chunk.Transfer(slot.Index, ref LastChunk);
        
        // Trim when last chunk is now empty and we havent reached the last chunk yet
        if (LastChunk.Size != 0 || Size <= 1) return false;
        
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
    internal void Set<T>(ref Slot slot, in T cmp) where T : struct
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Set(slot.Index, in cmp);
    }

    /// <summary>
    ///     Checks wether this chunk contains an array of the type.
    /// </summary>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>True if it does, false if it doesnt</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>() where T : struct
    { 
        var id = Component<T>.ComponentType.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Returns an component from the fitting component array for its entity.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The type</typeparam>
    /// <returns>The component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe ref T Get<T>(scoped ref Slot slot) where T : struct
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Get<T>(in slot.Index);
    }


    /// <summary>
    /// Returns the <see cref="Chunk"/> for the passed <see cref="Entity"/> within this archetype.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A referecene to the chunk in which the passed entity is stored in.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Chunk GetChunk(scoped in int index)
    {
        return ref Chunks[index];
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
}


/// <summary>
///     Adds capacity related methods.
/// </summary>
public sealed unsafe partial class Archetype
{
    
    /// <summary>
    ///     Returns the minimum required chunk size in kilobytes which is required to fit the <see cref="MinimumAmountOfEntitiesPerChunk"/> into it.
    ///     Always a multiple of <see cref="BaseSize"/>.
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public int MinimumRequiredChunkSize(ComponentType[] types)
    {
        var minimumEntities = (sizeof(Entity)+types.ToByteSize()) * MinimumAmountOfEntitiesPerChunk;
        return (int)Math.Ceiling((float)minimumEntities / BaseSize) * BaseSize;
    }
    
    /// <summary>
    ///     Calculates how many entities with the types fit into one chunk.
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public int CalculateEntitiesPerChunk(ComponentType[] types)
    {
        return ChunkSize / (sizeof(Entity) + types.ToByteSize());
    }
    
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
        }
        else if(newCapacity < Capacity)
        {
            // Always keep capacity for atleast one chunk
            newCapacity = newCapacity <= 0 ? 1 : newCapacity;

            // Decrease chunk size
            var newChunks = ArrayPool<Chunk>.Shared.Rent(newCapacity);
            Array.Copy(Chunks, newChunks, Size-1);
            ArrayPool<Chunk>.Shared.Return(Chunks, true);
            Chunks = newChunks;
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
    internal void Reserve(in int amount)
    {
        // Put into the last partial empty chunk 
        if (Size > 0)
        {
            // Calculate amount of required chunks
            ref var lastChunk = ref LastChunk;
            var freeSpots = lastChunk.Capacity - lastChunk.Size;
            var neededSpots = amount - freeSpots;
            var neededChunks = (int)Math.Ceiling((float)neededSpots / EntitiesPerChunk);

            // Set capacity and insert new empty chunks
            EnsureOrTrimCapacity(Capacity + neededChunks);
            for (var index = 0; index < neededChunks; index++)
            {
                var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            // If last chunk was full, add 
            if (freeSpots == 0) Size++;
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
    internal void CopyTo(Archetype to, ref Slot fromSlot, ref Slot toSlot)
    {
        // Copy items from old to new chunk 
        ref var oldChunk = ref GetChunk(fromSlot.ChunkIndex);
        ref var newChunk = ref to.GetChunk(toSlot.ChunkIndex);
        oldChunk.CopyToDifferent(ref newChunk, fromSlot.Index, toSlot.Index);
    }
}
