

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

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);
            OnComponentRemoved<T4>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);
            OnComponentRemoved<T4>(entity);
            OnComponentRemoved<T5>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);
            OnComponentRemoved<T4>(entity);
            OnComponentRemoved<T5>(entity);
            OnComponentRemoved<T6>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);
            OnComponentRemoved<T4>(entity);
            OnComponentRemoved<T5>(entity);
            OnComponentRemoved<T6>(entity);
            OnComponentRemoved<T7>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

            OnComponentRemoved<T0>(entity);
            OnComponentRemoved<T1>(entity);
            OnComponentRemoved<T2>(entity);
            OnComponentRemoved<T3>(entity);
            OnComponentRemoved<T4>(entity);
            OnComponentRemoved<T5>(entity);
            OnComponentRemoved<T6>(entity);
            OnComponentRemoved<T7>(entity);
            OnComponentRemoved<T8>(entity);

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T20>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T20>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T21>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T20>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T21>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T22>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T20>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T21>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T22>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T23>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }

    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Entity entity)
    {
        ref var data = ref EntityInfo.EntityData[entity.Id];
        var oldArchetype = data.Archetype;

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T9>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T10>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T11>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T12>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T13>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T14>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T15>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T16>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T17>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T18>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T19>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T20>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T21>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T22>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T23>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T24>.ComponentType.Id);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Remove(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

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

        Move(entity, ref data, oldArchetype, newArchetype, out _);
    }
    }
