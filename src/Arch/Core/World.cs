using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using JobScheduler;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;
using Component = Arch.Core.Utils.Component;

namespace Arch.Core;

#if PURE_ECS
// TODO: Documentation.
/// <summary>
///     The <see cref="Entity"/> struct
///     ...
/// </summary>
public readonly struct Entity : IEquatable<Entity>
{
    // TODO: Documentation.
    // The ID of this entity in the world, not in the archetype.
    public readonly int Id;
    public static readonly Entity Null = new(-1, 0);

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct
    ///     ...
    /// </summary>
    /// <param name="id"></param>
    /// <param name="worldId"></param>
    internal Entity(int id, int worldId)
    {
        Id = id;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Entity other)
    {
        return Id == other.Id;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}";
    }
}
#else
// TODO: Documentation.
/// <summary>
///     The <see cref="Entity"/> struct
///     ...
/// </summary>
public readonly struct Entity : IEquatable<Entity>
{
    // TODO: Documentation.
    // The ID of this entity in the world, not in the archetype.
    public readonly int Id;
    public readonly int WorldId;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public static Entity Null { get; } = new(-1, 0);

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct
    ///     ...
    /// </summary>
    /// <param name="id"></param>
    /// <param name="worldId"></param>
    internal Entity(int id, int worldId)
    {
        Id = id;
        WorldId = worldId;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Entity other)
    {
        return Id == other.Id && WorldId == other.WorldId;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(WorldId)}: {WorldId}";
    }
}
#endif

// TODO: Documentation.
/// <summary>
///     The <see cref="EntityInfo"/> struct
///     ...
/// </summary>
internal struct EntityInfo
{
    // TODO: Documentation.
    public Slot Slot;           // Slot inside the archetype
    public Archetype Archetype; // Archetype Index in World 
    public short Version;
}

// TODO: Documentation.
/// <summary>
///     The <see cref="IForEach"/> interface
///     ...
/// </summary>
public interface IForEach
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(in Entity entity);
}

// TODO: Documentation.
/// <summary>
///     The <see cref="ForEach"/> delegate
///     ...
/// </summary>
/// <param name="entity"></param>
public delegate void ForEach(in Entity entity);

// TODO: Documentation.
/// <summary>
///     The <see cref="World"/> class
///     ...
/// </summary>
public partial class World
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="World"/> class
    ///     ...
    /// </summary>
    /// <param name="id"></param>
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

    // TODO: Documentation.
    public static List<World> Worlds { get; } = new(1);
    public int Id { get; }
    public int Size { get; private set; }
    public int Capacity { get; private set; }
    public int HighestEntityId { get; private set; }
    public PooledList<Archetype> Archetypes { get; }
    internal PooledDictionary<int, EntityInfo> EntityInfo { get; }
    internal PooledQueue<int> RecycledIds { get; set; }
    internal PooledDictionary<QueryDescription, Query> QueryCache { get; set; }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static World Create()
    {
        var worldSize = Worlds.Count;
        if (worldSize >= int.MaxValue)
        {
            // FIXME: `int.MaxValue` is not 255.
            throw new Exception("Can not create world, there can only be 255 existing worlds.");
        }

        var world = new World(worldSize);
        Worlds.Add(world);

        return world;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="world"></param>
    public static void Destroy(World world)
    {
        Worlds.Remove(world);
        world.Capacity = 0;
        world.Size = 0;
        world.RecycledIds.Clear();
        world.Archetypes.Clear();
        world.EntityInfo.Clear();
        world.GroupToArchetype.Clear();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="amount"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(ComponentType[] types, int amount)
    {
        var archetype = GetOrCreate(types);
        archetype.Reserve(amount);

        var requiredCapacity = Capacity + amount;
        EntityInfo.EnsureCapacity(requiredCapacity);
        Capacity = requiredCapacity;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
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
        EntityInfo.Add(id, new EntityInfo { Version = 0, Archetype = archetype, Slot = slot });

        Size++;
        return entity;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="newSlot"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="list"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetArchetypes(in QueryDescription queryDescription, IList<Archetype> archetypes)
    {
        var query = Query(in queryDescription);
        foreach (ref var archetype in query.GetArchetypeIterator())
        {
            archetypes.Add(archetype);
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="chunks"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetChunks(in QueryDescription queryDescription, IList<Chunk> chunks)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            chunks.Add(chunk);
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Enumerator<Archetype> GetEnumerator()
    {
        return new Enumerator<Archetype>(Archetypes.Span);
    }
}

public partial class World
{
    // TODO: Documentation.
    internal PooledDictionary<int, Archetype> GroupToArchetype { get; set; }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="archetype"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(ComponentType[] types, out Archetype archetype)
    {
        var hash = Component.GetHashCode(types);
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="archetype"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Span<int> ids, out Archetype archetype)
    {
        var hash = Component.GetHashCode(ids);
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
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
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="forEntity"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryDescription"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryDescription"></param>
    /// <param name="iForEach"></param>
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
    // TODO: Documentation.
    private PooledList<JobHandle> JobHandles { get; }
    internal List<IJob> JobsCache { get; set; }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="forEntity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery(in QueryDescription queryDescription, ForEach forEntity)
    {
        var foreachJob = new ForEachJob
        {
            ForEach = forEntity
        };

        ParallelChunkQuery(in queryDescription, foreachJob);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryDescription"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        ParallelChunkQuery(in queryDescription, iForEachJob);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryDescription"></param>
    /// <param name="iForEach"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription, in IForEachJob<T> iForEach) where T : struct, IForEach
    {
        ParallelChunkQuery(in queryDescription, in iForEach);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryDescription"></param>
    /// <param name="innerJob"></param>
    /// <exception cref="Exception"></exception>
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

public partial class World
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="cmp"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T cmp = default)
    {
        var entityInfo = EntityInfo[entity.Id];
        entityInfo.Archetype.Set(ref entityInfo.Slot, in cmp);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(in Entity entity)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
        return archetype.Has<T>();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(in Entity entity)
    {
        var entityInfo = EntityInfo[entity.Id];
        return ref entityInfo.Archetype.Get<T>(ref entityInfo.Slot);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="component"></param>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="exists"></param>
    /// <returns></returns>
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
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive(in Entity entity)
    {
        return entity.Id < EntityInfo.Count && EntityInfo[entity.Id].Archetype is not null;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public short Version(in Entity entity)
    {
        return EntityInfo[entity.Id].Version;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype GetArchetype(in Entity entity)
    {
        return EntityInfo[entity.Id].Archetype;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref readonly Chunk GetChunk(in Entity entity)
    {
        var entityInfo = EntityInfo[entity.Id];
        return ref entityInfo.Archetype.GetChunk(entityInfo.Slot.ChunkIndex);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentType[] GetComponentTypes(in Entity entity)
    {
        var archetype = EntityInfo[entity.Id].Archetype;
        return archetype.Types;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
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
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="components"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="cmp"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="types"></param>
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
