using Arch.Core.Events;
using Arch.Core.Extensions;
using Arch.Core.Utils;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

public partial class World
{
    // A note on multithreading:
    // This area is the trickiest part of World in terms of thread-safety.
    // It is currently thread-safe, but it relies on an important fact: No list of handlers can ever shrink or be disposed.
    // i.e. no handlers can ever be unsubscribed.
    // So don't try to write any unsubscribe methods without refactoring the thread-safety!

    /// <summary>
    ///     The initial capacity for the <see cref="_compEvents"/> array.
    /// </summary>
    private const int InitialCapacity = 128;

    /// <summary>
    ///     All <see cref="EntityCreatedHandler"/>s in a <see cref="List{T}"/> which will be called upon entity creation.
    /// </summary>
    private readonly List<EntityCreatedHandler> _entityCreatedHandlers = new(InitialCapacity);

    /// <summary>
    ///     All <see cref="EntityDestroyedHandler"/>s in a <see cref="List{T}"/> which will be called after entity destruction.
    /// </summary>
    private readonly List<EntityDestroyedHandler> _entityDestroyedHandlers = new(InitialCapacity);

    /// <summary>
    ///     All <see cref="Events"/> in an array which will be acessed for add, remove or set operations.
    /// </summary>
    private Events.Events[] _compEvents = new Events.Events[InitialCapacity];

    /// <summary>
    ///     Adds a delegate to be called when an entity is created.
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    public void SubscribeEntityCreated(EntityCreatedHandler handler)
    {
#if EVENTS
        lock (_entityCreatedHandlers)
        {
            _entityCreatedHandlers.Add(handler);
        }
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called after an entity is destroyed.
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    public void SubscribeEntityDestroyed(EntityDestroyedHandler handler)
    {
#if EVENTS
        lock (_entityDestroyedHandlers)
        {
            _entityDestroyedHandlers.Add(handler);
        }
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <typeparamref name="T"/> is added to an entity.
    ///     <see cref="Add"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentAdded<T>(ComponentAddedHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        lock (events.ComponentAddedGenericHandlers)
        {
            events.ComponentAddedGenericHandlers.Add(handler);
        }

        lock (events.ComponentAddedHandlers)
        {
            events.ComponentAddedHandlers.Add((in Entity entity) =>
            {
                ref var compGeneric = ref entity.Get<T>();
                handler(entity, ref compGeneric);
            });
        }
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <typeparamref name="T"/> is set on an entity.
    ///     <see cref="Set"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentSet<T>(ComponentSetHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        lock (events.ComponentSetGenericHandlers)
        {
            events.ComponentSetGenericHandlers.Add(handler);
        }

        lock (events.ComponentSetHandlers)
        {
            events.ComponentSetHandlers.Add((in Entity entity) =>
            {
                ref var compGeneric = ref entity.Get<T>();
                handler(entity, ref compGeneric);
            });
        }
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <typeparamref name="T"/> is removed from an entity.
    ///     <see cref="Remove"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentRemoved<T>(ComponentRemovedHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        lock (events.ComponentRemovedGenericHandlers)
        {
            events.ComponentRemovedGenericHandlers.Add(handler);
        }

        lock (events.ComponentRemovedHandlers)
        {
            events.ComponentRemovedHandlers.Add((in Entity entity) =>
            {
                ref var compGeneric = ref entity.Get<T>();
                handler(entity, ref compGeneric);
            });
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to entity creation.
    /// </summary>
    /// <param name="entity">The entity that got created.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnEntityCreated(Entity entity)
    {
#if EVENTS
        int count;
        lock (_entityCreatedHandlers)
        {
            count = _entityCreatedHandlers.Count;
        }
        // The thread-safety here relies on the fact that handlers can NEVER be unsubscribed.
        // We still have to lock to access the handler, because what if someone is adding in the middle of our access?
        for (var i = 0; i < count; i++)
        {
            EntityCreatedHandler handler;
            lock (_entityCreatedHandlers)
            {
                handler = _entityCreatedHandlers[i];
            }

            handler.Invoke(in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to entity deletion.
    /// </summary>
    /// <param name="entity">The entity that got destroyed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnEntityDestroyed(Entity entity)
    {
#if EVENTS
        int count;
        lock (_entityDestroyedHandlers)
        {
            count = _entityDestroyedHandlers.Count;
        }

        for (var i = 0; i < _entityDestroyedHandlers.Count; i++)
        {
            EntityDestroyedHandler handler;
            lock (_entityDestroyedHandlers)
            {
                handler = _entityDestroyedHandlers[i];
            }

            handler.Invoke(in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all generic handlers subscribed to component addition of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was added to.</param>
    /// <typeparam name="T">The type of component that got added.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentAdded<T>(Entity entity)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        ref var added = ref entity.Get<T>();

        int count;
        lock (events.ComponentAddedGenericHandlers)
        {
            count = events.ComponentAddedGenericHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentAddedHandler<T> handler;
            lock (events.ComponentAddedGenericHandlers)
            {
                handler = events.ComponentAddedGenericHandlers[i];
            }

            handler(in entity, ref added);
        }
#endif
    }

    /// <summary>
    ///     Calls all generic handlers subscribed to component setting of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was set on.</param>
    /// <typeparam name="T">The type of component that got set.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentSet<T>(Entity entity)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        ref var set = ref entity.Get<T>();

        int count;
        lock (events.ComponentSetGenericHandlers)
        {
            count = events.ComponentSetGenericHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentSetHandler<T> handler;
            lock (events.ComponentSetGenericHandlers)
            {
                handler = events.ComponentSetGenericHandlers[i];
            }

            handler(in entity, ref set);
        }
#endif
    }

    /// <summary>
    ///     Calls all generic handlers subscribed to component removal.
    /// </summary>
    /// <param name="entity">The entity that the component was removed from.</param>
    /// <typeparam name="T">The type of component that got removed.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentRemoved<T>(Entity entity)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        ref var removed = ref entity.Get<T>();

        int count;
        lock (events.ComponentRemovedGenericHandlers)
        {
            count = events.ComponentRemovedGenericHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentRemovedHandler<T> handler;
            lock (events.ComponentRemovedGenericHandlers)
            {
                handler = events.ComponentRemovedGenericHandlers[i];
            }

            handler(in entity, ref removed);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component addition of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was added to.</param>
    /// <param name="compType">The type of component that got added.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentAdded(Entity entity, ComponentType compType)
    {
#if EVENTS
        var events = GetEvents(compType);
        if (events == null)
        {
            return;
        }

        int count;
        lock (events.ComponentAddedHandlers)
        {
            count = events.ComponentAddedHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentAddedHandler handler;
            lock (events.ComponentAddedHandlers)
            {
                handler = events.ComponentAddedHandlers[i];
            }

            handler(in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component setting of this type.
    /// </summary>
    /// <param name="entity">The entity that the component was set on.</param>
    /// <param name="comp">The component instance that got set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentSet(Entity entity, object comp)
    {
#if EVENTS
        var events = GetEvents(comp.GetType());
        if (events == null)
        {
            return;
        }

        int count;
        lock (events.ComponentSetHandlers)
        {
            count = events.ComponentSetHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentSetHandler handler;
            lock (events.ComponentSetHandlers)
            {
                handler = events.ComponentSetHandlers[i];
            }

            handler(in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component removal.
    /// </summary>
    /// <param name="entity">The entity that the component was removed from.</param>
    /// <param name="compType">The type of component that got removed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentRemoved(Entity entity, ComponentType compType)
    {
#if EVENTS
        var events = GetEvents(compType);
        if (events == null)
        {
            return;
        }

        int count;
        lock (events.ComponentRemovedHandlers)
        {
            count = events.ComponentRemovedHandlers.Count;
        }

        for (var i = 0; i < count; i++)
        {
            ComponentRemovedHandler handler;
            lock (events.ComponentRemovedHandlers)
            {
                handler = events.ComponentRemovedHandlers[i];
            }

            handler(in entity);
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component addition of this type for entities in a archetype range.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void OnComponentAdded<T>(Archetype archetype)
    {
#if EVENTS
        // Set the added component, start from the last slot and move down
        foreach (ref var chunk in archetype)
        {
            ref var firstEntity = ref chunk.Entity(0);
            foreach (var index in chunk)
            {
                var entity = Unsafe.Add(ref firstEntity, index);
                OnComponentAdded<T>(entity);
            }
        }
#endif
    }

    /// <summary>
    ///     Calls all handlers subscribed to component removal of this type for entities in a archetype range.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void OnComponentRemoved<T>(Archetype archetype)
    {
#if EVENTS
        // Set the added component, start from the last slot and move down
        foreach (ref var chunk in archetype)
        {
            ref var firstEntity = ref chunk.Entity(0);
            foreach (var index in chunk)
            {
                var entity = Unsafe.Add(ref firstEntity, index);
                OnComponentRemoved<T>(entity);
            }
        }
#endif
    }

    /// <summary>
    ///     Gets all generic event handlers for a certain component type.
    /// </summary>
    /// <typeparam name="T">The type of component to get handlers for.</typeparam>
    /// <returns>All handlers for the given component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ref readonly Events<T> GetEvents<T>()
    {
        var index = EventType<T>.Id;
        lock (_compEvents)
        {
            if (index >= _compEvents.Length)
            {
                Array.Resize(ref _compEvents, (index * 2) + 1);
            }
            // This must be in lock so we get a current(ish) reference
            ref var events = ref _compEvents[index];
            // This must be in lock along with Resize in case we resize in a different thread before assigning a new Events
            // to the old array.
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            events ??= new Events<T>();

            // Thread safety: Here, even though it's a reference, if the array was/will be resized, our Events will still be valid.
            // So any callers can use it in peace, knowing that it won't get GC'd out of existence, until they're done.
            // Of note, once created, an Events<T> can never change index or go invalid, just get copied to new arrays.
            return ref Unsafe.As<Events.Events, Events<T>>(ref events);
        }
    }

    /// TODO : Remove creating by activator. Instead we should probably keep two lists. One for object based calls, one for generics.
    /// <summary>
    ///     Gets all event handlers for a certain component type.
    /// </summary>
    /// <param name="compType">The type of component to get handlers for.</param>
    /// <returns>All handlers for the given component type, or null if there are none.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Events.Events? GetEvents(ComponentType compType)
    {
        // Try to get the event from the registry, otherwhise return a null ref since there's none
        // This is thread-safe due to ConcurrentDictionary.
        if (!EventTypeRegistry.EventIds.TryGetValue(compType, out var index))
        {
            return null;
        }

        lock (_compEvents)
        {
            if (index >= _compEvents.Length)
            {
                Array.Resize(ref _compEvents, (index * 2) + 1);
            }

            ref var events = ref _compEvents[index];
            // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
            // Better hope it is not null
            events ??= (Events.Events?)Activator.CreateInstance(typeof(Events<>).MakeGenericType(compType))!;
            return events;
        }
    }
}
