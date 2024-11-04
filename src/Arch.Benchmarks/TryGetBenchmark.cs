using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class TryGetBenchmark
{
    private static World _world;
    private static List<Entity> _entities;

    private Consumer _consumer = new();

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();

        _entities = new List<Entity>(1_000_000);
        for (var index = 0; index < 1_000_000; index++)
        {
            _entities.Add(_world.Create(new Transform(), new Velocity()));
        }
    }

    [Benchmark]
    public void TryGetGenericRef()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            var xform = _world.TryGetRef<Transform>(entity, out var exists);

            if (exists)
            {
                _consumer.Consume(xform);
            }
        }
    }

    [Benchmark]
    public void TryGetGeneric()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];

            if (_world.TryGet<Transform>(entity, out var xform))
            {
                _consumer.Consume(xform);
            }
        }
    }

    [Benchmark]
    public void TryGet()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];

            if (_world.TryGet(entity, Component.GetComponentType(typeof(Transform)), out var xform))
            {
                _consumer.Consume(xform);
            }
        }
    }
}
