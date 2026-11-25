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
    public void CommandBufferAddAndRemoveForManyEntities()
    {
        const int amount = 100_000;
        var world = World.Create();
        var entities = new Entity[amount];
        for (var index = 0; index < amount; index++)
        {
            entities[index] = world.Create<Transform>();
        }

        using (var commandBuffer = new CommandBuffer())
        {
            for (var index = 0; index < amount; index++)
            {
                commandBuffer.Add(in entities[index], new Rotation { X = index });
            }

            commandBuffer.Playback(world);
        }

        using (var commandBuffer = new CommandBuffer())
        {
            for (var index = 0; index < amount; index += 2)
            {
                commandBuffer.Remove<Rotation>(in entities[index]);
            }

            commandBuffer.Playback(world);
        }

        for (var index = 0; index < amount; index++)
        {
            var hasRotation = index % 2 == 1;
            That(world.Has<Rotation>(entities[index]), Is.EqualTo(hasRotation));
            if (hasRotation)
            {
                That(world.Get<Rotation>(entities[index]).X, Is.EqualTo(index));
            }
        }

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferRecordsFromMultipleThreads()
    {
        const int amount = 20_000;
        var world = World.Create();
        var entities = new Entity[amount];
        for (var index = 0; index < amount; index++)
        {
            entities[index] = world.Create<Transform>();
        }

        using var commandBuffer = new CommandBuffer();
        System.Threading.Tasks.Parallel.For(0, amount, index =>
        {
            commandBuffer.Add(in entities[index], new Rotation { X = index });
            commandBuffer.Set(in entities[index], new Transform { X = -index });
            if (index % 2 == 0)
            {
                commandBuffer.Add(in entities[index], new Ai());
            }
        });

        commandBuffer.Playback(world);

        for (var index = 0; index < amount; index++)
        {
            That(world.Get<Rotation>(entities[index]).X, Is.EqualTo(index));
            That(world.Get<Transform>(entities[index]).X, Is.EqualTo(-index));
            That(world.Has<Ai>(entities[index]), Is.EqualTo(index % 2 == 0));
        }

        World.Destroy(world);
    }

    [Test]
    public void CommandBufferRecordsOnlyForOneThread()
    {
        var world = World.Create();
        var entity = world.Create<Transform>();
        var other = world.Create<Transform>();

        using var commandBuffer = new CommandBuffer(threadSafe: false);
        commandBuffer.Set(entity, new Transform { X = 1 });
        commandBuffer.Add(entity, new Rotation { X = 2 });
        commandBuffer.Remove<Transform>(other);
        commandBuffer.Playback(world);

        That(world.Get<Transform>(entity).X, Is.EqualTo(1));
        That(world.Get<Rotation>(entity).X, Is.EqualTo(2));
        That(world.Has<Transform>(other), Is.False);

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that entities with different kinds of operations are played back correctly, since they are registered in the sets only when one is recorded.
    /// </summary>
    [Test]
    public void CommandBufferMixesOperationsOfDifferentEntities()
    {
        var world = World.Create();
        var setOnly = world.Create<Transform>();
        var addOnly = world.Create<Transform>();
        var removeOnly = world.Create(new Transform(), new Rotation());
        var all = world.Create(new Transform(), new Rotation());

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Remove<Rotation>(removeOnly);
        commandBuffer.Set(setOnly, new Transform { X = 3 });
        commandBuffer.Add(addOnly, new Ai());
        commandBuffer.Add(all, new Ai());
        commandBuffer.Set(all, new Transform { X = 4 });
        commandBuffer.Remove<Rotation>(all);
        commandBuffer.Playback(world);

        That(world.Get<Transform>(setOnly).X, Is.EqualTo(3));
        That(world.Has<Ai>(addOnly), Is.True);
        That(world.Has<Rotation>(removeOnly), Is.False);
        That(world.Has<Ai>(all) && !world.Has<Rotation>(all), Is.True);
        That(world.Get<Transform>(all).X, Is.EqualTo(4));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that a component which is added and removed again leaves the entity as it was, without throwing.
    /// </summary>
    [Test]
    public void CommandBufferCancelsAddedAndRemovedComponents()
    {
        var world = World.Create();
        var entity = world.Create<Transform>();

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Add(entity, new Ai());
        commandBuffer.Remove<Ai>(entity);
        commandBuffer.Add(entity, new Rotation { X = 7 });
        commandBuffer.Playback(world);

        That(world.Has<Ai>(entity), Is.False);
        That(world.Get<Rotation>(entity).X, Is.EqualTo(7));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that a removed component wins over an add recorded later, since removes are played back after adds.
    /// </summary>
    [Test]
    public void CommandBufferRemovesWinOverSets()
    {
        var world = World.Create();
        var entity = world.Create(new Transform(), new Rotation());

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Remove<Rotation>(entity);
        commandBuffer.Set(entity, new Rotation { X = 3 });
        commandBuffer.Set(entity, new Transform { X = 1 });
        commandBuffer.Set(entity, new Transform { X = 2 });
        commandBuffer.Playback(world);

        That(world.Has<Rotation>(entity), Is.False);
        That(world.Get<Transform>(entity).X, Is.EqualTo(2));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that an entity which is destroyed ignores its other commands, and that the other commands still apply to the remaining entities.
    /// </summary>
    [Test]
    public void CommandBufferDestroysEntitiesWithOtherCommands()
    {
        var world = World.Create();
        var destroyed = world.Create<Transform>();
        var remaining = world.Create<Transform>();

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Add(destroyed, new Ai());
        commandBuffer.Set(destroyed, new Transform { X = 1 });
        commandBuffer.Destroy(destroyed);
        commandBuffer.Add(remaining, new Ai());
        commandBuffer.Playback(world);

        That(world.IsAlive(destroyed), Is.False);
        That(world.Has<Ai>(remaining), Is.True);
        That(world.Size, Is.EqualTo(1));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that every thread can create entities and use them, and that all of them are created exactly once.
    /// </summary>
    [Test]
    public void CommandBufferCreatesEntitiesFromMultipleThreads()
    {
        const int amount = 5_000;
        var world = World.Create();
        var created = new Entity[amount];

        using var commandBuffer = new CommandBuffer();
        System.Threading.Tasks.Parallel.For(0, amount, index =>
        {
            created[index] = commandBuffer.Create([typeof(Transform)]);
            commandBuffer.Set(created[index], new Transform { X = index });
            commandBuffer.Add(created[index], new Rotation { X = -index });
        });

        commandBuffer.Playback(world);

        That(world.Size, Is.EqualTo(amount));
        That(world.CountEntities(new QueryDescription(all: _group)), Is.EqualTo(amount));
        var query = new QueryDescription(all: _group);
        world.Query(in query, (ref Transform transform, ref Rotation rotation) => That(rotation.X, Is.EqualTo(-transform.X)));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that commands recorded by another thread can target an entity which was created on this one.
    /// </summary>
    [Test]
    public void CommandBufferTargetsEntitiesCreatedByOtherThreads()
    {
        var world = World.Create();
        using var commandBuffer = new CommandBuffer();

        var created = commandBuffer.Create([typeof(Transform)]);
        System.Threading.Tasks.Task.Run(() =>
        {
            commandBuffer.Set(created, new Transform { X = 5 });
            commandBuffer.Add(created, new Ai());
        }).Wait();

        commandBuffer.Playback(world);

        var query = new QueryDescription(all: [typeof(Transform), typeof(Ai)]);
        That(world.CountEntities(query), Is.EqualTo(1));
        world.Query(in query, (ref Transform transform) => That(transform.X, Is.EqualTo(5)));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that a thread which records into several command buffers alternately keeps their commands apart.
    /// </summary>
    [Test]
    public void CommandBuffersRecordedAlternatelyStayApart()
    {
        var world = World.Create();
        var first = world.Create<Transform>();
        var second = world.Create<Transform>();

        using var firstBuffer = new CommandBuffer();
        using var secondBuffer = new CommandBuffer();
        firstBuffer.Set(first, new Transform { X = 1 });
        secondBuffer.Set(second, new Transform { X = 2 });
        firstBuffer.Add(first, new Ai());
        secondBuffer.Remove<Transform>(second);

        That(firstBuffer.Size, Is.EqualTo(2));
        That(secondBuffer.Size, Is.EqualTo(2));

        firstBuffer.Playback(world);
        That(world.Get<Transform>(first).X, Is.EqualTo(1));
        That(world.Has<Ai>(first), Is.True);
        That(world.Has<Transform>(second), Is.True);

        secondBuffer.Playback(world);
        That(world.Has<Transform>(second), Is.False);

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that the size counts the recorded commands and that a playback that disposes them resets it.
    /// </summary>
    [Test]
    public void CommandBufferSizeCountsRecordedCommands()
    {
        var world = World.Create();
        var entity = world.Create<Transform>();

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Set(entity, new Transform { X = 1 });
        commandBuffer.Set(entity, new Transform { X = 2 });
        commandBuffer.Create([typeof(Transform)]);
        That(commandBuffer.Size, Is.EqualTo(3));

        commandBuffer.Playback(world, dispose: false);
        That(commandBuffer.Size, Is.EqualTo(3));

        commandBuffer.Playback(world);
        That(commandBuffer.Size, Is.EqualTo(0));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that a stream which only sets values is played back in the order the values were recorded.
    /// </summary>
    [Test]
    public void CommandBufferSetsValuesInRecordedOrder()
    {
        var world = World.Create();
        var entity = world.Create<Transform>();

        using var commandBuffer = new CommandBuffer(threadSafe: false);
        commandBuffer.Set(entity, new Transform { X = 1 });
        commandBuffer.Set(entity, new Transform { X = 2 });
        commandBuffer.Playback(world);

        That(world.Get<Transform>(entity).X, Is.EqualTo(2));

        World.Destroy(world);
    }

    /// <summary>
    ///     Checks that a <see cref="CommandBuffer"/> does not keep managed components alive after they were played back.
    /// </summary>
    [Test]
    public void CommandBufferDoesNotKeepPlayedBackManagedComponentsAlive()
    {
        var world = World.Create();
        var entity = world.Create(new Label { Value = "initial" });

        using var commandBuffer = new CommandBuffer();
        var reference = RecordAndPlaybackLabel(world, commandBuffer, entity);

        // The world replaces the label, so only the command buffer could still reference the recorded one.
        world.Set(entity, new Label { Value = "replaced" });

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        That(reference.IsAlive, Is.False);
        World.Destroy(world);
    }

    // Separate method, so no local of the test keeps the object alive.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference RecordAndPlaybackLabel(World world, CommandBuffer commandBuffer, Entity entity)
    {
        var value = new string('x', 100);
        commandBuffer.Set(entity, new Label { Value = value });
        commandBuffer.Playback(world);

        return new WeakReference(value);
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
        _jobScheduler = new JobScheduler(
            new JobScheduler.Config{
            ThreadPrefixName = "CommandBuffer",
            ThreadCount = 0,
            MaxExpectedConcurrentJobs = 64,
            StrictAllocationMode = false,
        });
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        _jobScheduler.Dispose();
    }
}
