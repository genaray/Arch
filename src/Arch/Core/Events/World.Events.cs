using Arch.Core.Events;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

public partial class World
{
    private const int InitialCapacity = 128;
    private readonly List<EntityCreatedHandler> _entityCreatedHandlers = new(InitialCapacity);
    private readonly List<EntityDestroyedHandler> _entityDestroyedHandlers = new(InitialCapacity);
    private Events.Events[] _compEvents = new Events.Events[InitialCapacity];

    /// <summary>
    ///     Adds a delegate to be called when an entity is created.
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    public void SubscribeEntityCreated(EntityCreatedHandler handler)
    {
#if EVENTS
        _entityCreatedHandlers.Add(handler);
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when an entity is destroyed.
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    public void SubscribeEntityDestroyed(EntityDestroyedHandler handler)
    {
#if EVENTS
        _entityDestroyedHandlers.Add(handler);
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <see cref="T"/> is added to an entity.
    ///     <see cref="Add"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentAdded<T>(ComponentAddedHandler handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        events.ComponentAddedHandlers.Add(handler);
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <see cref="T"/> is set on an entity.
    ///     <see cref="Set"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentSet<T>(ComponentSetHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        events.ComponentSetHandlers.Add(handler);
        events.NonGenericComponentSetHandlers.Add((in Entity entity, in object comp) =>
        {
            ref var compGeneric = ref Unsafe.As<object, T>(ref Unsafe.AsRef(comp));
            handler(entity, in compGeneric);
        });
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <see cref="T"/> is removed from an entity.
    ///     <see cref="Remove"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentRemoved<T>(ComponentRemovedHandler handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        events.ComponentRemovedHandlers.Add(handler);
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to entity creation.
    /// </summary>
    /// <param name="entity">The entity that got created.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnEntityCreated(in Entity entity)
    {
#if EVENTS
        for (var i = 0; i < _entityCreatedHandlers.Count; i++)
        {
            _entityCreatedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to entity deletion.
    /// </summary>
    /// <param name="entity">The entity that got destroyed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnEntityDestroyed(in Entity entity)
    {
#if EVENTS
        for (var i = 0; i < _entityDestroyedHandlers.Count; i++)
        {
            _entityDestroyedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component addition of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was added to.</param>
    /// <typeparam name="T">The type of component that got added.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentAdded<T>(in Entity entity)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentAddedHandlers.Count; i++)
        {
            events.ComponentAddedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component addition of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was added to.</param>
    /// <param name="compType">The type of component that got added.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentAdded(in Entity entity, Type compType)
    {
#if EVENTS
        var events = GetEvents(compType);
        if (events == null)
        {
            return;
        }

        for (var i = 0; i < events.ComponentAddedHandlers.Count; i++)
        {
            events.ComponentAddedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component setting of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was set on.</param>
    /// <param name="comp">The component instance that got set.</param>
    /// <typeparam name="T">The type of component that got set.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentSet<T>(in Entity entity, in T comp)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentSetHandlers.Count; i++)
        {
            events.ComponentSetHandlers[i](in entity, in comp);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component setting of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was set on.</param>
    /// <param name="comp">The component instance that got set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentSet(in Entity entity, in object comp)
    {
#if EVENTS
        var events = GetEvents(comp.GetType());
        if (events == null)
        {
            return;
        }

        for (var i = 0; i < events.NonGenericComponentSetHandlers.Count; i++)
        {
            events.NonGenericComponentSetHandlers[i](in entity, in comp);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component removal.
    /// </summary>
    /// <param name="entity">The entity that the component was removed from.</param>
    /// <typeparam name="T">The type of component that got removed.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentRemoved<T>(in Entity entity)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component removal.
    /// </summary>
    /// <param name="entity">The entity that the component was removed from.</param>
    /// <param name="compType">The type of component that got removed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentRemoved(in Entity entity, Type compType)
    {
#if EVENTS
        var events = GetEvents(compType);
        if (events == null)
        {
            return;
        }

        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedHandlers[i](in entity);
        }
#endif
    }

    /// <summary>
    ///     Gets all event handlers for a certain component type.
    /// </summary>
    /// <typeparam name="T">The type of component to get handlers for.</typeparam>
    /// <returns>All handlers for the given component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        return ref Unsafe.As<Events.Events, Events<T>>(ref events);
    }

    /// <summary>
    ///     Gets all event handlers for a certain component type.
    /// </summary>
    /// <param name="compType">The type of component to get handlers for.</param>
    /// <returns>All handlers for the given component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Events.Events? GetEvents(Type compType)
    {
        // Try to get the event from the registry, otherwhise return a null ref since theres none
        if (!EventTypeRegistry.EventIds.TryGetValue(compType, out var index))
        {
            return null;
        }

        if (index >= _compEvents.Length)
        {
            Array.Resize(ref _compEvents, (index * 2) + 1);
        }

        ref var events = ref _compEvents[index];
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        // Better hope it is not null
        events ??= (Events.Events?) Activator.CreateInstance(typeof(Events<>).MakeGenericType(compType))!;

        return events;
    }
}
