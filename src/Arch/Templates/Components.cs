

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;


[SkipLocalsInit]
public ref struct Components<T0, T1>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component)
    {
        t0Component = t0;
        t1Component = t1;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    public Ref<T20> t20;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    public ref T20 t20;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component,ref T20 t20Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        t20 = new Ref<T20>(ref t20Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        t20 = ref t20Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component, out T20? t20Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        t20Component = t20;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    public Ref<T20> t20;
    public Ref<T21> t21;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    public ref T20 t20;
    public ref T21 t21;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component,ref T20 t20Component,ref T21 t21Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        t20 = new Ref<T20>(ref t20Component);
        t21 = new Ref<T21>(ref t21Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        t20 = ref t20Component;
        t21 = ref t21Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component, out T20? t20Component, out T21? t21Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        t20Component = t20;
        t21Component = t21;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    public Ref<T20> t20;
    public Ref<T21> t21;
    public Ref<T22> t22;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    public ref T20 t20;
    public ref T21 t21;
    public ref T22 t22;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component,ref T20 t20Component,ref T21 t21Component,ref T22 t22Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        t20 = new Ref<T20>(ref t20Component);
        t21 = new Ref<T21>(ref t21Component);
        t22 = new Ref<T22>(ref t22Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        t20 = ref t20Component;
        t21 = ref t21Component;
        t22 = ref t22Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component, out T20? t20Component, out T21? t21Component, out T22? t22Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        t20Component = t20;
        t21Component = t21;
        t22Component = t22;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    public Ref<T20> t20;
    public Ref<T21> t21;
    public Ref<T22> t22;
    public Ref<T23> t23;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    public ref T20 t20;
    public ref T21 t21;
    public ref T22 t22;
    public ref T23 t23;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component,ref T20 t20Component,ref T21 t21Component,ref T22 t22Component,ref T23 t23Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        t20 = new Ref<T20>(ref t20Component);
        t21 = new Ref<T21>(ref t21Component);
        t22 = new Ref<T22>(ref t22Component);
        t23 = new Ref<T23>(ref t23Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        t20 = ref t20Component;
        t21 = ref t21Component;
        t22 = ref t22Component;
        t23 = ref t23Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component, out T20? t20Component, out T21? t21Component, out T22? t22Component, out T23? t23Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        t20Component = t20;
        t21Component = t21;
        t22Component = t22;
        t23Component = t23;
        
    }
}

[SkipLocalsInit]
public ref struct Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
{
#if NETSTANDARD2_1 || NET6_0
    public Ref<T0> t0;
    public Ref<T1> t1;
    public Ref<T2> t2;
    public Ref<T3> t3;
    public Ref<T4> t4;
    public Ref<T5> t5;
    public Ref<T6> t6;
    public Ref<T7> t7;
    public Ref<T8> t8;
    public Ref<T9> t9;
    public Ref<T10> t10;
    public Ref<T11> t11;
    public Ref<T12> t12;
    public Ref<T13> t13;
    public Ref<T14> t14;
    public Ref<T15> t15;
    public Ref<T16> t16;
    public Ref<T17> t17;
    public Ref<T18> t18;
    public Ref<T19> t19;
    public Ref<T20> t20;
    public Ref<T21> t21;
    public Ref<T22> t22;
    public Ref<T23> t23;
    public Ref<T24> t24;
    
#else
    public ref T0 t0;
    public ref T1 t1;
    public ref T2 t2;
    public ref T3 t3;
    public ref T4 t4;
    public ref T5 t5;
    public ref T6 t6;
    public ref T7 t7;
    public ref T8 t8;
    public ref T9 t9;
    public ref T10 t10;
    public ref T11 t11;
    public ref T12 t12;
    public ref T13 t13;
    public ref T14 t14;
    public ref T15 t15;
    public ref T16 t16;
    public ref T17 t17;
    public ref T18 t18;
    public ref T19 t19;
    public ref T20 t20;
    public ref T21 t21;
    public ref T22 t22;
    public ref T23 t23;
    public ref T24 t24;
    
#endif

    [SkipLocalsInit]
    public Components(ref T0 t0Component,ref T1 t1Component,ref T2 t2Component,ref T3 t3Component,ref T4 t4Component,ref T5 t5Component,ref T6 t6Component,ref T7 t7Component,ref T8 t8Component,ref T9 t9Component,ref T10 t10Component,ref T11 t11Component,ref T12 t12Component,ref T13 t13Component,ref T14 t14Component,ref T15 t15Component,ref T16 t16Component,ref T17 t17Component,ref T18 t18Component,ref T19 t19Component,ref T20 t20Component,ref T21 t21Component,ref T22 t22Component,ref T23 t23Component,ref T24 t24Component)
    {
#if NETSTANDARD2_1 || NET6_0
        t0 = new Ref<T0>(ref t0Component);
        t1 = new Ref<T1>(ref t1Component);
        t2 = new Ref<T2>(ref t2Component);
        t3 = new Ref<T3>(ref t3Component);
        t4 = new Ref<T4>(ref t4Component);
        t5 = new Ref<T5>(ref t5Component);
        t6 = new Ref<T6>(ref t6Component);
        t7 = new Ref<T7>(ref t7Component);
        t8 = new Ref<T8>(ref t8Component);
        t9 = new Ref<T9>(ref t9Component);
        t10 = new Ref<T10>(ref t10Component);
        t11 = new Ref<T11>(ref t11Component);
        t12 = new Ref<T12>(ref t12Component);
        t13 = new Ref<T13>(ref t13Component);
        t14 = new Ref<T14>(ref t14Component);
        t15 = new Ref<T15>(ref t15Component);
        t16 = new Ref<T16>(ref t16Component);
        t17 = new Ref<T17>(ref t17Component);
        t18 = new Ref<T18>(ref t18Component);
        t19 = new Ref<T19>(ref t19Component);
        t20 = new Ref<T20>(ref t20Component);
        t21 = new Ref<T21>(ref t21Component);
        t22 = new Ref<T22>(ref t22Component);
        t23 = new Ref<T23>(ref t23Component);
        t24 = new Ref<T24>(ref t24Component);
        
#else
        t0 = ref t0Component;
        t1 = ref t1Component;
        t2 = ref t2Component;
        t3 = ref t3Component;
        t4 = ref t4Component;
        t5 = ref t5Component;
        t6 = ref t6Component;
        t7 = ref t7Component;
        t8 = ref t8Component;
        t9 = ref t9Component;
        t10 = ref t10Component;
        t11 = ref t11Component;
        t12 = ref t12Component;
        t13 = ref t13Component;
        t14 = ref t14Component;
        t15 = ref t15Component;
        t16 = ref t16Component;
        t17 = ref t17Component;
        t18 = ref t18Component;
        t19 = ref t19Component;
        t20 = ref t20Component;
        t21 = ref t21Component;
        t22 = ref t22Component;
        t23 = ref t23Component;
        t24 = ref t24Component;
        
#endif
    }

    [SkipLocalsInit]
    public readonly void Deconstruct(out T0? t0Component, out T1? t1Component, out T2? t2Component, out T3? t3Component, out T4? t4Component, out T5? t5Component, out T6? t6Component, out T7? t7Component, out T8? t8Component, out T9? t9Component, out T10? t10Component, out T11? t11Component, out T12? t12Component, out T13? t13Component, out T14? t14Component, out T15? t15Component, out T16? t16Component, out T17? t17Component, out T18? t18Component, out T19? t19Component, out T20? t20Component, out T21? t21Component, out T22? t22Component, out T23? t23Component, out T24? t24Component)
    {
        t0Component = t0;
        t1Component = t1;
        t2Component = t2;
        t3Component = t3;
        t4Component = t4;
        t5Component = t5;
        t6Component = t6;
        t7Component = t7;
        t8Component = t8;
        t9Component = t9;
        t10Component = t10;
        t11Component = t11;
        t12Component = t12;
        t13Component = t13;
        t14Component = t14;
        t15Component = t15;
        t16Component = t16;
        t17Component = t17;
        t18Component = t18;
        t19Component = t19;
        t20Component = t20;
        t21Component = t21;
        t22Component = t22;
        t23Component = t23;
        t24Component = t24;
        
    }
}


