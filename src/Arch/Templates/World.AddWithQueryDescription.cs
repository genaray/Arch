

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 3)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 4)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 5)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 6)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 7)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 8)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 9)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 10)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 11)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 12)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 13)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 14)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 15)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 16)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 17)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 18)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 19)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 20)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 21)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 22)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);
            spanBitSet.SetBit(Component<T20>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);
            OnComponentAdded<T20>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 23)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);
            spanBitSet.SetBit(Component<T20>.ComponentType.Id);
            spanBitSet.SetBit(Component<T21>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);
            OnComponentAdded<T20>(archetype);
            OnComponentAdded<T21>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 24)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);
            spanBitSet.SetBit(Component<T20>.ComponentType.Id);
            spanBitSet.SetBit(Component<T21>.ComponentType.Id);
            spanBitSet.SetBit(Component<T22>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);
            OnComponentAdded<T20>(archetype);
            OnComponentAdded<T21>(archetype);
            OnComponentAdded<T22>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 25)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);
            spanBitSet.SetBit(Component<T20>.ComponentType.Id);
            spanBitSet.SetBit(Component<T21>.ComponentType.Id);
            spanBitSet.SetBit(Component<T22>.ComponentType.Id);
            spanBitSet.SetBit(Component<T23>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);
            OnComponentAdded<T20>(archetype);
            OnComponentAdded<T21>(archetype);
            OnComponentAdded<T22>(archetype);
            OnComponentAdded<T23>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Add<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in QueryDescription queryDescription, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default,in T24? t24Component = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size + 26)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount == 0 || archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);
            spanBitSet.SetBit(Component<T2>.ComponentType.Id);
            spanBitSet.SetBit(Component<T3>.ComponentType.Id);
            spanBitSet.SetBit(Component<T4>.ComponentType.Id);
            spanBitSet.SetBit(Component<T5>.ComponentType.Id);
            spanBitSet.SetBit(Component<T6>.ComponentType.Id);
            spanBitSet.SetBit(Component<T7>.ComponentType.Id);
            spanBitSet.SetBit(Component<T8>.ComponentType.Id);
            spanBitSet.SetBit(Component<T9>.ComponentType.Id);
            spanBitSet.SetBit(Component<T10>.ComponentType.Id);
            spanBitSet.SetBit(Component<T11>.ComponentType.Id);
            spanBitSet.SetBit(Component<T12>.ComponentType.Id);
            spanBitSet.SetBit(Component<T13>.ComponentType.Id);
            spanBitSet.SetBit(Component<T14>.ComponentType.Id);
            spanBitSet.SetBit(Component<T15>.ComponentType.Id);
            spanBitSet.SetBit(Component<T16>.ComponentType.Id);
            spanBitSet.SetBit(Component<T17>.ComponentType.Id);
            spanBitSet.SetBit(Component<T18>.ComponentType.Id);
            spanBitSet.SetBit(Component<T19>.ComponentType.Id);
            spanBitSet.SetBit(Component<T20>.ComponentType.Id);
            spanBitSet.SetBit(Component<T21>.ComponentType.Id);
            spanBitSet.SetBit(Component<T22>.ComponentType.Id);
            spanBitSet.SetBit(Component<T23>.ComponentType.Id);
            spanBitSet.SetBit(Component<T24>.ComponentType.Id);


            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Add(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.CurrentSlot;
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component,in t24Component);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
            OnComponentAdded<T0>(archetype);
            OnComponentAdded<T1>(archetype);
            OnComponentAdded<T2>(archetype);
            OnComponentAdded<T3>(archetype);
            OnComponentAdded<T4>(archetype);
            OnComponentAdded<T5>(archetype);
            OnComponentAdded<T6>(archetype);
            OnComponentAdded<T7>(archetype);
            OnComponentAdded<T8>(archetype);
            OnComponentAdded<T9>(archetype);
            OnComponentAdded<T10>(archetype);
            OnComponentAdded<T11>(archetype);
            OnComponentAdded<T12>(archetype);
            OnComponentAdded<T13>(archetype);
            OnComponentAdded<T14>(archetype);
            OnComponentAdded<T15>(archetype);
            OnComponentAdded<T16>(archetype);
            OnComponentAdded<T17>(archetype);
            OnComponentAdded<T18>(archetype);
            OnComponentAdded<T19>(archetype);
            OnComponentAdded<T20>(archetype);
            OnComponentAdded<T21>(archetype);
            OnComponentAdded<T22>(archetype);
            OnComponentAdded<T23>(archetype);
            OnComponentAdded<T24>(archetype);

        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    }
