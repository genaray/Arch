using Arch.Core;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
public class AddRemoveBenchmark
{
    [Params(10000, 100000, 1000000)] public int Amount;

    private static World _world;
    private static List<Entity> _entities;

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
    public void Add()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            _world.Add<Position2D>(entity);
        }
    }

    [Benchmark]
    public void Remove()
    {
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            _world.Remove<Transform>(entity);
        }
    }
}
