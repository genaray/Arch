

using System;
using System.Diagnostics.Contracts;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Core;

public partial struct Chunk
{
    
    [Pure]
    public bool Has<T0, T1>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
    public bool Has<T0, T1, T2>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
    public bool Has<T0, T1, T2, T3>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
    public bool Has<T0, T1, T2, T3, T4>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
    public bool Has<T0, T1, T2, T3, T4, T5>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
    public bool Has<T0, T1, T2, T3, T4, T5, T6>()
    {
        var t0ComponentId = Component<T0>.ComponentType.Id;
        var t1ComponentId = Component<T1>.ComponentType.Id;
        var t2ComponentId = Component<T2>.ComponentType.Id;
        var t3ComponentId = Component<T3>.ComponentType.Id;
        var t4ComponentId = Component<T4>.ComponentType.Id;
        var t5ComponentId = Component<T5>.ComponentType.Id;
        var t6ComponentId = Component<T6>.ComponentType.Id;
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t20ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t20ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t20ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t21ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t20ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t21ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t20ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t21ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t22ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t20ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t21ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t22ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t20ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t21ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t22ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t23ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t20ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t21ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t22ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t23ComponentId] == -1) return false;
        

        return true;
    }

    
    [Pure]
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
        
        if (t0ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t1ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t2ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t3ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t4ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t5ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t6ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t7ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t8ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t9ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t10ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t11ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t12ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t13ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t14ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t15ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t16ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t17ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t18ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t19ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t20ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t21ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t22ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t23ComponentId >= ComponentIdToArrayIndex.Length) return false;
        if (t24ComponentId >= ComponentIdToArrayIndex.Length) return false;
        
        if (ComponentIdToArrayIndex[t0ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t1ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t2ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t3ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t4ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t5ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t6ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t7ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t8ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t9ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t10ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t11ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t12ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t13ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t14ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t15ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t16ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t17ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t18ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t19ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t20ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t21ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t22ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t23ComponentId] == -1) return false;
        if (ComponentIdToArrayIndex[t24ComponentId] == -1) return false;
        

        return true;
    }

    }
