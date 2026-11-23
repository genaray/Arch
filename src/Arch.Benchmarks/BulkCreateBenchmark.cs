using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

/// <summary>
///     Measures creating many entities at once.
///     Every benchmark destroys the entities again, so the world is the same for each invocation and chunks and ids get reused.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class BulkCreateBenchmark
{
    [Params(10000, 100000, 1000000)] public int Amount;

    private static readonly QueryDescription _description = new QueryDescription().WithAll<Transform, Velocity>();
    private static readonly Signature _signature = new(typeof(Transform), typeof(Velocity));

    private World _world;
    private Entity[] _entities;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        _entities = new Entity[Amount];
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    [Benchmark]
    public void CreateAmountOfComponents()
    {
        _world.Create<Transform, Velocity>(Amount);
        _world.Destroy(in _description);
    }

    [Benchmark]
    public void CreateAmountOfSignature()
    {
        _world.Create(_entities.AsSpan(), in _signature, Amount);
        _world.Destroy(in _description);
    }

    [Benchmark]
    public void CreateOneByOne()
    {
        for (var index = 0; index < Amount; index++)
        {
            _world.Create(new Transform(), new Velocity());
        }

        _world.Destroy(in _description);
    }
}
