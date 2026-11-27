using Arch.Buffer;
using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="ReusedSlotTest"/> class
///     checks that an <see cref="Entity"/> placed in a slot another <see cref="Entity"/> left starts with default components, instead of the old ones.
/// </summary>
[TestFixture]
public sealed class ReusedSlotTest
{
    private static readonly ComponentType[] Group = [typeof(Transform), typeof(Health)];

    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = World.Create();
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    /// <summary>
    ///     Creates two entities and removes the first one with <paramref name="leave"/>, so the second one moves into its slot and leaves the last slot behind.
    /// </summary>
    private void LeaveSlotBehind(Action<Entity> leave)
    {
        var first = _world.Create(new Transform(), new Health());
        _world.Create(new Transform { X = 3 }, new Health { Value = 5 });
        leave(first);
    }

    [Test]
    public void CreateAfterDestroy()
    {
        LeaveSlotBehind(entity => _world.Destroy(entity));

        var entity = _world.Create(Group);
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
        That(_world.Get<Transform>(entity).X, Is.EqualTo(0));
    }

    [Test]
    public void CreateAfterMovingToAnotherArchetype()
    {
        LeaveSlotBehind(entity => _world.Add(entity, new Rotation()));

        var entity = _world.Create(Group);
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
    }

    [Test]
    public void BulkCreateAfterDestroy()
    {
        LeaveSlotBehind(entity => _world.Destroy(entity));

        var entities = new Entity[2];
        _world.Create(entities.AsSpan(), Group, entities.Length);
        That(_world.Get<Health>(entities[0]).Value, Is.EqualTo(0));
        That(_world.Get<Health>(entities[1]).Value, Is.EqualTo(0));
    }

    [Test]
    public void AddWithoutValueAfterRemove()
    {
        LeaveSlotBehind(entity => _world.Remove<Health>(entity));

        var entity = _world.Create(new Transform());
        _world.Add<Health>(entity);
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
    }

    [Test]
    public void AddRangeAfterRemove()
    {
        LeaveSlotBehind(entity => _world.Remove<Health>(entity));

        var entity = _world.Create(new Transform());
        _world.AddRange(entity, new ComponentType[] { typeof(Health) });
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
    }

    [Test]
    public void CreateAfterDestroyingAQuery()
    {
        _world.Create(new Transform { X = 3 }, new Health { Value = 5 });
        _world.Destroy(new QueryDescription().WithAll<Transform, Health>());

        var entity = _world.Create(Group);
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
    }

    [Test]
    public void CreateAfterAddingToAQuery()
    {
        _world.Create(new Transform { X = 3 }, new Health { Value = 5 });
        _world.Add(new QueryDescription().WithAll<Transform, Health>(), new Rotation());

        var entity = _world.Create(Group);
        That(_world.Get<Health>(entity).Value, Is.EqualTo(0));
    }

    [Test]
    public void CommandBufferCreateAfterDestroy()
    {
        LeaveSlotBehind(entity => _world.Destroy(entity));

        using var commandBuffer = new CommandBuffer();
        commandBuffer.Create(Group);
        commandBuffer.Playback(_world);

        var values = new List<int>();
        _world.Query(new QueryDescription().WithAll<Health>(), (ref Health health) => values.Add(health.Value));
        That(values, Is.EquivalentTo(new[] { 5, 0 }));
    }
}
