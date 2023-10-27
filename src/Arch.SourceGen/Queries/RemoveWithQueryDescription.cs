namespace Arch.SourceGen;

/// <summary>
///     Adds extension methods for generating `World.Remove(in query);` methods.
/// </summary>
public static class RemoveWithQueryDesription
{
    /// <summary>
    ///     Appends `World.Remove(in query)` methods.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    public static StringBuilder AppendRemoveWithQueryDescriptions(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendRemoveWithQueryDescription(index);
        }

        return sb;
    }

    /// <summary>
    ///     Appends a `World.Remove(in query)` method.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount of generic parameters.</param>
    public static void AppendRemoveWithQueryDescription(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var types = new StringBuilder().GenericTypeParams(amount);

        var clearIds = new StringBuilder();
        var removeEvents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            clearIds.AppendLine($"spanBitSet.ClearBit(Component<T{index}>.ComponentType.Id);");
            removeEvents.AppendLine($"OnComponentRemoved<T{index}>(archetype);");
        }

        var template =
            $$"""
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [StructuralChange]
            public void Remove<{{generics}}>(in QueryDescription queryDescription)
            {
                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

                var query = Query(in queryDescription);
                foreach (var archetype in query.GetArchetypeIterator())
                {
                    // Archetype without T shouldnt be skipped to prevent undefined behaviour.
                    if(archetype.Entities <= 0 || !archetype.Has<{{generics}}>())
                    {
                        continue;
                    }

                    // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
                    var bitSet = archetype.BitSet;
                    var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
                    {{clearIds}}

                    // Get or create new archetype.
                    if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    {
                        newArchetype = GetOrCreate(archetype.Types.Remove({{types}}));
                    }

                    {{removeEvents}}

                    // Get last slots before copy, for updating entityinfo later
                    var archetypeSlot = archetype.LastSlot;
                    var newArchetypeLastSlot = newArchetype.LastSlot;
                    Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
                    EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

                    Archetype.Copy(archetype, newArchetype);
                    archetype.Clear();
                }
            }
            """;

        sb.AppendLine(template);
    }
}

