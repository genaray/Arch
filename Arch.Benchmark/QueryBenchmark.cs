using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Test;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;

namespace Arch.Benchmark;

[HtmlExporter]
[MemoryDiagnoser(true)]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class QueryBenchmark {

    [Params( 10000, 100000, 1000000)]
    public int amount;
    
    private Type[] group = { typeof(Transform), typeof(Velocity) };

    private World world;
    private QueryDescription queryDescription;
    
    [GlobalSetup]
    public void Setup() {
        
        world = World.Create();
        world.Reserve(group, amount);
        
        for (var index = 0; index < amount; index++) {
            var entity = world.Create(group);
            entity.Set(new Transform{ x = 0, y = 0});
            entity.Set(new Velocity{ x = 1, y = 1});
        }

        queryDescription = new QueryDescription { All = group };
    }
    
    [Benchmark]
    public void Query() {

        world.Query(in queryDescription, (ref Transform t, ref Velocity v) => {
            t.x += v.x;
            t.y += v.y;
        });
    }
    
    [Benchmark]
    public void EntityQuery() {

        world.Query(in queryDescription, (in Entity entity, ref Transform t, ref Velocity v) => {
            t.x += v.x;
            t.y += v.y;
        });
    }

    public struct VelocityUpdate : IForEach<Transform, Velocity> {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Transform t, ref Velocity v) {
            t.x += v.x;
            t.y += v.y;
        }
    }
    
    [Benchmark]
    public void StructQuery() {
        var vel = new VelocityUpdate();
        world.HPQuery<VelocityUpdate, Transform, Velocity>(in queryDescription, ref vel);
    }
    
    public struct VelocityEntityUpdate : IForEachEntity<Transform, Velocity> {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(in Entity entity, ref Transform t, ref Velocity v) {
            t.x += v.x;
            t.y += v.y;
        }
    }
    
    [Benchmark]
    public void StructEntityQuery() {
        var vel = new VelocityEntityUpdate();
        world.HPEQuery<VelocityEntityUpdate, Transform, Velocity>(in queryDescription, ref vel);
    }
}