using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core.CommandBuffer;

/// <summary>
/// Represents a creation command for an entity. 
/// </summary>
public struct CreateCommand
{
    public int Index;
    public ComponentType[] Types;
}

/// <summary>
/// Information about a buffered entity for fast acess to its internal storages. 
/// </summary>
public struct BufferedEntityInfo
{
    public int Index;
    public int SetIndex;
    public int AddIndex;
    public int RemoveIndex;
}

/// <summary>
/// A command buffer to rule them all. 
/// </summary>
public class CommandBuffer : IDisposable
{
    private readonly PooledList<ComponentType> _addTypes;
    private readonly PooledList<ComponentType> _removeTypes;

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
    /// The world
    /// </summary>
    public World World { get; }

    /// <summary>
    /// Amount of entities targeted by this buffer.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Entities targeted by this buffer.
    /// </summary>
    internal PooledList<Entity> Entities { get; set; }

    /// <summary>
    /// Lookup
    /// </summary>
    internal PooledDictionary<int, BufferedEntityInfo> BufferedEntityInfo { get; set; }

    /// <summary>
    /// Create commands
    /// </summary>
    internal PooledList<CreateCommand> Creates { get; set; }

    /// <summary>
    /// Set commands
    /// </summary>
    internal SparseSet Sets { get; set; }

    /// <summary>
    /// Add commands
    /// </summary>
    internal StructuralSparseSet Adds { get; set; }

    /// <summary>
    /// Remove commands
    /// </summary>
    internal StructuralSparseSet Removes { get; set; }

    /// <summary>
    /// Destroy commands 
    /// </summary>
    internal PooledList<int> Destroys { get; set; }

    /// <summary>
    /// Registers a new entity into the command buffer and returns its info struct. 
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

    /// <summary>
    /// Buffers a create command for a certain entity. Will be created upon playback.
    /// </summary>
    /// <param name="types">Its archetype.</param>
    /// <returns>The buffered entity with a negative id.</returns>#
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

    /// <summary>
    /// Buffers a destroy command for the passed entity. Will be destroyed upon playback.
    /// </summary>
    /// <param name="entity">The entity to destroy.</param>
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
    /// Buffers a set command for the passed entity. Will be set upon playback.
    /// </summary>
    /// <param name="entity">The entity on which we wanna set a component.</param>
    /// <param name="component">The component instance</param>
    /// <typeparam name="T">The generic type.</typeparam>
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
    /// Buffers a add command for the passed entity. Will be set upon playback.
    /// </summary>
    /// <param name="entity">The entity which we wanna add a component to.</param>
    /// <param name="component">The component instance.</param>
    /// <typeparam name="T">The generic type.</typeparam>
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
    /// Buffers a remove command for the passed entity. Will be set upon playback.
    /// </summary>
    /// <param name="entity">The entity which we wanna remove a component from.</param>
    /// <typeparam name="T">The generic type.</typeparam>
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
    /// Playbacks all recorded operations and modifies the world.
    /// Should only happen on the mainthread. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        // Create recorded entities
        foreach (var cmd in Creates)
        {
            var entity = World.Create(cmd.Types);
            Entities[cmd.Index] = entity;
        }

        // Playback adds
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

        // Playback removes 
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

        // Loop over all sparset entities
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

        // Create recorded entities
        foreach (var cmd in Destroys)
        {
            World.Destroy(Entities[cmd]);
        }

        // Reset 
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
    /// Disposes this command buffer. 
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
