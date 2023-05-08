using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
// [HardwareCounters(HardwareCounter.CacheMisses)]
public class ArchetypeChangeBenchmark
{
    private readonly ComponentType[] _groupOne = { typeof(Transform) };

    private readonly ComponentType[] _groupFive =
    {
        typeof(Transform), typeof(Velocity), typeof(Rotation), typeof(Position2D), typeof(Position3D)
    };

    private World _world = default!;

    [Params(10000, 100000, 1000000)] public int Amount = default!;

    private Entity[] _entitiesOne = default!;
    private Entity[] _entitiesFive = default!;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        _entitiesOne = new Entity[Amount];
        _entitiesFive = new Entity[Amount];

        for (var i = 0; i < Amount; i++)
        {
            _entitiesOne[i] = _world.Create(_groupOne);
            _entitiesFive[i] = _world.Create(_groupFive);
        }
    }

    [Benchmark]
    public void AddArchetypeOne()
    {
        foreach (var entity in _entitiesOne.AsSpan())
        {
            _world.Add<Health>(entity);
        }
    }

    [Benchmark]
    public void AddArchetypeFive()
    {
        foreach (var entity in _entitiesFive.AsSpan())
        {
            _world.Add<Health>(entity);
        }
    }
}
