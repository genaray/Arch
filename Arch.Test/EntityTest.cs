using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class EntityTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        _world = World.Create();
        _group = new[] { typeof(Transform), typeof(Rotation) };
        _entity = _world.Create(_group);
    }

    private World _world;
    private Type[] _group;
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