namespace Arch.Core;

/// <inheritdoc cref="ForEachJob"/>
[Variadic(nameof(T0), 24)]
public struct ForEachJob<T0> : IChunkJob
{
    /// <inheritdoc cref="ForEachJob.ForEach"/>
    public ForEach<T0> ForEach;

    /// <inheritdoc cref="ForEachJob.Execute"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        // [Variadic: CopyLines]
        ref var firstElement_T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            // [Variadic: CopyLines]
            ref var component_T0 = ref Unsafe.Add(ref firstElement_T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach(ref component_T0);
        }
    }
}

/// <inheritdoc cref="ForEachJob"/>
[Variadic(nameof(T0), 24)]
public struct ForEachWithEntityJob<T0> : IChunkJob
{
    /// <inheritdoc cref="ForEachJob.ForEach"/>
    public ForEachWithEntity<T0> ForEach;

    /// <inheritdoc cref="ForEachJob.Execute"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Execute(int index, ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        // [Variadic: CopyLines]
        ref var firstElement_T0 = ref chunk.GetFirst<T0>();

        foreach (var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            // [Variadic: CopyLines]
            ref var component_T0 = ref Unsafe.Add(ref firstElement_T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach(entity, ref component_T0);
        }
    }
}

/// <inheritdoc cref="IForEachJob{T}"/>
[Variadic(nameof(T0), 24)]
public struct IForEachJob<T, T0> : IChunkJob where T : struct, IForEach<T0>
{
    /// <inheritdoc cref="IForEachJob{T}.ForEach"/>
    public T ForEach;

    /// <inheritdoc cref="IForEachJob{T}.Execute"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        // [Variadic: CopyLines]
        ref var firstElement_T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            // [Variadic: CopyLines]
            ref var component_T0 = ref Unsafe.Add(ref firstElement_T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach.Update(ref component_T0);
        }
    }
}

[Variadic(nameof(T0), 24)]
public struct IForEachWithEntityJob<T, T0> : IChunkJob where T : struct, IForEachWithEntity<T0>
{
    /// <inheritdoc cref="IForEachJob{T}.ForEach"/>
    public T ForEach;

    /// <inheritdoc cref="IForEachJob{T}.Execute"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref chunk.Entity(0);
        // [Variadic: CopyLines]
        ref var firstElement_T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            // [Variadic: CopyLines]
            ref var component_T0 = ref Unsafe.Add(ref firstElement_T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach.Update(entity, ref component_T0);
        }
    }
}
