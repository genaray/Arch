using Arch.Core;
using Arch.Core.CommandBuffer;
using Arch.Core.Utils;

namespace Arch.Tests;

[TestFixture]
public partial class CommandBufferTest
{

    private static readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };
    private static readonly ComponentType[] _secondGroup = { typeof(Transform), typeof(Rotation), typeof(Ai), typeof(int) };
    private readonly QueryDescription _queryDescription = new() { All = _group };

    [Test]
    public void CommandBufferSparseSet()
    {
        var mySet = new SparseSet();

        var first = mySet.Create(new Entity(0, 0));
        mySet.Set(first, new Transform { X = 10, Y = 10 });
        var transform = mySet.Get<Transform>(first);

        var second = mySet.Create(new Entity(0, 0));
        mySet.Set(second, new Rotation { X = 10, Y = 10 });
        var rotation = mySet.Get<Rotation>(second);

        Assert.That(transform.X, Is.EqualTo(10));
        Assert.That(rotation.X, Is.EqualTo(10));
    }

    [Test]
    public void CommandBufferForExistingEntity()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = world.Create(new ComponentType[] { typeof(Transform), typeof(Rotation), typeof(int) });
        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback();
        Assert.That(world.Get<Transform>(in entity).X, Is.EqualTo(20));
        Assert.That(world.Get<Transform>(in entity).Y, Is.EqualTo(20));
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));

        World.Destroy(world);
    }

    [Test]
    public void CommandBuffer()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = commandBuffer.Create(new ComponentType[] { typeof(Transform), typeof(Rotation), typeof(int) });
        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback();

        entity = new Entity(0, 0);
        Assert.That(world.Get<Transform>(in entity).X, Is.EqualTo(20));
        Assert.That(world.Get<Transform>(in entity).Y, Is.EqualTo(20));
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferCombined()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer(world);

        var entity = world.Create(new ComponentType[] { typeof(Transform), typeof(Rotation), typeof(int) });
        var bufferedEntity = commandBuffer.Create(new ComponentType[] { typeof(Transform), typeof(Rotation), typeof(int) });

        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Set(in bufferedEntity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in bufferedEntity, new Ai());
        commandBuffer.Remove<int>(in bufferedEntity);

        commandBuffer.Playback();

        bufferedEntity = new Entity(1, 0);

        Assert.That(world.Get<Transform>(in entity).X, Is.EqualTo(20));
        Assert.That(world.Get<Transform>(in entity).Y, Is.EqualTo(20));
        Assert.IsTrue(world.Has<Ai>(in entity));
        Assert.IsFalse(world.Has<int>(in entity));

        Assert.That(world.Get<Transform>(in bufferedEntity).X, Is.EqualTo(20));
        Assert.That(world.Get<Transform>(in bufferedEntity).Y, Is.EqualTo(20));
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
