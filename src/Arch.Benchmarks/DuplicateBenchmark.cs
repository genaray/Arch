using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
//[MemoryDiagnoser]
//[HardwareCounters(HardwareCounter.CacheMisses)]
public class DuplicateBenchmark
{
    public int Amount = 100000;

    private static readonly ComponentType[] _group = { typeof(Transform), typeof(Velocity) };
    private readonly QueryDescription _queryDescription = new(all: _group);

    private static World? _world;
    private static Entity _entity = Entity.Null;
    private static Entity[]? _array = null;

    [IterationSetup]
    public void Setup()
    {
        _world = World.Create();
        _world.Reserve(_group, 1);
        _entity = _world.Create(new Transform { X = 111, Y = 222}, new Velocity { X = 333, Y = 444 });
        _array = new Entity[Amount];
    }

    [IterationCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
        _world = null;
    }

    /// DuplicateN() method.
    [Benchmark]
    public void DuplicateNInternal()
    {
        _world.DuplicateN(_entity, _array.AsSpan());
    }

    /// DuplicateN() in terms of Duplicate() method.
    [Benchmark]
    public void DuplicateNDuplicate()
    {
        for (int i = 0; i < Amount; ++i)
        {
            _array[i] = _world.Duplicate(_entity);
        }
    }

    /// Benchmark DuplicateN() if implemented via GetAllComponents.
    [Benchmark]
    public void DuplicateNGetAllComponents()
    {
        for (int i = 0; i < Amount; ++i)
        {
            var arch = _world.GetArchetype(_entity);
            var copiedEntity = _world.Create(arch.Signature);
            foreach (var c in _world.GetAllComponents(_entity))
            {
                _world.Set(_entity, c);
            }
        }
    }
}
