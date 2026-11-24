using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Buffer;

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
    /// <param name="setIndex">Its <see cref="CommandBuffer.Sets"/> index, -1 as long as no set was recorded.</param>
    /// <param name="addIndex">Its <see cref="CommandBuffer.Adds"/> index, -1 as long as no add was recorded.</param>
    /// <param name="removeIndex">Its <see cref="CommandBuffer.Removes"/> index, -1 as long as no remove was recorded.</param>
    public BufferedEntityInfo(int index, int setIndex, int addIndex, int removeIndex)
    {
        Index = index;
        SetIndex = setIndex;
        AddIndex = addIndex;
        RemoveIndex = removeIndex;
    }

    /// <summary>
    ///     Returns a copy of this instance with another <see cref="SetIndex"/>.
    /// </summary>
    internal BufferedEntityInfo WithSetIndex(int setIndex)
    {
        return new BufferedEntityInfo(Index, setIndex, AddIndex, RemoveIndex);
    }

    /// <summary>
    ///     Returns a copy of this instance with another <see cref="AddIndex"/>.
    /// </summary>
    internal BufferedEntityInfo WithAddIndex(int addIndex)
    {
        return new BufferedEntityInfo(Index, SetIndex, addIndex, RemoveIndex);
    }

    /// <summary>
    ///     Returns a copy of this instance with another <see cref="RemoveIndex"/>.
    /// </summary>
    internal BufferedEntityInfo WithRemoveIndex(int removeIndex)
    {
        return new BufferedEntityInfo(Index, SetIndex, AddIndex, removeIndex);
    }
}

/// <summary>
///     The <see cref="CommandBuffer"/> class
///     stores operation to <see cref="Entity"/>'s between to play and implement them at a later time in the <see cref="World"/>.
/// </summary>
public sealed partial class CommandBuffer : IDisposable
{
    /// <summary>
    ///     The <see cref="ComponentType"/>s that are added to one <see cref="Entity"/> during <see cref="Playback"/>, reused for every <see cref="Entity"/>.
    /// </summary>
    private readonly PooledList<ComponentType> _addTypes;

    /// <summary>
    ///     The <see cref="ComponentType"/>s that are removed from one <see cref="Entity"/> during <see cref="Playback"/>, reused for every <see cref="Entity"/>.
    /// </summary>
    private readonly PooledList<ComponentType> _removeTypes;

    /// <summary>
    ///     Guards the recording of commands if the <see cref="CommandBuffer"/> is thread-safe.
    ///     A spin lock, since the critical sections are tiny and it is cheaper than a monitor as long as nobody waits.
    ///     Not readonly, since it is a mutable struct.
    /// </summary>
    private SpinLock _lock = new(false);

    /// <summary>
    ///     Whether commands can be recorded from several threads at once, see <see cref="CommandBuffer(int, bool)"/>.
    /// </summary>
    private readonly bool _threadSafe;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBuffer"/> class
    ///     with the specified <see cref="Core.World"/> and an optional <paramref name="initialCapacity"/> (default: 128).
    /// </summary>
    /// <param name="initialCapacity">The initial capacity.</param>
    /// <param name="threadSafe">
    ///     If true (default) the commands can be recorded from several threads at once, for example from a parallel query.
    ///     If false recording is a bit faster, but must only happen from one thread at a time.
    /// </param>
    public CommandBuffer(int initialCapacity = 128, bool threadSafe = true)
    {
        _threadSafe = threadSafe;
        Entities = new PooledList<Entity>(initialCapacity);
        BufferedEntityInfo = new Dictionary<int, BufferedEntityInfo>(initialCapacity);
        Creates = new PooledList<CreateCommand>(initialCapacity);
        Sets = new SparseSet(initialCapacity);
        Adds = new StructuralSparseSet(initialCapacity);
        Removes = new StructuralSparseSet(initialCapacity);
        Destroys = new PooledList<int>(initialCapacity);
        _addTypes = new PooledList<ComponentType>(16);
        _removeTypes = new PooledList<ComponentType>(16);
    }

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
    internal Dictionary<int, BufferedEntityInfo> BufferedEntityInfo { get; set; }

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
    ///     An <see langword="out"/> parameter contains its <see cref="Arch.Buffer.BufferedEntityInfo"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to register.</param>
    /// <param name="info">Its <see cref="BufferedEntityInfo"/> which stores indexes used for <see cref="CommandBuffer"/> operations.</param>

    internal void Register(in Entity entity, out BufferedEntityInfo info)
    {
        // The indexes in the sets are created once the first operation of their kind is recorded.
        info = new BufferedEntityInfo(Size, -1, -1, -1);

        Entities.Add(entity);
        BufferedEntityInfo.Add(entity.Id, info);
        Size++;
    }

    /// <summary>
    ///     Returns the <see cref="Arch.Buffer.BufferedEntityInfo"/> of an <see cref="Entity"/> and registers the <see cref="Entity"/> if it is not buffered yet.
    ///     The caller has to hold the lock of this <see cref="CommandBuffer"/>, since it is the only synchronization for all its sets.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="Arch.Buffer.BufferedEntityInfo"/>.</returns>
    private BufferedEntityInfo GetOrRegister(in Entity entity)
    {
        if (!BufferedEntityInfo.TryGetValue(entity.Id, out var info))
        {
            Register(entity, out info);
        }

        return info;
    }

    /// <summary>
    ///     Returns the index of an <see cref="Entity"/> in the <see cref="Sets"/> and creates it if it has none yet.
    ///     The caller has to hold the lock of this <see cref="CommandBuffer"/>.
    /// </summary>
    private int GetSetIndex(in Entity entity)
    {
        var info = GetOrRegister(entity);
        if (info.SetIndex >= 0)
        {
            return info.SetIndex;
        }

        var setIndex = Sets.Create(in entity);
        BufferedEntityInfo[entity.Id] = info.WithSetIndex(setIndex);
        return setIndex;
    }

    /// <summary>
    ///     Returns the index of an <see cref="Entity"/> in the <see cref="Adds"/> and creates it if it has none yet.
    ///     The caller has to hold the lock of this <see cref="CommandBuffer"/>.
    /// </summary>
    private int GetAddIndex(in Entity entity)
    {
        var info = GetOrRegister(entity);
        if (info.AddIndex >= 0)
        {
            return info.AddIndex;
        }

        var addIndex = Adds.Create(in entity);
        BufferedEntityInfo[entity.Id] = info.WithAddIndex(addIndex);
        return addIndex;
    }

    /// <summary>
    ///     Returns the index of an <see cref="Entity"/> in the <see cref="Removes"/> and creates it if it has none yet.
    ///     The caller has to hold the lock of this <see cref="CommandBuffer"/>.
    /// </summary>
    private int GetRemoveIndex(in Entity entity)
    {
        var info = GetOrRegister(entity);
        if (info.RemoveIndex >= 0)
        {
            return info.RemoveIndex;
        }

        var removeIndex = Removes.Create(in entity);
        BufferedEntityInfo[entity.Id] = info.WithRemoveIndex(removeIndex);
        return removeIndex;
    }

#if EVENTS
    /// <summary>
    ///     Checks whether the component of a certain type was recorded to be added to an <see cref="Entity"/>, and not only to be set.
    /// </summary>
    private bool IsAdded(Entity wrappedEntity, ComponentType type)
    {
        var addIndex = BufferedEntityInfo[wrappedEntity.Id].AddIndex;
        return addIndex >= 0 && type.Id < Adds.Components.Length && Adds.Components[type.Id]?.Contains(addIndex) == true;
    }
#endif

    /// <summary>
    ///     Takes the lock of this <see cref="CommandBuffer"/> until the returned scope is disposed, if it is thread-safe.
    /// </summary>
    private LockScope Lock()
    {
        var taken = false;
        if (_threadSafe)
        {
            _lock.Enter(ref taken);
        }

        return new LockScope(this, taken);
    }

    /// <summary>
    ///     The <see cref="LockScope"/> struct
    ///     releases the lock of a <see cref="CommandBuffer"/> once it is disposed, if it was taken. Meant for a <c>using</c> declaration.
    /// </summary>
    private readonly struct LockScope : IDisposable
    {
        private readonly CommandBuffer _buffer;
        private readonly bool _taken;

        /// <summary>
        ///     Initializes a new instance of the <see cref="LockScope"/> struct.
        /// </summary>
        /// <param name="buffer">The <see cref="CommandBuffer"/> whose lock is released.</param>
        /// <param name="taken">Whether the lock was taken, it is not for a <see cref="CommandBuffer"/> that is not thread-safe.</param>
        public LockScope(CommandBuffer buffer, bool taken)
        {
            _buffer = buffer;
            _taken = taken;
        }

        /// <summary>
        ///     Releases the lock if it was taken.
        /// </summary>
        public void Dispose()
        {
            if (_taken)
            {
                _buffer._lock.Exit(false);
            }
        }
    }

    /// <summary>
    ///     Resolves an <see cref="Entity"/> originally either from a <see cref="StructuralSparseArray"/> or <see cref="SparseArray"/> to its real <see cref="Entity"/>.
    ///     This is required since we can also create new entities via this buffer and buffer operations for it. So sometimes there negative entities stored in the arrays and those must then be resolved to its newly created real entity.
    ///     <remarks>Probably hard to understand, blame genaray for this.</remarks>
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> with a negative or positive id to resolve.</param>
    /// <returns>Its real <see cref="Entity"/>.</returns>

    internal Entity Resolve(Entity entity)
    {
        // Only the entities created by this buffer have a negative id, all others are real already.
        if (entity.Id >= 0)
        {
            return entity;
        }

        var entityIndex = BufferedEntityInfo[entity.Id].Index;
        return Entities[entityIndex];
    }

    /// <summary>
    ///     Records a Create operation for an <see cref="Entity"/> based on its component structure.
    ///     Will be created during <see cref="Playback"/>.
    /// </summary>
    /// <param name="types">The <see cref="Entity"/>'s component structure/<see cref="Archetype"/>.</param>
    /// <returns>The buffered <see cref="Entity"/> with an index of <c>-1</c>.</returns>

    public Entity Create(ComponentType[] types)
    {
        using var scope = Lock();

        var entity = new Entity(-(Size + 1), -1);
        Register(entity, out _);

        var command = new CreateCommand(Size - 1, types);
        Creates.Add(command);

        return entity;
    }

    /// <summary>
    ///     Record a Destroy operation for an (buffered) <see cref="Entity"/>.
    ///     Will be destroyed during <see cref="Playback"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to destroy.</param>

    public void Destroy(in Entity entity)
    {
        using var scope = Lock();
        Destroys.Add(GetOrRegister(entity).Index);
    }

    /// <summary>
    ///     Records a set operation for an (buffered) <see cref="Entity"/>.
    ///     Overwrites previous values.
    ///     Will be set during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value.</param>

    public void Set<T>(in Entity entity, in T? component = default)
    {
        using var scope = Lock();
        Sets.Set(GetSetIndex(entity), in component);
    }

    /// <summary>
    ///     Records a add operation for an (buffered) <see cref="Entity"/>.
    ///     Overwrites previous values.
    ///     Will be added during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value.</param>

    public void Add<T>(in Entity entity, in T? component = default)
    {
        using var scope = Lock();
        Adds.Set<T>(GetAddIndex(entity));
        Sets.Set(GetSetIndex(entity), in component);
    }

    /// <summary>
    ///     Records a remove operation for an (buffered) <see cref="Entity"/>.
    ///     Will be removed during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>

    public void Remove<T>(in Entity entity)
    {
        using var scope = Lock();
        Removes.Set<T>(GetRemoveIndex(entity));
    }

    /// <summary>
    ///     Plays back all recorded commands, modifying the world.
    /// </summary>
    /// <remarks>
    ///     This operation should only happen on the main thread.
    /// </remarks>
    /// <param name="world">The <see cref="World"/> where the commands will be playbacked too.</param>
    /// <param name="dispose">If true it will clear the recorded operations after they were playbacked, if not they will stay.</param>

    public void Playback(World world, bool dispose = true)
    {
        // Create recorded entities.
        int createCount = Creates.Count;
        foreach (var cmd in Creates)
        {
            var entity = world.Create(cmd.Types);
            Entities[cmd.Index] = entity;
        }

        // Play back additions.
        int addCount = Adds.Count;
        for (var index = 0; index < addCount; index++)
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
            Debug.Assert(world.IsAlive(entity), $"CommandBuffer can not to add components to the dead {wrappedEntity.Entity}");

            world.AddRangeWithoutEvents(entity, _addTypes.Span);
            _addTypes.Clear();
        }

        // Play back sets.
        int setCount = Sets.Count;
        for (var index = 0; index < setCount; index++)
        {
            // Get wrapped entity
            var wrappedEntity = Sets.Entities[index];
            var entity = Resolve(wrappedEntity.Entity);
            var id = wrappedEntity.Index;

            Debug.Assert(world.IsAlive(entity), $"CommandBuffer can not to set components to the dead {wrappedEntity.Entity}");

            // Get entity chunk
            var entityInfo = world.EntityInfo.GetEntityData(entity.Id);
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
                sparseArray.Ops.Copy(sparseArray.Components, sparseArray.Entities[id], chunkArray, chunkIndex);

#if EVENTS
                // Entity also exists in add and the set component was added recently
                if (IsAdded(wrappedEntity.Entity, sparseArray.Type))
                {
                    world.OnComponentAdded(entity, sparseArray.Type);
                }
                else
                {
                    world.OnComponentSet(entity, sparseArray.Type);
                }
#endif
            }
        }

        // Play back removals.
        int removeCount = Removes.Count;
        for (var index = 0; index < removeCount; index++)
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
            Debug.Assert(world.IsAlive(entity), $"CommandBuffer can not to remove components from the dead {wrappedEntity.Entity}");

            world.RemoveRange(entity, _removeTypes.Span);
            _removeTypes.Clear();
        }

        // Play back destructions.
        int destroyCount = Destroys.Count;
        foreach (var cmd in Destroys)
        {
            world.Destroy(Entities[cmd]);
        }

        // Reset values.
        if (!dispose)
        {
            return;
        }

        Size = 0;
        Entities.Clear();
        BufferedEntityInfo.Clear();
        if (createCount > 0)
        {
            Creates.Clear();
        }
        if (setCount > 0)
        {
            Sets.Clear();
        }
        if (addCount > 0)
        {
            Adds.Clear();
        }
        if (removeCount > 0)
        {
            Removes.Clear();
        }
        if (destroyCount > 0)
        {
            Destroys.Clear();
        }
        _addTypes.Clear();
        _removeTypes.Clear();
    }

    /// <summary>
    ///     Disposes the <see cref="CommandBuffer"/>.
    /// </summary>
    public void Dispose()
    {
        Entities.Dispose();
        BufferedEntityInfo.Clear();
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
