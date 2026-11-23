using Arch.Core;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     What a <see cref="World"/> is expected to hold for one <see cref="Entity"/>.
///     Every entity has a <see cref="Transform"/>, all others are optional.
/// </summary>
internal sealed class ExpectedEntity
{
    public Transform Transform;
    public Rotation? Rotation;
    public Label? Label;
    public Health? Health;
}

/// <summary>
///     The <see cref="ReferenceModel"/> class
///     provides what the tests need to compare a <see cref="World"/> with the plain <see cref="ExpectedEntity"/>s they keep on the side:
///     a world with small chunks, components made from a seed and the asserts for the expected state.
/// </summary>
internal static class ReferenceModel
{
    // Small chunks, so a few hundred entities already span many chunks and swap-removes cross chunk boundaries.
    private const int ChunkSizeInBytes = 1024;
    private const int MinimumEntitiesPerChunk = 8;

    public static readonly QueryDescription AllTransforms = new QueryDescription().WithAll<Transform>();

    public static World CreateWorldWithSmallChunks() => World.Create(ChunkSizeInBytes, MinimumEntitiesPerChunk);

    public static Transform NewTransform(int seed) => new() { X = seed, Y = -seed };

    public static Rotation NewRotation(int seed) => new() { X = seed, Y = seed + 1, Z = seed + 2, W = seed + 3 };

    public static Label NewLabel(int seed) => new() { Value = "label" + seed };

    public static Health NewHealth(int seed) => new() { Value = seed * 3 };

    public static void AssertEqual(Transform expected, Transform actual)
    {
        That(actual.X, Is.EqualTo(expected.X));
        That(actual.Y, Is.EqualTo(expected.Y));
    }

    public static void AssertEqual(Rotation expected, Rotation actual)
    {
        That(actual.X, Is.EqualTo(expected.X));
        That(actual.Y, Is.EqualTo(expected.Y));
        That(actual.Z, Is.EqualTo(expected.Z));
        That(actual.W, Is.EqualTo(expected.W));
    }

    /// <summary>
    ///     Asserts that the <see cref="Entity"/> is alive and has exactly the components and values of the <see cref="ExpectedEntity"/>.
    /// </summary>
    public static void AssertMatches(this World world, Entity entity, ExpectedEntity expected)
    {
        That(world.IsAlive(entity), Is.True);
        AssertEqual(expected.Transform, world.Get<Transform>(entity));

        That(world.Has<Rotation>(entity), Is.EqualTo(expected.Rotation.HasValue));
        if (expected.Rotation.HasValue)
        {
            AssertEqual(expected.Rotation.Value, world.Get<Rotation>(entity));
        }

        That(world.Has<Label>(entity), Is.EqualTo(expected.Label.HasValue));
        if (expected.Label.HasValue)
        {
            That(world.Get<Label>(entity).Value, Is.EqualTo(expected.Label.Value.Value));
        }

        That(world.Has<Health>(entity), Is.EqualTo(expected.Health.HasValue));
        if (expected.Health.HasValue)
        {
            That(world.Get<Health>(entity).Value, Is.EqualTo(expected.Health.Value.Value));
        }
    }

    /// <summary>
    ///     Asserts that the <see cref="World"/> holds exactly the entities of the model, that the destroyed ones are dead
    ///     and that queries see every entity once with the same data as <see cref="World.Get{T}(Entity)"/>.
    /// </summary>
    public static void AssertMatchesModel(this World world, Dictionary<Entity, ExpectedEntity> model, List<Entity> destroyed)
    {
        That(world.Size, Is.EqualTo(model.Count));
        That(world.CountEntities(in AllTransforms), Is.EqualTo(model.Count));

        foreach (var (entity, expected) in model)
        {
            world.AssertMatches(entity, expected);
        }

        foreach (var entity in destroyed)
        {
            That(world.IsAlive(entity), Is.False);
        }

        var visited = new HashSet<Entity>();
        world.Query(in AllTransforms, (Entity entity, ref Transform transform) =>
        {
            That(visited.Add(entity), Is.True);
            AssertEqual(model[entity].Transform, transform);
        });
        That(visited.Count, Is.EqualTo(model.Count));

        var withRotation = new QueryDescription().WithAll<Transform, Rotation>();
        var rotations = 0;
        world.Query(in withRotation, (Entity entity, ref Transform _, ref Rotation rotation) =>
        {
            AssertEqual(model[entity].Rotation!.Value, rotation);
            rotations++;
        });
        That(rotations, Is.EqualTo(model.Values.Count(expected => expected.Rotation.HasValue)));
    }
}
