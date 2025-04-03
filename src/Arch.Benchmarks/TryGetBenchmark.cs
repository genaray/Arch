using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
public class TryGetBenchmark
{
    [Params(10000, 100000, 1000000)] public int Amount;

    private static World _world;
    private static List<Entity> _entities;

    private Consumer _consumer = new();

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();

        _entities = new List<Entity>(Amount);
        for (var index = 0; index < Amount; index++)
        {
            _entities.Add(_world.Create(new Transform(), new Velocity()));
        }
    }

    [Benchmark]
    public void TryGetGenericRefSuccess()
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
    public void TryGetGenericRefFail()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            _world.TryGetRef<Position2D>(entity, out var exists);
            _consumer.Consume(exists);
        }
    }

    [Benchmark]
    public void TryGetGenericSuccess()
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
    public void TryGetGenericFail()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];

            if (!_world.TryGet<Position2D>(entity, out var pos))
            {
                _consumer.Consume(pos);
            }
        }
    }

    [Benchmark]
    public void TryGetSuccess()
    {
        var xformType = Component.GetComponentType(typeof(Transform));

        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];

            if (_world.TryGet(entity, xformType, out var xform))
            {
                _consumer.Consume(xform);
            }
        }
    }

    [Benchmark]
    public void TryGetFail()
    {
        var xformType = Component.GetComponentType(typeof(Position2D));

        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];

            if (!_world.TryGet(entity, xformType, out var pos))
            {
                _consumer.Consume(pos);
            }
        }
    }
}
