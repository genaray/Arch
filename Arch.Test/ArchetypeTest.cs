using Arch.Core;

namespace Arch.Test;

[TestFixture]
public class ArchetypeTest
{
    private readonly Type[] _group = { typeof(Transform), typeof(Rotation) };
    private readonly Type[] _otherGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };

    [Test]
    public void CreateChunk()
    {
        var archetype = new Archetype(_group);
        var entities = Archetype.CalculateEntitiesPerChunk(_group);

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }

        Assert.AreEqual(1, archetype.Size);
    }

    [Test]
    public void CreateMultipleChunk()
    {
        var archetype = new Archetype(_group);
        var entities = Archetype.CalculateEntitiesPerChunk(_group) * 2;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }

        Assert.AreEqual(2, archetype.Size);
    }

    [Test]
    public void AllocateFor()
    {
        var archetype = new Archetype(_group);
        var entities = Archetype.CalculateEntitiesPerChunk(_group) * 10;
        archetype.Reserve(entities);

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }

        Assert.AreEqual(10, archetype.Size);
        Assert.AreEqual(10, archetype.Capacity);
    }

    [Test]
    public void RemoveFromChunkWithReplacement()
    {
        var archetype = new Archetype(_group);
        var entities = Archetype.CalculateEntitiesPerChunk(_group) + 50;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }

        archetype.Remove(new Entity(0, 0, 0));

        Assert.AreEqual(2, archetype.Size);
        Assert.AreEqual(2, archetype.Capacity);
        Assert.AreEqual(entities - 50, archetype.Chunks[0].Size);
        Assert.AreEqual(49, archetype.Chunks[1].Size);
        Assert.AreEqual(Archetype.CalculateEntitiesPerChunk(_group) + 50 - 1,
            archetype.Chunks[0].Entities[0].EntityId); // Last entity from second chunk now replaced the removed entity and is in the first chunk
        Assert.AreEqual(0, archetype.EntityIdToChunkIndex[493]); // Archetype knows that the moved entity is not in a different chunk 
    }

    [Test]
    public void RemoveChunk()
    {
        var archetype = new Archetype(_group);
        var entities = Archetype.CalculateEntitiesPerChunk(_group) + 1;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }

        archetype.Remove(new Entity(0, 0, 0));

        Assert.AreEqual(1, archetype.Size);
        Assert.AreEqual(1, archetype.Capacity);
        Assert.AreEqual(entities - 1, archetype.Chunks[0].Size);
        Assert.AreEqual(Archetype.CalculateEntitiesPerChunk(_group),
            archetype.Chunks[0].Entities[0].EntityId); // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }

    [Test]
    public void Move()
    {
        var archetype = new Archetype(_group);
        var otherArchetype = new Archetype(_otherGroup);

        var entity = new Entity(1, 0, 0);
        archetype.Add(entity);
        otherArchetype.Add(new Entity(2, 0, 0));
        
        archetype.Set(in entity, new Transform{ X = 10, Y = 10});
        archetype.Set(in entity, new Rotation{ X = 10, Y = 10});
        
        archetype.Move(in entity, otherArchetype, out var created, out var destroyed);
        
        Assert.AreEqual(0, archetype.Chunks[0].Size);
        Assert.AreEqual(2, otherArchetype.Chunks[0].Size);
        Assert.AreEqual(10, otherArchetype.Get<Transform>(in entity).X);
        Assert.AreEqual(10, otherArchetype.Get<Transform>(in entity).Y);
        Assert.AreEqual(10, otherArchetype.Get<Rotation>(in entity).X);
        Assert.AreEqual(10, otherArchetype.Get<Rotation>(in entity).Y);
    }
}