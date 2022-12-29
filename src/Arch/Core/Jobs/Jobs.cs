using CommunityToolkit.HighPerformance;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core;

/// <summary>
///     A default pooling policy for a class T.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DefaultObjectPolicy<T> : IPooledObjectPolicy<T> where T : class, new()
{
    public T Create()
    {
        return new();
    }

    public bool Return(T obj)
    {
        return true;
    }
}

/// <summary>
///     A struct containing a range to indicate a thread which part of an array it should iterate over.
/// </summary>
public readonly ref struct Range
{
    public Range(int start, int length)
    {
        Start = start;
        Length = length;
    }

    public readonly int Start;
    public readonly int Length;
}

/// <summary>
///     A parallel job which is executed upon a <see cref="Chunk" /> to execute logic on its entities.
/// </summary>
public interface IChunkJob
{
    public void Execute(int index, ref Chunk chunk);
}

/// <summary>
///     A <see cref="IChunkJob" /> which executes a <see cref="Core.ForEach" /> on each entity.
/// </summary>
public struct ForEachJob : IChunkJob
{
    public ForEach ForEach;

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

/// <summary>
///     A <see cref="IChunkJob" /> which executes a <see cref="Core.ForEach" /> on each entity.
/// </summary>
public struct IForEachJob<T> : IChunkJob where T : IForEach
{
    public T ForEach;

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

/// <summary>
///     A job which processes a slice of <see cref="Chunk" />'s and executes an <see cref="ForEach" /> for each entity.
///     Used for multithreading.
/// </summary>
public class ChunkIterationJob<T> : IJob where T : IChunkJob
{
    public ChunkIterationJob() { }

    public ChunkIterationJob(int start, int size, Chunk[] chunks)
    {
        Start = start;
        Size = size;
        Chunks = chunks;
    }

    public Chunk[] Chunks { get; set; }
    public T Instance { get; set; }
    public int Size { get; set; }

    public int Start;

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
