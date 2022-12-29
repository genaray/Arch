using System.Diagnostics;
using Arch.Core;
using Arch.Core.CommandBuffer;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public partial class CommandBufferTest
{

    private static readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };
    private static readonly ComponentType[] _secondGroup = { typeof(Transform), typeof(Rotation), typeof(Ai), typeof(int) };
    private readonly QueryDescription _queryDescription = new(){ All = _group };
    
    [Test]
    public void CommandBufferSparseSet()
    {
        var mySet = new SparseSet();
        
        var first = mySet.Create(new Entity(0, 0));
        mySet.Set(first, new Transform{ X = 10, Y = 10});
        var transform = mySet.Get<Transform>(first);
        
        var second = mySet.Create(new Entity(0, 0));
        mySet.Set(second, new Rotation{ X = 10, Y = 10});
        var rotation = mySet.Get<Rotation>(second);
        
        Assert.AreEqual(10, transform.X);
        Assert.AreEqual(10, rotation.X);
    }

    [Test]
    public void CommandBufferForExistingEntity()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = world.Create(new ComponentType[] {typeof(Transform), typeof(Rotation), typeof(int) });
        commandBuffer.Set(in entity, new Transform{ X = 20, Y = 20});
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback();
        Assert.AreEqual(20, world.Get<Transform>(in entity).X);
        Assert.AreEqual(20, world.Get<Transform>(in entity).Y);
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));
        
        World.Destroy(world);
    }
    
    [Test]
    public void CommandBuffer()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = commandBuffer.Create(new ComponentType[] {typeof(Transform), typeof(Rotation), typeof(int) });
        commandBuffer.Set(in entity, new Transform{ X = 20, Y = 20});
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback();

        entity = new Entity(0, 0);
        Assert.AreEqual(20, world.Get<Transform>(in entity).X);
        Assert.AreEqual(20, world.Get<Transform>(in entity).Y);
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));
        
        World.Destroy(world);
    }
    
    [Test]
    public void CommandBufferCombined()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = world.Create(new ComponentType[] {typeof(Transform), typeof(Rotation), typeof(int) });
        var bufferedEntity = commandBuffer.Create(new ComponentType[] {typeof(Transform), typeof(Rotation), typeof(int) });
        
        commandBuffer.Set(in entity, new Transform{ X = 20, Y = 20});
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);
        
        commandBuffer.Set(in bufferedEntity, new Transform{ X = 20, Y = 20});
        commandBuffer.Add(in bufferedEntity, new Ai());
        commandBuffer.Remove<int>(in bufferedEntity);

        commandBuffer.Playback();

        bufferedEntity = new Entity(1, 0);
        
        Assert.AreEqual(20, world.Get<Transform>(in entity).X);
        Assert.AreEqual(20, world.Get<Transform>(in entity).Y);
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));
        
        Assert.AreEqual(20, world.Get<Transform>(in bufferedEntity).X);
        Assert.AreEqual(20, world.Get<Transform>(in bufferedEntity).Y);
        Assert.IsTrue(world.Has<Ai>(in bufferedEntity));
        Assert.IsFalse(world.Has<int>(in bufferedEntity));
        
        World.Destroy(world);
    }
}


[TestFixture]
public partial class CommandBufferTest
{

    private JobScheduler.JobScheduler _jobScheduler;
    
    [OneTimeSetUp]
    public void Setup()
    {
        _jobScheduler = new JobScheduler.JobScheduler("CommandBuffer");
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _jobScheduler.Dispose();
    }
}