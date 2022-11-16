using Arch.Core;

namespace Arch.Test;

public class EnumeratorTest
{
    private QueryDescription _description;
    private Type[] _group;
    private Type[] _otherGroup;

    private World _world;

    [OneTimeSetUp]
    public void Setup()
    {
        _group = new[] { typeof(Transform), typeof(Rotation) };
        _otherGroup = new[] { typeof(Transform), typeof(Rotation), typeof(Ai) };

        _world = World.Create();
        _world.Reserve(_group, 10000);
        _world.Reserve(_otherGroup, 10000);

        for (var index = 0; index < 10000; index++)
            _world.Create(_group);

        for (var index = 0; index < 10000; index++)
            _world.Create(_otherGroup);

        _description = new QueryDescription { All = _group };
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

        Assert.AreEqual(10000 / Archetype.CalculateEntitiesPerChunk(_group), counter);
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

        Assert.AreEqual(41, counter);
    }
}