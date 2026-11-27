using Arch.Core;
using static Arch.Tests.ReferenceModel;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="StructuralChangeTest"/> class
///     tests that adding, removing and destroying keeps all components of all entities intact, also across chunks and after archetypes were dropped.
/// </summary>
[TestFixture]
public sealed class StructuralChangeTest
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
    ///     Moves entities of several chunks in both directions through the same add and remove transitions,
    ///     for the first, the last and the swapped-in entity of a chunk.
    /// </summary>
    [Test]
    public void AddAndRemoveKeepAllComponentsOfAllEntitiesIntact()
    {
        const int count = 50;
        var entities = new Entity[count];
        for (var index = 0; index < count; index++)
        {
            entities[index] = _world.Create(NewTransform(index), NewLabel(index));
        }

        // Forward and backward, so the moved entity is first and last in its chunk
        for (var index = 0; index < count; index += 2)
        {
            _world.Add(entities[index], NewHealth(index));
        }

        for (var index = count - 1; index > 0; index -= 2)
        {
            _world.Add(entities[index], NewHealth(index));
        }

        for (var index = 0; index < count; index++)
        {
            _world.AssertMatches(entities[index], new ExpectedEntity { Transform = NewTransform(index), Label = NewLabel(index), Health = NewHealth(index) });
        }

        // 7 and 50 are coprime, so this visits each entity once in a scattered order
        for (var step = 0; step < count; step++)
        {
            _world.Remove<Label>(entities[step * 7 % count]);
        }

        for (var index = 0; index < count; index++)
        {
            _world.AssertMatches(entities[index], new ExpectedEntity { Transform = NewTransform(index), Health = NewHealth(index) });
        }

        for (var index = 0; index < count; index++)
        {
            _world.Add(entities[index], NewRotation(index));
            _world.Remove<Health>(entities[index]);
        }

        for (var index = 0; index < count; index++)
        {
            _world.AssertMatches(entities[index], new ExpectedEntity { Transform = NewTransform(index), Rotation = NewRotation(index) });
        }
    }

    /// <summary>
    ///     Checks that cached transitions to archetypes which were dropped by <see cref="World.TrimExcess"/>
    ///     or <see cref="World.Clear"/> are not followed.
    /// </summary>
    [Test]
    public void AddAndRemoveWorkAfterArchetypesWereDropped()
    {
        var first = _world.Create(NewTransform(1));
        _world.Add(first, NewHealth(1));
        _world.Remove<Health>(first);

        var second = _world.Create(NewTransform(2), NewHealth(2));
        _world.Destroy(second);
        _world.Destroy(first);
        _world.TrimExcess();

        var third = _world.Create(NewTransform(3));
        _world.Add(third, NewHealth(3));
        _world.AssertMatches(third, new ExpectedEntity { Transform = NewTransform(3), Health = NewHealth(3) });
        _world.Remove<Health>(third);
        _world.AssertMatches(third, new ExpectedEntity { Transform = NewTransform(3) });

        _world.Clear();

        var fourth = _world.Create(NewTransform(4));
        _world.Add(fourth, NewHealth(4));
        _world.AssertMatches(fourth, new ExpectedEntity { Transform = NewTransform(4), Health = NewHealth(4) });
        _world.Remove<Health>(fourth);
        _world.AssertMatches(fourth, new ExpectedEntity { Transform = NewTransform(4) });
    }

    /// <summary>
    ///     Checks that an archetype which stays alive does not follow a cached transition into an archetype that was dropped.
    /// </summary>
    [Test]
    public void CachedTransitionsToDroppedArchetypesAreForgotten()
    {
        var withHealth = new QueryDescription().WithAll<Transform, Health>();
        var withoutHealth = new QueryDescription().WithAll<Transform>().WithNone<Health>();

        // Transform -> Transform+Health is cached, then Transform+Health is dropped while Transform lives on
        var adding = _world.Create(NewTransform(1));
        var added = _world.Create(NewTransform(2));
        _world.Add(added, NewHealth(2));
        _world.Destroy(added);
        _world.TrimExcess();

        _world.Add(adding, NewHealth(1));
        _world.AssertMatches(adding, new ExpectedEntity { Transform = NewTransform(1), Health = NewHealth(1) });
        That(_world.CountEntities(in withHealth), Is.EqualTo(1));
        That(_world.CountEntities(in withoutHealth), Is.EqualTo(0));

        // Transform+Health -> Transform is cached, then Transform is dropped while Transform+Health lives on
        var removing = _world.Create(NewTransform(3), NewHealth(3));
        _world.Remove<Health>(adding);
        _world.Destroy(adding);
        _world.TrimExcess();

        _world.Remove<Health>(removing);
        _world.AssertMatches(removing, new ExpectedEntity { Transform = NewTransform(3) });
        That(_world.CountEntities(in withHealth), Is.EqualTo(0));
        That(_world.CountEntities(in withoutHealth), Is.EqualTo(1));
    }

    /// <summary>
    ///     Checks that adding and removing by <see cref="ComponentType"/>s finds the existing archetypes,
    ///     also if many more components are registered than the entity has, which makes the bitset buffers larger than the ones of the archetypes.
    /// </summary>
    [Test]
    public void UntypedAndRangeChangesReuseArchetypesWithManyRegisteredComponents()
    {
        ComponentType[] types = [typeof(Label), typeof(Health)];
        var entity = _world.Create(NewTransform(0));

        // Components registered after the ones above, so their ids are higher.
        var filler = typeof(int);
        for (var index = 0; index < 100; index++)
        {
            filler = filler.MakeArrayType();
            Component.GetComponentType(filler);
        }

        AddAndRemoveByTypes(entity, types);
        var archetypeCount = _world.Archetypes.Count;

        AddAndRemoveByTypes(entity, types);
        That(_world.Archetypes.Count, Is.EqualTo(archetypeCount));
    }

    private void AddAndRemoveByTypes(Entity entity, ComponentType[] types)
    {
        _world.AddRange(entity, types);
        That(_world.Has<Label>(entity) && _world.Has<Health>(entity), Is.True);

        _world.Remove(entity, typeof(Health));
        That(_world.Has<Health>(entity), Is.False);
        That(_world.Has<Label>(entity), Is.True);

        _world.RemoveRange(entity, types);
        That(_world.Has<Label>(entity), Is.False);
    }

    /// <summary>
    ///     Checks that adding and removing several <see cref="ComponentType"/>s reuses the cached transition, no matter in which order the types are passed.
    /// </summary>
    [Test]
    public void RangeChangesReuseTransitionsRegardlessOfTheOrderOfTheTypes()
    {
        ComponentType[] types = [typeof(Label), typeof(Health)];
        ComponentType[] reversed = [typeof(Health), typeof(Label)];

        var entity = _world.Create(NewTransform(0));
        var source = _world.GetArchetype(entity);

        _world.AddRange(entity, types);
        var addTransition = source.AddTransitions.ByTypes.Get(types);
        var target = _world.GetArchetype(entity);
        That(addTransition, Is.Not.Null);
        That(addTransition!.Target, Is.SameAs(target));

        _world.RemoveRange(entity, reversed);
        var removeTransition = target.RemoveTransitions.ByTypes.Get(types);
        That(removeTransition, Is.Not.Null);
        That(_world.GetArchetype(entity), Is.SameAs(source));

        _world.AddRange(entity, reversed);
        That(source.AddTransitions.ByTypes.Get(types), Is.SameAs(addTransition));
        That(_world.GetArchetype(entity), Is.SameAs(target));
        That(_world.Has<Label>(entity) && _world.Has<Health>(entity), Is.True);
    }

    /// <summary>
    ///     Checks that destroyed entities do not keep their managed components alive.
    /// </summary>
    [Test]
    public void DestroyedEntitiesDoNotKeepManagedComponentsAlive()
    {
        var reference = CreateAndDestroyLabelledEntity();

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        That(reference.IsAlive, Is.False);
    }

    // Separate method, so no local of the test keeps the object alive.
    [MethodImpl(MethodImplOptions.NoInlining)]
    private WeakReference CreateAndDestroyLabelledEntity()
    {
        var value = new string('x', 100);
        var keep = _world.Create(NewTransform(0), new Label { Value = "keep" });
        var entity = _world.Create(NewTransform(1), new Label { Value = value });
        _world.Destroy(entity);
        That(_world.IsAlive(keep), Is.True);

        return new WeakReference(value);
    }
}
