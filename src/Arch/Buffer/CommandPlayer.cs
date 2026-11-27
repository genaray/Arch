using System.Diagnostics;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="CommandPlayer"/> class
///     applies the recorded <see cref="Command"/>s of a <see cref="CommandStream"/> to a <see cref="World"/>.
///     Reused for every playback to avoid allocations.
/// </summary>
internal sealed class CommandPlayer : IDisposable
{
    private readonly EntityCreator _creator;
    private readonly EntityGroups _groups = new();

    /// <summary>
    ///     The changes of the <see cref="Entity"/> that is played back right now.
    /// </summary>
    private readonly StructuralChanges _changes = new();

    /// <summary>
    ///     The <see cref="Entity"/>s to destroy once all streams are played back.
    /// </summary>
    private readonly PooledList<Entity> _destroyed = new(16);

    /// <summary>
    ///     The <see cref="World"/> and the stream of the current <see cref="Play"/>, only set while it runs.
    /// </summary>
    private World _world = null!;
    private CommandStream _stream = null!;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandPlayer"/> class.
    /// </summary>
    /// <param name="creator">The <see cref="EntityCreator"/> that resolves the buffered <see cref="Entity"/>s.</param>
    public CommandPlayer(EntityCreator creator)
    {
        _creator = creator;
    }

    /// <summary>
    ///     Applies the commands of a stream, all commands for the same <see cref="Entity"/> together.
    ///     Destroys are only collected, see <see cref="DestroyCollected"/>.
    /// </summary>
    /// <param name="world">The <see cref="World"/> to apply them to.</param>
    /// <param name="stream">The stream with the recorded commands.</param>
    public void Play(World world, CommandStream stream)
    {
        _world = world;
        _stream = stream;

        if (stream.ContainsOnlySets)
        {
            PlaySets();
        }
        else
        {
            PlayEntities();
        }

        _world = null!;
        _stream = null!;
    }

    /// <summary>
    ///     Destroys the <see cref="Entity"/>s collected by <see cref="Play"/>.
    ///     Done last, so the commands of every stream can still target them.
    /// </summary>
    /// <param name="world">The <see cref="World"/> to destroy them in.</param>
    public void DestroyCollected(World world)
    {
        foreach (var entity in _destroyed)
        {
            world.Destroy(entity);
        }

        _destroyed.Clear();
    }

    /// <summary>
    ///     Disposes the lists.
    /// </summary>
    public void Dispose()
    {
        _changes.Dispose();
        _destroyed.Dispose();
    }

    /// <summary>
    ///     Applies a stream that only sets values one by one, since there are no structural changes to combine.
    /// </summary>
    private void PlaySets()
    {
        foreach (ref readonly var command in _stream.Commands)
        {
            var entity = _creator.Resolve(command.Entity);
            Debug.Assert(_world.IsAlive(entity), $"CommandBuffer can not set components of the dead {command.Entity}");

            CopyValue(command, entity);
            RaiseValueEvent(entity, command.Type, false);
        }
    }

    /// <summary>
    ///     Applies a stream with structural changes, by grouping its commands per <see cref="Entity"/> first.
    /// </summary>
    private void PlayEntities()
    {
        var commands = _stream.Commands;
        _groups.Build(commands);

        foreach (var group in _groups.Groups)
        {
            PlayEntity(commands, group);
        }

        _groups.Clear();
    }

    /// <summary>
    ///     Applies all commands for one <see cref="Entity"/>: first the components to add, then the values, then the components to remove.
    /// </summary>
    private void PlayEntity(ReadOnlySpan<Command> commands, in EntityGroup group)
    {
        var entity = _creator.Resolve(group.Entity);
        Debug.Assert(_world.IsAlive(entity), $"CommandBuffer can not change the dead {group.Entity}");

        _changes.Collect(commands, group.First);
        if (_changes.IsDestroyed)
        {
            _destroyed.Add(entity);
            return;
        }

        if (_changes.Added.Count > 0)
        {
            _world.AddRangeWithoutEvents(entity, _changes.Added.Span);
        }

        if (_changes.HasValues)
        {
            SetValues(commands, group.First, entity);
        }

        if (_changes.Removed.Count > 0)
        {
            _world.RemoveRange(entity, _changes.Removed.Span);
        }
    }

    /// <summary>
    ///     Copies the recorded values of one <see cref="Entity"/> into its <see cref="Chunk"/>, except the ones of components that are removed again.
    /// </summary>
    private void SetValues(ReadOnlySpan<Command> commands, int first, Entity entity)
    {
        for (var index = first; index >= 0; index = commands[index].Next)
        {
            ref readonly var command = ref commands[index];
            if (command.Kind is not (CommandKind.Set or CommandKind.Add) || _changes.IsDropped(command.Type))
            {
                continue;
            }

            CopyValue(command, entity);
            RaiseValueEvent(entity, command.Type, _changes.IsAdded(command.Type));
        }
    }

    /// <summary>
    ///     Copies the recorded value of a <see cref="Command"/> into the <see cref="Chunk"/> of its <see cref="Entity"/>.
    /// </summary>
    private void CopyValue(in Command command, Entity entity)
    {
        var data = _world.EntityInfo.GetEntityData(entity.Id);
        ref readonly var chunk = ref data.Archetype.GetChunk(data.Slot.ChunkIndex);

        var values = _stream.GetValues(command.Type);
        values.Ops.Copy(values.Items, command.ValueIndex, chunk.GetArray(command.Type), data.Slot.Index);
    }

    /// <summary>
    ///     Raises the event for a value that was written, only in builds with events.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component that was written.</param>
    /// <param name="wasAdded">Whether the component was added together with the value, otherwise it was set.</param>
    [Conditional("EVENTS")]
    private void RaiseValueEvent(Entity entity, in ComponentType type, bool wasAdded)
    {
        if (wasAdded)
        {
            _world.OnComponentAdded(entity, type);
        }
        else
        {
            _world.OnComponentSet(entity, type);
        }
    }
}

/// <summary>
///     The <see cref="EntityCreator"/> class
///     creates the <see cref="Entity"/>s a <see cref="CommandBuffer"/> recorded and resolves the buffered <see cref="Entity"/>s it handed out to the real ones.
///     Buffered <see cref="Entity"/>s have a negative id, which encodes the index of their create.
/// </summary>
internal sealed class EntityCreator
{
    /// <summary>
    ///     The created <see cref="Entity"/>s, at the index of their buffered <see cref="Entity"/>.
    /// </summary>
    private Entity[] _created = [];

    /// <summary>
    ///     Receives the <see cref="Entity"/>s of one batch, reused for every batch.
    /// </summary>
    private Entity[] _batch = [];

    /// <summary>
    ///     Returns the buffered <see cref="Entity"/> of a create.
    /// </summary>
    /// <param name="index">The index of the create, in the order the <see cref="CommandBuffer"/> handed them out.</param>
    public static Entity BufferedEntity(int index)
    {
        return new Entity(-(index + 1), -1);
    }

    /// <summary>
    ///     Returns the index of the create of a buffered <see cref="Entity"/>.
    /// </summary>
    /// <param name="entityId">The negative id of the buffered <see cref="Entity"/>.</param>
    public static int IndexOf(int entityId)
    {
        return -entityId - 1;
    }

    /// <summary>
    ///     Resolves an <see cref="Entity"/> to the real one, once it was created.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> with a negative or positive id to resolve.</param>
    /// <returns>Its real <see cref="Entity"/>.</returns>
    public Entity Resolve(in Entity entity)
    {
        return entity.Id >= 0 ? entity : _created[IndexOf(entity.Id)];
    }

    /// <summary>
    ///     Creates the <see cref="Entity"/>s of all streams, before any of their other commands are played back.
    /// </summary>
    /// <param name="world">The <see cref="World"/> to create the <see cref="Entity"/>s in.</param>
    /// <param name="streams">The streams with the recorded creates.</param>
    /// <param name="createdCount">The amount of creates recorded in all streams.</param>
    public void Create(World world, List<CommandStream> streams, int createdCount)
    {
        if (_created.Length < createdCount)
        {
            Array.Resize(ref _created, Math.Max(createdCount, _created.Length * 2));
        }

        foreach (var stream in streams)
        {
            Create(world, stream.Creates);
        }
    }

    /// <summary>
    ///     Creates the <see cref="Entity"/>s of one stream, batch by batch.
    /// </summary>
    private void Create(World world, List<CreateCommand> creates)
    {
        for (var start = 0; start < creates.Count;)
        {
            var end = EndOfBatch(creates, start);
            CreateBatch(world, creates, start, end);
            start = end;
        }
    }

    /// <summary>
    ///     Finds the end of the creates that share the same component structure, starting at <paramref name="start"/>.
    ///     Only creates that were recorded with the same array of <see cref="ComponentType"/>s are batched, which is what callers do who create many equal entities.
    /// </summary>
    private static int EndOfBatch(List<CreateCommand> creates, int start)
    {
        var types = creates[start].Types;
        var end = start + 1;
        while (end < creates.Count && ReferenceEquals(creates[end].Types, types))
        {
            end++;
        }

        return end;
    }

    /// <summary>
    ///     Creates the <see cref="Entity"/>s of a batch of creates at once, which is cheaper than one by one.
    /// </summary>
    private void CreateBatch(World world, List<CreateCommand> creates, int start, int end)
    {
        var amount = end - start;
        var types = creates[start].Types;
        if (amount == 1)
        {
            _created[IndexOf(creates[start].Entity.Id)] = world.Create(types);
            return;
        }

        if (_batch.Length < amount)
        {
            _batch = new Entity[Math.Max(amount, _batch.Length * 2)];
        }

        world.Create(_batch.AsSpan(0, amount), types, amount);
        for (var index = 0; index < amount; index++)
        {
            var entity = _batch[index];
            _created[IndexOf(creates[start + index].Entity.Id)] = entity;
            RaiseCreatedEvents(world, entity, types);
        }
    }

    /// <summary>
    ///     Raises the events a single create raises, which the bulk create does not.
    /// </summary>
    private static void RaiseCreatedEvents(World world, Entity entity, ComponentType[] types)
    {
        world.OnEntityCreated(entity);
#if EVENTS
        foreach (var type in types)
        {
            world.OnComponentAdded(entity, type);
        }
#endif
    }
}

/// <summary>
///     The <see cref="EntityGroup"/> struct
///     is the chain of all <see cref="Command"/>s that target one <see cref="Entity"/>.
/// </summary>
internal struct EntityGroup
{
    /// <summary>
    ///     The <see cref="Entity"/> all commands of the group target.
    /// </summary>
    public readonly Entity Entity;

    /// <summary>
    ///     The index of its first <see cref="Command"/>, the others follow through <see cref="Command.Next"/>.
    /// </summary>
    public readonly int First;

    /// <summary>
    ///     The index of its last <see cref="Command"/>, to append the next one.
    /// </summary>
    public int Last;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityGroup"/> struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="first">The index of its first <see cref="Command"/>.</param>
    public EntityGroup(in Entity entity, int first)
    {
        Entity = entity;
        First = first;
        Last = first;
    }
}

/// <summary>
///     The <see cref="EntityGroups"/> class
///     finds all <see cref="Command"/>s that target the same <see cref="Entity"/>, so <see cref="CommandBuffer.Playback"/> can apply them together, with one structural change per <see cref="Entity"/>.
///     Looks the <see cref="Entity"/> up in a table indexed by its id, which is much cheaper than hashing it.
///     The table is reused, so grouping does not allocate once it has grown.
/// </summary>
internal sealed class EntityGroups
{
    // The tables store the index of the group plus one, so a zeroed slot means the entity has no group yet.
    private int[] _slotsOfExisting = [];
    private int[] _slotsOfBuffered = [];

    /// <summary>
    ///     The built groups, only the first <see cref="_count"/> are in use.
    /// </summary>
    private EntityGroup[] _groups = new EntityGroup[16];
    private int _count;

    /// <summary>
    ///     The groups built by the last call of <see cref="Build"/>, in the order their first command was recorded.
    /// </summary>
    public ReadOnlySpan<EntityGroup> Groups => _groups.AsSpan(0, _count);

    /// <summary>
    ///     Groups the commands by their <see cref="Entity"/> and links the commands of a group through <see cref="Command.Next"/>, in the order they were recorded.
    ///     Call <see cref="Clear"/> before the next one.
    /// </summary>
    /// <param name="commands">The recorded commands.</param>
    public void Build(Span<Command> commands)
    {
        for (var index = 0; index < commands.Length; index++)
        {
            ref var command = ref commands[index];
            command.Next = -1;

            ref var slot = ref SlotOf(command.Entity.Id);
            if (slot == 0)
            {
                slot = Add(new EntityGroup(command.Entity, index));
                continue;
            }

            ref var group = ref _groups[slot - 1];
            commands[group.Last].Next = index;
            group.Last = index;
        }
    }

    /// <summary>
    ///     Forgets the groups, so the next <see cref="Build"/> starts from scratch.
    /// </summary>
    public void Clear()
    {
        foreach (var group in Groups)
        {
            SlotOf(group.Entity.Id) = 0;
        }

        _count = 0;
    }

    /// <summary>
    ///     Appends a group and grows the array if it is full.
    /// </summary>
    /// <returns>The index of the group plus one, as stored in the slot tables.</returns>
    private int Add(in EntityGroup group)
    {
        if (_count == _groups.Length)
        {
            Array.Resize(ref _groups, _count * 2);
        }

        _groups[_count++] = group;
        return _count;
    }

    // Entities created by the command buffer have a negative id, their slots are kept apart from the ones of existing entities.
    /// <summary>
    ///     Returns the slot of an <see cref="Entity"/> in the table of existing or buffered entities.
    /// </summary>
    private ref int SlotOf(int entityId)
    {
        return ref entityId >= 0 ? ref SlotAt(ref _slotsOfExisting, entityId) : ref SlotAt(ref _slotsOfBuffered, EntityCreator.IndexOf(entityId));
    }

    /// <summary>
    ///     Returns a slot of a table and grows the table if it does not reach the index yet.
    /// </summary>
    private static ref int SlotAt(ref int[] slots, int index)
    {
        if (index >= slots.Length)
        {
            Array.Resize(ref slots, Math.Max(index + 1, slots.Length * 2));
        }

        return ref slots[index];
    }
}

/// <summary>
///     The <see cref="StructuralChanges"/> class
///     works out which components one <see cref="Entity"/> gets and loses from all its recorded <see cref="Command"/>s.
///     Reused for every <see cref="Entity"/> to avoid allocations.
/// </summary>
internal sealed class StructuralChanges : IDisposable
{
    /// <summary>
    ///     The components that were added and removed again, their values are dropped too.
    /// </summary>
    private readonly PooledList<ComponentType> _cancelled = new(4);

    /// <summary>
    ///     The components to add to the <see cref="Entity"/>, without duplicates.
    /// </summary>
    public PooledList<ComponentType> Added { get; } = new(16);

    /// <summary>
    ///     The components to remove from the <see cref="Entity"/>, without duplicates.
    /// </summary>
    public PooledList<ComponentType> Removed { get; } = new(16);

    /// <summary>
    ///     Whether the <see cref="Entity"/> has values to set, either for existing components or for the <see cref="Added"/> ones.
    /// </summary>
    public bool HasValues { get; private set; }

    /// <summary>
    ///     Whether the <see cref="Entity"/> has to be destroyed, which makes all its other commands pointless.
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    ///     Collects the changes of one <see cref="Entity"/>.
    ///     A component that is added and removed again leaves the <see cref="Entity"/> as it was, so it ends up in neither <see cref="Added"/> nor <see cref="Removed"/>.
    /// </summary>
    /// <param name="commands">All commands, linked through <see cref="Command.Next"/> by <see cref="EntityGroups"/>.</param>
    /// <param name="first">The index of the first command of the <see cref="Entity"/>.</param>
    public void Collect(ReadOnlySpan<Command> commands, int first)
    {
        Clear();

        for (var index = first; index >= 0; index = commands[index].Next)
        {
            ref readonly var command = ref commands[index];
            switch (command.Kind)
            {
                case CommandKind.Add:
                    AddUnique(Added, command.Type);
                    HasValues = true;
                    break;
                case CommandKind.Set:
                    HasValues = true;
                    break;
                case CommandKind.Remove:
                    AddUnique(Removed, command.Type);
                    break;
                case CommandKind.Destroy:
                    IsDestroyed = true;
                    return;
            }
        }

        if (Added.Count > 0 && Removed.Count > 0)
        {
            CancelAddedAndRemoved();
        }
    }

    /// <summary>
    ///     Checks whether the value of a component is pointless, since the component gets removed again.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsDropped(in ComponentType type)
    {
        return (Removed.Count > 0 || _cancelled.Count > 0) && (Removed.Contains(type) || _cancelled.Contains(type));
    }

    /// <summary>
    ///     Checks whether a component is added to the <see cref="Entity"/>.
    /// </summary>
    public bool IsAdded(in ComponentType type)
    {
        return Added.Contains(type);
    }

    /// <summary>
    ///     Disposes the lists.
    /// </summary>
    public void Dispose()
    {
        Added.Dispose();
        Removed.Dispose();
        _cancelled.Dispose();
    }

    /// <summary>
    ///     Forgets the changes of the previous <see cref="Entity"/>.
    /// </summary>
    private void Clear()
    {
        Added.Clear();
        Removed.Clear();
        _cancelled.Clear();
        HasValues = false;
        IsDestroyed = false;
    }

    /// <summary>
    ///     Removes the components that are both <see cref="Added"/> and <see cref="Removed"/> from both lists and remembers them.
    /// </summary>
    private void CancelAddedAndRemoved()
    {
        for (var index = Removed.Count - 1; index >= 0; index--)
        {
            var type = Removed[index];
            if (Added.Remove(type))
            {
                Removed.RemoveAt(index);
                _cancelled.Add(type);
            }
        }
    }

    /// <summary>
    ///     Adds a component type to a list, unless it is in there already.
    /// </summary>
    private static void AddUnique(PooledList<ComponentType> types, in ComponentType type)
    {
        if (!types.Contains(type))
        {
            types.Add(type);
        }
    }
}
