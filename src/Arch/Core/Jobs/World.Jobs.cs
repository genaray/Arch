using Arch.Core.Utils;
using Collections.Pooled;
using Schedulers;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

// Multithreading / Parallel

public partial class World
{

    /// <summary>
    ///     A list of <see cref="JobHandle"/> which are pooled to avoid allocs.
    /// </summary>
    private PooledList<JobHandle> JobHandles { get; }

    /// <summary>
    ///     A cache used for the parallel queries to prevent list allocations.
    /// </summary>
    internal List<IJob> JobsCache { get; set; }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/> delegate.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ParallelQuery(in QueryDescription queryDescription, ForEach forEntity)
    {
        var foreachJob = new ForEachJob
        {
            ForEach = forEntity
        };

        InlineParallelChunkQuery(in queryDescription, foreachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelQuery<T>(in QueryDescription queryDescription) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        InlineParallelChunkQuery(in queryDescription, iForEachJob);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IForEach"/> interface which is called on each <see cref="Entity"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelQuery<T>(in QueryDescription queryDescription, in IForEachJob<T> iForEach) where T : struct, IForEach
    {
        InlineParallelChunkQuery(in queryDescription, in iForEach);
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>'s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <remarks>
    ///     Processes <see cref="Chunk"/>s parallel, but blocks the thread until all <see cref="Chunk"/>s are processed.
    /// </remarks>
    ///  <remarks>
    ///     Pools generated <see cref="ChunkIterationJob{T}"/>s internally to avoid garbage.
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void InlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (SharedJobScheduler is null)
        {
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        }

        // Cast pool in an unsafe fast way and run the query.
        var pool = JobMeta<ChunkIterationJob<T>>.Pool;
        var query = Query(in queryDescription);
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

                var jobHandle = SharedJobScheduler.Schedule(job);
                JobsCache.Add(job);
                JobHandles.Add(jobHandle);
            }

            // Schedule, flush, wait, return.
            var handle = SharedJobScheduler.CombineDependencies(JobHandles.Span);
            SharedJobScheduler.Flush();
            handle.Complete();

            for (var index = 0; index < JobsCache.Count; index++)
            {
                var job = Unsafe.As<ChunkIterationJob<T>>(JobsCache[index]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>'s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public JobHandle ScheduleInlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (SharedJobScheduler is null)
        {
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        }

        // Cast pool in an unsafe fast way and run the query.
        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.ChunkCount;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = new ChunkIterationJob<T>
                {
                    Start = range.Start,
                    Size = range.Length,
                    Chunks = archetype.Chunks,
                    Instance = innerJob
                };

                var jobHandle = SharedJobScheduler.Schedule(job);
                JobHandles.Add(jobHandle);
            }
        }

        // Schedule, flush, wait, return.
        var handle = SharedJobScheduler.CombineDependencies(JobHandles.Span);
        SharedJobScheduler.Flush();
        JobHandles.Clear();

        return handle;
    }
}
