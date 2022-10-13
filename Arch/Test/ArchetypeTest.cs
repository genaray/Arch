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
        
        Assert.AreEqual(archetype.Size, 1);
    }
    
    [Test]
    public void CreateMultipleChunk() {

        var archetype = new Archetype(group);
        var entities = Archetype.CalculateEntitiesPerChunk(group) * 2;
        
        for (var index = 0; index < entities; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(entity);
        }
        
        Assert.AreEqual(archetype.Size, 2);
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
        
        Assert.AreEqual(archetype.Size, 2);
        Assert.AreEqual(archetype.Chunks[0].Size, entities-50);
        Assert.AreEqual(archetype.Chunks[1].Size, 49);
        Assert.AreEqual(archetype.Chunks[0].Entities[0].EntityId, 493);    // Last entity from second chunk now replaced the removed entity and is in the first chunk
        Assert.AreEqual(archetype.EntityIdToChunkIndex[493], 0);  // Archetype knows that the moved entity is not in a different chunk 
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
        
        Assert.AreEqual(archetype.Size, 1);
        Assert.AreEqual(archetype.Chunks[0].Size, entities-1);
        Assert.AreEqual(archetype.Chunks[0].Entities[0].EntityId, 444);    // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }
}