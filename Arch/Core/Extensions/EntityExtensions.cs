using System;
using System.Runtime.CompilerServices;
using Arch.Core.Utils;

namespace Arch.Core.Extensions; 

public static class EntityExtensions {

    /// <summary>
    /// Returns the <see cref="Archetype"/> in which the <see cref="Entity"/> and its components are stored in.
    /// </summary>
    /// <param name="entity">The entity we wanna receive the <see cref="Archetype"/> from. </param>
    /// <returns>The <see cref="Archetype"/> in which the entity and all its components are stored. </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Archetype GetArchetype(this in Entity entity) {
            
        var world = World.Worlds[entity.WorldId];
        return world.EntityToArchetype[entity.EntityId];
    }
    
    /// <summary>
    /// Returns true if the passed entity is alive. 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public static bool IsAlive(this in Entity entity) {
        
        var world = World.Worlds[entity.WorldId];
        return world.EntityToArchetype.ContainsKey(entity.EntityId);
    }
    
    /// <summary>
    /// Sets or updates a <see cref="Entity"/>'s component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/></param>
    /// <param name="component">A reference to the component</param>
    /// <typeparam name="T">The component type</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set<T>(this in Entity entity, in T component) {

        var componentCopy = component;
        var archetype = entity.GetArchetype();
        archetype.Set(in entity.EntityId, in componentCopy);
    }
    
    /// <summary>
    /// Returns true if the <see cref="Entity"/> has a certain component assigned.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="component"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>(this in Entity entity) {

        var componentId = Component<T>.Id;
        var archetype = entity.GetArchetype();
        return archetype.Has<T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get<T>(this in Entity entity) {

        var archetype = entity.GetArchetype();
        return ref archetype.Get<T>(entity.EntityId);
    }
    
}