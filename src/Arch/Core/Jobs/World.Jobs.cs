using Schedulers;

// ReSharper disable once CheckNamespace
namespace Arch.Core;

// Multithreading / Parallel

public partial class World
{
    /// <summary>
    /// Thrown when the <see cref="World"/> has not been assigned a <see cref="JobScheduler"/>.
    /// </summary>
    public class JobSchedulerNotAssignedException : Exception
    {
        internal JobSchedulerNotAssignedException()
            : base($"{nameof(World.Scheduler)} is not assigned! Call {nameof(World.AttachScheduler)} to assign a scheduler first.") { }
    }

    /// <summary>
    /// Thrown when a scheduling method has been called on a different thread.
    /// </summary>
    public class NotOnMainThreadException : Exception
    {
        internal NotOnMainThreadException()
            : base($"A scheduling method cannot be called on a different thread than {nameof(World.Scheduler)} was created on. " +
                  $"Either create the {nameof(JobScheduler)} on a different thread, or only schedule queries on the main thread.") { }
    }

    /// <summary>
    /// The <see cref="JobScheduler"/> attached to this <see cref="World"/>, or null if none has been attached.
    /// </summary>
    public JobScheduler? Scheduler { get; private set; }

    /// <summary>
    /// Attach a <see cref="JobScheduler"/> to this <see cref="World"/>. Only one scheduler can be attached, and it cannot
    /// be changed once set.
    /// </summary>
    /// <param name="scheduler">The scheduler to assign.</param>
    public void AttachScheduler(JobScheduler scheduler)
    {
        if (Scheduler is not null)
        {
            throw new InvalidOperationException(
                $"The {nameof(Scheduler)} is already assigned! Once assigned, a {nameof(World.Scheduler)} cannot be changed.");
        }

        Scheduler = scheduler;
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the passed <see cref="ForEach"/> delegate.
    /// </summary>
    /// <param name="queryDescription"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='queryDescription']"/></param>
    /// <param name="forEntity">The <see cref="ForEach"/> delegate.</param>
    /// <param name="dependency"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='dependency']"/></param>
    /// <param name="batchSize"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='batchSize']"/></param>
    /// <inheritdoc cref="InlineParallelChunkQuery"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public JobHandle ParallelQuery(in QueryDescription queryDescription, ForEach forEntity, in JobHandle? dependency = null, int batchSize = 16)
    {
        var foreachJob = new ForEachJob
        {
            ForEach = forEntity
        };

        return InlineParallelChunkQuery(in queryDescription, foreachJob, in dependency, batchSize);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>'s by a <see cref="QueryDescription"/> and calls the <see cref="IForEach"/> struct.
    /// </summary>
    /// <inheritdoc cref="InlineParallelChunkQuery"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public JobHandle InlineParallelQuery<T>(in QueryDescription queryDescription, in JobHandle? dependency = null, int batchSize = 16) where T : struct, IForEach
    {
        var iForEachJob = new IForEachJob<T>();
        return InlineParallelChunkQuery(in queryDescription, iForEachJob, in dependency, batchSize);
    }

    /// <summary>
    ///     Searches all matching <see cref="Entity"/>s by a <see cref="QueryDescription"/> and calls the passed <see cref="IForEach"/> struct.
    /// </summary>
    /// <param name="queryDescription"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='queryDescription']"/></param>
    /// <param name="iForEach">The struct instance of the generic type being invoked.</param>
    /// <param name="dependency"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='dependency']"/></param>
    /// <param name="batchSize"><inheritdoc cref="InlineParallelChunkQuery{T}" path="/param[@name='batchSize']"/></param>
    /// <inheritdoc cref="InlineParallelChunkQuery"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public JobHandle InlineParallelQuery<T>(in QueryDescription queryDescription, ref T iForEach, in JobHandle? dependency = null, int batchSize = 16)
        where T : struct, IForEach
    {
        var innerJob = new IForEachJob<T>()
        {
            ForEach = iForEach
        };
        return InlineParallelChunkQuery(in queryDescription, in innerJob, in dependency, batchSize);
    }

    /// <summary>
    ///     Finds all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls an <see cref="IChunkJob"/> on them in parallel.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A job is scheduled and will only begin once the scheduler is flushed. Unlike normal scheduled queries, this query is parallelized, meaning it
    ///         runs across all available cores at once. However, parallelization incurs additional overhead. Always benchmark to decide whether to use a parallel
    ///         scheduled query or a normal scheduled query when running multithreaded queries.
    ///     </para>
    ///     <para>
    ///         Additionally, as with all scheduled queries, this query is only valid when called from the main thread.
    ///         It will throw <see cref="NotOnMainThreadException"/> otherwise.
    ///     </para>
    /// </remarks>
    /// <returns>The <see cref="JobHandle"/> associated with the scheduled jobs.</returns>
    /// <typeparam name="T">A struct implementation of the <see cref="IChunkJob"/> interface which is called on each <see cref="Chunk"/> found.</typeparam>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which <see cref="Chunk"/>'s are searched for.</param>
    /// <param name="innerJob">The struct instance of the generic type being invoked.</param>
    /// <param name="dependency">A <see cref="JobHandle"/> that must complete beforehand.</param>
    /// <param name="batchSize">The number of chunks to process per batch. See <seealso cref="IJobParallelFor.BatchSize"/> for more information.</param>
    /// <exception cref="JobSchedulerNotAssignedException">If <see cref="AttachScheduler"/> has not been called.</exception>
    /// <exception cref="NotOnMainThreadException">If the method is called on a thread other than the scheduler's thread.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public JobHandle InlineParallelChunkQuery<T>(in QueryDescription queryDescription, in T innerJob, in JobHandle? dependency = null, int batchSize = 16)
        where T : struct, IChunkJob
    {
        // Job scheduler needs to be initialized.
        if (Scheduler is null)
        {
            throw new JobSchedulerNotAssignedException();
        }

        if (!Scheduler.IsMainThread)
        {
            throw new NotOnMainThreadException();
        }

        var query = Query(in queryDescription);
        var job = ChunkIterationJob<T>.GetPooled();
        job.Instance = innerJob;
        int size = 0;
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                job.AddChunks(archetype.Chunks, range.Start, range.Length);
                size += range.Length;
            }
        }

        return Scheduler.Schedule(job, size, dependency);
    }
}
