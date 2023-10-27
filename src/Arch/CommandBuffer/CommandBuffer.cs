using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.CommandBuffer;

/// <summary>
///     The <see cref="CreateCommand"/> struct
///     contains data for creating a new <see cref="Entity"/>.
/// </summary>
public readonly record struct CreateCommand
{
    public readonly int Index;
    public readonly ComponentType[] Types;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateCommand"/> struct.
    /// </summary>
    /// <param name="index">The <see cref="Entity"/>'s buffer id.</param>
    /// <param name="types">Its <see cref="ComponentType"/>'s array.</param>
    public CreateCommand(int index, ComponentType[] types)
    {
        Index = index;
        Types = types;
    }
}

/// <summary>
///     The <see cref="BufferedEntityInfo"/> struct
///     contains data about a buffered <see cref="Entity"/>.
/// </summary>
/// <remarks>
///     This struct's purpose is to speed up lookups into an <see cref="Entity"/>'s internal data.
/// </remarks>
public readonly record struct BufferedEntityInfo
{
    public readonly int Index;
    public readonly int SetIndex;
    public readonly int AddIndex;
    public readonly int RemoveIndex;

    /// <summary>
    ///      Initializes a new instance of the <see cref="CreateCommand"/> struct.
    /// </summary>
    /// <param name="index">Its <see cref="CommandBuffer"/> index.</param>
    /// <param name="setIndex">Its <see cref="CommandBuffer.Sets"/> index.</param>
    /// <param name="addIndex">Its <see cref="CommandBuffer.Adds"/> index.</param>
    /// <param name="removeIndex">Its <see cref="CommandBuffer.Removes"/> index.</param>
    public BufferedEntityInfo(int index, int setIndex, int addIndex, int removeIndex)
    {
        Index = index;
        SetIndex = setIndex;
        AddIndex = addIndex;
        RemoveIndex = removeIndex;
    }
}

/// <summary>
///     The <see cref="CommandBuffer"/> class
///     stores operation to <see cref="Entity"/>'s between to play and implement them at a later time in the <see cref="World"/>.
/// </summary>
public class CommandBuffer : IDisposable
{
    private readonly PooledList<ComponentType> _addTypes;
    private readonly PooledList<ComponentType> _removeTypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBuffer"/> class
    ///     with the specified <see cref="Core.World"/> and an optional <paramref name="initialCapacity"/> (default: 128).
    /// </summary>
    /// <param name="world">The <see cref="World"/>.</param>
    /// <param name="initialCapacity">The initial capacity.</param>
    public CommandBuffer(World world, int initialCapacity = 128)
    {
        World = world;
        Entities = new PooledList<Entity>(initialCapacity);
        BufferedEntityInfo = new PooledDictionary<int, BufferedEntityInfo>(initialCapacity);
        Creates = new PooledList<CreateCommand>(initialCapacity);
        Sets = new SparseSet(initialCapacity);
        Adds = new StructuralSparseSet(initialCapacity);
        Removes = new StructuralSparseSet(initialCapacity);
        Destroys = new PooledList<int>(initialCapacity);
        _addTypes = new PooledList<ComponentType>(16);
        _removeTypes = new PooledList<ComponentType>(16);
    }

    /// <summary>
    ///     Gets the <see cref="Core.World"/>.
    /// </summary>
    public World World { get; }

    /// <summary>
    ///     Gets the amount of <see cref="Entity"/> instances targeted by this <see cref="CommandBuffer"/>.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     All <see cref="Entity"/>'s created or modified in this <see cref="CommandBuffer"/>.
    /// </summary>
    internal PooledList<Entity> Entities { get; set; }

    /// <summary>
    ///     A map that stores some additional information for each <see cref="Entity"/>, which is needed for the internal <see cref="CommandBuffer"/> operations.
    /// </summary>
    internal PooledDictionary<int, BufferedEntityInfo> BufferedEntityInfo { get; set; }

    /// <summary>
    ///     All create commands recorded in this <see cref="CommandBuffer"/>. Used to create <see cref="Entity"/>'s during <see cref="Playback"/>.
    /// </summary>
    internal PooledList<CreateCommand> Creates { get; set; }

    /// <summary>
    ///     Saves set operations for components to play them back later during <see cref="Playback"/>.
    /// </summary>
    internal SparseSet Sets { get; set; }

    /// <summary>
    ///     Saves add operations for components to play them back later during <see cref="Playback"/>.
    /// </summary>
    internal StructuralSparseSet Adds { get; set; }

    /// <summary>
    ///     Saves remove operations for components to play them back later during <see cref="Playback"/>.
    /// </summary>
    internal StructuralSparseSet Removes { get; set; }

    /// <summary>
    ///     Saves remove operations for <see cref="Entity"/>'s to play them back later during <see cref="Playback"/>.
    /// </summary>
    internal PooledList<int> Destroys { get; set; }

    /// <summary>
    ///     Registers a new <see cref="Entity"/> into the <see cref="CommandBuffer"/>.
    ///     An <see langword="out"/> parameter contains its <see cref="Arch.CommandBuffer.BufferedEntityInfo"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to register.</param>
    /// <param name="info">Its <see cref="BufferedEntityInfo"/> which stores indexes used for <see cref="CommandBuffer"/> operations.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Register(in Entity entity, out BufferedEntityInfo info)
    {
        var setIndex = Sets.Create(in entity);
        var addIndex = Adds.Create(in entity);
        var removeIndex = Removes.Create(in entity);

        info = new BufferedEntityInfo(Size, setIndex, addIndex, removeIndex);

        Entities.Add(entity);
        BufferedEntityInfo.Add(entity.Id, info);
        Size++;
    }

    /// TODO : Probably just run this if the wrapped entity is negative? To save some overhead? 
    /// <summary>
    ///     Resolves an <see cref="Entity"/> originally either from a <see cref="StructuralSparseArray"/> or <see cref="SparseArray"/> to its real <see cref="Entity"/>.
    ///     This is required since we can also create new entities via this buffer and buffer operations for it. So sometimes there negative entities stored in the arrays and those must then be resolved to its newly created real entity. 
    ///     <remarks>Probably hard to understand, blame genaray for this.</remarks>
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> with a negative or positive id to resolve.</param>
    /// <returns>Its real <see cref="Entity"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity Resolve(Entity entity)
    {
        var entityIndex = BufferedEntityInfo[entity.Id].Index;
        return Entities[entityIndex];
    }

    /// <summary>
    ///     Records a Create operation for an <see cref="Entity"/> based on its component structure.
    ///     Will be created during <see cref="Playback"/>.
    /// </summary>
    /// <param name="types">The <see cref="Entity"/>'s component structure/<see cref="Archetype"/>.</param>
    /// <returns>The buffered <see cref="Entity"/> with an index of <c>-1</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(ComponentType[] types)
    {
        lock (this)
        {
            var entity = new Entity(-(Size + 1), World.Id);
            Register(entity, out _);

            var command = new CreateCommand(Size - 1, types);
            Creates.Add(command);

            return entity;
        }
    }

    /// <summary>
    ///     Record a Destroy operation for an (buffered) <see cref="Entity"/>.
    ///     Will be destroyed during <see cref="Playback"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to destroy.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in Entity entity)
    {
        lock (this)
        {
            if (!BufferedEntityInfo.TryGetValue(entity.Id, out var info))
            {
                Register(entity, out info);
            }

            Destroys.Add(info.Index);
        }
    }

    /// <summary>
    ///     Records a set operation for an (buffered) <see cref="Entity"/>.
    ///     Overwrites previous values.
    ///     Will be set during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in Entity entity, in T? component = default)
    {
        BufferedEntityInfo info;
        lock (this)
        {
            if (!BufferedEntityInfo.TryGetValue(entity.Id, out info))
            {
                Register(entity, out info);
            }
        }

        Sets.Set(info.SetIndex, in component);
    }

    /// <summary>
    ///     Records a add operation for an (buffered) <see cref="Entity"/>.
    ///     Overwrites previous values.
    ///     Will be added during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity, in T? component = default)
    {
        BufferedEntityInfo info;
        lock (this)
        {
            if (!BufferedEntityInfo.TryGetValue(entity.Id, out info))
            {
                Register(entity, out info);
            }
        }

        Adds.Set<T>(info.AddIndex);
        Sets.Set(info.SetIndex, in component);
    }

    /// <summary>
    ///     Records a remove operation for an (buffered) <see cref="Entity"/>.
    ///     Will be removed during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in Entity entity)
    {
        BufferedEntityInfo info;
        lock (this)
        {
            if (!BufferedEntityInfo.TryGetValue(entity.Id, out info))
            {
                Register(entity, out info);
            }
        }

        Removes.Set<T>(info.RemoveIndex);
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="world">The world to operate on.</param>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AddRange(World world, Entity entity, IList<ComponentType> components)
    {
        var oldArchetype = world.EntityInfo.GetArchetype(entity.Id);

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);

        for (var index = 0; index < components.Count; index++)
        {
            var type = components[index];
            spanBitSet.SetBit(type.Id);
        }

        if (!world.TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = world.GetOrCreate(oldArchetype.Types.Add(components));
        }

        world.Move(entity, oldArchetype, newArchetype, out _);
    }
    
    /// <summary>
    ///     Plays back all recorded commands, modifying the world.
    /// </summary>
    /// <remarks>
    ///     This operation should only happen on the main thread.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        // Create recorded entities.
        foreach (var cmd in Creates)
        {
            var entity = World.Create(cmd.Types);
            Entities[cmd.Index] = entity;
        }

        // Play back additions.
        for (var index = 0; index < Adds.Count; index++)
        {
            var wrappedEntity = Adds.Entities[index];
            for (var i = 0; i < Adds.UsedSize; i++)
            {
                var usedIndex = Adds.Used[i];
                var sparseSet = Adds.Components[usedIndex];

                if (!sparseSet.Contains(wrappedEntity.Index))
                {
                    continue;
                }

                _addTypes.Add(sparseSet.Type);
            }

            if (_addTypes.Count <= 0)
            {
                continue;
            }
            
            // Resolves the entity to get the real one (e.g. for newly created negative entities and stuff). 
            var entity = Resolve(wrappedEntity.Entity);
            Debug.Assert(World.IsAlive(entity), $"CommandBuffer can not to add components to the dead {wrappedEntity.Entity}");

            AddRange(World, entity, _addTypes);
            _addTypes.Clear();
        }

        // Play back sets.
        for (var index = 0; index < Sets.Count; index++)
        {
            // Get wrapped entity
            var wrappedEntity = Sets.Entities[index];
            var entity = Resolve(wrappedEntity.Entity);
            var id = wrappedEntity.Index;
            
            Debug.Assert(World.IsAlive(entity), $"CommandBuffer can not to set components to the dead {wrappedEntity.Entity}");

            // Get entity chunk
            var entityInfo = World.EntityInfo[entity.Id];
            var archetype = entityInfo.Archetype;
            ref readonly var chunk = ref archetype.GetChunk(entityInfo.Slot.ChunkIndex);
            var chunkIndex = entityInfo.Slot.Index;

            // Loop over all sparset component arrays and if our entity is in one, copy the set component to its chunk
            for (var i = 0; i < Sets.UsedSize; i++)
            {
                var used = Sets.Used[i];
                var sparseArray = Sets.Components[used];

                if (!sparseArray.Contains(id))
                {
                    continue;
                }

                var chunkArray = chunk.GetArray(sparseArray.Type);
                Array.Copy(sparseArray.Components, sparseArray.Entities[id], chunkArray, chunkIndex, 1);

#if EVENTS
                // Entity also exists in add and the set component was added recently
                if (Adds.Used.Length > i && Adds.Components[Adds.Used[i]].Contains(id))
                {
                    World.OnComponentAdded(entity, sparseArray.Type);
                }
                else
                {
                    World.OnComponentSet(entity, sparseArray.Type);
                }
#endif
            }
        }

        // Play back removals.
        for (var index = 0; index < Removes.Count; index++)
        {
            var wrappedEntity = Removes.Entities[index];
            for (var i = 0; i < Removes.UsedSize; i++)
            {
                var usedIndex = Removes.Used[i];
                var sparseSet = Removes.Components[usedIndex];
                if (!sparseSet.Contains(wrappedEntity.Index))
                {
                    continue;
                }

                _removeTypes.Add(sparseSet.Type);
            }

            if (_removeTypes.Count <= 0)
            {
                continue;
            }
            
            var entity = Resolve(wrappedEntity.Entity);
            Debug.Assert(World.IsAlive(entity), $"CommandBuffer can not to remove components from the dead {wrappedEntity.Entity}");

            World.RemoveRange(entity, _removeTypes);
            _removeTypes.Clear();
        }

        // Play back destructions.
        foreach (var cmd in Destroys)
        {
            World.Destroy(Entities[cmd]);
        }

        // Reset values.
        Size = 0;
        Entities.Clear();
        BufferedEntityInfo.Clear();
        Creates.Clear();
        Sets.Clear();
        Adds.Clear();
        Removes.Clear();
        Destroys.Clear();
        _addTypes.Clear();
        _removeTypes.Clear();
    }

    /// <summary>
    ///     Disposes the <see cref="CommandBuffer"/>.
    /// </summary>
    public void Dispose()
    {
        Entities.Dispose();
        BufferedEntityInfo.Dispose();
        Creates.Clear();
        Sets.Clear();
        Adds.Clear();
        Removes.Clear();
        Destroys.Dispose();
        _addTypes.Dispose();
        _removeTypes.Dispose();
        GC.SuppressFinalize(this);
    }
}
