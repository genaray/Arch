

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);
        OnComponentRemoved<T20>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);
        OnComponentRemoved<T20>(entity);
        OnComponentRemoved<T21>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);
        OnComponentRemoved<T20>(entity);
        OnComponentRemoved<T21>(entity);
        OnComponentRemoved<T22>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);
        OnComponentRemoved<T20>(entity);
        OnComponentRemoved<T21>(entity);
        OnComponentRemoved<T22>(entity);
        OnComponentRemoved<T23>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        var transition = GetOrCreateRemoveTransition(Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature, oldArchetype);

        OnComponentRemoved<T0>(entity);
        OnComponentRemoved<T1>(entity);
        OnComponentRemoved<T2>(entity);
        OnComponentRemoved<T3>(entity);
        OnComponentRemoved<T4>(entity);
        OnComponentRemoved<T5>(entity);
        OnComponentRemoved<T6>(entity);
        OnComponentRemoved<T7>(entity);
        OnComponentRemoved<T8>(entity);
        OnComponentRemoved<T9>(entity);
        OnComponentRemoved<T10>(entity);
        OnComponentRemoved<T11>(entity);
        OnComponentRemoved<T12>(entity);
        OnComponentRemoved<T13>(entity);
        OnComponentRemoved<T14>(entity);
        OnComponentRemoved<T15>(entity);
        OnComponentRemoved<T16>(entity);
        OnComponentRemoved<T17>(entity);
        OnComponentRemoved<T18>(entity);
        OnComponentRemoved<T19>(entity);
        OnComponentRemoved<T20>(entity);
        OnComponentRemoved<T21>(entity);
        OnComponentRemoved<T22>(entity);
        OnComponentRemoved<T23>(entity);
        OnComponentRemoved<T24>(entity);

        Move(entity, ref data, oldArchetype, transition.Target, out _, transition);
    }
    }
