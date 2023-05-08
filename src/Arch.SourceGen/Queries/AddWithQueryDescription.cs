namespace Arch.SourceGen;

/// <summary>
///     Adds extension methods for generating `World.Add(in query, T0...TN);` methods.
/// </summary>
public static class AddWithQueryDescription
{
    /// <summary>
    ///     Appends `World.Add(in query, T0...TN)` methods.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    public static StringBuilder AppendAddWithQueryDescriptions(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendAddWithQueryDescription(index);
        }

        return sb;
    }

    /// <summary>
    ///     Appends a `World.Add(in query, T0...TN)` method.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount of generic parameters.</param>
    public static void AppendAddWithQueryDescription(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        var inParameters = new StringBuilder().InsertGenericInParams(amount);
        var types = new StringBuilder().GenericTypeParams(amount);

        var setIds = new StringBuilder();
        var addEvents = new StringBuilder();
        var setEvents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            setIds.AppendLine($"spanBitSet.SetBit(Component<T{index}>.ComponentType.Id);");
            addEvents.AppendLine($"OnComponentAdded<T{index}>(in entity);");
            setEvents.AppendLine($"OnComponentSet(in entity, in t{index}Component);");
        }

        var template =
            $$"""
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Add<{{generics}}>(in QueryDescription queryDescription, {{parameters}})
            {
                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

                var query = Query(in queryDescription);
                foreach (var archetype in query.GetArchetypeIterator())
                {
                    // Archetype with T shouldnt be skipped to prevent undefined behaviour.
                    if(archetype.Entities == 0 || archetype.Has<{{generics}}>())
                    {
                        continue;
                    }

                    // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
                    archetype.BitSet.AsSpan(stack);
                    var spanBitSet = new SpanBitSet(stack);
                    {{setIds}}

                    // Get or create new archetype.
                    if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    {
                        newArchetype = GetOrCreate(archetype.Types.Add({{types}}));
                    }

                    // Get last slots before copy, for updating entityinfo later
                    var archetypeSlot = archetype.LastSlot;
                    var newArchetypeLastSlot = newArchetype.LastSlot;
                    Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            #if EVENTS
                    // TODO stackalloc under a certain size?
                    var entitiesLength = archetype.Entities;
                    var entities = ArrayPool<Entity>.Shared.Rent(entitiesLength);
                    var i = 0;
                    foreach (ref var chunk in archetype)
                    {
                        foreach (var j in chunk)
                        {
                            entities[i++] = chunk.Entity(j);
                        }
                    }
            #endif
                    EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

                    // Copy, set and clear
                    Archetype.Copy(archetype, newArchetype);
            #if EVENTS
                    var entitiesSpan = entities.AsSpan();
                    for (i = 0; i < entitiesLength; i++)
                    {
                        ref var entity = ref entitiesSpan[i];
                        {{addEvents.ToString().TrimEnd()}}
                    }
            #endif
                    var lastSlot = newArchetype.LastSlot;
                    newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, {{inParameters}});
            #if EVENTS
                    for (i = 0; i < entitiesLength; i++)
                    {
                        ref var entity = ref entitiesSpan[i];
                        {{setEvents.ToString().TrimEnd()}}
                    }

                    ArrayPool<Entity>.Shared.Return(entities, true);
            #endif
                    archetype.Clear();
                }
            }
            """;

        sb.AppendLine(template);
    }
}
