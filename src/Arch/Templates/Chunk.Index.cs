

using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;
public partial struct Chunk
{
    
    [Pure]
    private void Index<T0, T1>(out int t0Index, out int t1Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2>(out int t0Index, out int t1Index, out int t2Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3>(out int t0Index, out int t1Index, out int t2Index, out int t3Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index, out int t20Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        t20Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T20>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index, out int t20Index, out int t21Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        t20Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T20>.ComponentType.Id);
        t21Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T21>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index, out int t20Index, out int t21Index, out int t22Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        t20Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T20>.ComponentType.Id);
        t21Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T21>.ComponentType.Id);
        t22Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T22>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index, out int t20Index, out int t21Index, out int t22Index, out int t23Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        t20Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T20>.ComponentType.Id);
        t21Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T21>.ComponentType.Id);
        t22Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T22>.ComponentType.Id);
        t23Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T23>.ComponentType.Id);
        
    }
    
    [Pure]
    private void Index<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(out int t0Index, out int t1Index, out int t2Index, out int t3Index, out int t4Index, out int t5Index, out int t6Index, out int t7Index, out int t8Index, out int t9Index, out int t10Index, out int t11Index, out int t12Index, out int t13Index, out int t14Index, out int t15Index, out int t16Index, out int t17Index, out int t18Index, out int t19Index, out int t20Index, out int t21Index, out int t22Index, out int t23Index, out int t24Index)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        t0Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        t1Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
        t2Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T2>.ComponentType.Id);
        t3Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T3>.ComponentType.Id);
        t4Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T4>.ComponentType.Id);
        t5Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T5>.ComponentType.Id);
        t6Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T6>.ComponentType.Id);
        t7Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T7>.ComponentType.Id);
        t8Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T8>.ComponentType.Id);
        t9Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T9>.ComponentType.Id);
        t10Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T10>.ComponentType.Id);
        t11Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T11>.ComponentType.Id);
        t12Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T12>.ComponentType.Id);
        t13Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T13>.ComponentType.Id);
        t14Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T14>.ComponentType.Id);
        t15Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T15>.ComponentType.Id);
        t16Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T16>.ComponentType.Id);
        t17Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T17>.ComponentType.Id);
        t18Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T18>.ComponentType.Id);
        t19Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T19>.ComponentType.Id);
        t20Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T20>.ComponentType.Id);
        t21Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T21>.ComponentType.Id);
        t22Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T22>.ComponentType.Id);
        t23Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T23>.ComponentType.Id);
        t24Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T24>.ComponentType.Id);
        
    }
    }

