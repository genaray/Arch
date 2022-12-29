using Arch.Core.Utils;

namespace Arch.Core.Extensions;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `Entity` type directly?
// TODO: Documentation.
/// <summary>
///     The <see cref="EntityExtensions"/> class
///     ...
/// </summary>
public static partial class EntityExtensions
{
#if !PURE_ECS
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Archetype GetArchetype(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetArchetype(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Chunk GetChunk(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.GetChunk(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType[] GetComponentTypes(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetComponentTypes(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object[] GetAllComponents(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.GetAllComponents(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlive(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.IsAlive(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Version(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Version(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="component"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set<T>(this in Entity entity, in T component)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set(in entity, in component);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T>(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.Get<T>(entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="component"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(this in Entity entity, out T component)
    {
        var world = World.Worlds[entity.WorldId];
        return world.TryGet(in entity, out component);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="exists"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T TryGetRef<T>(this in Entity entity, out bool exists)
    {
        var world = World.Worlds[entity.WorldId];
        return ref world.TryGetRef<T>(in entity, out exists);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="cmp"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Add<T>(this in Entity entity, in T cmp = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Add<T>(in entity);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Remove<T>(this in Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T>(in entity);
    }
#endif
}
