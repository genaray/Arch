using Arch.Core;
using static Arch.Tests.ReferenceModel;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="ChunkSpanTest"/> class
///     tests the access to the components of whole chunks as spans.
/// </summary>
[TestFixture]
public sealed class ChunkSpanTest
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
    ///     Checks that the used span covers exactly the entities of a chunk, while the plain span covers its whole capacity.
    /// </summary>
    [Test]
    public void UsedSpanCoversOnlyTheEntitiesOfTheChunk()
    {
        const int amount = 100;
        for (var index = 0; index < amount; index++)
        {
            _world.Create(NewTransform(index));
        }

        var total = 0;
        var partiallyFilledChunks = 0;
        foreach (ref var chunk in _world.Query(in AllTransforms))
        {
            var used = chunk.GetUsedSpan<Transform>();
            That(used.Length, Is.EqualTo(chunk.Count));
            That(chunk.GetSpan<Transform>().Length, Is.EqualTo(chunk.Capacity));

            total += used.Length;
            partiallyFilledChunks += chunk.Count < chunk.Capacity ? 1 : 0;
        }

        That(total, Is.EqualTo(amount));
        That(partiallyFilledChunks, Is.EqualTo(1));
    }

    /// <summary>
    ///     Checks that <c>World.InlineChunkQuery</c> hands over the aligned components of every matching entity exactly once,
    ///     for the overload with a <see cref="QueryDescription"/> as well as for the one with a cached <see cref="Query"/>.
    /// </summary>
    [Test]
    public void InlineChunkQueryPassesTheComponentsOfEveryEntityOnce()
    {
        const int amount = 400;
        var withHealth = new List<(Entity Entity, int Index)>();
        var expectedSum = 0d;
        for (var index = 0; index < amount; index++)
        {
            var entity = _world.Create(NewTransform(index));
            if (index % 2 == 0)
            {
                _world.Add(entity, NewHealth(index));
                withHealth.Add((entity, index));
                expectedSum += index;
            }
        }

        var description = new QueryDescription().WithAll<Transform, Health>();
        var query = _world.Query(in description);

        var visitor = new ChunkVisitor();
        _world.InlineChunkQuery<ChunkVisitor, Transform, Health>(in description, ref visitor);
        That(visitor.Entities, Is.EqualTo(withHealth.Count));
        That(visitor.Calls, Is.GreaterThan(1));
        That(visitor.Sum, Is.EqualTo(expectedSum));

        _world.InlineChunkQuery<ChunkVisitor, Transform, Health>(query, ref visitor);
        That(visitor.Entities, Is.EqualTo(withHealth.Count * 2));
        That(visitor.Sum, Is.EqualTo(expectedSum * 2));

        // The visitor changed every health twice through the spans, an entity must still see its own components.
        foreach (var (entity, index) in withHealth)
        {
            That(_world.Get<Health>(entity).Value, Is.EqualTo(NewHealth(index).Value + 2));
            AssertEqual(NewTransform(index), _world.Get<Transform>(entity));
        }

        // Archetypes without entities must not call the visitor.
        _world.Destroy(in description);
        var untouched = new ChunkVisitor();
        _world.InlineChunkQuery<ChunkVisitor, Transform, Health>(in description, ref untouched);
        That(untouched.Calls, Is.EqualTo(0));
    }

    private struct ChunkVisitor : IChunkForEach<Transform, Health>
    {
        public int Calls;
        public int Entities;
        public double Sum;

        public void Update(Span<Transform> transforms, Span<Health> healths)
        {
            Calls++;
            Entities += transforms.Length;
            That(healths.Length, Is.EqualTo(transforms.Length));

            for (var index = 0; index < transforms.Length; index++)
            {
                Sum += transforms[index].X;
                healths[index].Value++;
            }
        }
    }
}
