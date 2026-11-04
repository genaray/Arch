using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public sealed class QueryMatchTest
{
    private static readonly ComponentType[] _types =
    [
        typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int),
        typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double)
    ];

    private static readonly QueryDescription[] _descriptions =
    [
        new(all: [typeof(int)]),
        new(all: [typeof(int), typeof(long)], none: [typeof(double)]),
        new(any: [typeof(byte), typeof(float)]),
        new(any: [typeof(short), typeof(ulong)], none: [typeof(uint)]),
        new(none: [typeof(sbyte)]),
        new(exclusive: [typeof(int), typeof(long)]),
        new(exclusive: [typeof(ushort)]),
    ];

    private World _world;
    private Query[] _queries;
    private Dictionary<Archetype, Entity> _entities;
    private int _nextSubset;

    [SetUp]
    public void Setup()
    {
        _world = World.Create();
        _entities = new Dictionary<Archetype, Entity>();
        _nextSubset = 1;
        _queries = CreateQueries();
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    private Query[] CreateQueries()
    {
        return _descriptions.Select(description => _world.Query(description)).ToArray();
    }

    private static ComponentType[] Subset(int bits)
    {
        return _types.Where((_, index) => (bits & (1 << index)) != 0).ToArray();
    }

    private void CreateArchetypes(int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            var entity = _world.Create(Subset(_nextSubset++));
            _entities[_world.GetArchetype(entity)] = entity;
        }
    }

    private void AssertMatches()
    {
        foreach (var query in _queries)
        {
            var matched = new List<Archetype>();
            foreach (var archetype in query.GetArchetypeIterator())
            {
                matched.Add(archetype);
            }

            var expected = _world.Archetypes.AsSpan().ToArray().Where(archetype => archetype.EntityCount > 0 && query.Matches(archetype.BitSet));
            That(matched, Is.EquivalentTo(expected));
        }
    }

    [Test]
    public void MatchAfterAppends()
    {
        AssertMatches();
        for (var step = 0; step < 10; step++)
        {
            CreateArchetypes(step * 7 + 1);
            AssertMatches();
            AssertMatches();
        }
    }

    [Test]
    public void MatchAfterDestroyArchetype()
    {
        var random = new Random(1);
        for (var step = 0; step < 10; step++)
        {
            CreateArchetypes(40);
            AssertMatches();

            for (var index = 0; index < 3; index++)
            {
                var archetype = _world.Archetypes[random.Next(_world.Archetypes.Count)];
                _entities.Remove(archetype);
                _world.DestroyArchetype(archetype);
            }

            AssertMatches();
        }
    }

    [Test]
    public void MatchAfterTrimExcess()
    {
        var random = new Random(2);
        for (var step = 0; step < 10; step++)
        {
            CreateArchetypes(40);
            AssertMatches();

            for (var index = 0; index < 5; index++)
            {
                var archetype = _world.Archetypes[random.Next(_world.Archetypes.Count)];
                if (_entities.Remove(archetype, out var entity))
                {
                    _world.Destroy(entity);
                }
            }

            AssertMatches();
            _world.TrimExcess();
            AssertMatches();
        }
    }

    [Test]
    public void MatchAfterIndexSetter()
    {
        var random = new Random(3);
        for (var step = 0; step < 10; step++)
        {
            CreateArchetypes(40);
            AssertMatches();

            var first = random.Next(_world.Archetypes.Count);
            var second = random.Next(_world.Archetypes.Count);
            var archetype = _world.Archetypes[first];
            _world.Archetypes[first] = _world.Archetypes[second];
            _world.Archetypes[second] = archetype;
            AssertMatches();

            var replaced = _world.Archetypes[first];
            _world.Archetypes[first] = _world.Archetypes[_world.Archetypes.Count - 1];
            AssertMatches();
            _world.Archetypes[first] = replaced;
        }
    }

    [Test]
    public void MatchAfterArchetypesClear()
    {
        CreateArchetypes(200);
        AssertMatches();

        _world.Archetypes.Clear();
        AssertMatches();
    }

    [Test]
    public void MatchAfterWorldClear()
    {
        for (var step = 0; step < 3; step++)
        {
            CreateArchetypes(100);
            AssertMatches();

            _world.Clear();
            _entities.Clear();
            _nextSubset = 1 + step * 50;
            AssertMatches();

            CreateArchetypes(100);
            AssertMatches();
            _queries = CreateQueries();
            AssertMatches();
        }
    }

    [Test]
    public void MatchAfterItemsRemove()
    {
        CreateArchetypes(100);
        AssertMatches();

        _world.Archetypes.Items.RemoveAt(10);
        AssertMatches();

        CreateArchetypes(5);
        _world.Archetypes.Items.Remove(_world.Archetypes[0]);
        AssertMatches();

        _world.Archetypes.Items.Clear();
        AssertMatches();

        CreateArchetypes(5);
        AssertMatches();
    }

    [Test]
    public void ArchetypesHashCodeFollowsChanges()
    {
        CreateArchetypes(10);
        var hashCode = _world.Archetypes.GetHashCode();
        That(_world.Archetypes.GetHashCode(), Is.EqualTo(hashCode));

        CreateArchetypes(1);
        That(_world.Archetypes.GetHashCode(), Is.Not.EqualTo(hashCode));

        var archetype = _world.Archetypes[0];
        _world.Archetypes[0] = _world.Archetypes[1];
        _world.Archetypes[1] = archetype;
        hashCode = _world.Archetypes.GetHashCode();
        _world.Archetypes[1] = _world.Archetypes[0];
        _world.Archetypes[0] = archetype;
        That(_world.Archetypes.GetHashCode(), Is.Not.EqualTo(hashCode));
    }
}
