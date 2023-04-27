#if EVENTS
namespace Arch.Core;

public partial class World
{
    private const int StartingSize = 128;
    private readonly List<EntityCreatedHandler> _entityCreatedHandlers = new(StartingSize);
    private readonly List<EntityDestroyedHandler> _entityDestroyedHandlers = new(StartingSize);
    private Events[] _compEvents = new Events[StartingSize];

    public void SubscribeEntityCreated(EntityCreatedHandler handler)
    {
        _entityCreatedHandlers.Add(handler);
    }

    public void SubscribeEntityDestroyed(EntityDestroyedHandler handler)
    {
        _entityDestroyedHandlers.Add(handler);
    }

    public void SubscribeComponentAdded<T>(ComponentAddedHandler handler)
    {
        ref readonly var events = ref GetEvents<T>();
        events.ComponentAddedHandlers.Add(handler);
    }

    public void SubscribeComponentSet<T>(ComponentSetHandler<T> handler)
    {
        ref readonly var events = ref GetEvents<T>();
        events.ComponentSetHandlers.Add(handler);
        events.NonGenericComponentSetHandlers.Add((in Entity entity, in object comp) =>
        {
            ref var compGeneric = ref Unsafe.As<object, T>(ref Unsafe.AsRef(comp));
            handler(entity, in compGeneric);
        });
    }

    public void SubscribeComponentRemoved<T>(ComponentRemovedHandler handler)
    {
        ref readonly var events = ref GetEvents<T>();
        events.ComponentRemovedHandlers.Add(handler);
    }

    internal void OnEntityCreated(in Entity entity)
    {
        for (var i = 0; i < _entityCreatedHandlers.Count; i++)
        {
            _entityCreatedHandlers[i](in entity);
        }
    }

    internal void OnEntityDestroyed(in Entity entity)
    {
        for (var i = 0; i < _entityDestroyedHandlers.Count; i++)
        {
            _entityDestroyedHandlers[i](in entity);
        }
    }

    internal void OnComponentAdded<T>(in Entity entity)
    {
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentAddedHandlers.Count; i++)
        {
            events.ComponentAddedHandlers[i](in entity);
        }
    }

    internal void OnComponentAdded(in Entity entity, Type compType)
    {
        ref readonly var events = ref GetEvents(compType);
        for (var i = 0; i < events.ComponentAddedHandlers.Count; i++)
        {
            events.ComponentAddedHandlers[i](in entity);
        }
    }

    internal void OnComponentSet<T>(in Entity entity, in T comp)
    {
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentSetHandlers.Count; i++)
        {
            events.ComponentSetHandlers[i](in entity, in comp);
        }
    }

    internal void OnComponentSet(in Entity entity, in object comp)
    {
        ref readonly var events = ref GetEvents(comp.GetType());
        for (var i = 0; i < events.NonGenericComponentSetHandlers.Count; i++)
        {
            events.NonGenericComponentSetHandlers[i](in entity, in comp);
        }
    }

    internal void OnComponentRemoved<T>(in Entity entity)
    {
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedHandlers[i](in entity);
        }
    }

    internal void OnComponentRemoved(in Entity entity, Type compType)
    {
        ref readonly var events = ref GetEvents(compType);
        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedHandlers[i](in entity);
        }
    }

    private ref readonly Events<T> GetEvents<T>()
    {
        var index = EventType<T>.Id;
        if (index >= _compEvents.Length)
        {
            Array.Resize(ref _compEvents, (index * 2) + 1);
        }

        ref var events = ref _compEvents[index];
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        events ??= new Events<T>();

        return ref Unsafe.As<Events, Events<T>>(ref events);
    }

    private ref readonly Events GetEvents(Type compType)
    {
        var index = EventType.EventIds[compType];
        if (index >= _compEvents.Length)
        {
            Array.Resize(ref _compEvents, (index * 2) + 1);
        }

        ref var events = ref _compEvents[index];
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        // Better hope it is not null
        events ??= (Events?) Activator.CreateInstance(typeof(Events<>).MakeGenericType(compType))!;

        return ref events;
    }
}
#endif
