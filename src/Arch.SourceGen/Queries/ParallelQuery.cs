namespace Arch.SourceGen;

public static class StringBuilderParallelQueryExtensions
{
    public static StringBuilder AppendParallelQuerys(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendParallelQuery(index);
        }

        return sb;
    }

    public static StringBuilder AppendParallelQuery(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public JobHandle ParallelQuery<{{generics}}>(in QueryDescription queryDescription, ForEach<{{generics}}> forEntity, in JobHandle? dependency = null, int batchSize = 16)
            {
                var foreachJob = new ForEachJob<{{generics}}>
                {
                    ForEach = forEntity
                };
                    
                return InlineParallelChunkQuery(in queryDescription, foreachJob, in dependency, batchSize);
            }
            """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendParallelEntityQuerys(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendParallelEntityQuery(index);
        }

        return sb;
    }

    public static StringBuilder AppendParallelEntityQuery(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public JobHandle ParallelQuery<{{generics}}>(in QueryDescription queryDescription, ForEachWithEntity<{{generics}}> forEntity, in JobHandle? dependency = null, int batchSize = 16)
            {
                var foreachJob = new ForEachWithEntityJob<{{generics}}>
                {
                    ForEach = forEntity
                };
                    
                return InlineParallelChunkQuery(in queryDescription, foreachJob, in dependency, batchSize);
            }
            """;

        sb.AppendLine(template);
        return sb;
    }
}
