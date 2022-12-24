using System;
using System.Runtime.CompilerServices;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

public static partial class EntityExtensions
{

#if !PURE_ECS

    /// <summary>
    ///     Returns the <see cref="Archetype" /> in which the <see cref="Entity" /> and its components are stored in.
    /// </summary>
    /// <param name="entity">The entity we wanna receive the <see cref="Archetype" /> from. </param>
    /// <returns>The <see cref="Archetype" /> in which the entity and all its components are stored. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Archetype GetArchetype(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetArchetype(in entity);
    }

    /// <summary>
    ///     Returns the <see cref="Chunk" /> in which the <see cref="Entity" /> is located in.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>A reference to its chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Chunk GetChunk(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.GetChunk(in entity);
    }

    /// <summary>
    ///     Returns the component types which the passed <see cref="Entity" /> has assigned.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>An array of components types.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType[] GetComponentTypes(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetComponentTypes(in entity);
    }

    /// <summary>
    ///     Returns the components which the passed <see cref="Entity" /> has assigned.
    ///     In case of struct components they will be boxed which causes memory allocations.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>An array of components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object[] GetAllComponents(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetAllComponents(in entity);
    }

    /// <summary>
    ///     Returns true if the passed entity is alive.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if the entity is alive in its world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlive(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.IsAlive(in entity);
    }
    
    /// <summary>
    ///     Returns true if the passed entity is alive.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>True if the entity is alive in its world.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Version(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Version(in entity);
    }

    /// <summary>
    ///     Sets or updates a <see cref="Entity" />'s component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity" /></param>
    /// <param name="component">A reference to the component</param>
    /// <typeparam name="T">The component type</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set<T>(this in Entity entity, in T component)
    { 
        var world = World.Worlds[entity.WorldId];
        world.Set(in entity, in component);
    }

    /// <summary>
    ///     Returns true if the <see cref="Entity" /> has a certain component assigned.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>True if it exists for that entity</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T>(in entity);
    }

    /// <summary>
    ///     Returns a reference to the component of an <see cref="Entity" />.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <returns>A reference to the component</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.Get<T>(entity);
    }

    /// <summary>
    ///     Returns the component if it exists for that entity.
    ///     In case of a struct it will only returns a copy.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <param name="component">The component itself</param>
    /// <returns>True if the component exists on the entity and could be returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(this in Entity entity, out T component)
    {
        var world = World.Worlds[entity.WorldId];
        return world.TryGet(in entity, out component);
    }
    
    /// <summary>
    ///     Returns the component if it exists for that entity.
    ///     In case of a struct it will only returns a copy.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <param name="component">The component itself</param>
    /// <returns>True if the component exists on the entity and could be returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T TryGetRef<T>(this in Entity entity, out bool exists)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.TryGetRef<T>(in entity, out exists);
    }
    
    /// <summary>
    ///     Adds a component to the existing entity.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <param name="component">The component itself</param>
    /// <returns>True if the component exists on the entity and could be returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Add<T>(this in Entity entity, in T cmp = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Add<T>(in entity);
    }
    
    /// <summary>
    ///     Removes an component from an entity. 
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <typeparam name="T">The component type</typeparam>
    /// <param name="component">The component itself</param>
    /// <returns>True if the component exists on the entity and could be returned.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Remove<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T>(in entity);
    }
    
#endif
}