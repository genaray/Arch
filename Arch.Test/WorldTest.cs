using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using NUnit.Framework;

namespace Arch.Test; 

[TestFixture]
public class WorldTest {

    private World world;
    private Type[] group;
    private Type[] otherGroup;
        
    [OneTimeSetUp]
    public void Setup() {
        
        group = new []{ typeof(Transform), typeof(Rotation) };
        otherGroup = new[] { typeof(Transform), typeof(Rotation), typeof(AI) };
    }

    [Test]
    public void Create() {

        world = World.Create();
        
        var entity = world.Create(group);
        Assert.AreEqual(1, world.Size);
        Assert.AreEqual(0, entity.EntityId);
        Assert.True(entity.IsAlive());
    }
    
    [Test]
    public void Destroy() {

        world = World.Create();
        var worldSizeBefore = world.Size;
        var entity = world.Create(group);
        var worldSizeAfter = world.Size;
        world.Destroy(in entity);

        Assert.AreEqual(worldSizeBefore, world.Size);
        Assert.Less(worldSizeBefore, worldSizeAfter);
        Assert.False(entity.IsAlive());
    }
    
    [Test]
    public void CapacityTest() {

        world = World.Create();
        
        for (var index = 0; index < 500; index++)
            world.Create(group);
            
        for (var index = 0; index < 500; index++)
            world.Destroy(new Entity(index, world.Id,0));

        Assert.AreEqual(world.Size, 0);
        Assert.AreEqual(world.Capacity, world.Archetypes[0].EntitiesPerChunk);
    }

    [Test]
    public void RecycleId() {
        
        world = World.Create();
        var entity = world.Create(group);
        world.Destroy(in entity);
        var recycledEntity = world.Create(group);
        var newEntity = world.Create(group);

        Assert.AreEqual(entity.EntityId, recycledEntity.EntityId);
        Assert.AreNotEqual(recycledEntity.EntityId, newEntity.EntityId);
    }
    
    [Test]
    public void Reserve() {
        
        world = World.Create();
        world.Reserve(group, 10000);
        
        for(var index = 0; index < 10000; index++)
            world.Create(group);
        
        Assert.AreEqual(10000, world.Size);
        Assert.AreEqual(10000, world.Capacity);
    }
    
    [Test]
    public void AllQuery() {

        var query = new QueryDescription {
            All = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);

        var count = 0;
        world.Query(query, entity => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void AnyQuery() {

        var query = new QueryDescription {
            Any = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);

        var count = 0;
        world.Query(query, entity => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void NoneQuery() {

        var query = new QueryDescription {
            None = new []{ typeof(Transform) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);

        var count = 0;
        world.Query(query, entity => { count++; });
        Assert.AreEqual(count,0);
    }
    
    [Test]
    public void ComplexQuery() {

        var query = new QueryDescription {
            All = new []{ typeof(Transform) },
            Any = new []{ typeof(Rotation) },
            None = new []{ typeof(AI) }
        };

        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);

        var count = 0;
        world.Query(query, entity => { count++; });
        Assert.AreEqual(count,100);
    }
    
    [Test]
    public void ComplexScenarioQuery() {

        var query = new QueryDescription {
            All = new []{ typeof(Transform) },
            Any = new []{ typeof(Rotation) },
            None = new []{ typeof(AI) }
        };
        
        var otherQuery = new QueryDescription {
            All = new []{ typeof(Transform), typeof(Rotation) },
            Any = new []{ typeof(AI) },
        };


        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);
        
        for (var index = 0; index < 100; index++)
            world.Create(otherGroup);

        var queryCount = 0;
        world.Query(query, entity => { queryCount++; });
        
        var otherQueryCount = 0;
        world.Query(otherQuery, entity => { otherQueryCount++; });
        
        Assert.AreEqual(queryCount,100);
        Assert.AreEqual(otherQueryCount,100);
    }
    
    [Test]
    public void GeneratedQueryTest() {

        var query = new QueryDescription {
            All = new []{ typeof(Transform) },
            Any = new []{ typeof(Rotation) },
            None = new []{ typeof(AI) }
        };
        
        var otherQuery = new QueryDescription {
            All = new []{ typeof(Transform), typeof(Rotation) },
            Any = new []{ typeof(AI) },
        };


        world = World.Create();
        for (var index = 0; index < 100; index++)
            world.Create(group);
        
        for (var index = 0; index < 100; index++)
            world.Create(otherGroup);

        var queryCount = 0;
        world.Query(query, (in Entity entity, ref Transform t) => { queryCount++; });
        
        var otherQueryCount = 0;
        world.Query(otherQuery, (ref Rotation rot) => { otherQueryCount++; });
        
        Assert.AreEqual(queryCount,100);
        Assert.AreEqual(otherQueryCount,100);
    }
}