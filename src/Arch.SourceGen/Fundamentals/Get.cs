namespace Arch.SourceGen;

public static class GetExtensions
{

    public static StringBuilder AppendChunkGetArrays(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkGetArray(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkGetArray(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var outs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            outs.Append($"out T{index}[] t{index}Array,");
        }
        outs.Length--;

        var indexes = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            indexes.Append($"out var t{index}Index,");
        }
        indexes.Length--;

        var assignComponents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            assignComponents.AppendLine($"t{index}Array = Unsafe.As<T{index}[]>(Unsafe.Add(ref arrays, t{index}Index));");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public void GetArray<{{generics}}>({{outs}})
            {
                Index<{{generics}}>({{indexes}});
                ref var arrays = ref Components.DangerousGetReference();
                {{assignComponents}}
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendChunkGetSpans(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkGetSpan(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkGetSpan(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var outs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            outs.Append($"out Span<T{index}> t{index}Span,");
        }
        outs.Length--;

        var arrays = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            arrays.Append($"out var t{index}Array,");
        }
        arrays.Length--;

        var assignComponents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            assignComponents.AppendLine($"t{index}Span = new Span<T{index}>(t{index}Array);");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public void GetSpan<{{generics}}>({{outs}})
            {
                GetArray<{{generics}}>({{arrays}});
                {{assignComponents}}
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendChunkGetFirsts(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkGetFirst(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkGetFirst(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var arrays = new StringBuilder().GetChunkArrays(amount);

        var insertParams = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            insertParams.Append($"ref t{index}Array.DangerousGetReference(),");
        }
        insertParams.Length--;

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public Components<{{generics}}> GetFirst<{{generics}}>()
            {
                {{arrays}}
                return new Components<{{generics}}>({{insertParams}});
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendChunkIndexGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkIndexGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkIndexGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var inParams = new StringBuilder().InsertGenericParams(amount);
        var arrays = new StringBuilder().GetChunkArrays(amount);

        var gets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            gets.AppendLine($"ref var t{index}Component = ref t{index}Array[index];");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public Components<{{generics}}> Get<{{generics}}>(int index)
            {
                {{arrays}}
                {{gets}}

                return new Components<{{generics}}>({{inParams}});
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendChunkIndexGetRows(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkIndexGetRow(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkIndexGetRow(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetChunkArrays(amount);
        var inParams = new StringBuilder().InsertGenericParams(amount);

        var gets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            gets.AppendLine($"ref var t{index}Component = ref t{index}Array[index];");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public EntityComponents<{{generics}}> GetRow<{{generics}}>(int index)
            {
                {{getArrays}}

                ref var entity = ref Entities[index];
                {{gets}}

                return new EntityComponents<{{generics}}>(ref entity, {{inParams}});
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendArchetypeGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendArchetypeGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendArchetypeGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal unsafe Components<{{generics}}> Get<{{generics}}>(scoped ref Slot slot)
            {
                ref var chunk = ref GetChunk(slot.ChunkIndex);
                return chunk.Get<{{generics}}>(slot.Index);
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendWorldGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendWorldGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendWorldGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public Components<{{generics}}> Get<{{generics}}>(Entity entity)
            {
                var slot = EntityInfo.GetSlot(entity.Id);
                var archetype = EntityInfo.GetArchetype(entity.Id);
                return archetype.Get<{{generics}}>(ref slot);
            }
            """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendEntityGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Pure]
            public static Components<{{generics}}> Get<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                return world.Get<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
