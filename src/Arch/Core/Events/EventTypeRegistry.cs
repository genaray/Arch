using System.Collections.Concurrent;
using System.Threading;
using Arch.Core.Utils;

namespace Arch.Core.Events;

/// <summary>
///     The <see cref="EventTypeRegistry"/> class
///     acts as a static class storing and managing the <see cref="EventType{T}"/>s.
/// </summary>
internal static class EventTypeRegistry
{
    /// <summary>
    ///     The TypeId of the next event.
    /// </summary>
    internal static int NextEventTypeId = -1;

    /// <summary>
    ///     A <see cref="Dictionary{TKey,TValue}"/> mapping all EventTypes to their id.
    /// </summary>
    internal static readonly ConcurrentDictionary<ComponentType, int> EventIds = new();
}

/// <summary>
///     The <see cref="EventType{T}"/> class
///     acts as a compile time static class to store meta data for an registered event.
/// </summary>
/// <typeparam name="T"></typeparam>
internal static class EventType<T>
{
    /// <summary>
    ///     The Id of this <see cref="EventType{T}"/>.
    /// </summary>
    // ReSharper disable once StaticMemberInGenericType
    internal static readonly int Id;

    /// <summary>
    ///     Creates a new instance of the <see cref="EventType{T}"/> class.
    /// </summary>
    static EventType()
    {
        Id = Interlocked.Increment(ref EventTypeRegistry.NextEventTypeId);
        EventTypeRegistry.EventIds.TryAdd(typeof(T), Id);
    }
}
