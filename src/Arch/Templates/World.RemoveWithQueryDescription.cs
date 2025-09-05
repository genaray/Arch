

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T2>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T3>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T4>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T5>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T6>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T7>.ComponentType.Id);
            spanBitSet.ClearBit(Component<T8>.ComponentType.Id);
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            OnComponentRemoved<T20>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            OnComponentRemoved<T20>(archetype);
            OnComponentRemoved<T21>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            OnComponentRemoved<T20>(archetype);
            OnComponentRemoved<T21>(archetype);
            OnComponentRemoved<T22>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            OnComponentRemoved<T20>(archetype);
            OnComponentRemoved<T21>(archetype);
            OnComponentRemoved<T22>(archetype);
            OnComponentRemoved<T23>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    
    [SkipLocalsInit]
    [StructuralChange]
    public void Remove<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if(archetype.EntityCount <= 0 || !archetype.Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
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
            

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var newSignature = Signature.Remove(archetype.Signature, Component<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>.Signature);
                newArchetype = GetOrCreate(newSignature);
            }

            OnComponentRemoved<T0>(archetype);
            OnComponentRemoved<T1>(archetype);
            OnComponentRemoved<T2>(archetype);
            OnComponentRemoved<T3>(archetype);
            OnComponentRemoved<T4>(archetype);
            OnComponentRemoved<T5>(archetype);
            OnComponentRemoved<T6>(archetype);
            OnComponentRemoved<T7>(archetype);
            OnComponentRemoved<T8>(archetype);
            OnComponentRemoved<T9>(archetype);
            OnComponentRemoved<T10>(archetype);
            OnComponentRemoved<T11>(archetype);
            OnComponentRemoved<T12>(archetype);
            OnComponentRemoved<T13>(archetype);
            OnComponentRemoved<T14>(archetype);
            OnComponentRemoved<T15>(archetype);
            OnComponentRemoved<T16>(archetype);
            OnComponentRemoved<T17>(archetype);
            OnComponentRemoved<T18>(archetype);
            OnComponentRemoved<T19>(archetype);
            OnComponentRemoved<T20>(archetype);
            OnComponentRemoved<T21>(archetype);
            OnComponentRemoved<T22>(archetype);
            OnComponentRemoved<T23>(archetype);
            OnComponentRemoved<T24>(archetype);
            

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.CurrentSlot;
            var newArchetypeLastSlot = newArchetype.CurrentSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            var oldCapacity = newArchetype.EntityCapacity;
            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();

            Capacity += newArchetype.EntityCapacity - oldCapacity;
        }

        EntityInfo.EnsureCapacity(Capacity);
    }
    }
