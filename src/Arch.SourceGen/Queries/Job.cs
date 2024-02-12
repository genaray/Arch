namespace Arch.SourceGen;

public static class StringBuilderChunkJobExtensions
{
    public static void AppendForEachJobs(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendForEachJob(index);
        }
    }

    public static void AppendForEachJob(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""
            public struct ForEachJob<{{generics}}> : IChunkJob
            {
                public ForEach<{{generics}}> ForEach;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Execute(ref Chunk chunk)
                {
                    var chunkSize = chunk.Size;
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        {{getComponents}}
                        ForEach({{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
    }

    public static void AppendEntityForEachJobs(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendEntityForEachJob(index);
        }
    }

    public static void AppendEntityForEachJob(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""
            public struct ForEachWithEntityJob<{{generics}}> : IChunkJob
            {
                public ForEachWithEntity<{{generics}}> ForEach;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Execute(ref Chunk chunk)
                {
                    ref var entityFirstElement = ref chunk.Entity(0);
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                        {{getComponents}}

                        ForEach(entity, {{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
    }

    public static void AppendIForEachJobs(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendIForEachJob(index);
        }
    }

    public static void AppendIForEachJob(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""
            public struct IForEachJob<T,{{generics}}> : IChunkJob where T : struct, IForEach<{{generics}}>
            {
                public T ForEach;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Execute(ref Chunk chunk)
                {
                    var chunkSize = chunk.Size;
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        {{getComponents}}
                        ForEach.Update({{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
    }

    public static void AppendIForEachWithEntityJobs(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendIForEachWithEntityJob(index);
        }
    }

    public static void AppendIForEachWithEntityJob(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""
            public struct IForEachWithEntityJob<T,{{generics}}> : IChunkJob where T : struct, IForEachWithEntity<{{generics}}>
            {
                public T ForEach;

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void Execute(ref Chunk chunk)
                {
                    var chunkSize = chunk.Size;
                    ref var entityFirstElement = ref chunk.Entity(0);
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                        {{getComponents}}
                        ForEach.Update(entity, {{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
    }
}
