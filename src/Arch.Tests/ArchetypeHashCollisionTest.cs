using System.Reflection;
using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     Simulate collisions by registering an impostor under the target hash, since the hash seed is random.
/// </summary>
[TestFixture]
public sealed class ArchetypeHashCollisionTest
{
    private World _world;

    [SetUp]
    public void Setup()
    {
        _ = Component<Transform>.ComponentType;
        _ = Component<Rotation>.ComponentType;
        _ = Component<Ai>.ComponentType;
        _world = World.Create();
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    private void RegisterImpostor(Archetype impostor, Signature target)
    {
        _world.GroupToArchetype[target.GetHashCode()] = impostor;

        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        var spanBitSet = new SpanBitSet(stack);
        foreach (var type in target)
        {
            spanBitSet.SetBit(type.Id);
        }

        _world.GroupToArchetype[spanBitSet.GetHashCode()] = impostor;
    }

    private static T WithHash<T>(T value, int hash) where T : struct
    {
        object boxed = value;
        typeof(T).GetField("_hashCode", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(boxed, hash);
        return (T)boxed;
    }

    [Test]
    public void SignatureEqualityIgnoresSharedHash()
    {
        var transform = WithHash(new Signature(typeof(Transform)), 42);
        var rotation = WithHash(new Signature(typeof(Rotation)), 42);

        That(transform.Equals(rotation), Is.False);
        That(transform.Equals(WithHash(new Signature(typeof(Transform)), 42)), Is.True);
    }

    [Test]
    public void QueryCacheIgnoresSharedHash()
    {
        _world.Create(new Transform());
        var transformQuery = WithHash(new QueryDescription().WithAll<Transform>(), 42);
        var rotationQuery = WithHash(new QueryDescription().WithAll<Rotation>(), 42);

        That(_world.CountEntities(in transformQuery), Is.EqualTo(1));
        That(_world.CountEntities(in rotationQuery), Is.EqualTo(0));
    }

    [Test]
    public void CreateIgnoresImpostor()
    {
        var impostor = _world.GetArchetype(_world.Create(new Transform()));
        RegisterImpostor(impostor, new Signature(typeof(Rotation)));

        var entity = _world.Create(new Rotation());

        That(_world.GetArchetype(entity), Is.Not.SameAs(impostor));
        That(_world.Has<Rotation>(entity), Is.True);
        That(_world.Has<Transform>(entity), Is.False);
    }

    [Test]
    public void AddIgnoresImpostor()
    {
        var entity = _world.Create(new Transform());
        var impostor = _world.GetArchetype(_world.Create(new Rotation(), new Ai()));
        RegisterImpostor(impostor, new Signature(typeof(Transform), typeof(Rotation)));

        _world.Add(entity, new Rotation());

        That(_world.GetArchetype(entity), Is.Not.SameAs(impostor));
        That(_world.Has<Transform>(entity), Is.True);
        That(_world.Has<Rotation>(entity), Is.True);
        That(_world.Has<Ai>(entity), Is.False);
    }

    [Test]
    public void AddRangeIgnoresImpostor()
    {
        var entity = _world.Create(new Transform());
        var impostor = _world.GetArchetype(_world.Create(new Rotation(), new Ai()));
        RegisterImpostor(impostor, new Signature(typeof(Transform), typeof(Rotation)));

        _world.AddRange(entity, new object[] { new Rotation() }.AsSpan());

        That(_world.GetArchetype(entity), Is.Not.SameAs(impostor));
        That(_world.Has<Transform>(entity), Is.True);
        That(_world.Has<Rotation>(entity), Is.True);
        That(_world.Has<Ai>(entity), Is.False);
    }

    [Test]
    public void RemoveIgnoresImpostor()
    {
        var entity = _world.Create(new Transform(), new Rotation());
        var impostor = _world.GetArchetype(_world.Create(new Ai()));
        RegisterImpostor(impostor, new Signature(typeof(Transform)));

        _world.Remove<Rotation>(entity);

        That(_world.GetArchetype(entity), Is.Not.SameAs(impostor));
        That(_world.Has<Transform>(entity), Is.True);
        That(_world.Has<Ai>(entity), Is.False);
    }

    [Test]
    public void DestroyArchetypeKeepsSharedHash()
    {
        var impostor = _world.GetArchetype(_world.Create(new Transform()));
        var signature = new Signature(typeof(Rotation));
        RegisterImpostor(impostor, signature);

        var entity = _world.Create(new Rotation());
        var archetype = _world.GetArchetype(entity);
        _world.Destroy(entity);
        _world.DestroyArchetype(archetype);

        That(_world.TryGetArchetype(signature, out _), Is.False);
        That(_world.TryGetArchetype(new Signature(typeof(Transform)), out var transformArchetype), Is.True);
        That(transformArchetype, Is.SameAs(impostor));
    }
}
