using System.Buffers;
using System.Diagnostics.Contracts;
using System.Threading;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Collections.Pooled;
using CommunityToolkit.HighPerformance;
using Schedulers;
using Array = System.Array;

namespace Arch.Core;

/// <summary>
///     The <see cref="RecycledEntity"/> struct
///     stores information about a recycled <see cref="Entity"/>: its ID and its version.
/// </summary>
[SkipLocalsInit]
internal record struct RecycledEntity
{
    /// <summary>
    ///     The recycled id.
    /// </summary>
    public readonly int Id;

    /// <summary>
    ///     The new version.
    /// </summary>
    public readonly int Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecycledEntity"/> struct.
    /// </summary>
    /// <param name="id">Its ID.</param>
    /// <param name="version">Its version.</param>
    public RecycledEntity(int id, int version)
    {
        Id = id;
        Version = version;
    }
}

/// <summary>
///     The <see cref="IForEach"/> interface
///     provides a method to execute logic on an <see cref="Entity"/>.
/// </summary>
/// <remarks>
///     Commonly used with queries to provide a clean API.
/// </remarks>
public interface IForEach
{
    /// <summary>
    ///     Called on an <see cref="Entity"/> to execute logic on it.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>

    public void Update(Entity entity);
}

/// <summary>
///     The <see cref="ForEach"/> delegate
///     provides a callback to execute logic on an <see cref="Entity"/>.
/// </summary>
/// <param name="entity">The <see cref="Entity"/>.</param>
public delegate void ForEach(Entity entity);

// Static world, create and destroy
#region Static Create and Destroy
public partial class World
{
    /// <summary>
    ///     A list of all existing <see cref="Worlds"/>.
    ///     Should not be modified by the user.
    /// </summary>
    public static World[] Worlds { get; private set; } = new World[4];

    /// <summary>
    ///     Stores recycled <see cref="World"/> IDs.
    /// </summary>
    private static PooledQueue<int> RecycledWorldIds {  get; set; } = new(8);

    /// <summary>
    ///     Tracks how many <see cref="World"/>s exists.
    /// </summary>
    public static int WorldSize => Interlocked.CompareExchange(ref worldSizeUnsafe, 0, 0);

    private static int worldSizeUnsafe;

    /// <summary>
    ///     The shared static <see cref="JobScheduler"/> used for Multithreading.
    /// </summary>
    public static JobScheduler? SharedJobScheduler { get; set; }

    private bool _isDisposed;

    /// <summary>
    ///     Creates a <see cref="World"/> instance.
    /// </summary>
    /// <param name="chunkSizeInBytes">The base/minimum <see cref="Chunk"/> size in bytes.</param>
    /// <param name="minimumAmountOfEntitiesPerChunk">The minimum amount of <see cref="Entity"/>s per <see cref="Chunk"/>.</param>
    /// <param name="archetypeCapacity">The initial <see cref="Archetypes"/> capacity.</param>
    /// <param name="entityCapacity">The initial <see cref="Entity"/> capacity.</param>
    /// <returns>The created <see cref="World"/> instance.</returns>
    public static World Create(int chunkSizeInBytes = 16_384, int minimumAmountOfEntitiesPerChunk = 100, int archetypeCapacity = 2, int entityCapacity = 64)
    {
#if PURE_ECS
        return new World(-1, chunkSizeInBytes, minimumAmountOfEntitiesPerChunk, archetypeCapacity, entityCapacity);
#else
        lock (Worlds)
        {
            var recycle = RecycledWorldIds.TryDequeue(out var id);
            var recycledId = recycle ? id : WorldSize;

            var world = new World(recycledId, chunkSizeInBytes, minimumAmountOfEntitiesPerChunk, archetypeCapacity, entityCapacity);

            // If you need to ensure a higher capacity, you can manually check and increase it
            if (recycledId >= Worlds.Length)
            {
                var newCapacity = Worlds.Length * 2;
                var worlds = Worlds;
                Array.Resize(ref worlds, newCapacity);
                Worlds = worlds;
            }

            Worlds[recycledId] = world;
            Interlocked.Increment(ref worldSizeUnsafe);
            return world;
        }
#endif
    }

    /// <summary>
    ///     Destroys an existing <see cref="World"/>.
    /// </summary>
    /// <param name="world">The <see cref="World"/> to destroy.</param>
    public static void Destroy(World world)
    {
        world.Dispose();
    }
}

#endregion

// Constructors, properties, disposal
#region World Management

/// <summary>
///     The <see cref="World"/> class
///     stores <see cref="Entity"/>s in <see cref="Archetype"/>s and <see cref="Chunk"/>s, manages them, and provides methods to query for specific <see cref="Entity"/>s.
/// </summary>
/// <remarks>
///     The <see cref="World"/> class is only thread-safe under specific circumstances. Read-only operations like querying entities can be done simultaneously by multiple threads.
///     However, any method which mentions "structural changes" must not run alongside any other methods. Any operation that adds or removes an <see cref="Entity"/>, or changes
///     its <see cref="Archetype"/> counts as a structural change. Structural change methods are also marked with <see cref="StructuralChangeAttribute"/>, to enable source-generators
///     to edit their behavior based on the thread-safety of the method.
/// </remarks>
public partial class World : IDisposable
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="World"/> class.
    /// </summary>
    /// <param name="id">Its unique ID.</param>
    /// <param name="baseChunkSize">The base/minimum <see cref="Chunk"/> size in bytes.</param>
    /// <param name="baseChunkEntityCount">The minimum amount of <see cref="Entity"/>s per <see cref="Chunk"/>.</param>
    /// <param name="archetypeCapacity">The initial capacity for <see cref="Archetypes"/>.</param>
    /// <param name="entityCapacity">The initial capacity for <see cref="Entity"/>s.</param>
    private World(int id, int baseChunkSize, int baseChunkEntityCount, int archetypeCapacity, int entityCapacity)
    {
        Id = id;

        // Mapping.
        GroupToArchetype = new Dictionary<int, Archetype>(archetypeCapacity);

        // Entity stuff.
        Archetypes = new Archetypes(archetypeCapacity);
        EntityInfo = new EntityInfoStorage(baseChunkSize, entityCapacity);
        RecycledIds = new Queue<RecycledEntity>(entityCapacity);

        // Query.
        QueryCache = new Dictionary<QueryDescription, Query>(archetypeCapacity);

        // Multithreading/Jobs.
        JobHandles = new NetStandardList<JobHandle>(Environment.ProcessorCount);
        JobsCache = new List<IJob>(Environment.ProcessorCount);

        // Config
        BaseChunkSize = baseChunkSize;
        BaseChunkEntityCount = baseChunkEntityCount;
    }

    /// <summary>
    ///     The unique <see cref="World"/> ID.
    /// </summary>
    public int Id {  get; }

    /// <summary>
    ///     The amount of <see cref="Entity"/>s currently stored by this <see cref="World"/>.
    /// </summary>
    public int Size {  get; internal set; }

    /// <summary>
    ///     The available <see cref="Entity"/> capacity of this <see cref="World"/>.
    /// </summary>
    public int Capacity {  get; internal set; }

    /// <summary>
    ///     All <see cref="Archetype"/>s that exist in this <see cref="World"/>.
    /// </summary>
    public Archetypes Archetypes {  get; }

    /// <summary>
    ///     Maps an <see cref="Entity"/> to its <see cref="EntityInfo"/> for quick lookup.
    /// </summary>
    internal EntityInfoStorage EntityInfo {  get; }

    /// <summary>
    ///     Stores recycled <see cref="Entity"/> IDs and their last version.
    /// </summary>
    internal Queue<RecycledEntity> RecycledIds {  get; set; }

    /// <summary>
    ///     A cache to map <see cref="QueryDescription"/> to their <see cref="Core.Query"/>, to avoid allocs.
    /// </summary>
    internal Dictionary<QueryDescription, Query> QueryCache {  get; set; }

    /// <summary>
    ///     The <see cref="Chunk"/> size of each <see cref="Archetype"/> in bytes.
    /// <remarks>For the best cache optimisation use values that are divisible by 16Kb.</remarks>
    /// </summary>
    public int BaseChunkSize { get; private set; } = 16_384;

    /// <summary>
    ///     The minimum number of <see cref="Arch.Core.Entity"/>'s that should fit into a <see cref="Chunk"/> within all <see cref="Archetype"/>s.
    ///     On the basis of this, the <see cref="Archetypes"/>s chunk size may increase.
    /// </summary>
    public int BaseChunkEntityCount { get; private set; } = 100;

    /// <summary>
    ///     Returns the next <see cref="Entity"/>, either recycled from <see cref="RecycledIds"/> or newly created.
    /// <param name="entity">The next <see cref="Entity"/> which was either recycled from an <see cref="RecycledEntity"/> or newly created.</param>
    /// </summary>
    /// <remarks>Does not add it to the <see cref="EntityInfo"/> yet.</remarks>
    private void GetOrCreateEntityInternal(out Entity entity)
    {
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var recycled = recycle ? recycledId : new RecycledEntity(Size, 1);
        entity = new Entity(recycled.Id, Id, recycled.Version);
        Size++;
    }

    /// <summary>
    ///     Destroys/Recycles an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to destroy/recycle.</param>
    /// <remarks>Also removes it from the <see cref="EntityInfo"/>.</remarks>
    private void DestroyEntityInternal(Entity entity)
    {
        var recycledEntity = new RecycledEntity(entity.Id, unchecked(entity.Version + 1));
        RecycledIds.Enqueue(recycledEntity);
        EntityInfo.Remove(entity.Id);
        Size--;
    }

    /// <summary>
    ///     Creates a new <see cref="Entity"/> using its given component structure/<see cref="Archetype"/>.
    ///     Might resize its target <see cref="Archetype"/> and allocate new space if its full.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="types">Its component structure/<see cref="Archetype"/>.</param>
    /// <returns></returns>
    [StructuralChange]
    public Entity Create(params ComponentType[] types)
    {
        return Create((Signature)types);
    }

    // TODO: Find cleaner way to resize the EntityInfo? Let archetype.Create return an amount which is added to Capacity or whatever?
    /// <summary>
    ///     Creates a new <see cref="Entity"/> using its given component structure/<see cref="Archetype"/>.
    ///     Might resize its target <see cref="Archetype"/> and allocate new space if its full.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="types">Its component structure/<see cref="Archetype"/>.</param>
    /// <returns></returns>
    [StructuralChange]
    public Entity Create(in Signature types)
    {
        // Create new entity and put it to the back of the array
        GetOrCreateEntityInternal(out var entity);

        // Add to archetype & mapping
        var archetype = GetOrCreate(in types);
        var allocatedEntities = archetype.Add(entity, out _, out var slot);

        // Resize map & Array to fit all potential new entities
        Capacity += allocatedEntities;
        EntityInfo.EnsureCapacity(Capacity);

        // Add entity to info storage
        EntityInfo.Add(entity.Id, archetype, slot, entity.Version);
        OnEntityCreated(entity);

#if EVENTS
        foreach (ref var type in types)
        {
            OnComponentAdded(entity, type);
        }
#endif

        return entity;
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> from one <see cref="Archetype"/> <see cref="Slot"/> to another.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="data">The <see cref="EntityData"/> of the supplied entity.</param>
    /// <param name="source">Its <see cref="Archetype"/>.</param>
    /// <param name="destination">The new <see cref="Archetype"/>.</param>
    /// <param name="destinationSlot">The new <see cref="Slot"/> in which the moved <see cref="Entity"/> will land.</param>
    internal void Move(Entity entity, ref EntityData data, Archetype source, Archetype destination, out Slot destinationSlot)
    {
        // Entity should match the supplied EntityData.
        Debug.Assert(entity == data.Archetype.Entity(ref data.Slot));

        // A common mistake, happening in many cases.
        Debug.Assert(source != destination, "From-Archetype is the same as the To-Archetype. Entities cannot move within the same archetype using this function. Probably an attempt was made to attach already existing components to the entity or to remove non-existing ones.");

        // Copy entity to other archetype
        var slot = data.Slot;
        var allocatedEntities = destination.Add(entity, out _, out destinationSlot);
        Archetype.CopyComponents(source, ref slot, destination, ref destinationSlot);
        source.Remove(slot, out var movedEntity);

        // Update moved entity from the remove
        EntityInfo.Move(movedEntity, slot);

        data.Archetype = destination;
        data.Slot = destinationSlot;

        // Calculate the entity difference between the moved archetypes to allocate more space accordingly.
        Capacity += allocatedEntities;
        EntityInfo.EnsureCapacity(Capacity);
    }

    /// <summary>
    ///     Destroys an <see cref="Entity"/>.
    ///     Might resize its target <see cref="Archetype"/> and release memory.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [StructuralChange]
    public void Destroy(Entity entity)
    {
        #if EVENTS
        // Raise the OnComponentRemoved event for each component on the entity.
        var arch = GetArchetype(entity);
        foreach (var compType in arch.Signature)
        {
            OnComponentRemoved(entity, compType);
        }
        #endif

        OnEntityDestroyed(entity);

        // Remove from archetype and move other entity to replace its slot
        ref var entityData = ref EntityInfo.GetEntityData(entity.Id);
        entityData.Archetype.Remove(entityData.Slot, out var movedEntityId);
        EntityInfo.Move(movedEntityId, entityData.Slot);

        DestroyEntityInternal(entity);
    }

    /// <summary>
    ///     Creates a <see cref="Core.Query"/> using a <see cref="QueryDescription"/>
    ///     which can be used to iterate over the matching <see cref="Entity"/>s, <see cref="Archetype"/>s and <see cref="Chunk"/>s.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <returns>The generated <see cref="Core.Query"/></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [Pure]
    public Query Query(in QueryDescription queryDescription)
    {
        // Looping over all archetypes, their chunks and their entities.
        var queryCache = QueryCache; // Storing locally to only access the QueryCache once
        if (queryCache.TryGetValue(queryDescription, out var query))
        {
            return query;
        }

        query = new Query(Archetypes, queryDescription);
        queryCache[queryDescription] = query;

        return query;
    }

    /// <summary>
    ///     Counts all <see cref="Entity"/>s that match a <see cref="QueryDescription"/> and returns the number.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies the components or <see cref="Entity"/>s for which to search.</param>
    [Pure]
    public int CountEntities(in QueryDescription queryDescription)
    {
        var counter = 0;
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var entities = archetype.EntityCount;
            counter += entities;
        }

        return counter;
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>s and puts them into the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies the components or <see cref="Entity"/>s for which to search.</param>
    /// <param name="list">The <see cref="Span{T}"/> receiving the found <see cref="Entity"/>s.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    public void GetEntities(in QueryDescription queryDescription, Span<Entity> list, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                list[start + index] = entity;
                index++;
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Archetype"/>s and puts them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies the components for which to search.</param>
    /// <param name="archetypes">The <see cref="Span{T}"/> receiving <see cref="Archetype"/>s containing <see cref="Entity"/>s with the matching components.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    public void GetArchetypes(in QueryDescription queryDescription, Span<Archetype> archetypes, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            archetypes[start + index] = archetype;
            index++;
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="chunks">The <see cref="Span{T}"/> receiving <see cref="Chunk"/>s containing <see cref="Entity"/>s with the matching components.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    public void GetChunks(in QueryDescription queryDescription, Span<Chunk> chunks, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            chunks[start + index] = chunk;
            index++;
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="Enumerator{T}"/> instance to iterate over all <see cref="Archetype"/>s.
    /// </summary>
    /// <returns>A new <see cref="Enumerator{T}"/> instance.</returns>
    [Pure]
    public Enumerator<Archetype> GetEnumerator()
    {
        return new Enumerator<Archetype>(Archetypes.AsSpan());
    }


    /// <summary>
    ///     Clears or resets this <see cref="World"/> instance. Will drop used <see cref="Archetypes"/> and therefore release some memory to the garbage collector.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    [StructuralChange]
    public void Clear()
    {
        Capacity = 0;
        Size = 0;

        // Clear
        RecycledIds.Clear();
        JobHandles.Clear();
        GroupToArchetype.Clear();
        EntityInfo.Clear();
        QueryCache.Clear();
        Archetypes.Clear();
    }

    /// <summary>
    ///     Disposes this <see cref="World"/> instance and removes it from the static <see cref="Worlds"/> list.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    [StructuralChange]
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    ///     Implementation of the dispose pattern.
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;
        var world = this;
#if !PURE_ECS
        lock (Worlds)
        {
            Worlds[world.Id] = null!;
            RecycledWorldIds.Enqueue(world.Id);
            Interlocked.Decrement(ref worldSizeUnsafe);
        }
#endif

        world.Capacity = 0;
        world.Size = 0;

        // Dispose
        world.JobHandles.Clear();
        world.GroupToArchetype.Clear();
        world.RecycledIds.Clear();
        world.QueryCache.Clear();
        world.Archetypes.Clear();
    }

    // It fails the WorldRecycle test.
    //~World()
    //{
    //    Dispose(false);
    //}

    /// <summary>
    ///     Converts this <see cref="World"/> to a human-readable <c>string</c>.
    /// </summary>
    /// <returns>A <c>string</c>.</returns>
    [Pure]
    public override string ToString()
    {
        return $"{GetType().Name} {{ {nameof(Id)} = {Id}, {nameof(Capacity)} = {Capacity}, {nameof(Size)} = {Size} }}";
    }
}

#endregion

// Archetype management of the world
#region Archetypes

public partial class World
{
    /// <summary>
    ///     Maps a <see cref="Components"/> hash to its <see cref="Archetype"/>.
    /// </summary>
    internal Dictionary<int, Archetype> GroupToArchetype {  get; set; }

        /// <summary>
    ///     Ensures the capacity of a specific <see cref="Archetype"/> determined by the <see cref="Signature"/>.
    /// </summary>
    /// <param name="signature">The <see cref="Signature"/>.</param>
    /// <param name="amount">The amount of <see cref="Entity"/>s that should fit in there.</param>
    /// <returns>The <see cref="Archetype"/> where the capacity was ensured.</returns>
    public Archetype EnsureCapacity(in Signature signature, int amount)
    {
        // Ensure size of archetype
        var archetype = GetOrCreate(signature);
        Capacity -= archetype.EntityCapacity;     // Reduce capacity, in case the previous capacity was already included, ensures more and more till memory leak
        archetype.EnsureEntityCapacity(archetype.EntityCount + amount);

        // Ensure size of world
        var requiredCapacity = Capacity + archetype.EntityCapacity;
        EntityInfo.EnsureCapacity(requiredCapacity);
        Capacity = requiredCapacity;

        return archetype;
    }

    /// <summary>
    ///     Ensures the capacity of a specific <see cref="Archetype"/> determined by the <see cref="Signature"/>.
    /// </summary>
    /// <param name="amount">The amount of <see cref="Entity"/>s that should fit in there.</param>
    /// <returns>The <see cref="Archetype"/> where the capacity was ensured.</returns>
    public Archetype EnsureCapacity<T>(int amount)
    {
        return EnsureCapacity(in Component<T>.Signature, amount);
    }

        /// <summary>
    ///     Returns an <see cref="Archetype"/> based on its components. If it does not exist, it will be created.
    /// </summary>
    /// <param name="signature">Its <see cref="ComponentType"/>s.</param>
    /// <returns>An existing or new <see cref="Archetype"/>.</returns>
    internal Archetype GetOrCreate(in Signature signature)
    {
        var hashCode = signature.GetHashCode();
        if (TryGetArchetype(hashCode, out var archetype))
        {
            return archetype;
        }

        // Create archetype
        archetype = new Archetype(signature, BaseChunkSize, BaseChunkEntityCount);

        GroupToArchetype[hashCode] = archetype;
        Archetypes.Add(archetype);

        // Archetypes always allocate one single chunk upon construction
        Capacity += archetype.EntitiesPerChunk;
        EntityInfo.EnsureCapacity(Capacity);

        return archetype;
    }

    /// <summary>
    ///     Tries to find an <see cref="Archetype"/> by the hash of its components.
    /// </summary>
    /// <param name="hash">Its hash.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwise false.</returns>
    [Pure]
    internal bool TryGetArchetype(int hash, [MaybeNullWhen(false)] out Archetype archetype)
    {
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Tries to find an <see cref="Archetype"/> by a provided <see cref="Signature"/>.
    /// </summary>
    /// <param name="signature">Its <see cref="Signature"/>.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwise false.</returns>
    [Pure]
    public bool TryGetArchetype(in Signature signature, [MaybeNullWhen(false)] out Archetype archetype)
    {
        return TryGetArchetype(signature.GetHashCode(), out archetype);
    }

    /// <summary>
    ///     Destroys the passed <see cref="Archetype"/> and removes it from this <see cref="World"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to destroy.</param>
    internal void DestroyArchetype(Archetype archetype)
    {
        var hash = Component.GetHashCode(archetype.Signature);
        Archetypes.Remove(archetype);
        GroupToArchetype.Remove(hash);

        // Remove archetype from other archetypes edges.
        foreach (var otherArchetype in this)
        {
            otherArchetype.RemoveEdge(archetype);
        }

        archetype.Clear();
        Capacity -= archetype.EntitiesPerChunk;
    }

    /// <summary>
    ///     Trims this <see cref="World"/> instance and releases unused memory.
    ///     Should not be called every single update or frame.
    ///     One single <see cref="Chunk"/> from each <see cref="Archetype"/> is spared.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    [StructuralChange]
    public void TrimExcess()
    {
        Capacity = 0;

        // Trim entity info and archetypes
        EntityInfo.TrimExcess();
        for (var index = Archetypes.Count - 1; index >= 0; index--)
        {
            // Remove empty archetypes.
            var archetype = Archetypes[index];
            if (archetype.EntityCount == 0)
            {
                Capacity += archetype.EntitiesPerChunk; // Since the destruction substracts that amount, add it before due to the way we calculate the new capacity.
                DestroyArchetype(archetype);
                continue;
            }

            archetype.TrimExcess();
            Capacity += archetype.EntityCapacity;
        }

        // Traverse recycled ids and remove all that are higher than the current capacity.
        // If we do not do this, a new entity might get a id higher than the entityinfo array which causes it to go out of bounds.
        RecycledIds = new Queue<RecycledEntity>(
            RecycledIds.Where(entity => entity.Id < Capacity)
        );
    }
}

#endregion

// Queries
#region Queries

public partial class World
{
    /// <summary>
    ///     Searches all matching <see cref="Entity"/>s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s are searched for.</param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>
    public void Query(in QueryDescription queryDescription, ForEach forEntity)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityLastElement = ref chunk.Entity(0);
            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityLastElement, entityIndex);
                forEntity(entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    ///     Inlines the call and is therefore faster than normal queries.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    public void InlineQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var t = new T();

        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                t.Update(entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    ///     Inlines the call and is therefore faster than normal queries.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    public void InlineQuery<T>(in QueryDescription queryDescription, ref T iForEach) where T : struct, IForEach
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                iForEach.Update(entity);
            }
        }
    }
}

#endregion

// Batch query operations
#region Batch Query Operations

public partial class World
{
    /// <summary>
    ///     An efficient method to destroy all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be destroyed.</param>
    [StructuralChange]
    public void Destroy(in QueryDescription queryDescription)
    {
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Size -= archetype.EntityCount; Commented since DestroyEntity already does Size--
            foreach (ref var chunk in archetype)
            {
                ref var entityFirstElement = ref chunk.Entity(0);
                foreach (var index in chunk)
                {
                    var entity = Unsafe.Add(ref entityFirstElement, index);

                    #if EVENTS
                    // Raise the OnComponentRemoved event for each component on the entity.
                    var arch = GetArchetype(entity);
                    foreach (var compType in arch.Signature)
                    {
                        OnComponentRemoved(entity, compType);
                    }
                    #endif

                    OnEntityDestroyed(entity);
                    DestroyEntityInternal(entity);
                }

                chunk.Clear();
            }

            archetype.Clear();
        }
    }

    /// <summary>
    ///     An efficient method to set one component for all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/> lookups which makes it as fast as a inline query.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    /// <param name="value">The value of the component to set.</param>
    public void Set<T>(in QueryDescription queryDescription, in T? value = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var componentFirstElement = ref chunk.GetFirst<T>();
            foreach (var index in chunk)
            {
                ref var component = ref Unsafe.Add(ref componentFirstElement, index);
                component = value;
#if EVENTS
                ref var entity = ref chunk.Entity(index);
                OnComponentSet<T>(entity);
#endif
            }
        }
    }

    /// <summary>
    ///     An efficient method to add one component to all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    /// <param name="component">The value of the component to add.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T>(in QueryDescription queryDescription, in T? component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if (archetype.EntityCount == 0 || archetype.Has<T>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T>.ComponentType.Id);

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, Set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in component);
            archetype.Clear();

            // Adjust capacity since the new archetype may have changed in size
            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T>(newArchetype);
        }

        EntityInfo.EnsureCapacity(Capacity);
    }

    /// <summary>
    ///     An efficient method to remove one component from <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if (archetype.EntityCount <= 0 || !archetype.Has<T>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T>.ComponentType.Id);

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T>(archetype);

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy and track capacity difference
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            // Adjust capacity since the new archetype may have changed in size
            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
}

#endregion

// Set, get and has
#region Accessors

public partial class World
{
    /// <summary>
    ///     Creates <see cref="Entity"/>s with <see cref="EntityData"/> in the given  <see cref="Archetype"/> without them already being added to the  <see cref="Archetype"/>.
    ///     They effectively point to slots in the  <see cref="Archetype"/>, but are not yet part of it.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <param name="entities">The <see cref="Span{T}"/> of <see cref="Entity"/>s where the entities will be written to.</param>
    /// <param name="entityData">The <see cref="Span{T}"/> of <see cref="EntityData"/> where the <see cref="EntityData"/>s will be written to.</param>
    /// <param name="amount">The amount of <see cref="Entity"/> to create.</param>
    internal void GetOrCreateEntitiesInternal(Archetype archetype, Span<Entity> entities, Span<EntityData> entityData, int amount)
    {
        // Rent array
        using var slotArray = Pool<Slot>.Rent(amount);
        var slots = slotArray.AsSpan();

        // Get slots for entities and put them into the lists
        Archetype.GetNextSlots(archetype, slots, amount);
        for(var index = 0; index < amount; index++)
        {
            GetOrCreateEntityInternal(out var entity);
            entities[index] = entity;
            entityData[index] = new EntityData(archetype, slots[index], entity.Version);
        }
    }

    /// <summary>
    ///     Adds <see cref="Entity"/> with their <see cref="EntityData"/> to the <see cref="EntityInfo"/>.
    /// </summary>
    /// <param name="entities">The <see cref="Entity"/>s.</param>
    /// <param name="entityData">The <see cref="EntityData"/>.</param>
    /// <param name="amount">The amount.</param>
    internal void AddEntityData(Span<Entity> entities, Span<EntityData> entityData, int amount)
    {
        // Transfer entity and data into the EntityInfo
        var existingEntityData = EntityInfo.EntityData;
        for (var index = 0; index < amount; index++)
        {
            var entity = entities[index];
            ref var data = ref entityData[index];
            existingEntityData.Add(entity.Id, in data);
        }
    }

    // TODO: Add entity creation event
    /// <summary>
    ///     Creates a set of <see cref="Entity"/>s of a certain <see cref="Signature"/> with default values and writes them into a desired <see cref="createdEntities"/>.
    /// </summary>
    /// <param name="createdEntities">An <see cref="Span{T}"/> with enough capacity to write all created <see cref="Entity"/>s into.</param>
    /// <param name="signature">The <see cref="Signature"/> each created entity will have.</param>
    /// <param name="amount">The amount of <see cref="Entity"/>s to create.</param>
    [StructuralChange]
    public void Create(Span<Entity> createdEntities, in Signature signature, int amount)
    {
        var archetype = EnsureCapacity(in signature, amount);

        // Rent arrays
        using var entityDataArray = Pool<EntityData>.Rent(amount);
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, createdEntities, entityData, amount);
        archetype.AddAll(createdEntities, amount);

        // Add entities to entityinfo
        AddEntityData(createdEntities, entityData, amount);
    }

    /// <summary>
    ///     Creates a set of <see cref="Entity"/> with the desired structure and components.
    /// </summary>
    /// <param name="amount">The amount of <see cref="Entity"/>s to create.</param>
    /// <param name="cmp">The component.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [StructuralChange]
    public void Create<T>(int amount, in T? cmp = default)
    {
        var archetype = EnsureCapacity<T>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange(in firstSlot, in lastSlot, cmp);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The instance, optional.</param>
    public void Set<T>(Entity entity, in T? component = default)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set(ref slot, in component);
        OnComponentSet<T>(entity);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired component, otherwise false.</returns>
    [Pure]
    public bool Has<T>(Entity entity)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Has<T>();
    }

    /// <summary>
    ///     Returns a reference to the <typeparamref name="T"/> component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the <typeparamref name="T"/> component.</returns>
    [Pure]
    public ref T Get<T>(Entity entity)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        return ref archetype.Get<T>(ref slot);
    }

    /// <summary>
    ///     Tries to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwise false.</returns>
    [Pure]
    public bool TryGet<T>(Entity entity, out T? component)
    {
        var slot = EntityInfo.GetEntityData(entity.Id);
        if (!slot.Archetype.TryIndex<T>(out int compIndex))
        {
            component = default;
            return false;
        }

        ref var chunk = ref slot.Archetype.GetChunk(slot.Slot.ChunkIndex);
        Debug.Assert(compIndex != -1 && compIndex < chunk.Components.Length, $"Index is out of bounds, component {typeof(T)} with id {compIndex} does not exist in this archetype.");

        var array = Unsafe.As<T[]>(chunk.Components.DangerousGetReferenceAt(compIndex));
        component = array[slot.Slot.Index];
        return true;
    }

    /// <summary>
    ///     Tries to return a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, otherwise false.</param>
    /// <returns>A reference to the component.</returns>
    [Pure]
    public ref T TryGetRef<T>(Entity entity, out bool exists)
    {
        var slot = EntityInfo.GetEntityData(entity.Id);

        if (!slot.Archetype.TryIndex<T>(out int compIndex))
        {
            exists = false;
            return ref Unsafe.NullRef<T>();
        }

        exists = true;
        ref var chunk = ref slot.Archetype.GetChunk(slot.Slot.ChunkIndex);
        Debug.Assert(compIndex != -1 && compIndex < chunk.Components.Length, $"Index is out of bounds, component {typeof(T)} with id {compIndex} does not exist in this archetype.");

        var array = Unsafe.As<T[]>(chunk.Components.DangerousGetReferenceAt(compIndex));
        return ref array[slot.Slot.Index];
    }

    /// <summary>
    ///     Ensures the existence of an component on an <see cref="Entity"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value used if its being added.</param>
    /// <returns>A reference to the component.</returns>
    [StructuralChange]
    public ref T AddOrGet<T>(Entity entity, T? component = default)
    {
        ref T cmp = ref TryGetRef<T>(entity, out var exists);
        if (exists)
        {
            return ref cmp;
        }

        Add(entity, component);
        return ref Get<T>(entity);
    }

    /// <summary>
    ///     Adds a new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="newArchetype">The <see cref="Entity"/>'s new <see cref="Archetype"/>.</param>
    /// <param name="slot">The new <see cref="Slot"/> in which the moved <see cref="Entity"/> will land.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [SkipLocalsInit]
    [StructuralChange]
    internal void Add<T>(Entity entity, out Archetype newArchetype, out Slot slot)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;
        var type = Component<T>.ComponentType;
        newArchetype = GetOrCreateArchetypeByAddEdge(in type, oldArchetype);

        Move(entity, ref data, oldArchetype, newArchetype, out slot);
    }

    /// <summary>
    ///     Adds a new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T>(Entity entity)
    {
        Add<T>(entity, out _, out _);
        OnComponentAdded<T>(entity);
    }

    /// <summary>
    ///     Adds a new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="component">The component instance.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T>(Entity entity, in T component)
    {
        Add<T>(entity, out var newArchetype, out var slot);
        newArchetype.Set(ref slot, component);
        OnComponentAdded<T>(entity);
    }

    /// <summary>
    ///     Removes an component from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var type = Component<T>.ComponentType;
        var newArchetype = GetOrCreateArchetypeByRemoveEdge(in type, oldArchetype);

        OnComponentRemoved<T>(entity);
        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }
}

#endregion

// Set & Get & Has non generic
#region Non-Generic Accessors

public partial class World
{

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component.</param>

    public void Set(Entity entity, object component)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        entitySlot.Archetype.Set(ref entitySlot.Slot, component);
        OnComponentSet(entity, component);
    }

    /// <summary>
    ///     Sets or replaces a <see cref="Span{T}"/> of components for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The <see cref="Span{T}"/> of components.</param>

    public void SetRange(Entity entity, Span<object> components)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        foreach (var cmp in components)
        {
            entitySlot.Archetype.Set(ref entitySlot.Slot, cmp);
            OnComponentSet(entity, cmp);
        }
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwise false.</returns>

    [Pure]
    public bool Has(Entity entity, ComponentType type)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Has(type);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwise false.</returns>

    [Pure]
    public bool HasRange(Entity entity, Span<ComponentType> types)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        foreach (var type in types)
        {
            if (!archetype.Has(type))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <returns>A reference to the component.</returns>

    [Pure]
    public object? Get(Entity entity, ComponentType type)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        return entitySlot.Archetype.Get(ref entitySlot.Slot, type);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/> as a <see cref="Span{T}"/>.</param>
    /// <returns>A reference to the component.</returns>

    [Pure]
    public object?[] GetRange(Entity entity, Span<ComponentType> types)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        var array = new object?[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            array[index] = entitySlot.Archetype.Get(ref entitySlot.Slot, type);
        }

        return array;
    }

    /// <summary>
    ///     Outputs the components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <param name="components">A <see cref="Span{T}"/> in which the components are put.</param>

    public void GetRange(Entity entity, Span<ComponentType> types, Span<object?> components)
    {
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            components[index] = entitySlot.Archetype.Get(ref entitySlot.Slot, type);
        }
    }

    /// <summary>
    ///     Tries to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwise false.</returns>

    [Pure]
    public bool TryGet(Entity entity, ComponentType type, out object? component)
    {
        var slot = EntityInfo.GetEntityData(entity.Id);

        if (!slot.Archetype.TryIndex(type, out int compIndex))
        {
            component = default;
            return false;
        }

        ref var chunk = ref slot.Archetype.GetChunk(slot.Slot.ChunkIndex);
        Debug.Assert(compIndex != -1 && compIndex < chunk.Components.Length, $"Index is out of bounds, component {type} with id {compIndex} does not exist in this archetype.");
        var array = Unsafe.As<object[]>(chunk.Components.DangerousGetReferenceAt(compIndex));
        component = array[slot.Slot.Index];
        return true;
    }

    /// <summary>
    ///     Adds a new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [SkipLocalsInit]

    [StructuralChange]
    public void Add(Entity entity, in object cmp)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];

        var oldArchetype = data.Archetype;
        var type = (ComponentType)cmp.GetType();
        var newArchetype = GetOrCreateArchetypeByAddEdge(in type, oldArchetype);

        Move(entity, ref data, oldArchetype, newArchetype, out var slot);
        newArchetype.Set(ref slot, cmp);
        OnComponentAdded(entity, type);
    }

    /// <summary>
    ///     Adds a <see cref="IList{T}"/> of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The <see cref="Span{T}"/> of components.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void AddRange(Entity entity, Span<object> components)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < components.Length; index++)
        {
            var type = Component.GetComponentType(components[index].GetType());
            spanBitSet.SetBit(type.Id);
        }

        // Get existing or new archetype
        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var newComponents = new ComponentType[components.Length];
            for (var index = 0; index < components.Length; index++)
            {
                newComponents[index] = (ComponentType)components[index].GetType();
            }

            var newSignature = Signature.Add(oldArchetype.Signature, newComponents);
            newArchetype = GetOrCreate(newSignature);
        }

        // Move and fire events
        Move(entity, ref data, oldArchetype, newArchetype, out var slot);
        foreach (var cmp in components)
        {
            newArchetype.Set(ref slot, cmp);
            OnComponentAdded(entity, cmp.GetType());
        }
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="world">The <see cref="World"/>.</param>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="Span{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void AddRange(Entity entity, Span<ComponentType> components)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < components.Length; index++)
        {
            var type = components[index];
            spanBitSet.SetBit(type.Id);
        }

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var newSignature = Signature.Add(oldArchetype.Signature, components.ToArray());
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out _);

#if EVENTS
        for (var i = 0; i < components.Length; i++)
        {
            OnComponentAdded(entity, components[i]);
        }
#endif
    }

    /// <summary>
    ///     Removes a <see cref="ComponentType"/> from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The <see cref="ComponentType"/> to remove from the <see cref="Entity"/>.</param>
    [StructuralChange]
    public void Remove(Entity entity, ComponentType type)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.ClearBit(type.Id);

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var newSignature = Signature.Remove(oldArchetype.Signature,type);
            newArchetype = GetOrCreate(newSignature);
        }

        OnComponentRemoved(entity, type);
        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <remarks>
    ///     Causes a structural change.
    /// </remarks>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="Span{T}"/> of <see cref="ComponentType"/>s, that are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [StructuralChange]
    public void RemoveRange(Entity entity, Span<ComponentType> types)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < types.Length; index++)
        {
            ref var cmp = ref types[index];
            spanBitSet.ClearBit(cmp.Id);
        }

        // Get or Create new archetype
        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var newSignature = Signature.Remove(oldArchetype.Signature, types);
            newArchetype = GetOrCreate(newSignature);
        }

        // Fire events and move
        foreach (var type in types)
        {
            OnComponentRemoved(entity, type);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }
}

#endregion

// Utility methods
#region Utility

public partial class World
{

    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive in this <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it exists and is alive, otherwise false.</returns>
    [Pure]
    public bool IsAlive(Entity entity)
    {
        ref var entityData = ref EntityInfo.TryGetEntityData(entity.Id, out var entityDataExists);
        return entity.Version > 0 && entityDataExists && entityData.Version == entity.Version;
    }

    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive in this <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists"></param>
    /// <returns>Its <see cref="EntityData"/>.</returns>
    [Pure]
    public ref EntityData IsAlive(Entity entity, out bool exists)
    {
        ref var entityData = ref EntityInfo.TryGetEntityData(entity.Id, out var entityDataExists);
        exists = entity.Version > 0 && entityDataExists && entityData.Version == entity.Version;
        return ref entityData;
    }

    /// <summary>
    ///     Returns the <see cref="EntityData"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>The <see cref="EntityData"/> associated with that <see cref="Entity"/>.</returns>
    [Pure]
    public ref EntityData GetEntityData(Entity entity)
    {
        return ref EntityInfo.GetEntityData(entity.Id);
    }

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    [Pure]
    public Archetype GetArchetype(Entity entity)
    {
        return EntityInfo.GetArchetype(entity.Id);
    }

    /// <summary>
    ///     Returns the <see cref="Chunk"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to its <see cref="Chunk"/>.</returns>
    [Pure]
    public ref readonly Chunk GetChunk(Entity entity)
    {
        var entityInfo = EntityInfo.GetEntityData(entity.Id);
        return ref entityInfo.Archetype.GetChunk(entityInfo.Slot.ChunkIndex);
    }

    /// <summary>
    ///     Returns all <see cref="ComponentType"/>s of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its array of <see cref="ComponentType"/>s.</returns>
    [Pure]
    public Signature GetSignature(Entity entity)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Signature;
    }

    /// <summary>
    ///     Returns all components of an <see cref="Entity"/> as an array.
    ///     Will allocate memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A newly allocated array containing the entities components.</returns>
    [Pure]
    public object?[] GetAllComponents(Entity entity)
    {
        // Get archetype and chunk.
        var entitySlot = EntityInfo.GetEntityData(entity.Id);
        var archetype = entitySlot.Archetype;
        ref var chunk = ref archetype.GetChunk(entitySlot.Slot.ChunkIndex);
        var components = chunk.Components;

        // Loop over components, collect and returns them.
        var entityIndex = entitySlot.Slot.Index;
        var cmps = new object?[components.Length];

        for (var index = 0; index < components.Length; index++)
        {
            var componentArray = components[index];
            var component = componentArray.GetValue(entityIndex);
            cmps[index] = component;
        }

        return cmps;
    }
}

#endregion
