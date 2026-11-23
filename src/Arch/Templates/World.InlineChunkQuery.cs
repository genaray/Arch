

using System;

namespace Arch.Core;
public partial class World
{
    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0>
    {
        InlineChunkQuery<T, T0>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1>
    {
        InlineChunkQuery<T, T0, T1>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2>
    {
        InlineChunkQuery<T, T0, T1, T2>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3>
    {
        InlineChunkQuery<T, T0, T1, T2, T3>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>(), chunk.GetUsedSpan<T20>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>(), chunk.GetUsedSpan<T20>(), chunk.GetUsedSpan<T21>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>(), chunk.GetUsedSpan<T20>(), chunk.GetUsedSpan<T21>(), chunk.GetUsedSpan<T22>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>(), chunk.GetUsedSpan<T20>(), chunk.GetUsedSpan<T21>(), chunk.GetUsedSpan<T22>(), chunk.GetUsedSpan<T23>());
        }
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s by a <see cref="QueryDescription"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="description">The <see cref="QueryDescription"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in QueryDescription description, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
    {
        InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Query(in description), ref iChunkForEach);
    }

    /// <summary>
    ///     Searches all matching <see cref="Chunk"/>s of a <see cref="Core.Query"/> and calls the <see cref="IChunkForEach{T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24}"/> struct once for each,
    ///     with the components of its entities as <see cref="Span{T}"/>s. Inlines the call and allows to process whole chunks, for example with SIMD.
    /// </summary>
    /// <param name="query">The <see cref="Core.Query"/> from <see cref="Query(in QueryDescription)"/> which specifies the <see cref="Entity"/>s for which to search.</param>
    /// <param name="iChunkForEach">The struct instance of the generic type being invoked.</param>
    public void InlineChunkQuery<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Query query, ref T iChunkForEach) where T : struct, IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
    {
        foreach (ref var chunk in query)
        {
            if (chunk.Count == 0)
            {
                continue;
            }

            iChunkForEach.Update(chunk.GetUsedSpan<T0>(), chunk.GetUsedSpan<T1>(), chunk.GetUsedSpan<T2>(), chunk.GetUsedSpan<T3>(), chunk.GetUsedSpan<T4>(), chunk.GetUsedSpan<T5>(), chunk.GetUsedSpan<T6>(), chunk.GetUsedSpan<T7>(), chunk.GetUsedSpan<T8>(), chunk.GetUsedSpan<T9>(), chunk.GetUsedSpan<T10>(), chunk.GetUsedSpan<T11>(), chunk.GetUsedSpan<T12>(), chunk.GetUsedSpan<T13>(), chunk.GetUsedSpan<T14>(), chunk.GetUsedSpan<T15>(), chunk.GetUsedSpan<T16>(), chunk.GetUsedSpan<T17>(), chunk.GetUsedSpan<T18>(), chunk.GetUsedSpan<T19>(), chunk.GetUsedSpan<T20>(), chunk.GetUsedSpan<T21>(), chunk.GetUsedSpan<T22>(), chunk.GetUsedSpan<T23>(), chunk.GetUsedSpan<T24>());
        }
    }

}
