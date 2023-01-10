using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

internal readonly struct HeavyComponent
{
    private readonly decimal one;
    private readonly decimal second;
    private readonly decimal third;
    private readonly decimal forth;
    private readonly decimal fifth;
    private readonly decimal sixt;
    private readonly decimal seventh;
    private readonly decimal eigth;
    private readonly decimal ninth;
}

[TestFixture]
public class ArchetypeTest
{
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };
    private readonly ComponentType[] _otherGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };
    private readonly ComponentType[] _heavyGroup = { typeof(Transform), typeof(Rotation), typeof(HeavyComponent) };

    [Test]
    public void CreateChunk()
    {
        var archetype = new Archetype(_group);
        var entities = archetype.CalculateEntitiesPerChunk(_group);

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _);
        }

        That(archetype.Size, Is.EqualTo(1));
    }

    [Test]
    public void ScaleChunkCapacity()
    {
        var archetype = new Archetype(_heavyGroup);
        That(archetype.ChunkSize, Is.EqualTo(Archetype.BaseSize * 2)); // heavyGroup should be large enough to force the chunk to pick a 32KB chunk instead of 16KB
    }

    [Test]
    public void CreateMultipleChunk()
    {
        var archetype = new Archetype(_group);
        var entities = archetype.CalculateEntitiesPerChunk(_group) * 2;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _);
        }

        That(archetype.Size, Is.EqualTo(2));
    }

    [Test]
    public void AllocateFor()
    {
        var archetype = new Archetype(_group);
        var entities = archetype.CalculateEntitiesPerChunk(_group) * 10;
        archetype.Reserve(entities);

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _);
        }

        That(archetype.Size, Is.EqualTo(10));
        That(archetype.Capacity, Is.EqualTo(10));
    }

    [Test]
    public void RemoveFromChunkWithReplacement()
    {
        var archetype = new Archetype(_group);
        var entities = archetype.CalculateEntitiesPerChunk(_group) + 50;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _);
        }

        var slot = new Slot(0, 0);
        archetype.Remove(ref slot, out _);

        That(archetype.Size, Is.EqualTo(2));
        That(archetype.Capacity, Is.EqualTo(2));
        That(archetype.Chunks[0].Size, Is.EqualTo(entities - 50));
        That(archetype.Chunks[1].Size, Is.EqualTo(49));
        That(archetype.Chunks[0].Entities[0].Id, Is.EqualTo(archetype.CalculateEntitiesPerChunk(_group) + 50 - 1)); // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }

    [Test]
    public void RemoveChunk()
    {
        var archetype = new Archetype(_group);
        var entities = archetype.CalculateEntitiesPerChunk(_group) + 1;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _);
        }

        var slot = new Slot(0, 0);
        archetype.Remove(ref slot, out _);

        That(archetype.Size, Is.EqualTo(1));
        That(archetype.Capacity, Is.EqualTo(1));
        That(archetype.Chunks[0].Size, Is.EqualTo(entities - 1));
        That(archetype.Chunks[0].Entities[0].Id, Is.EqualTo(archetype.CalculateEntitiesPerChunk(_group))); // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }

    [Test]
    public void Move()
    {
        var archetype = new Archetype(_group);
        var otherArchetype = new Archetype(_otherGroup);

        var entity = new Entity(1, 0);
        var otherEntity = new Entity(2, 0);
        archetype.Add(entity, out var entityOneSlot);
        otherArchetype.Add(otherEntity, out var entityTwoSlot);

        archetype.Set(ref entityOneSlot, new Transform { X = 10, Y = 10 });
        archetype.Set(ref entityOneSlot, new Rotation { X = 10, Y = 10 });

        otherArchetype.Add(entity, out var newSlot);
        archetype.CopyTo(otherArchetype, ref entityOneSlot, ref newSlot);
        archetype.Remove(ref entityOneSlot, out var replacedEntityId);

        That(archetype.Chunks[0].Size, Is.EqualTo(0));
        That(otherArchetype.Chunks[0].Size, Is.EqualTo(2));
        That(otherArchetype.Get<Transform>(ref newSlot).X, Is.EqualTo(10));
        That(otherArchetype.Get<Transform>(ref newSlot).Y, Is.EqualTo(10));
        That(otherArchetype.Get<Rotation>(ref newSlot).X, Is.EqualTo(10));
        That(otherArchetype.Get<Rotation>(ref newSlot).Y, Is.EqualTo(10));
    }
}
