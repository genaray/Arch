using CommunityToolkit.HighPerformance;
using Microsoft.Extensions.ObjectPool;
using Schedulers;

namespace Arch.Core;

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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
///     The <see cref="ChunkIterationJob{T}"/> class
///     is an <see cref="IJob"/> that can be scheduled using the <see cref="JobScheduler"/> and the <see cref="World"/> to iterate multithreaded over chunks.
/// </summary>
/// <typeparam name="T">The generic type that implements the <see cref="IChunkJob"/> interface.</typeparam>
public sealed class ChunkIterationJob<T> : IJobParallelFor where T : IChunkJob
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

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class.
    /// </summary>
    public ChunkIterationJob()
    {
        Parts = new List<ChunkIterationPart>();
    }

    /// <summary>
    /// An instance of the generic type <typeparamref name="T"/>, being invoked upon each chunk.
    /// </summary>
    public T? Instance { get; set; }

    /// <summary>
    /// From the start how many chunks are processed.
    /// </summary>
    public int Size { get; set; }


    private List<ChunkIterationPart> Parts { get; set; }

    public int ThreadCount { get; } = Environment.ProcessorCount;
    public int BatchSize { get; } = 16;

    /// <summary>
    /// Add an array of chunks to be processed by this job.
    /// </summary>
    /// <param name="chunks">The chunks to add.</param>
    /// <param name="start">The first chunk to process in <paramref name="chunks"/></param>
    /// <param name="size">The amount of chunks to process in <paramref name="chunks"/></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddChunks(Chunk[] chunks, int start, int size)
    {
        Parts.Add(new ChunkIterationPart{
            Chunks = chunks,
            Start = start,
            Size = size
        });
    }

    public void Execute(int index)
    {
        var sizeSoFar = 0;
        for (var i = 0; i < Parts.Count; i++)
        {
            // If we're about to go over, we're ready to execute
            var part = Parts[i];
            if (sizeSoFar + part.Size > index)
            {
                // this had better be not null!
                ref var chunk = ref part.Chunks!.DangerousGetReferenceAt(index - sizeSoFar + part.Start);
                Instance?.Execute(ref chunk);
                return;
            }

            sizeSoFar += part.Size;
        }

        throw new InvalidOperationException("Reached end of chunk, but could not find the correct index!");
    }

    public void Finish()
    {
        Parts.Clear();
    }
}
