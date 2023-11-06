using Arch.Core;

namespace Arch.Benchmarks;

[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class EntityInfoStorageBenchmark
{

    private static World _world;
    private static List<Entity> _entities;

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

    /*
    [Benchmark(Baseline = true)]
    public Transform Get()
    {
        ref var transform = ref Unsafe.NullRef<Transform>();
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            transform = ref _world.Get<Transform>(entity);
        }

        return transform;
    }

    [Benchmark]
    public Transform MergedGet()
    {
        ref var transform = ref Unsafe.NullRef<Transform>();
        for (var index = 0; index < _entities.Count; index++)
        {
            var entity = _entities[index];
            transform = ref _world.MergedGet<Transform>(entity);
        }
        return transform;
    }*/
}
