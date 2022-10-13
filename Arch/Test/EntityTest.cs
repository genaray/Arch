using System;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using NUnit.Framework;

namespace Arch.Test; 

[TestFixture]
public class EntityTest {
    
    private World world;
    private Type[] group;
    private Entity entity;
        
    [OneTimeSetUp]
    public void Setup() {

        world = World.Create();
        group = new []{typeof(Transform), typeof(Rotation)};
        entity = world.Create(group);
    }

    [Test]
    public void GetArchetype() {

        var bitset = new BitSet();
        bitset.SetBits(group);
        
        var archetype = entity.GetArchetype();

        Assert.True(bitset.All(archetype.BitSet));
    }
    
    [Test]
    public void IsAlive() {
        Assert.True(entity.IsAlive());
    }
    
    [Test]
    public void Has() {
        Assert.True(entity.Has<Transform>());
    }
    
    [Test]
    public void HasNot() {
        Assert.False(entity.Has<int>());
    }
    
    [Test]
    public void SetAndGet() {

        var transform = new Transform { x = 10, y = 10 };
        entity.Set(transform);
        ref var tramsformReference = ref entity.Get<Transform>();
        
        Assert.AreEqual(transform.x, tramsformReference.x);
        Assert.AreEqual(transform.y, tramsformReference.y);
    }
}