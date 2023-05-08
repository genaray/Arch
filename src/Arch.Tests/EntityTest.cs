using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

#if !PURE_ECS

/// <summary>
///     The <see cref="EntityTest"/> class
///     tests basic <see cref="Entity"/> operations and especially their extensions.
/// </summary>
[TestFixture]
public partial class EntityTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        _world = World.Create();
        _entity = _world.Create(_group);
    }

    private World _world;
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };
    private Entity _entity;

    /// <summary>
    ///     Checks if the correct <see cref="Archetype"/> is returned by the <see cref="Entity"/> extension.
    /// </summary>
    [Test]
    public void GetArchetype()
    {
        var bitset = new BitSet();
        bitset.SetBits(_group);

        var archetype = _entity.GetArchetype();

        True(bitset.All(archetype.BitSet));
    }

    /// <summary>
    ///     Checks if the <see cref="Entity"/> is alive.
    /// </summary>
    [Test]
    public void IsAlive()
    {
        True(_entity.IsAlive());
        False(_world.IsAlive(new Entity()));
    }

    /// <summary>
    ///     Checks if the has extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void Has()
    {
        True(_entity.Has<Transform>());
    }

    /// <summary>
    ///     Checks if the has extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void HasNot()
    {
        False(_entity.Has<int>());
    }

    /// <summary>
    ///     Checks if the set and get extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void SetAndGet()
    {
        var transform = new Transform { X = 10, Y = 10 };
        _entity.Set(transform);
        ref var tramsformReference = ref _entity.Get<Transform>();

        That(tramsformReference.X, Is.EqualTo(transform.X));
        That(tramsformReference.Y, Is.EqualTo(transform.Y));
    }
}

// Non generics
public partial class EntityTest
{


    /// <summary>
    ///     Checks if the non generic has extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void Has_NonGeneric()
    {
        True(_entity.Has(typeof(Transform)));
    }


    /// <summary>
    ///     Checks if the non generic has extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void HasNot_NonGeneric()
    {
        False(_entity.Has(typeof(int)));
    }


    /// <summary>
    ///     Checks if the non generic set and get extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void SetAndGet_NonGeneric()
    {
        var transform = (object)new Transform { X = 10, Y = 10 };
        _entity.Set(transform);
        var tramsformReference = (Transform)_entity.Get(typeof(Transform));

        That(tramsformReference.X, Is.EqualTo(10));
        That(tramsformReference.Y, Is.EqualTo(10));
    }


    /// <summary>
    ///     Checks if the non generic add and remove extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void AddAndRemove_NonGeneric()
    {
        using var world = World.Create();
        var entity = world.Create();

        entity.AddRange(new List<ComponentType>{typeof(Transform),  typeof(Rotation)});
        That(entity.HasRange(typeof(Transform), typeof(Rotation)));

        entity.RemoveRange(typeof(Transform), typeof(Rotation));
        That(!entity.HasRange(typeof(Transform), typeof(Rotation)));

        entity.AddRange(new Transform(), new Rotation());
        That(entity.HasRange(typeof(Transform), typeof(Rotation)));
    }
}

public partial class EntityTest
{

    /// <summary>
    ///     Checks if the source generated set and get extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void GeneratedSetAndGet()
    {
        _entity.Set(new Transform { X = 10, Y = 10 }, new Rotation { X = 10, Y = 10 });
        var refs = _entity.Get<Transform, Rotation>();

        AreEqual(10, refs.t0.X);
        AreEqual(10, refs.t0.Y);
        AreEqual(10, refs.t1.X);
        AreEqual(10, refs.t1.Y);
    }

    /// <summary>
    ///     Checks if the source generated has extension works correctly for the <see cref="Entity"/>.
    /// </summary>
    [Test]
    public void GeneratedHas()
    {
        True(_entity.Has<Transform, Rotation>());
    }
}
#endif
