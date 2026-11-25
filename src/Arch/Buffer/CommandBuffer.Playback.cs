using System;
using System.Diagnostics;
using Arch.Core;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Buffer;

public sealed partial class CommandBuffer
{
    private readonly EntityGroups _groups = new();
    private readonly StructuralChanges _structuralChanges = new();
    private readonly PooledList<Entity> _destroyed = new(16);

    /// <summary>
    ///     The <see cref="Entity"/>s created during <see cref="Playback"/>, at the index of their buffered <see cref="Entity"/>.
    /// </summary>
    private Entity[] _created = [];

    /// <summary>
    ///     Plays back all recorded commands, modifying the world.
    /// </summary>
    /// <remarks>
    ///     This operation should only happen on the main thread, while no other thread records.
    /// </remarks>
    /// <param name="world">The <see cref="World"/> where the commands will be playbacked too.</param>
    /// <param name="dispose">If true it will clear the recorded operations after they were playbacked, if not they will stay.</param>
    public void Playback(World world, bool dispose = true)
    {
        CreateEntities(world);

        foreach (var stream in _streams)
        {
            PlaybackStream(world, stream);
        }

        DestroyEntities(world);

        if (dispose)
        {
            Clear();
        }
    }

    /// <summary>
    ///     Creates the <see cref="Entity"/>s of all streams first, since any thread can record commands for them.
    /// </summary>
    private void CreateEntities(World world)
    {
        if (_created.Length < _createdCount)
        {
            Array.Resize(ref _created, Math.Max(_createdCount, _created.Length * 2));
        }

        foreach (var stream in _streams)
        {
            foreach (var create in stream.Creates)
            {
                _created[IndexOfBuffered(create.Entity)] = world.Create(create.Types);
            }
        }
    }

    /// <summary>
    ///     Applies the commands of a stream, all commands for the same <see cref="Entity"/> together.
    /// </summary>
    private void PlaybackStream(World world, CommandStream stream)
    {
        if (stream.ContainsOnlySets)
        {
            PlaybackSets(world, stream);
            return;
        }

        var commands = stream.Commands;
        _groups.Build(commands);

        foreach (var group in _groups.Groups)
        {
            PlaybackEntity(world, stream, commands, group);
        }

        _groups.Clear();
    }

    /// <summary>
    ///     Applies a stream that only sets values one by one, since there are no structural changes to combine.
    /// </summary>
    private void PlaybackSets(World world, CommandStream stream)
    {
        foreach (ref readonly var command in stream.Commands)
        {
            var entity = Resolve(command.Entity);
            Debug.Assert(world.IsAlive(entity), $"CommandBuffer can not set components of the dead {command.Entity}");

            CopyValue(world, stream, command, entity);
#if EVENTS
            world.OnComponentSet(entity, command.Type);
#endif
        }
    }

    /// <summary>
    ///     Applies all commands for one <see cref="Entity"/>: first the components to add, then the values, then the components to remove.
    /// </summary>
    private void PlaybackEntity(World world, CommandStream stream, ReadOnlySpan<Command> commands, in EntityGroup group)
    {
        var entity = Resolve(group.Entity);
        Debug.Assert(world.IsAlive(entity), $"CommandBuffer can not change the dead {group.Entity}");

        var changes = _structuralChanges;
        changes.Collect(commands, group.First);

        if (changes.IsDestroyed)
        {
            _destroyed.Add(entity);
            return;
        }

        if (changes.Added.Count > 0)
        {
            world.AddRangeWithoutEvents(entity, changes.Added.Span);
        }

        if (changes.HasValues)
        {
            SetValues(world, stream, commands, group.First, entity);
        }

        if (changes.Removed.Count > 0)
        {
            world.RemoveRange(entity, changes.Removed.Span);
        }
    }

    /// <summary>
    ///     Copies the recorded values of one <see cref="Entity"/> into its <see cref="Chunk"/>.
    /// </summary>
    private void SetValues(World world, CommandStream stream, ReadOnlySpan<Command> commands, int first, Entity entity)
    {
        for (var index = first; index >= 0; index = commands[index].Next)
        {
            ref readonly var command = ref commands[index];
            if (command.Kind is not (CommandKind.Set or CommandKind.Add) || _structuralChanges.IsDropped(command.Type))
            {
                continue;
            }

            CopyValue(world, stream, command, entity);
#if EVENTS
            if (_structuralChanges.Added.Contains(command.Type))
            {
                world.OnComponentAdded(entity, command.Type);
            }
            else
            {
                world.OnComponentSet(entity, command.Type);
            }
#endif
        }
    }

    /// <summary>
    ///     Copies the recorded value of a <see cref="Command"/> into the <see cref="Chunk"/> of its <see cref="Entity"/>.
    /// </summary>
    private static void CopyValue(World world, CommandStream stream, in Command command, Entity entity)
    {
        var data = world.EntityInfo.GetEntityData(entity.Id);
        ref readonly var chunk = ref data.Archetype.GetChunk(data.Slot.ChunkIndex);

        var values = stream.GetValues(command.Type);
        values.Ops.Copy(values.Items, command.ValueIndex, chunk.GetArray(command.Type), data.Slot.Index);
    }

    /// <summary>
    ///     Destroys the <see cref="Entity"/>s last, so commands of every thread can still target them.
    /// </summary>
    private void DestroyEntities(World world)
    {
        foreach (var entity in _destroyed)
        {
            world.Destroy(entity);
        }

        _destroyed.Clear();
    }

    /// <summary>
    ///     Resolves an <see cref="Entity"/> to the real one.
    ///     Entities created by this <see cref="CommandBuffer"/> have a negative id until they are created during <see cref="Playback"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> with a negative or positive id to resolve.</param>
    /// <returns>Its real <see cref="Entity"/>.</returns>
    private Entity Resolve(in Entity entity)
    {
        return entity.Id >= 0 ? entity : _created[IndexOfBuffered(entity)];
    }

    private static int IndexOfBuffered(in Entity entity)
    {
        return -entity.Id - 1;
    }

    /// <summary>
    ///     Removes all recorded commands.
    /// </summary>
    private void Clear()
    {
        foreach (var stream in _streams)
        {
            stream.Clear();
        }

        _createdCount = 0;
    }
}
