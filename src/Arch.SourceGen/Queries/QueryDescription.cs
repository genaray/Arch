namespace Arch.SourceGen;

public static class QueryDescriptionExtensions
{
    public static StringBuilder AppendQueryDescriptionWithAlls(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendQueryDescriptionWithAll(index);
        }

        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithAll(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [UnscopedRef]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ref QueryDescription WithAll<{{generics}}>()
            {
               All = Component<{{generics}}>.Signature;
               _hashCode = -1;
               return ref this;
            }
            """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithAnys(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendQueryDescriptionWithAny(index);
        }

        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithAny(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [UnscopedRef]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ref QueryDescription WithAny<{{generics}}>()
            {
               Any = Component<{{generics}}>.Signature;
               _hashCode = -1;
               return ref this;
            }
            """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithNones(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendQueryDescriptionWithNone(index);
        }

        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithNone(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [UnscopedRef]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ref QueryDescription WithNone<{{generics}}>()
            {
               None = Component<{{generics}}>.Signature;
               _hashCode = -1;
               return ref this;
            }
            """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithExclusives(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendQueryDescriptionWithExclusive(index);
        }

        return sb;
    }

    public static StringBuilder AppendQueryDescriptionWithExclusive(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [UnscopedRef]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ref QueryDescription WithExclusive<{{generics}}>()
            {
               Exclusive = Component<{{generics}}>.Signature;
               _hashCode = -1;
               return ref this;
            }
            """;

        sb.AppendLine(template);
        return sb;
    }
}
