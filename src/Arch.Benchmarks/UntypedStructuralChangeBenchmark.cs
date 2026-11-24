using Arch.Core;

namespace Arch.Benchmarks;

/// <summary>
///     Measures adding and removing components by their <see cref="ComponentType"/>, which is what the non-generic API and tools like serializers use.
///     Many components are registered first, since the lookup of the target archetype depends on how many exist.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class UntypedStructuralChangeBenchmark
{
    [Params(10000)] public int Amount;

    private readonly ComponentType[] _types = [typeof(Velocity), typeof(Health)];

    private World _world;
    private Entity[] _entities;

    [GlobalSetup]
    public void Setup()
    {
        // Registered after the components of the benchmarks, so these have higher ids than the ones used below.
        _ = Component<Transform>.ComponentType;
        _ = Component<Velocity>.ComponentType;
        _ = Component<Health>.ComponentType;

        var filler = typeof(int);
        for (var index = 0; index < 100; index++)
        {
            filler = filler.MakeArrayType();
            Component.GetComponentType(filler);
        }

        _world = World.Create();
        _entities = new Entity[Amount];
        _world.Create(_entities.AsSpan(), new Signature(typeof(Transform)), Amount);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    [Benchmark(Baseline = true)]
    public void AddAndRemoveGeneric()
    {
        for (var index = 0; index < Amount; index++)
        {
            _world.Add<Velocity, Health>(_entities[index], new Velocity(), new Health());
        }

        for (var index = 0; index < Amount; index++)
        {
            _world.Remove<Velocity, Health>(_entities[index]);
        }
    }

    [Benchmark]
    public void AddAndRemoveRange()
    {
        for (var index = 0; index < Amount; index++)
        {
            _world.AddRange(_entities[index], _types);
        }

        for (var index = 0; index < Amount; index++)
        {
            _world.RemoveRange(_entities[index], _types);
        }
    }

    [Benchmark]
    public void AddRemoveSingleType()
    {
        ComponentType type = typeof(Velocity);
        for (var index = 0; index < Amount; index++)
        {
            _world.Add(_entities[index], new Velocity());
        }

        for (var index = 0; index < Amount; index++)
        {
            _world.Remove(_entities[index], type);
        }
    }
}
