using Arch.Core;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public class TrimExcessRegression
{
    private record struct Value(int Number);

    [TestCase(false)]
    [TestCase(true)]
    public void TrimExcessPreservesLiveEntities(bool batch)
    {
        using var world = World.Create();
        var entities = new Entity[10000];
        for (var index = 0; index < entities.Length; index++)
        {
            entities[index] = world.Create(new Value(index));
        }

        for (var index = 0; index < 9990; index++)
        {
            world.Destroy(entities[index]);
        }

        world.TrimExcess();
        var created = CreateEntities(world, entities.Length, batch);
        That(created.Select(entity => entity.Id).Distinct().Count(), Is.EqualTo(created.Length));
        That(world.Size, Is.EqualTo(10010));
        for (var index = 9990; index < entities.Length; index++)
        {
            That(world.IsAlive(entities[index]), Is.True);
            That(world.Get<Value>(entities[index]).Number, Is.EqualTo(index));
            That(created.Any(entity => entity.Id == entities[index].Id), Is.False);
        }
    }

    [TestCase(false)]
    [TestCase(true)]
    public void TrimExcessReusesHighIdsWithTheirVersions(bool batch)
    {
        using var world = World.Create();
        var entities = new Entity[10000];
        for (var index = 0; index < entities.Length; index++)
        {
            entities[index] = world.Create(new Value(index));
        }

        // Keep the lowest ID alive so that trimming removes the high metadata buckets.
        for (var index = entities.Length - 1; index > 0; index--)
        {
            world.Destroy(entities[index]);
        }

        for (var cycle = 0; cycle < 3; cycle++)
        {
            world.TrimExcess();
            // A small batch must grow the ID metadata even when its chunk has room.
            var first = CreateEntities(world, 1, batch);
            var remaining = CreateEntities(world, entities.Length - 2, batch);
            var created = first.Concat(remaining).ToArray();
            for (var index = 0; index < created.Length; index++)
            {
                var previous = entities[entities.Length - 1 - index];
                That(created[index].Id, Is.EqualTo(previous.Id));
                That(created[index].Version, Is.EqualTo(previous.Version + cycle + 1));
                That(world.IsAlive(previous), Is.False);
                That(world.IsAlive(created[index]), Is.True);
                That(world.Get<Value>(created[index]).Number, Is.EqualTo(-1));
            }

            That(world.Get<Value>(entities[0]).Number, Is.EqualTo(0));
            foreach (var entity in created)
            {
                world.Destroy(entity);
            }
        }
    }

    [TestCase(false)]
    [TestCase(true)]
    public void TrimExcessEmptyWorldPreservesVersions(bool batch)
    {
        using var world = World.Create();
        var previous = world.Create(new Value(42));
        world.Destroy(previous);
        world.TrimExcess();
        That(world.Capacity, Is.EqualTo(0));

        var created = CreateEntities(world, 1, batch)[0];
        That(created.Id, Is.EqualTo(previous.Id));
        That(created.Version, Is.EqualTo(previous.Version + 1));
        That(world.IsAlive(previous), Is.False);
        That(world.Get<Value>(created).Number, Is.EqualTo(-1));
    }

    private static Entity[] CreateEntities(World world, int amount, bool batch)
    {
        var entities = new Entity[amount];
        if (batch)
        {
            world.Create(entities.AsSpan(), Component<Value>.Signature, amount);
            foreach (var entity in entities)
            {
                world.Set(entity, new Value(-1));
            }
        }
        else
        {
            for (var index = 0; index < amount; index++)
            {
                entities[index] = world.Create(new Value(-1));
            }
        }

        return entities;
    }
}
