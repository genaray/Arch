using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Arch.Core;

namespace Arch.Tests;

[TestFixture]
public class ChangedQueryTest
{
    private struct A;
    private struct B;
    private struct C;

    private World _world;
    private Entity _e1, _e2, _e3, _e4, _e5, _e6;
    private Entity[] _nothing = [];

    [SetUp]
    public void Setup()
    {
        _world = World.Create();

        _e1 = _world.Create<A, B>();

        _e2 = _world.Create<A, B>();
        _world.MarkChanged<B>(_e2);

        _e3 = _world.Create<A, B, C>();
        _world.MarkChanged<B>(_e3);

        _e4 = _world.Create<B>();
        _world.MarkChanged<B>(_e4);

        // E5 = (C*)
        _e5 = _world.Create<C>();
        _world.MarkChanged<C>(_e5);

        // E6 = ()
        _e6 = _world.Create();
    }

    [Test]
    public void WithChangedOnly()
    {
        var query = new QueryDescription().WithChanged<B>();
        AssertMatches(query, _e2, _e3, _e4);
    }

    [Test]
    public void WithAnyAndChanged()
    {
        var query = new QueryDescription().WithAny<C>().WithChanged<C>();
        AssertMatches(query, _e3, _e5);
    }

    [Test]
    public void WithDisjointAnyAndChanged()
    {
        var query = new QueryDescription().WithAny<A>().WithChanged<B>();
        AssertMatches(query, _e2, _e3);
    }

    [Test]
    public void WithNoneAndChanged()
    {
        var query = new QueryDescription().WithNone<B>().WithChanged<B>();
        AssertMatches(query);
    }

    [Test]
    public void WithDisjointNoneAndChanged()
    {
        var query = new QueryDescription().WithNone<A>().WithChanged<C>();
        AssertMatches(query, _e5);
    }

    [Test]
    public void WithExclusiveAndChanged()
    {
        var query = new QueryDescription().WithExclusive<A, B>().WithChanged<B>();
        AssertMatches(query, _e2);
    }

    [Test]
    public void WithDisjointExclusiveAndChanged()
    {
        var query = new QueryDescription().WithExclusive<A, B>().WithChanged<C>();
        AssertMatches(query, _nothing);
    }

    [Test]
    public void WithDisjointAnyNoneAndChanged()
    {
        var query = new QueryDescription().WithAny<A>().WithChanged<B>().WithNone<C>();
        AssertMatches(query, _e2);
    }

    private void AssertMatches(QueryDescription query, params Entity[] expected)
    {
        var count = _world.CountEntities(query);
        var entities = new Entity[count];
        _world.GetEntities(query, entities);
        CollectionAssert.AreEquivalent(expected, entities);
    }
}
