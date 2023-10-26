using System.Diagnostics.Contracts;
using Arch.Core;
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
    [Pure]
    public static Archetype GetArchetype(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetArchetype(entity);
    }

    /// <summary>
    ///     Returns the <see cref="Chunk"/> of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to its <see cref="Chunk"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static ref readonly Chunk GetChunk(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.GetChunk(entity);
    }

    /// <summary>
    ///     Returns all <see cref="ComponentType"/>'s of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>'s array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static ComponentType[] GetComponentTypes(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetComponentTypes(entity);
    }

    /// <summary>
    ///     Returns all components of an <see cref="Entity"/> as an array.
    ///     Will allocate memory.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A newly allocated array containing the entities components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static object?[] GetAllComponents(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetAllComponents(entity);
    }

    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive in this <see cref="World"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it exists and is alive, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool IsAlive(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.IsAlive(entity);
    }

    /// <summary>
    ///     Returns the version of an <see cref="Entity"/>.
    ///     Indicating how often it was recycled.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its version.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static int Version(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Version(entity);
    }

    /// <summary>
    ///     Returns a <see cref="EntityReference"/> to an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its <see cref="EntityReference"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static EntityReference Reference(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Reference(entity);
    }

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The instance, optional.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set<T>(this in Entity entity, in T? component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set(entity, in component);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool Has<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T>(entity);
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static ref T Get<T>(this in Entity entity)
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
    [Pure]
    public static bool TryGet<T>(this in Entity entity, out T? component)
    {
        var world = World.Worlds[entity.WorldId];
        return world.TryGet(entity, out component);
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, oterhwhise false.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static ref T TryGetRef<T>(this in Entity entity, out bool exists)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.TryGetRef<T>(entity, out exists);
    }

    /// <summary>
    ///    Ensures the existance of an component on an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component value used if its being added.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AddOrGet<T>(this in Entity entity, T? component = default)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.AddOrGet(entity, component);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="component">The component instance, optional.</param>
    /// <typeparam name="T">The component type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Add<T>(this in Entity entity, in T? component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Add(entity, component);
    }

    /// <summary>
    ///     Removes an component from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Remove<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T>(entity);
    }
#endif
}

public static partial class EntityExtensions
{

#if !PURE_ECS

    /// <summary>
    ///     Sets or replaces a component for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set(this in Entity entity, object cmp)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set(entity, cmp);
    }

    /// <summary>
    ///     Sets or replaces a <see cref="IList{T}"/> of components for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The components <see cref="IList{T}"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetRange(this in Entity entity, params object[] components)
    {
        var world = World.Worlds[entity.WorldId];
        world.SetRange(entity, components);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool Has(this in Entity entity, ComponentType type)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has(entity, type);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool HasRange(this in Entity entity, params ComponentType[] types)
    {
        var world = World.Worlds[entity.WorldId];
        return world.HasRange(entity, types);
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static object? Get(this in Entity entity, ComponentType type)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Get(entity, type);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static object?[] GetRange(this in Entity entity, params ComponentType[] types)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetRange(entity, types);
    }

    // ReSharper disable once PureAttributeOnVoidMethod
    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> where the components are put it.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static void GetRange(this in Entity entity, ComponentType[] types, IList<object> components)
    {
        var world = World.Worlds[entity.WorldId];
        world.GetRange(entity, types, components);
    }

    /// <summary>
    ///     Trys to return a reference to the component of an <see cref="Entity"/>.
    ///     Will copy the component if its a struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="type">The component <see cref="ComponentType"/>.</param>
    /// <param name="component">The found component.</param>
    /// <returns>True if it exists, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static bool TryGet(this in Entity entity, ComponentType type, out object? component)
    {
        var world = World.Worlds[entity.WorldId];
        return world.TryGet(entity, type, out component);
    }

    /// <summary>
    ///     Adds an new component to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="cmp">The component.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Add(this in Entity entity, in object cmp)
    {
        var world = World.Worlds[entity.WorldId];
        world.Add(entity, cmp);
    }

    /// <summary>
    ///     Adds a <see cref="IList{T}"/> of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The component <see cref="IList{T}"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange(this in Entity entity, params object[] components)
    {
        var world = World.Worlds[entity.WorldId];
        world.AddRange(entity, components);
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange(this in Entity entity, IList<ComponentType> components)
    {
        var world = World.Worlds[entity.WorldId];
        world.AddRange(entity, components);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveRange(this in Entity entity, params ComponentType[] types)
    {
        var world = World.Worlds[entity.WorldId];
        world.RemoveRange(entity, types);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveRange(this in Entity entity, IList<ComponentType> types)
    {
        var world = World.Worlds[entity.WorldId];
        world.RemoveRange(entity, types);
    }

#endif
}
