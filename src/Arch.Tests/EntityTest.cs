using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

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

        Assert.True(bitset.All(archetype.BitSet));
    }

    [Test]
    public void IsAlive()
    {
        Assert.True(_entity.IsAlive());
    }

    [Test]
    public void Has()
    {
        Assert.True(_entity.Has<Transform>());
    }

    [Test]
    public void HasNot()
    {
        Assert.False(_entity.Has<int>());
    }

    [Test]
    public void SetAndGet()
    {
        var transform = new Transform { X = 10, Y = 10 };
        _entity.Set(transform);
        ref var tramsformReference = ref _entity.Get<Transform>();

        Assert.AreEqual(transform.X, tramsformReference.X);
        Assert.AreEqual(transform.Y, tramsformReference.Y);
    }
}

public partial class EntityTest
{

    [Test]
    public void GeneratedSetAndGet()
    {
        _entity.Set(new Transform { X = 10, Y = 10 }, new Rotation{ X = 10, Y = 10});
        var refs = _entity.Get<Transform, Rotation>();

        Assert.AreEqual(10, refs.t0.X);
        Assert.AreEqual(10, refs.t0.Y);
        Assert.AreEqual(10, refs.t1.X);
        Assert.AreEqual(10, refs.t1.Y);
    }

    [Test]
    public void GeneratedHas()
    {
        Assert.True(_entity.Has<Transform, Rotation>());
    }
}

#endif