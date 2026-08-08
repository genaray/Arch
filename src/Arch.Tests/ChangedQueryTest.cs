using Arch.Core;

namespace Arch.Tests;

[TestFixture]
public class ChangedQueryTest
{
    private struct A;
    private struct B;
    private struct C;

    private World _world;
    private Entity _e0, _e1, _e2, _e3, _e4, _e5;
    private Entity[] _nothing = [];

    [OneTimeSetUp]
    public void Setup()
    {
        _world = World.Create();

        _e0 = _world.Create<A, B>();

        _e1 = _world.Create<A, B>();
        _world.MarkChanged<B>(_e1);

        _e2 = _world.Create<A, B, C>();
        _world.MarkChanged<B>(_e2);

        _e3 = _world.Create<B>();
        _world.MarkChanged<B>(_e3);

        _e4 = _world.Create<C>();
        _world.MarkChanged<C>(_e4);

        _e5 = _world.Create();
    }

    [Test]
    public void WithChangedOnly()
    {
        var query = new QueryDescription().WithChanged<B>();
        AssertMatches(query, _e1, _e2, _e3);
    }

    [Test]
    public void WithAnyAndChanged()
    {
        var query = new QueryDescription().WithAny<C>().WithChanged<C>();
        AssertMatches(query, _e4);
    }

    [Test]
    public void WithDisjointAnyAndChanged()
    {
        var query = new QueryDescription().WithAny<A>().WithChanged<B>();
        AssertMatches(query, _e1, _e2, _e3);
    }

    [Test]
    public void WithNoneAndChanged()
    {
        var query = new QueryDescription().WithNone<B>().WithChanged<B>();
        AssertMatches(query, _nothing);
    }

    [Test]
    public void WithDisjointNoneAndChanged()
    {
        var query = new QueryDescription().WithNone<A>().WithChanged<C>();
        AssertMatches(query, _e4);
    }

    [Test]
    public void WithExclusiveAndChanged()
    {
        var query = new QueryDescription().WithExclusive<A, B>().WithChanged<B>();
        AssertMatches(query, _e1);
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
        AssertMatches(query, _e1, _e3);
    }

    private void AssertMatches(QueryDescription query, params Entity[] expected)
    {
        var entities = new Entity[10];
        var total = _world.GetEntities(query, entities);
        entities = entities[..total];
        CollectionAssert.AreEquivalent(expected, entities);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _world.Dispose();
    }
}
