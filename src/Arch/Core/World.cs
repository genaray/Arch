using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using CommunityToolkit.HighPerformance;
using JobScheduler;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;
using Component = Arch.Core.Utils.Component;

namespace Arch.Core;

/// <summary>
///     The <see cref="RecycledEntity"/> struct
///     stores information about an recycled <see cref="Entity"/>, its id and its version.
/// </summary>
internal record struct RecycledEntity
{
    /// <summary>
    ///     The recycled id.
    /// </summary>
    public int Id;

    /// <summary>
    ///     Its new version.
    /// </summary>
    public int Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecycledEntity"/> struct.
    /// </summary>
    /// <param name="id">Its id..</param>
    /// <param name="version">Its version.</param>
    public RecycledEntity(int id, int version)
    {
        Id = id;
        Version = version;
    }
}

/// <summary>
///     The <see cref="EntityInfo"/> struct
///     stores information about an <see cref="Entity"/> to quickly access its data and location.
/// </summary>
[SkipLocalsInit]
internal record struct EntityInfo
{
    /// <summary>
    /// Its slot inside its <see cref="Archetype"/>.
    /// </summary>
    public Slot Slot;

    /// <summary>
    /// Its <see cref="Archetype"/>.
    /// </summary>
    public Archetype Archetype;

    /// <summary>
    /// Its version.
    /// </summary>
    public int Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityInfo"/> struct.
    /// </summary>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="version">Its version.</param>
    public EntityInfo(Slot slot, Archetype archetype, int version)
    {
        Slot = slot;
        Archetype = archetype;
        Version = version;
    }
}

/// <summary>
///     The <see cref="IForEach"/> interface
///     provides a method to execute logic on an <see cref="Entity"/>.
///     Commonly used for queries to provide an clean interface.
/// </summary>
public interface IForEach
{
    /// <summary>
    ///     Called on an <see cref="Entity"/> to execute logic on it.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(in Entity entity);
}

/// <summary>
///     The <see cref="ForEach"/> delegate
///     provides a callback to execute logic on an <see cref="Entity"/>.
/// </summary>
/// <param name="entity">The <see cref="Entity"/>.</param>
public delegate void ForEach(in Entity entity);

/// <summary>
///     The <see cref="World"/> class
///     stores <see cref="Entity"/>'s in <see cref="Archetype"/>'s and <see cref="Chunk"/>'s, manages them and provides methods to query for specific <see cref="Entity"/>'s.
/// </summary>
public partial class World : IDisposable
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="World"/> class
    /// </summary>
    /// <param name="id">Its unique id.</param>
    internal World(int id)
    {
        Id = id;

        // Mapping.
        GroupToArchetype = new PooledDictionary<int, Archetype>(8);

        // Entity stuff.
        Archetypes = new PooledList<Archetype>(8, ClearMode.Never);
        EntityInfo = new EntityInfoDictionary(256);
        RecycledIds = new PooledQueue<RecycledEntity>(256);

        // Query.
        QueryCache = new PooledDictionary<QueryDescription, Query>(8);

        // Multithreading/Jobs.
        JobHandles = new PooledList<JobHandle>(Environment.ProcessorCount);
        JobsCache = new List<IJob>(Environment.ProcessorCount);
    }


    /// <summary>
    ///     A list of all existing <see cref="Worlds"/>.
    ///     Should not be modified by the user.
    /// </summary>
    public static List<World> Worlds { get; } = new(1);

    /// <summary>
    ///     The unique <see cref="World"/> id.
    /// </summary>
    public int Id { get; }

    /// <summary>
    ///     The amount of <see cref="Entity"/>'s stored by this instance.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     The available capacity for <see cref="Entity"/>'s by this instance.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    ///     All <see cref="Archetype"/>'s that exist in this <see cref="World"/>.
    /// </summary>
    public PooledList<Archetype?> Archetypes { get; }

    /// <summary>
    ///     Mapt an <see cref="Entity"/> to its <see cref="EntityInfo"/> for quick lookups.
    /// </summary>
    internal EntityInfoDictionary EntityInfo { get; }

    /// <summary>
    ///     Stores recycled <see cref="Entity"/> ids and their last version.
    /// </summary>
    internal PooledQueue<RecycledEntity> RecycledIds { get; set; }

    /// <summary>
    ///     A cache to map <see cref="QueryDescription"/> to their <see cref="Arch.Core.Query"/> to avoid allocs.
    /// </summary>
    internal PooledDictionary<QueryDescription, Query> QueryCache { get; set; }

    /// <summary>
    ///     Creates a <see cref="World"/> instance.
    /// </summary>
    /// <returns>The created <see cref="World"/> instance.</returns>
    public static World Create()
    {
        var worldSize = Worlds.Count;
        var world = new World(worldSize);
        Worlds.Add(world);

        return world;
    }

    /// <summary>
    ///     Destroys an existing <see cref="World"/>.
    /// </summary>
    /// <param name="world">The <see cref="World"/>.</param>
    public static void Destroy(World world)
    {
        Worlds.Remove(world);

        world.Capacity = 0;
        world.Size = 0;

        // Dispose
        world.JobHandles.Dispose();
        world.GroupToArchetype.Dispose();
        world.RecycledIds.Dispose();
        world.QueryCache.Dispose();

        // Set archetypes to null to free them manually since Archetypes are set to ClearMode.Never to fix #65
        for (var index = 0; index < world.Archetypes.Count; index++)
        {
            world.Archetypes[index] = null;
        }

        world.Archetypes.Dispose();
    }

    /// <summary>
    ///     Reserves space for a certain number of <see cref="Entity"/>'s of a given component structure/<see cref="Archetype"/>.
    /// </summary>
    /// <param name="types">The component structure/<see cref="Archetype"/>.</param>
    /// <param name="amount">The amount.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(ComponentType[] types, int amount)
    {
        var archetype = GetOrCreate(types);
        archetype.Reserve(amount);

        var requiredCapacity = Capacity + amount;
        EntityInfo.EnsureCapacity(requiredCapacity);
        Capacity = requiredCapacity;
    }

    /// <summary>
    ///     Creates a new <see cref="Entity"/> using its given component structure/<see cref="Archetype"/>.
    ///     Might resize its target <see cref="Archetype"/> and allocate new space if its full.
    /// </summary>
    /// <param name="types">Its component structure/<see cref="Archetype"/>.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(params ComponentType[] types)
    {
        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var recycled = recycle ? recycledId : new RecycledEntity(Size, 0);

        // Create new entity and put it to the back of the array
        var entity = new Entity(recycled.Id, Id);

        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        var createdChunk = archetype.Add(in entity, out var slot);

        // Resize map & Array to fit all potential new entities
        if (createdChunk)
        {
            Capacity += archetype.EntitiesPerChunk;
            EntityInfo.EnsureCapacity(Capacity);
        }

        // Map
        EntityInfo.Add(recycled.Id, new EntityInfo(slot, archetype, recycled.Version));

        Size++;
        return entity;
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> from one <see cref="Archetype"/> <see cref="Slot"/> to another.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="from">Its <see cref="Archetype"/>.</param>
    /// <param name="to">The new <see cref="Archetype"/>.</param>
    /// <param name="newSlot">The new <see cref="Slot"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Move(in Entity entity, Archetype from, Archetype to, out Slot newSlot)
    {
        // A common mistake, happening in many cases.
        Debug.Assert(from != to, "From-Archetype is the same as the To-Archetype. Entities cannot move within the same archetype using this function. Probably an attempt was made to attach already existing components to the entity or to remove non-existing ones.");

        // Cache id since in Entity is basically ref readonly entity.
        var id = entity.Id;

        // Copy entity to other archetype
        ref var entityInfo = ref EntityInfo[id];
        var created = to.Add(in entity, out newSlot);
        Archetype.CopyComponents(from, ref entityInfo.Slot, to,ref newSlot);
        from.Remove(ref entityInfo.Slot, out var movedEntity);

        // Update moved entity from the remove
        ref var movedEntityInfo = ref EntityInfo[movedEntity];
        movedEntityInfo.Slot = entityInfo.Slot;

        // Update mapping of target entity
        entityInfo.Archetype = to;
        entityInfo.Slot = newSlot;

        // Calculate the entity difference between the moved archetypes to allocate more space accordingly.
        var difference = 0;
        if (created)
        {
            difference += to.EntitiesPerChunk;
            Capacity += difference;
            EntityInfo.EnsureCapacity(Capacity);
        }
    }

    /// <summary>
    ///     Destroys an <see cref="Entity"/>.
    ///     Might resize its target <see cref="Archetype"/> and release memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in Entity entity)
    {
        // Cache id since in Entity is basically ref readonly entity.
        var id = entity.Id;

        // Remove from archetype
        var entityInfo = EntityInfo[id];
        var archetype = entityInfo.Archetype;
        archetype.Remove(ref entityInfo.Slot, out var movedEntityId);

        // Update info of moved entity which replaced the removed entity.
        var movedEntityInfo = EntityInfo[movedEntityId];
        movedEntityInfo.Slot = entityInfo.Slot;
        EntityInfo[movedEntityId] = movedEntityInfo;

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(new RecycledEntity(id, unchecked(entityInfo.Version+1)));
        EntityInfo.Remove(id);

        Size--;
    }

    /// <summary>
    ///     Trims this <see cref="World"/> instance and releases unused memory.
    ///     Should not be called every single update or frame.
    ///     One single <see cref="Chunk"/> from each <see cref="Archetype"/> is spared.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TrimExcess()
    {
        Capacity = 0;

        // Trim entity info and chunks
        EntityInfo.TrimExcess();
        foreach (ref var archetype in this)
        {
            archetype.TrimExcess();
            Capacity += archetype.Size * archetype.EntitiesPerChunk; // Since always one chunk always exists.
        }
    }

    /// <summary>
    ///     Clears or resets this <see cref="World"/> instance, will drop used <see cref="Archetypes"/> and therefore release some memory sooner or later.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Capacity = 0;
        Size = 0;

        // Clear
        RecycledIds.Clear();
        JobHandles.Clear();
        GroupToArchetype.Clear();
        EntityInfo.Clear();
        RecycledIds.Clear();
        QueryCache.Clear();

        // Set archetypes to null to free them manually since Archetypes are set to ClearMode.Never to fix #65
        for (var index = 0; index < Archetypes.Count; index++)
        {
            Archetypes[index] = null;
        }
        Archetypes.Clear();
    }

    /// <summary>
    ///     Creates a <see cref="Arch.Core.Query"/> using a <see cref="QueryDescription"/>
    ///     which can be used to iterate over the matching <see cref="Entity"/>'s, <see cref="Archetype"/>'s and <see cref="Chunk"/>'s.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Query Query(in QueryDescription queryDescription)
    {
        // Looping over all archetypes, their chunks and their entities.
        if (QueryCache.TryGetValue(queryDescription, out var query))
        {
            return query;
        }

        query = new Query(Archetypes, queryDescription);
        QueryCache[queryDescription] = query;

        return query;
    }

    /// <summary>
    ///     Counts all <see cref="Entity"/>'s that match a <see cref="QueryDescription"/> and returns the number.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CountEntities(in QueryDescription queryDescription)
    {
        var counter = 0;
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var entities = archetype.Entities;
            counter += entities;
        }

        return counter;
    }

    /// <summary>
    ///     Search all matching <see cref="Entity"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    /// <param name="list">The <see cref="IList{T}"/> receiving the found <see cref="Entity"/>'s.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, IList<Entity> list)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                list.Add(entity);
            }
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Entity"/>'s and put them into the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    /// <param name="list">The <see cref="Span{T}"/> receiving the found <see cref="Entity"/>'s.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>, will append after that index. Default is 0.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, Span<Entity> list, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                list[start+index] = entity;
                index++;
            }
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Archetype"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="archetypes">The <see cref="IList{T}"/> receiving <see cref="Archetype"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetArchetypes(in QueryDescription queryDescription, IList<Archetype> archetypes)
    {
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            archetypes.Add(archetype);
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Chunk"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="chunks">The <see cref="IList{T}"/> receiving <see cref="Chunk"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetChunks(in QueryDescription queryDescription, IList<Chunk> chunks)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            chunks.Add(chunk);
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="Enumerator{T}"/> instance to iterate over all <see cref="Archetypes"/>.
    /// </summary>
    /// <returns>A new <see cref="Enumerator{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Enumerator<Archetype> GetEnumerator()
    {
        return new Enumerator<Archetype>(Archetypes.Span);
    }

    /// <summary>
    ///     Disposes this <see cref="World"/> instance and destroys it from the <see cref="Worlds"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        Destroy(this);
    }

    /// <summary>
    ///     Converts this <see cref="World"/> to a human readable string.
    /// </summary>
    /// <returns>A string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"World {{ {nameof(Id)} = {Id}, {nameof(Capacity)} = {Capacity}, {nameof(Size)} = {Size} }}";
    }
}

public partial class World
{

    /// <summary>
    ///     Maps an <see cref="Group"/> hash to its <see cref="Archetype"/>.
    /// </summary>
    internal PooledDictionary<int, Archetype> GroupToArchetype { get; set; }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by the hash of its components.
    /// </summary>
    /// <param name="types">Its <see cref="ComponentType"/>'s.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool TryGetArchetype(int hash, out Archetype archetype)
    {
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by the hash of its components.
    /// </summary>
    /// <param name="types">Its <see cref="ComponentType"/>'s.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(ComponentType[] types, out Archetype archetype)
    {
        var hash = Component.GetHashCode(types);
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }


    /// <summary>
    ///     Returned an <see cref="Archetype"/> based on its components. If it does not exist, it will be created.
    /// </summary>
    /// <param name="types">Its <see cref="ComponentType"/>'s.</param>
    /// <returns>An existing or new <see cref="Archetype"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Archetype GetOrCreate(ComponentType[] types)
    {
        if (TryGetArchetype(types, out var archetype))
        {
            return archetype;
        }

        // Create archetype
        archetype = new Archetype(types);
        var hash = Component.GetHashCode(types);

        GroupToArchetype[hash] = archetype;
        Archetypes.Add(archetype);

        // Archetypes always allocate one single chunk upon construction
        Capacity += archetype.EntitiesPerChunk;
        EntityInfo.EnsureCapacity(Capacity);

        return archetype;
    }
}

public partial class World
{
    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query(in QueryDescription queryDescription, ForEach forEntity)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityLastElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityLastElement, entityIndex);
                forEntity(entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    ///     Inlines the call and is therefore faster than normal queries.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var t = new T();

        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
            foreach (var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                t.Update(in entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    ///     Inlines the call and is therefore faster than normal queries.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineQuery<T>(in QueryDescription queryDescription, ref T iForEach) where T : struct, IForEach
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                iForEach.Update(in entity);
            }
        }
    }
}

// Multithreading / Parallel

public partial class World
{

    /// <summary>
    ///     A list of <see cref="JobHandle"/> which are pooled to avoid allocs.
    /// </summary>
    private PooledList<JobHandle> JobHandles { get; }

    /// <summary>
    ///     A cache used for the parallel queries to prevent list allocations.
    /// </summary>
    internal List<IJob> JobsCache { get; set; }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/> delegate.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery(in QueryDescription queryDescription, ForEach forEntity)
    {
        var foreachJob = new ForEachJob
        {
            ForEach = forEntity
        };

        InlineParallelChunkQuery(in queryDescription, foreachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        InlineParallelChunkQuery(in queryDescription, iForEachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelQuery<T>(in QueryDescription queryDescription, in IForEachJob<T> iForEach) where T : struct, IForEach
    {
        InlineParallelChunkQuery(in queryDescription, in iForEach);
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>'s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (JobScheduler.JobScheduler.Instance is null)
        {
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        }

        // Cast pool in an unsafe fast way and run the query.
        var pool = JobMeta<ChunkIterationJob<T>>.Pool;
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = pool.Get();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                JobsCache.Add(job);
            }

            // Schedule, flush, wait, return.
            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool.
            for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
            {
                var job = Unsafe.As<ChunkIterationJob<T>>(JobsCache[jobIndex]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }
}

// Batch query operations

public partial class World
{

    /// <summary>
    ///     Updates the <see cref="EntityInfo"/> and all entities that moved/shifted between the archetypes.
    /// </summary>
    /// <param name="archetype">The old <see cref="Archetype"/>.</param>
    /// <param name="archetypeSlot">The old <see cref="Slot"/> where the shift operation started.</param>
    /// <param name="newArchetype">The new <see cref="Archetype"/>.</param>
    /// <param name="newArchetypeSlot">The new <see cref="Slot"/> where the entities were shifted to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ShiftEntityInfo(Archetype archetype, Slot archetypeSlot, Archetype newArchetype, Slot newArchetypeSlot)
    {
        // Update the entityInfo of all copied entities.
        for (var chunkIndex = archetypeSlot.ChunkIndex; chunkIndex >= 0; --chunkIndex)
        {
            ref var chunk = ref archetype.GetChunk(chunkIndex);
            ref var entityFirstElement = ref chunk.Entities.DangerousGetReference();
            for (var index = archetypeSlot.Index; index >= 0; --index)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, index);

                // Calculate new entity slot based on its old slot.
                var entitySlot = new Slot(index, chunkIndex);
                var newSlot = Slot.Shift(entitySlot, archetype.EntitiesPerChunk, newArchetypeSlot, newArchetype.EntitiesPerChunk);

                // Update entity info
                var entityInfo = EntityInfo[entity.Id];
                entityInfo.Slot = newSlot;
                entityInfo.Archetype = newArchetype;
                EntityInfo[entity.Id] = entityInfo;
            }
        }
    }

    /// <summary>
    ///     An efficient method to destroy all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s will be destroyed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in QueryDescription queryDescription)
    {
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            Size -= archetype.Entities;
            foreach (ref var chunk in archetype)
            {
                ref var entityFirstElement = ref chunk.Entities.DangerousGetReference();
                foreach (var index in chunk)
                {
                    ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, index);

                    var info = EntityInfo[entity.Id];
                    var recycledEntity = new RecycledEntity(entity.Id, info.Version);

                    RecycledIds.Enqueue(recycledEntity);
                    EntityInfo.Remove(entity.Id);
                }

                chunk.Clear();
            }

            archetype.Clear();
        }
    }

    /// <summary>
    ///     An efficient method to set one component for all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/> lookups which makes it as fast as a inlin query.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in QueryDescription queryDescription, in T value = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var componentFirstElement = ref chunk.GetFirst<T>();
            foreach (var index in chunk)
            {
                ref var component = ref Unsafe.Add(ref componentFirstElement, index);
                component = value;
            }
        }
    }

    /// <summary>
    ///     An efficient method to add one component to all <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in QueryDescription queryDescription, in T component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.Entities == 0 || archetype.Has<T>())
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
                newArchetype = GetOrCreate(archetype.Types.Add(typeof(T)));
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.LastSlot;
            var newArchetypeLastSlot = newArchetype.LastSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);

            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            // Set added value and update the entity info
            var lastSlot = newArchetype.LastSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in component);
            ShiftEntityInfo(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);
        }
    }

    /// <summary>
    ///     An efficient method to remove one component from <see cref="Entity"/>s matching a <see cref="QueryDescription"/>.
    ///     No <see cref="Entity"/>s are recopied which is much faster.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>s will be targeted.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.Entities <= 0 || !archetype.Has<T>())
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
                newArchetype = GetOrCreate(archetype.Types.Remove(typeof(T)));
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.LastSlot;
            var newArchetypeLastSlot = newArchetype.LastSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);

            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            ShiftEntityInfo(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);
        }
    }
}

// Set, get and has

public partial class World
{
    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The instance, optional.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T cmp = default)
    {
        var entityInfo = EntityInfo[entity.Id];
        entityInfo.Archetype.Set(ref entityInfo.Slot, in cmp);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(in Entity entity)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
        return archetype.Has<T>();
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(in Entity entity)
    {
        var entityInfo = EntityInfo[entity.Id];
        return ref entityInfo.Archetype.Get<T>(ref entityInfo.Slot);
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet<T>(in Entity entity, out T component)
    {
        component = default;
        if (!Has<T>(in entity))
        {
            return false;
        }

        var entityInfo = EntityInfo[entity.Id];
        component = entityInfo.Archetype.Get<T>(ref entityInfo.Slot);
        return true;
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, oterhwhise false.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T TryGetRef<T>(in Entity entity, out bool exists)
    {
        if (!(exists = Has<T>(in entity)))
        {
            return ref Unsafe.NullRef<T>();
        }

        var entityInfo = EntityInfo[entity.Id];
        return ref entityInfo.Archetype.Get<T>(ref entityInfo.Slot);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(Component<T>.ComponentType.Id);

        // Search for fitting archetype or create a new one
        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="cmp">The component instance.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity, in T cmp)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(Component<T>.ComponentType.Id);

        // Search for fitting archetype or create a new one
        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));
        }

        Move(in entity, oldArchetype, newArchetype, out var slot);
        newArchetype.Set(ref slot, cmp);
    }


    /// <summary>
    ///     Removes an component from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in Entity entity)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.ClearBit(Component<T>.ComponentType.Id);

        // Search for fitting archetype or create a new one
        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(typeof(T)));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }
}

// Set & Get & Has non generic

public partial class World
{

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(in Entity entity, object cmp)
    {
        var entityInfo = EntityInfo[entity.Id];
        entityInfo.Archetype.Set(ref entityInfo.Slot, cmp);
    }

    /// <summary>
    ///     Sets or replaces a <see cref="IList{T}"/> of components for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The components <see cref="IList{T}"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetRange(in Entity entity, params object[] components)
    {
        var entityInfo = EntityInfo[entity.Id];
        foreach (var cmp in components)
        {
            entityInfo.Archetype.Set(ref entityInfo.Slot, cmp);
        }
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(in Entity entity, ComponentType type)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
        return archetype.Has(type);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasRange(in Entity entity, params ComponentType[] types)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object Get(in Entity entity, ComponentType type)
    {
        var entityInfo = EntityInfo[entity.Id];
        return entityInfo.Archetype.Get(ref entityInfo.Slot, type);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object[] GetRange(in Entity entity, params ComponentType[] types)
    {
        var entityInfo = EntityInfo[entity.Id];

        var array = new object[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            array[index] = entityInfo.Archetype.Get(ref entityInfo.Slot, type);
        }

        return array;
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> where the components are put it.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetRange(in Entity entity, ComponentType[] types, IList<object> components)
    {
        var entityInfo = EntityInfo[entity.Id];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            components.Add(entityInfo.Archetype.Get(ref entityInfo.Slot, type));
        }
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(in Entity entity, ComponentType type, out object component)
    {
        component = default;
        if (!Has(in entity, type))
        {
            return false;
        }

        var entityInfo = EntityInfo[entity.Id];
        component = entityInfo.Archetype.Get(ref entityInfo.Slot, type);
        return true;
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(in Entity entity, in object cmp)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(Component.GetComponentType(cmp.GetType()).Id);

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(cmp.GetType()));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Adds a <see cref="IList{T}"/> of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The component <see cref="IList{T}"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddRange(in Entity entity, params object[] components)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

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

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var newComponents = components.Select(o => (ComponentType)o).ToArray();
            newArchetype = GetOrCreate(oldArchetype.Types.Add(newComponents));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddRange(in Entity entity, IList<ComponentType> components)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < components.Count; index++)
        {
            var type = Component.GetComponentType(components[index]);
            spanBitSet.SetBit(type.Id);
        }

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(components));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes one single of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The <see cref="ComponentType"/> to remove from the the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(in Entity entity, ComponentType type)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.ClearBit(type.Id);

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(type));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveRange(in Entity entity, params ComponentType[] types)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

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

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(types));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveRange(in Entity entity, IList<ComponentType> types)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < types.Count; index++)
        {
            var cmp = types[index];
            spanBitSet.ClearBit(cmp.Id);
        }

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(types));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }
}

// Utility methods

public partial class World
{
    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive in this <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it exists and is alive, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive(in Entity entity)
    {
        return EntityInfo.TryGetValue(entity.Id, out _);
    }

    /// <summary>
    ///     Returns the version of an <see cref="Entity"/>.
    ///     Indicating how often it was recycled.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its version.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Version(in Entity entity)
    {
        return EntityInfo[entity.Id].Version;
    }

    /// <summary>
    ///     Returns a <see cref="EntityReference"/> to an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="EntityReference"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityReference Reference(in Entity entity)
    {
        var entityInfo = EntityInfo.TryGetValue(entity.Id, out var info);
        return new EntityReference(in entity, entityInfo ? info.Version : 0);
    }

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype GetArchetype(in Entity entity)
    {
        return EntityInfo[entity.Id].Archetype;
    }

    /// <summary>
    ///     Returns the <see cref="Chunk"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to its <see cref="Chunk"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref readonly Chunk GetChunk(in Entity entity)
    {
        var entityInfo = EntityInfo[entity.Id];
        return ref entityInfo.Archetype.GetChunk(entityInfo.Slot.ChunkIndex);
    }

    /// <summary>
    ///     Returns all <see cref="ComponentType"/>'s of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>'s array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentType[] GetComponentTypes(in Entity entity)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
        return archetype.Types;
    }

    /// <summary>
    ///     Returns all components of an <see cref="Entity"/> as an array.
    ///     Will allocate memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A newly allocated array containing the entities components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object[] GetAllComponents(in Entity entity)
    {
        // Get archetype and chunk.
        var entityInfo = EntityInfo[entity.Id];
        var archetype = entityInfo.Archetype;
        ref var chunk = ref archetype.GetChunk(entityInfo.Slot.ChunkIndex);
        var components = chunk.Components;

        // Loop over components, collect and returns them.
        var entityIndex = entityInfo.Slot.Index;
        var cmps = new object[components.Length];

        for (var index = 0; index < components.Length; index++)
        {
            var componentArray = components[index];
            var component = componentArray.GetValue(entityIndex);
            cmps[index] = component;
        }

        return cmps;
    }
}

