using Arch.Core;
using static Arch.Tests.ReferenceModel;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="QueryOverloadTest"/> class
///     tests that the query overloads taking a cached <see cref="Query"/> behave like the ones taking a <see cref="QueryDescription"/>.
/// </summary>
[TestFixture]
public sealed class QueryOverloadTest
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
    ///     Checks that every query overload taking a cached <see cref="Query"/> visits the same entities as the one taking a <see cref="QueryDescription"/>.
    /// </summary>
    [Test]
    public void QueryOverloadsWithCachedQueryMatchTheOnesWithDescription()
    {
        const int amount = 50;
        for (var index = 0; index < amount; index++)
        {
            var entity = _world.Create(NewTransform(index));
            if (index % 3 == 0)
            {
                _world.Add(entity, NewHealth(index));
            }
        }

        var description = new QueryDescription().WithAll<Transform>();
        var query = _world.Query(in description);
        const double expectedSum = amount * (amount - 1) / 2;

        var sum = 0d;
        _world.Query(in description, (ref Transform transform) => sum += transform.X);
        That(sum, Is.EqualTo(expectedSum));
        sum = 0;
        _world.Query(query, (ref Transform transform) => sum += transform.X);
        That(sum, Is.EqualTo(expectedSum));

        sum = 0;
        var ids = new HashSet<int>();
        _world.Query(query, (Entity entity, ref Transform transform) =>
        {
            sum += transform.X;
            ids.Add(entity.Id);
        });
        That(sum, Is.EqualTo(expectedSum));
        That(ids.Count, Is.EqualTo(amount));

        var count = 0;
        _world.Query(query, (Entity _) => count++);
        That(count, Is.EqualTo(amount));

        SumX.Total = 0;
        var sumX = new SumX();
        _world.InlineQuery<SumX, Transform>(query, ref sumX);
        _world.InlineQuery<SumX, Transform>(query);
        That(SumX.Total, Is.EqualTo(expectedSum * 2));

        SumXWithEntity.Total = 0;
        var sumXWithEntity = new SumXWithEntity();
        _world.InlineEntityQuery<SumXWithEntity, Transform>(query, ref sumXWithEntity);
        _world.InlineEntityQuery<SumXWithEntity, Transform>(query);
        That(SumXWithEntity.Total, Is.EqualTo(expectedSum * 2));

        CountEntitiesVisited.Total = 0;
        var counter = new CountEntitiesVisited();
        _world.InlineQuery(query, ref counter);
        _world.InlineQuery<CountEntitiesVisited>(query);
        That(CountEntitiesVisited.Total, Is.EqualTo(amount * 2));
    }

    private struct SumX : IForEach<Transform>
    {
        public static double Total;

        public void Update(ref Transform transform) => Total += transform.X;
    }

    private struct SumXWithEntity : IForEachWithEntity<Transform>
    {
        public static double Total;

        public void Update(Entity entity, ref Transform transform) => Total += transform.X;
    }

    private struct CountEntitiesVisited : IForEach
    {
        public static int Total;

        public void Update(Entity entity) => Total++;
    }
}
