using CommunityToolkit.HighPerformance;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core;

/// <summary>
///     The <see cref="DefaultObjectPolicy{T}"/> class is a pool policy that creates and returns any generic object in the same way.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public class DefaultObjectPolicy<T> : IPooledObjectPolicy<T> where T : class, new()
{
    /// <summary>
    ///     Creates an instance of the generic type <see cref="T"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="T"/>.</returns>
    public T Create()
    {
        return new();
    }

    /// <summary>
    ///     Returns an instance of <see cref="T"/>;
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
    public void Execute(int index, ref Chunk chunk);
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
    /// <param name="index">The chunk index.</param>
    /// <param name="chunk">A reference to the chunk which is currently processed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
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
    public void Execute(int index, ref Chunk chunk)
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
public class ChunkIterationJob<T> : IJob where T : IChunkJob
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class.
    /// </summary>
    public ChunkIterationJob()
    {
        Chunks = Array.Empty<Chunk>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class.
    /// </summary>
    /// <param name="start">The start at which this job begins to process the <see cref="Chunks"/>.</param>
    /// <param name="size">The size or lengths, how man <see cref="Chunks"/> this job will process.</param>
    /// <param name="chunks">The <see cref="Chunk"/> array being processed.</param>
    public ChunkIterationJob(int start, int size, Chunk[] chunks)
    {
        Start = start;
        Size = size;
        Chunks = chunks;
    }

    /// <summary>
    /// A <see cref="Chunk"/> array, this will be processed.
    /// </summary>
    public Chunk[] Chunks { get; set; }

    /// <summary>
    /// An instance of the generic type <see cref="T"/>, being invoked upon each chunk.
    /// </summary>
    public T Instance { get; set; }

    /// <summary>
    /// From the start how many chunks are processed.
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// The start index.
    /// </summary>
    public int Start;

    /// <summary>
    ///     Iterates over all <see cref="Chunks"/> between <see cref="Start"/> and <see cref="Size"/> and calls <see cref="Instance"/>.
    /// </summary>
    public void Execute()
    {
        ref var chunk = ref Chunks.DangerousGetReferenceAt(Start);

        for (var chunkIndex = 0; chunkIndex < Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            Instance.Execute(Start + chunkIndex, ref currentChunk);
        }
    }
}
