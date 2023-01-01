using System.Text;
using CodeGenHelpers;
using Microsoft.CodeAnalysis;

namespace ArchSourceGenerator;

public static class StringBuilderParallelQueryExtensions
{
    public static void AppendProperties(this ClassBuilder builder, string name, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            var propertyBuilder = builder.AddProperty($"{name}{index}", Accessibility.Internal);
            propertyBuilder.SetType<object>();
            propertyBuilder.UseAutoProps(); 
        }
    }
    
    public static StringBuilder AppendParallelQuerys(this StringBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
            builder.AppendParallelQuery(index);

        return builder;
    }

    public static void AppendParallelQuery(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount).ToString();
        var whereT = new StringBuilder().GenericWhereStruct(amount);

        sb.Append($@"
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void ParallelQuery<{generics}>(in QueryDescription description, ForEach<{generics}> forEach) {whereT} {{
        
                var innerJob = new ForEachJob<{generics}>();
                innerJob.ForEach = forEach;

                var pool = JobMeta<ChunkIterationJob<ForEachJob<{generics}>>>.Pool;
                var query = Query(in description);
                foreach (ref var archetype in query.GetArchetypeIterator()) {{

                    var archetypeSize = archetype.Size;
                    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
                    foreach (var range in part) {{
    
                        var job = pool.Get();
                        job.Start = range.Start;
                        job.Size = range.Length;
                        job.Chunks = archetype.Chunks;
                        job.Instance = innerJob;
                        JobsCache.Add(job);
                    }}

                    IJob.Schedule(JobsCache, JobHandles);
                    JobScheduler.JobScheduler.Instance.Flush();
                    JobHandle.Complete(JobHandles);
                    JobHandle.Return(JobHandles);

                    // Return jobs to pool
                    for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++) {{

                        var job = Unsafe.As<ChunkIterationJob<ForEachJob<{generics}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }}

                    JobHandles.Clear();
                    JobsCache.Clear();
                }}
            }}
        
        ");
    }

    public static StringBuilder AppendParallelEntityQuerys(this StringBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
            builder.AppendParallelEntityQuery(index);

        return builder;
    }

    public static void AppendParallelEntityQuery(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount).ToString();
        var whereT = new StringBuilder().GenericWhereStruct(amount);

        sb.Append($@"
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void ParallelQuery<{generics}>(in QueryDescription description, ForEachWithEntity<{generics}> forEach) {whereT} {{
        
                var innerJob = new ForEachWithEntityJob<{generics}>();
                innerJob.ForEach = forEach;

                var pool = JobMeta<ChunkIterationJob<ForEachWithEntityJob<{generics}>>>.Pool;
                var query = Query(in description);
                foreach (ref var archetype in query.GetArchetypeIterator()) {{

                    var archetypeSize = archetype.Size;
                    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
                    foreach (var range in part) {{
    
                        var job = pool.Get();
                        job.Start = range.Start;
                        job.Size = range.Length;
                        job.Chunks = archetype.Chunks;
                        job.Instance = innerJob;
                        JobsCache.Add(job);
                    }}

                    IJob.Schedule(JobsCache, JobHandles);
                    JobScheduler.JobScheduler.Instance.Flush();
                    JobHandle.Complete(JobHandles);
                    JobHandle.Return(JobHandles);

                    // Return jobs to pool
                    for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++) {{

                        var job = Unsafe.As<ChunkIterationJob<ForEachWithEntityJob<{generics}>>>(JobsCache[jobIndex]);
                        pool.Return(job);
                    }}

                    JobHandles.Clear();
                    JobsCache.Clear();
                }}
            }}
        ");
    }
}