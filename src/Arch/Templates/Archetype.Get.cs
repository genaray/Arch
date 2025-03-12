

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class Archetype
{
    internal unsafe Components<T0, T1> Get<T0, T1>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2> Get<T0, T1, T2>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3> Get<T0, T1, T2, T3>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4> Get<T0, T1, T2, T3, T4>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5> Get<T0, T1, T2, T3, T4, T5>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6> Get<T0, T1, T2, T3, T4, T5, T6>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7> Get<T0, T1, T2, T3, T4, T5, T6, T7>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(slot.Index);
    }

    internal unsafe Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(scoped ref Slot slot)
    {
        ref var chunk = ref GetChunk(slot.ChunkIndex);
        return chunk.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(slot.Index);
    }

}
