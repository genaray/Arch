using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="CommandKind"/> enum
///     describes what a <see cref="Command"/> does to its <see cref="Entity"/>.
/// </summary>
internal enum CommandKind : byte
{
    /// <summary>
    ///     Overwrites the value of a component the <see cref="Entity"/> has.
    /// </summary>
    Set,

    /// <summary>
    ///     Adds a component with a value to the <see cref="Entity"/>.
    /// </summary>
    Add,

    /// <summary>
    ///     Removes a component from the <see cref="Entity"/>.
    /// </summary>
    Remove,

    /// <summary>
    ///     Destroys the <see cref="Entity"/>.
    /// </summary>
    Destroy
}

/// <summary>
///     The <see cref="Command"/> struct
///     is one recorded operation on an <see cref="Entity"/>.
/// </summary>
internal struct Command
{
    /// <summary>
    ///     What the command does.
    /// </summary>
    public readonly CommandKind Kind;

    /// <summary>
    ///     The <see cref="Entity"/> it targets, a negative id if it was created by the same <see cref="CommandBuffer"/>.
    /// </summary>
    public readonly Entity Entity;

    /// <summary>
    ///     The component it adds, sets or removes, not used by <see cref="CommandKind.Destroy"/>.
    /// </summary>
    public readonly ComponentType Type;

    /// <summary>
    ///     The index of its value in the <see cref="ComponentValues"/> of the <see cref="Type"/>, only used by <see cref="CommandKind.Set"/> and <see cref="CommandKind.Add"/>.
    /// </summary>
    public readonly int ValueIndex;

    /// <summary>
    ///     The index of the next command for the same <see cref="Entity"/>, or <c>-1</c>. Linked by <see cref="EntityGroups.Build"/> during <see cref="CommandBuffer.Playback"/>.
    /// </summary>
    public int Next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Command"/> struct.
    /// </summary>
    /// <param name="kind">What the command does.</param>
    /// <param name="entity">The <see cref="Entity"/> it targets.</param>
    /// <param name="type">The component it adds, sets or removes.</param>
    /// <param name="valueIndex">The index of its value in the <see cref="ComponentValues"/>.</param>
    public Command(CommandKind kind, in Entity entity, in ComponentType type, int valueIndex)
    {
        Kind = kind;
        Entity = entity;
        Type = type;
        ValueIndex = valueIndex;
        Next = -1;
    }
}

/// <summary>
///     The <see cref="CreateCommand"/> struct
///     is the recorded creation of an <see cref="Entity"/>.
/// </summary>
internal readonly struct CreateCommand
{
    /// <summary>
    ///     The buffered <see cref="Entity"/> returned to the caller, it has a negative id.
    /// </summary>
    public readonly Entity Entity;

    /// <summary>
    ///     The component structure of the <see cref="Entity"/> to create.
    /// </summary>
    public readonly ComponentType[] Types;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateCommand"/> struct.
    /// </summary>
    /// <param name="entity">The buffered <see cref="Entity"/>.</param>
    /// <param name="types">Its component structure.</param>
    public CreateCommand(in Entity entity, ComponentType[] types)
    {
        Entity = entity;
        Types = types;
    }
}
