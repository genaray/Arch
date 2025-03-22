

using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;

public partial struct Chunk
{
    [Pure]
    public void GetSpan<T0, T1>(out Span<T0> t0Span,out Span<T1> t1Span)
    {
        GetArray<T0, T1>(out var t0Array,out var t1Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span)
    {
        GetArray<T0, T1, T2>(out var t0Array,out var t1Array,out var t2Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span)
    {
        GetArray<T0, T1, T2, T3>(out var t0Array,out var t1Array,out var t2Array,out var t3Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span)
    {
        GetArray<T0, T1, T2, T3, T4>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span,out Span<T20> t20Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array,out var t20Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        t20Span = new Span<T20>(t20Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span,out Span<T20> t20Span,out Span<T21> t21Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array,out var t20Array,out var t21Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        t20Span = new Span<T20>(t20Array);
        t21Span = new Span<T21>(t21Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span,out Span<T20> t20Span,out Span<T21> t21Span,out Span<T22> t22Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array,out var t20Array,out var t21Array,out var t22Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        t20Span = new Span<T20>(t20Array);
        t21Span = new Span<T21>(t21Array);
        t22Span = new Span<T22>(t22Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span,out Span<T20> t20Span,out Span<T21> t21Span,out Span<T22> t22Span,out Span<T23> t23Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array,out var t20Array,out var t21Array,out var t22Array,out var t23Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        t20Span = new Span<T20>(t20Array);
        t21Span = new Span<T21>(t21Array);
        t22Span = new Span<T22>(t22Array);
        t23Span = new Span<T23>(t23Array);
        
    }

    [Pure]
    public void GetSpan<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out Span<T0> t0Span,out Span<T1> t1Span,out Span<T2> t2Span,out Span<T3> t3Span,out Span<T4> t4Span,out Span<T5> t5Span,out Span<T6> t6Span,out Span<T7> t7Span,out Span<T8> t8Span,out Span<T9> t9Span,out Span<T10> t10Span,out Span<T11> t11Span,out Span<T12> t12Span,out Span<T13> t13Span,out Span<T14> t14Span,out Span<T15> t15Span,out Span<T16> t16Span,out Span<T17> t17Span,out Span<T18> t18Span,out Span<T19> t19Span,out Span<T20> t20Span,out Span<T21> t21Span,out Span<T22> t22Span,out Span<T23> t23Span,out Span<T24> t24Span)
    {
        GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out var t0Array,out var t1Array,out var t2Array,out var t3Array,out var t4Array,out var t5Array,out var t6Array,out var t7Array,out var t8Array,out var t9Array,out var t10Array,out var t11Array,out var t12Array,out var t13Array,out var t14Array,out var t15Array,out var t16Array,out var t17Array,out var t18Array,out var t19Array,out var t20Array,out var t21Array,out var t22Array,out var t23Array,out var t24Array);
        t0Span = new Span<T0>(t0Array);
        t1Span = new Span<T1>(t1Array);
        t2Span = new Span<T2>(t2Array);
        t3Span = new Span<T3>(t3Array);
        t4Span = new Span<T4>(t4Array);
        t5Span = new Span<T5>(t5Array);
        t6Span = new Span<T6>(t6Array);
        t7Span = new Span<T7>(t7Array);
        t8Span = new Span<T8>(t8Array);
        t9Span = new Span<T9>(t9Array);
        t10Span = new Span<T10>(t10Array);
        t11Span = new Span<T11>(t11Array);
        t12Span = new Span<T12>(t12Array);
        t13Span = new Span<T13>(t13Array);
        t14Span = new Span<T14>(t14Array);
        t15Span = new Span<T15>(t15Array);
        t16Span = new Span<T16>(t16Array);
        t17Span = new Span<T17>(t17Array);
        t18Span = new Span<T18>(t18Array);
        t19Span = new Span<T19>(t19Array);
        t20Span = new Span<T20>(t20Array);
        t21Span = new Span<T21>(t21Array);
        t22Span = new Span<T22>(t22Array);
        t23Span = new Span<T23>(t23Array);
        t24Span = new Span<T24>(t24Array);
        
    }

}


