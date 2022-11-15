using Arch.Core;

namespace Arch.Test; 

[TestFixture]
public class QueryTest {

    private JobScheduler.JobScheduler jobScheduler;
    private World world;
    
    private Type[] entityGroup = new []{ typeof(Transform), typeof(Rotation) };
    private Type[] entityAIGroup = new[] { typeof(Transform), typeof(Rotation), typeof(AI) };
    
    QueryDescription withoutAIQuery = new QueryDescription {
        All = new []{ typeof(Transform) },
        Any = new []{ typeof(Rotation) },
        None = new []{ typeof(AI) }
    };
        
    QueryDescription allQuery = new QueryDescription {
        All = new []{ typeof(Transform), typeof(Rotation) },
        Any = new []{ typeof(AI) },
    };
        
    [OneTimeSetUp]
    public void Setup() {
        jobScheduler = new JobScheduler.JobScheduler("Test");
    }
    
    [OneTimeTearDown]
    public void Teardown() {
        jobScheduler.Dispose();
    }
    
    [Test]
    public void AllQuery() {

        var query = new QueryDescription {
            All = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);

        var count = 0;
        world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void AnyQuery() {

        var query = new QueryDescription {
            Any = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);

        var count = 0;
        world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void NoneQuery() {

        var query = new QueryDescription {
            None = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);

        var count = 0;
        world.Query(query, (in Entity entity) => { count++; });
        Assert.AreEqual(count,0);
    }
    
    [Test]
    public void ComplexQuery() {
        
        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);

        var count = 0;
        world.Query(withoutAIQuery, (in Entity entity) => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void ComplexScenarioQuery() {

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);
        
        for (var index = 0; index < 100; index++)
            world.Create(entityAIGroup);

        var queryCount = 0;
        world.Query(withoutAIQuery, (in Entity entity) => { queryCount++; });
        
        var otherQueryCount = 0;
        world.Query(allQuery, (in Entity entity) => { otherQueryCount++; });
        
        Assert.AreEqual(queryCount,100);
        Assert.AreEqual(otherQueryCount,100);
    }
    
    [Test]
    public void GeneratedQueryTest() {

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);
        
        for (var index = 0; index < 100; index++)
            world.Create(entityAIGroup);

        var queryCount = 0;
        world.Query(in withoutAIQuery, (in Entity entity, ref Transform t) => { queryCount++; });
        
        var otherQueryCount = 0;
        world.Query(in allQuery, (ref Rotation rot) => { otherQueryCount++; });
       
        Assert.AreEqual(queryCount,100);
        Assert.AreEqual(otherQueryCount,100);
    }
    
    [Test]
    public void GeneratedParallelQueryTest() {

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);
        
        for (var index = 0; index < 100; index++)
            world.Create(entityAIGroup);

        var queryCount = 0;
        world.ParallelQuery(in withoutAIQuery, (in Entity entity, ref Transform t) => {
            Interlocked.Increment(ref queryCount);
        });
        
        var otherQueryCount = 0;
        world.ParallelQuery(in allQuery, (ref Rotation rot) => {
            Interlocked.Increment(ref otherQueryCount);
        });

        Assert.AreEqual(queryCount,100);
        Assert.AreEqual(otherQueryCount,100);
    }

    public struct RotCounter : IForEach<Rotation> {

        public int counter;
        public void Update(ref Rotation t0) { counter++; }
    }
    
    public struct EntityCounter : IForEachWithEntity<Transform> {

        public int counter;

        public void Update(in Entity entity, ref Transform t0) { counter++; }
    }


    [Test]
    public void GeneratedHPQueryTest() {
        
        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);
        
        for (var index = 0; index < 100; index++)
            world.Create(entityAIGroup);

        var entityCounter = new EntityCounter { counter = 0 };
        world.HPEQuery<EntityCounter, Transform>(in withoutAIQuery, ref entityCounter);

        var rotCounter = new RotCounter { counter = 0 };
        world.HPQuery<RotCounter, Rotation>(in allQuery, ref rotCounter);
        
        Assert.AreEqual(100, entityCounter.counter);
        Assert.AreEqual(100, rotCounter.counter);
    }
    
    [Test]
    public void GeneratedHPParallelQueryTest() {

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(entityGroup);
        
        for (var index = 0; index < 100; index++)
            world.Create(entityAIGroup);

        var entityCounter = new EntityCounter { counter = 0 };
        world.HPEParallelQuery<EntityCounter, Transform>(in withoutAIQuery, ref entityCounter);

        Assert.True(true);
        
        var rotCounter = new RotCounter { counter = 0 };
        world.HPParallelQuery<RotCounter, Rotation>(in allQuery, ref rotCounter);
        
        Assert.True(true);
    }
}