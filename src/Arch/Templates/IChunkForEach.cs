

using System;

namespace Arch.Core;

/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span, Span<T20> t20Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span, Span<T20> t20Span, Span<T21> t21Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span, Span<T20> t20Span, Span<T21> t21Span, Span<T22> t22Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span, Span<T20> t20Span, Span<T21> t21Span, Span<T22> t22Span, Span<T23> t23Span);
}
/// <summary>
///     A struct implementation of this interface is called by <see cref="World"/>.InlineChunkQuery once for each <see cref="Chunk"/> with entities,
///     with the components of all its entities as <see cref="Span{T}"/>s. Allows to process whole chunks, for example with SIMD.
/// </summary>
public interface IChunkForEach<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
{
    /// <summary>
    ///     Called once for each <see cref="Chunk"/> with the components of its <see cref="Chunk.Count"/> entities, all spans have the same length.
    /// </summary>
    void Update(Span<T0> t0Span, Span<T1> t1Span, Span<T2> t2Span, Span<T3> t3Span, Span<T4> t4Span, Span<T5> t5Span, Span<T6> t6Span, Span<T7> t7Span, Span<T8> t8Span, Span<T9> t9Span, Span<T10> t10Span, Span<T11> t11Span, Span<T12> t12Span, Span<T13> t13Span, Span<T14> t14Span, Span<T15> t15Span, Span<T16> t16Span, Span<T17> t17Span, Span<T18> t18Span, Span<T19> t19Span, Span<T20> t20Span, Span<T21> t21Span, Span<T22> t22Span, Span<T23> t23Span, Span<T24> t24Span);
}
