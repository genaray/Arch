using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;

[assembly: InternalsVisibleTo("Arch.Test")]
[assembly: InternalsVisibleTo("Arch.Benchmark")]

namespace Arch.Core;

/// <summary>
///     Represents an entity in our world.
/// </summary>
public readonly struct Entity : IEquatable<Entity>
{
    // The id of this entity in the world, not in the archetype
    public readonly int EntityId;
    public readonly byte WorldId;
    public readonly ushort Version;

    public static Entity Null => new(-1, 0, 0);

    internal Entity(int entityId, byte worldId, ushort version)
    {
        EntityId = entityId;
        WorldId = worldId;
        Version = version;
    }

    public bool Equals(Entity other)
    {
        return EntityId == other.EntityId && WorldId == other.WorldId && Version == other.Version;
    }

    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap
            var hash = 17;
            hash = hash * 23 + EntityId;
            hash = hash * 23 + WorldId;
            hash = hash * 23 + Version;
            return hash;
        }
    }

    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    public override string ToString()
    {
        return $"{nameof(EntityId)}: {EntityId}, {nameof(WorldId)}: {WorldId}, {nameof(Version)}: {Version}";
    }
}

/// <summary>
///     A interface which passes a <see cref="Entity" /> to execute logic on.
/// </summary>
public interface IForEach
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Update(in Entity entity);
}

/// <summary>
///     A delegate passing an <see cref="Entity" /> to execute logic on it.
/// </summary>
public delegate void ForEach(in Entity entity);

/// <summary>
///     A world contains multiple <see cref="Archetypes" />, <see cref="Entity" />'s and their components.
///     It is used to manage the <see cref="Entity" />'s and query for them.
/// </summary>
public partial class World
{
    internal World(byte id)
    {
        this.Id = id;

        GroupToArchetype = new PooledDictionary<int, Archetype>(8);
        EntityToArchetype = new PooledDictionary<int, Archetype>(0);

        Archetypes = new PooledList<Archetype>(8);
        RecycledIds = new PooledQueue<int>(256);

        QueryCache = new PooledDictionary<QueryDescription, Query>(8);

        JobHandles = new PooledList<JobHandle>(32);
        ParallelJobsListCache = new PooledDictionary<Type, object>(16);
        JobPools = new PooledDictionary<Type, object>(16);
    }

    /// <summary>
    ///     All active <see cref="World" />'s.
    /// </summary>
    public static List<World> Worlds { get; } = new(1);

    /// <summary>
    ///     The world id
    /// </summary>
    public byte Id { get; }

    /// <summary>
    ///     The size of the world, the amount of <see cref="Entities" />.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     The total capacity for entities and their components.
    /// </summary>
    public int Capacity { get; private set; }

    /// <summary>
    ///     All registered <see cref="Archetypes" /> in this world.
    ///     Should not be modified.
    /// </summary>
    public PooledList<Archetype> Archetypes { get; }

    /// <summary>
    ///     A map which assigns a archetype to each group for fast acess.
    /// </summary>
    internal PooledDictionary<int, Archetype> GroupToArchetype { get; set; }

    /// <summary>
    ///     A map which maps each entity to its archetype for fast acess of its components
    /// </summary>
    internal PooledDictionary<int, Archetype> EntityToArchetype { get; set; }

    /// <summary>
    ///     Recycled entity ids.
    /// </summary>
    internal PooledQueue<int> RecycledIds { get; set; }

    /// <summary>
    ///     A cache for mapping a <see cref="QueryDescription" /> to its <see cref="Query" /> instance for preventing new <see cref="BitSet" /> allocations every query.
    /// </summary>
    internal PooledDictionary<QueryDescription, Query> QueryCache { get; set; }

    /// <summary>
    ///     Creates a <see cref="World" /> and adds it to the <see cref="Worlds" /> list.
    /// </summary>
    /// <returns>The newly created <see cref="World" /></returns>
    public static World Create()
    {
        var worldSize = Worlds.Count;
        if (worldSize >= byte.MaxValue)
            throw new Exception("Can not create world, there can only be 255 existing worlds.");

        var world = new World((byte)worldSize);
        Worlds.Add(world);

        return world;
    }

    /// <summary>
    ///     Destroys an existing <see cref="World" /> and releases its entities and instances.
    /// </summary>
    /// <param name="world">The world to destroy</param>
    public static void Destroy(World world)
    {
        Worlds.Remove(world);
        world.Capacity = 0;
        world.Size = 0;
        world.RecycledIds.Clear();
        world.Archetypes.Clear();
        world.EntityToArchetype.Clear();
        world.GroupToArchetype.Clear();
    }

    /// <summary>
    ///     Returns a <see cref="Archetype"/> by its structure represented as a type array.
    /// </summary>
    /// <param name="types">The archetype structure</param>
    /// <param name="archetype">The archetype with those entities</param>
    /// <returns>True if such an <see cref="Archetype" /> exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Type[] types, out Archetype archetype)
    {
        var hash = ComponentMeta.GetHashCode(types); 
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }
    
    /// <summary>
    ///     Returns a <see cref="Archetype"/> by its structure represented as a array of component ids. 
    /// </summary>
    /// <param name="types">The archetype structure</param>
    /// <param name="archetype">The archetype with those entities</param>
    /// <returns>True if such an <see cref="Archetype" /> exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Span<int> ids, out Archetype archetype)
    {
        var hash = ComponentMeta.GetHashCode(ids); 
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Either gets or creates a <see cref="Archetype" /> based on the passed <see cref="Group" /> and registers it in the <see cref="World" />
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreate(Type[] types)
    {
        if (TryGetArchetype(types, out var archetype)) return archetype;

        archetype = new Archetype(types);
        
        var hash = ComponentMeta.GetHashCode(types);
        GroupToArchetype[hash] = archetype;
        Archetypes.Add(archetype);
        return archetype;
    }

    /// <summary>
    ///     Reserves space for the passed amount of entities upon the already existing amount. It allocates space for additional entities.
    /// </summary>
    /// <param name="types">The archetype, the entities components</param>
    /// <param name="amount">The amount of entities we wanna allocate in one go</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(Type[] types, int amount)
    {
        var archetype = GetOrCreate(types);
        archetype.Reserve(amount);

        var requiredCapacity = Capacity + amount;
        EntityToArchetype.EnsureCapacity(requiredCapacity);
        Capacity = requiredCapacity;
    }

    /// <summary>
    ///     Creates an <see cref="Entity" /> in this world.
    ///     Will use the passed <see cref="Archetype" /> to initialize its components once.
    /// </summary>
    /// <param name="types">The group of components this entity should have, its archetype</param>
    /// <returns>The created entity</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(params Type[] types)
    {
        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var id = recycle ? recycledId : Size;

        // Create new entity and put it to the back of the array
        var entity = new Entity(id, Id, 0);

        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        var createdChunk = archetype.Add(in entity);

        // Resize map & Array to fit all potential new entities
        if (createdChunk)
        {
            var requiredCapacity = Capacity + archetype.EntitiesPerChunk;
            EntityToArchetype.EnsureCapacity(requiredCapacity);
            Capacity = requiredCapacity;
        }

        // Map
        EntityToArchetype[id] = archetype;

        Size++;
        return entity;
    }

    /// <summary>
    /// Moves an <see cref="Entity"/> from one archetype to another.
    /// Copies the entity and its components if they also exist in the other archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="from">Its origin archetype.</param>
    /// <param name="to">The archetype it should move to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Move(in Entity entity, Archetype from, Archetype to)
    {
        
        from.Move(in entity, to, out var created, out var destroyed);
        EntityToArchetype[entity.EntityId] = to;
        
        var difference = 0;
        if (created) difference += to.EntitiesPerChunk;
        if (destroyed) difference -= from.EntitiesPerChunk;
        Capacity += difference;
        
        if (difference >= 0) EntityToArchetype.EnsureCapacity(Capacity);
        else EntityToArchetype.TrimExcess(Capacity);
    }
    
    /// <summary>
    ///     Destroys the passed entity.
    ///     Uses a dense array technique and recycles the id properly.
    /// </summary>
    /// <param name="entity">The entity to destroy</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in Entity entity)
    {
        // Remove from archetype
        var archetype = entity.GetArchetype();
        var destroyedChunk = archetype.Remove(in entity);

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(entity.EntityId);
        EntityToArchetype.Remove(entity.EntityId);

        // Resizing and releasing memory 
        if (destroyedChunk)
        {
            EntityToArchetype.TrimExcess();
            Capacity = EntityToArchetype.Count;
        }

        Size--;
    }

    /// <summary>
    ///     Constructs a <see cref="Query" /> from the passed <see cref="QueryDescription" />.
    ///     Is being cached.
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <returns></returns>
    public Query Query(in QueryDescription queryDescription)
    {
        // Looping over all archetypes, their chunks and their entities. 
        if (QueryCache.TryGetValue(queryDescription, out var query)) return query;

        query = new Query(Archetypes, queryDescription);
        QueryCache[queryDescription] = query;

        return query;
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and copies all fitting entities into a list.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="list">A list where all fitting entities will be added to</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, IList<Entity> list)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                list.Add(entity);
            }
        }
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and fills in the passed list with all valid <see cref="Archetype" />'s.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="archetypes">A list where all fitting archetypes will be added to</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetArchetypes(in QueryDescription queryDescription, IList<Archetype> archetypes)
    {
        var query = Query(in queryDescription);
        foreach (ref var archetype in query.GetArchetypeIterator())
            archetypes.Add(archetype);
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and fills in the passed list with all valid <see cref="Chunk" />'s.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="chunks">A list where all fitting chunks will be added to</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetChunks(in QueryDescription queryDescription, IList<Chunk> chunks)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
            chunks.Add(chunk);
    }

    /// <summary>
    ///     Returns an enumerator to iterate over all <see cref="Archetypes" />
    /// </summary>
    /// <returns></returns>
    public Enumerator<Archetype> GetEnumerator()
    {
        return new Enumerator<Archetype>(Archetypes.Span);
    }
}

/// <summary>
///     Partial world with single threaded query methods.
/// </summary>
public partial class World
{
    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the passed action on all found entities.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="forEntity">A call back which passes the entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query(in QueryDescription queryDescription, ForEach forEntity)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                forEntity(entity);
            }
        }
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the <see cref="IForEach.Update" /> method implemented in the struct <see cref="T" />.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var t = new T();

        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                t.Update(in entity);
            }
        }
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the <see cref="IForEach.Update" /> method implemented in the struct <see cref="T" />.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="iForEach">The struct implementing <see cref="IForEach" /> to define the logic for each entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query<T>(in QueryDescription queryDescription, ref T iForEach) where T : struct, IForEach
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query.GetChunkIterator())
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);

            for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                iForEach.Update(in entity);
            }
        }
    }
}

/// <summary>
///     Partial world with multi threaded query methods.
/// </summary>
public partial class World
{
    /// <summary>
    ///     JobPool for different job types used internally by this multithreaded world.
    /// </summary>
    private PooledDictionary<Type, object> JobPools { get; }

    /// <summary>
    ///     Used handles by this world.
    /// </summary>
    private PooledList<JobHandle> JobHandles { get; }

    /// <summary>
    ///     JobList cache for avoiding allocs.
    /// </summary>
    private PooledDictionary<Type, object> ParallelJobsListCache { get; }

    /// <summary>
    /// Gets a job from the <see cref="JobPools"/> ( pooled ). 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetJob<T>() where T : class, new()
    {
        var type = typeof(T);

        if (!JobPools.TryGetValue(type, out var obj))
        {
            obj = new DefaultObjectPool<T>(new DefaultObjectPolicy<T>());
            JobPools[type] = obj;
        }

        var pool = obj as DefaultObjectPool<T>;
        return pool.Get();
    }

    /// <summary>
    /// Returns a job to the <see cref="JobPools"/>.
    /// </summary>
    /// <param name="instance"></param>
    /// <typeparam name="T"></typeparam>
    public void ReturnJob<T>(T instance) where T : class
    {
        var obj = JobPools[typeof(T)];
        var pool = obj as DefaultObjectPool<T>;
        pool.Return(instance);
    }

    /// <summary>
    /// Returns a list of a specific type for caching purposes, like an arraypool. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public List<T> GetListCache<T>() where T : class, new()
    {
        var type = typeof(T);
        if (!ParallelJobsListCache.TryGetValue(type, out var obj))
        {
            obj = new List<T>(64);
            ParallelJobsListCache[type] = obj;
        }

        return obj as List<T>;
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the passed action on all found entities.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="forEntity">A call back which passes the entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        if (JobScheduler.JobScheduler.Instance == null)
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");

        var listCache = GetListCache<ChunkIterationJob<T>>();

        var query = Query(in queryDescription);
        foreach (ref var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);

            foreach (var range in part)
            {
                var job = GetJob<ChunkIterationJob<T>>();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                listCache.Add(job);
            }

            IJob.Schedule(listCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < listCache.Count; jobIndex++)
            {
                var job = listCache[jobIndex];
                ReturnJob(job);
            }

            JobHandles.Clear();
            listCache.Clear();
        }
    }
}

public partial class World
{

    /// <summary>
    /// Adds an Component <see cref="T"/> to the entity and moves it to the new archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    /// <typeparam name="T">The Component.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity)
    {
        var oldArchetype = entity.GetArchetype();

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length+1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = ComponentMeta<T>.Id;
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));

        Move(in entity, oldArchetype, newArchetype);
    }
    
    /// <summary>
    /// Adds an Component <see cref="T"/> to the entity and moves it to the new archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    /// <typeparam name="T">The Component.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity, in T cmp)
    {
        var oldArchetype = entity.GetArchetype();

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length+1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = ComponentMeta<T>.Id;
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));

        Move(in entity, oldArchetype, newArchetype);
        newArchetype.Set(in entity, cmp);
    }
    
    /// <summary>
    /// Adds an Component <see cref="T"/> to the entity and moves it to the new archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    /// <typeparam name="T">The Component.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in Entity entity)
    {
        var oldArchetype = entity.GetArchetype();

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length];
        oldArchetype.Types.WriteComponentIds(ids);
        ids.Remove(ComponentMeta<T>.Id);
        ids = ids[..^1];
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(typeof(T)));

        Move(in entity, oldArchetype, newArchetype);
    }
}