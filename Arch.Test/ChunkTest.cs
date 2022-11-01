using System;
using Arch.Core;
using NUnit.Framework;

namespace Arch.Test; 

[TestFixture]
public class ChunkTest {
    
    private Chunk chunk;
    private Type[] Types = new[] { typeof(Transform), typeof(Rotation) };
        
    [Test]
    public void ArchetypeSet() {
            
        chunk = new Chunk(Archetype.CalculateEntitiesPerChunk(Types), Types);
        for (var index = 0; index < chunk.Capacity; index++) {

            var entity = new Entity(index, 0, 0);
            chunk.Add(in entity);
                
            var t = new Transform();
            var r = new Rotation();
            chunk.Set(index, t);
            chunk.Set(index, r);
        }
            
        // Make sure the amount fits
        Assert.AreEqual(chunk.Capacity, chunk.Size);
    }

    [Test]
    public void ArchetypeRemove() {
            
        chunk = new Chunk(Archetype.CalculateEntitiesPerChunk(Types), Types);
        for (var index = 0; index < chunk.Capacity; index++) {

            var entity = new Entity(index, 0, 0);
            chunk.Add(in entity);
                
            var t = new Transform();
            var r = new Rotation();
            chunk.Set(index, t);
            chunk.Set(index, r);
        }

        // Get last one, remove first one
        var last = chunk.Entities[chunk.Size-1];
        var first = new Entity(0, 0, 0);
        chunk.Remove(in first);
            
        // Check if the first one was replaced with the last one correctly 
        Assert.AreEqual(chunk.Entities[0].EntityId, last.EntityId);
    }
        
    [Test]
    public void ArchetypeRemoveAll() {
            
        chunk = new Chunk(Archetype.CalculateEntitiesPerChunk(Types), Types);
        for (var index = 0; index < 5; index++) {

            var entity = new Entity(index, 0, 0);
            chunk.Add(in entity);
                
            var t = new Transform();
            var r = new Rotation();
            chunk.Set(index, t);
            chunk.Set(index, r);
        }

        // Backward delete all since forward does not work while keeping the array dense
        for (var index = chunk.Size-1; index >= 0; index--) {

            ref var toRemove = ref chunk.Entities[index];
            chunk.Remove(in toRemove);
        }

        // Check if the first one was replaced with the last one correctly 
        Assert.AreEqual(chunk.Size, 0);
        Assert.AreEqual(chunk.Entities[0].EntityId, 0); // Needs to be 1, because it will be the last one getting removed and being moved to that position
    }
        
    [Test]
    public void ArchetypeRemoveAndSetAgain() {
            
        chunk = new Chunk(Archetype.CalculateEntitiesPerChunk(Types), Types);
        
        var newEntity = new Entity(1,0,0);
        var newEntityTwo = new Entity(2,0,0);
        
        chunk.Add(in newEntity);
        chunk.Add(in newEntityTwo);
        
        chunk.Remove(in newEntity);
        chunk.Add(in newEntity);

        // Check if the first one was replaced with the last one correctly 
        Assert.AreEqual(chunk.Size, 2);
        Assert.AreEqual(chunk.Entities[0].EntityId, 2); // Needs to be 1, because it will be the last one getting removed and being moved to that position
        Assert.AreEqual(chunk.Entities[1].EntityId, 1); // Needs to be 1, because it will be the last one getting removed and being moved to that position
    }
}