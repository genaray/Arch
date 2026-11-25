using System;
using System.Collections.Generic;
using System.Threading;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="CommandBuffer"/> class
///     stores operation to <see cref="Entity"/>'s between to play and implement them at a later time in the <see cref="World"/>.
/// </summary>
/// <remarks>
///     Every thread records into its own stream of commands without any locking.
///     During <see cref="Playback"/> the commands are applied in phases: all creates, then for every stream all adds, sets and removes, then all destroys.
///     The commands one thread recorded for the same <see cref="Entity"/> are applied together, as a single structural change.
///     A component that is added and removed again in the same <see cref="CommandBuffer"/> does not change the <see cref="Entity"/> and raises no events.
/// </remarks>
public sealed partial class CommandBuffer : IDisposable
{
    private readonly bool _threadSafe;
    private readonly int _initialCapacity;

    /// <summary>
    ///     All streams ever created, so commands of threads that finished are still played back. Guarded by itself.
    /// </summary>
    private readonly List<CommandStream> _streams = new();

    /// <summary>
    ///     The stream of every thread, null if the <see cref="CommandBuffer"/> is not thread-safe.
    /// </summary>
    private readonly ThreadLocal<CommandStream>? _threadStreams;

    /// <summary>
    ///     The only stream if the <see cref="CommandBuffer"/> is not thread-safe, which saves looking it up per thread.
    /// </summary>
    private readonly CommandStream? _singleStream;

    /// <summary>
    ///     Identifies this <see cref="CommandBuffer"/> in <see cref="_lastStream"/>, without keeping the whole <see cref="CommandBuffer"/> alive.
    /// </summary>
    private readonly object _owner = new();

    /// <summary>
    ///     The stream the calling thread used last, whichever <see cref="CommandBuffer"/> it belongs to.
    ///     Finding the stream of a thread this way is faster than <see cref="ThreadLocal{T}.Value"/>, as long as a thread records into one <see cref="CommandBuffer"/> at a time.
    /// </summary>
    [ThreadStatic]
    private static CommandStream? _lastStream;

    /// <summary>
    ///     The amount of <see cref="Entity"/>s created by this <see cref="CommandBuffer"/> since the last <see cref="Playback"/> that disposed its commands.
    /// </summary>
    private int _createdCount;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandBuffer"/> class
    ///     with the specified <see cref="Core.World"/> and an optional <paramref name="initialCapacity"/> (default: 128).
    /// </summary>
    /// <param name="initialCapacity">The initial capacity, of every thread that records commands.</param>
    /// <param name="threadSafe">
    ///     If true (default) the commands can be recorded from several threads at once, for example from a parallel query.
    ///     If false recording is a bit faster, but must only happen from one thread at a time.
    /// </param>
    public CommandBuffer(int initialCapacity = 128, bool threadSafe = true)
    {
        _threadSafe = threadSafe;
        _initialCapacity = initialCapacity;

        if (threadSafe)
        {
            _threadStreams = new ThreadLocal<CommandStream>(CreateStream);
        }
        else
        {
            _singleStream = CreateStream();
        }
    }

    /// <summary>
    ///     Gets the amount of commands recorded by all threads, since the last <see cref="Playback"/> that disposed them.
    /// </summary>
    public int Size
    {
        get
        {
            lock (_streams)
            {
                var size = 0;
                foreach (var stream in _streams)
                {
                    size += stream.Count;
                }

                return size;
            }
        }
    }

    /// <summary>
    ///     The stream the calling thread records into.
    /// </summary>
    private CommandStream CurrentStream
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (_singleStream is not null)
            {
                return _singleStream;
            }

            var stream = _lastStream;
            return stream is not null && ReferenceEquals(stream.Owner, _owner) ? stream : FindThreadStream();
        }
    }

    /// <summary>
    ///     Records a Create operation for an <see cref="Entity"/> based on its component structure.
    ///     Will be created during <see cref="Playback"/>.
    /// </summary>
    /// <param name="types">The <see cref="Entity"/>'s component structure/<see cref="Archetype"/>.</param>
    /// <returns>The buffered <see cref="Entity"/>, with a negative id. It can be used for further commands of this <see cref="CommandBuffer"/>.</returns>
    public Entity Create(ComponentType[] types)
    {
        var index = _threadSafe ? Interlocked.Increment(ref _createdCount) - 1 : _createdCount++;
        var entity = new Entity(-(index + 1), -1);

        CurrentStream.RecordCreate(entity, types);
        return entity;
    }

    /// <summary>
    ///     Record a Destroy operation for an (buffered) <see cref="Entity"/>.
    ///     Will be destroyed during <see cref="Playback"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to destroy.</param>
    public void Destroy(in Entity entity)
    {
        CurrentStream.RecordDestroy(entity);
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
        CurrentStream.RecordValue(CommandKind.Set, entity, in component);
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
        CurrentStream.RecordValue(CommandKind.Add, entity, in component);
    }

    /// <summary>
    ///     Records a remove operation for an (buffered) <see cref="Entity"/>.
    ///     Will be removed during <see cref="Playback"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    public void Remove<T>(in Entity entity)
    {
        CurrentStream.RecordRemove(entity, Component<T>.ComponentType);
    }

    /// <summary>
    ///     Disposes the <see cref="CommandBuffer"/>.
    /// </summary>
    public void Dispose()
    {
        _threadStreams?.Dispose();
        lock (_streams)
        {
            foreach (var stream in _streams)
            {
                stream.Release();
            }

            _streams.Clear();
        }

        _structuralChanges.Dispose();
        _destroyed.Dispose();
        GC.SuppressFinalize(this);
    }

    private CommandStream FindThreadStream()
    {
        var stream = _threadStreams!.Value!;
        _lastStream = stream;
        return stream;
    }

    /// <summary>
    ///     Creates the stream of a thread that records for the first time, and registers it.
    /// </summary>
    private CommandStream CreateStream()
    {
        var stream = new CommandStream(_owner, _initialCapacity);
        lock (_streams)
        {
            _streams.Add(stream);
        }

        return stream;
    }
}
