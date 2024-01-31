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
            public void ParallelQuery<{{generics}}>(in QueryDescription description, ForEach<{{generics}}> forEach)
            {
                var innerJob = new ForEachJob<{{generics}}>();
                innerJob.ForEach = forEach;

                InlineParallelChunkQuery(in description, innerJob);
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
            public void ParallelQuery<{{generics}}>(in QueryDescription description, ForEachWithEntity<{{generics}}> forEach)
            {
                var innerJob = new ForEachWithEntityJob<{{generics}}>();
                innerJob.ForEach = forEach;

                InlineParallelChunkQuery(in description, innerJob);
            }
            """;

        sb.AppendLine(template);
        return sb;
    }
}
