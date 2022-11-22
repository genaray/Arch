using System;
using System.Runtime.CompilerServices;

namespace Arch.Core.Extensions;

public static class EntityExtensions
{
    /// <summary>
    ///     Returns the <see cref="Archetype" /> in which the <see cref="Entity" /> and its components are stored in.
    /// </summary>
    /// <param name="entity">The entity we wanna receive the <see cref="Archetype" /> from. </param>
    /// <returns>The <see cref="Archetype" /> in which the entity and all its components are stored. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Archetype GetArchetype(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.EntityToArchetype[entity.EntityId];
    }

    /// <summary>
    ///     Returns the <see cref="Chunk" /> in which the <see cref="Entity" /> is located in.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>A reference to its chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Chunk GetChunk(this in Entity entity)
    {
        var archetype = entity.GetArchetype();
        var chunkIndex = archetype.EntityIdToChunkIndex[entity.EntityId];
        return ref archetype.Chunks[chunkIndex];
    }

    /// <summary>
    ///     Returns the component types which the passed <see cref="Entity" /> has assigned.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>An array of components types.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Type[] GetComponentTypes(this in Entity entity)
    {
        var archetype = entity.GetArchetype();
        return archetype.Types;
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
        // Get archetype and chunk
        var archetype = entity.GetArchetype();
        var chunkIndex = archetype.EntityIdToChunkIndex[entity.EntityId];
        var chunk = archetype.Chunks[chunkIndex];
        var components = chunk.Components;

        // Loop over components, collect and returns them
        var entityIndex = chunk.EntityIdToIndex[entity.EntityId];
        var cmps = new object[components.Length];

        for (var index = 0; index < components.Length; index++)
        {
            var componentArray = components[index];
            var component = componentArray.GetValue(entityIndex);
            cmps[index] = component;
        }

        return cmps;
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
        return world.EntityToArchetype.ContainsKey(entity.EntityId);
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
        var componentCopy = component;
        var archetype = entity.GetArchetype();
        archetype.Set(in entity, in componentCopy);
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
        var archetype = entity.GetArchetype();
        return archetype.Has<T>();
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
        var archetype = entity.GetArchetype();
        return ref archetype.Get<T>(entity);
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
        component = default;
        if (!entity.Has<T>()) return false;

        var archetype = entity.GetArchetype();
        component = archetype.Get<T>(entity);
        return true;
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
}