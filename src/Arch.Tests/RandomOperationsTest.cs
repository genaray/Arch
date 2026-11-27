using Arch.Core;
using Arch.Core.Utils;
using static Arch.Tests.ReferenceModel;

namespace Arch.Tests;

/// <summary>
///     The <see cref="RandomOperationsTest"/> class
///     applies a long, seeded sequence of structural changes to a <see cref="World"/> and to a plain dictionary
///     and checks that both always agree, so optimizations of the hot paths can not silently change behaviour.
/// </summary>
[TestFixture]
public sealed class RandomOperationsTest
{
    private const int RandomSeed = 12345;
    private const int Steps = 25_000;

    // Checking the whole world is slow, but a mistake is found closer to the step that caused it
    private const int StepsBetweenChecks = 1000;

    // Every few steps all entities get a Health by query and some steps later all lose it again
    private const int StepsBetweenBulkChanges = 3000;

    // Checking all destroyed entities again and again gets slow, the latest ones are enough
    private const int DestroyedEntitiesToRecheck = 200;

    private enum Operation
    {
        Create,
        Destroy,
        AddComponent,
        RemoveComponent,
        SetTransform,
        AddPair,
        RemovePair
    }

    private World _world;
    private Random _random;
    private Dictionary<Entity, ExpectedEntity> _model;
    private List<Entity> _alive;
    private List<Entity> _destroyed;
    private int _lastSeed;

    [SetUp]
    public void Setup()
    {
        _world = CreateWorldWithSmallChunks();
        _random = new Random(RandomSeed);
        _model = new Dictionary<Entity, ExpectedEntity>();
        _alive = new List<Entity>();
        _destroyed = new List<Entity>();
        _lastSeed = 0;
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    [Test]
    public void RandomOperationsMatchReferenceModel()
    {
        for (var step = 1; step <= Steps; step++)
        {
            ApplyRandomOperation(step);

            if (step % StepsBetweenChecks == 0)
            {
                _world.AssertMatchesModel(_model, _destroyed);
                _destroyed.RemoveRange(0, Math.Max(0, _destroyed.Count - DestroyedEntitiesToRecheck));
            }
        }

        _world.AssertMatchesModel(_model, _destroyed);
    }

    // The chances in percent: create 30, destroy 10, add 15, remove 15, set 10, add pair 10, remove pair 10
    private static Operation ToOperation(int percent)
    {
        return percent switch
        {
            < 30 => Operation.Create,
            < 40 => Operation.Destroy,
            < 55 => Operation.AddComponent,
            < 70 => Operation.RemoveComponent,
            < 80 => Operation.SetTransform,
            < 90 => Operation.AddPair,
            _ => Operation.RemovePair
        };
    }

    private void ApplyRandomOperation(int step)
    {
        var seed = ++_lastSeed;
        var operation = ToOperation(_random.Next(100));

        if (step % StepsBetweenBulkChanges == StepsBetweenBulkChanges / 2)
        {
            AddHealthToAllWithout();
        }
        else if (step % StepsBetweenBulkChanges == 0)
        {
            RemoveHealthFromAll();
        }
        else if (operation == Operation.Create || _alive.Count == 0)
        {
            CreateRandomEntity(seed);
        }
        else
        {
            ChangeRandomEntity(operation, seed);
        }
    }

    private void AddHealthToAllWithout()
    {
        var withoutHealth = new QueryDescription().WithAll<Transform>().WithNone<Health>();
        _world.Add(in withoutHealth, NewHealth(7));

        foreach (var expected in _model.Values.Where(expected => !expected.Health.HasValue))
        {
            expected.Health = NewHealth(7);
        }
    }

    private void RemoveHealthFromAll()
    {
        _world.Remove<Health>(new QueryDescription().WithAll<Health>());

        foreach (var expected in _model.Values)
        {
            expected.Health = null;
        }
    }

    private void CreateRandomEntity(int seed)
    {
        var transform = NewTransform(seed);
        var rotation = NewRotation(seed);
        var label = NewLabel(seed);
        var health = NewHealth(seed);

        switch (_random.Next(6))
        {
            case 0:
                Track(_world.Create(transform), new ExpectedEntity { Transform = transform });
                break;
            case 1:
                Track(_world.Create(transform, rotation), new ExpectedEntity { Transform = transform, Rotation = rotation });
                break;
            case 2:
                Track(_world.Create(transform, label), new ExpectedEntity { Transform = transform, Label = label });
                break;
            case 3:
                Track(_world.Create(transform, health), new ExpectedEntity { Transform = transform, Health = health });
                break;
            case 4:
                Track(_world.Create(transform, rotation, label, health), new ExpectedEntity { Transform = transform, Rotation = rotation, Label = label, Health = health });
                break;
            default:
                // Created without values and set afterwards
                var entity = _world.Create(new Signature(typeof(Transform), typeof(Label)));
                _world.Set(entity, transform);
                _world.Set(entity, label);
                Track(entity, new ExpectedEntity { Transform = transform, Label = label });
                break;
        }
    }

    private void Track(Entity entity, ExpectedEntity expected)
    {
        _model.Add(entity, expected);
        _alive.Add(entity);
    }

    private void ChangeRandomEntity(Operation operation, int seed)
    {
        var index = _random.Next(_alive.Count);
        var entity = _alive[index];
        var expected = _model[entity];

        switch (operation)
        {
            case Operation.Destroy:
                DestroyEntity(index);
                break;
            case Operation.AddComponent:
                AddRandomComponent(entity, expected, seed);
                break;
            case Operation.RemoveComponent:
                RemoveRandomComponent(entity, expected);
                break;
            case Operation.SetTransform:
                _world.Set(entity, NewTransform(seed));
                expected.Transform = NewTransform(seed);
                break;
            case Operation.AddPair:
                AddRotationAndLabel(entity, expected, seed);
                break;
            case Operation.RemovePair:
                RemoveRotationAndLabel(entity, expected);
                break;
        }
    }

    private void DestroyEntity(int aliveIndex)
    {
        var entity = _alive[aliveIndex];
        _world.Destroy(entity);

        _model.Remove(entity);
        _destroyed.Add(entity);
        _alive[aliveIndex] = _alive[^1];
        _alive.RemoveAt(_alive.Count - 1);
    }

    // Adds one component the entity does not have yet, the Label without a value and sets it afterwards
    private void AddRandomComponent(Entity entity, ExpectedEntity expected, int seed)
    {
        switch (_random.Next(3))
        {
            case 0 when !expected.Rotation.HasValue:
                _world.Add(entity, NewRotation(seed));
                expected.Rotation = NewRotation(seed);
                break;
            case 1 when !expected.Label.HasValue:
                _world.Add<Label>(entity);
                _world.Set(entity, NewLabel(seed));
                expected.Label = NewLabel(seed);
                break;
            case 2 when !expected.Health.HasValue:
                _world.Add(entity, NewHealth(seed));
                expected.Health = NewHealth(seed);
                break;
        }
    }

    private void RemoveRandomComponent(Entity entity, ExpectedEntity expected)
    {
        switch (_random.Next(3))
        {
            case 0 when expected.Rotation.HasValue:
                _world.Remove<Rotation>(entity);
                expected.Rotation = null;
                break;
            case 1 when expected.Label.HasValue:
                _world.Remove<Label>(entity);
                expected.Label = null;
                break;
            case 2 when expected.Health.HasValue:
                _world.Remove<Health>(entity);
                expected.Health = null;
                break;
        }
    }

    private void AddRotationAndLabel(Entity entity, ExpectedEntity expected, int seed)
    {
        if (expected.Rotation.HasValue || expected.Label.HasValue)
        {
            return;
        }

        _world.Add(entity, NewRotation(seed), NewLabel(seed));
        expected.Rotation = NewRotation(seed);
        expected.Label = NewLabel(seed);
    }

    private void RemoveRotationAndLabel(Entity entity, ExpectedEntity expected)
    {
        if (!expected.Rotation.HasValue || !expected.Label.HasValue)
        {
            return;
        }

        _world.Remove<Rotation, Label>(entity);
        expected.Rotation = null;
        expected.Label = null;
    }
}
