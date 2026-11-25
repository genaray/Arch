using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="CommandStream"/> class
///     holds the commands recorded by one thread, in the order they were recorded.
///     Recording is a plain append, the commands are only related to each other during <see cref="CommandBuffer.Playback"/>.
///     Not thread-safe, a <see cref="CommandBuffer"/> gives every thread its own.
/// </summary>
internal sealed class CommandStream
{
    private readonly int _initialCapacity;
    private Command[] _commands;
    private int _commandCount;
    private bool _containsOnlySets = true;
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
