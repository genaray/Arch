using Arch.Core;

namespace Arch.Test;

[TestFixture]
public class QueryTest
{
    
    private JobScheduler.JobScheduler _jobScheduler;
    private World _world;

    private readonly Type[] _entityGroup = { typeof(Transform), typeof(Rotation) };
    private readonly Type[] _entityAiGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };

    private readonly QueryDescription _withoutAiQuery = new() { All = new[] { typeof(Transform) }, Any = new[] { typeof(Rotation) }, None = new[] { typeof(Ai) } };
    private readonly QueryDescription _allQuery = new() { All = new[] { typeof(Transform), typeof(Rotation) }, Any = new[] { typeof(Ai) } };

    
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
        var query = new QueryDescription { All = new[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 100);
    }

    [Test]
    public void AnyQuery()
    {
        var query = new QueryDescription { Any = new[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 100);
    }

    [Test]
    public void NoneQuery()
    {
        var query = new QueryDescription { None = new[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 0);
    }

    [Test]
    public void ComplexQuery()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(_withoutAiQuery, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 100);
    }

    [Test]
    public void ComplexScenarioQuery()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 100; index++)
            _world.Create(_entityAiGroup);

        var queryCount = 0;
        _world.Query(_withoutAiQuery, (in Entity entity) => { queryCount++; });

        var otherQueryCount = 0;
        _world.Query(_allQuery, (in Entity entity) => { otherQueryCount++; });

        Assert.AreEqual(queryCount, 100);
        Assert.AreEqual(otherQueryCount, 100);
    }

    [Test]
    public void GeneratedQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 100; index++)
            _world.Create(_entityAiGroup);

        var queryCount = 0;
        _world.Query(in _withoutAiQuery, (in Entity entity, ref Transform t) => { queryCount++; });

        var otherQueryCount = 0;
        _world.Query(in _allQuery, (ref Rotation rot) => { otherQueryCount++; });

        Assert.AreEqual(queryCount, 100);
        Assert.AreEqual(otherQueryCount, 100);
    }

    [Test]
    public void GeneratedParallelQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 1000; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 1000; index++)
            _world.Create(_entityAiGroup);

        var queryCount = 0;
        _world.ParallelQuery(in _withoutAiQuery, (in Entity entity, ref Transform t) => { Interlocked.Increment(ref queryCount); });
        
        var otherQueryCount = 0;
        _world.ParallelQuery(in _allQuery, (ref Rotation rot) => { Interlocked.Increment(ref otherQueryCount); });

        Assert.AreEqual(1000,queryCount);
        Assert.AreEqual(1000,otherQueryCount);
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

        public void Update(in Entity entity, ref Transform t0)
        {
            Counter++;
        }
    }


    [Test]
    public void GeneratedHpQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 100; index++)
            _world.Create(_entityAiGroup);

        var entityCounter = new EntityCounter { Counter = 0 };
        _world.HPEQuery<EntityCounter, Transform>(in _withoutAiQuery, ref entityCounter);

        var rotCounter = new RotCounter { Counter = 0 };
        _world.HPQuery<RotCounter, Rotation>(in _allQuery, ref rotCounter);

        Assert.AreEqual(100, entityCounter.Counter);
        Assert.AreEqual(100, rotCounter.Counter);
    }

    [Test]
    public void GeneratedHpParallelQueryTest()
    {
        _world = World.Create();
        for (var index = 0; index < 1000; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 1000; index++)
            _world.Create(_entityAiGroup);

        var entityCounter = new EntityCounter { Counter = 0 };
        _world.HPEParallelQuery<EntityCounter, Transform>(in _withoutAiQuery, ref entityCounter);

        Assert.True(true);

        var rotCounter = new RotCounter { Counter = 0 };
        _world.HPParallelQuery<RotCounter, Rotation>(in _allQuery, ref rotCounter);

        Assert.True(true);
    }
}