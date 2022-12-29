using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core.CommandBuffer;

// NOTE: This can probably be a `record struct`.
/// <summary>
///     The <see cref="CreateCommand"/> struct
///     contains data for creating a new <see cref="Entity"/>.
/// </summary>
public struct CreateCommand
{
    // TODO: Documentation.
    public int Index;
    public ComponentType[] Types;
}

// NOTE: This can probably be a `record struct`.
/// <summary>
///     The <see cref="BufferedEntityInfo"/> struct
///     contains data about a buffered <see cref="Entity"/>.
/// </summary>
/// <remarks>
///     This struct's purpose is to speed up lookups into an <see cref="Entity"/>'s internal data.
/// </remarks>
public struct BufferedEntityInfo
{
    // TODO: Documentation.
    public int Index;
    public int SetIndex;
    public int AddIndex;
    public int RemoveIndex;
}

// TODO: Documentation.
/// <summary>
///     The <see cref="CommandBuffer"/> class
///     ...
/// </summary>
public class CommandBuffer : IDisposable
{
    private readonly PooledList<ComponentType> _addTypes;
    private readonly PooledList<ComponentType> _removeTypes;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBuffer"/> class
    ///     with the specified <see cref="Core.World"/> and an optional <paramref name="initialCapacity"/> (default: 128).
    /// </summary>
    /// <param name="world"></param>
    /// <param name="initialCapacity"></param>
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

    // TODO: Documentation.
    /// <summary>
    ///     Gets the <see cref="Core.World"/>.
    /// </summary>
    public World World { get; }

    /// <summary>
    ///     Gets the amount of <see cref="Entity"/> instances targeted by this <see cref="CommandBuffer"/>.
    /// </summary>
    public int Size { get; private set; }

    // TODO: Documentation.
    internal PooledList<Entity> Entities { get; set; }
    internal PooledDictionary<int, BufferedEntityInfo> BufferedEntityInfo { get; set; }
    internal PooledList<CreateCommand> Creates { get; set; }
    internal SparseSet Sets { get; set; }
    internal StructuralSparseSet Adds { get; set; }
    internal StructuralSparseSet Removes { get; set; }
    internal PooledList<int> Destroys { get; set; }

    // TODO: Documentation.
    /// <summary>
    ///     Registers a new <see cref="Entity"/> into the <see cref="CommandBuffer"/>.
    ///     An <see langword="out"/> parameter contains its <see cref="Core.CommandBuffer.BufferedEntityInfo"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="info"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Register(in Entity entity, out BufferedEntityInfo info)
    {
        var setIndex = Sets.Create(in entity);
        var addIndex = Adds.Create(in entity);
        var removeIndex = Removes.Create(in entity);

        info = new BufferedEntityInfo
        {
            Index = Size,
            SetIndex = setIndex,
            AddIndex = addIndex,
            RemoveIndex = removeIndex
        };

        Entities.Add(entity);
        BufferedEntityInfo.Add(entity.Id, info);
        Size++;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns>The buffered <see cref="Entity"/> with an index of <c>-1</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Entity Create(ComponentType[] types)
    {
        lock (this)
        {
            var entity = new Entity(-Math.Abs(Size - 1), World.Id);
            Register(entity, out _);

            var command = new CreateCommand { Index = Size - 1, Types = types };
            Creates.Add(command);

            return entity;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="component"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="component"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
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
        for (var index = 0; index < Adds.Size; index++)
        {
            var wrappedEntity = Adds.Entities[index];
            for (var i = 0; i < Adds.UsedSize; i++)
            {
                ref var usedIndex = ref Adds.Used[i];
                ref var sparseSet = ref Adds.Components[usedIndex];

                if (!sparseSet.Has(wrappedEntity.Index))
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
            World.Add(in entity, (IList<ComponentType>)_addTypes);

            _addTypes.Clear();
        }

        // Play back removals.
        for (var index = 0; index < Removes.Size; index++)
        {
            var wrappedEntity = Removes.Entities[index];
            for (var i = 0; i < Removes.UsedSize; i++)
            {
                ref var usedIndex = ref Removes.Used[i];
                ref var sparseSet = ref Removes.Components[usedIndex];
                if (!sparseSet.Has(wrappedEntity.Index))
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
            World.Remove(in entity, _removeTypes);

            _removeTypes.Clear();
        }

        // Play back sets.
        for (var index = 0; index < Sets.Size; index++)
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

                if (!sparseArray.Has(id))
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
