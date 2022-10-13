using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Core;

/// <summary>
/// Represents an entity in our world. 
/// </summary>
public readonly struct Entity {
        
    // The id of this entity in the world, not in the archetype
    public readonly int EntityId; 
    public readonly int WorldId;
    public readonly int Version;

    public Entity(int entityId, int worldId, int version) {
        EntityId = entityId;
        WorldId = worldId;
        Version = version;
    }
    
    public static Entity Null => new (-1, -1, -1);
}

/// <summary>
/// A world contains multiple <see cref="Archetypes"/>, <see cref="Entity"/>'s and their components.
/// It is used to manage the <see cref="Entity"/>'s and query for them. 
/// </summary>
public class World {

    public World(int Id) {

        this.Id = Id;
        Entities = new Entity[256];
        RecycledIds = new Queue<int>(256);
        GroupToArchetype = new Dictionary<Type[], Archetype>(8);
        EntityToArchetype = new Dictionary<int, Archetype>(256);
        Archetypes = new List<Archetype>(8);
    }

    /// <summary>
    /// Creates a <see cref="World"/> and adds it to the <see cref="Worlds"/> list. 
    /// </summary>
    /// <returns></returns>
    public static World Create() {

        var worldSize = Worlds.Count;
        var world = new World(worldSize);
        Worlds.Add(world);

        return world;
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

        // Increase size
        if (Size >= Capacity) {

            var entities = Entities;
            Array.Resize(ref entities, Size * 2);
            Entities = entities;
        }

        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var id = recycle ? recycledId : Size;
        
        // Create new entity and put it to the back of the array
        var entity = new Entity(id,Id,0);
        Entities[Size] = entity;
        
        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        archetype.Add(in entity);
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
        archetype.Remove(in entity);

        // Swap last entity and destroyed one
        var lastIndex = Size - 1;
        var lastEntity = Entities[lastIndex];
        Entities[lastIndex] = entity;
        Entities[entity.EntityId] = lastEntity;

        // Recycle id && Remove mapping
        RecycledIds.Enqueue(entity.EntityId);
        EntityToArchetype.Remove(entity.EntityId);
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

            for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

                ref var chunk = ref archetype.Chunks[chunkIndex];
                for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++) {

                    ref var entity = ref chunk.Entities[entityIndex];
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
    public void GetArchetypes(in QueryDescription queryDescription, List<Archetype> archetypes = default) {

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
    public void GetChunks(in QueryDescription queryDescription, List<Chunk> chunks = default) {

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
    public int Id { get; }
    
    /// <summary>
    /// A array of all entities within this world. 
    /// </summary>
    public Entity[] Entities { get; private set; }
    
    /// <summary>
    /// The current world capacity in terms of entities.
    /// </summary>
    public int Capacity => Entities.Length;
    
    /// <summary>
    /// The size of the world, the amount of <see cref="Entities"/>.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// A map which assigns a archetype to each group for fast acess. 
    /// </summary>
    internal Dictionary<Type[], Archetype> GroupToArchetype { get; set; }
    
    /// <summary>
    /// A map which maps each entity to its archetype for fast acess of its components
    /// </summary>
    internal Dictionary<int, Archetype> EntityToArchetype { get; set; }
    
    /// <summary>
    /// All registered <see cref="Archetypes"/> in this world. 
    /// </summary>
    internal List<Archetype> Archetypes { get; }

    /// <summary>
    /// Recycled entity ids. 
    /// </summary>
    internal Queue<int> RecycledIds { get; set; }
}