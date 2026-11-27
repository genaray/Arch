using Arch.Core.Utils;
using Arch.LowLevel;
using Collections.Pooled;
using CommunityToolkit.HighPerformance;
using Schedulers;
using Schedulers.Utils;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

// Multithreading / Parallel

public partial class World
{

    /// <summary>
    ///     A list of <see cref="JobHandle"/> which are pooled to avoid allocs.
    /// </summary>
    private NetStandardList<JobHandle> JobHandles { get; }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/> delegate.
    ///     Runs multithreaded and will process the matching <see cref="Entity"/>'s in parallel.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Entity"/>'s are searched for.</param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>

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
    ///     If the <paramref name="innerJob"/> throws, all threads finish their work first, and the exception is thrown afterwards.
    ///     The calling thread processes <see cref="Chunk"/>s too, so a small query runs on it alone and is not slower than a serial one.
    ///     Every <see cref="Chunk"/> is processed with its own copy of <paramref name="innerJob"/>, and the <see cref="Chunk"/>s are handed out dynamically to the threads, so the work balances itself.
    /// </remarks>
    ///  <remarks>
    ///     Does not allocate, the dispatcher and the jobs for the helper threads are pooled.
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>

    public void InlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (SharedJobScheduler is null)
        {
            throw new Exception($"SharedJobScheduler is missing, assign an instance to {nameof(World)}.{nameof(SharedJobScheduler)}. This singleton used for parallel iterations.");
        }

        var dispatcher = JobMeta<JobChunkDispatcher>.Pool.Get();
        foreach (var archetype in Query(in queryDescription).GetArchetypeIterator())
        {
            dispatcher.Add(archetype);
        }

        // The calling thread works too, so small queries need no helpers at all and never wait for a thread to wake up.
        var helperCount = dispatcher.GetHelperCount(SharedJobScheduler.ThreadCount);
        dispatcher.Start(helperCount + 1);
        var handle = ScheduleHelpers(dispatcher, innerJob, helperCount);

        // The helpers must be done before leaving, even if the job throws, as they work on chunks the caller is about to use again.
        try
        {
            dispatcher.Run(innerJob);
        }
        finally
        {
            try
            {
                SharedJobScheduler.Wait(handle);
            }
            finally
            {
                // This thread is the last one done, the helpers are finished after waiting. The dispatcher goes back to its pool.
                dispatcher.ThreadDone();
            }
        }
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>'s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them.
    /// </summary>
    /// <remarks>
    ///     NOT thread-safe! Do not call a parallel query from anything but the main thread!
    /// </remarks>
    /// <remarks>
    ///     Does not wait, wait for the returned <see cref="JobHandle"/> with <see cref="JobScheduler.Wait(JobHandle)"/> before the <see cref="Chunk"/>s are used or changed.
    ///     Does not allocate, the dispatcher and the jobs for the helper threads give themselves back once they are done.
    /// </remarks>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <exception cref="Exception">An <see cref="Exception"/> if the <see cref="JobScheduler"/> was not initialized before.</exception>

    public JobHandle ScheduleInlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob) where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (SharedJobScheduler is null)
        {
            throw new Exception("JobScheduler was not initialized, create one instance of JobScheduler. This creates a singleton used for parallel iterations.");
        }

        var dispatcher = JobMeta<JobChunkDispatcher>.Pool.Get();
        foreach (var archetype in Query(in queryDescription).GetArchetypeIterator())
        {
            dispatcher.Add(archetype);
        }

        // Nobody else works on the chunks, so at least one helper is needed.
        var helperCount = Math.Max(1, dispatcher.GetHelperCount(SharedJobScheduler.ThreadCount));
        dispatcher.Start(helperCount);
        return ScheduleHelpers(dispatcher, innerJob, helperCount);
    }

    /// <summary>
    ///     Schedules jobs which help processing the <see cref="Chunk"/>s of a <see cref="JobChunkDispatcher"/> and flushes them to the worker threads.
    /// </summary>
    /// <param name="dispatcher">The <see cref="JobChunkDispatcher"/> which hands out the <see cref="Chunk"/>s.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked on each <see cref="Chunk"/>.</param>
    /// <param name="helperCount">The amount of jobs to schedule.</param>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface.</typeparam>
    /// <returns>A <see cref="JobHandle"/> that is finished once all jobs are done, or a default one if there are no jobs.</returns>
    private JobHandle ScheduleHelpers<T>(JobChunkDispatcher dispatcher, in T innerJob, int helperCount) where T : struct, IChunkJob
    {
        if (helperCount == 0)
        {
            return default;
        }

        // The first job is the parent of all others, so waiting for its handle means waiting for all helpers.
        var scheduler = SharedJobScheduler!;
        var parent = default(JobHandle);
        for (var index = 0; index < helperCount; index++)
        {
            var job = new ChunkHelperJob<T> { Dispatcher = dispatcher, Instance = innerJob };

            var handle = index == 0 ? scheduler.Schedule(job) : scheduler.Schedule(job, parent);
            parent = index == 0 ? handle : parent;
            JobHandles.Add(handle);
        }

        scheduler.Flush(JobHandles.AsSpan());
        JobHandles.Clear();

        return parent;
    }
}
