using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;
using Component = Arch.Core.Utils.Component;

[assembly: InternalsVisibleTo("Arch.Test")]
[assembly: InternalsVisibleTo("Arch.Benchmark")]

namespace Arch.Core;

/// <summary>
///     Represents an entity in our world.
/// </summary>
[StructLayout(Layout.Sequential)]
public readonly struct Entity : IEquatable<Entity>
{
    // The id of this entity in the world, not in the archetype
    public readonly int EntityId;
    public readonly byte WorldId;
    private readonly byte _pad;
    public readonly ushort Version;

    public static readonly Entity Null = new(-1, 0, 0);

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

        JobHandles = new PooledList<JobHandle>(Environment.ProcessorCount);
        JobsCache = new List<IJob>(Environment.ProcessorCount);
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
    ///     Reserves space for the passed amount of entities upon the already existing amount. It allocates space for additional entities.
    /// </summary>
    /// <param name="types">The archetype, the entities components</param>
    /// <param name="amount">The amount of entities we wanna allocate in one go</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(ComponentType[] types, int amount)
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
    public Entity Create(params ComponentType[] types)
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
            Capacity += archetype.EntitiesPerChunk;
            EntityToArchetype.EnsureCapacity(Capacity);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
/// A partial addition to the world which adds <see cref="Archetypes"/> and methods handling them. 
/// </summary>
public partial class World
{
    
    /// <summary>
    ///     A map which assigns a archetype to each group for fast acess.
    /// </summary>
    internal PooledDictionary<int, Archetype> GroupToArchetype { get; set; }
    
    /// <summary>
    ///     A map which maps each entity to its archetype for fast acess of its components
    /// </summary>
    internal PooledDictionary<int, Archetype> EntityToArchetype { get; set; }

    /// <summary>
    ///     Returns a <see cref="Archetype"/> by its structure represented as a type array.
    /// </summary>
    /// <param name="types">The archetype structure</param>
    /// <param name="archetype">The archetype with those entities</param>
    /// <returns>True if such an <see cref="Archetype" /> exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(ComponentType[] types, out Archetype archetype)
    {
        var hash = Component.GetHashCode(types); 
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
        var hash = Component.GetHashCode(ids); 
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Either gets or creates a <see cref="Archetype" /> based on the passed <see cref="Group" /> and registers it in the <see cref="World" />
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Archetype GetOrCreate(ComponentType[] types)
    {
        if (TryGetArchetype(types, out var archetype)) return archetype;

        // Create archetype
        archetype = new Archetype(types);
        var hash = Component.GetHashCode(types);
        
        GroupToArchetype[hash] = archetype;
        Archetypes.Add(archetype);

        // Archetypes always allocate one single chunk upon construction
        Capacity += archetype.EntitiesPerChunk; 
        EntityToArchetype.EnsureCapacity(Capacity);
        
        return archetype;
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
    ///     Used handles by this world.
    /// </summary>
    private PooledList<JobHandle> JobHandles { get; }
    
    /// <summary>
    ///     List for preventing allocs for jobs. 
    /// </summary>
    internal List<IJob> JobsCache { get; set; }
    
    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the passed action on all found entities.
    ///     Runs multithreaded. 
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="forEntity">A call back which passes the entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery(in QueryDescription queryDescription, ForEach forEntity)
    {
        var foreachJob = new ForEachJob();
        foreachJob.ForEach = forEntity;
        ParallelChunkQuery(in queryDescription, foreachJob);
    }
    
        /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the <see cref="IForEach.Update" /> method implemented in the struct <see cref="T" />.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        ParallelChunkQuery(in queryDescription, iForEachJob);
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the <see cref="IForEach.Update" /> method implemented in the struct <see cref="T" />.
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="iForEach">The struct implementing <see cref="IForEach" /> to define the logic for each entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery<T>(in QueryDescription queryDescription, in IForEachJob<T> iForEach) where T : struct, IForEach
    {
        ParallelChunkQuery(in queryDescription, in iForEach);
    }

    /// <summary>
    ///     Queries for the passed <see cref="QueryDescription" /> and calls the passed action on all found entities.
    ///     Runs multithreaded. 
    /// </summary>
    /// <param name="queryDescription">The description</param>
    /// <param name="forEntity">A call back which passes the entity</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
 
        // Job scheduler needs to be initialized
        if (JobScheduler.JobScheduler.Instance == null)
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        
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

            // Schedule, flush, wait, return
            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
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

/// <summary>
/// Partial addition to the world which adds methods methods for setting and getting components. 
/// </summary>
public partial class World
{
    
    /// <summary>
    /// Sets a component for the passed <see cref="Entity"/>.
    /// This replaces the previous values. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component instance.</param>
    /// <typeparam name="T">The generic/component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T cmp = default)
    {
        var archetype = EntityToArchetype[entity.EntityId];
        archetype.Set(in entity, in cmp);
    }
    
    /// <summary>
    ///     Returns true if the <see cref="Entity" /> has a certain component assigned.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>True if it exists for that entity</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(in Entity entity)
    {  
        var archetype = EntityToArchetype[entity.EntityId];
        return archetype.Has<T>();
    }
    
    /// <summary>
    /// Returns a component reference from the <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <typeparam name="T">The generic/component type.</typeparam>
    /// <returns>A reference to the entity component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(in Entity entity)
    {
        var archetype = EntityToArchetype[entity.EntityId];
        return ref archetype.Get<T>(in entity);
    }
    
    /// <summary>
    ///     Returns the component if it exists for that entity.
    ///     In case of a struct it will only returns a copy.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <param name="component">The component itself</param>
    /// <returns>True if the component exists on the entity and could be returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet<T>(in Entity entity, out T component)
    {
        component = default;
        if (!Has<T>(in entity)) return false;

        var archetype = EntityToArchetype[entity.EntityId];
        component = archetype.Get<T>(entity);
        return true;
    }
}

/// <summary>
/// Adds utility methods for entities. 
/// </summary>
public partial class World
{
    
    /// <summary>
    ///     Returns true if the passed entity is alive.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if the entity is alive in its world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsAlive(in Entity entity)
    {
        var world = Worlds[entity.WorldId];
        return world.EntityToArchetype.ContainsKey(entity.EntityId);
    }
    
    /// <summary>
    ///     Returns the <see cref="Archetype" /> in which the <see cref="Entity" /> and its components are stored in.
    /// </summary>
    /// <param name="entity">The entity we wanna receive the <see cref="Archetype" /> from. </param>
    /// <returns>The <see cref="Archetype" /> in which the entity and all its components are stored. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype GetArchetype(in Entity entity)
    {
        var world = Worlds[entity.WorldId];
        return world.EntityToArchetype[entity.EntityId];
    }

    /// <summary>
    ///     Returns the <see cref="Chunk" /> in which the <see cref="Entity" /> is located in.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>A reference to its chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref readonly Chunk GetChunk(in Entity entity)
    {
        var archetype = GetArchetype(in entity);
        return ref archetype.GetChunk(in entity);
    }
    
    /// <summary>
    ///     Returns the component types which the passed <see cref="Entity" /> has assigned.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>An array of components types.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentType[] GetComponentTypes(in Entity entity)
    {
        var archetype = EntityToArchetype[entity.EntityId];
        return archetype.Types;
    }

    /// <summary>
    ///     Returns the components which the passed <see cref="Entity" /> has assigned.
    ///     In case of struct components they will be boxed which causes memory allocations.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>An array of components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public object[] GetAllComponents(in Entity entity)
    {
        // Get archetype and chunk
        var archetype = EntityToArchetype[entity.EntityId];
        var chunkIndex = archetype.EntityIdToChunkIndex[entity.EntityId];
        var chunk = archetype.Chunks[chunkIndex];
        var components = chunk.Components;

        // Loop over components, collect and returns them
        var entityIndex = chunk.EntityIdToIndex[entity.EntityId];
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

/// <summary>
/// Adds methods for structural changes of entities and archetypes. 
/// </summary>
public partial class World{

    /// <summary>
    /// Adds an Component <see cref="T"/> to the entity and moves it to the new archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    /// <typeparam name="T">The Component.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity)
    {
        var oldArchetype = EntityToArchetype[entity.EntityId];

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length+1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = Component<T>.ComponentType.Id;
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Add(typeof(T)));

        Move(in entity, oldArchetype, newArchetype);
    }

    /// <summary>
    /// Adds an list of components to the entity. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(in Entity entity, IList<ComponentType> components)
    {
        var oldArchetype = EntityToArchetype[entity.EntityId];

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length+components.Count];
        oldArchetype.Types.WriteComponentIds(ids);

        // Add ids from array to all ids 
        for (var index = 0; index < components.Count; index++)
        {
            var type = components[index];
            ids[oldArchetype.Types.Length + index] = type.Id;
        }
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Add(components));

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
        var oldArchetype = EntityToArchetype[entity.EntityId];

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length+1];
        oldArchetype.Types.WriteComponentIds(ids);
        ids[^1] = Component<T>.ComponentType.Id;
        
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
        var oldArchetype = EntityToArchetype[entity.EntityId];

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length];
        oldArchetype.Types.WriteComponentIds(ids);
        ids.Remove(Component<T>.ComponentType.Id);
        ids = ids[..^1];
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(typeof(T)));

        Move(in entity, oldArchetype, newArchetype);
    }
    
    /// <summary>
    /// Adds an Component <see cref="T"/> to the entity and moves it to the new archetype. 
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cmp">The component value.</param>
    /// <typeparam name="T">The Component.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(in Entity entity, IList<ComponentType> types)
    { 
        var oldArchetype = EntityToArchetype[entity.EntityId];

        // Create a stack array with all component we now search an archetype for. 
        Span<int> ids = stackalloc int[oldArchetype.Types.Length];
        oldArchetype.Types.WriteComponentIds(ids);

        foreach (var type in types)
            ids.Remove(type.Id);
        
        ids = ids[..^types.Count];
        
        if (!TryGetArchetype(ids, out var newArchetype))
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(types));

        Move(in entity, oldArchetype, newArchetype);
    }
}
