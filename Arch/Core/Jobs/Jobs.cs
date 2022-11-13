using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Arch.Core.Extensions;
using Collections.Pooled;
using JobScheduler;
using Microsoft.Extensions.ObjectPool;

using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;

namespace Arch.Core;

/// <summary>
/// A default pooling policy for a class T.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DefaultObjectPolicy<T> : IPooledObjectPolicy<T> where T : class, new() {
    public T Create() { return new T(); }
    public bool Return(T obj) { return true; }
}

/// <summary>
/// A struct containing a range to indicate a thread which part of an array it should iterate over. 
/// </summary>
public readonly ref struct Range {

    public readonly int start;
    public readonly int range;

    public Range(int start, int range) {
        this.start = start;
        this.range = range;
    }
}

/// <summary>
/// A parallel job which is executed upon a <see cref="Chunk"/> to execute logic on its entities. 
/// </summary>
public interface IChunkJob {

    public void Execute(int index, ref Chunk chunk);

}

/// <summary>
/// A <see cref="IChunkJob"/> which executes a <see cref="ForEach"/> on each entity. 
/// </summary>
public struct ForEachJob : IChunkJob {

    public ForEach forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {

        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {

            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            forEach(in entity);
        }
    }
}

/// <summary>
/// A <see cref="IChunkJob"/> which executes a <see cref="ForEach"/> on each entity. 
/// </summary>
public struct IForEachJob<T> : IChunkJob where T : IForEach{

    public T forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {

        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {

            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            forEach.Update(in entity);
        }
    }
}



/// <summary>
/// A job which processes a slice of <see cref="Chunk"/>'s and executes an <see cref="ForEach"/> for each entity.
/// Used for multithreading.
/// </summary>
public unsafe class ChunkIterationJob<T> : IJob where T : IChunkJob{

    public int start;
    public int size;

    public Chunk[] chunks;
    public T instance;

    public ChunkIterationJob(){}
    public ChunkIterationJob(int start, int size, Chunk[] chunks) {
        this.start = start;
        this.size = size;
        this.chunks = chunks;
    }

    public void Execute() {
            
        ref var chunk = ref ArrayExtensions.DangerousGetReferenceAt(chunks, start);
        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++) {

            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            instance.Execute(start+chunkIndex, ref currentChunk);
        }
    }
}


