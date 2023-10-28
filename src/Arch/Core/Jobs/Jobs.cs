using System.Collections.Concurrent;
using CommunityToolkit.HighPerformance;
using Schedulers;

namespace Arch.Core;

/// <summary>
///     The <see cref="IChunkJob"/> interface
///     represents a parallel job which is executed on a <see cref="Chunk"/> to execute logic on its entities.
/// </summary>
public interface IChunkJob
{
    public void Execute(ref Chunk chunk);
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        // NOTE: this was iterated in reverse in the sourcegen; I'm not sure which was correct. Normalizing to forwards here but if we want reverse,
        // change it in both.
        foreach (var entityIndex in chunk)
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
public struct IForEachJob<T> : IChunkJob where T : struct, IForEach
{

    /// <summary>
    /// The <see cref="IForEach{T0}"/> interface reference being invoked.
    /// </summary>
    public T ForEach;

    /// <summary>
    ///     Called on each <see cref="Chunk"/> and iterates over all <see cref="Entity"/>'s to call the <see cref="ForEach"/> callback for each.
    /// </summary>
    /// <param name="chunk">A reference to the chunk which is currently processed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        // NOTE: this was iterated in reverse in the sourcegen; I'm not sure which was correct. Normalizing to forwards here but if we want reverse,
        // change it in both.
        foreach (var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ForEach.Update(entity);
        }
    }
}

internal static class ChunkIterationJobPool<T> where T : IChunkJob
{
    private static readonly ConcurrentQueue<ChunkIterationJob<T>> _pool = new();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ChunkIterationJob<T> Get()
    {
        if (!_pool.TryDequeue(out var item))
        {
            // start with, idk, 32 archetype capacity
            // if the job processes more archetypes, it'll have to resize
            item = new(32);
        }

        return item;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Return(ChunkIterationJob<T> job)
    {
        _pool.Enqueue(job);
    }
}

/// <summary>
///     The <see cref="ChunkIterationJob{T}"/> class
///     is an <see cref="IJob"/> that can be scheduled using the <see cref="JobScheduler"/> and the <see cref="World"/> to iterate multithreaded over chunks.
///     It automatically pools itself to an internal thread-safe pool once completed.
/// </summary>
/// <typeparam name="T">The generic type that implements the <see cref="IChunkJob"/> interface.</typeparam>
public class ChunkIterationJob<T> : IJobParallelFor where T : IChunkJob
{
    /// <summary>
    ///     Represents a section of chunk iteration from one archetype.
    /// </summary>
    private struct ChunkIterationPart
    {
        public int Start;
        public int Size;
        public Chunk[]? Chunks;
    }

    private ChunkIterationPart[] _parts;
    private int _partsCount;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ChunkIterationJob(int initialCapacity)
    {
        if (initialCapacity == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(initialCapacity));
        }

        _parts = new ChunkIterationPart[initialCapacity];
    }

    /// <summary>
    ///     Get a fresh pooled <see cref="ChunkIterationJob{T}"/>, ready for scheduling.
    ///     Once it is executed, it will automatically return itself to the pool.
    /// </summary>
    /// <returns>An empty <see cref="ChunkIterationJob{T}"/>, possibly reused from a previous execution.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ChunkIterationJob<T> GetPooled()
    {
        return ChunkIterationJobPool<T>.Get();
    }

    /// <summary>
    /// An instance of the generic <see cref="IChunkJob"/>, <typeparamref name="T"/>, being invoked upon each chunk.
    /// </summary>
    public T? Instance { get; set; }

    /// <inheritdoc/>
    public int ThreadCount { get; set; } = 0;

    /// <summary>
    ///     The amount of work to do in a thread, in chunks.
    /// </summary>
    /// <inheritdoc/>
    public int BatchSize { get; set; } = 16;

    /// <summary>
    ///     Iterates over all 
    /// </summary>
    /// <param name="index"></param>
    public void Execute(int index)
    {
        // Navigate to the correct chunk:
        // This will take O(n) to find the proper chunk, where n is the number of archetypes in the query.
        // The alternative is to spawn a separate parallel job for each archetype, which could overwhelm the scheduler easily. (the old behaviour).
        // A faster, but way more memory intensive solution: Store an array of size m, where m is the count of total chunks for this job. In that array,
        // keep a reference to a Chunks[] and an offset. That would mean each job needs to allocate O(m) memory where m is the max number of chunks the
        // job operates on. (currently it's O(n) where n is the max archetypes).
        // My instinct is that this is fast enough as-is; that this overhead is way less than the scheduling overhead and less than the work performed in a chunk.
        // But it needs benchmarking to find the optimal solution.
        int sizeSoFar = 0;
        for (int i = 0; i < _partsCount; i++)
        {
            // If we're about to go over, we're ready to execute
            if (sizeSoFar + _parts[i].Size > index)
            {
                // this had better be not null!
                ref var chunk = ref _parts[i].Chunks!.DangerousGetReferenceAt(index - sizeSoFar + _parts[i].Start);
                Instance?.Execute(ref chunk);
                return;
            }

            sizeSoFar += _parts[i].Size;
        }

        throw new InvalidOperationException("Reached end of chunk, but could not find the correct index!");
    }

    /// <summary>
    /// Automatically clear and pool this job once complete.
    /// </summary>
    public void Finish()
    {
        for (int i = 0; i < _partsCount; i++)
        {
            _parts[i].Chunks = null;
            _parts[i].Start = 0;
            _parts[i].Size = 0;
        }

       _partsCount = 0;
       ChunkIterationJobPool<T>.Return(this);
    }

    /// <summary>
    /// Add an array of chunks to be processed by this job.
    /// </summary>
    /// <param name="chunks">The chunks to add.</param>
    /// <param name="start">The first chunk to process in <paramref name="chunks"/></param>
    /// <param name="size">The amount of chunks to process in <paramref name="chunks"/></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddChunks(Chunk[] chunks, int start, int size)
    {
        if (_partsCount >= _parts.Length)
        {
            Array.Resize(ref _parts, _parts.Length * 2);
        }

        _parts[_partsCount] = new()
        {
            Chunks = chunks,
            Start = start,
            Size = size
        };

        _partsCount++;
    }
}
