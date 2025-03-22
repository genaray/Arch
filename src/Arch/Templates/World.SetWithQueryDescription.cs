

using System;
using System.Runtime.CompilerServices;
using CommunityToolkit.HighPerformance;
using Arch.Core.Utils;

namespace Arch.Core;
public partial class World
{
    
    [SkipLocalsInit]
    public void Set<T0, T1>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                OnComponentSet<T4>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                OnComponentSet<T4>(entity);
                OnComponentSet<T5>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                OnComponentSet<T4>(entity);
                OnComponentSet<T5>(entity);
                OnComponentSet<T6>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                OnComponentSet<T4>(entity);
                OnComponentSet<T5>(entity);
                OnComponentSet<T6>(entity);
                OnComponentSet<T7>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                OnComponentSet<T1>(entity);
                OnComponentSet<T2>(entity);
                OnComponentSet<T3>(entity);
                OnComponentSet<T4>(entity);
                OnComponentSet<T5>(entity);
                OnComponentSet<T6>(entity);
                OnComponentSet<T7>(entity);
                OnComponentSet<T8>(entity);
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default,in T23? t23ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            ref var t23FirstElement = ref chunk.GetFirst<T23>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                ref var t23Component = ref Unsafe.Add(ref t23FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                t23Component = t23ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    
    [SkipLocalsInit]
    public void Set<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in QueryDescription queryDescription, in T0? t0ComponentValue = default,in T1? t1ComponentValue = default,in T2? t2ComponentValue = default,in T3? t3ComponentValue = default,in T4? t4ComponentValue = default,in T5? t5ComponentValue = default,in T6? t6ComponentValue = default,in T7? t7ComponentValue = default,in T8? t8ComponentValue = default,in T9? t9ComponentValue = default,in T10? t10ComponentValue = default,in T11? t11ComponentValue = default,in T12? t12ComponentValue = default,in T13? t13ComponentValue = default,in T14? t14ComponentValue = default,in T15? t15ComponentValue = default,in T16? t16ComponentValue = default,in T17? t17ComponentValue = default,in T18? t18ComponentValue = default,in T19? t19ComponentValue = default,in T20? t20ComponentValue = default,in T21? t21ComponentValue = default,in T22? t22ComponentValue = default,in T23? t23ComponentValue = default,in T24? t24ComponentValue = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var t0FirstElement = ref chunk.GetFirst<T0>();
            ref var t1FirstElement = ref chunk.GetFirst<T1>();
            ref var t2FirstElement = ref chunk.GetFirst<T2>();
            ref var t3FirstElement = ref chunk.GetFirst<T3>();
            ref var t4FirstElement = ref chunk.GetFirst<T4>();
            ref var t5FirstElement = ref chunk.GetFirst<T5>();
            ref var t6FirstElement = ref chunk.GetFirst<T6>();
            ref var t7FirstElement = ref chunk.GetFirst<T7>();
            ref var t8FirstElement = ref chunk.GetFirst<T8>();
            ref var t9FirstElement = ref chunk.GetFirst<T9>();
            ref var t10FirstElement = ref chunk.GetFirst<T10>();
            ref var t11FirstElement = ref chunk.GetFirst<T11>();
            ref var t12FirstElement = ref chunk.GetFirst<T12>();
            ref var t13FirstElement = ref chunk.GetFirst<T13>();
            ref var t14FirstElement = ref chunk.GetFirst<T14>();
            ref var t15FirstElement = ref chunk.GetFirst<T15>();
            ref var t16FirstElement = ref chunk.GetFirst<T16>();
            ref var t17FirstElement = ref chunk.GetFirst<T17>();
            ref var t18FirstElement = ref chunk.GetFirst<T18>();
            ref var t19FirstElement = ref chunk.GetFirst<T19>();
            ref var t20FirstElement = ref chunk.GetFirst<T20>();
            ref var t21FirstElement = ref chunk.GetFirst<T21>();
            ref var t22FirstElement = ref chunk.GetFirst<T22>();
            ref var t23FirstElement = ref chunk.GetFirst<T23>();
            ref var t24FirstElement = ref chunk.GetFirst<T24>();
            
            foreach (var entityIndex in chunk)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
                ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
                ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
                ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
                ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
                ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
                ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
                ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
                ref var t9Component = ref Unsafe.Add(ref t9FirstElement, entityIndex);
                ref var t10Component = ref Unsafe.Add(ref t10FirstElement, entityIndex);
                ref var t11Component = ref Unsafe.Add(ref t11FirstElement, entityIndex);
                ref var t12Component = ref Unsafe.Add(ref t12FirstElement, entityIndex);
                ref var t13Component = ref Unsafe.Add(ref t13FirstElement, entityIndex);
                ref var t14Component = ref Unsafe.Add(ref t14FirstElement, entityIndex);
                ref var t15Component = ref Unsafe.Add(ref t15FirstElement, entityIndex);
                ref var t16Component = ref Unsafe.Add(ref t16FirstElement, entityIndex);
                ref var t17Component = ref Unsafe.Add(ref t17FirstElement, entityIndex);
                ref var t18Component = ref Unsafe.Add(ref t18FirstElement, entityIndex);
                ref var t19Component = ref Unsafe.Add(ref t19FirstElement, entityIndex);
                ref var t20Component = ref Unsafe.Add(ref t20FirstElement, entityIndex);
                ref var t21Component = ref Unsafe.Add(ref t21FirstElement, entityIndex);
                ref var t22Component = ref Unsafe.Add(ref t22FirstElement, entityIndex);
                ref var t23Component = ref Unsafe.Add(ref t23FirstElement, entityIndex);
                ref var t24Component = ref Unsafe.Add(ref t24FirstElement, entityIndex);
                
                t0Component = t0ComponentValue;
                t1Component = t1ComponentValue;
                t2Component = t2ComponentValue;
                t3Component = t3ComponentValue;
                t4Component = t4ComponentValue;
                t5Component = t5ComponentValue;
                t6Component = t6ComponentValue;
                t7Component = t7ComponentValue;
                t8Component = t8ComponentValue;
                t9Component = t9ComponentValue;
                t10Component = t10ComponentValue;
                t11Component = t11ComponentValue;
                t12Component = t12ComponentValue;
                t13Component = t13ComponentValue;
                t14Component = t14ComponentValue;
                t15Component = t15ComponentValue;
                t16Component = t16ComponentValue;
                t17Component = t17ComponentValue;
                t18Component = t18ComponentValue;
                t19Component = t19ComponentValue;
                t20Component = t20ComponentValue;
                t21Component = t21ComponentValue;
                t22Component = t22ComponentValue;
                t23Component = t23ComponentValue;
                t24Component = t24ComponentValue;
                
        #if EVENTS
                var entity = chunk.Entity(entityIndex);
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
                
        #endif
            }
        }
    }
    }

