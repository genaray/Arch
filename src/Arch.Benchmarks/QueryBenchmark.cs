using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class QueryBenchmark
{
    [Params(10000, 100000, 1000000)] public int Amount;

    private readonly JobScheduler.JobScheduler? _jobScheduler;
    private static readonly ComponentType[] _group = { typeof(Transform), typeof(Velocity) };
    private QueryDescription _queryDescription = new() { All = _group };

    private static World? _world;

    [GlobalSetup]
    public void Setup()
    {
        // _jobScheduler = new JobScheduler.JobScheduler("Arch");

        _world = World.Create();
        _world.Reserve(_group, Amount);

        for (var index = 0; index < Amount; index++)
        {
            var entity = _world.Create(_group);
            _world.Set(in entity, new Transform { X = 0, Y = 0 }, new Velocity { X = 1, Y = 1 });
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        //_jobScheduler.Dispose();
    }

    [Benchmark]
    public void WorldEntityQuery()
    {
        _world.Query(in _queryDescription, static (in Entity entity) =>
        {
            var refs = _world.Get<Transform, Velocity>(in entity);

            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        });
    }

#if !PURE_ECS
    [Benchmark]
    public void EntityExtensionQuery()
    {
        _world.Query(in _queryDescription, (in Entity entity) =>
        {
            var refs = entity.Get<Transform, Velocity>();

            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        });
    }
#endif

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
    public void QueryIterator()
    {
        foreach (var refs in _world.Query(in _queryDescription).GetIterator<Transform, Velocity>())
        {
            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        }
    }

    [Benchmark]
    public void EntityQueryIterator()
    {
        foreach (var refs in _world.Query(in _queryDescription).GetEntityIterator<Transform, Velocity>())
        {
            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        }
    }

    [Benchmark]
    public void StructQuery()
    {
        var vel = new VelocityUpdate();
        _world.InlineQuery<VelocityUpdate, Transform, Velocity>(in _queryDescription, ref vel);
    }

    [Benchmark]
    public void StructEntityQuery()
    {
        var vel = new VelocityEntityUpdate();
        _world.InlineEntityQuery<VelocityEntityUpdate, Transform, Velocity>(in _queryDescription, ref vel);
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
