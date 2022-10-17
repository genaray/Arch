using System;
using Arch.Core;
using NUnit.Framework;

namespace Arch.Test; 

[TestFixture]
public class ArchetypeTest {

    private Type[] group = { typeof(Transform), typeof(Rotation) };
    
    [Test]
    public void CreateChunk() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group);
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        Assert.AreEqual(1, archetype.Size);
    }
    
    [Test]
    public void CreateMultipleChunk() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group) * 2;
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        Assert.AreEqual(2, archetype.Size);
    }
    
    [Test]
    public void AllocateFor() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group) * 10;
        archetype.AllocateFor(entities);
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        Assert.AreEqual(10, archetype.Size);
        Assert.AreEqual(10, archetype.Capacity);
    }
    
    [Test]
    public void RemoveFromChunkWithReplacement() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group)+50;
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        archetype.Remove(new Entity(0,0,0));
        
        Assert.AreEqual(2, archetype.Size);
        Assert.AreEqual(2, archetype.Capacity);
        Assert.AreEqual(entities-50, archetype.Chunks[0].Size);
        Assert.AreEqual(49, archetype.Chunks[1].Size);
        Assert.AreEqual(493, archetype.Chunks[0].Entities[0].EntityId);    // Last entity from second chunk now replaced the removed entity and is in the first chunk
        Assert.AreEqual(0, archetype.EntityIdToChunkIndex[493]);  // Archetype knows that the moved entity is not in a different chunk 
    }
    
    [Test]
    public void RemoveChunk() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group)+1;
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        archetype.Remove(new Entity(0,0,0));
        
        Assert.AreEqual(1, archetype.Size);
        Assert.AreEqual(1, archetype.Capacity);
        Assert.AreEqual(entities-1, archetype.Chunks[0].Size);
        Assert.AreEqual(444, archetype.Chunks[0].Entities[0].EntityId);    // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }
}