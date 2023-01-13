using System.Buffers;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Core;

/// <summary>
///     The <see cref="Slot"/> struct references an <see cref="Entity"/> entry within an <see cref="Archetype"/> using a reference to its <see cref="Chunk"/> and its index.
/// </summary>
[SkipLocalsInit]
internal record struct Slot
{
    /// <summary>
    ///     The index of the <see cref="Entity"/> in the <see cref="Chunk"/>.
    /// </summary>
    public int Index;

    /// <summary>
    ///     The index of the <see cref="Chunk"/> in which the <see cref="Entity"/> is located.
    /// </summary>
    public int ChunkIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Slot"/> struct.
    /// </summary>
    /// <param name="index">The index of the <see cref="Entity"/> in the <see cref="Chunk"/>.</param>
    /// <param name="chunkIndex">The index of the <see cref="Chunk"/> in which the <see cref="Entity"/> is located.</param>
    public Slot(int index, int chunkIndex)
    {
        Index = index;
        ChunkIndex = chunkIndex;
    }
}

/// <summary>
///     The <see cref="Archetype"/> class contains all <see cref="Entity"/>'s of a unique combination of component types.
///     These are stored in multiple <see cref="Chunk"/>'s located within the <see cref="Chunks"/>-Array.
///     The <see cref="Archetype"/> class provides several methods to manage its stored <see cref="Entity"/>'s and their <see cref="Chunk"/>'s.
/// </summary>
public sealed partial class Archetype
{
    /// <summary>
    ///     The minimum size of a regular L1 cache.
    /// </summary>
    internal const int BaseSize = 16000; // 16KB Chunk size

    /// <summary>
    ///     A lookup array that maps the component id to an index within the component array of a <see cref="Chunk"/> to quickly find the correct array for the component type.
    ///     Is being stored here since all <see cref="Chunks"/> share the same instance to reduce allocations.
    /// </summary>
    private readonly int[] _componentIdToArrayIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Archetype"/> class by a group of components.
    /// </summary>
    /// <param name="types">The component structure of the <see cref="Entity"/>'s that can be stored in this <see cref="Archetype"/>.</param>
    internal Archetype(params ComponentType[] types)
    {
        Types = types;

        // Calculations
        ChunkSize = MinimumRequiredChunkSize(types);
        EntitiesPerChunk = CalculateEntitiesPerChunk(types);

        // The bitmask/set
        BitSet = types.ToBitSet();
        _componentIdToArrayIndex = types.ToLookupArray();

        // Setup arrays and mappings
        Chunks = ArrayPool<Chunk>.Shared.Rent(1);
        Chunks[0] = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, types);

        Size = 1;
        Capacity = 1;
    }

    /// <summary>
    ///     The component types that the <see cref="Entity"/>'s stored here have.
    /// </summary>
    public ComponentType[] Types { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    ///     A bitset representation of the <see cref="Types"/> array for fast lookups and queries.
    /// </summary>
    public BitSet BitSet { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    ///     The number of entities that are stored per <see cref="Chunk"/>.
    /// </summary>
    public int EntitiesPerChunk { get; }

    /// <summary>
    ///     The size of a <see cref="Chunk"/> within the <see cref="Chunks"/> in KB.
    ///     Necessary because the <see cref="Archetype"/> adjusts the size of a <see cref="Chunk"/>.
    /// </summary>
    public int ChunkSize { get; } = BaseSize;

    /// <summary>
    ///     The minimum number of <see cref="Entity"/>'s that should fit into a <see cref="Chunk"/> within this <see cref="Archetype"/>.
    ///     On the basis of this, the <see cref="ChunkSize"/> is increased.
    /// </summary>
    public int MinimumAmountOfEntitiesPerChunk { get; } = 100;

    /// <summary>
    ///     How many <see cref="Chunk"/>' have been deposited within the <see cref="Chunks"/> array.
    ///     The total capacity.
    /// </summary>
    public int Capacity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }

    /// <summary>
    ///     The number of occupied/used <see cref="Chunk"/>'s within the <see cref="Chunks"/> array.
    /// </summary>
    public int Size { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }

    /// <summary>
    ///     An array which stores the <see cref="Chunk"/>'s.
    ///     May contain null references since its being pooled, therefore use the <see cref="Size"/> and <see cref="Capacity"/> for acessing it.
    /// </summary>
    public Chunk[] Chunks { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; }

    /// <summary>
    ///     Points to the last <see cref="Chunk"/> that is not yet full.
    /// </summary>
    private ref Chunk LastChunk { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref Chunks[Size - 1]; }

    /// <summary>
    ///     Adds an <see cref="Entity"/> to the <see cref="Archetype"/> and offloads it to a <see cref="Chunk"/>.
    ///     Uses the last <see cref="Chunk"/> that is not full, once it is full and the capacity is exhausted, a new <see cref="Chunk"/> is allocated.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> that is added.</param>
    /// <param name="slot">The <see cref="Slot"/> in which it was deposited.</param>
    /// <returns>True if a new <see cref="Chunk"/> was allocated, otherwhise false.</returns>
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
        var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
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
    ///     Removes an <see cref="Entity"/> from a <see cref="Slot"/> and moves the last <see cref="Entity"/> of the <see cref="Archetype"/> to its position.
    /// </summary>
    /// <param name="slot">The slot of the <see cref="Entity"/> to be removed.</param>
    /// <param name="movedEntityId">The id of the <see cref="Entity"/> that was moved to the position of the deleted <see cref="Entity"/>.</param>
    /// <returns>True if a <see cref="Chunk"/> was deleted, otherwhise false.</returns>
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

    /// <summary>
    ///     Sets or replaces the components of an <see cref="Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/> at which the component of an <see cref="Entity"/> is to be set or replaced.</param>
    /// <param name="cmp">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Set<T>(ref Slot slot, in T cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Set(slot.Index, in cmp);
    }

    /// <summary>
    ///      Checks if the <see cref="Archetype"/> stores <see cref="Entity"/>'s with a specific component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>True if the <see cref="Archetype"/> stores <see cref="Entity"/>'s with such a component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Returns a reference of the component of an <see cref="Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref T Get<T>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Get<T>(in slot.Index);
    }

    /// NOTE: Causes bounds check, any way to avoid that ?
    /// <summary>
    ///     Returns a reference to a given <see cref="Chunk"/> using its index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A reference to the <see cref="Chunk"/> at the given index.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Chunk GetChunk(scoped in int index)
    {
        return ref Chunks[index];
    }

    /// <summary>
    ///     Creates an <see cref="Enumerator{T}"/> which iterates over all <see cref="Chunks"/> in this <see cref="Archetype"/>.
    /// </summary>
    /// <returns>An <see cref="Enumerator{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator<Chunk> GetEnumerator()
    {
        return new Enumerator<Chunk>(Chunks.AsSpan(), Size);
    }
}

public sealed unsafe partial class Archetype
{

    /// <summary>
    ///     Calculates how many <see cref="Chunk"/>'s are needed to fulfill the <see cref="MinimumAmountOfEntitiesPerChunk"/>.
    /// </summary>
    /// <param name="types">The component structure of the <see cref="Entity"/>'s.</param>
    /// <returns>The amount of <see cref="Chunk"/>'s required.</returns>
    public int MinimumRequiredChunkSize(ComponentType[] types)
    {
        var minimumEntities = (sizeof(Entity) + types.ToByteSize()) * MinimumAmountOfEntitiesPerChunk;
        return (int)Math.Ceiling((float)minimumEntities / BaseSize) * BaseSize;
    }

    /// <summary>
    ///     Calculates how many <see cref="Entity"/>'s fit into one <see cref="Chunk"/>.
    /// </summary>
    /// <param name="types">The component structure of the <see cref="Entity"/>'s.</param>
    /// <returns>The amount of <see cref="Entity"/>'s.</returns>
    public int CalculateEntitiesPerChunk(ComponentType[] types)
    {
        return ChunkSize / (sizeof(Entity) + types.ToByteSize());
    }

    /// <summary>
    ///     Ensures or trims the capacity of the <see cref="Chunks"/> array.
    /// </summary>
    /// <param name="newCapacity">The amount of <see cref="Chunk"/>'s required, in total.</param>
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

    /// <summary>
    ///     Reserves space for a certain number of <see cref="Entity"/>'s in addition to the already existing amount.
    /// </summary>
    /// <param name="amount">The amount of new <see cref="Entity"/>'s.</param>
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
                var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
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
                var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
                Chunks[Capacity + index] = newChunk;
            }

            Capacity += neededChunks; // So many chunks are allocated.
        }
    }
}

public sealed partial class Archetype
{

    /// <summary>
    ///     Copies an <see cref="Entity"/> from a <see cref="Slot"/> within this <see cref="Archetype"/> to a <see cref="Slot"/> within another <see cref="Archetype"/> .
    /// </summary>
    /// <param name="to">The <see cref="Archetype"/> into which the <see cref="Entity"/> should move.</param>
    /// <param name="fromSlot">The <see cref="Slot"/> that targets the <see cref="Entity"/> that should move.</param>
    /// <param name="toSlot">The <see cref="Slot"/> to which the <see cref="Entity"/> should move.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void CopyTo(Archetype to, ref Slot fromSlot, ref Slot toSlot)
    {
        // Copy items from old to new chunk
        ref var oldChunk = ref GetChunk(fromSlot.ChunkIndex);
        ref var newChunk = ref to.GetChunk(toSlot.ChunkIndex);
        oldChunk.CopyToDifferent(ref newChunk, fromSlot.Index, toSlot.Index);
    }
}
