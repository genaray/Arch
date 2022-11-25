using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Test;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;

namespace Arch.Benchmark;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class QueryBenchmark
{
    private readonly Type[] _group = { typeof(Transform), typeof(Velocity) };

    [Params(10000, 100000, 1000000)] public int Amount;

    private JobScheduler.JobScheduler _jobScheduler;
    private QueryDescription _queryDescription;

    private World _world;

    [GlobalSetup]
    public void Setup()
    {
        //_jobScheduler = new JobScheduler.JobScheduler("Arch");

        _world = World.Create();
        _world.Reserve(_group, Amount);

        for (var index = 0; index < Amount; index++)
        {
            var entity = _world.Create(_group);
            entity.Set(new Transform { X = 0, Y = 0 });
            entity.Set(new Velocity { X = 1, Y = 1 });
        }

        _queryDescription = new QueryDescription { All = _group };
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        //_jobScheduler.Dispose();
    }

    [Benchmark]
    public void Query()
    {
        _world.Query(in _queryDescription, (ref Transform t, ref Velocity v) =>
        {
            t.X += v.X;
            t.Y += v.Y;
        });
    }

    [Benchmark]
    public void EntityQuery()
    {
        _world.Query(in _queryDescription, (in Entity entity, ref Transform t, ref Velocity v) =>
        {
            t.X += v.X;
            t.Y += v.Y;
        });
    }

    [Benchmark]
    public void StructQuery()
    {
        var vel = new VelocityUpdate();
        _world.HPQuery<VelocityUpdate, Transform, Velocity>(in _queryDescription, ref vel);
    }

    [Benchmark]
    public void StructEntityQuery()
    {
        var vel = new VelocityEntityUpdate();
        _world.HPEQuery<VelocityEntityUpdate, Transform, Velocity>(in _queryDescription, ref vel);
    }

    [Benchmark]
    public void PureEntityQuery()
    {
        _world.Query(in _queryDescription, (in Entity entity) =>
        {
            ref var t = ref entity.Get<Transform>();
            ref var v = ref entity.Get<Velocity>();

            t.X += v.X;
            t.Y += v.Y;
        });
    }

    public struct VelocityUpdate : IForEach<Transform, Velocity>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Transform t, ref Velocity v)
        {
            t.X += v.X;
            t.Y += v.Y;
        }
    }

    public struct VelocityEntityUpdate : IForEachWithEntity<Transform, Velocity>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(in Entity entity, ref Transform t, ref Velocity v)
        {
            t.X += v.X;
            t.Y += v.Y;
        }
    }
}