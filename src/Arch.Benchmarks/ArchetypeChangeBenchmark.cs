using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
// [HardwareCounters(HardwareCounter.CacheMisses)]
public class ArchetypeChangeBenchmark
{
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };

    private World _world = default!;

    [Params(10000, 100000, 1000000)] public int Amount = default!;

    private Entity[] Entities = default!;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        Entities = new Entity[Amount];

        for (var i = 0; i < Amount; i++)
        {
            _world.Create(_group);
            Entities[i] = new Entity(i, _world.Id);
        }
    }

    [Benchmark]
    public void ChangeArchetypeTwoToThree()
    {
        foreach (var entity in Entities.AsSpan())
        {
            _world.Add<Velocity>(entity);
        }
    }
}
