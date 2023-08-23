using System.Diagnostics.Contracts;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Collections.Pooled;
using JobScheduler;
using ArchArrayExtensions = Arch.Core.Extensions.Internal.ArrayExtensions;
using Component = Arch.Core.Utils.Component;

namespace Arch.Core;

/// <summary>
///     The <see cref="RecycledEntity"/> struct
///     stores information about an recycled <see cref="Entity"/>, its id and its version.
/// </summary>
[SkipLocalsInit]
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
        EntityInfo = new EntityInfoStorage();
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
    public static World[] Worlds {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; private set; } = new World[4];

    /// <summary>
    ///     Stores recycled <see cref="World"/> ids.
    /// </summary>
    internal static PooledQueue<int> RecycledWorldIds {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; } = new(8);

    /// <summary>
    ///     Tracks how many <see cref="Worlds"/> exists.
    /// </summary>
    internal static int WorldSize = 0;

    /// <summary>
    ///     The unique <see cref="World"/> id.
    /// </summary>
    public int Id {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    ///     The amount of <see cref="Entity"/>'s stored by this instance.
    /// </summary>
    public int Size { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; internal set; }

    /// <summary>
    ///     The available capacity for <see cref="Entity"/>'s by this instance.
    /// </summary>
    public int Capacity {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; internal set; }

    /// <summary>
    ///     All <see cref="Archetype"/>'s that exist in this <see cref="World"/>.
    /// </summary>
    public PooledList<Archetype> Archetypes {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    ///     Mapt an <see cref="Entity"/> to its <see cref="EntityInfo"/> for quick lookups.
    /// </summary>
    internal EntityInfoStorage EntityInfo { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; }

    /// <summary>
    ///     Stores recycled <see cref="Entity"/> ids and their last version.
    /// </summary>
    internal PooledQueue<RecycledEntity> RecycledIds {  [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

    /// <summary>
    ///     A cache to map <see cref="QueryDescription"/> to their <see cref="Arch.Core.Query"/> to avoid allocs.
    /// </summary>
    internal PooledDictionary<QueryDescription, Query> QueryCache { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

    /// <summary>
    ///     Creates a <see cref="World"/> instance.
    /// </summary>
    /// <returns>The created <see cref="World"/> instance.</returns>
    public static World Create()
    {
#if PURE_ECS
        return new World(-1);
#else
        var recycle = RecycledWorldIds.TryDequeue(out var id);
        var recycledId = recycle ? id : WorldSize;

        var world = new World(recycledId);

        // If you need to ensure a higher capacity, you can manually check and increase it
        if (recycledId >= Worlds.Length)
        {
            var newCapacity = Worlds.Length * 2;
            var worlds = Worlds;
            Array.Resize(ref worlds, newCapacity);
            Worlds = worlds;
        }

        Worlds[recycledId] = world;
        WorldSize++;
        return world;
#endif
    }

    /// <summary>
    ///     Destroys an existing <see cref="World"/>.
    /// </summary>
    /// <param name="world">The <see cref="World"/>.</param>
    public static void Destroy(World world)
    {

#if !PURE_ECS
        Worlds[world.Id] = null;
        RecycledWorldIds.Enqueue(world.Id);
        WorldSize--;
#endif

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
            world.Archetypes[index] = null!;
        }

        world.Archetypes.Dispose();
    }

    /// <summary>
    ///     Reserves space for a certain number of <see cref="Entity"/>'s of a given component structure/<see cref="Archetype"/>.
    /// </summary>
    /// <param name="types">The component structure/<see cref="Archetype"/>.</param>
    /// <param name="amount">The amount.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(Span<ComponentType> types, int amount)
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
        return Create(types.AsSpan());
    }

    /// <summary>
    ///     Creates a new <see cref="Entity"/> using its given component structure/<see cref="Archetype"/>.
    ///     Might resize its target <see cref="Archetype"/> and allocate new space if its full.
    /// </summary>
    /// <param name="types">Its component structure/<see cref="Archetype"/>.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(Span<ComponentType> types)
    {
        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var recycled = recycle ? recycledId : new RecycledEntity(Size, 1);

        // Create new entity and put it to the back of the array
        var entity = new Entity(recycled.Id, Id);

        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        var createdChunk = archetype.Add(entity, out var slot);

        // Resize map & Array to fit all potential new entities
        if (createdChunk)
        {
            Capacity += archetype.EntitiesPerChunk;
            EntityInfo.EnsureCapacity(Capacity);
        }

        // Map
        EntityInfo.Add(entity.Id, recycled.Version, archetype, slot);
        Size++;
        OnEntityCreated(entity);
        return entity;
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> from one <see cref="Archetype"/> <see cref="Slot"/> to another.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="source">Its <see cref="Archetype"/>.</param>
    /// <param name="destination">The new <see cref="Archetype"/>.</param>
    /// <param name="destinationSlot">The new <see cref="Slot"/> where moved <see cref="Entity"/> landed in.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Move(Entity entity, Archetype source, Archetype destination, out Slot destinationSlot)
    {
        // A common mistake, happening in many cases.
        Debug.Assert(source != destination, "From-Archetype is the same as the To-Archetype. Entities cannot move within the same archetype using this function. Probably an attempt was made to attach already existing components to the entity or to remove non-existing ones.");

        // Copy entity to other archetype
        ref var slot = ref EntityInfo.GetSlot(entity.Id);
        var created = destination.Add(entity, out destinationSlot);
        Archetype.CopyComponents(source, ref slot, destination,ref destinationSlot);
        source.Remove(ref slot, out var movedEntity);

        // Update moved entity from the remove
        EntityInfo.Move(movedEntity, slot);
        EntityInfo.Move(entity.Id, destination, destinationSlot);

        // Calculate the entity difference between the moved archetypes to allocate more space accordingly.
        if (created)
        {
            Capacity += destination.EntitiesPerChunk;
            EntityInfo.EnsureCapacity(Capacity);
        }
    }

    /// <summary>
    ///     Destroys an <see cref="Entity"/>.
    ///     Might resize its target <see cref="Archetype"/> and release memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(Entity entity)
    {
        OnEntityDestroyed(entity);

        // Remove from archetype
        var entityInfo = EntityInfo[entity.Id];
        entityInfo.Archetype.Remove(ref entityInfo.Slot, out var movedEntityId);

        // Update info of moved entity which replaced the removed entity.
        EntityInfo.Move(movedEntityId, entityInfo.Slot);
        EntityInfo.Remove(entity.Id);

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(new RecycledEntity(entity.Id, unchecked(entityInfo.Version+1)));
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
            Archetypes[index] = null!;
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
    [Pure]
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
    ///     Search all matching <see cref="Entity"/>'s and put them into the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    /// <param name="list">The <see cref="Span{T}"/> receiving the found <see cref="Entity"/>'s.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, Span<Entity> list, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
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
    /// <param name="archetypes">The <see cref="Span{T}"/> receiving <see cref="Archetype"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetArchetypes(in QueryDescription queryDescription, Span<Archetype> archetypes, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            archetypes[start+index] = archetype;
            index++;
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Chunk"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="chunks">The <see cref="Span{T}"/> receiving <see cref="Chunk"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    /// <param name="start">The start index inside the <see cref="Span{T}"/>. Default is 0.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetChunks(in QueryDescription queryDescription, Span<Chunk> chunks, int start = 0)
    {
        var index = 0;
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            chunks[start+index] = chunk;
            index++;
        }
    }

    /// <summary>
    ///     Creates and returns a new <see cref="Enumerator{T}"/> instance to iterate over all <see cref="Archetypes"/>.
    /// </summary>
    /// <returns>A new <see cref="Enumerator{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
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
    [Pure]
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
    internal PooledDictionary<int, Archetype> GroupToArchetype { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by the hash of its components.
    /// </summary>
    /// <param name="hash">Its hash..</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    internal bool TryGetArchetype(int hash, [MaybeNullWhen(false)] out Archetype archetype)
    {
        return GroupToArchetype.TryGetValue(hash, out archetype);
    }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by a <see cref="BitSet"/>.
    /// </summary>
    /// <param name="bitset">A <see cref="BitSet"/> indicating the <see cref="Archetype"/> structure.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool TryGetArchetype(BitSet bitset, [MaybeNullWhen(false)] out Archetype archetype)
    {
        return GroupToArchetype.TryGetValue(bitset.GetHashCode(), out archetype);
    }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by a <see cref="BitSet"/>.
    /// </summary>
    /// <param name="bitset">A <see cref="SpanBitSet"/> indicating the <see cref="Archetype"/> structure.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool TryGetArchetype(SpanBitSet bitset, [MaybeNullWhen(false)] out Archetype archetype)
    {
        return GroupToArchetype.TryGetValue(bitset.GetHashCode(), out archetype);
    }

    /// <summary>
    ///     Trys to find an <see cref="Archetype"/> by the hash of its components.
    /// </summary>
    /// <param name="types">Its <see cref="ComponentType"/>'s.</param>
    /// <param name="archetype">The found <see cref="Archetype"/>.</param>
    /// <returns>True if found, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool TryGetArchetype(Span<ComponentType> types, [MaybeNullWhen(false)] out Archetype archetype)
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
    internal Archetype GetOrCreate(Span<ComponentType> types)
    {
        if (TryGetArchetype(types, out var archetype))
        {
            return archetype;
        }

        // Create archetype
        archetype = new Archetype(types.ToArray());
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
            ref var entityLastElement = ref chunk.Entity(0);
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
            ref var entityFirstElement = ref chunk.Entity(0);
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
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                iForEach.Update(in entity);
            }
        }
    }
}


// Batch query operations

public partial class World
{


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
                ref var entityFirstElement = ref chunk.Entity(0);
                foreach (var index in chunk)
                {
                    ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, index);
                    OnEntityDestroyed(entity);

                    var version = EntityInfo.GetVersion(entity.Id);
                    var recycledEntity = new RecycledEntity(entity.Id, version);

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
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.LastSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in component);
            archetype.Clear();

            OnComponentAdded<T>(newArchetype);
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

            OnComponentRemoved<T>(archetype);

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.LastSlot;
            var newArchetypeLastSlot = newArchetype.LastSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();
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
    public void Set<T>(Entity entity, in T cmp = default)
    {
        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);
        archetype.Set(ref slot, in cmp);
        OnComponentSet(entity, cmp);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool Has<T>(Entity entity)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Has<T>();
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T Get<T>(Entity entity)
    {
        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return ref archetype.Get<T>(ref slot);
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
    [Pure]
    public bool TryGet<T>(Entity entity, out T component)
    {
        component = default;

        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);

        if (!archetype.Has<T>())
        {
            return false;
        }

        component = archetype.Get<T>(ref slot);
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
    [Pure]
    public ref T TryGetRef<T>(Entity entity, out bool exists)
    {
        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);

        if (!(exists = archetype.Has<T>()))
        {
            return ref Unsafe.NullRef<T>();
        }

        return ref archetype.Get<T>(ref slot);
    }

    /// <summary>
    ///     Ensures the existence of an component on an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component value used if its being added.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T AddOrGet<T>(Entity entity, T cmp = default)
    {
        ref var component = ref TryGetRef<T>(entity, out var exists);
        if (exists)
        {
            return ref component;
        }

        Add(entity, cmp);
        return ref Get<T>(entity);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="newArchetype">The entity's new <see cref="Archetype"/>.</param>
    /// <param name="slot">The new <see cref="Slot"/> where the moved <see cref="Entity"/> landed in.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Add<T>(Entity entity, out Archetype newArchetype, out Slot slot)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);
        var type = Component<T>.ComponentType;
        newArchetype = GetOrCreateArchetypeByEdge(in type, oldArchetype);

        Move(entity, oldArchetype, newArchetype, out slot);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(Entity entity)
    {
        Add<T>(entity, out _, out _);
        OnComponentAdded<T>(entity);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="cmp">The component instance.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(Entity entity, in T cmp)
    {
        Add<T>(entity, out var newArchetype, out var slot);
        newArchetype.Set(ref slot, cmp);
        OnComponentAdded<T>(entity);
    }

    /// <summary>
    ///     Removes an component from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(Entity entity)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

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

        OnComponentRemoved<T>(entity);
        Move(entity, oldArchetype, newArchetype, out _);
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
    public void Set(Entity entity, object cmp)
    {
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        entitySlot.Archetype.Set(ref entitySlot.Slot, cmp);
        OnComponentSet(entity, cmp);
    }

    /// <summary>
    ///     Sets or replaces a <see cref="Span{T}"/> of components for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The components <see cref="Span{T}"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetRange(Entity entity, Span<object> components)
    {
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
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
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public object Get(Entity entity, ComponentType type)
    {
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        return entitySlot.Archetype.Get(ref entitySlot.Slot, type);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/> as a <see cref="Span{T}"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public object[] GetRange(Entity entity, Span<ComponentType> types)
    {
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        var array = new object[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            array[index] = entitySlot.Archetype.Get(ref entitySlot.Slot, type);
        }

        return array;
    }

    // ReSharper disable once PureAttributeOnVoidMethod
    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <param name="components">A <see cref="Span{T}"/> where the components are put it.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public void GetRange(Entity entity, Span<ComponentType> types, Span<object> components)
    {
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            components[index] = entitySlot.Archetype.Get(ref entitySlot.Slot, type);
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
    [Pure]
    public bool TryGet(Entity entity, ComponentType type, out object component)
    {
        component = default;
        if (!Has(entity, type))
        {
            return false;
        }

        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        component = entitySlot.Archetype.Get(ref entitySlot.Slot, type);
        return true;
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(Entity entity, in object cmp)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);
        var type = (ComponentType) cmp.GetType();
        var newArchetype = GetOrCreateArchetypeByEdge(in type, oldArchetype);

        Move(entity, oldArchetype, newArchetype, out var slot);
        newArchetype.Set(ref slot, cmp);
        OnComponentAdded(entity, type);
    }

    /// <summary>
    ///     Adds a <see cref="IList{T}"/> of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The component <see cref="Span{T}"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddRange(Entity entity, Span<object> components)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

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

            newArchetype = GetOrCreate(oldArchetype.Types.Add(newComponents));
        }

        // Move and fire events
        Move(entity, oldArchetype, newArchetype, out var slot);
        foreach (var cmp in components)
        {
            newArchetype.Set(ref slot, cmp);
            OnComponentAdded(entity, cmp.GetType());
        }
    }


    /// <summary>
    ///     Removes one single of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The <see cref="ComponentType"/> to remove from the the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(Entity entity, ComponentType type)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

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

        OnComponentRemoved(entity, type);
        Move(entity, oldArchetype, newArchetype, out _);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="Span{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveRange(Entity entity, Span<ComponentType> types)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

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
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(types.ToArray()));
        }

        // Fire events and move
        foreach (var type in types)
        {
            OnComponentRemoved(entity, type);
        }
        Move(entity, oldArchetype, newArchetype, out _);
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
    [Pure]
    public bool IsAlive(Entity entity)
    {
        return EntityInfo.Has(entity.Id);
    }

    /// <summary>
    ///     Returns the version of an <see cref="Entity"/>.
    ///     Indicating how often it was recycled.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its version.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public int Version(Entity entity)
    {
        return EntityInfo.GetVersion(entity.Id);
    }

    /// <summary>
    ///     Returns a <see cref="EntityReference"/> to an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="EntityReference"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public EntityReference Reference(Entity entity)
    {
        var entityInfo = EntityInfo.TryGetVersion(entity.Id, out var version);
        return entityInfo ? new EntityReference(in entity, version) : EntityReference.Null;
    }

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref readonly Chunk GetChunk(Entity entity)
    {
        var entityInfo = EntityInfo.GetEntitySlot(entity.Id);
        return ref entityInfo.Archetype.GetChunk(entityInfo.Slot.ChunkIndex);
    }

    /// <summary>
    ///     Returns all <see cref="ComponentType"/>'s of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>'s array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ComponentType[] GetComponentTypes(Entity entity)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Types;
    }

    /// <summary>
    ///     Returns all components of an <see cref="Entity"/> as an array.
    ///     Will allocate memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A newly allocated array containing the entities components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public object[] GetAllComponents(Entity entity)
    {
        // Get archetype and chunk.
        var entitySlot = EntityInfo.GetEntitySlot(entity.Id);
        var archetype = entitySlot.Archetype;
        ref var chunk = ref archetype.GetChunk(entitySlot.Slot.ChunkIndex);
        var components = chunk.Components;

        // Loop over components, collect and returns them.
        var entityIndex = entitySlot.Slot.Index;
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

