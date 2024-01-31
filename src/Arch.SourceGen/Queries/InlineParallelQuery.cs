namespace Arch.SourceGen;

public static class StringBuilderHpParallelQueryExtensions
{
    public static StringBuilder AppendHpParallelQuerys(this StringBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            builder.AppendHpParallelQuery(index);
        }

        return builder;
    }

    public static void AppendHpParallelQuery(this StringBuilder builder, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void InlineParallelQuery<T,{{generics}}>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<{{generics}}>
            {
                var innerJob = new IForEachJob<T,{{generics}}>();
                innerJob.ForEach = iForEach;

                InlineParallelChunkQuery(in description, innerJob);
            }
            """;

        builder.AppendLine(template);
    }

    public static StringBuilder AppendHpeParallelQuerys(this StringBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            builder.AppendHpeParallelQuery(index);
        }

        return builder;
    }

    public static void AppendHpeParallelQuery(this StringBuilder builder, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void InlineParallelEntityQuery<T,{{generics}}>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<{{generics}}>
            {
                var innerJob = new IForEachWithEntityJob<T,{{generics}}>();
                innerJob.ForEach = iForEach;

                InlineParallelChunkQuery(in description, innerJob);
            }
            """;

        builder.AppendLine(template);
    }
}
