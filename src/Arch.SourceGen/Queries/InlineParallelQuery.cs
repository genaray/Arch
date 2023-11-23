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

                var pool = JobMeta<ChunkIterationJob<IForEachJob<T,{{generics}}>>>.Pool;
                var query = Query(in description);
                foreach (var archetype in query.GetArchetypeIterator())
                {
                    var archetypeSize = archetype.ChunkCount;
                    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
                    foreach (var range in part)
                    {
                        var job = pool.Get();
                        job.Start = range.Start;
                        job.Size = range.Length;
                        job.Chunks = archetype.Chunks;
                        job.Instance = innerJob;
                        JobsCache.Add(job);
                    }

                    IJob.Schedule(JobsCache, JobHandles);
                    JobScheduler.JobScheduler.Instance.Flush();
                    JobHandle.Complete(JobHandles);
                    JobHandle.Return(JobHandles);

                    // Return jobs to pool
                    for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
                    {
                        var job = Unsafe.As<ChunkIterationJob<IForEachJob<T,{{generics}}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }

                    JobHandles.Clear();
                    JobsCache.Clear();
                }
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

                var pool = JobMeta<ChunkIterationJob<IForEachWithEntityJob<T,{{generics}}>>>.Pool;
                var query = Query(in description);
                foreach (var archetype in query.GetArchetypeIterator()) {

                    var archetypeSize = archetype.ChunkCount;
                    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
                    foreach (var range in part)
                    {
                        var job = pool.Get();
                        job.Start = range.Start;
                        job.Size = range.Length;
                        job.Chunks = archetype.Chunks;
                        job.Instance = innerJob;
                        JobsCache.Add(job);
                    }

                    IJob.Schedule(JobsCache, JobHandles);
                    JobScheduler.JobScheduler.Instance.Flush();
                    JobHandle.Complete(JobHandles);
                    JobHandle.Return(JobHandles);

                    // Return jobs to pool
                    for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
                    {
                        var job = Unsafe.As<ChunkIterationJob<IForEachWithEntityJob<T,{{generics}}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }

                    JobHandles.Clear();
                    JobsCache.Clear();
                }
            }
            """;

        builder.AppendLine(template);
    }
}
