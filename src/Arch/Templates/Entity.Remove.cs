

using System;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public static partial class EntityExtensions
{
#if !PURE_ECS
    public static void Remove<T0, T1>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1>(entity);
    }

    public static void Remove<T0, T1, T2>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2>(entity);
    }

    public static void Remove<T0, T1, T2, T3>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(entity);
    }

    public static void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(this Entity entity)
    {
        var world = World.Worlds[entity.WorldId];
        world.Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(entity);
    }

#endif
}
