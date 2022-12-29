using System.Buffers;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Core;

// TODO: Documentation.
/// <summary>
///     The <see cref="Slot"/> struct
///     ...
/// </summary>
internal struct Slot
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Slot"/> struct
    ///     ...
    /// </summary>
    /// <param name="index"></param>
    /// <param name="chunkIndex"></param>
    public Slot(int index, int chunkIndex)
    {
        Index = index;
        ChunkIndex = chunkIndex;
    }

    // TODO: Documentation.
    public int Index;      // The entity index inside the chunk
    public int ChunkIndex; // The chunk index inside the archetype
}

// TODO: Documentation.
/// <summary>
///     The <see cref="Archetype"/> class
///     ...
/// </summary>
public sealed unsafe partial class Archetype
{
    internal const int BaseSize = 16000; // 16KB Chunk size

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Archetype"/> class
    ///     ...
    /// </summary>
    /// <param name="types"></param>
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

    // TODO: Documentation.
    public ComponentType[] Types { get; }
    public BitSet BitSet { get; }
    public int[] ComponentIdToArrayIndex { get; }
    public Chunk[] Chunks { get; private set; }
    public ref Chunk LastChunk { get => ref Chunks[Size - 1]; }
    public int Capacity { get; private set; }
    public int Size { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }
    public int EntitiesPerChunk { get; }
    public int ChunkSize { get; } = BaseSize;
    public int MinimumAmountOfEntitiesPerChunk { get; } = 100;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="slot"></param>
    /// <returns></returns>
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
            if (lastChunk.Size == lastChunk.Capacity && Size < Capacity)
            {
                Size++;
            }

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="movedEntityId"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Remove(ref Slot slot, out int movedEntityId)
    {
        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        ref var chunk = ref Chunks[slot.ChunkIndex];
        movedEntityId = chunk.Transfer(slot.Index, ref LastChunk);

        // Trim when last chunk is now empty and we havent reached the last chunk yet
        if (LastChunk.Size != 0 || Size <= 1)
        {
            return false;
        }

        EnsureOrTrimCapacity(Size - 1);
        Capacity--;
        Size--;
        return true;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="slot"></param>
    /// <param name="cmp"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Set<T>(ref Slot slot, in T cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Set(slot.Index, in cmp);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return BitSet.IsSet(id);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="slot"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal unsafe ref T Get<T>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Get<T>(in slot.Index);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Chunk GetChunk(scoped in int index)
    {
        return ref Chunks[index];
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator<Chunk> GetEnumerator()
    {
        return new Enumerator<Chunk>(Chunks.AsSpan(), Size);
    }
}

public sealed unsafe partial class Archetype
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public int MinimumRequiredChunkSize(ComponentType[] types)
    {
        var minimumEntities = (sizeof(Entity) + types.ToByteSize()) * MinimumAmountOfEntitiesPerChunk;
        return (int)Math.Ceiling((float)minimumEntities / BaseSize) * BaseSize;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public int CalculateEntitiesPerChunk(ComponentType[] types)
    {
        return ChunkSize / (sizeof(Entity) + types.ToByteSize());
    }

    // TODO: Documentation.
    /// <summary>
    /// 
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
        else if (newCapacity < Capacity)
        {
            // Always keep capacity for atleast one chunk
            newCapacity = newCapacity <= 0 ? 1 : newCapacity;

            // Decrease chunk size
            var newChunks = ArrayPool<Chunk>.Shared.Rent(newCapacity);
            Array.Copy(Chunks, newChunks, Size - 1);
            ArrayPool<Chunk>.Shared.Return(Chunks, true);
            Chunks = newChunks;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Reserve(in int amount)
    {
        // Put into the last partial empty chunk.
        if (Size > 0)
        {
            // Calculate amount of required chunks.
            ref var lastChunk = ref LastChunk;
            var freeSpots = lastChunk.Capacity - lastChunk.Size;
            var neededSpots = amount - freeSpots;
            var neededChunks = (int)Math.Ceiling((float)neededSpots / EntitiesPerChunk);

            // Set capacity and insert new empty chunks.
            EnsureOrTrimCapacity(Capacity + neededChunks);
            for (var index = 0; index < neededChunks; index++)
            {
                var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            // If last chunk was full, add.
            if (freeSpots == 0)
            {
                Size++;
            }

            Capacity += neededChunks;
        }
        else
        {
            // Allocate new chunks in one go.
            var neededChunks = (int)Math.Ceiling((float)amount / EntitiesPerChunk);
            EnsureOrTrimCapacity(Capacity + neededChunks);

            for (var index = 0; index < neededChunks; index++)
            {
                var newChunk = new Chunk(EntitiesPerChunk, ComponentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            Capacity += neededChunks; // So many chunks are allocated.
        }
    }
}

public sealed partial class Archetype
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="fromSlot"></param>
    /// <param name="toSlot"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void CopyTo(Archetype to, ref Slot fromSlot, ref Slot toSlot)
    {
        // Copy items from old to new chunk 
        ref var oldChunk = ref GetChunk(fromSlot.ChunkIndex);
        ref var newChunk = ref to.GetChunk(toSlot.ChunkIndex);
        oldChunk.CopyToDifferent(ref newChunk, fromSlot.Index, toSlot.Index);
    }
}
