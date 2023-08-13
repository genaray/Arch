using System.Buffers;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;

namespace Arch.Core;

/// <summary>
///     The <see cref="Slot"/> struct references an <see cref="Arch.Core.Entity"/> entry within an <see cref="Archetype"/> using a reference to its <see cref="Chunk"/> and its index.
/// </summary>
[SkipLocalsInit]
internal record struct Slot
{
    /// <summary>
    ///     The index of the <see cref="Arch.Core.Entity"/> in the <see cref="Chunk"/>.
    /// </summary>
    public int Index;

    /// <summary>
    ///     The index of the <see cref="Chunk"/> in which the <see cref="Arch.Core.Entity"/> is located.
    /// </summary>
    public int ChunkIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Slot"/> struct.
    /// </summary>
    /// <param name="index">The index of the <see cref="Arch.Core.Entity"/> in the <see cref="Chunk"/>.</param>
    /// <param name="chunkIndex">The index of the <see cref="Chunk"/> in which the <see cref="Arch.Core.Entity"/> is located.</param>
    public Slot(int index, int chunkIndex)
    {
        Index = index;
        ChunkIndex = chunkIndex;
    }

    /// <summary>
    ///     Adds a plus operator for easy calculation of new <see cref="Slot"/>. Adds the positions of both <see cref="Slot"/>s.
    /// </summary>
    /// <param name="first">The first <see cref="Slot"/>.</param>
    /// <param name="second">The second <see cref="Slot"/>.</param>
    /// <returns>The result <see cref="Slot"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Slot operator +(Slot first, Slot second)
    {
        return new Slot(first.Index + second.Index, first.ChunkIndex + second.ChunkIndex);
    }

    /// <summary>
    ///     Adds a plus plus operator for easy calculation of new <see cref="Slot"/>. Increases the index by one.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>The <see cref="Slot"/> with index increased by one..</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Slot operator ++(Slot slot)
    {
        slot.Index++;
        return slot;
    }

    /// <summary>
    ///     Validates the <see cref="Slot"/>, moves the <see cref="Slot"/> if it is outside a <see cref="Chunk.Capacity"/> to match it.
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Wrap(int capacity)
    {
        // Result outside valid chunk, wrap into next one
        if (Index < capacity)
        {
            return;
        }

        // Index outside of its chunk, so we calculate how many times a chunk fit into the index for adjusting the chunkindex to that position.
        // Floor since we do not neet a rounded value since the index is within that chunk and not the next one.
        ChunkIndex += (int)Math.Floor(Index / (float)capacity);

        // After moving the chunk index we can simply take the rest and assign it as a index.
        Index %= capacity;
    }

    /// <summary>
    ///     Moves or shifts this <see cref="Slot"/> by one slot forward.
    ///     Ensures that the slots chunkindex updated properly once the end was reached.
    /// </summary>
    /// <param name="source">The <see cref="Slot"/> to shift by one.</param>
    /// <param name="sourceCapacity">The capacity of the chunk the slot is in.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Slot Shift(ref Slot source, int sourceCapacity)
    {
        source.Index++;
        source.Wrap(sourceCapacity);
        return source;
    }

    /// <summary>
    ///     Moves or shifts the source <see cref="Slot"/> based on the destination <see cref="Slot"/> and calculates its new position.
    ///     Used for copy operations to predict where the source <see cref="Slot"/> will end up.
    /// </summary>
    /// <param name="source">The source <see cref="Slot"/>, from which we want to calculate where it lands..</param>
    /// <param name="destination">The destination <see cref="Slot"/>, a reference point at which the copy or shift operation starts.</param>
    /// <param name="sourceCapacity">The source <see cref="Chunk.Capacity"/>.</param>
    /// <param name="destinationCapacity">The destination <see cref="Chunk.Capacity"/></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Slot Shift(in Slot source, int sourceCapacity, in Slot destination, int destinationCapacity)
    {
        var freeSpot = destination;
        var resultSlot = source + freeSpot;
        resultSlot.Index += source.ChunkIndex * (sourceCapacity - destinationCapacity); // Berücksichtigen der differenz zwischen den chunks und weiter verschieben.
        resultSlot.Wrap(destinationCapacity);

        return resultSlot;
    }
}

/// <summary>
///     The <see cref="Archetype"/> class contains all <see cref="Arch.Core.Entity"/>'s of a unique combination of component types.
///     These are stored in multiple <see cref="Chunk"/>'s located within the <see cref="Chunks"/>-Array.
///     The <see cref="Archetype"/> class provides several methods to manage its stored <see cref="Arch.Core.Entity"/>'s and their <see cref="Chunk"/>'s.
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
    /// <param name="types">The component structure of the <see cref="Arch.Core.Entity"/>'s that can be stored in this <see cref="Archetype"/>.</param>
    internal Archetype(ComponentType[] types)
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

        _addEdges = new ArrayDictionary<Archetype>(EdgesArrayMaxSize);
    }

    /// <summary>
    ///     The component types that the <see cref="Arch.Core.Entity"/>'s stored here have.
    /// </summary>
    public ComponentType[] Types { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }
    
    /// <summary>
    ///     The lookup array used by this <see cref="Archetype"/>, is being passed to all its <see cref="Chunks"/> to save memory. 
    /// </summary>
    internal int[] LookupArray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _componentIdToArrayIndex;
    }

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
    ///     The minimum number of <see cref="Arch.Core.Entity"/>'s that should fit into a <see cref="Chunk"/> within this <see cref="Archetype"/>.
    ///     On the basis of this, the <see cref="ChunkSize"/> is increased.
    /// </summary>
    public int MinimumAmountOfEntitiesPerChunk { get; } = 100;

    /// <summary>
    ///     How many <see cref="Chunk"/>' have been deposited within the <see cref="Chunks"/> array.
    ///     The total capacity.
    /// </summary>
    public int Capacity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] internal set; }

    /// <summary>
    ///     The number of occupied/used <see cref="Chunk"/>'s within the <see cref="Chunks"/> array.
    /// </summary>
    public int Size { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] internal set; }

    /// <summary>
    ///     An array which stores the <see cref="Chunk"/>'s.
    ///     May contain null references since its being pooled, therefore use the <see cref="Size"/> and <see cref="Capacity"/> for acessing it.
    /// </summary>
    public Chunk[] Chunks { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; [MethodImpl(MethodImplOptions.AggressiveInlining)] internal set; }

    /// <summary>
    ///     Points to the last <see cref="Chunk"/> that is not yet full.
    /// </summary>
    private ref Chunk LastChunk { [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref Chunks[Size - 1]; }

    /// <summary>
    ///     Points to the last <see cref="Slot"/>.
    /// </summary>
    internal Slot LastSlot
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)] get
        {
            var lastRow = LastChunk.Size - 1;
            //lastRow = lastRow > 0 ? lastRow : 0; // Make sure no negative slot is returned when chunk is empty.
            return new(lastRow, Size - 1);
        }
    }

    /// <summary>
    ///     The number of <see cref="Arch.Core.Entity"/>s in this <see cref="Archetype"/>.
    /// </summary>
    public int Entities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (Size * EntitiesPerChunk) - (EntitiesPerChunk - GetChunk(Size - 1).Size);
    }

    /// <summary>
    ///     Adds an <see cref="Arch.Core.Entity"/> to the <see cref="Archetype"/> and offloads it to a <see cref="Chunk"/>.
    ///     Uses the last <see cref="Chunk"/> that is not full, once it is full and the capacity is exhausted, a new <see cref="Chunk"/> is allocated.
    /// </summary>
    /// <param name="entity">The <see cref="Arch.Core.Entity"/> that is added.</param>
    /// <param name="slot">The <see cref="Slot"/> in which it was deposited.</param>
    /// <returns>True if a new <see cref="Chunk"/> was allocated, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool Add(Entity entity, out Slot slot)
    {
        // Increase size by one if the current chunk is full and theres capcity to prevent new chunk allocation.
        ref var lastChunk = ref LastChunk;
        Size = lastChunk.Size == lastChunk.Capacity && Size < Capacity ? Size + 1 : Size;

        // Fill chunk
        lastChunk = ref LastChunk;
        if (lastChunk.Size != lastChunk.Capacity)
        {
            slot.Index = lastChunk.Add(entity);
            slot.ChunkIndex = Size - 1;

            return false;
        }

        // Create new chunk
        var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
        slot.Index = newChunk.Add(entity);
        slot.ChunkIndex = Size;

        // Resize chunks & map entity
        EnsureCapacity(Size + 1);
        Chunks[Size] = newChunk;

        // Increase size
        Size++;
        return true;
    }

    /// <summary>
    ///     Removes an <see cref="Arch.Core.Entity"/> from a <see cref="Slot"/> and moves the last <see cref="Arch.Core.Entity"/> of the <see cref="Archetype"/> to its position.
    /// </summary>
    /// <param name="slot">The slot of the <see cref="Arch.Core.Entity"/> to be removed.</param>
    /// <param name="movedEntityId">The id of the <see cref="Arch.Core.Entity"/> that was moved to the position of the deleted <see cref="Arch.Core.Entity"/>.</param>
    /// <returns>True if a <see cref="Chunk"/> was deleted, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Remove(ref Slot slot, out int movedEntityId)
    {
        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        ref var chunk = ref Chunks[slot.ChunkIndex];
        movedEntityId = chunk.Transfer(slot.Index, ref LastChunk);

        // Return to prevent that Size decreases when chunk IS not Empty and to prevent Size becoming 0 or -1.
        if (LastChunk.Size != 0 || Size <= 1)
        {
            return;
        }

        Size--;
    }

    /// <summary>
    ///     Sets or replaces the components of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/> at which the component of an <see cref="Arch.Core.Entity"/> is to be set or replaced.</param>
    /// <param name="cmp">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Set<T>(ref Slot slot, in T cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Set(slot.Index, in cmp);
    }

    /// <summary>
    ///      Checks if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with a specific component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>True if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with such a component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Returns a reference of the component of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref T Get<T>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Get<T>(slot.Index);
    }

    /// <summary>
    ///     Returns a reference of the <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the <see cref="Arch.Core.Entity"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref Entity Entity(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Entity(slot.Index);
    }

    /// NOTE: Causes bounds check, any way to avoid that ?
    /// <summary>
    ///     Returns a reference to a given <see cref="Chunk"/> using its index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A reference to the <see cref="Chunk"/> at the given index.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Chunk GetChunk(int index)
    {
        return ref Chunks[index];
    }

    /// <summary>
    ///     Sets a component value for all entities within an <see cref="Archetype"/> in a certain range of <see cref="Slot"/>s
    /// </summary>
    /// <param name="from">The <see cref="Slot"/> where we start.</param>
    /// <param name="to">The <see cref="Slot"/> where we end.</param>
    /// <param name="component">The component value.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void SetRange<T>(in Slot from, in Slot to, in T component = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var firstElement = ref chunk.GetFirst<T>();

            // Only move within the range, depening on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Size-1;
            var lower = isEnd ? to.Index : 0;

            for (var index = upper; index >= lower; --index)
            {
                ref var cmp = ref Unsafe.Add(ref firstElement, index);
                cmp = component;
            }
        }
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

    /// <summary>
    ///     Creates an <see cref="ChunkRangeEnumerator"/> which iterates over all <see cref="Chunks"/> within a range backwards.
    /// </summary>
    /// <returns>A <see cref="ChunkRangeEnumerator"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ChunkRangeIterator GetRangeIterator(int from, int to)
    {
        return new ChunkRangeIterator(this, from, to);
    }

    /// <summary>
    ///     Creates an <see cref="ChunkRangeEnumerator"/> which iterates from the last valid chunk to another <see cref="Chunks"/> within a range backwards.
    /// </summary>
    /// <returns>A <see cref="ChunkRangeEnumerator"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ChunkRangeIterator GetRangeIterator(int to)
    {
        return new ChunkRangeIterator(this, LastSlot.ChunkIndex, to);
    }

    /// <summary>
    ///     Cleares this <see cref="Archetype"/>, an efficient method to delete all <see cref="Arch.Core.Entity"/>s.
    ///     Does not dispose any resources nor modifies its <see cref="Capacity"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Size = 1;
        foreach (ref var chunk in this)
        {
            chunk.Clear();
        }
    }

    /// <summary>
    ///     Converts this <see cref="Archetype"/> to a human readable string.
    /// </summary>
    /// <returns>A string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        var types =  string.Join(",", Types.Select(p => p.Type.Name).ToArray());
        return $"Archetype {{ {nameof(Types)} = {{ {types} }}, {nameof(BitSet)} = {{ {BitSet} }}, {nameof(EntitiesPerChunk)} = {EntitiesPerChunk}, {nameof(ChunkSize)} = {ChunkSize}, {nameof(Capacity)} = {Capacity}, {nameof(Size)} = {Size}, {nameof(Entities)} = {Entities} }}";
    }
}

public sealed unsafe partial class Archetype
{

    /// <summary>
    ///     Sets or replaces the components of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/> at which the component of an <see cref="Arch.Core.Entity"/> is to be set or replaced.</param>
    /// <param name="cmp">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Set(ref Slot slot, in object cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Set(slot.Index, cmp);
    }

    /// <summary>
    ///      Checks if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with a specific component.
    /// </summary>
    /// <param name="type">The <see cref="Type"/>.</param>
    /// <returns>True if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with such a component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(ComponentType type)
    {
        var id = type.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Returns a reference of the component of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <param name="type">The component <see cref="Type"/>.</param>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal object Get(scoped ref Slot slot, ComponentType type)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get(slot.Index, type);
    }
}

// Capacity related methods

public sealed unsafe partial class Archetype
{

    /// <summary>
    ///     Calculates how many <see cref="Chunk"/>'s are needed to fulfill the <see cref="MinimumAmountOfEntitiesPerChunk"/>.
    /// </summary>
    /// <param name="types">The component structure of the <see cref="Arch.Core.Entity"/>'s.</param>
    /// <returns>The amount of <see cref="Chunk"/>'s required.</returns>
    public int MinimumRequiredChunkSize(ComponentType[] types)
    {
        var minimumEntities = (sizeof(Entity) + types.ToByteSize()) * MinimumAmountOfEntitiesPerChunk;
        return (int)Math.Ceiling((float)minimumEntities / BaseSize) * BaseSize;
    }

    /// <summary>
    ///     Calculates how many <see cref="Arch.Core.Entity"/>'s fit into one <see cref="Chunk"/>.
    /// </summary>
    /// <param name="types">The component structure of the <see cref="Arch.Core.Entity"/>'s.</param>
    /// <returns>The amount of <see cref="Arch.Core.Entity"/>'s.</returns>
    public int CalculateEntitiesPerChunk(ComponentType[] types)
    {
        return ChunkSize / (sizeof(Entity) + types.ToByteSize());
    }

    /// <summary>
    ///     Ensures the capacity of the <see cref="Chunks"/> array.
    ///     Increases the <see cref="Capacity"/>.
    /// </summary>
    /// <param name="newCapacity">The amount of <see cref="Chunk"/>'s required, in total.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureCapacity(int newCapacity)
    {
        // Increase chunk array size
        var newChunks = ArrayPool<Chunk>.Shared.Rent(newCapacity);
        Array.Copy(Chunks, newChunks, Capacity);
        ArrayPool<Chunk>.Shared.Return(Chunks, true);
        Chunks = newChunks;
        Capacity = newCapacity;
    }

    /// TODO : Currently this only ensures additional entity capacity, instead it should take the whole capacity in count.
    /// <summary>
    ///     Ensures the capacity of the <see cref="Chunks"/> array.
    ///     Increases the <see cref="Capacity"/>.
    /// </summary>
    /// <param name="newCapacity">The amount of <see cref="Chunk"/>'s required, in total.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void EnsureEntityCapacity(int newCapacity)
    {
        // Calculate amount of required chunks.
        ref var lastChunk = ref LastChunk;
        var freeSpots = lastChunk.Capacity - lastChunk.Size;
        var neededSpots = newCapacity - freeSpots;
        var neededChunks = (int)Math.Ceiling((float)neededSpots / EntitiesPerChunk);

        if (Capacity-Size > neededChunks)
        {
            return;
        }

        // Set capacity and insert new empty chunks.
        var previousCapacity = Capacity;
        EnsureCapacity(previousCapacity + neededChunks);

        for (var index = 0; index < neededChunks; index++)
        {
            var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
            Chunks[previousCapacity + index] = newChunk;
        }

        // If last chunk was full, add.
        if (freeSpots == 0)
        {
            Size++;
        }
    }

    /// <summary>
    ///     Trims the capacity of the <see cref="Chunks"/> array to its used minimum.
    ///     Reduces the <see cref="Capacity"/>.
    /// </summary>
    internal void TrimExcess()
    {
        // This always spares one single chunk.
        var minimalSize = Size > 0 ? Size : 1;

        // Decrease chunk size
        var newChunks = ArrayPool<Chunk>.Shared.Rent(minimalSize);
        Array.Copy(Chunks, newChunks, minimalSize);
        ArrayPool<Chunk>.Shared.Return(Chunks, true);
        Chunks = newChunks;
        Capacity = minimalSize;
    }

    /// <summary>
    ///     Reserves space for a certain number of <see cref="Arch.Core.Entity"/>'s in addition to the already existing amount.
    /// </summary>
    /// <param name="amount">The amount of new <see cref="Arch.Core.Entity"/>'s.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Reserve(in int amount)
    {
        // Calculate amount of required chunks.
        ref var lastChunk = ref LastChunk;
        var freeSpots = lastChunk.Capacity - lastChunk.Size;
        var neededSpots = amount - freeSpots;
        var neededChunks = (int)Math.Ceiling((float)neededSpots / EntitiesPerChunk);

        // Set capacity and insert new empty chunks.
        var previousCapacity = Capacity;
        EnsureCapacity(previousCapacity + neededChunks);
        for (var index = 0; index < neededChunks; index++)
        {
            var newChunk = new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Types);
            Chunks[previousCapacity + index] = newChunk;
        }

        // If last chunk was full, add.
        if (freeSpots == 0)
        {
            Size++;
        }
    }
}

public sealed partial class Archetype
{

    /// <summary>
    ///     Copies all <see cref="Chunks"/> from one <see cref="Archetype"/> to another.
    ///     Deterministic, the content of the first <see cref="Archetype"/> will be copied to the other <see cref="Archetype"/>, attached to its last partial <see cref="Chunk"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Archetype"/>.</param>
    /// <param name="destination">The destination <see cref="Archetype"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Copy(Archetype source, Archetype destination)
    {
        // Make sure other archetype can fit additional entities from this archetype.
        destination.EnsureEntityCapacity(source.Entities);

        // Copy chunks into destination chunks
        var sourceChunkIndex = 0;
        var destinationChunkIndex = destination.Size - 1;
        while (sourceChunkIndex < source.Size)
        {
            ref var sourceChunk = ref source.Chunks[sourceChunkIndex];
            var index = 0;
            while (sourceChunk.Size > 0 && destinationChunkIndex < destination.Capacity)  // Making sure that we dont go out of bounds
            {
                ref var destinationChunk = ref destination.Chunks[destinationChunkIndex];

                // Check how many entities fit into the destination chunk and choose the minimum as a copy length to prevent out of range exceptions.
                var destinationRemainingCapacity = destinationChunk.Capacity - destinationChunk.Size;
                var length = Math.Min(sourceChunk.Size, destinationRemainingCapacity);

                // Copy source array into destination chunk.
                Chunk.Copy(ref sourceChunk, index, ref destinationChunk, destinationChunk.Size, length);

                sourceChunk.Size -= length;
                destinationChunk.Size += length;
                index += length;

                // Current source chunk still has remaining capacity, resume with next destination chunk.
                if (destinationChunk.Size >= destinationChunk.Capacity)
                {
                    destinationChunkIndex++;
                    if(destination.Size + 1 <= destination.Capacity)
                    {
                        destination.Size++;
                    }
                }
            }

            sourceChunkIndex++;
        }
    }

    /// <summary>
    ///     Copies an <see cref="Arch.Core.Entity"/> and all its components from a <see cref="Slot"/> within this <see cref="Archetype"/> to a <see cref="Slot"/> within another <see cref="Archetype"/> .
    /// </summary>
    /// <param name="to">The <see cref="Archetype"/> into which the <see cref="Arch.Core.Entity"/> should move.</param>
    /// <param name="fromSlot">The <see cref="Slot"/> that targets the <see cref="Arch.Core.Entity"/> that should move.</param>
    /// <param name="toSlot">The <see cref="Slot"/> to which the <see cref="Arch.Core.Entity"/> should move.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void CopyComponents(Archetype from, ref Slot fromSlot, Archetype to, ref Slot toSlot)
    {
        // Copy items from old to new chunk
        ref var oldChunk = ref from.GetChunk(fromSlot.ChunkIndex);
        ref var newChunk = ref to.GetChunk(toSlot.ChunkIndex);
        Chunk.CopyComponents(ref oldChunk, fromSlot.Index, ref newChunk, toSlot.Index, 1);
    }
}
