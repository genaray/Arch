namespace Arch.Core;

[Variadic(nameof(T0), 1, 25)]
public struct ForEachJob<T0> : IChunkJob
{
    public ForEach<T0> ForEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        // [Variadic: CopyLines(firstElement)]
        ref var firstElement__T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            // [Variadic: CopyLines(component, firstElement)]
            ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach(ref component__T0);
        }
    }
}

[Variadic(nameof(T0), 1, 25)]
public struct ForEachWithEntityJob<T0> : IChunkJob
{
    public ForEachWithEntity<T0> ForEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Execute(int index, ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        var chunkSize = chunk.Size;
        // [Variadic: CopyLines(firstElement)]
        ref var firstElement__T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            // [Variadic: CopyLines(component, firstElement)]
            ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach(entity, ref component__T0);
        }
    }
}

[Variadic(nameof(T0), 1, 25)]
public struct IForEachJob<T, T0> : IChunkJob where T : struct, IForEach<T0>
{
    public T ForEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        // [Variadic: CopyLines(firstElement)]
        ref var firstElement__T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            // [Variadic: CopyLines(component, firstElement)]
            ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach.Update(ref component__T0);
        }
    }
}

[Variadic(nameof(T0), 1, 25)]
public struct IForEachWithEntityJob<T, T0> : IChunkJob where T : struct, IForEachWithEntity<T0>
{
    public T ForEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk)
    {
        var chunkSize = chunk.Size;
        ref var entityFirstElement = ref chunk.Entity(0);
        // [Variadic: CopyLines(firstElement)]
        ref var firstElement__T0 = ref chunk.GetFirst<T0>();

        for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            // [Variadic: CopyLines(component, firstElement)]
            ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
            // [Variadic: CopyArgs(component)]
            ForEach.Update(entity, ref component__T0);
        }
    }
}
