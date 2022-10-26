using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core;

/// <summary>
/// Represents an entity in our world. 
/// </summary>
public readonly struct Entity {
        
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
        
        unchecked{ // Overflow is fine, just wrap{
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + EntityId;
            hash = hash * 23 + WorldId;
            hash = hash * 23 + Version;
            return hash;
        }
    }

    public override string ToString() { return $"{nameof(EntityId)}: {EntityId}, {nameof(WorldId)}: {WorldId}, {nameof(Version)}: {Version}"; }
}

/// <summary>
/// A world contains multiple <see cref="Archetypes"/>, <see cref="Entity"/>'s and their components.
/// It is used to manage the <see cref="Entity"/>'s and query for them. 
/// </summary>
public partial class World {

    internal World(byte Id) {

        this.Id = Id;
        RecycledIds = new PooledQueue<int>(256);
        GroupToArchetype = new PooledDictionary<Type[], Archetype>(8);
        EntityToArchetype = new PooledDictionary<int, Archetype>(0);
        Archetypes = new PooledList<Archetype>(8);
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
    /// Either gets or creates a <see cref="Archetype"/> based on the passed <see cref="Group"/> and registers it in the <see cref="World"/>
    /// </summary>
    /// <param name="group"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreate(Type[] types) {
        
        var exists = GroupToArchetype.TryGetValue(types, out var archetype);
        if (exists) return archetype;
        
        archetype = new Archetype(types);
        GroupToArchetype[types] = archetype;
        Archetypes.Add(archetype);
        return archetype;
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
        var query = new Query(queryDescription);
        for (var index = 0; index < Archetypes.Count; index++) {

            var archetype = Archetypes[index];
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;

            var chunks = archetype.Chunks;
            for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

                ref var chunk = ref chunks[chunkIndex];
                var entities = chunk.Entities;
                
                for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++) {

                    ref var entity = ref entities[entityIndex];
                    forEntity(entity);
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

        // Looping over all archetypes, their chunks and their entities. 
        var query = new Query(queryDescription);
        for (var index = 0; index < Archetypes.Count; index++) {

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

        // Looping over all archetypes, their chunks and their entities. 
        var query = new Query(queryDescription);
        for (var index = 0; index < Archetypes.Count; index++) {

            var archetype = Archetypes[index];
            var bitset = archetype.BitSet;

            // Only process archetypes within the query decribtion
            if (!query.Valid(bitset)) continue;
            
            for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

                ref var chunk = ref archetype.Chunks[chunkIndex];
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
}