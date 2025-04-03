

using System;
using System.Diagnostics.Contracts;

namespace Arch.Core;
public partial struct Chunk
{

    public void Set<T0, T1>(int index, in T0? t0Component = default,in T1? t1Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;

    }

    public void Set<T0, T1, T2>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;

    }

    public void Set<T0, T1, T2, T3>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;

    }

    public void Set<T0, T1, T2, T3, T4>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();
            ref var t20FirstElement = ref GetFirst<T20>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;
            Unsafe.Add(ref t20FirstElement, index) = t20Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();
            ref var t20FirstElement = ref GetFirst<T20>();
            ref var t21FirstElement = ref GetFirst<T21>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;
            Unsafe.Add(ref t20FirstElement, index) = t20Component;
            Unsafe.Add(ref t21FirstElement, index) = t21Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();
            ref var t20FirstElement = ref GetFirst<T20>();
            ref var t21FirstElement = ref GetFirst<T21>();
            ref var t22FirstElement = ref GetFirst<T22>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;
            Unsafe.Add(ref t20FirstElement, index) = t20Component;
            Unsafe.Add(ref t21FirstElement, index) = t21Component;
            Unsafe.Add(ref t22FirstElement, index) = t22Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();
            ref var t20FirstElement = ref GetFirst<T20>();
            ref var t21FirstElement = ref GetFirst<T21>();
            ref var t22FirstElement = ref GetFirst<T22>();
            ref var t23FirstElement = ref GetFirst<T23>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;
            Unsafe.Add(ref t20FirstElement, index) = t20Component;
            Unsafe.Add(ref t21FirstElement, index) = t21Component;
            Unsafe.Add(ref t22FirstElement, index) = t22Component;
            Unsafe.Add(ref t23FirstElement, index) = t23Component;

    }

    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(int index, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default,in T24? t24Component = default)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
            ref var t1FirstElement = ref GetFirst<T1>();
            ref var t2FirstElement = ref GetFirst<T2>();
            ref var t3FirstElement = ref GetFirst<T3>();
            ref var t4FirstElement = ref GetFirst<T4>();
            ref var t5FirstElement = ref GetFirst<T5>();
            ref var t6FirstElement = ref GetFirst<T6>();
            ref var t7FirstElement = ref GetFirst<T7>();
            ref var t8FirstElement = ref GetFirst<T8>();
            ref var t9FirstElement = ref GetFirst<T9>();
            ref var t10FirstElement = ref GetFirst<T10>();
            ref var t11FirstElement = ref GetFirst<T11>();
            ref var t12FirstElement = ref GetFirst<T12>();
            ref var t13FirstElement = ref GetFirst<T13>();
            ref var t14FirstElement = ref GetFirst<T14>();
            ref var t15FirstElement = ref GetFirst<T15>();
            ref var t16FirstElement = ref GetFirst<T16>();
            ref var t17FirstElement = ref GetFirst<T17>();
            ref var t18FirstElement = ref GetFirst<T18>();
            ref var t19FirstElement = ref GetFirst<T19>();
            ref var t20FirstElement = ref GetFirst<T20>();
            ref var t21FirstElement = ref GetFirst<T21>();
            ref var t22FirstElement = ref GetFirst<T22>();
            ref var t23FirstElement = ref GetFirst<T23>();
            ref var t24FirstElement = ref GetFirst<T24>();

        Unsafe.Add(ref t0FirstElement, index) = t0Component;
            Unsafe.Add(ref t1FirstElement, index) = t1Component;
            Unsafe.Add(ref t2FirstElement, index) = t2Component;
            Unsafe.Add(ref t3FirstElement, index) = t3Component;
            Unsafe.Add(ref t4FirstElement, index) = t4Component;
            Unsafe.Add(ref t5FirstElement, index) = t5Component;
            Unsafe.Add(ref t6FirstElement, index) = t6Component;
            Unsafe.Add(ref t7FirstElement, index) = t7Component;
            Unsafe.Add(ref t8FirstElement, index) = t8Component;
            Unsafe.Add(ref t9FirstElement, index) = t9Component;
            Unsafe.Add(ref t10FirstElement, index) = t10Component;
            Unsafe.Add(ref t11FirstElement, index) = t11Component;
            Unsafe.Add(ref t12FirstElement, index) = t12Component;
            Unsafe.Add(ref t13FirstElement, index) = t13Component;
            Unsafe.Add(ref t14FirstElement, index) = t14Component;
            Unsafe.Add(ref t15FirstElement, index) = t15Component;
            Unsafe.Add(ref t16FirstElement, index) = t16Component;
            Unsafe.Add(ref t17FirstElement, index) = t17Component;
            Unsafe.Add(ref t18FirstElement, index) = t18Component;
            Unsafe.Add(ref t19FirstElement, index) = t19Component;
            Unsafe.Add(ref t20FirstElement, index) = t20Component;
            Unsafe.Add(ref t21FirstElement, index) = t21Component;
            Unsafe.Add(ref t22FirstElement, index) = t22Component;
            Unsafe.Add(ref t23FirstElement, index) = t23Component;
            Unsafe.Add(ref t24FirstElement, index) = t24Component;

    }
    }
