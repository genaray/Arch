using System.Threading;
using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.ObjectPool;
using Schedulers;

namespace Arch.Core;

/// <summary>
///     The <see cref="JobMeta{T}"/> class registers each job during compiletime to ensure static access to some information, which is more efficient.
/// </summary>
/// <typeparam name="T">The job struct generic type..</typeparam>
/// /// <remarks>
///     A <see cref="JobMeta{T}"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class JobMeta<T> where T : class, new()
{
    /// <summary>
    ///     How many jobs the <see cref="Pool"/> keeps. A parallel query takes one job per helper thread at once,
    ///     the default of twice the processor count would drop and reallocate jobs if the scheduler runs more threads than that.
    /// </summary>
    private const int MaximumRetainedJobs = 1024;

    /// <summary>
    ///     Creates a compiletime static instance of this job.
    /// </summary>
    static JobMeta()
    {
        Id = JobMeta.Id++;
        Policy = new DefaultObjectPolicy<T>();
        Pool = new DefaultObjectPool<T>(Policy, MaximumRetainedJobs);
    }

    /// <summary>
    ///     The unique Id of the job.
    /// </summary>
    public static readonly int Id;

    /// <summary>
    ///     The pool policy of the registered job.
    ///     Used for <see cref="Pool"/>.
    /// </summary>
    public static readonly DefaultObjectPolicy<T> Policy;

    /// <summary>
    ///     The pool of the job.
    ///     So that during multithreading new jobs are not permanently associated, which is better for efficiency.
    /// </summary>
    public static readonly DefaultObjectPool<T> Pool;
}

/// <summary>
///     The <see cref="DefaultObjectPolicy{T}"/> class is a pool policy that creates and returns any generic object in the same way.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public sealed class DefaultObjectPolicy<T> : IPooledObjectPolicy<T> where T : class, new()
{
    /// <summary>
    ///     Creates an instance of the generic type <typeparamref name="T"/>.
    /// </summary>
    /// <returns>A new instance of <typeparamref name="T"/>.</returns>
    public T Create()
    {
        return new();
    }

    /// <summary>
    ///     Returns an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <param name="obj">The instance.</param>
    /// <returns>True if it was returned sucessfully.</returns>
    public bool Return(T obj)
    {
        return true;
    }
}

// NOTE: Should this perhaps have a different name so that it doesn't get confused with `System.Range`?
/// <summary>
///     The <see cref="Core.Range"/> struct represents a section of an array.
/// </summary>
public readonly ref struct Range
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Core.Range"/> struct.
    /// </summary>
    /// <param name="start">Its start index.</param>
    /// <param name="length">Its length, beginning from the start index.</param>
    public Range(int start, int length)
    {
        Start = start;
        Length = length;
    }

    /// <summary>
    ///     The start index of the array section.
    /// </summary>
    public readonly int Start;

    /// <summary>
    ///     The length, beginning from the <see cref="Start"/>.
    /// </summary>
    public readonly int Length;
}

/// <summary>
///     The <see cref="IChunkJob"/> interface
///     represents a parallel job which is executed on a <see cref="Chunk"/> to execute logic on its entities.
/// </summary>
public interface IChunkJob
{
    public void Execute(ref Chunk chunk);
}

/// <summary>
///     The <see cref="ForEachJob"/> struct
///     is an <see cref="IChunkJob"/>, executing <see cref="Core.ForEach"/> on each entity.
/// </summary>
public struct ForEachJob : IChunkJob
{

    /// <summary>
    ///     The <see cref="ForEach"/> callback invoked for each <see cref="Entity"/>;
    /// </summary>
    public ForEach ForEach;

    /// <summary>
    ///     Called on each <see cref="Chunk"/> and iterates over all <see cref="Entity"/>'s to call the <see cref="ForEach"/> callback for each.
    /// </summary>
    /// <param name="chunk">A reference to the chunk which is currently processed.</param>

    public readonly void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ForEach(entity);
        }
    }
}

// NOTE: Should this be `ForEachJob<T>` instead of `IForEachJob<T>`?
/// <summary>
///     The <see cref="IForEachJob{T}"/> struct
///     is an <see cref="IChunkJob"/>, executing <see cref="Core.ForEach"/> on each entity.
/// </summary>
/// <typeparam name="T">The generic type, inhereting from <see cref="IForEach{T0}"/>.</typeparam>
public struct IForEachJob<T> : IChunkJob where T : IForEach
{

    /// <summary>
    /// The <see cref="IForEach{T0}"/> interface reference being invoked.
    /// </summary>
    public T ForEach;

    /// <summary>
    ///     Called on each <see cref="Chunk"/> and iterates over all <see cref="Entity"/>'s to call the <see cref="ForEach"/> callback for each.
    /// </summary>
    /// <param name="index">The chunk index.</param>
    /// <param name="chunk">A reference to the chunk which is currently processed.</param>

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ForEach.Update(entity);
        }
    }
}

/// <summary>
///     The <see cref="JobChunkDispatcher"/> class
///     hands out the used <see cref="Chunk"/>s of several <see cref="Archetype"/>s to several threads, in small batches.
///     Every thread asks for its next batch once it is done with the previous one, so threads that are faster or have cheaper <see cref="Chunk"/>s simply process more of them
///     and the work balances itself, no matter how it is distributed.
/// </summary>
/// <remarks>
///     Fill it with <see cref="Add"/>, then call <see cref="Start"/> once and <see cref="Run{T}"/> on every thread that takes part.
///     <see cref="Chunk"/>s of all <see cref="Archetype"/>s share one index space, so many small <see cref="Archetype"/>s need no more jobs than one large one.
/// </remarks>
internal sealed class JobChunkDispatcher
{
    /// <summary>
    ///     The minimum amount of <see cref="Entity"/>s a helper thread should have, since scheduling a job costs more than processing fewer.
    /// </summary>
    private const int MinEntitiesPerHelper = 4096;

    /// <summary>
    ///     How many batches each thread should get on average. More batches than threads balance the load if some <see cref="Chunk"/>s take longer than others.
    /// </summary>
    private const int BatchesPerThread = 8;

    /// <summary>
    ///     The <see cref="Chunk"/> arrays of the added <see cref="Archetype"/>s.
    /// </summary>
    private Chunk[][] _chunkArrays = new Chunk[8][];

    /// <summary>
    ///     For every <see cref="Chunk"/> array the index behind its last used <see cref="Chunk"/> in the shared index space, which is the same as the first index of the next one.
    /// </summary>
    private int[] _endIndexes = new int[8];

    private int _arrayCount;
    private int _nextBatch;
    private int _batchSize = 1;

    /// <summary>
    ///     How many threads still have to call <see cref="ThreadDone"/> before the dispatcher is given back to its pool.
    /// </summary>
    private int _unfinishedThreads;

    /// <summary>
    ///     The amount of <see cref="Entity"/>s in all added <see cref="Archetype"/>s.
    /// </summary>
    internal int EntityCount { get; private set; }

    /// <summary>
    ///     The amount of used <see cref="Chunk"/>s in all added <see cref="Archetype"/>s.
    /// </summary>
    internal int ChunkCount
    {
        get => _arrayCount == 0 ? 0 : _endIndexes[_arrayCount - 1];
    }

    /// <summary>
    ///     Adds the used <see cref="Chunk"/>s of an <see cref="Archetype"/>, the allocated but unused ones at its end are skipped.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    internal void Add(Archetype archetype)
    {
        if (_arrayCount == _chunkArrays.Length)
        {
            Array.Resize(ref _chunkArrays, _arrayCount * 2);
            Array.Resize(ref _endIndexes, _arrayCount * 2);
        }

        _chunkArrays[_arrayCount] = archetype.Chunks;
        _endIndexes[_arrayCount] = ChunkCount + archetype.Count + 1;
        _arrayCount++;

        EntityCount += archetype.EntityCount;
    }

    /// <summary>
    ///     Calculates how many helper threads are worth to wake up for the added <see cref="Chunk"/>s, from none to all available ones.
    /// </summary>
    /// <param name="availableThreads">The amount of threads that can help.</param>
    /// <returns>The amount of helper threads.</returns>
    internal int GetHelperCount(int availableThreads)
    {
        return Math.Clamp(EntityCount / MinEntitiesPerHelper, 0, availableThreads);
    }

    /// <summary>
    ///     Prepares handing out the added <see cref="Chunk"/>s to a number of threads.
    /// </summary>
    /// <param name="threadCount">The amount of threads that call <see cref="Run{T}"/> and <see cref="ThreadDone"/>.</param>
    internal void Start(int threadCount)
    {
        _batchSize = Math.Max(1, ChunkCount / (threadCount * BatchesPerThread));
        _nextBatch = 0;
        _unfinishedThreads = threadCount;
    }

    /// <summary>
    ///     Called by every thread that took part, once it is done with the <see cref="Chunk"/>s.
    ///     The last one gives the dispatcher back to its pool, so nobody has to know when that is.
    /// </summary>
    internal void ThreadDone()
    {
        if (Interlocked.Decrement(ref _unfinishedThreads) != 0)
        {
            return;
        }

        Clear();
        JobMeta<JobChunkDispatcher>.Pool.Return(this);
    }

    /// <summary>
    ///     Removes all added <see cref="Archetype"/>s, so the instance can be used again.
    /// </summary>
    internal void Clear()
    {
        Array.Clear(_chunkArrays, 0, _arrayCount);
        _arrayCount = 0;
        EntityCount = 0;
    }

    /// <summary>
    ///     Processes <see cref="Chunk"/>s with an <see cref="IChunkJob"/> until all of them are taken, by this and by all other threads running this method.
    ///     Every <see cref="Chunk"/> gets its own copy of the job.
    /// </summary>
    /// <param name="job">The <see cref="IChunkJob"/> that is called on each <see cref="Chunk"/>.</param>
    /// <typeparam name="T">The type of the <see cref="IChunkJob"/>.</typeparam>
    internal void Run<T>(in T job) where T : struct, IChunkJob
    {
        // Batches are taken in ascending order, so the array of the next chunk is never before the one of the last.
        var arrayIndex = 0;
        while (TryTakeBatch(out var chunkIndex, out var endIndex))
        {
            for (; chunkIndex < endIndex; chunkIndex++)
            {
                while (_endIndexes[arrayIndex] <= chunkIndex)
                {
                    arrayIndex++;
                }

                var firstIndex = arrayIndex == 0 ? 0 : _endIndexes[arrayIndex - 1];
                var jobCopy = job;
                jobCopy.Execute(ref _chunkArrays[arrayIndex][chunkIndex - firstIndex]);
            }
        }
    }

    /// <summary>
    ///     Takes the next batch of <see cref="Chunk"/>s, no other thread gets the same.
    /// </summary>
    /// <param name="startIndex">The index of the first <see cref="Chunk"/> of the batch.</param>
    /// <param name="endIndex">The index behind the last <see cref="Chunk"/> of the batch.</param>
    /// <returns>True if there was a batch left, false if all <see cref="Chunk"/>s are taken.</returns>
    private bool TryTakeBatch(out int startIndex, out int endIndex)
    {
        startIndex = Interlocked.Add(ref _nextBatch, _batchSize) - _batchSize;
        endIndex = Math.Min(startIndex + _batchSize, ChunkCount);
        return startIndex < ChunkCount;
    }
}

/// <summary>
///     The <see cref="ChunkHelperJob{T}"/> struct
///     is an <see cref="IJob"/> that helps processing the <see cref="Chunk"/>s of a <see cref="JobChunkDispatcher"/> on another thread.
/// </summary>
/// <remarks>
///     It is a struct, so the <see cref="JobScheduler"/> carries it without boxing and pools the wrapper itself. There is nothing to give back after it ran.
/// </remarks>
/// <typeparam name="T">The generic type that implements the <see cref="IChunkJob"/> interface.</typeparam>
internal struct ChunkHelperJob<T> : IJob where T : struct, IChunkJob
{
    /// <summary>
    ///     The <see cref="JobChunkDispatcher"/> which hands out the <see cref="Chunk"/>s.
    /// </summary>
    internal JobChunkDispatcher Dispatcher;

    /// <summary>
    ///     An instance of the generic type <typeparamref name="T"/>, being invoked upon each chunk.
    /// </summary>
    internal T Instance;

    /// <summary>
    ///     Processes <see cref="Chunk"/>s until the <see cref="Dispatcher"/> has none left.
    /// </summary>
    public void Execute()
    {
        Dispatcher.Run(Instance);
        Dispatcher.ThreadDone();
    }
}
