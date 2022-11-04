using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

[assembly: InternalsVisibleTo("Arch.Test")]
[assembly: InternalsVisibleTo("Arch.Benchmark")]
namespace Arch.Core;

/// <summary>
/// Represents an entity in our world. 
/// </summary>
public readonly struct Entity : IEquatable<Entity> {
        
    // The id of this entity in the world, not in the archetype
    public readonly int EntityId; 
    public readonly byte WorldId;
    public readonly ushort Version;

    public static Entity Null => new (-1, 0, 0);
    
    internal Entity(int entityId, byte worldId, ushort version) {
        EntityId = entityId;
        WorldId = worldId;
        Version = version;
    }

    public bool Equals(Entity other) {
        return EntityId == other.EntityId && WorldId == other.WorldId && Version == other.Version;
    }

    public override bool Equals(object obj) {
        return obj is Entity other && Equals(other);
    }

    public override int GetHashCode() {
        
        unchecked{ // Overflow is fine, just wrap
            int hash = 17;
            hash = hash * 23 + EntityId;
            hash = hash * 23 + WorldId;
            hash = hash * 23 + Version;
            return hash;
        }
    }

    public static bool operator ==(Entity left, Entity right) { return left.Equals(right); }
    public static bool operator !=(Entity left, Entity right) { return !left.Equals(right); }

    public override string ToString() { return $"{nameof(EntityId)}: {EntityId}, {nameof(WorldId)}: {WorldId}, {nameof(Version)}: {Version}"; }
}

/// <summary>
/// A world contains multiple <see cref="Archetypes"/>, <see cref="Entity"/>'s and their components.
/// It is used to manage the <see cref="Entity"/>'s and query for them. 
/// </summary>
public partial class World {

    internal World(byte Id) {

        this.Id = Id;
        
        GroupToArchetype = new PooledDictionary<Type[], Archetype>(8);
        EntityToArchetype = new PooledDictionary<int, Archetype>(0);
        Archetypes = new PooledList<Archetype>(8);
        
        RecycledIds = new PooledQueue<int>(256);
        QueryCache = new PooledDictionary<QueryDescription, Query>(8);
    }

    /// <summary>
    /// Creates a <see cref="World"/> and adds it to the <see cref="Worlds"/> list. 
    /// </summary>
    /// <returns>The newly created <see cref="World"/></returns>
    public static World Create() {

        var worldSize = Worlds.Count;
        if (worldSize >= byte.MaxValue)
            throw new Exception("Can not create world, there can only be 255 existing worlds.");
            
        var world = new World((byte)worldSize);
        Worlds.Add(world);

        return world;
    }

    /// <summary>
    /// Destroys an existing <see cref="World"/> and releases its entities and instances. 
    /// </summary>
    /// <param name="world">The world to destroy</param>
    public static void Destroy(World world) {

        Worlds.Remove(world);
        world.Capacity = 0;
        world.Size = 0;
        world.RecycledIds.Clear();
        world.Archetypes.Clear();
        world.EntityToArchetype.Clear();
        world.GroupToArchetype.Clear();
    }

    /// <summary>
    /// Returns the fitting archetype for a passed <see cref="QueryDescription"/>.
    /// </summary>
    /// <param name="types">The archetype structure</param>
    /// <param name="archetype">The archetype with those entities</param>
    /// <returns>True if such an <see cref="Archetype"/> exists</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetArchetype(Type[] types, out Archetype archetype) {
        return GroupToArchetype.TryGetValue(types, out archetype);
    }
    
    /// <summary>
    /// Either gets or creates a <see cref="Archetype"/> based on the passed <see cref="Group"/> and registers it in the <see cref="World"/>
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreate(Type[] types) {
        
        if (TryGetArchetype(types, out var archetype)) return archetype;
        
        archetype = new Archetype(types);
        GroupToArchetype[types] = archetype;
        Archetypes.Add(archetype);
        return archetype;
    }

    /// <summary>
    /// Reserves space for the passed amount of entities upon the already existing amount. It allocates space for additional entities. 
    /// </summary>
    /// <param name="types">The archetype, the entities components</param>
    /// <param name="amount">The amount of entities we wanna allocate in one go</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reserve(Type[] types, int amount) {

        var archetype = GetOrCreate(types);
        archetype.Reserve(amount);
        
        var requiredCapacity = Capacity + amount;
        EntityToArchetype.EnsureCapacity(requiredCapacity);
        Capacity = requiredCapacity;
    }
    
    /// <summary>
    /// Creates an <see cref="Entity"/> in this world.
    /// Will use the passed <see cref="Archetype"/> to initialize its components once. 
    /// </summary>
    /// <param name="group">The group of components this entity should have</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(Type[] types) {

        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var id = recycle ? recycledId : Size;
        
        // Create new entity and put it to the back of the array
        var entity = new Entity(id, Id,0);
        
        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        var createdChunk = archetype.Add(in entity);

        // Resize map & Array to fit all potential new entities
        if (createdChunk) {
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
    /// Destroys the passed entity.
    /// Uses a dense array technique and recycles the id properly. 
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in Entity entity) {

        // Remove from archetype
        var archetype = entity.GetArchetype();
        var destroyedChunk = archetype.Remove(in entity);

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(entity.EntityId);
        EntityToArchetype.Remove(entity.EntityId);
        
        // Resizing and releasing memory 
        if (destroyedChunk) {
            var requiredCapacity = Capacity - archetype.EntitiesPerChunk;
            EntityToArchetype.TrimExcess(requiredCapacity);
            Capacity = requiredCapacity;
        }

        Size--;
    }

    /// <summary>
    /// Queries for the passed <see cref="QueryDescription"/> and calls the passed action on all found entities. 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="forEntity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Query(in QueryDescription queryDescription, Action<Entity> forEntity) {

        // Looping over all archetypes, their chunks and their entities. 
        if (!QueryCache.TryGetValue(queryDescription, out var query)) {
            query = new Query(queryDescription);
            QueryCache[queryDescription] = query;
        }

        // Iterate over all archetypes
        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {

            var archetype = Archetypes[index];
            var archetypeSize = archetype.Size;
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;

            ref var chunkFirstElement = ref archetype.Chunks[0];
            for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {

                ref var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
                var chunkSize = chunk.Size;
                
                ref var entityFirstElement = ref chunk.Entities[0];
                for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {

                    ref var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                    forEntity(entity);
                }
            }
        }
    }
    
    /// <summary>
    /// Queries for the passed <see cref="QueryDescription"/> and copies all fitting entities into a list. 
    /// </summary>
    /// <param name="queryDescription"></param>
    /// <param name="forEntity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetEntities(in QueryDescription queryDescription, IList<Entity> list) {

        // Looping over all archetypes, their chunks and their entities. 
        if (!QueryCache.TryGetValue(queryDescription, out var query)) {
            query = new Query(queryDescription);
            QueryCache[queryDescription] = query;
        }

        // Iterate over all archetypes
        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {

            var archetype = Archetypes[index];
            var archetypeSize = archetype.Size;
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;

            ref var chunkFirstElement = ref archetype.Chunks[0];
            for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {

                ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
                var chunkSize = chunk.Size;
                
                ref var entityFirstElement = ref chunk.Entities[0];
                for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {

                    ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                    list.Add(entity);
                }
            }
        }
    }
    
    /// <summary>
    /// Queries for the passed <see cref="QueryDescription"/> and fills in the passed list with all valid <see cref="Archetype"/>'s.
    /// </summary>
    /// <param name="queryDescription"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetArchetypes(in QueryDescription queryDescription, IList<Archetype> archetypes) {

        // Cache query
        if (!QueryCache.TryGetValue(queryDescription, out var query)) {
            query = new Query(queryDescription);
            QueryCache[queryDescription] = query;
        }
        
        // Looping over all archetypes, their chunks and their entities. 
        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {

            var archetype = Archetypes[index];
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;
            archetypes.Add(archetype);
        }
    }
    
    /// <summary>
    /// Queries for the passed <see cref="QueryDescription"/> and fills in the passed list with all valid <see cref="Chunk"/>'s.
    /// </summary>
    /// <param name="queryDescription"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetChunks(in QueryDescription queryDescription, IList<Chunk> chunks) {

        // Cache query
        if (!QueryCache.TryGetValue(queryDescription, out var query)) {
            query = new Query(queryDescription);
            QueryCache[queryDescription] = query;
        }
        
        // Looping over all archetypes, their chunks and their entities. 
        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {

            var archetype = Archetypes[index];
            var archetypeSize = archetype.Size;
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;

            ref var chunkFirstElement = ref archetype.Chunks[0];
            for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {

                ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
                chunks.Add(chunk);
            }
        }
    }

    
    /// <summary>
    /// All active <see cref="World"/>'s.
    /// </summary>
    public static List<World> Worlds { get; } = new(1);

    /// <summary>
    /// The world id
    /// </summary>
    public byte Id { get; }

    /// <summary>
    /// The size of the world, the amount of <see cref="Entities"/>.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// The total capacity for entities and their components. 
    /// </summary>
    public int Capacity { get; private set; }
    
    /// <summary>
    /// All registered <see cref="Archetypes"/> in this world.
    /// Should not be modified.
    /// </summary>
    public PooledList<Archetype> Archetypes { get; }
    
    /// <summary>
    /// A map which assigns a archetype to each group for fast acess. 
    /// </summary>
    internal PooledDictionary<Type[], Archetype> GroupToArchetype { get; set; }
    
    /// <summary>
    /// A map which maps each entity to its archetype for fast acess of its components
    /// </summary>
    internal PooledDictionary<int, Archetype> EntityToArchetype { get; set; }

    /// <summary>
    /// Recycled entity ids. 
    /// </summary>
    internal PooledQueue<int> RecycledIds { get; set; }
    
    /// <summary>
    /// A cache for mapping a <see cref="QueryDescription"/> to its <see cref="Query"/> instance for preventing new <see cref="BitSet"/> allocations every query. 
    /// </summary>
    internal PooledDictionary<QueryDescription, Query> QueryCache { get; set; }
}