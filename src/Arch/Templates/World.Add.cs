

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1>(Entity entity, in T0? t0Component = default,in T1? t1Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1>(ref newSlot, in t0Component,in t1Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2>(ref newSlot, in t0Component,in t1Component,in t2Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);
        OnComponentAdded<T20>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);
        OnComponentAdded<T20>(entity);
        OnComponentAdded<T21>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);
        OnComponentAdded<T20>(entity);
        OnComponentAdded<T21>(entity);
        OnComponentAdded<T22>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);
        OnComponentAdded<T20>(entity);
        OnComponentAdded<T21>(entity);
        OnComponentAdded<T22>(entity);
        OnComponentAdded<T23>(entity);

    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default,in T24? t24Component = default)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateAddTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature, oldArchetype);
        var newArchetype = transition.Target;

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot, transition);
        newArchetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(ref newSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component,in t24Component);
        OnComponentAdded<T0>(entity);
        OnComponentAdded<T1>(entity);
        OnComponentAdded<T2>(entity);
        OnComponentAdded<T3>(entity);
        OnComponentAdded<T4>(entity);
        OnComponentAdded<T5>(entity);
        OnComponentAdded<T6>(entity);
        OnComponentAdded<T7>(entity);
        OnComponentAdded<T8>(entity);
        OnComponentAdded<T9>(entity);
        OnComponentAdded<T10>(entity);
        OnComponentAdded<T11>(entity);
        OnComponentAdded<T12>(entity);
        OnComponentAdded<T13>(entity);
        OnComponentAdded<T14>(entity);
        OnComponentAdded<T15>(entity);
        OnComponentAdded<T16>(entity);
        OnComponentAdded<T17>(entity);
        OnComponentAdded<T18>(entity);
        OnComponentAdded<T19>(entity);
        OnComponentAdded<T20>(entity);
        OnComponentAdded<T21>(entity);
        OnComponentAdded<T22>(entity);
        OnComponentAdded<T23>(entity);
        OnComponentAdded<T24>(entity);

    }
    }
