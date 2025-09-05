

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{
    
    public void Set<T0, T1>(Entity entity, in T0? t0Component = default,in T1? t1Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1>(ref slot,  in t0Component,in t1Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        
    }
    
    public void Set<T0, T1, T2>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2>(ref slot,  in t0Component,in t1Component,in t2Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        OnComponentSet<T20>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        OnComponentSet<T20>(entity);
        OnComponentSet<T21>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        OnComponentSet<T20>(entity);
        OnComponentSet<T21>(entity);
        OnComponentSet<T22>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        OnComponentSet<T20>(entity);
        OnComponentSet<T21>(entity);
        OnComponentSet<T22>(entity);
        OnComponentSet<T23>(entity);
        
    }
    
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Entity entity, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default,in T24? t24Component = default)
    {
        ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
        var slot = entitySlot.Slot;
        var archetype = entitySlot.Archetype;
        archetype.Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(ref slot,  in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component,in t24Component);

        OnComponentSet<T0>(entity);
        OnComponentSet<T1>(entity);
        OnComponentSet<T2>(entity);
        OnComponentSet<T3>(entity);
        OnComponentSet<T4>(entity);
        OnComponentSet<T5>(entity);
        OnComponentSet<T6>(entity);
        OnComponentSet<T7>(entity);
        OnComponentSet<T8>(entity);
        OnComponentSet<T9>(entity);
        OnComponentSet<T10>(entity);
        OnComponentSet<T11>(entity);
        OnComponentSet<T12>(entity);
        OnComponentSet<T13>(entity);
        OnComponentSet<T14>(entity);
        OnComponentSet<T15>(entity);
        OnComponentSet<T16>(entity);
        OnComponentSet<T17>(entity);
        OnComponentSet<T18>(entity);
        OnComponentSet<T19>(entity);
        OnComponentSet<T20>(entity);
        OnComponentSet<T21>(entity);
        OnComponentSet<T22>(entity);
        OnComponentSet<T23>(entity);
        OnComponentSet<T24>(entity);
        
    }
    }

