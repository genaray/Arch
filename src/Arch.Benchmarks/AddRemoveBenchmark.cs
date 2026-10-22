using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

/// <summary>
///     Measures the cost of the same-archetype guard in <see cref="World.Move"/> on the
///     success path. A valid add + remove round-trip pays the guard's single reference
///     comparison (<c>source == destination</c>) on already-computed archetypes but never
///     takes the throwing branch, so this quantifies the overhead the safety check adds to
///     normal structural changes.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class AddRemoveBenchmark
{
    [Params(10000, 100000)] public int Amount;

    private static readonly ComponentType[] _group = { typeof(Transform) };

    private World _world = null!;
    private Entity[] _entities = null!;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        _world.Reserve(_group, Amount);

        _entities = new Entity[Amount];
        for (var index = 0; index < Amount; index++)
        {
            _entities[index] = _world.Create(_group);
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    [Benchmark]
    public void AddRemove()
    {
        var world = _world;
        var entities = _entities;
        for (var index = 0; index < entities.Length; index++)
        {
            var entity = entities[index];
            world.Add<Velocity>(entity);
            world.Remove<Velocity>(entity);
        }
    }
}
