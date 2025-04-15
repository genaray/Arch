using System.Buffers;
using System.Diagnostics.Contracts;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Arch.LowLevel;
using Arch.LowLevel.Jagged;
using Collections.Pooled;
using CommunityToolkit.HighPerformance;
using Array = System.Array;
using System.Runtime.InteropServices;

namespace Arch.Core;

/// <summary>
///     The <see cref="Slot"/> struct references an <see cref="Arch.Core.Entity"/> entry within an <see cref="Archetype"/> using a reference to its <see cref="Chunk"/> and its index.
/// </summary>
[SkipLocalsInit]
public record struct Slot
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
    public static Slot operator +(Slot first, Slot second)
    {
        return new Slot(first.Index + second.Index, first.ChunkIndex + second.ChunkIndex);
    }

    /// <summary>
    ///     Adds a plus plus operator for easy calculation of new <see cref="Slot"/>. Increases the index by one.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>The <see cref="Slot"/> with index increased by one..</returns>
    public static Slot operator ++(Slot slot)
    {
        slot.Index++;
        return slot;
    }

    /// <summary>
    ///     Validates the <see cref="Slot"/>, moves the <see cref="Slot"/> if it is outside a <see cref="Chunk.Capacity"/> to match it.
    /// </summary>
    /// <returns></returns>
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
///     The <see cref="Archetypes"/> struct
///     Contains a list of archetypes with a cached hash that only changes when a new one is added or removed.
///     This means that others can use the hash to determine whether there has been a change.
/// </summary>
public class Archetypes : IDisposable
{
    /// <summary>
    ///     The cached hashcode.
    /// </summary>
    private int _hashCode;

    /// <summary>
    ///     Creates a new <see cref="Archetype"/> instance.
    /// <param name="capacity">The capacity.</param>
    /// </summary>
    public Archetypes(int capacity)
    {
        Items = new NetStandardList<Archetype>(capacity);
        _hashCode = -1;
    }

    /// <summary>
    ///     The <see cref="PooledList{T}"/> that contains all <see cref="Archetype"/>s.
    /// </summary>
    public NetStandardList<Archetype> Items {  get; }

    /// <summary>
    ///     The count of this instance.
    /// </summary>
    public int Count
    {
        get
        {
            return Items.Count;
        }
    }

    /// <summary>
    ///     Adds a new <see cref="Archetype"/> to the list and updates the <see cref="_hashCode"/>.
    /// </summary>
    /// <param name="archetype">The new <see cref="Archetype"/>.</param>
    public void Add(Archetype archetype)
    {
        Items.Add(archetype);
        _hashCode = -1;
        GetHashCode();
    }

    /// <summary>
    ///     Removed an existing <see cref="Archetype"/> from the list and updates the <see cref="_hashCode"/>.
    /// </summary>
    /// <param name="archetype">The new <see cref="Archetype"/>.</param>
    public void Remove(Archetype archetype)
    {
        Items.Remove(archetype);
        _hashCode = -1;
        GetHashCode();
    }

    /// <summary>
    ///     Returns a <see cref="Span{T}"/> of this instance.
    /// </summary>
    /// <returns>The <see cref="Span{T}"/>.</returns>
    public Span<Archetype> AsSpan()
    {
        return Items.AsSpan();
    }

    /// <summary>
    ///     Gets or sets the item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    public Archetype this[int index]
    {
        get => Items[index];
        set => Items[index] = value;
    }

    /// <summary>
    ///     Checks this <see cref="Archetypes"/> for equality with another.
    /// </summary>
    /// <param name="other">The other <see cref="Archetypes"/>.</param>
    /// <returns>True if they are equal, false if not.</returns>
    public bool Equals(Archetypes other)
    {
        return Items.Equals(other.Items);
    }

    /// <summary>
    ///      Checks this <see cref="Archetypes"/> for equality with another object.
    /// </summary>
    /// <param name="obj">The other <see cref="object"/>.</param>
    /// <returns>True if they are equal, false if not.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Archetypes other && Equals(other);
    }

    /// <summary>
    ///     Calculates the hash and or returns the cached <see cref="_hashCode"/>.
    /// </summary>
    /// <returns>The hash.</returns>
    public override int GetHashCode()
    {
        // Cached hashcode, return
        if (_hashCode != -1)
        {
            return _hashCode;
        }

        // Calculate and cache hashcode
        var hash = 17;
        foreach (var item in Items)
        {
            hash = (hash * 31) + (item?.GetHashCode() ?? 0);
        }

        _hashCode = hash;
        return hash;
    }

    /// <summary>
    ///     Clears this instance.
    /// </summary>
    public void Clear()
    {
        Items.Clear();
    }

    /// <summary>
    ///     Disposes this instance.
    /// </summary>
    public void Dispose()
    {
        Items.Clear();
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
    ///     A lookup array that maps the component id to an index within the component array of a <see cref="Chunk"/> to quickly find the correct array for the component type.
    ///     Is being stored here since all <see cref="Chunks"/> share the same instance to reduce allocations.
    /// </summary>
    private readonly int[] _componentIdToArrayIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Archetype"/> class by a group of components.
    /// </summary>
    /// <param name="signature">The component structure of the <see cref="Arch.Core.Entity"/>'s that can be stored in this <see cref="Archetype"/>.</param>
    /// <param name="baseChunkSize">The minimum <see cref="Chunk"/> size in bytes.</param>
    /// <param name="baseChunkEntityCount">The minimum amount of entities per <see cref="Chunk"/>.</param>
    internal Archetype(Signature signature, int baseChunkSize, int baseChunkEntityCount)
    {
        Signature = signature;
        BaseChunkSize = baseChunkSize;

        // Calculations
        ChunkSize = GetChunkSizeInBytesFor(baseChunkSize, baseChunkEntityCount, signature);
        EntitiesPerChunk = GetEntityCountFor(ChunkSize, signature);

        // The bitmask/set
        BitSet = signature;
        _componentIdToArrayIndex = signature.Components.ToLookupArray();

        // Setup arrays and mappings
        Chunks = new Chunks(1);
        AddChunk();

        _addEdges = new SparseJaggedArray<Archetype>(BucketSize);
        _removeEdges = new SparseJaggedArray<Archetype>(BucketSize);
    }

    /// <summary>
    ///     The component types that the <see cref="Arch.Core.Entity"/>'s stored here have.
    ///     The base size of a <see cref="Chunk"/> within the <see cref="Chunks"/> in KB.
    ///     All <see cref="Chunk"/>s will have a minimum of this size. The actual size is <see cref="ChunkSize"/>.
    /// </summary>
    public int BaseChunkSize { get; }

    /// <summary>
    ///     The size of a <see cref="Chunk"/> within the <see cref="Chunks"/> in KB.
    ///     Necessary because the <see cref="Archetype"/> adjusts the size of a <see cref="Chunk"/> based on the minimum amount of <see cref="Entity"/> passed during construction.
    /// </summary>
    public int ChunkSize { get; }

    /// <summary>
    ///     The number of entities that are stored per <see cref="Chunk"/>.
    /// </summary>
    public int EntitiesPerChunk { get; }

    /// <summary>
    ///     The component types that the <see cref="Arch.Core.Entity"/>'s stored here have.
    /// </summary>
    public Signature Signature {  get; }

    /// <summary>
    ///     A bitset representation of the <see cref="Signature"/> array for fast lookups and queries.
    /// </summary>
    public BitSet BitSet {  get; }

    /// <summary>
    ///     The lookup array used by this <see cref="Archetype"/>, is being passed to all its <see cref="Chunks"/> to save memory.
    /// </summary>
    internal int[] LookupArray
    {
        get => _componentIdToArrayIndex;
    }

    /// <summary>
    ///     An array which stores the <see cref="Chunk"/>'s.
    ///     May contain null references since its being pooled, therefore use the <see cref="ChunkCount"/> and <see cref="ChunkCapacity"/> for acessing it.
    /// </summary>
    public Chunks Chunks {  get;  internal set; }

    /// <summary>
    ///     The number of <see cref="Chunk"/>'s within the <see cref="Chunks"/> array.
    /// </summary>
    public int ChunkCount {
        get
        {
            return Chunks.Count;
        }
    }

    /// <summary>
    ///     How many <see cref="Chunk"/>' have been deposited within the <see cref="Chunks"/> array.
    ///     The total capacity.
    /// </summary>
    public int ChunkCapacity {
        get
        {
            return Chunks.Capacity;
        }
    }

    /// <summary>
    ///     The number of filled chunks within the <see cref="Chunks"/> array.
    /// </summary>
    public int Count { get; internal set; }

    /// <summary>
    ///     Points to the current <see cref="Chunk"/> in use with remaining capacity.
    /// </summary>
    internal ref Chunk CurrentChunk {  get => ref Chunks[Count]; }

    /// <summary>
    ///     Points to the last <see cref="Slot"/>.
    /// </summary>
    internal Slot CurrentSlot
    {
         get
        {
            var lastRow = CurrentChunk.Count - 1;
            return new(lastRow, Count);
        }
    }

    /// <summary>
    ///     The number of <see cref="Arch.Core.Entity"/>s in this <see cref="Archetype"/>.
    /// </summary>
    public int EntityCount
    {
        get;
        internal set;
    }

    /// <summary>
    ///     The capacity of total <see cref="Arch.Core.Entity"/>s in this <see cref="Archetype"/>.
    /// </summary>
    public int EntityCapacity
    {
        get => ChunkCapacity * EntitiesPerChunk;
    }

    /// <summary>
    ///     Creates a new <see cref="Chunk"/> at the last <see cref="ChunkCount"/>.
    /// </summary>
    /// <returns>The new created <see cref="Chunk"/>.</returns>
    public ref Chunk AddChunk()
    {
        Chunks.EnsureCapacity(Chunks.Count+1);

        // Insert chunk
        var count = Chunks.Count;
        Chunks.Add(new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Signature));
        return ref Chunks[count];
    }

    /// <summary>
    ///     Returns a reference to a given <see cref="Chunk"/> using its index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>A reference to the <see cref="Chunk"/> at the given index.</returns>
    public ref Chunk GetChunk(int index)
    {
        return ref Chunks[index];
    }

    /// <summary>
    ///     Adds an <see cref="Arch.Core.Entity"/> to the <see cref="Archetype"/> and offloads it to a <see cref="Chunk"/>.
    ///     Uses the last <see cref="Chunk"/> that is not full, once it is full and the capacity is exhausted, a new <see cref="Chunk"/> is allocated.
    /// </summary>
    /// <param name="entity">The <see cref="Arch.Core.Entity"/> that is added.</param>
    /// <param name="chunk">The chunk in which the <see cref="Entity"/> was created in.</param>
    /// <param name="slot">The <see cref="Slot"/> in which it was deposited.</param>
    /// <returns>The amount of newly allocated entities in <see cref="Chunk"/>s.</returns>
    internal int Add(Entity entity, out Chunk chunk, out Slot slot)  // TODO: Store chunk reference in slot?
    {
        EntityCount++;

        // Storing stack variables to prevent multiple times accessing those fields.
        var count = Count;
        ref var currentChunk = ref GetChunk(count);

        // Fill chunk
        if (currentChunk.IsEmpty)
        {
            slot = new Slot(currentChunk.Add(entity), count);
            chunk = currentChunk;

            return 0;
        }

        // Chunk full? Use next allocated chunk
        count++;
        if (count < ChunkCapacity)
        {
            currentChunk = ref GetChunk(count);

            slot = new Slot(currentChunk.Add(entity), count);
            chunk = currentChunk;
            Count = count;

            return 0;
        }

        // No more free allocated chunks? Create new chunk
        ref var newChunk = ref AddChunk();
        slot = new Slot(newChunk.Add(entity), count);
        chunk = newChunk;
        Count = count;

        return EntitiesPerChunk;
    }

    /// <summary>
    ///     Adds an array of <see cref="Entity"/>s to this instance. This is much more efficient than adding <see cref="Entity"/>s individually.
    /// </summary>
    /// <param name="entities">The <see cref="Span{T}"/> of <see cref="Entity"/>s.</param>
    /// <param name="amount">The amount.</param>
    public void AddAll(Span<Entity> entities, int amount)
    {
        EnsureEntityCapacity(EntityCount + amount);

        // Track created and the last filled chunk
        var created = 0;
        var chunkIndex = Count;

        // Fill with entities until no entity is left or chunk capacity is reached
        for(var index = Count; index < ChunkCapacity && created < amount; index++)
        {
            ref var chunk = ref GetChunk(index);
            var fillAmount = Math.Min(chunk.Buffer, amount - created);

            // Copy batch of entities into the chunk
            Chunk.Copy(ref entities, created, ref chunk, chunk.Count, fillAmount);
            chunk.Count += fillAmount;

            chunkIndex = index;
            created += fillAmount;
        }

        // Set counts
        EntityCount += amount;
        Count = chunkIndex;  // To the last filled chunk
    }

    /// <summary>
    ///     Removes an <see cref="Arch.Core.Entity"/> from a <see cref="Slot"/> and moves the last <see cref="Arch.Core.Entity"/> of the <see cref="Archetype"/> to its position.
    /// </summary>
    /// <param name="slot">The slot of the <see cref="Arch.Core.Entity"/> to be removed.</param>
    /// <param name="movedEntityId">The id of the <see cref="Arch.Core.Entity"/> that was moved to the position of the deleted <see cref="Arch.Core.Entity"/>.</param>
    /// <returns>True if a <see cref="Chunk"/> was deleted, otherwise false.</returns>
    internal void Remove(Slot slot, out int movedEntityId)
    {
        // Move the last entity from the last chunk into the chunk to replace the removed entity directly
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        ref var lastChunk = ref CurrentChunk;

        movedEntityId = chunk.Transfer(slot.Index, ref lastChunk);
        EntityCount--;

        // Return to prevent that Size decreases when chunk IS not Empty and to prevent Size becoming 0 or -1.
        if (lastChunk.Count > 0 || Count <= 0)
        {
            return;
        }

        Count--;
    }

    /// <summary>
    ///     Returns a reference of the <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the <see cref="Arch.Core.Entity"/>.</returns>
    internal ref Entity Entity(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Entity(slot.Index);
    }

    /// <summary>
    ///     Adds an <see cref="Arch.Core.Entity"/> to the <see cref="Archetype"/> and offloads it to a <see cref="Chunk"/>.
    ///     Uses the last <see cref="Chunk"/> that is not full, once it is full and the capacity is exhausted, a new <see cref="Chunk"/> is allocated.
    /// </summary>
    /// <param name="entity">The <see cref="Arch.Core.Entity"/> that is added.</param>
    /// <param name="slot">The <see cref="Slot"/> in which it was deposited.</param>
    /// <param name="cmp">The component which will be set directly.</param>
    /// <returns>The amount of newly allocated entities if a new <see cref="Chunk"/> was created.</returns>
    internal int Add<T>(Entity entity, out Slot slot, in T? cmp = default)
    {
        var createdChunk = Add(entity, out var chunk, out slot);
        chunk.Copy(slot.Index, in cmp);
        return createdChunk;
    }

    /// <summary>
    ///     Sets or replaces the components of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/> at which the component of an <see cref="Arch.Core.Entity"/> is to be set or replaced.</param>
    /// <param name="cmp">The component value.</param>
    internal void Set<T>(ref Slot slot, in T? cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Copy(slot.Index, in cmp);
    }

    /// <summary>
    ///      Checks if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with a specific component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>True if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with such a component, otherwise false.</returns>
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return BitSet.IsSet(id);
    }

    /// <summary>
    ///     Try get the index of a component within this archetype. Returns false if the archetype does not have this
    ///     component.
    /// </summary>
    /// <param name="i">The index.</param>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>True if it was successfully.</returns>
    [Pure]
    internal bool TryIndex<T>(out int i)
    {
        var id = Component<T>.ComponentType.Id;
        Debug.Assert(id != -1, $"Supplied component index is invalid");

        if (id >= _componentIdToArrayIndex.Length)
        {
            i = -1;
            return false;
        }

        i = _componentIdToArrayIndex.DangerousGetReferenceAt(id);
        return i != -1;
    }

    /// <summary>
    ///     Try get the index of a component within this archetype. Returns false if the archetype does not have this
    ///     component.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/>.</param>
    /// <param name="i">The index.</param>
    /// <returns>True if it was successfully.</returns>
    [Pure]
    internal bool TryIndex(ComponentType type, out int i)
    {
        var id = type.Id;
        Debug.Assert(id != -1, $"Supplied component index is invalid");

        if (id >= _componentIdToArrayIndex.Length)
        {
            i = -1;
            return false;
        }

        i = _componentIdToArrayIndex.DangerousGetReferenceAt(id);
        return i != -1;
    }

    /// <summary>
    ///     Returns a reference of the component of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="slot">The <see cref="Slot"/>.</param>
    /// <returns>A reference to the component.</returns>
    internal ref T Get<T>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return ref chunk.Get<T>(slot.Index);
    }


    /// <summary>
    ///     Sets a component value for all entities within an <see cref="Archetype"/> in a certain range of <see cref="Slot"/>s
    /// </summary>
    /// <param name="from">The <see cref="Slot"/> where we start.</param>
    /// <param name="to">The <see cref="Slot"/> where we end.</param>
    /// <param name="component">The component value.</param>
    /// <typeparam name="T">The component type.</typeparam>
    internal void SetRange<T>(in Slot from, in Slot to, in T? component = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);

            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index+1 : chunk.Count;
            var lower = isEnd ? to.Index : 0;

            Chunk.Fill(ref chunk, lower, upper-lower, component);
        }
    }

    /// <summary>
    ///     Creates an <see cref="Enumerator{T}"/> which iterates over all <see cref="Chunks"/> in this <see cref="Archetype"/>.
    /// </summary>
    /// <returns>An <see cref="Enumerator{T}"/>.</returns>
    public Enumerator<Chunk> GetEnumerator()
    {
        return new Enumerator<Chunk>(Chunks.AsSpan()[..ChunkCount]);
    }

    /// <summary>
    ///     Creates an <see cref="ChunkRangeEnumerator"/> which iterates over all <see cref="Chunks"/> within a range backwards.
    /// </summary>
    /// <returns>A <see cref="ChunkRangeEnumerator"/>.</returns>
    internal ChunkRangeIterator GetRangeIterator(int from, int to)
    {
        return new ChunkRangeIterator(this, from, to);
    }

    /// <summary>
    ///     Creates an <see cref="ChunkRangeEnumerator"/> which iterates from the last valid chunk to another <see cref="Chunks"/> within a range backwards.
    /// </summary>
    /// <returns>A <see cref="ChunkRangeEnumerator"/>.</returns>
    internal ChunkRangeIterator GetRangeIterator(int to)
    {
        return new ChunkRangeIterator(this, CurrentSlot.ChunkIndex, to);
    }

    /// <summary>
    ///     Cleares this <see cref="Archetype"/>, an efficient method to delete all <see cref="Arch.Core.Entity"/>s.
    ///     Does not dispose any resources nor modifies its <see cref="ChunkCapacity"/>.
    /// </summary>
    public void Clear()
    {
        Count = 0;
        EntityCount = 0;
        Chunks.Clear();
    }

    /// <summary>
    ///     Converts this <see cref="Archetype"/> to a human readable string.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
        return $"Archetype {{ {nameof(Signature)} = {{ {Signature} }}, {nameof(BitSet)} = {{ {BitSet} }}, {nameof(EntitiesPerChunk)} = {EntitiesPerChunk}, {nameof(ChunkSize)} = {ChunkSize}, {nameof(ChunkCapacity)} = {ChunkCapacity}, {nameof(ChunkCount)} = {ChunkCount}, {nameof(EntityCapacity)} = {EntityCapacity}, {nameof(EntityCount)} = {EntityCount} }}}}";
    }
}

public sealed unsafe partial class Archetype
{

    /// <summary>
    ///     Sets or replaces the components of an <see cref="Arch.Core.Entity"/> at a given <see cref="Slot"/>.
    /// </summary>
    /// <param name="slot">The <see cref="Slot"/> at which the component of an <see cref="Arch.Core.Entity"/> is to be set or replaced.</param>
    /// <param name="cmp">The component value.</param>

    internal void Set(ref Slot slot, in object cmp)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        chunk.Copy(slot.Index, cmp);
    }

    /// <summary>
    ///      Checks if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with a specific component.
    /// </summary>
    /// <param name="type">The <see cref="Type"/>.</param>
    /// <returns>True if the <see cref="Archetype"/> stores <see cref="Arch.Core.Entity"/>'s with such a component, otherwise false.</returns>

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

    internal object? Get(scoped ref Slot slot, ComponentType type)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get(slot.Index, type);
    }
}

// Capacity related methods

public sealed partial class Archetype
{
    /// <summary>
    ///     Ensures the capacity of the <see cref="Chunks"/> array.
    ///     Increases the <see cref="ChunkCapacity"/>.
    /// </summary>
    /// <param name="newCapacity">The amount of <see cref="Chunk"/>'s required, in total.</param>
    private void EnsureChunkCapacity(int newCapacity)
    {
        Chunks.EnsureCapacity(newCapacity);
    }

    /// <summary>
    ///     Ensures the capacity of the <see cref="Chunks"/> array for a certain amount of <see cref="Entity"/>s.
    ///     Increases the <see cref="ChunkCapacity"/> to fit all entities within it.
    /// </summary>
    /// <param name="newCapacity">The amount of <see cref="Entity"/>'s required, in total.</param>
    internal void EnsureEntityCapacity(int newCapacity)
    {
        // Calculate amount of required chunks.
        var neededChunks = (int)Math.Ceiling((float)newCapacity / EntitiesPerChunk);
        if (ChunkCount >= neededChunks)
        {
            return;
        }

        // Set capacity and insert new empty chunks.
        var previousCapacity = ChunkCapacity;
        EnsureChunkCapacity(neededChunks);

        for (var index = previousCapacity; index < neededChunks; index++)
        {
            Chunks.Add(new Chunk(EntitiesPerChunk, _componentIdToArrayIndex, Signature));
        }
    }

    /// <summary>
    ///     Trims the capacity of the <see cref="Chunks"/> array to its used minimum.
    ///     Reduces the <see cref="ChunkCapacity"/>.
    /// </summary>
    internal void TrimExcess()
    {
        Chunks.Count = Count + 1; // By setting the Count we will assure that unnecessary chunks are trimmed.
        Chunks.TrimExcess();
    }
}

public sealed partial class Archetype
{
    /// <summary>
    ///     Calculates the size of the memory in bytes required to store the number of <see cref="entityAmount"/>.
    ///     The <see cref="baseChunkSize"/> (L1 cache size) is taken into account and, if necessary, rounded up to a multiple of this to ensure maximum cache performance.
    ///     So if the number of <see cref="entityAmount"/> exceeds the <see cref="baseChunkSize"/> value, a multiple of this is used.
    /// </summary>
    /// <param name="baseChunkSize">The minimum <see cref="Chunk"/> size in KB. </param>
    /// <param name="entityAmount">The amount of entities.</param>
    /// <param name="types">The component structure of the <see cref="Arch.Core.Entity"/>'s.</param>
    /// <returns>The amount of bytes required to store the <see cref="Entity"/>s.</returns>
    public unsafe static int GetChunkSizeInBytesFor(int baseChunkSize, int entityAmount, Span<ComponentType> types)
    {
        var entityBytes = (sizeof(Entity) + types.ToByteSize()) * entityAmount;
        return (int)Math.Ceiling((float)entityBytes / baseChunkSize ) * baseChunkSize;  // Calculates and rounds to a multiple of BaseSize to store the number of entities
    }

    /// <summary>
    ///     Calculates how many <see cref="Arch.Core.Entity"/>'s fit into the desired <see cref="byteAmount"/>.
    /// </summary>
    /// <param name="byteAmount">The available bytes.</param>
    /// <param name="types">The component structure of the <see cref="Arch.Core.Entity"/>'s.</param>
    /// <returns>The amount of <see cref="Arch.Core.Entity"/>'s.</returns>
    public unsafe static int GetEntityCountFor(int byteAmount, Span<ComponentType> types)
    {
        return byteAmount / (sizeof(Entity) + types.ToByteSize());
    }

    /// <summary>
    ///     Calculates how many <see cref="Chunks"/> are required to store the desired <see cref="entityAmount"/>.
    /// </summary>
    /// <param name="entitiesPerChunk">The amount of entities inside a <see cref="Chunk"/>.</param>
    /// <param name="entityAmount">The amount.</param>
    /// <returns>The amount of <see cref="Chunks"/>s required to store the entities.</returns>
    public static int GetChunkCapacityFor(int entitiesPerChunk, int entityAmount)
    {
        return (int)Math.Ceiling((float)entityAmount / entitiesPerChunk);
    }

    /// <summary>
    ///     Calculates the next <see cref="Slot"/>s within the <see cref="Archetype"/> and its <see cref="ChunkCapacity"/> and writes them into a <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <param name="slots">The <see cref="Span{T}"/> to fill the <see cref="Slot"/>s into.</param>
    /// <param name="amount">The amount of <see cref="Slot"/>'s we want to calculate.</param>
    /// <returns>The amount of <see cref="Slot"/>s that fit into the <see cref="Archetype"/></returns>
    internal static int GetNextSlots(Archetype archetype, Span<Slot> slots, int amount)
    {
        // Loop over chunks and calculate next n slots.
        var next = 0;
        for (var chunkIndex = archetype.Count; chunkIndex < archetype.ChunkCapacity && amount > 0; chunkIndex++)
        {
            ref var chunk = ref archetype.GetChunk(chunkIndex);
            var chunkSize = chunk.Count;
            var fillLimit = Math.Min(chunk.Capacity - chunkSize, amount);

            // Put n empty slots into the slots span
            for (var index = chunkSize; index < chunkSize+fillLimit; index++)
            {
                slots[next++] = new Slot(index, chunkIndex);
            }

            amount -= fillLimit;
        }

        return next;
    }

    // TODO: Copy should only copy, add transfer methods and those should modify the destination and source state.
    /// <summary>
    ///     Copies all <see cref="Chunks"/> from one <see cref="Archetype"/> to another.
    ///     Deterministic, the content of the first <see cref="Archetype"/> will be copied to the other <see cref="Archetype"/>, attached to its last partial <see cref="Chunk"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Archetype"/>.</param>
    /// <param name="destination">The destination <see cref="Archetype"/>.</param>
    internal static void Copy(Archetype source, Archetype destination)
    {
        // Make sure other archetype can fit additional entities from this archetype.
        destination.EnsureEntityCapacity(destination.EntityCount + source.EntityCount);
        var sourceSignature = source.Signature;

        // Iterate each source chunk to copy them
        for (var sourceChunkIndex = 0; sourceChunkIndex <= source.Count; sourceChunkIndex++)
        {
            ref var sourceChunk = ref source.GetChunk(sourceChunkIndex);

            var amountCopied = 0;
            var chunkIndex = 0;

            // Loop over destination chunk and fill them with the source chunk till either the source chunk is empty or theres no more capacity
            for (int destinationChunkIndex = destination.Count; destinationChunkIndex < destination.ChunkCapacity && sourceChunk.Count > 0; destinationChunkIndex++)
            {
                // Determine amount that can be copied into destination
                ref var destinationChunk = ref destination.GetChunk(destinationChunkIndex);
                var remainingCapacity = destinationChunk.Buffer;
                var amountToCopy = Math.Min(sourceChunk.Count, remainingCapacity);

                Chunk.Copy(ref sourceChunk, amountCopied, ref sourceSignature, ref destinationChunk, destinationChunk.Count, amountToCopy);

                // Apply copied amount to track the progress
                sourceChunk.Count -= amountToCopy;
                destinationChunk.Count += amountToCopy;
                amountCopied += amountToCopy;
                chunkIndex = destinationChunkIndex;  // Track the last destination chunk we filled, important
            }

            destination.Count = chunkIndex;
        }

        // Update entity counts
        destination.EntityCount += source.EntityCount;
        source.EntityCount = 0;
        source.Count = 0;
    }

    /// <summary>
    ///     Copies all components from an <see cref="Archetype"/> to another archetype <see cref="Archetype"/> .
    /// </summary>
    /// <param name="source">The <see cref="Archetype"/> from which the <see cref="Arch.Core.Entity"/> should move.</param>
    /// <param name="sourceIndex">The <see cref="Chunk"/>-Index in the <see cref="source"/> where we start to copy.</param>
    /// <param name="destination">The <see cref="Archetype"/> into which the <see cref="Arch.Core.Entity"/> should move.</param>
    /// <param name="destinationIndex">The <see cref="Chunk"/>-Index in the <see cref="destination"/> where start to inser the copy.</param>
    internal static void CopyComponents(Archetype source, int sourceIndex, Archetype destination, int destinationIndex, int length)
    {
        // Iterate each source chunk to copy them
        var sourceSignature = source.Signature;
        for (var sourceChunkIndex = sourceIndex; sourceChunkIndex <= length; sourceChunkIndex++)
        {
            ref var sourceChunk = ref source.GetChunk(sourceChunkIndex);

            var amountLeft = sourceChunk.Count;
            var amountCopied = 0;

            // Loop over destination chunk and fill them with the source chunk till either the source chunk is empty or theres no more capacity
            for (int destinationChunkIndex = destinationIndex; destinationChunkIndex < destination.ChunkCapacity && amountLeft > 0; destinationChunkIndex++)
            {
                // Determine amount that can be copied into destination
                ref var destinationChunk = ref destination.GetChunk(destinationChunkIndex);
                var amountToCopy = Math.Min(amountLeft, destinationChunk.Buffer);

                Chunk.CopyComponents(ref sourceChunk, amountCopied, ref sourceSignature, ref destinationChunk, destinationChunk.Count, amountToCopy);

                // Apply copied amount to track the progress
                amountLeft -= amountToCopy;
                amountCopied += amountToCopy;
            }
        }
    }

    /// <summary>
    ///     Copies an <see cref="Arch.Core.Entity"/> and all its components from a <see cref="Slot"/> within this <see cref="Archetype"/> to a <see cref="Slot"/> within another <see cref="Archetype"/> .
    /// </summary>
    /// <param name="source">The <see cref="Archetype"/> from which the <see cref="Arch.Core.Entity"/> should move.</param>
    /// <param name="to">The <see cref="Archetype"/> into which the <see cref="Arch.Core.Entity"/> should move.</param>
    /// <param name="fromSlot">The <see cref="Slot"/> that targets the <see cref="Arch.Core.Entity"/> that should move.</param>
    /// <param name="toSlot">The <see cref="Slot"/> to which the <see cref="Arch.Core.Entity"/> should move.</param>
    internal static void CopyComponents(Archetype source, ref Slot fromSlot, Archetype to, ref Slot toSlot)
    {
        var sourceSignature = source.Signature;

        // Copy items from old to new chunk
        ref var oldChunk = ref source.GetChunk(fromSlot.ChunkIndex);
        ref var newChunk = ref to.GetChunk(toSlot.ChunkIndex);
        Chunk.CopyComponents(ref oldChunk, fromSlot.Index, ref sourceSignature, ref newChunk, toSlot.Index, 1);
    }
}
