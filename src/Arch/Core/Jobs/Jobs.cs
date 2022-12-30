using CommunityToolkit.HighPerformance;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core;

// TODO: Documentation.
/// <summary>
///     The <see cref="DefaultObjectPolicy{T}"/> class
///     ...
/// </summary>
/// <typeparam name="T"></typeparam>
public class DefaultObjectPolicy<T> : IPooledObjectPolicy<T> where T : class, new()
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T Create()
    {
        return new();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Return(T obj)
    {
        return true;
    }
}

// NOTE: Should this perhaps have a different name so that it doesn't get confused with `System.Range`?
// TODO: Documentation.
/// <summary>
///     The <see cref="Core.Range"/> struct
///     ...
/// </summary>
public readonly ref struct Range
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Core.Range"/> struct
    ///     ...
    /// </summary>
    /// <param name="start"></param>
    /// <param name="length"></param>
    public Range(int start, int length)
    {
        Start = start;
        Length = length;
    }

    public readonly int Start;
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
    // TODO: Documentation.
    public ForEach ForEach;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="chunk"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref chunk.Entities.DangerousGetReference();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            ForEach(in entity);
        }
    }
}

// NOTE: Should this be `ForEachJob<T>` instead of `IForEachJob<T>`?
// TODO: Documentation.
/// <summary>
///     The <see cref="IForEachJob{T}"/> struct
///     is an <see cref="IChunkJob"/>, executing <see cref="Core.ForEach"/> on each entity.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct IForEachJob<T> : IChunkJob where T : IForEach
{
    // TODO: Documentation.
    public T ForEach;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="chunk"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref chunk.Entities.DangerousGetReference();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            ForEach.Update(in entity);
        }
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="ChunkIterationJob{T}"/> class
///     ...
/// </summary>
/// <typeparam name="T"></typeparam>
public class ChunkIterationJob<T> : IJob where T : IChunkJob
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class
    ///     ...
    /// </summary>
    public ChunkIterationJob() { }

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="ChunkIterationJob{T}"/> class
    ///     ...
    /// </summary>
    /// <param name="start"></param>
    /// <param name="size"></param>
    /// <param name="chunks"></param>
    public ChunkIterationJob(int start, int size, Chunk[] chunks)
    {
        Start = start;
        Size = size;
        Chunks = chunks;
    }

    // TODO: Documentation.
    public Chunk[] Chunks { get; set; }
    public T Instance { get; set; }
    public int Size { get; set; }

    public int Start;

    // TODO: Documentation.
    /// <summary>
    /// 
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
