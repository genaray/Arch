using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Tests;

[TestFixture]
public partial class WorldTest
{
    private World _world;

    private readonly ComponentType[] _entityGroup = { typeof(Transform), typeof(Rotation) };
    private readonly ComponentType[] _entityAiGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };

    [SetUp]
    public void Setup()
    {
        _world = World.Create();

        for (var index = 0; index < 10000; index++)
        {
            _world.Create(_entityGroup);
        }

        for (var index = 0; index < 10000; index++)
        {
            _world.Create(_entityAiGroup);
        }
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    [Test]
    public void Create()
    {
        var size = _world.Size;
        var entity = _world.Create(_entityGroup);

        Assert.That(_world.Size, Is.EqualTo(size + 1));
        Assert.True(_world.IsAlive(in entity));
    }

    [Test]
    public void Destroy()
    {
        var worldSizeBefore = _world.Size;
        var entity = _world.Create(_entityGroup);
        var worldSizeAfter = _world.Size;
        _world.Destroy(in entity);

        Assert.That(_world.Size, Is.EqualTo(worldSizeBefore));
        Assert.Less(worldSizeBefore, worldSizeAfter);
        Assert.False(_world.IsAlive(in entity));
    }

    [Test]
    public void DestroyAll()
    {
        var query = new QueryDescription { All = new ComponentType[] { typeof(Transform) } };

        var entities = new List<Entity>();
        _world.GetEntities(query, entities);

        for (var i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];
            _world.Destroy(in entity);
        }

        Assert.That(_world.Size, Is.EqualTo(0));
        Assert.That(_world.Archetypes[0].Size, Is.EqualTo(1));
        Assert.That(_world.Archetypes[1].Size, Is.EqualTo(1));
    }

    [Test]
    public void RecycleId()
    {
        var localWorld = World.Create();

        var entity = localWorld.Create(_entityGroup);
        localWorld.Destroy(in entity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var newEntity = localWorld.Create(_entityGroup);

        Assert.That(recycledEntity.Id, Is.EqualTo(entity.Id));
        Assert.That(newEntity.Id, Is.Not.EqualTo(recycledEntity.Id));

        World.Destroy(localWorld);
    }

    [Test]
    public void CapacityTest()
    {
        // Add
        var before = _world.Size;
        for (var index = 0; index < 500; index++)
        {
            _world.Create(_entityGroup);
        }

        // Remove
        for (var index = 0; index < 500; index++)
        {
            _world.Destroy(new Entity(index, _world.Id));
        }

        var after = _world.Size;

        Assert.That(after, Is.EqualTo(before));
    }

    [Test]
    public void Reserve()
    {
        var beforeSize = _world.Size;
        var beforeCapacity = _world.Capacity;

        _world.Reserve(_entityGroup, 10000);
        for (var index = 0; index < 10000; index++)
        {
            _world.Create(_entityGroup);
        }

        Assert.Greater(_world.Size, beforeSize);
        Assert.That(_world.Size, Is.EqualTo(beforeSize + 10000));
        Assert.That(_world.Capacity, Is.EqualTo(beforeCapacity + 10000));
    }

    [Test]
    public void MultipleArchetypesTest()
    {
        var archTypes1 = new ComponentType[] { typeof(Transform) };
        var archTypes2 = new ComponentType[] { typeof(Transform) };

        var entity1 = _world.Create(archTypes1);
        var entity2 = _world.Create(archTypes2);

        Assert.That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity1)));
    }

    [Test]
    public void GetEntitesTest()
    {
        var world = World.Create();

        var archTypes = new ComponentType[] { typeof(Transform) };
        var query = new QueryDescription { All = archTypes };

        var entity = world.Create(archTypes);

        var entites = new List<Entity>();
        world.GetEntities(query, entites);

        Assert.That(entites.Count, Is.EqualTo(1));

        entites.Clear();
        world.Destroy(entity);
        world.GetEntities(query, entites);

        Assert.That(entites.Count, Is.EqualTo(0));
        World.Destroy(world);
    }
}

// Get, Set, Has, Remove, Add
public partial class WorldTest
{

    [Test]
    public void SetGetAndHas()
    {

        var entity = _world.Create(_entityGroup);
        Assert.True(_world.Has<Transform>(in entity));

        _world.Set(entity, new Transform { X = 10, Y = 10 });
        ref var transform = ref _world.Get<Transform>(in entity);

        Assert.That(transform.X, Is.EqualTo(10));
        Assert.That(transform.Y, Is.EqualTo(10));
    }

    [Test]
    public void Remove()
    {

        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Remove<Transform>(in entity);
        _world.Remove<Transform>(in entity2);

        Assert.That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        Assert.That(_world.GetArchetype(in entity).Size, Is.EqualTo(1));
        Assert.That(_world.GetArchetype(in entity).Chunks[0].Size, Is.EqualTo(2));
    }

    [Test]
    public void Add()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Add<Ai>(in entity);
        _world.Add<Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        Assert.That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        Assert.That(arch, Is.EqualTo(_world.GetArchetype(in entity)));
    }
}

/// <summary>
/// Testing generated methods 
/// </summary>
public partial class WorldTest
{

    [Test]
    public void GeneratedCreate()
    {
        var size = _world.Size;
        var entity = _world.Create(new Transform(), new Rotation());

        Assert.That(_world.Size, Is.EqualTo(size + 1));
        Assert.True(_world.IsAlive(in entity));
    }

    [Test]
    public void GeneratedSetGetAndHas()
    {
        var entity = _world.Create(new Transform { X = 10, Y = 10 }, new Rotation { X = 10, Y = 10 });
        Assert.True(_world.Has<Transform, Rotation>(in entity));

        _world.Set(entity, new Transform { X = 20, Y = 20 }, new Rotation { X = 20, Y = 20 });
        var references = _world.Get<Transform, Rotation>(in entity);

        Assert.AreEqual(20, references.t0.X);
        Assert.AreEqual(20, references.t0.Y);
        Assert.AreEqual(20, references.t1.X);
        Assert.AreEqual(20, references.t1.Y);
    }

    [Test]
    public void GeneratedRemove()
    {

        var entity = _world.Create(new Transform(), new Rotation(), new Ai());
        var entity2 = _world.Create(new Transform(), new Rotation(), new Ai());
        _world.Remove<Rotation, Ai>(in entity);
        _world.Remove<Rotation, Ai>(in entity2);

        Assert.That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        Assert.That(_world.GetArchetype(in entity).Size, Is.EqualTo(1));
        Assert.That(_world.GetArchetype(in entity).Chunks[0].Size, Is.EqualTo(2));
    }

    [Test]
    public void GeneratedAdd()
    {
        var entity = _world.Create<Transform>();
        var entity2 = _world.Create<Transform>();
        _world.Add<Rotation, Ai>(in entity);
        _world.Add<Rotation, Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        Assert.That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        Assert.That(arch, Is.EqualTo(_world.GetArchetype(in entity)));
    }
}

// Tests if operations during query performed sucessfull
public partial class WorldTest
{

    [Test]
    public void QueryAndCreate()
    {
    }
}
