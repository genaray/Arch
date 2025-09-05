

using System;

namespace Arch.Core;
public struct ForEachWithEntityJob<T0> : IChunkJob
{
    public ForEachWithEntity<T0> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1> : IChunkJob
{
    public ForEachWithEntity<T0, T1> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        ref var t4FirstElement = ref chunk.GetFirst<T4>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        ref var t4FirstElement = ref chunk.GetFirst<T4>();
        ref var t5FirstElement = ref chunk.GetFirst<T5>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
            ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        ref var t4FirstElement = ref chunk.GetFirst<T4>();
        ref var t5FirstElement = ref chunk.GetFirst<T5>();
        ref var t6FirstElement = ref chunk.GetFirst<T6>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
            ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
            ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        ref var t4FirstElement = ref chunk.GetFirst<T4>();
        ref var t5FirstElement = ref chunk.GetFirst<T5>();
        ref var t6FirstElement = ref chunk.GetFirst<T6>();
        ref var t7FirstElement = ref chunk.GetFirst<T7>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
            ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
            ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
            ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        ref var t1FirstElement = ref chunk.GetFirst<T1>();
        ref var t2FirstElement = ref chunk.GetFirst<T2>();
        ref var t3FirstElement = ref chunk.GetFirst<T3>();
        ref var t4FirstElement = ref chunk.GetFirst<T4>();
        ref var t5FirstElement = ref chunk.GetFirst<T5>();
        ref var t6FirstElement = ref chunk.GetFirst<T6>();
        ref var t7FirstElement = ref chunk.GetFirst<T7>();
        ref var t8FirstElement = ref chunk.GetFirst<T8>();
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
            ref var t2Component = ref Unsafe.Add(ref t2FirstElement, entityIndex);
            ref var t3Component = ref Unsafe.Add(ref t3FirstElement, entityIndex);
            ref var t4Component = ref Unsafe.Add(ref t4FirstElement, entityIndex);
            ref var t5Component = ref Unsafe.Add(ref t5FirstElement, entityIndex);
            ref var t6Component = ref Unsafe.Add(ref t6FirstElement, entityIndex);
            ref var t7Component = ref Unsafe.Add(ref t7FirstElement, entityIndex);
            ref var t8Component = ref Unsafe.Add(ref t8FirstElement, entityIndex);
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component);
        }
    }
}

public struct ForEachWithEntityJob<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> : IChunkJob
{
    public ForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
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
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
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
            
            ForEach(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component,ref t24Component);
        }
    }
}

