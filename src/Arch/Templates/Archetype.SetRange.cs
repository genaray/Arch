

// TODO: Generated code is slightly outdated, migrate?

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class Archetype
{
    internal void SetRange<T0, T1>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default,in T23? t23ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            ref var t23FirstElement = ref chunk.GetFirst<T23>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                ref var t23Component = ref Unsafe.Add(ref t23FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                t23Component = t23ComponentValue;
                
            }
        }
    }

    internal void SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in Slot from, in Slot to, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default,in T23? t23ComponentValue = default,in T24? t24ComponentValue = default)
    {
        // Set the added component, start from the last slot and move down
        for (var chunkIndex = from.ChunkIndex; chunkIndex >= to.ChunkIndex; --chunkIndex)
        {
            ref var chunk = ref GetChunk(chunkIndex);
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            ref var t23FirstElement = ref chunk.GetFirst<T23>();
            ref var t24FirstElement = ref chunk.GetFirst<T24>();
            

            // Only move within the range, depending on which chunk we are at.
            var isStart = chunkIndex == from.ChunkIndex;
            var isEnd = chunkIndex == to.ChunkIndex;

            var upper = isStart ? from.Index : chunk.Count - 1;
            var lower = isEnd ? to.Index : 0;

            for (var entityIndex = upper; entityIndex >= lower; --entityIndex)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                ref var t23Component = ref Unsafe.Add(ref t23FirstElement, entityIndex);
                ref var t24Component = ref Unsafe.Add(ref t24FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                t23Component = t23ComponentValue;
                t24Component = t24ComponentValue;
                
            }
        }
    }

}
