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
    
    private Type[] entityGroup = new []{ typeof(Transform), typeof(Rotation) };
    private Type[] entityAIGroup = new[] { typeof(Transform), typeof(Rotation), typeof(AI) };
    
    QueryDescription withoutAIQuery = new QueryDescription {
        All = new []{ typeof(Transform) },
        Any = new []{ typeof(Rotation) },
        None = new []{ typeof(AI) }
    };
        
    QueryDescription withAIQuery = new QueryDescription {
        All = new []{ typeof(Transform), typeof(Rotation) },
        Any = new []{ typeof(AI) },
    };
        
    [OneTimeSetUp]
    public void Setup() {
        
        world = World.Create();
        
        for(var index = 0; index < 10000; index++)
            world.Create(entityGroup);
        
        for(var index = 0; index < 10000; index++)
            world.Create(entityAIGroup);
    }
    
    [OneTimeTearDown]
    public void Teardown() {
        World.Destroy(world);
    }

    [Test]
    public void Create() {

        var size = world.Size;
        var entity = world.Create(entityGroup);
        
        Assert.AreEqual(size+1, world.Size);
        Assert.True(entity.IsAlive());
    }
    
    [Test]
    public void Destroy() {
        
        var worldSizeBefore = world.Size;
        var entity = world.Create(entityGroup);
        var worldSizeAfter = world.Size;
        world.Destroy(in entity);

        Assert.AreEqual(worldSizeBefore, world.Size);
        Assert.Less(worldSizeBefore, worldSizeAfter);
        Assert.False(entity.IsAlive());
    }
    
    [Test]
    public void CapacityTest() {

        // Add
        var before = world.Size;
        for (var index = 0; index < 500; index++)
            world.Create(entityGroup);

        // Remove
        for (var index = 0; index < 500; index++)
            world.Destroy(new Entity(index, world.Id,0));
        var after = world.Size;

        Assert.AreEqual(before, after);
    }

    [Test]
    public void RecycleId() {

        var localWorld = World.Create();
        
        var entity = localWorld.Create(entityGroup);
        localWorld.Destroy(in entity);
        var recycledEntity = localWorld.Create(entityGroup);
        var newEntity = localWorld.Create(entityGroup);

        Assert.AreEqual(entity.EntityId, recycledEntity.EntityId);
        Assert.AreNotEqual(recycledEntity.EntityId, newEntity.EntityId);
        
        World.Destroy(localWorld);
    }
    
    [Test]
    public void Reserve() {

        var beforeSize = world.Size;
        var beforeCapacity = world.Capacity;
        
        world.Reserve(entityGroup, 10000);
        for(var index = 0; index < 10000; index++)
            world.Create(entityGroup);

        Assert.Greater(world.Size, beforeSize);
        Assert.AreEqual(beforeSize+10000, world.Size);
        Assert.AreEqual(beforeCapacity+10000, world.Capacity);
    }
    
        
    [Test]
    public void DestroyAll() {

        var query = new QueryDescription { All = new[] { typeof(Transform) } };
        
        var entities = new List<Entity>();
        world.GetEntities(query, entities);

        for (var i = 0; i < entities.Count; i++) {
            var entity = entities[i];
            world.Destroy(in entity);
        }
        
        Assert.AreEqual(0, world.Size);
        Assert.AreEqual(0, world.Archetypes[0].Size);
        Assert.AreEqual(0, world.Archetypes[1].Size);
    }

    [Test]
    public void GetEntitesTest()
    {
        world = World.Create();

        var archTypes = new Type[] { typeof(Transform) };
        var query = new QueryDescription { All = archTypes };

        var entity = world.Create(archTypes);

        var entites = new List<Entity>();
        world.GetEntities(query, entites);

        Assert.That(entites.Count, Is.EqualTo(1));

        entites.Clear();
        world.Destroy(entity);
        world.GetEntities(query, entites);

        Assert.That(entites.Count, Is.EqualTo(0));
    }
}