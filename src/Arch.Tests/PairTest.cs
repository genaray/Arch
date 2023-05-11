using Arch.Core;

namespace Arch.Tests;

[TestFixture]
public class PairTest
{
    private World _world = default!;

    [SetUp]
    public void SetUp()
    {
        _world = World.Create();
    }

    [Test]
    public void CreatePair()
    {
        // Create without setting any pairs yet
        var parent = _world.Create();
        var childOne = _world.Create();
        var childTwo = _world.Create();

        // Get
        Assert.That(() => _world.GetPairs<ParentOf>(parent), Throws.Exception);
        Assert.That(() => _world.GetPairs<ChildOf>(childOne), Throws.Exception);
        Assert.That(() => _world.GetPairs<ChildOf>(childTwo), Throws.Exception);

        // TryGet
        Assert.False(_world.TryGetPairs<ParentOf>(parent, out _));
        Assert.False(_world.TryGetPairs<ChildOf>(childOne, out _));
        Assert.False(_world.TryGetPairs<ChildOf>(childTwo, out _));

        // TryGetRef
        _world.TryGetRefPairs<ParentOf>(parent, out var exists);
        Assert.False(exists);

        _world.TryGetRefPairs<ChildOf>(childOne, out exists);
        Assert.False(exists);

        _world.TryGetRefPairs<ChildOf>(childTwo, out exists);
        Assert.False(exists);

        // Add the pairs
        _world.AddPair<ParentOf>(parent, childOne);
        _world.AddPair<ParentOf>(parent, childTwo);
        _world.AddPair<ChildOf>(childOne, parent);
        _world.AddPair<ChildOf>(childTwo, parent);

        // Get
        Assert.That(_world.GetPairs<ParentOf>(parent).Elements, Does
            .ContainKey(childOne).And
            .ContainKey(childTwo));
        Assert.That(_world.GetPairs<ChildOf>(childOne).Elements, Does.ContainKey(parent));
        Assert.That(_world.GetPairs<ChildOf>(childTwo).Elements, Does.ContainKey(parent));

        // TryGet
        Assert.True(_world.TryGetPairs<ParentOf>(parent, out var parentPairs));
        Assert.That(parentPairs.Elements, Does.ContainKey(childOne).And.ContainKey(childTwo));

        Assert.True(_world.TryGetPairs<ChildOf>(childOne, out var childPairs));
        Assert.That(childPairs.Elements, Does.ContainKey(parent));

        Assert.True(_world.TryGetPairs<ChildOf>(childTwo, out childPairs));
        Assert.That(childPairs.Elements, Does.ContainKey(parent));

        // TryGetRef
        ref var parentPairsRef = ref _world.TryGetRefPairs<ParentOf>(parent, out exists);
        Assert.True(exists);
        Assert.That(parentPairsRef.Elements, Does.ContainKey(childOne).And.ContainKey(childTwo));

        ref var childOnePairsRef = ref _world.TryGetRefPairs<ChildOf>(childOne, out exists);
        Assert.True(exists);
        Assert.That(childOnePairsRef.Elements, Does.ContainKey(parent));

        ref var childTwoPairsRef = ref _world.TryGetRefPairs<ChildOf>(childTwo, out exists);
        Assert.True(exists);
        Assert.That(childTwoPairsRef.Elements, Does.ContainKey(parent));

        // Destroy childOne, should remove any relationships containing it
        _world.Destroy(childOne);

        // Get
        Assert.That(_world.GetPairs<ParentOf>(parent).Elements, Does.Not.ContainKey(childOne).And.ContainKey(childTwo));
        Assert.That(() => _world.GetPairs<ChildOf>(childOne).Elements, Throws.Exception);
        Assert.That(_world.GetPairs<ChildOf>(childTwo).Elements, Does.ContainKey(parent));

        // Destroy childTwo, should remove any relationships containing it
        _world.Destroy(childTwo);

        // Get
        Assert.That(() => _world.GetPairs<ParentOf>(parent), Throws.Exception);
        Assert.That(() => _world.GetPairs<ChildOf>(childOne), Throws.Exception);
        Assert.That(() => _world.GetPairs<ChildOf>(childTwo), Throws.Exception);
    }

    [Test]
    public void QueryPair()
    {
        var parent = _world.Create();
        var childOne = _world.Create();
        var childTwo = _world.Create();

        _world.AddPair<ParentOf>(parent, childOne);
        _world.AddPair<ParentOf>(parent, childTwo);
        _world.AddPair<ChildOf>(childOne, parent);
        _world.AddPair<ChildOf>(childTwo, parent);

        var query = new QueryDescription().WithAll<PairBuffer<ParentOf>>();
        var entities = new List<Entity>();
        _world.Query(query, (in Entity _, ref PairBuffer<ParentOf> parentOf) =>
        {
            entities.AddRange(parentOf.Elements.Select(p => p.Key));
        });

        Assert.That(entities, Has.Count.EqualTo(2));
        Assert.That(entities, Does.Contain(childOne));
        Assert.That(entities, Does.Contain(childTwo));
    }
}
