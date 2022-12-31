using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Test;

public class EnumeratorTest
{

    private static readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };
    private static readonly ComponentType[] _otherGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };
    private readonly QueryDescription _description = new() { All = _group };

    private World _world;

    [OneTimeSetUp]
    public void Setup()
    {
        _world = World.Create();
        _world.Reserve(_group, 10000);
        _world.Reserve(_otherGroup, 10000);

        for (var index = 0; index < 10000; index++)
            _world.Create(_group);

        for (var index = 0; index < 10000; index++)
            _world.Create(_otherGroup);
    }

    [Test]
    public void WorldArchetypeEnumeration()
    {
        var counter = 0;
        foreach (ref var archetype in _world)
            counter++;

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void ArchetypeChunkEnumeration()
    {
        var counter = 0;
        var archetype = _world.Archetypes[0];
        foreach (ref var chunk in archetype)
            counter++;

        Assert.AreEqual((int)Math.Ceiling((float)10000/archetype.CalculateEntitiesPerChunk(_group)), counter);
    }

    [Test]
    public void QueryArchetypeEnumeration()
    {
        var counter = 0;
        var query = _world.Query(in _description);
        foreach (ref var archetype in query.GetArchetypeIterator())
            counter++;

        Assert.AreEqual(2, counter);
    }

    [Test]
    public void QueryChunkEnumeration()
    {
        var counter = 0;
        var query = _world.Query(in _description);
        foreach (ref var chunk in query.GetChunkIterator())
            counter++;

        var archetype1ChunkCount = _world.Archetypes[0].Size;
        var archetype2ChunkCount = _world.Archetypes[1].Size;
        Assert.AreEqual(archetype1ChunkCount+archetype2ChunkCount, counter);
    }
    
    [Test]
    public void QueryEntityEnumeration()
    {
        var counter = 0;
        var query = _world.Query(in _description);
        foreach (var entity in query.GetEntityIterator())
            counter++;
        
        Assert.AreEqual(20000, counter);
    }
}