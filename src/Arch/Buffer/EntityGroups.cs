using Arch.Core;

namespace Arch.Buffer;

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

    private int Add(in EntityGroup group)
    {
        if (_count == _groups.Length)
        {
            Array.Resize(ref _groups, _count * 2);
        }

        _groups[_count++] = group;
        return _count;
    }

    // Entities created by the command buffer have a negative id, which are mapped to the index -id - 1.
    private ref int SlotOf(int entityId)
    {
        return ref entityId >= 0 ? ref SlotAt(ref _slotsOfExisting, entityId) : ref SlotAt(ref _slotsOfBuffered, -entityId - 1);
    }

    private static ref int SlotAt(ref int[] slots, int index)
    {
        if (index >= slots.Length)
        {
            Array.Resize(ref slots, Math.Max(index + 1, slots.Length * 2));
        }

        return ref slots[index];
    }
}
