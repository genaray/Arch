namespace Arch.SourceGen;

public static class StructuralChangesExtensions
{
    public static StringBuilder AppendWorldAdds(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendWorldAdd(index);
        }

        return sb;
    }

    public static StringBuilder AppendWorldAdd(this StringBuilder sb, int amount)
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
            public void Add<{{generics}}>(Entity entity, {{parameters}})
            {
                var oldArchetype = EntityInfo.GetArchetype(entity.Id);

                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
                oldArchetype.BitSet.AsSpan(stack);

                // Create a span bitset, doing it local saves us headache and gargabe
                var spanBitSet = new SpanBitSet(stack);
                {{setIds}}

                if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    newArchetype = GetOrCreate(oldArchetype.Types.Add({{types}}));

                Move(entity, oldArchetype, newArchetype, out var newSlot);
                newArchetype.Set<{{generics}}>(ref newSlot, {{inParameters}});
            #if EVENTS
                {{addEvents}}
                {{setEvents.ToString().TrimEnd()}}
            #endif
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendWorldRemoves(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendWorldRemove(index);
        }

        return sb;
    }

    public static StringBuilder AppendWorldRemove(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var types = new StringBuilder().GenericTypeParams(amount);

        var removes = new StringBuilder();
        var events = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            removes.AppendLine($"spanBitSet.ClearBit(Component<T{index}>.ComponentType.Id);");
            events.AppendLine($"OnComponentRemoved<T{index}>(in entity);");
        }

        var template =
            $$"""
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Remove<{{generics}}>(Entity entity)
            {
                var oldArchetype = EntityInfo.GetArchetype(entity.Id);

                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
                oldArchetype.BitSet.AsSpan(stack);

                // Create a span bitset, doing it local saves us headache and gargabe
                var spanBitSet = new SpanBitSet(stack);
                {{removes}}

                if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    newArchetype = GetOrCreate(oldArchetype.Types.Remove({{types}}));

                Move(entity, oldArchetype, newArchetype, out _);
            #if EVENTS
                {{events.ToString().TrimEnd()}}
            #endif
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendEntityAdds(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityAdd(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityAdd(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Add<{{generics}}>(this Entity entity, {{parameters}})
            {
                var world = World.Worlds[entity.WorldId];
                world.Add<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendEntityRemoves(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityRemove(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityRemove(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Remove<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                world.Remove<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
