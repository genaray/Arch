

using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Core;
public partial struct Chunk
{
    
    [Pure]
    public Components<T0, T1> Get<T0, T1>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        
        return new Components<T0, T1>(ref t0Component,ref t1Component);
    }
    
    [Pure]
    public Components<T0, T1, T2> Get<T0, T1, T2>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        
        return new Components<T0, T1, T2>(ref t0Component,ref t1Component,ref t2Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3> Get<T0, T1, T2, T3>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        ref var t3FirstElement = ref GetFirst<T3>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        
        return new Components<T0, T1, T2, T3>(ref t0Component,ref t1Component,ref t2Component,ref t3Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4> Get<T0, T1, T2, T3, T4>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        ref var t3FirstElement = ref GetFirst<T3>();
        ref var t4FirstElement = ref GetFirst<T4>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5> Get<T0, T1, T2, T3, T4, T5>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        ref var t3FirstElement = ref GetFirst<T3>();
        ref var t4FirstElement = ref GetFirst<T4>();
        ref var t5FirstElement = ref GetFirst<T5>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6> Get<T0, T1, T2, T3, T4, T5, T6>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        ref var t3FirstElement = ref GetFirst<T3>();
        ref var t4FirstElement = ref GetFirst<T4>();
        ref var t5FirstElement = ref GetFirst<T5>();
        ref var t6FirstElement = ref GetFirst<T6>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7> Get<T0, T1, T2, T3, T4, T5, T6, T7>(int index)
    {
        ref var t0FirstElement = ref GetFirst<T0>();
        ref var t1FirstElement = ref GetFirst<T1>();
        ref var t2FirstElement = ref GetFirst<T2>();
        ref var t3FirstElement = ref GetFirst<T3>();
        ref var t4FirstElement = ref GetFirst<T4>();
        ref var t5FirstElement = ref GetFirst<T5>();
        ref var t6FirstElement = ref GetFirst<T6>();
        ref var t7FirstElement = ref GetFirst<T7>();
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        ref var t20Component = ref Unsafe.Add(ref t20FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        ref var t20Component = ref Unsafe.Add(ref t20FirstElement, index);
        ref var t21Component = ref Unsafe.Add(ref t21FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        ref var t20Component = ref Unsafe.Add(ref t20FirstElement, index);
        ref var t21Component = ref Unsafe.Add(ref t21FirstElement, index);
        ref var t22Component = ref Unsafe.Add(ref t22FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        ref var t20Component = ref Unsafe.Add(ref t20FirstElement, index);
        ref var t21Component = ref Unsafe.Add(ref t21FirstElement, index);
        ref var t22Component = ref Unsafe.Add(ref t22FirstElement, index);
        ref var t23Component = ref Unsafe.Add(ref t23FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component);
    }
    
    [Pure]
    public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(int index)
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
        
        ref var t0Component = ref Unsafe.Add(ref t0FirstElement, index);
        ref var t1Component = ref Unsafe.Add(ref t1FirstElement, index);
        ref var t2Component = ref Unsafe.Add(ref t2FirstElement, index);
        ref var t3Component = ref Unsafe.Add(ref t3FirstElement, index);
        ref var t4Component = ref Unsafe.Add(ref t4FirstElement, index);
        ref var t5Component = ref Unsafe.Add(ref t5FirstElement, index);
        ref var t6Component = ref Unsafe.Add(ref t6FirstElement, index);
        ref var t7Component = ref Unsafe.Add(ref t7FirstElement, index);
        ref var t8Component = ref Unsafe.Add(ref t8FirstElement, index);
        ref var t9Component = ref Unsafe.Add(ref t9FirstElement, index);
        ref var t10Component = ref Unsafe.Add(ref t10FirstElement, index);
        ref var t11Component = ref Unsafe.Add(ref t11FirstElement, index);
        ref var t12Component = ref Unsafe.Add(ref t12FirstElement, index);
        ref var t13Component = ref Unsafe.Add(ref t13FirstElement, index);
        ref var t14Component = ref Unsafe.Add(ref t14FirstElement, index);
        ref var t15Component = ref Unsafe.Add(ref t15FirstElement, index);
        ref var t16Component = ref Unsafe.Add(ref t16FirstElement, index);
        ref var t17Component = ref Unsafe.Add(ref t17FirstElement, index);
        ref var t18Component = ref Unsafe.Add(ref t18FirstElement, index);
        ref var t19Component = ref Unsafe.Add(ref t19FirstElement, index);
        ref var t20Component = ref Unsafe.Add(ref t20FirstElement, index);
        ref var t21Component = ref Unsafe.Add(ref t21FirstElement, index);
        ref var t22Component = ref Unsafe.Add(ref t22FirstElement, index);
        ref var t23Component = ref Unsafe.Add(ref t23FirstElement, index);
        ref var t24Component = ref Unsafe.Add(ref t24FirstElement, index);
        
        return new Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component,ref t24Component);
    }
    }

