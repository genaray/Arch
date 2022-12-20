using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Test;

internal struct HeavyComponent
{
    private decimal one;
    private decimal second;
    private decimal third;
    private decimal forth;
    private decimal fifth;
    private decimal sixt;
    private decimal seventh;
    private decimal eigth;
    private decimal ninth;
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

        Assert.AreEqual(1, archetype.Size);
    }
    
    [Test]
    public void ScaleChunkCapacity()
    {
        var archetype = new Archetype(_heavyGroup);
        Assert.AreEqual(Archetype.BaseSize * 2, archetype.ChunkSize);  // heavyGroup should be large enough to force the chunk to pick a 32KB chunk instead of 16KB
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

        Assert.AreEqual(2, archetype.Size);
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

        Assert.AreEqual(10, archetype.Size);
        Assert.AreEqual(10, archetype.Capacity);
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

        Assert.AreEqual(2, archetype.Size);
        Assert.AreEqual(2, archetype.Capacity);
        Assert.AreEqual(entities - 50, archetype.Chunks[0].Size);
        Assert.AreEqual(49, archetype.Chunks[1].Size);
        Assert.AreEqual(archetype.CalculateEntitiesPerChunk(_group) + 50 - 1,archetype.Chunks[0].Entities[0].Id); // Last entity from second chunk now replaced the removed entity and is in the first chunk
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

        Assert.AreEqual(1, archetype.Size);
        Assert.AreEqual(1, archetype.Capacity);
        Assert.AreEqual(entities - 1, archetype.Chunks[0].Size);
        Assert.AreEqual(archetype.CalculateEntitiesPerChunk(_group),archetype.Chunks[0].Entities[0].Id); // Last entity from second chunk now replaced the removed entity and is in the first chunk
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
        
        archetype.Set(ref entityOneSlot, new Transform{ X = 10, Y = 10});
        archetype.Set(ref entityOneSlot, new Rotation{ X = 10, Y = 10});

        otherArchetype.Add(entity, out var newSlot);
        archetype.CopyTo(otherArchetype, ref entityOneSlot, ref newSlot);
        archetype.Remove(ref entityOneSlot, out var replacedEntityId);
    
        Assert.AreEqual(0, archetype.Chunks[0].Size);
        Assert.AreEqual(2, otherArchetype.Chunks[0].Size);
        Assert.AreEqual(10, otherArchetype.Get<Transform>(ref newSlot).X);
        Assert.AreEqual(10, otherArchetype.Get<Transform>(ref newSlot).Y);
        Assert.AreEqual(10, otherArchetype.Get<Rotation>(ref newSlot).X);
        Assert.AreEqual(10, otherArchetype.Get<Rotation>(ref newSlot).Y);
    }
}