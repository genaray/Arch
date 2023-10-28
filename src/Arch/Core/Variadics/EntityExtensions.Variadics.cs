using System.Diagnostics.Contracts;

namespace Arch.Core;
public static partial class EntityExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 2)]
    public static bool Has<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1>(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2)]
    public static void Set<T0, T1>(this Entity entity, T0 component__T0, T1 component__T1)
    {
        var world = World.Worlds[entity.WorldId];
        // [Variadic: CopyArgs(component)]
        world.Set(entity, component__T0, component__T1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 2)]
    public static Components<T0, T1> Get<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Get<T0, T1>(entity);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2)]
    public static void Add<T0, T1>(this Entity entity, in T0 component__T0, in T1 component__T1)
    {
        var world = World.Worlds[entity.WorldId];
        // [Variadic: CopyArgs(component)]
        world.Add(entity, component__T0, component__T1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2)]
    public static void Remove<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1>(entity);
    }
}
