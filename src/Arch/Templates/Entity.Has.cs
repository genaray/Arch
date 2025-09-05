

using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public static partial class EntityExtensions
{
#if !PURE_ECS
    [Pure]
    public static bool Has<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(entity);
    }

    [Pure]
    public static bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        return world.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(entity);
    }

#endif
}
