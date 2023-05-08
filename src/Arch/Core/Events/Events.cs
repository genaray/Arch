#if EVENTS
namespace Arch.Core;

internal class Events
{
    internal readonly List<ComponentAddedHandler> ComponentAddedHandlers = new();
    internal readonly List<ComponentSetHandler> NonGenericComponentSetHandlers = new();
    internal readonly List<ComponentRemovedHandler> ComponentRemovedHandlers = new();
}

internal class Events<T> : Events
{
    internal readonly List<ComponentSetHandler<T>> ComponentSetHandlers = new();
}
#endif
