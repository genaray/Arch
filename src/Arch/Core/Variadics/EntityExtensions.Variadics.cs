using System.Diagnostics.Contracts;

namespace Arch.Core.Extensions;
#if !PURE_ECS
public static partial class EntityExtensions
{
    /// <inheritdoc cref="Has{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 24)]
    public static bool Has<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1>(entity);
    }

    /// <inheritdoc cref="Set{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 24)]
    public static void Set<T0, T1>(this Entity entity, in T0 component_T0, in T1 component_T1)
    {
        var world = World.Worlds[entity.WorldId];
        // [Variadic: CopyArgs(component)]
        world.Set(entity, in component_T0, in component_T1);
    }

    /// <inheritdoc cref="Get{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 24)]
    public static Components<T0, T1> Get<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Get<T0, T1>(entity);
    }

    /// <inheritdoc cref="Add{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 24)]
    public static void Add<T0, T1>(this Entity entity, in T0 component_T0, in T1 component_T1)
    {
        var world = World.Worlds[entity.WorldId];
        // [Variadic: CopyArgs(component)]
        world.Add(entity, in component_T0, in component_T1);
    }

    /// <inheritdoc cref="Remove{T}"/>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 24)]
    public static void Remove<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1>(entity);
    }
}
#endif
