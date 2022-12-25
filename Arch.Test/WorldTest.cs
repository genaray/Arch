using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

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
            _world.Create(_entityGroup);

        for (var index = 0; index < 10000; index++)
            _world.Create(_entityAiGroup);
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

        Assert.AreEqual(size + 1, _world.Size);
        Assert.True(_world.IsAlive(in entity));
    }

    [Test]
    public void Destroy()
    {
        var worldSizeBefore = _world.Size;
        var entity = _world.Create(_entityGroup);
        var worldSizeAfter = _world.Size;
        _world.Destroy(in entity);

        Assert.AreEqual(worldSizeBefore, _world.Size);
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

        Assert.AreEqual(0, _world.Size);
        Assert.AreEqual(1, _world.Archetypes[0].Size);
        Assert.AreEqual(1, _world.Archetypes[1].Size);
    }
    
    [Test]
    public void RecycleId()
    {
        var localWorld = World.Create();

        var entity = localWorld.Create(_entityGroup);
        localWorld.Destroy(in entity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var newEntity = localWorld.Create(_entityGroup);

        Assert.AreEqual(entity.Id, recycledEntity.Id);
        Assert.AreNotEqual(recycledEntity.Id, newEntity.Id);

        World.Destroy(localWorld);
    }

    [Test]
    public void CapacityTest()
    {
        // Add
        var before = _world.Size;
        for (var index = 0; index < 500; index++)
            _world.Create(_entityGroup);

        // Remove
        for (var index = 0; index < 500; index++)
            _world.Destroy(new Entity(index, _world.Id));
        var after = _world.Size;

        Assert.AreEqual(before, after);
    }

    [Test]
    public void Reserve()
    {
        var beforeSize = _world.Size;
        var beforeCapacity = _world.Capacity;

        _world.Reserve(_entityGroup, 10000);
        for (var index = 0; index < 10000; index++)
            _world.Create(_entityGroup);

        Assert.Greater(_world.Size, beforeSize);
        Assert.AreEqual(beforeSize + 10000, _world.Size);
        Assert.AreEqual(beforeCapacity + 10000, _world.Capacity);
    }
    

    [Test]
    public void MultipleArchetypesTest()
    {
        var archTypes1 = new ComponentType[] { typeof(Transform) };
        var archTypes2 = new ComponentType[] { typeof(Transform) };
  
        var entity1 = _world.Create(archTypes1);
        var entity2 = _world.Create(archTypes2);
        
        Assert.AreEqual(_world.GetArchetype(in entity1), _world.GetArchetype(in entity2));
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
        
        _world.Set(entity, new Transform{ X = 10, Y = 10});
        ref var transform = ref _world.Get<Transform>(in entity);
        
        Assert.AreEqual(10, transform.X);
        Assert.AreEqual(10, transform.Y);
    }
    
    [Test]
    public void Remove()
    {

        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Remove<Transform>(in entity);
        _world.Remove<Transform>(in entity2);
        
        Assert.AreEqual(_world.GetArchetype(in entity), _world.GetArchetype(in entity2));
        Assert.AreEqual(1,_world.GetArchetype(in entity).Size);
        Assert.AreEqual(2,_world.GetArchetype(in entity).Chunks[0].Size);
    }
    
    [Test]
    public void Add()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Add<Ai>(in entity);
        _world.Add<Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        Assert.AreEqual(_world.GetArchetype(in entity), _world.GetArchetype(in entity2));
        Assert.AreEqual(_world.GetArchetype(in entity), arch);
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

        Assert.AreEqual(size + 1, _world.Size);
        Assert.True(_world.IsAlive(in entity));
    }
    
    [Test]
    public void GeneratedSetGetAndHas()
    {
        var entity = _world.Create(new Transform{ X = 10, Y = 10}, new Rotation{ X = 10, Y = 10});
        Assert.True(_world.Has<Transform, Rotation>(in entity));
        
        _world.Set(entity, new Transform{ X = 20, Y = 20}, new Rotation{ X = 20, Y = 20});
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
        
        Assert.AreEqual(_world.GetArchetype(in entity), _world.GetArchetype(in entity2));
        Assert.AreEqual(1, _world.GetArchetype(in entity).Size);
        Assert.AreEqual(2, _world.GetArchetype(in entity).Chunks[0].Size);
    }
    
    [Test]
    public void GeneratedAdd()
    {
        var entity = _world.Create<Transform>();
        var entity2 = _world.Create<Transform>();
        _world.Add<Rotation, Ai>(in entity);
        _world.Add<Rotation, Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        Assert.AreEqual(_world.GetArchetype(in entity), _world.GetArchetype(in entity2));
        Assert.AreEqual(_world.GetArchetype(in entity), arch);
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