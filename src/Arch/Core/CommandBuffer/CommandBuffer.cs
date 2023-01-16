using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core.CommandBuffer;

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
    /// <param name="initialCapacity">The <see cref="initialCapacity"/>.</param>
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
    ///     An <see langword="out"/> parameter contains its <see cref="Core.CommandBuffer.BufferedEntityInfo"/>.
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
            var entity = new Entity(-Math.Abs(Size - 1), World.Id);
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
    public void Set<T>(in Entity entity, in T component)
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
    public void Add<T>(in Entity entity, in T component)
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
                ref var usedIndex = ref Adds.Used[i];
                ref var sparseSet = ref Adds.Components[usedIndex];

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

            var entityIndex = BufferedEntityInfo[wrappedEntity.Entity.Id].Index;
            var entity = Entities[entityIndex];
            World.AddRange(in entity, (IList<ComponentType>)_addTypes);

            _addTypes.Clear();
        }

        // Play back removals.
        for (var index = 0; index < Removes.Count; index++)
        {
            var wrappedEntity = Removes.Entities[index];
            for (var i = 0; i < Removes.UsedSize; i++)
            {
                ref var usedIndex = ref Removes.Used[i];
                ref var sparseSet = ref Removes.Components[usedIndex];
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

            var entityIndex = BufferedEntityInfo[wrappedEntity.Entity.Id].Index;
            var entity = Entities[entityIndex];
            World.RemoveRange(in entity, _removeTypes);

            _removeTypes.Clear();
        }

        // Play back sets.
        for (var index = 0; index < Sets.Count; index++)
        {
            // Get wrapped entity
            var wrappedEntity = Sets.Entities[index];
            var entityIndex = BufferedEntityInfo[wrappedEntity.Entity.Id].Index;
            var entity = Entities[entityIndex];
            ref readonly var id = ref wrappedEntity.Index;

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
                Array.Copy(sparseArray.Components, id, chunkArray, chunkIndex, 1);
            }
        }

        // Play back destructions.
        foreach (var cmd in Destroys)
        {
            World.Destroy(Entities[cmd]);
        }

        // Reset values.
        Size = 0;
        Entities?.Clear();
        BufferedEntityInfo?.Clear();
        Creates?.Clear();
        Sets?.Dispose();
        Adds?.Dispose();
        Removes?.Dispose();
        Destroys?.Clear();
        _addTypes?.Clear();
        _removeTypes?.Clear();
    }

    /// <summary>
    ///     Disposes the <see cref="CommandBuffer"/>.
    /// </summary>
    public void Dispose()
    {
        Entities?.Dispose();
        BufferedEntityInfo?.Dispose();
        Creates?.Dispose();
        Sets?.Dispose();
        Adds?.Dispose();
        Removes?.Dispose();
        Destroys?.Dispose();
        _addTypes?.Dispose();
        _removeTypes?.Dispose();
    }
}
