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
            public JobHandle InlineParallelQuery<T, {{generics}}>(in QueryDescription queryDescription, ref T forEach, in JobHandle? dependency = null, int batchSize = 16)
                where T : struct, IForEach<{{generics}}>
            {
                var innerJob = new IForEachJob<T, {{generics}}>()
                {
                    ForEach = forEach
                };
                return InlineParallelChunkQuery(in queryDescription, in innerJob, in dependency, batchSize);
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
            public JobHandle InlineParallelEntityQuery<T, {{generics}}>(in QueryDescription queryDescription, ref T forEach,
                in JobHandle? dependency = null, int batchSize = 16)
                where T : struct, IForEachWithEntity<{{generics}}>
            {
                var innerJob = new IForEachWithEntityJob<T, {{generics}}>()
                {
                    ForEach = forEach
                };
                return InlineParallelChunkQuery(in queryDescription, in innerJob, in dependency, batchSize);
            }
            """;

        builder.AppendLine(template);
    }
}
