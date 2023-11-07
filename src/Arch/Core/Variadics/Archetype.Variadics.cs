using Arch.Core.Utils;

namespace Arch.Core;
public partial class Archetype
{
    /// <inheritdoc cref="Has{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    public bool Has<T0, T1>()
    {
        var componentId_T0 = Component<T0>.ComponentType.Id;
        // [Variadic: CopyLines]
        var componentId_T1 = Component<T1>.ComponentType.Id;
        return BitSet.IsSet(componentId_T0) &&
            // [Variadic: CopyLines]
            BitSet.IsSet(componentId_T1) &&
            true;
    }

    /// <inheritdoc cref="Get{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    internal unsafe Components<T0, T1> Get<T0, T1>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1>(slot.Index);
    }

    /// <inheritdoc cref="Set{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    // [Variadic: CopyParams(T1?)]
    internal void Set<T0, T1>(ref Slot slot, in T0? component_T0, in T1? component_T1)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        // [Variadic: CopyArgs(component)]
        chunk.Set<T0, T1>(slot.Index, in component_T0, in component_T1);
    }

    /// <inheritdoc cref="SetRange{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    // [Variadic: CopyParams(T1?)]
    internal void SetRange<T0, T1>(in Slot from, in Slot to, in T0? componentValue_T0 = default, in T1? componentValue_T1 = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var firstElement_T0 = ref chunk.GetFirst<T0>();
            // [Variadic: CopyLines]
            ref var firstElement_T1 = ref chunk.GetFirst<T1>();

            // Only move within the range, depening on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Size - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var component_T0 = ref Unsafe.Add(ref firstElement_T0, entityIndex);
                // [Variadic: CopyLines]
                ref var component_T1 = ref Unsafe.Add(ref firstElement_T1, entityIndex);
                component_T0 = componentValue_T0;
                // [Variadic: CopyLines]
                component_T1 = componentValue_T1;
            }
        }
    }
}
