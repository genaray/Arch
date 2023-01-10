using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using JobScheduler;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;
using Component = Arch.Core.Utils.Component;

namespace Arch.Core;

#if PURE_ECS
/// <summary>
///     The <see cref="Entity"/> struct
///     represents a general-purpose object and can be assigned a set of components that act as data.
/// </summary>
public readonly struct Entity : IEquatable<Entity>
{
    /// <summary>
    ///     Its Id, unique in its <see cref="World"/>.
    /// </summary>
    public readonly int Id;

    /// <summary>
    ///     A null entity, used for comparison.
    /// </summary>
    public static readonly Entity Null = new(-1, 0);

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="worldId">Its world id, not used for this entity since its pure ecs.</param>
    internal Entity(int id, int worldId)
    {
        Id = id;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>True if equal, false if not.</returns>
    public bool Equals(Entity other)
    {
        return Id == other.Id;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another object..
    /// </summary>
    /// <param name="obj">The other <see cref="Entity"/> object.</param>
    /// <returns>True if equal, false if not.</returns>
    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }

    /// <summary>
    ///     Calculates the hash of this <see cref="Entity"/>.
    /// </summary>
    /// <returns>Its hash.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap
            var hash = 17;
            hash = hash * 23 + Id;
            return hash;
        }
    }

    /// <summary>
    ///     Checks the left <see cref="Entity"/> for equality with the right one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if both are equal, otherwhise false.</returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks the left <see cref="Entity"/> for unequality with the right one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if both are unequal, otherwhise false.</returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Converts this entity to a string.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}";
    }
}
#else

/// <summary>
///     The <see cref="Entity"/> struct
///     represents a general-purpose object and can be assigned a set of components that act as data.
/// </summary>
public readonly struct Entity : IEquatable<Entity>
{

    /// <summary>
    ///      Its Id, unique in its <see cref="World"/>.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Its <see cref="World"/> id.
    /// </summary>
    public readonly int WorldId;

    /// <summary>
    ///     A null <see cref="Entity"/> used for comparison.
    /// </summary>
    public static Entity Null = new(-1, 0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="worldId">Its <see cref="World"/> id.</param>
    internal Entity(int id, int worldId)
    {
        Id = id;
        WorldId = worldId;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>True if equal, false if not.</returns>
    public bool Equals(Entity other)
    {
        return Id == other.Id && WorldId == other.WorldId;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another <see cref="object"/>.
    /// </summary>
    /// <param name="obj">The other <see cref="Entity"/> object.</param>
    /// <returns>True if equal, false if not.</returns>
    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }

    /// <summary>
    ///     Calculates the hash of this <see cref="Entity"/>.
    /// </summary>
    /// <returns>Its hash.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap
            var hash = 17;
            hash = (hash * 23) + Id;
            hash = (hash * 23) + WorldId;
            return hash;
        }
    }

    /// <summary>
    ///      Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if equal, otherwhise false.</returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///      Checks the <see cref="Entity"/> for unequality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if unequal, otherwhise false.</returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Converts this <see cref="Entity"/> to a string.
    /// </summary>
    /// <returns>Its string.</returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(WorldId)}: {WorldId}";
    }
}
#endif

/// <summary>
///     The <see cref="EntityInfo"/> struct
///     stores information about an <see cref="Entity"/> to quickly access its data and location.
/// </summary>
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
    public short Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityInfo"/> struct.
    /// </summary>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="version">Its version.</param>
    public EntityInfo(Slot slot, Archetype archetype, short version)
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
public partial class World
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
        Archetypes = new PooledList<Archetype>(8);
        EntityInfo = new PooledDictionary<int, EntityInfo>(256);
        RecycledIds = new PooledQueue<int>(256);

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
    public PooledList<Archetype> Archetypes { get; }

    /// <summary>
    ///     Mapt an <see cref="Entity"/> to its <see cref="EntityInfo"/> for quick lookups.
    /// </summary>
    internal PooledDictionary<int, EntityInfo> EntityInfo { get; }

    /// <summary>
    ///     Stores recycled <see cref="Entity"/> ids.
    /// </summary>
    internal PooledQueue<int> RecycledIds { get; set; }

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
        world.JobHandles.Dispose();
        world.GroupToArchetype.Dispose();
        world.Archetypes.Dispose();
        world.EntityInfo.Dispose();
        world.RecycledIds.Dispose();
        world.QueryCache.Dispose();
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
        var id = recycle ? recycledId : Size;

        // Create new entity and put it to the back of the array
        var entity = new Entity(id, Id);

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
        EntityInfo.Add(id, new EntityInfo(slot, archetype, 0));

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
        // Copy entity to other archetype
        var entityInfo = EntityInfo[entity.Id];
        var created = to.Add(in entity, out newSlot);
        from.CopyTo(to, ref entityInfo.Slot, ref newSlot);
        var destroyed = from.Remove(ref entityInfo.Slot, out var movedEntity);

        // Update moved entity from the remove
        var movedEntityInfo = EntityInfo[movedEntity];
        movedEntityInfo.Slot = entityInfo.Slot;
        EntityInfo[movedEntity] = movedEntityInfo;

        // Update mapping of target entity
        entityInfo.Archetype = to;
        entityInfo.Slot = newSlot;
        EntityInfo[entity.Id] = entityInfo;

        // Calculate the entity difference between the moved archetypes to allocate more space accordingly.
        var difference = 0;
        if (created)
        {
            difference += to.EntitiesPerChunk;
        }

        if (destroyed)
        {
            difference -= from.EntitiesPerChunk;
        }

        Capacity += difference;

        if (difference >= 0)
        {
            EntityInfo.EnsureCapacity(Capacity);
        }
        else
        {
            EntityInfo.TrimExcess(Capacity);
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
        var destroyedChunk = archetype.Remove(ref entityInfo.Slot, out var movedEntityId);

        // Update info of moved entity which replaced the removed entity.
        var movedEntityInfo = EntityInfo[movedEntityId];
        movedEntityInfo.Slot = entityInfo.Slot;
        EntityInfo[movedEntityId] = movedEntityInfo;

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(id);
        EntityInfo.Remove(id);

        // Resizing and releasing memory
        if (destroyedChunk)
        {
            Capacity -= archetype.EntitiesPerChunk;
            EntityInfo.TrimExcess(Capacity);
        }

        Size--;
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
    ///     Search all matching <see cref="Entity"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    /// <param name="list">The <see cref="IList{T}"/> receiving the found <see cref="Entity"/>'s.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, IList<Entity> list)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                list.Add(entity);
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
        foreach (ref var archetype in query.GetArchetypeIterator())
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
        foreach (ref var chunk in query.GetChunkIterator())
        {
            chunks.Add(chunk);
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="Enumerator{T}"/> instance to iterate over all <see cref="Archetypes"/>.
    /// </summary>
    /// <returns>A new <see cref="Enumerator{T}"/> instance.</returns>
    public Enumerator<Archetype> GetEnumerator()
    {
        return new Enumerator<Archetype>(Archetypes.Span);
    }

    /// <summary>
    ///     Disposes this <see cref="World"/> instance and destroys it from the <see cref="Worlds"/>.
    /// </summary>
    public void Dispose()
    {
        Destroy(this);
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
    public bool TryGetArchetype(ComponentType[] types, out Archetype archetype)
    {
        var hash = Component.GetHashCode(types);
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by the ids of its components.
    ///     Those are being represented as a <see cref="Span{T}"/> array of ints.
    /// </summary>
    /// <param name="ids">A <see cref="Span{T}"/> with the component ids.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Span<int> ids, out Archetype archetype)
    {
        var hash = Component.GetHashCode(ids);
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
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityLastElement = ref ArrayExtensions.DangerousGetReferenceAt(chunk.Entities, chunkSize - 1);

            for (var entityIndex = 0; entityIndex < chunkSize; ++entityIndex)
            {
                ref readonly var entity = ref Unsafe.Subtract(ref entityLastElement, entityIndex);
                forEntity(entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var t = new T();

        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                t.Update(in entity);
            }
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query<T>(in QueryDescription queryDescription, ref T iForEach) where T : struct, IForEach
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                iForEach.Update(in entity);
            }
        }
    }
}

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

        ParallelChunkQuery(in queryDescription, foreachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        ParallelChunkQuery(in queryDescription, iForEachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription, in IForEachJob<T> iForEach) where T : struct, IForEach
    {
        ParallelChunkQuery(in queryDescription, in iForEach);
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>'s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them.
    /// </summary>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (JobScheduler.JobScheduler.Instance is null)
        {
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        }

        // Cast pool in an unsafe fast way and run the query.
        var pool = JobMeta<ChunkIterationJob<T>>.Pool;
        var query = Query(in queryDescription);
        foreach (ref var archetype in query.GetArchetypeIterator())
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

public partial class World : IDisposable
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
}

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
    public short Version(in Entity entity)
    {
        return EntityInfo[entity.Id].Version;
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

public partial class World
{
    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // Create a stack array with all component we now search an archetype for.
        Span<int> ids = stackalloc int[oldArchetype.Types.Length + 1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = Component<T>.ComponentType.Id;

        if (!TryGetArchetype(ids, out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(in Entity entity, IList<ComponentType> components)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // Create a stack array with all component we now search an archetype for.
        Span<int> ids = stackalloc int[oldArchetype.Types.Length + components.Count];
        oldArchetype.Types.WriteComponentIds(ids);

        // Add ids from array to all ids
        for (var index = 0; index < components.Count; index++)
        {
            var type = components[index];
            ids[oldArchetype.Types.Length + index] = type.Id;
        }

        if (!TryGetArchetype(ids, out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(components));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="cmp">The component instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity, in T cmp)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // Create a stack array with all component we now search an archetype for.
        Span<int> ids = stackalloc int[oldArchetype.Types.Length + 1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = Component<T>.ComponentType.Id;

        if (!TryGetArchetype(ids, out var newArchetype))
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in Entity entity)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // Create a stack array with all component we now search an archetype for.
        Span<int> ids = stackalloc int[oldArchetype.Types.Length];
        oldArchetype.Types.WriteComponentIds(ids);
        ids.Remove(Component<T>.ComponentType.Id);
        ids = ids[..^1];

        if (!TryGetArchetype(ids, out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(typeof(T)));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(in Entity entity, IList<ComponentType> types)
    {
        var oldArchetype = EntityInfo[entity.Id].Archetype;

        // Create a stack array with all component we now search an archetype for.
        Span<int> ids = stackalloc int[oldArchetype.Types.Length];
        oldArchetype.Types.WriteComponentIds(ids);

        foreach (var type in types)
        {
            ids.Remove(type.Id);
        }

        ids = ids[..^types.Count];

        if (!TryGetArchetype(ids, out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(types));
        }

        Move(in entity, oldArchetype, newArchetype, out _);
    }
}
