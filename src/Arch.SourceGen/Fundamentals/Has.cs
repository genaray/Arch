namespace Arch.SourceGen;

public static class HasExtensions
{
    public static StringBuilder AppendChunkHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkHas(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkHas(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var getIds = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            getIds.AppendLine($"var t{index}ComponentId = Component<T{index}>.ComponentType.Id;");
        }

        var boundChecks = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            boundChecks.AppendLine($"if (t{index}ComponentId >= ComponentIdToArrayIndex.Length) return false;");
        }

        var ifs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            ifs.AppendLine($"if (ComponentIdToArrayIndex[t{index}ComponentId] == -1) return false;");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public bool Has<{{generics}}>()
            {
                {{getIds}}
                {{boundChecks}}
                {{ifs}}

                return true;
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendArchetypeHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendArchetypeHas(index);
        }

        return sb;
    }

    public static StringBuilder AppendArchetypeHas(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var getIds = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            getIds.AppendLine($"var t{index}ComponentId = Component<T{index}>.ComponentType.Id;");
        }

        var isSet = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            isSet.AppendLine($"BitSet.IsSet(t{index}ComponentId) &&");
        }

        isSet.Length -= 4;

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool Has<{{generics}}>()
            {
                {{getIds}}
                return {{isSet}};
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendWorldHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendWorldHas(index);
        }

        return sb;
    }

    public static StringBuilder AppendWorldHas(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public bool Has<{{generics}}>(Entity entity)
            {
                var archetype = EntityInfo.GetArchetype(entity.Id);
                return archetype.Has<{{generics}}>();
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendEntityHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityHas(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityHas(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public static bool Has<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                return world.Has<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
