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

                var pool = JobMeta<ChunkIterationJob<ForEachJob<{{generics}}>>>.Pool;
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
                        var job = Unsafe.As<ChunkIterationJob<ForEachJob<{{generics}}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }

                    JobHandles.Clear();
                    JobsCache.Clear();
                }
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

                var pool = JobMeta<ChunkIterationJob<ForEachWithEntityJob<{{generics}}>>>.Pool;
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
                        var job = Unsafe.As<ChunkIterationJob<ForEachWithEntityJob<{{generics}}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }

                    JobHandles.Clear();
                    JobsCache.Clear();
                }
            }
            """;

        sb.AppendLine(template);
        return sb;
    }
}
