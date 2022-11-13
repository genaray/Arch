using System.Text;
using CodeGenHelpers;

namespace ArchSourceGenerator; 

public static class StringBuilderHPParallelQueryExtensions {

    public static void AppendHPParallelQuerys(this StringBuilder builder, int amount) {

        for (var index = 0; index < amount; index++) 
            builder.AppendHPParallelQuery(index);
    }
    
    public static void AppendHPParallelQuery(this StringBuilder builder, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template = $@"
public partial class World{{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HPParallelQuery<T,{generics}>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<{generics}>{{
        
        var innerJob = new IForEachJob<T,{generics}>();
        innerJob.forEach = iForEach;

        var listCache = GetListCache<ChunkIterationJob<IForEachJob<T,{generics}>>>();

        var query = Query(in description);
        foreach (ref var archetype in query.GetArchetypeIterator()) {{

            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part) {{
            
                var job = GetJob<ChunkIterationJob<IForEachJob<T,{generics}>>>();
                job.start = range.start;
                job.size = range.range;
                job.chunks = archetype.Chunks;
                job.instance = innerJob;
                listCache.Add(job);
            }}

            IJob.Schedule(listCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < listCache.Count; jobIndex++) {{

                var job = listCache[jobIndex];
                ReturnJob(job);
            }}

            JobHandles.Clear();
            listCache.Clear();
        }}
    }}
}}
";

        builder.AppendLine(template);
    }
    
    public static void AppendHPEParallelQuerys(this StringBuilder builder, int amount) {

        for (var index = 0; index < amount; index++) 
            builder.AppendHPEParallelQuery(index);
    }
    
    public static void AppendHPEParallelQuery(this StringBuilder builder, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template = $@"
public partial class World{{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HPEParallelQuery<T,{generics}>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<{generics}>{{
        
        var innerJob = new IForEachWithEntityJob<T,{generics}>();
        innerJob.forEach = iForEach;

        var listCache = GetListCache<ChunkIterationJob<IForEachWithEntityJob<T,{generics}>>>();

        var query = Query(in description);
        foreach (ref var archetype in query.GetArchetypeIterator()) {{

            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part) {{
            
                var job = GetJob<ChunkIterationJob<IForEachWithEntityJob<T,{generics}>>>();
                job.start = range.start;
                job.size = range.range;
                job.chunks = archetype.Chunks;
                job.instance = innerJob;
                listCache.Add(job);
            }}

            IJob.Schedule(listCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < listCache.Count; jobIndex++) {{

                var job = listCache[jobIndex];
                ReturnJob(job);
            }}

            JobHandles.Clear();
            listCache.Clear();
        }}
    }}
}}
";

        builder.AppendLine(template);
    }
}