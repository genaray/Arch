using Arch.Core;
using Arch.Core.Relationships;

namespace Arch.Tests;

#if EVENTS

[TestFixture]
public class RelationshipTest
{
    private World _world = default!;

    [SetUp]
    public void SetUp()
    {
        _world = World.Create();
    }

    [Test]
    public void NoRelationships()
    {
        // Create entities without setting any relationships
        var parent = _world.Create();
        var childOne = _world.Create();
        var childTwo = _world.Create();

        // Get should throw
        Assert.That(() => _world.GetRelationships<ParentOf>(parent), Throws.Exception);
        Assert.That(() => _world.GetRelationships<ChildOf>(childOne), Throws.Exception);
        Assert.That(() => _world.GetRelationships<ChildOf>(childTwo), Throws.Exception);

        // TryGet should return false
        Assert.False(_world.TryGetRelationships<ParentOf>(parent, out _));
        Assert.False(_world.TryGetRelationships<ChildOf>(childOne, out _));
        Assert.False(_world.TryGetRelationships<ChildOf>(childTwo, out _));

        // TryGetRef should return a null ref and false
        Assert.That(Unsafe.IsNullRef(ref _world.TryGetRefRelationships<ParentOf>(parent, out var exists)));
        Assert.False(exists);

        _world.TryGetRefRelationships<ChildOf>(childOne, out exists);
        Assert.False(exists);

        _world.TryGetRefRelationships<ChildOf>(childTwo, out exists);
        Assert.False(exists);
    }

    [Test]
    public void RelationshipCleanup()
    {
        // Setup handling relationship cleanup
        _world.HandleRelationshipCleanup();

        // Create entities
        var parent = _world.Create();
        var childOne = _world.Create();
        var childTwo = _world.Create();

        // Add the relationships
        _world.AddRelationship<ParentOf>(parent, childOne);
        _world.AddRelationship<ParentOf>(parent, childTwo);
        _world.AddRelationship<ChildOf>(childOne, parent);
        _world.AddRelationship<ChildOf>(childTwo, parent);

        // Get should return a reference to the relationships
        Assert.That(_world.GetRelationships<ParentOf>(parent).Elements, Does
            .ContainKey(childOne).And
            .ContainKey(childTwo));
        Assert.That(_world.GetRelationships<ChildOf>(childOne).Elements, Does.ContainKey(parent));
        Assert.That(_world.GetRelationships<ChildOf>(childTwo).Elements, Does.ContainKey(parent));

        // TryGet should return true and the relationships
        Assert.True(_world.TryGetRelationships<ParentOf>(parent, out var parentRelationships));
        Assert.That(parentRelationships.Elements, Does.ContainKey(childOne).And.ContainKey(childTwo));

        Assert.True(_world.TryGetRelationships<ChildOf>(childOne, out var childRelationships));
        Assert.That(childRelationships.Elements, Does.ContainKey(parent));

        Assert.True(_world.TryGetRelationships<ChildOf>(childTwo, out childRelationships));
        Assert.That(childRelationships.Elements, Does.ContainKey(parent));

        // TryGetRef should return a reference to the relationships and true
        ref var parentRelationshipsRef = ref _world.TryGetRefRelationships<ParentOf>(parent, out var exists);
        Assert.True(exists);
        Assert.That(parentRelationshipsRef.Elements, Does.ContainKey(childOne).And.ContainKey(childTwo));

        ref var childOneRelationshipsRef = ref _world.TryGetRefRelationships<ChildOf>(childOne, out exists);
        Assert.True(exists);
        Assert.That(childOneRelationshipsRef.Elements, Does.ContainKey(parent));

        ref var childTwoRelationshipsRef = ref _world.TryGetRefRelationships<ChildOf>(childTwo, out exists);
        Assert.True(exists);
        Assert.That(childTwoRelationshipsRef.Elements, Does.ContainKey(parent));

        // Destroy childOne, should remove any relationships containing it
        _world.Destroy(childOne);

        // Get should throw on childOne and not return it in any of the other references
        Assert.That(_world.GetRelationships<ParentOf>(parent).Elements, Does.Not.ContainKey(childOne).And.ContainKey(childTwo));
        Assert.That(() => _world.GetRelationships<ChildOf>(childOne).Elements, Throws.Exception);
        Assert.That(_world.GetRelationships<ChildOf>(childTwo).Elements, Does.ContainKey(parent));

        // Destroy childTwo, should remove any relationships containing it
        _world.Destroy(childTwo);

        // Get, all should throw as no relationships are left
        Assert.That(() => _world.GetRelationships<ParentOf>(parent), Throws.Exception);
        Assert.That(() => _world.GetRelationships<ChildOf>(childOne), Throws.Exception);
        Assert.That(() => _world.GetRelationships<ChildOf>(childTwo), Throws.Exception);

        // Destroy parent
        Assert.DoesNotThrow(() => _world.Destroy(parent));
    }

    [Test]
    public void QueryRelationship()
    {
        // Create entities
        var parent = _world.Create();
        var childOne = _world.Create();
        var childTwo = _world.Create();

        // Add relationships
        _world.AddRelationship<ParentOf>(parent, childOne);
        _world.AddRelationship<ParentOf>(parent, childTwo);
        _world.AddRelationship<ChildOf>(childOne, parent);
        _world.AddRelationship<ChildOf>(childTwo, parent);

        // Query all ParentOf relationships
        var query = new QueryDescription().WithAll<EntityRelationshipBuffer<ParentOf>>();
        var entities = new List<Entity>();
        _world.Query(query, (in Entity _, ref EntityRelationshipBuffer<ParentOf> parentOf) =>
        {
            entities.AddRange(parentOf.Elements.Select(p => p.Key));
        });

        // Should find two ParentOf relationships, from childOne and childTwo
        Assert.That(entities, Has.Count.EqualTo(2));
        Assert.That(entities, Does.Contain(childOne));
        Assert.That(entities, Does.Contain(childTwo));
    }
}

#endif
