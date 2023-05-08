namespace Arch.Core.Events;

/// <summary>
///     A delegate called once a new <see cref="Entity"/> was created.
/// </summary>
public delegate void EntityCreatedHandler(in Entity entity);

/// <summary>
///     A delegate called once a <see cref="Entity"/> was destroyed.
/// </summary>
public delegate void EntityDestroyedHandler(in Entity entity);

/// <summary>
///     A delegate called once a componnet was added for a specific <see cref="Entity"/>.
/// </summary>
public delegate void ComponentAddedHandler(in Entity entity);

/// <summary>
///     A delegate called once a component was set for a specific <see cref="Entity"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public delegate void ComponentSetHandler<T>(in Entity entity, in T comp);

/// <summary>
///     A delegate called once a component was set for a specific <see cref="Entity"/>.
/// </summary>
internal delegate void ComponentSetHandler(in Entity entity, in object comp);

/// <summary>
///     A delegate called once a component was removed from a specific <see cref="Entity"/>.
/// </summary>
public delegate void ComponentRemovedHandler(in Entity entity);
