using System.Diagnostics;
using Arch.Core;
using Arch.Core.CommandBuffer;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public partial class CommandBufferTest
{

    private static readonly Type[] _group = { typeof(Transform), typeof(Rotation) };
    private static readonly Type[] _secondGroup = { typeof(Transform), typeof(Rotation), typeof(Ai), typeof(int) };
    private readonly QueryDescription _queryDescription = new QueryDescription { All = _group };

    [Test]
    public void CommandBufferSparseSet()
    {
        
        var mySet = new SparseSet();
        
        var first = mySet.Create(new Entity(0, 0, 0));
        mySet.Set(first, new Transform{ X = 10, Y = 10});
        var transform = mySet.Get<Transform>(first);
        
        var second = mySet.Create(new Entity(0, 0, 0));
        mySet.Set(second, new Rotation{ X = 10, Y = 10});
        var rotation = mySet.Get<Rotation>(second);
        
        Assert.AreEqual(10, transform.X);
        Assert.AreEqual(10, rotation.X);
    }
    
    [Test]
    public void DestructionBuffer()
    {
        var world = World.Create();
        var buffer = new DestructionBuffer(world, 1000);
        for (var index = 0; index < 1000; index++)
            world.Create(new Transform{ X = 10}, new Rotation{ W = 10});
        
        Assert.AreEqual(1000, world.Size);
        
        world.Query(in _queryDescription, buffer.Destroy);
        buffer.Playback();
        
        Assert.AreEqual(0, world.Size);
        World.Destroy(world);
    }
    
    [Test]
    public void CreationBuffer()
    {
        var world = World.Create();
        var buffer = new CreationBuffer(world);
        
        var entity = buffer.Create(_group);
        buffer.Set(in entity, new Transform{ X = 10 });
        buffer.Set(in entity, new Rotation{ W = 10 });
        
        var secondEntity = buffer.Create(_group);
        buffer.Set(in secondEntity, new Transform{ X = 10 });
        buffer.Set(in secondEntity, new Rotation{ W = 10 });
        
        buffer.Playback();
        
        Assert.AreEqual(2, world.Size);

        var entities = new List<Entity>();
        world.GetEntities(in _queryDescription, entities);
        Assert.AreEqual(10, entities[0].Get<Transform>().X);
        Assert.AreEqual(10, entities[0].Get<Rotation>().W);
        Assert.AreEqual(10, entities[1].Get<Transform>().X);
        Assert.AreEqual(10, entities[1].Get<Rotation>().W);
        
        World.Destroy(world);
    }
    
    [Test]
    public void ModificationBuffer()
    {
        var world = World.Create();
        var buffer = new ModificationBuffer(world);

        var entity = world.Create(_group);
        var bufferedEntity = buffer.Modify(in entity);
        buffer.Set(in bufferedEntity, new Transform{ X = 10 });
        buffer.Set(in bufferedEntity, new Rotation{ X = 10 });
        
        buffer.Playback();
        
        Assert.AreEqual(10, entity.Get<Transform>().X);
        Assert.AreEqual(10, entity.Get<Rotation>().X);
        
        World.Destroy(world);
    }
    
    [Test]
    public void StructuralBuffer()
    {
        var world = World.Create();
        var buffer = new StructuralBuffer(world);
        
        var entity = world.Create(_group);
        var secondEntity = world.Create(_secondGroup);
        
        var addToEntity = buffer.BatchAdd(in entity);
        buffer.Add<Ai>(in addToEntity);
        buffer.Add<int>(in addToEntity);
        
        var removeFromEntity = buffer.BatchRemove(in secondEntity);
        buffer.Remove<Ai>(in removeFromEntity);
        buffer.Remove<int>(in removeFromEntity);
        
        buffer.Playback();
        
        Assert.AreEqual(true, entity.Has<Ai, int>());
        Assert.AreEqual(false, secondEntity.Has<Ai, int>());
        

        World.Destroy(world);
    }
}


[TestFixture]
public partial class CommandBufferTest
{
    
    [Test]
    public void ParallelDestructionBuffer()
    {

        var scheduler = new JobScheduler.JobScheduler("Arch");
        
        var world = World.Create();
        var buffer = new DestructionBuffer(world, 1000);
        for (var index = 0; index < 1000; index++)
            world.Create(new Transform{ X = 10}, new Rotation{ W = 10});
        
        Assert.AreEqual(1000, world.Size);
        
        world.ParallelQuery(in _queryDescription, buffer.Destroy);
        buffer.Playback();
        
        Assert.AreEqual(0, world.Size);
        World.Destroy(world);
        scheduler.Dispose();
    }
    
    [Test]
    public void ParallelCreationBuffer()
    {
        var scheduler = new JobScheduler.JobScheduler("Arch");
        
        var world = World.Create();
        var buffer = new CreationBuffer(world);
        for (var index = 0; index < 1000; index++)
            world.Create(new Transform{ X = 10}, new Rotation{ W = 10});
        
                
        world.ParallelQuery(in _queryDescription, (in Entity entity1) => {
            
            var entity = buffer.Create(_group);
            buffer.Set(in entity, new Transform{ X = 10 });
            buffer.Set(in entity, new Rotation{ W = 10 });
        });


        buffer.Playback();
        
        Assert.AreEqual(2000, world.Size);

        World.Destroy(world);
        scheduler.Dispose();
    }
}