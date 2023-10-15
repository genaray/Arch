using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public partial class QueryTest
{
    private JobScheduler.JobScheduler _jobScheduler;
    private World? _world;

    private static readonly ComponentType[] _entityGroup = { typeof(Transform), typeof(Rotation) };
    private static readonly ComponentType[] _entityAiGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };

    private readonly QueryDescription _withoutAiQuery = new() { All = new ComponentType[] { typeof(Transform) }, Any = new ComponentType[] { typeof(Rotation) }, None = new ComponentType[] { typeof(Ai) } };
    private readonly QueryDescription _allQuery = new() { All = new ComponentType[] { typeof(Transform), typeof(Rotation) }, Any = new ComponentType[] { typeof(Ai) } };

    [OneTimeSetUp]
    public void Setup()
    {
        _jobScheduler = new JobScheduler.JobScheduler("Test");
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _jobScheduler.Dispose();
    }

    [Test]
    public void AllQuery()
    {
        var query = new QueryDescription { All = new ComponentType[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        var count = 0;
        _world.Query(query, (Entity entity) => count++);
        That(count, Is.EqualTo(100));
    }

    [Test]
    public void AnyQuery()
    {
        var query = new QueryDescription { Any = new ComponentType[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        var count = 0;
        _world.Query(query, (Entity entity) => count++);
        That(count, Is.EqualTo(100));
    }

    [Test]
    public void NoneQuery()
    {
        var query = new QueryDescription { None = new ComponentType[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        var count = 0;
        _world.Query(query, (Entity entity) => count++);
        That(count, Is.EqualTo(0));
    }

    [Test]
    public void EmptyQuery()
    {
        var query = new QueryDescription { None = new ComponentType[] { typeof(int) } };

        _world = World.Create();
        _world.Create();

        var count = 0;
        _world.Query(query, _ => count++);
        That(count, Is.EqualTo(1));
    }

    [Test]
    public void ExclusiveQuery()
    {
        var exclusiveGroup = new ComponentType[] { typeof(Transform), typeof(Rotation) };
        var query = new QueryDescription { Exclusive = exclusiveGroup };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var count = 0;
        _world.Query(query, (Entity entity) => count++);
        That(count, Is.EqualTo(0));

        for (var index = 0; index < 100; index++)
        {
            _world.Create(exclusiveGroup);
        }

        count = 0;
        _world.Query(query, (Entity entity) => count++);
        That(count, Is.EqualTo(100));
    }

    [Test]
    public void ComplexQuery()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        var count = 0;
        _world.Query(_withoutAiQuery, (Entity entity) => count++);
        That(count, Is.EqualTo(100));
    }

    [Test]
    public void ComplexScenarioQuery()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var queryCount = 0;
        _world.Query(_withoutAiQuery, (Entity entity) => queryCount++);

        var otherQueryCount = 0;
        _world.Query(_allQuery, (Entity entity) => otherQueryCount++);

        That(queryCount, Is.EqualTo(100));
        That(otherQueryCount, Is.EqualTo(100));
    }
}

public partial class QueryTest
{

    [Test]
    public void GeneratedQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var queryCount = 0;
        _world.Query(in _withoutAiQuery, (Entity entity, ref Transform t) => queryCount++);

        var otherQueryCount = 0;
        _world.Query(in _allQuery, (ref Rotation rot) => otherQueryCount++);

        That(queryCount, Is.EqualTo(100));
        That(otherQueryCount, Is.EqualTo(100));
    }

    [Test]
    public void GeneratedParallelQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 1000; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 1000; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var queryCount = 0;
        _world.ParallelQuery(in _withoutAiQuery, (Entity entity, ref Transform t) => Interlocked.Increment(ref queryCount));

        var otherQueryCount = 0;
        _world.ParallelQuery(in _allQuery, (ref Rotation rot) => Interlocked.Increment(ref otherQueryCount));

        That(queryCount, Is.EqualTo(1000));
        That(otherQueryCount, Is.EqualTo(1000));
    }

    public struct RotCounter : IForEach<Rotation>
    {
        public int Counter;

        public void Update(ref Rotation t0)
        {
            Counter++;
        }
    }

    public struct EntityCounter : IForEachWithEntity<Transform>
    {
        public int Counter;

        public void Update(Entity entity, ref Transform t0)
        {
            Counter++;
        }
    }

    [Test]
    public void GeneratedHpQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 100; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var entityCounter = new EntityCounter { Counter = 0 };
        _world.InlineEntityQuery<EntityCounter, Transform>(in _withoutAiQuery, ref entityCounter);

        var rotCounter = new RotCounter { Counter = 0 };
        _world.InlineQuery<RotCounter, Rotation>(in _allQuery, ref rotCounter);

        That(entityCounter.Counter, Is.EqualTo(100));
        That(rotCounter.Counter, Is.EqualTo(100));
    }

    [Test]
    public void GeneratedHpParallelQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 1000; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 1000; index++)
        {
            _world.Create(_entityAiGroup);
        }

        var entityCounter = new EntityCounter { Counter = 0 };
        _world.InlineEntityQuery<EntityCounter, Transform>(in _withoutAiQuery, ref entityCounter);

        True(true);

        var rotCounter = new RotCounter { Counter = 0 };
        _world.InlineQuery<RotCounter, Rotation>(in _allQuery, ref rotCounter);

        True(true);
    }
}
