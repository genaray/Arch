using Arch.Core.Events;
using Arch.Core.Extensions;
using Arch.Core.Utils;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

public partial class World
{
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
        _entityCreatedHandlers.Add(handler);
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called after an entity is destroyed.
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
    public void SubscribeComponentAdded<T>(ComponentAddedHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        events.ComponentAddedGenericHandlers.Add(handler);
        events.ComponentAddedHandlers.Add((in Entity entity) =>
        {
            ref var compGeneric = ref entity.Get<T>();
            handler(entity, ref compGeneric);
        });
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
        events.ComponentSetGenericHandlers.Add(handler);
        events.ComponentSetHandlers.Add((in Entity entity) =>
        {
            ref var compGeneric = ref entity.Get<T>();
            handler(entity, ref compGeneric);
        });
#endif
    }

    /// <summary>
    ///     Adds a delegate to be called when a component of type <see cref="T"/> is removed from an entity.
    ///     <see cref="Remove"/>
    /// </summary>
    /// <param name="handler">The delegate to call.</param>
    /// <typeparam name="T">The component type.</typeparam>
    public void SubscribeComponentRemoved<T>(ComponentRemovedHandler<T> handler)
    {
#if EVENTS
        ref readonly var events = ref GetEvents<T>();
        events.ComponentRemovedGenericHandlers.Add(handler);
        events.ComponentRemovedHandlers.Add((in Entity entity) =>
        {
            ref var compGeneric = ref entity.Get<T>();
            handler(entity, ref compGeneric);
        });
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
    public void OnEntityDestroyed(Entity entity)
    {
#if EVENTS
        for (var i = 0; i < _entityDestroyedHandlers.Count; i++)
        {
            _entityDestroyedHandlers[i](in entity);
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
        for (var i = 0; i < events.ComponentAddedHandlers.Count; i++)
        {
            events.ComponentAddedGenericHandlers[i](in entity, ref added);
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
        for (var i = 0; i < events.ComponentSetGenericHandlers.Count; i++)
        {
            events.ComponentSetGenericHandlers[i](in entity, ref set);
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
        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedGenericHandlers[i](entity, ref removed);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void OnComponentSet(Entity entity, object comp)
    {
#if EVENTS
        var events = GetEvents(comp.GetType());
        if (events == null)
        {
            return;
        }

        for (var i = 0; i < events.ComponentSetHandlers.Count; i++)
        {
            events.ComponentSetHandlers[i](in entity);
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


        for (var i = 0; i < events.ComponentRemovedHandlers.Count; i++)
        {
            events.ComponentRemovedHandlers[i](entity);
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
        foreach(ref var chunk in archetype)
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
        foreach(ref var chunk in archetype)
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
        if (index >= _compEvents.Length)
        {
            Array.Resize(ref _compEvents, (index * 2) + 1);
        }

        ref var events = ref _compEvents[index];
        // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
        events ??= new Events<T>();

        return ref Unsafe.As<Events.Events, Events<T>>(ref events);
    }

    /// TODO : Remove creating by activator. Instead we should probably keep two lists. One for object based calls, one for generics.
    /// <summary>
    ///     Gets all event handlers for a certain component type.
    /// </summary>
    /// <param name="compType">The type of component to get handlers for.</param>
    /// <returns>All handlers for the given component type.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Events.Events? GetEvents(ComponentType compType)
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
