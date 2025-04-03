

using System;
using System.Runtime.CompilerServices;
using System.Diagnostics.Contracts;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;

public partial struct Chunk
{

    [Pure]
    public void GetArray<T0, T1>(out T0[] t0Array,out T1[] t1Array)
    {
        Index<T0, T1>(out var t0Index,out var t1Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array)
    {
        Index<T0, T1, T2>(out var t0Index,out var t1Index,out var t2Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array)
    {
        Index<T0, T1, T2, T3>(out var t0Index,out var t1Index,out var t2Index,out var t3Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array)
    {
        Index<T0, T1, T2, T3, T4>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array)
    {
        Index<T0, T1, T2, T3, T4, T5>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array,out T20[] t20Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index,out var t20Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        t20Array = Unsafe.As<T20[]>(Unsafe.Add(ref arrays, t20Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array,out T20[] t20Array,out T21[] t21Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index,out var t20Index,out var t21Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        t20Array = Unsafe.As<T20[]>(Unsafe.Add(ref arrays, t20Index));
        t21Array = Unsafe.As<T21[]>(Unsafe.Add(ref arrays, t21Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array,out T20[] t20Array,out T21[] t21Array,out T22[] t22Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index,out var t20Index,out var t21Index,out var t22Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        t20Array = Unsafe.As<T20[]>(Unsafe.Add(ref arrays, t20Index));
        t21Array = Unsafe.As<T21[]>(Unsafe.Add(ref arrays, t21Index));
        t22Array = Unsafe.As<T22[]>(Unsafe.Add(ref arrays, t22Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array,out T20[] t20Array,out T21[] t21Array,out T22[] t22Array,out T23[] t23Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index,out var t20Index,out var t21Index,out var t22Index,out var t23Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        t20Array = Unsafe.As<T20[]>(Unsafe.Add(ref arrays, t20Index));
        t21Array = Unsafe.As<T21[]>(Unsafe.Add(ref arrays, t21Index));
        t22Array = Unsafe.As<T22[]>(Unsafe.Add(ref arrays, t22Index));
        t23Array = Unsafe.As<T23[]>(Unsafe.Add(ref arrays, t23Index));
        
    }

    [Pure]
    public void GetArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out T0[] t0Array,out T1[] t1Array,out T2[] t2Array,out T3[] t3Array,out T4[] t4Array,out T5[] t5Array,out T6[] t6Array,out T7[] t7Array,out T8[] t8Array,out T9[] t9Array,out T10[] t10Array,out T11[] t11Array,out T12[] t12Array,out T13[] t13Array,out T14[] t14Array,out T15[] t15Array,out T16[] t16Array,out T17[] t17Array,out T18[] t18Array,out T19[] t19Array,out T20[] t20Array,out T21[] t21Array,out T22[] t22Array,out T23[] t23Array,out T24[] t24Array)
    {
        Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out var t0Index,out var t1Index,out var t2Index,out var t3Index,out var t4Index,out var t5Index,out var t6Index,out var t7Index,out var t8Index,out var t9Index,out var t10Index,out var t11Index,out var t12Index,out var t13Index,out var t14Index,out var t15Index,out var t16Index,out var t17Index,out var t18Index,out var t19Index,out var t20Index,out var t21Index,out var t22Index,out var t23Index,out var t24Index);
        ref var arrays = ref Components.DangerousGetReference();
        t0Array = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, t0Index));
        t1Array = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, t1Index));
        t2Array = Unsafe.As<T2[]>(Unsafe.Add(ref arrays, t2Index));
        t3Array = Unsafe.As<T3[]>(Unsafe.Add(ref arrays, t3Index));
        t4Array = Unsafe.As<T4[]>(Unsafe.Add(ref arrays, t4Index));
        t5Array = Unsafe.As<T5[]>(Unsafe.Add(ref arrays, t5Index));
        t6Array = Unsafe.As<T6[]>(Unsafe.Add(ref arrays, t6Index));
        t7Array = Unsafe.As<T7[]>(Unsafe.Add(ref arrays, t7Index));
        t8Array = Unsafe.As<T8[]>(Unsafe.Add(ref arrays, t8Index));
        t9Array = Unsafe.As<T9[]>(Unsafe.Add(ref arrays, t9Index));
        t10Array = Unsafe.As<T10[]>(Unsafe.Add(ref arrays, t10Index));
        t11Array = Unsafe.As<T11[]>(Unsafe.Add(ref arrays, t11Index));
        t12Array = Unsafe.As<T12[]>(Unsafe.Add(ref arrays, t12Index));
        t13Array = Unsafe.As<T13[]>(Unsafe.Add(ref arrays, t13Index));
        t14Array = Unsafe.As<T14[]>(Unsafe.Add(ref arrays, t14Index));
        t15Array = Unsafe.As<T15[]>(Unsafe.Add(ref arrays, t15Index));
        t16Array = Unsafe.As<T16[]>(Unsafe.Add(ref arrays, t16Index));
        t17Array = Unsafe.As<T17[]>(Unsafe.Add(ref arrays, t17Index));
        t18Array = Unsafe.As<T18[]>(Unsafe.Add(ref arrays, t18Index));
        t19Array = Unsafe.As<T19[]>(Unsafe.Add(ref arrays, t19Index));
        t20Array = Unsafe.As<T20[]>(Unsafe.Add(ref arrays, t20Index));
        t21Array = Unsafe.As<T21[]>(Unsafe.Add(ref arrays, t21Index));
        t22Array = Unsafe.As<T22[]>(Unsafe.Add(ref arrays, t22Index));
        t23Array = Unsafe.As<T23[]>(Unsafe.Add(ref arrays, t23Index));
        t24Array = Unsafe.As<T24[]>(Unsafe.Add(ref arrays, t24Index));
        
    }


}


