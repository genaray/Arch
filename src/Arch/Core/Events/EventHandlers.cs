#if EVENTS
namespace Arch.Core;

public delegate void EntityCreatedHandler(in Entity entity);

public delegate void EntityDestroyedHandler(in Entity entity);

public delegate void ComponentAddedHandler(in Entity entity);

public delegate void ComponentSetHandler<T>(in Entity entity, in T comp);

internal delegate void ComponentSetHandler(in Entity entity, in object comp);

public delegate void ComponentRemovedHandler(in Entity entity);
#endif
