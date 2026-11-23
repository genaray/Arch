using Arch.Core;
using Arch.Core.Utils;
using static Arch.Tests.ReferenceModel;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="BulkCreateTest"/> class
///     tests creating many entities at once across chunk boundaries, with and without recycled ids.
/// </summary>
[TestFixture]
public sealed class BulkCreateTest
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
    ///     Checks that the bulk create fills a different amount of entities across chunk boundaries.
    /// </summary>
    [TestCase(1)]
    [TestCase(7)]
    [TestCase(8)]
    [TestCase(9)]
    [TestCase(100)]
    [TestCase(1000)]
    public void BulkCreateFillsEveryEntity(int amount)
    {
        _world.Create<Transform, Rotation>(amount, NewTransform(5), NewRotation(5));

        That(_world.Size, Is.EqualTo(amount));

        var ids = new HashSet<int>();
        var query = new QueryDescription().WithAll<Transform, Rotation>();
        _world.Query(in query, (Entity entity, ref Transform transform, ref Rotation rotation) =>
        {
            That(ids.Add(entity.Id), Is.True);
            That(_world.IsAlive(entity), Is.True);
            AssertEqual(NewTransform(5), transform);
            AssertEqual(NewRotation(5), rotation);
        });
        That(ids.Count, Is.EqualTo(amount));
    }

    /// <summary>
    ///     Checks that bulk creates reuse recycled ids, fill up half-empty chunks and leave existing entities untouched.
    /// </summary>
    [Test]
    public void BulkCreateAfterDestroysReusesIdsAndKeepsExistingEntities()
    {
        var created = new List<Entity>();
        for (var index = 0; index < 300; index++)
        {
            created.Add(_world.Create(NewTransform(index)));
        }

        var survivors = new Dictionary<Entity, int>();
        var destroyed = new List<Entity>();
        for (var index = 0; index < created.Count; index++)
        {
            if (index % 3 == 0)
            {
                _world.Destroy(created[index]);
                destroyed.Add(created[index]);
            }
            else
            {
                survivors.Add(created[index], index);
            }
        }

        // More than were destroyed, so recycled ids and brand new ids are mixed
        _world.Create<Transform>(250, NewTransform(-1));

        That(_world.Size, Is.EqualTo(survivors.Count + 250));

        var ids = new HashSet<int>();
        var newEntities = 0;
        _world.Query(in AllTransforms, (Entity entity, ref Transform transform) =>
        {
            That(ids.Add(entity.Id), Is.True);
            if (survivors.TryGetValue(entity, out var index))
            {
                AssertEqual(NewTransform(index), transform);
                return;
            }

            AssertEqual(NewTransform(-1), transform);
            newEntities++;
        });

        That(newEntities, Is.EqualTo(250));
        foreach (var entity in destroyed)
        {
            That(_world.IsAlive(entity), Is.False);
        }
    }

    /// <summary>
    ///     Checks that consecutive bulk creates continue in partially filled chunks, with and without components.
    /// </summary>
    [Test]
    public void BulkCreatesContinueInPartiallyFilledChunks()
    {
        _world.Create(NewTransform(0), NewRotation(0));
        _world.Create<Transform, Rotation>(5, NewTransform(1), NewRotation(1));
        _world.Create<Transform, Rotation>(0, NewTransform(2), NewRotation(2));
        _world.Create<Transform, Rotation>(3, NewTransform(3), NewRotation(3));
        _world.Create<Transform, Rotation>(20, NewTransform(4), NewRotation(4));
        _world.Create(NewTransform(5), NewRotation(5));

        var expectedAmounts = new Dictionary<int, int> { [0] = 1, [1] = 5, [3] = 3, [4] = 20, [5] = 1 };
        var amounts = new Dictionary<int, int>();
        var ids = new HashSet<int>();
        var query = new QueryDescription().WithAll<Transform, Rotation>();
        _world.Query(in query, (Entity entity, ref Transform transform, ref Rotation rotation) =>
        {
            var seed = (int)transform.X;
            That(ids.Add(entity.Id), Is.True);
            AssertEqual(NewTransform(seed), transform);
            AssertEqual(NewRotation(seed), rotation);
            amounts[seed] = amounts.GetValueOrDefault(seed) + 1;
        });

        That(_world.Size, Is.EqualTo(30));
        That(amounts.Count, Is.EqualTo(expectedAmounts.Count));
        foreach (var (seed, amount) in expectedAmounts)
        {
            That(amounts[seed], Is.EqualTo(amount));
        }
    }

    /// <summary>
    ///     Checks that the bulk create by <see cref="Signature"/> returns distinct, living entities of the right structure.
    /// </summary>
    [Test]
    public void BulkCreateBySignatureReturnsDistinctLivingEntities()
    {
        var signature = new Signature(typeof(Transform), typeof(Label));
        var first = new Entity[500];
        _world.Create(first.AsSpan(), in signature, first.Length);

        // Destroy some, so the second batch reuses ids
        for (var index = 0; index < first.Length; index += 2)
        {
            _world.Destroy(first[index]);
        }

        var second = new Entity[300];
        _world.Create(second.AsSpan(), in signature, second.Length);

        var living = first.Where((_, index) => index % 2 == 1).Concat(second).ToArray();
        That(_world.Size, Is.EqualTo(living.Length));
        That(living.Select(entity => entity.Id).Distinct().Count(), Is.EqualTo(living.Length));

        for (var index = 0; index < living.Length; index++)
        {
            That(_world.IsAlive(living[index]), Is.True);
            That(_world.Has<Transform>(living[index]), Is.True);
            That(_world.Has<Label>(living[index]), Is.True);

            _world.Set(living[index], NewTransform(index));
            _world.Set(living[index], NewLabel(index));
        }

        for (var index = 0; index < living.Length; index++)
        {
            _world.AssertMatches(living[index], new ExpectedEntity { Transform = NewTransform(index), Label = NewLabel(index) });
        }
    }
}
