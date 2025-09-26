using Arch.Buffer;
using Arch.Core;
using Arch.Core.Utils;
using Schedulers;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public sealed partial class CommandBufferTest
{

    private static readonly ComponentType[] _group = [typeof(Transform), typeof(Rotation)];
    private static readonly ComponentType[] _secondGroup = [typeof(Transform), typeof(Rotation), typeof(Ai), typeof(int)];
    private readonly QueryDescription _queryDescription = new(all: _group);

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

        That(transform.X, Is.EqualTo(10));
        That(rotation.X, Is.EqualTo(10));
    }

    [Test]
    public void CommandBufferForExistingEntity()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer();

        var entity = world.Create([typeof(Transform), typeof(Rotation), typeof(int)]);
        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback(world);
        That(world.Get<Transform>(entity).X, Is.EqualTo(20));
        That(world.Get<Transform>(entity).Y, Is.EqualTo(20));
        IsTrue(world.Has<Ai>(entity));
        IsFalse(world.Has<int>(entity));

        World.Destroy(world);
    }

    [Test]
    public void CommandBuffer()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer();

        var entity = commandBuffer.Create([typeof(Transform), typeof(Rotation), typeof(int)]);
        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Playback(world);

        entity = new Entity(0, 0);
        That(world.Get<Transform>(entity).X, Is.EqualTo(20));
        That(world.Get<Transform>(entity).Y, Is.EqualTo(20));
        IsTrue(world.Has<Ai>(entity));
        IsFalse(world.Has<int>(entity));

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferCreateMultipleEntities()
    {
        var world = World.Create();

        var entities = new List<Entity>();
        using (var commandBuffer = new CommandBuffer())
        {
            entities.Add(commandBuffer.Create([typeof(Transform)]));
            entities.Add(commandBuffer.Create([typeof(Transform)]));
            entities.Add(commandBuffer.Create([typeof(Transform)]));
            commandBuffer.Playback(world);
        }

        That(world.Size, Is.EqualTo(entities.Count));

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferCreateAndDestroy()
    {
        var world = World.Create();

        using (var commandBuffer = new CommandBuffer())
        {
            commandBuffer.Create([typeof(Transform)]);
            commandBuffer.Create([typeof(Transform)]);
            var e = commandBuffer.Create([typeof(Transform)]);
            commandBuffer.Destroy(e);
            commandBuffer.Playback(world);
        }

        That(world.Size, Is.EqualTo(2));

        var query = new QueryDescription(all: [typeof(Transform)]);
        var entities = new Entity[world.CountEntities(query)];
        world.GetEntities(query, entities);

        using (var commandBuffer = new CommandBuffer())
        {
            commandBuffer.Destroy(entities[0]);
            commandBuffer.Playback(world);
        }

        That(world.Size, Is.EqualTo(1));

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferModify()
    {
        var world = World.Create();

        // Create an entity
        using (var commandBuffer = new CommandBuffer())
        {
            commandBuffer.Create([typeof(int)]);
            commandBuffer.Playback(world);
        }

        That(world.Size, Is.EqualTo(1));

        // Retrieve the entity we just created
        var query = new QueryDescription(all: [typeof(int)]);
        var entities = new Entity[world.CountEntities(query)];
        world.GetEntities(query, entities);

        // Check that it doesn't yet have anything
        Multiple(() =>
        {
            That(world.TryGet<Transform>(entities[0], out _), Is.False);
            That(world.TryGet<Rotation>(entities[0], out _), Is.False);
        });

        // Add to it
        using (var commandBuffer = new CommandBuffer())
        {
            commandBuffer.Add<Transform>(entities[0]);
            commandBuffer.Add<Rotation>(entities[0]);
            commandBuffer.Playback(world);
        }

        // Check modification added things
        Multiple(() =>
        {
            That(world.TryGet<Transform>(entities[0], out _), Is.True);
            That(world.TryGet<Rotation>(entities[0], out _), Is.True);
        });

        // Remove from it
        using (var commandBuffer = new CommandBuffer())
        {
            commandBuffer.Remove<Rotation>(entities[0]);
            commandBuffer.Playback(world);
        }

        // Check modification removed rotation
        Multiple(() =>
        {
            That(world.TryGet<Transform>(entities[0], out _), Is.True);
            That(world.TryGet<Rotation>(entities[0], out _), Is.False);
        });



        World.Destroy(world);
    }

    [Test]
    public void CommandBufferCombined()
    {
        var world = World.Create();
        var commandBuffer = new CommandBuffer();

        var entity = world.Create([typeof(Transform), typeof(Rotation), typeof(int)]);
        var bufferedEntity = commandBuffer.Create([typeof(Transform), typeof(Rotation), typeof(int)]);

        commandBuffer.Set(in entity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in entity, new Ai());
        commandBuffer.Remove<int>(in entity);

        commandBuffer.Set(in bufferedEntity, new Transform { X = 20, Y = 20 });
        commandBuffer.Add(in bufferedEntity, new Ai());
        commandBuffer.Remove<int>(in bufferedEntity);

        commandBuffer.Playback(world);

        bufferedEntity = new Entity(1, 0);

        That(world.Get<Transform>(entity).X, Is.EqualTo(20));
        That(world.Get<Transform>(entity).Y, Is.EqualTo(20));
        IsTrue(world.Has<Ai>(entity));
        IsFalse(world.Has<int>(entity));

        That(world.Get<Transform>(bufferedEntity).X, Is.EqualTo(20));
        That(world.Get<Transform>(bufferedEntity).Y, Is.EqualTo(20));
        IsTrue(world.Has<Ai>(bufferedEntity));
        IsFalse(world.Has<int>(bufferedEntity));

        World.Destroy(world);
    }
}

[TestFixture]
public partial class CommandBufferTest
{

    private JobScheduler _jobScheduler;

    [OneTimeSetUp]
    public void Setup()
    {
        _jobScheduler = new();
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _jobScheduler.Dispose();
    }
}
