

using System;

namespace Arch.Core;
public struct IForEachWithEntityJob<T, T0> : IChunkJob where T : struct, IForEachWithEntity<T0>
{
    public T ForEach;

    public void Execute(ref Chunk chunk)
    {
        ref var entityFirstElement = ref chunk.Entity(0);
        ref var t0FirstElement = ref chunk.GetFirst<T0>();
        
        
        foreach(var entityIndex in chunk)
        {
            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
            ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
            
            ForEach.Update(entity, ref t0Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1> : IChunkJob where T : struct, IForEachWithEntity<T0, T1>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component);
        }
    }
}
public struct IForEachWithEntityJob<T, T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> : IChunkJob where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
{
    public T ForEach;

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
            
            ForEach.Update(entity, ref t0Component,ref t1Component,ref t2Component,ref t3Component,ref t4Component,ref t5Component,ref t6Component,ref t7Component,ref t8Component,ref t9Component,ref t10Component,ref t11Component,ref t12Component,ref t13Component,ref t14Component,ref t15Component,ref t16Component,ref t17Component,ref t18Component,ref t19Component,ref t20Component,ref t21Component,ref t22Component,ref t23Component,ref t24Component);
        }
    }
}

