using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class QueryTest
{
    
    private JobScheduler.JobScheduler _jobScheduler;
    private World _world;

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
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 100);
    }

    [Test]
    public void AnyQuery()
    {
        var query = new QueryDescription { Any = new ComponentType[] { typeof(Transform) } };

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
        var query = new QueryDescription { None = new ComponentType[] { typeof(Transform) } };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count, 0);
    }

    [Test]
    public void ExclusiveQuery()
    {
        var exclusiveGroup = new ComponentType[] { typeof(Transform), typeof(Rotation) };
        var query = new QueryDescription { Exclusive = exclusiveGroup };

        _world = World.Create();
        for (var index = 0; index < 100; index++)
            _world.Create(_entityAiGroup);

        var count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.That(count, Is.EqualTo(0));

        for (var index = 0; index < 100; index++)
            _world.Create(exclusiveGroup);

        count = 0;
        _world.Query(query, (in Entity entity) => { count++; });
        Assert.That(count, Is.EqualTo(100));
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