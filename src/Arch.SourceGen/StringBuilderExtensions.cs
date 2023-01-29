namespace Arch.SourceGen;

public static class StringBuilderExtensions
{
    public static StringBuilder Tab(this StringBuilder sb)
    {
        sb.Append(" ");
        return sb;
    }

    public static StringBuilder Generic(this StringBuilder sb, int amount)
    {
        sb.Append("<");
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"T{localIndex},");
        }

        sb.Length--;
        sb.Append(">");
        return sb;
    }

    public static StringBuilder GenericWithoutBrackets(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"T{localIndex},");
        }

        sb.Length--;

        return sb;
    }


    // Queries, set, has & get

    public static StringBuilder GetFirstGenericElements(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref var t{localIndex}FirstElement = ref chunk.GetFirst<T{localIndex}>();");
        }

        return sb;
    }

    public static StringBuilder GetLastGenericElements(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref var t{localIndex}LastElement = ref ArrayExtensions.DangerousGetReferenceAt(t{localIndex}Array, chunkSize-1);");
        }

        return sb;
    }

    public static StringBuilder GetGenericComponents(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");
        }

        return sb;
    }

    public static StringBuilder GenericRefParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"ref T{localIndex} t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder InsertGenericParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"ref t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder GenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in T{localIndex} t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder GenericTypeParams(this StringBuilder sb, int amount)
    {
        for (var index = 0; index <= amount; index++)
        {
            sb.Append($"typeof(T{index}),");
        }
        sb.Length--;
        return sb;
    }

    public static StringBuilder GenericInDefaultParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in T{localIndex} t{localIndex}Component = default,");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder InsertGenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder GetChunkArrays(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"var t{localIndex}Array = GetSpan<T{localIndex}>();");
        }

        sb.Length--;
        return sb;
    }


    // Enumerator Extensions

    public static StringBuilder SpanFields(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"Span<T{localIndex}> t{localIndex}Span;");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder AssignSpanFields(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"t{localIndex}Span = _chunkEnumerator.Current.GetSpan<T{localIndex}>();");
        }

        sb.Length--;
        return sb;
    }

    public static StringBuilder InsertSpanRefs(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref t{localIndex}Span[_index],");
        }

        sb.Length -= 3;
        return sb;
    }
}
