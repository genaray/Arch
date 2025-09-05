

using System;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public static partial class EntityExtensions
{
#if !PURE_ECS
    public static void Set<T0, T1>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1>(entity, in t0Component,in t1Component);
    }

    public static void Set<T0, T1, T2>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2>(entity, in t0Component,in t1Component,in t2Component);
    }

    public static void Set<T0, T1, T2, T3>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3>(entity, in t0Component,in t1Component,in t2Component,in t3Component);
    }

    public static void Set<T0, T1, T2, T3, T4>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component);
    }

    public static void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(this Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default,in T24? t24Component = default)
    {
        var world = World.Worlds[entity.WorldId];
        world.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(entity, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component,in t24Component);
    }

#endif
}
