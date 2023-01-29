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
        for (var index = 0; index <= amount; index++)
        {
            setIds.AppendLine($"spanBitSet.SetBit(Component<T{index}>.ComponentType.Id);");
        }

        var template =
            $$"""
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Add<{{generics}}>(in Entity entity, {{parameters}})
            {
                var oldArchetype = EntityInfo[entity.Id].Archetype;

                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
                oldArchetype.BitSet.AsSpan(stack);

                // Create a span bitset, doing it local saves us headache and gargabe
                var spanBitSet = new SpanBitSet(stack);
                {{setIds}}

                if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    newArchetype = GetOrCreate(oldArchetype.Types.Add({{types}}));

                Move(in entity, oldArchetype, newArchetype, out var newSlot);
                newArchetype.Set<{{generics}}>(ref newSlot, {{inParameters}});
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
        for (var index = 0; index <= amount; index++)
        {
            removes.AppendLine($"spanBitSet.ClearBit(Component<T{index}>.ComponentType.Id);");
        }

        var template =
            $$"""
            [SkipLocalsInit]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Remove<{{generics}}>(in Entity entity)
            {
                var oldArchetype = EntityInfo[entity.Id].Archetype;

                // BitSet to stack/span bitset, size big enough to contain ALL registered components.
                Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
                oldArchetype.BitSet.AsSpan(stack);

                // Create a span bitset, doing it local saves us headache and gargabe
                var spanBitSet = new SpanBitSet(stack);
                {{removes}}

                if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
                    newArchetype = GetOrCreate(oldArchetype.Types.Remove({{types}}));

                Move(in entity, oldArchetype, newArchetype, out _);
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
            public static void Add<{{generics}}>(this in Entity entity, {{parameters}})
            {
                var world = World.Worlds[entity.WorldId];
                world.Add<{{generics}}>(in entity);
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
            public static void Remove<{{generics}}>(this in Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                world.Remove<{{generics}}>(in entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
