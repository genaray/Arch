using Arch.Core;
using Arch.Core.Extensions.Dangerous;
using static Arch.Tests.ReferenceModel;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="RecycledEntityIdTest"/> class
///     tests that the ids of destroyed entities are reused, the last destroyed first, with a new version.
/// </summary>
[TestFixture]
public sealed class RecycledEntityIdTest
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _world = CreateWorldWithSmallChunks();
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    /// <summary>
    ///     Checks that a recycled id keeps the entity alive checks working, no matter in which order ids are reused.
    /// </summary>
    [Test]
    public void RecycledIdsGetANewVersionAndOldEntitiesStayDead()
    {
        var entities = new Entity[20];
        for (var index = 0; index < entities.Length; index++)
        {
            entities[index] = _world.Create(NewTransform(index));
        }

        foreach (var entity in entities)
        {
            _world.Destroy(entity);
        }

        That(_world.Size, Is.EqualTo(0));

        var recreated = new Entity[entities.Length];
        for (var index = 0; index < recreated.Length; index++)
        {
            recreated[index] = _world.Create(NewTransform(100 + index));
            That(_world.IsAlive(recreated[index]), Is.True);
        }

        // The very same ids are used again, each with a higher version
        That(recreated.Select(entity => entity.Id).OrderBy(id => id), Is.EqualTo(entities.Select(entity => entity.Id).OrderBy(id => id)));
        foreach (var entity in recreated)
        {
            var old = entities.Single(candidate => candidate.Id == entity.Id);
            That(entity.Version, Is.EqualTo(old.Version + 1));
            That(_world.IsAlive(old), Is.False);
        }

        for (var index = 0; index < recreated.Length; index++)
        {
            AssertEqual(NewTransform(100 + index), _world.Get<Transform>(recreated[index]));
        }
    }

    /// <summary>
    ///     Checks that creating an entity of a destroyed one returns it with a higher version, also if only one id was recycled.
    /// </summary>
    [Test]
    public void SingleRecycledIdIsReusedWithNextVersion()
    {
        var entity = _world.Create(NewTransform(1));
        _world.Destroy(entity);

        var recreated = _world.Create(NewTransform(2));

        That(recreated.Id, Is.EqualTo(entity.Id));
        That(recreated.Version, Is.EqualTo(entity.Version + 1));
        That(_world.IsAlive(entity), Is.False);
        That(_world.IsAlive(recreated), Is.True);
    }

    /// <summary>
    ///     Checks that the most recently destroyed id is the first one that gets reused.
    /// </summary>
    [Test]
    public void RecycledIdsAreReusedLastDestroyedFirst()
    {
        var entities = new Entity[5];
        for (var index = 0; index < entities.Length; index++)
        {
            entities[index] = _world.Create(NewTransform(index));
        }

        _world.Destroy(entities[0]);
        _world.Destroy(entities[3]);
        _world.Destroy(entities[1]);

        foreach (var destroyed in new[] { entities[1], entities[3], entities[0] })
        {
            var recreated = _world.Create(NewTransform(destroyed.Id));
            That(recreated.Id, Is.EqualTo(destroyed.Id));
            That(recreated.Version, Is.EqualTo(destroyed.Version + 1));
        }

        // Only now new ids are handed out
        That(_world.Create(NewTransform(5)).Id, Is.EqualTo(entities.Length));
    }

    /// <summary>
    ///     Checks that the recycled ids can be read and restored, the first one is the one reused next.
    /// </summary>
    [Test]
    public void RecycledEntityIdsKeepTheirOrderWhenRestored()
    {
        var entities = new Entity[4];
        for (var index = 0; index < entities.Length; index++)
        {
            entities[index] = _world.Create(NewTransform(index));
        }

        _world.Destroy(entities[0]);
        _world.Destroy(entities[2]);
        _world.Destroy(entities[1]);

        var recycled = _world.GetRecycledEntityIds();
        CollectionAssert.AreEqual(new List<(int, int)> { (entities[1].Id, entities[1].Version + 1), (entities[2].Id, entities[2].Version + 1), (entities[0].Id, entities[0].Version + 1) }, recycled);

        _world.SetRecycledEntityIds(recycled);
        CollectionAssert.AreEqual(recycled, _world.GetRecycledEntityIds());
        That(_world.Create(NewTransform(10)).Id, Is.EqualTo(recycled[0].Item1));
        That(_world.Create(NewTransform(11)).Id, Is.EqualTo(recycled[1].Item1));
        That(_world.Create(NewTransform(12)).Id, Is.EqualTo(recycled[2].Item1));
    }
}
