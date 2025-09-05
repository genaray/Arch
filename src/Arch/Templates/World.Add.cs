

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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;
        var id20 = Component<T20>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);
        spanBitSet.SetBit(id20);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;
        var id20 = Component<T20>.ComponentType.Id;
        var id21 = Component<T21>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);
        spanBitSet.SetBit(id20);
        spanBitSet.SetBit(id21);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;
        var id20 = Component<T20>.ComponentType.Id;
        var id21 = Component<T21>.ComponentType.Id;
        var id22 = Component<T22>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);
        spanBitSet.SetBit(id20);
        spanBitSet.SetBit(id21);
        spanBitSet.SetBit(id22);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;
        var id20 = Component<T20>.ComponentType.Id;
        var id21 = Component<T21>.ComponentType.Id;
        var id22 = Component<T22>.ComponentType.Id;
        var id23 = Component<T23>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);
        spanBitSet.SetBit(id20);
        spanBitSet.SetBit(id21);
        spanBitSet.SetBit(id22);
        spanBitSet.SetBit(id23);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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

        // Get all the ids here just in case we are adding a new component as this will grow the ComponentRegistry.Size
        var id0 = Component<T0>.ComponentType.Id;
        var id1 = Component<T1>.ComponentType.Id;
        var id2 = Component<T2>.ComponentType.Id;
        var id3 = Component<T3>.ComponentType.Id;
        var id4 = Component<T4>.ComponentType.Id;
        var id5 = Component<T5>.ComponentType.Id;
        var id6 = Component<T6>.ComponentType.Id;
        var id7 = Component<T7>.ComponentType.Id;
        var id8 = Component<T8>.ComponentType.Id;
        var id9 = Component<T9>.ComponentType.Id;
        var id10 = Component<T10>.ComponentType.Id;
        var id11 = Component<T11>.ComponentType.Id;
        var id12 = Component<T12>.ComponentType.Id;
        var id13 = Component<T13>.ComponentType.Id;
        var id14 = Component<T14>.ComponentType.Id;
        var id15 = Component<T15>.ComponentType.Id;
        var id16 = Component<T16>.ComponentType.Id;
        var id17 = Component<T17>.ComponentType.Id;
        var id18 = Component<T18>.ComponentType.Id;
        var id19 = Component<T19>.ComponentType.Id;
        var id20 = Component<T20>.ComponentType.Id;
        var id21 = Component<T21>.ComponentType.Id;
        var id22 = Component<T22>.ComponentType.Id;
        var id23 = Component<T23>.ComponentType.Id;
        var id24 = Component<T24>.ComponentType.Id;


        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(id0);
        spanBitSet.SetBit(id1);
        spanBitSet.SetBit(id2);
        spanBitSet.SetBit(id3);
        spanBitSet.SetBit(id4);
        spanBitSet.SetBit(id5);
        spanBitSet.SetBit(id6);
        spanBitSet.SetBit(id7);
        spanBitSet.SetBit(id8);
        spanBitSet.SetBit(id9);
        spanBitSet.SetBit(id10);
        spanBitSet.SetBit(id11);
        spanBitSet.SetBit(id12);
        spanBitSet.SetBit(id13);
        spanBitSet.SetBit(id14);
        spanBitSet.SetBit(id15);
        spanBitSet.SetBit(id16);
        spanBitSet.SetBit(id17);
        spanBitSet.SetBit(id18);
        spanBitSet.SetBit(id19);
        spanBitSet.SetBit(id20);
        spanBitSet.SetBit(id21);
        spanBitSet.SetBit(id22);
        spanBitSet.SetBit(id23);
        spanBitSet.SetBit(id24);


        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype)){
            var newSignature = Signature.Add(oldArchetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature);
            newArchetype = GetOrCreate(newSignature);
        }

        Move(entity, ref data, oldArchetype, newArchetype, out var newSlot);
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
