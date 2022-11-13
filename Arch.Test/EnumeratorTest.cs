using Arch.Core;
using Arch.Core.Extensions;

namespace Arch.Test; 

public class EnumeratorTest {

    private World world;
    private Type[] group;
    private Type[] otherGroup;

    private QueryDescription description;
        
    [OneTimeSetUp]
    public void Setup() {
        
        group = new []{ typeof(Transform), typeof(Rotation) };
        otherGroup = new[] { typeof(Transform), typeof(Rotation), typeof(AI) };
        
        world = World.Create();
        world.Reserve(group, 10000);
        world.Reserve(otherGroup,10000);
        
        for (var index = 0; index < 10000; index++)
            world.Create(group);
        
        for (var index = 0; index < 10000; index++)
            world.Create(otherGroup);

        description = new QueryDescription { All = group };
    }

    [Test]
    public void WorldArchetypeEnumeration() {

        var counter = 0;
        foreach (ref var archetype in world) 
            counter++;
        
        Assert.AreEqual(2, counter);
    }
    
    [Test]
    public void ArchetypeChunkEnumeration() {

        var counter = 0;
        var archetype = world.Archetypes[0];
        foreach (ref var chunk in archetype) 
            counter++;

        Assert.AreEqual(10000/Archetype.CalculateEntitiesPerChunk(group), counter);
    }
    
    [Test]
    public void QueryArchetypeEnumeration() {

        var counter = 0;
        var query = world.Query(in description);
        foreach (ref var archetype in query.GetArchetypeIterator()) 
            counter++;
        
        Assert.AreEqual(2, counter);
    }
    
    [Test]
    public void QueryChunkEnumeration() {

        var counter = 0;
        var query = world.Query(in description);
        foreach (ref var chunk in query.GetChunkIterator()) 
            counter++;
        
        Assert.AreEqual(41, counter);
    }
}