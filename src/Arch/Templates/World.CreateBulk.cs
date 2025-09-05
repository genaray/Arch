

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{
    

    [StructuralChange]
    public void Create<T0, T1>(int amount, in T0? t0Component = default,in T1? t1Component = default)
    {
        var archetype = EnsureCapacity<T0, T1>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1>(in lastSlot, in firstSlot, in t0Component,in t1Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }


    [StructuralChange]
    public void Create<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(int amount, in T0? t0Component = default,in T1? t1Component = default,in T2? t2Component = default,in T3? t3Component = default,in T4? t4Component = default,in T5? t5Component = default,in T6? t6Component = default,in T7? t7Component = default,in T8? t8Component = default,in T9? t9Component = default,in T10? t10Component = default,in T11? t11Component = default,in T12? t12Component = default,in T13? t13Component = default,in T14? t14Component = default,in T15? t15Component = default,in T16? t16Component = default,in T17? t17Component = default,in T18? t18Component = default,in T19? t19Component = default,in T20? t20Component = default,in T21? t21Component = default,in T22? t22Component = default,in T23? t23Component = default)
    {
        var archetype = EnsureCapacity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(amount);

        // Prepare entities, slots and data
        using var entityArray =  Pool<Entity>.Rent(amount);
        using var entityDataArray =  Pool<EntityData>.Rent(amount);
        var entities = entityArray.AsSpan();
        var entityData = entityDataArray.AsSpan();

        // Create entities
        GetOrCreateEntitiesInternal(archetype, entities, entityData, amount);
        archetype.AddAll(entities, amount);

        // Fill entities
        var firstSlot = entityData[0].Slot;
        var lastSlot = entityData[amount - 1].Slot;
        archetype.SetRange<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in lastSlot, in firstSlot, in t0Component,in t1Component,in t2Component,in t3Component,in t4Component,in t5Component,in t6Component,in t7Component,in t8Component,in t9Component,in t10Component,in t11Component,in t12Component,in t13Component,in t14Component,in t15Component,in t16Component,in t17Component,in t18Component,in t19Component,in t20Component,in t21Component,in t22Component,in t23Component);

        // Add entities to entityinfo
        AddEntityData(entities, entityData, amount);
    }
}
