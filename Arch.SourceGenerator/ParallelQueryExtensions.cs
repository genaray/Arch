using System.Text;
using CodeGenHelpers;

namespace ArchSourceGenerator; 

public static class StringBuilderParallelQueryExtensions {

    public static void AppendParallelQuerys(this ClassBuilder builder, int amount) {

        for (var index = 0; index < amount; index++) 
            builder.AppendParallelQuery(index);
    }
    
    public static void AppendParallelQuery(this ClassBuilder builder, int amount) {
        
        var methodBuilder = builder.AddMethod("ParallelQuery").MakePublicMethod().WithReturnType("void");
        methodBuilder.AddParameter("in QueryDescription", "description");
        methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");
        
        var generics = new StringBuilder().Generic(amount).ToString();
        methodBuilder.AddParameter($"ForEach{generics}", "forEach");

        for (var index = 0; index <= amount; index++)
            methodBuilder.AddGeneric($"T{index}");
        
        methodBuilder.WithBody(writer => {

            var generics = new StringBuilder().GenericWithoutBrackets(amount);
            
            var template = 
                $@"
var innerJob = new ForEachJob<{generics}>();
innerJob.forEach = forEach;

var listCache = GetListCache<ChunkIterationJob<ForEachJob<{generics}>>>();

var query = Query(in description);
foreach (ref var archetype in query.GetArchetypeIterator()) {{

    var archetypeSize = archetype.Size;
    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
    foreach (var range in part) {{
    
        var job = GetJob<ChunkIterationJob<ForEachJob<{generics}>>>();
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
";
            writer.AppendLine(template);
        });
    }
    
    public static void AppendParallelEntityQuerys(this ClassBuilder builder, int amount) {

        for (var index = 0; index < amount; index++) 
            builder.AppendParallelEntityQuery(index);
    }
    
    public static void AppendParallelEntityQuery(this ClassBuilder builder, int amount) {
        
        var methodBuilder = builder.AddMethod("ParallelQuery").MakePublicMethod().WithReturnType("void");
        methodBuilder.AddParameter("in QueryDescription", "description");
        methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");
        
        var generics = new StringBuilder().Generic(amount).ToString();
        methodBuilder.AddParameter($"ForEachWithEntity{generics}", "forEach");

        for (var index = 0; index <= amount; index++)
            methodBuilder.AddGeneric($"T{index}");
        
        methodBuilder.WithBody(writer => {

            var generics = new StringBuilder().GenericWithoutBrackets(amount);
            
            var template = 
                $@"
var innerJob = new ForEachWithEntityJob<{generics}>();
innerJob.forEach = forEach;

var listCache = GetListCache<ChunkIterationJob<ForEachWithEntityJob<{generics}>>>();

var query = Query(in description);
foreach (ref var archetype in query.GetArchetypeIterator()) {{

    var archetypeSize = archetype.Size;
    var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
    foreach (var range in part) {{
    
        var job = GetJob<ChunkIterationJob<ForEachWithEntityJob<{generics}>>>();
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
";
            writer.AppendLine(template);
        });
    }

}