using Arch.Core;

namespace Arch.Benchmarks;

/// <summary>
///     Measures moving entities between archetypes and destroying them in creation and in random order.
///     Every benchmark leaves the world as it found it, so all of them can be repeated.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class StructuralChangeBenchmark
{
    [Params(10000, 100000, 1000000)] public int Amount;

    private World _world;
    private Entity[] _entities;
    private Entity[] _created;
    private int[] _randomOrder;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();

        _entities = new Entity[Amount];
        for (var index = 0; index < Amount; index++)
        {
            _entities[index] = _world.Create(new Transform(), new Velocity());
        }

        _created = new Entity[Amount];
        _randomOrder = Enumerable.Range(0, Amount).ToArray();
        new Random(42).Shuffle(_randomOrder);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    /// <summary>
    ///     Moves every entity to the archetype with an additional component and back again.
    /// </summary>
    [Benchmark]
    public void AddAndRemove()
    {
        for (var index = 0; index < _entities.Length; index++)
        {
            _world.Add<Health>(_entities[index]);
        }

        for (var index = 0; index < _entities.Length; index++)
        {
            _world.Remove<Health>(_entities[index]);
        }
    }

    /// <summary>
    ///     Creates entities and destroys them in the order they were created.
    /// </summary>
    [Benchmark]
    public void CreateAndDestroy()
    {
        for (var index = 0; index < _created.Length; index++)
        {
            _created[index] = _world.Create(new Transform(), new Velocity());
        }

        for (var index = 0; index < _created.Length; index++)
        {
            _world.Destroy(_created[index]);
        }
    }

    /// <summary>
    ///     Creates entities and destroys them in random order, which makes every destroy touch a different chunk.
    /// </summary>
    [Benchmark]
    public void CreateAndDestroyInRandomOrder()
    {
        for (var index = 0; index < _created.Length; index++)
        {
            _created[index] = _world.Create(new Transform(), new Velocity());
        }

        for (var index = 0; index < _randomOrder.Length; index++)
        {
            _world.Destroy(_created[_randomOrder[index]]);
        }
    }

    /// <summary>
    ///     Destroys and creates entities in turns, so every created entity reuses the id of a destroyed one.
    /// </summary>
    [Benchmark]
    public void DestroyAndCreateChurn()
    {
        for (var index = 0; index < _entities.Length; index++)
        {
            _world.Destroy(_entities[index]);
            _entities[index] = _world.Create(new Transform(), new Velocity());
        }
    }
}
