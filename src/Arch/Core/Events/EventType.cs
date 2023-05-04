#if EVENTS
using System.Collections.Concurrent;
using System.Threading;

namespace Arch.Core;

internal static class EventType
{
    internal static int NextEventTypeId = -1;
    internal static readonly ConcurrentDictionary<Type, int> EventIds = new();
}

// TODO merge this and ComponentType
internal static class EventType<T>
{
    static EventType()
    {
        Id = Interlocked.Increment(ref EventType.NextEventTypeId);
        EventType.EventIds.TryAdd(typeof(T), Id);
    }

    // ReSharper disable once StaticMemberInGenericType
    internal static readonly int Id;

}
#endif
