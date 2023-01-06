using Arch.Core.Utils;

namespace Arch.Core.Extensions;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `Entity` type directly?
/// <summary>
///     The <see cref="EntityExtensions"/> class
///     adds several extension methods for <see cref="Entity"/>.
/// </summary>
public static partial class EntityExtensions
{
#if !PURE_ECS

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Archetype GetArchetype(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetArchetype(in entity);
    }

    /// <summary>
    ///     Returns the <see cref="Chunk"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to its <see cref="Chunk"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Chunk GetChunk(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.GetChunk(in entity);
    }

    /// <summary>
    ///     Returns all <see cref="ComponentType"/>'s of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>'s array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType[] GetComponentTypes(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetComponentTypes(in entity);
    }

    /// <summary>
    ///     Returns all components of an <see cref="Entity"/> as an array.
    ///     Will allocate memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A newly allocated array containing the entities components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object[] GetAllComponents(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetAllComponents(in entity);
    }


    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive in this <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it exists and is alive, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlive(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.IsAlive(in entity);
    }

    /// <summary>
    ///     Returns the version of an <see cref="Entity"/>.
    ///     Indicating how often it was recycled.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its version.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Version(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Version(in entity);
    }

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The instance, optional.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set<T>(this in Entity entity, in T component) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        world.Set(in entity, in component);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>(this in Entity entity) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T>(in entity);
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get<T>(this in Entity entity) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.Get<T>(entity);
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(this in Entity entity, out T component) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        return world.TryGet(in entity, out component);
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, oterhwhise false.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T TryGetRef<T>(this in Entity entity, out bool exists) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.TryGetRef<T>(in entity, out exists);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component instance, optional.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Add<T>(this in Entity entity, in T cmp = default) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        world.Add<T>(in entity);
    }

    /// <summary>
    ///     Removes an component from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Remove<T>(this in Entity entity) where T : struct
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T>(in entity);
    }
#endif
}
