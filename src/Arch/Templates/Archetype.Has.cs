

//TODO: Improve source generation by extracting the stringbuilder stuff to own methods

using Arch.Core.Utils;

namespace Arch.Core;
public partial class Archetype
{
    public bool Has<T0, T1>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) ;
    }

    public bool Has<T0, T1, T2>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        var t20ComponentId = Component<T20>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) && BitSet.IsSet(t20ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        var t20ComponentId = Component<T20>.ComponentType.Id;
        var t21ComponentId = Component<T21>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) && BitSet.IsSet(t20ComponentId) && BitSet.IsSet(t21ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        var t20ComponentId = Component<T20>.ComponentType.Id;
        var t21ComponentId = Component<T21>.ComponentType.Id;
        var t22ComponentId = Component<T22>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) && BitSet.IsSet(t20ComponentId) && BitSet.IsSet(t21ComponentId) && BitSet.IsSet(t22ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        var t20ComponentId = Component<T20>.ComponentType.Id;
        var t21ComponentId = Component<T21>.ComponentType.Id;
        var t22ComponentId = Component<T22>.ComponentType.Id;
        var t23ComponentId = Component<T23>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) && BitSet.IsSet(t20ComponentId) && BitSet.IsSet(t21ComponentId) && BitSet.IsSet(t22ComponentId) && BitSet.IsSet(t23ComponentId) ;
    }

    public bool Has<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        var t7ComponentId = Component<T7>.ComponentType.Id;
        var t8ComponentId = Component<T8>.ComponentType.Id;
        var t9ComponentId = Component<T9>.ComponentType.Id;
        var t10ComponentId = Component<T10>.ComponentType.Id;
        var t11ComponentId = Component<T11>.ComponentType.Id;
        var t12ComponentId = Component<T12>.ComponentType.Id;
        var t13ComponentId = Component<T13>.ComponentType.Id;
        var t14ComponentId = Component<T14>.ComponentType.Id;
        var t15ComponentId = Component<T15>.ComponentType.Id;
        var t16ComponentId = Component<T16>.ComponentType.Id;
        var t17ComponentId = Component<T17>.ComponentType.Id;
        var t18ComponentId = Component<T18>.ComponentType.Id;
        var t19ComponentId = Component<T19>.ComponentType.Id;
        var t20ComponentId = Component<T20>.ComponentType.Id;
        var t21ComponentId = Component<T21>.ComponentType.Id;
        var t22ComponentId = Component<T22>.ComponentType.Id;
        var t23ComponentId = Component<T23>.ComponentType.Id;
        var t24ComponentId = Component<T24>.ComponentType.Id;
        
        return BitSet.IsSet(t0ComponentId) && BitSet.IsSet(t1ComponentId) && BitSet.IsSet(t2ComponentId) && BitSet.IsSet(t3ComponentId) && BitSet.IsSet(t4ComponentId) && BitSet.IsSet(t5ComponentId) && BitSet.IsSet(t6ComponentId) && BitSet.IsSet(t7ComponentId) && BitSet.IsSet(t8ComponentId) && BitSet.IsSet(t9ComponentId) && BitSet.IsSet(t10ComponentId) && BitSet.IsSet(t11ComponentId) && BitSet.IsSet(t12ComponentId) && BitSet.IsSet(t13ComponentId) && BitSet.IsSet(t14ComponentId) && BitSet.IsSet(t15ComponentId) && BitSet.IsSet(t16ComponentId) && BitSet.IsSet(t17ComponentId) && BitSet.IsSet(t18ComponentId) && BitSet.IsSet(t19ComponentId) && BitSet.IsSet(t20ComponentId) && BitSet.IsSet(t21ComponentId) && BitSet.IsSet(t22ComponentId) && BitSet.IsSet(t23ComponentId) && BitSet.IsSet(t24ComponentId) ;
    }

}
