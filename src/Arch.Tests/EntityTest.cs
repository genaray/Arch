using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

#if !PURE_ECS
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

    [Test]
    public void GetArchetype()
    {
        var bitset = new BitSet();
        bitset.SetBits(_group);

        var archetype = _entity.GetArchetype();

        True(bitset.All(archetype.BitSet));
    }

    [Test]
    public void IsAlive()
    {
        True(_entity.IsAlive());
    }

    [Test]
    public void Has()
    {
        True(_entity.Has<Transform>());
    }

    [Test]
    public void HasNot()
    {
        False(_entity.Has<int>());
    }

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

public partial class EntityTest
{

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

    [Test]
    public void GeneratedHas()
    {
        True(_entity.Has<Transform, Rotation>());
    }
}
#endif
