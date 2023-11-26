using System.Runtime.InteropServices;
using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="EnumeratorTest"/>
///     checks if the enumerators inside the common classes work correctly.
/// </summary>
[TestFixture]
public sealed class EnumeratorTest
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
        {
            _world.Create(_group);
        }

        for (var index = 0; index < 10000; index++)
        {
            _world.Create(_otherGroup);
        }
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> <see cref="World.Archetypes"/> are enumerated correctly.
    /// </summary>
    [Test]
    public void WorldArchetypeEnumeration()
    {
        var bitset = new BitSet();
        bitset.SetBit(1);
        bitset.SetBit(5);

        var counter = 0;
        foreach (ref var archetype in _world)
        {
            counter++;
        }

        That(counter, Is.EqualTo(2));
    }

    /// <summary>
    ///     Checks if the <see cref="Archetype"/> <see cref="Archetype.Chunks"/> are enumerated correctly.
    /// </summary>
    [Test]
    public void ArchetypeChunkEnumeration()
    {
        var counter = 0;
        var archetype = _world.Archetypes[0];
        foreach (ref var chunk in archetype)
        {
            counter++;
        }

        That(counter, Is.EqualTo((int)Math.Ceiling((float)10000 / archetype.CalculateEntitiesPerChunk(_group))));
    }

    /// <summary>
    ///     Checks if the <see cref="Query"/> archetypes are enumerated correctly.
    /// </summary>
    [Test]
    public void QueryArchetypeEnumeration()
    {
        var counter = 0;
        var query = _world.Query(in _description);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            counter++;
        }

        That(counter, Is.EqualTo(2));
    }

    /// <summary>
    ///     Checks if the <see cref="Query"/> archetypes are enumerated correctly when theres one empty archetype.
    ///     In the past it did not which caused weird query behaviour in certain situations where there was one empty archetype with one empty chunk.
    /// </summary>
    [Test]
    public void QueryArchetypeEmptyEnumeration()
    {
        // Create world, entity and move it.
        using var world = World.Create();
        var entity = world.Create();
        world.Add<int>(entity);

        var counter = 0;
        var query = world.Query(QueryDescription.Null);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            counter++;
        }

        That(counter, Is.EqualTo(1));
    }

    /// <summary>
    ///     Checks if the <see cref="Query"/> chunks are enumerated correctly.
    /// </summary>
    [Test]
    public void QueryChunkEnumeration()
    {
        var counter = 0;
        var query = _world.Query(in _description);
        foreach (ref var chunk in query)
        {
            counter++;
        }

        var archetype1ChunkCount = _world.Archetypes[0].ChunkCount;
        var archetype2ChunkCount = _world.Archetypes[1].ChunkCount;
        That(counter, Is.EqualTo(archetype1ChunkCount + archetype2ChunkCount));
    }
}
