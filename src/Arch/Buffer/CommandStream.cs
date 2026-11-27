using System.Runtime.CompilerServices;
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

/// <summary>
///     The <see cref="CommandStream"/> class
///     holds the commands recorded by one thread, in the order they were recorded.
///     Recording is a plain append, the commands are only related to each other during <see cref="CommandBuffer.Playback"/>.
///     Not thread-safe, a <see cref="CommandBuffer"/> gives every thread its own.
/// </summary>
internal sealed class CommandStream
{
    private readonly int _initialCapacity;

    /// <summary>
    ///     The recorded commands, only the first <see cref="_commandCount"/> are in use.
    /// </summary>
    private Command[] _commands;
    private int _commandCount;
    private bool _containsOnlySets = true;

    /// <summary>
    ///     The values of the recorded commands, at the id of their component type, null as long as nothing of that type was recorded.
    /// </summary>
    private ComponentValues?[] _values = [];

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandStream"/> class.
    /// </summary>
    /// <param name="owner">An object that identifies the <see cref="CommandBuffer"/> the stream belongs to.</param>
    /// <param name="initialCapacity">The amount of commands and values per component type it can hold without growing.</param>
    public CommandStream(object owner, int initialCapacity)
    {
        Owner = owner;
        _initialCapacity = Math.Max(initialCapacity, 1);
        _commands = new Command[_initialCapacity];
        Creates = new List<CreateCommand>();
    }

    /// <summary>
    ///     Identifies the <see cref="CommandBuffer"/> the stream belongs to.
    /// </summary>
    public object Owner { get; }

    /// <summary>
    ///     The amount of recorded commands, including the <see cref="Creates"/>.
    /// </summary>
    public int Count => _commandCount + Creates.Count;

    /// <summary>
    ///     The recorded commands, without the <see cref="Creates"/>.
    /// </summary>
    public Span<Command> Commands
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _commands.AsSpan(0, _commandCount);
    }

    /// <summary>
    ///     Whether all recorded <see cref="Commands"/> are <see cref="CommandKind.Set"/>, which can be played back one by one.
    /// </summary>
    public bool ContainsOnlySets => _containsOnlySets;

    /// <summary>
    ///     The recorded creations of <see cref="Entity"/>s.
    /// </summary>
    public List<CreateCommand> Creates { get; }

    /// <summary>
    ///     Records the creation of an <see cref="Entity"/>.
    /// </summary>
    public void RecordCreate(in Entity entity, ComponentType[] types)
    {
        Creates.Add(new CreateCommand(entity, types));
    }

    /// <summary>
    ///     Records a <see cref="CommandKind.Set"/> or <see cref="CommandKind.Add"/> together with the value of its component.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RecordValue<T>(CommandKind kind, in Entity entity, in T? component)
    {
        var type = Component<T>.ComponentType;
        var valueIndex = GetValues(type).Add(in component);
        Record(new Command(kind, entity, type, valueIndex));
    }

    /// <summary>
    ///     Records a <see cref="CommandKind.Remove"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RecordRemove(in Entity entity, in ComponentType type)
    {
        Record(new Command(CommandKind.Remove, entity, type, -1));
    }

    /// <summary>
    ///     Records a <see cref="CommandKind.Destroy"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RecordDestroy(in Entity entity)
    {
        Record(new Command(CommandKind.Destroy, entity, default, -1));
    }

    /// <summary>
    ///     Returns the <see cref="ComponentValues"/> of a component type, and creates them if nothing of that type was recorded yet.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ComponentValues GetValues(in ComponentType type)
    {
        var values = (uint)type.Id < (uint)_values.Length ? _values[type.Id] : null;
        return values ?? AddValues(type);
    }

    /// <summary>
    ///     Removes all recorded commands and values, the capacity stays.
    /// </summary>
    public void Clear()
    {
        _commandCount = 0;
        _containsOnlySets = true;
        Creates.Clear();

        foreach (var values in _values)
        {
            values?.Clear();
        }
    }

    /// <summary>
    ///     Appends a command and grows the array if it is full.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Record(in Command command)
    {
        if (_commandCount == _commands.Length)
        {
            Array.Resize(ref _commands, _commandCount * 2);
        }

        _commands[_commandCount++] = command;
        _containsOnlySets &= command.Kind == CommandKind.Set;
    }

    /// <summary>
    ///     Creates the <see cref="ComponentValues"/> of a component type and makes room for its id.
    /// </summary>
    private ComponentValues AddValues(in ComponentType type)
    {
        if (type.Id >= _values.Length)
        {
            Array.Resize(ref _values, Math.Max(type.Id + 1, _values.Length * 2));
        }

        return _values[type.Id] = new ComponentValues(type, _initialCapacity);
    }

    /// <summary>
    ///     Drops everything the stream holds, so a thread that remembers it does not keep the memory of a disposed <see cref="CommandBuffer"/> alive.
    /// </summary>
    public void Release()
    {
        _commands = [];
        _commandCount = 0;
        _values = [];
        Creates.Clear();
        Creates.TrimExcess();
    }
}

/// <summary>
///     The <see cref="ComponentValues"/> class
///     stores the values of all recorded commands for one component type, one after another.
/// </summary>
internal sealed class ComponentValues
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentValues"/> class.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> of the stored values.</param>
    /// <param name="capacity">The amount of values it can hold without growing.</param>
    public ComponentValues(ComponentType type, int capacity)
    {
        Type = type;
        Ops = ComponentOps.Get(type);
        Items = Ops.Create(Math.Max(capacity, 1));
    }

    /// <summary>
    ///     The <see cref="ComponentType"/> of the stored values.
    /// </summary>
    public ComponentType Type { get; }

    /// <summary>
    ///     The <see cref="ComponentOps"/> to copy the values into a <see cref="Chunk"/>.
    /// </summary>
    public ComponentOps Ops { get; }

    /// <summary>
    ///     The array of values, its first <see cref="Count"/> items are in use.
    /// </summary>
    public Array Items { get; private set; }

    /// <summary>
    ///     The amount of stored values.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Stores a value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The component type, must be the type this instance was created for.</typeparam>
    /// <returns>Its index.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add<T>(in T value)
    {
        if (Count == Items.Length)
        {
            Grow();
        }

        Unsafe.As<T[]>(Items)[Count] = value;
        return Count++;
    }

    /// <summary>
    ///     Removes all values and clears them, since they might hold references that must not outlive the recorded commands.
    /// </summary>
    public void Clear()
    {
        Array.Clear(Items, 0, Count);
        Count = 0;
    }

    /// <summary>
    ///     Doubles the capacity and keeps the stored values.
    /// </summary>
    private void Grow()
    {
        var larger = Ops.Create(Items.Length * 2);
        Array.Copy(Items, larger, Count);
        Items = larger;
    }
}
