using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class WorldTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        _world = World.Create();

        for (var index = 0; index < 10000; index++)
            _world.Create(_entityGroup);

        for (var index = 0; index < 10000; index++)
            _world.Create(_entityAiGroup);
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    private World _world;

    private readonly Type[] _entityGroup = { typeof(Transform), typeof(Rotation) };
    private readonly Type[] _entityAiGroup = { typeof(Transform), typeof(Rotation), typeof(Ai) };

    private QueryDescription _withoutAiQuery = new() { All = new[] { typeof(Transform) }, Any = new[] { typeof(Rotation) }, None = new[] { typeof(Ai) } };

    private QueryDescription _withAiQuery = new() { All = new[] { typeof(Transform), typeof(Rotation) }, Any = new[] { typeof(Ai) } };

    [Test]
    public void Create()
    {
        var size = _world.Size;
        var entity = _world.Create(_entityGroup);

        Assert.AreEqual(size + 1, _world.Size);
        Assert.True(entity.IsAlive());
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
        Assert.False(entity.IsAlive());
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
            _world.Destroy(new Entity(index, _world.Id, 0));
        var after = _world.Size;

        Assert.AreEqual(before, after);
    }

    [Test]
    public void RecycleId()
    {
        var localWorld = World.Create();

        var entity = localWorld.Create(_entityGroup);
        localWorld.Destroy(in entity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var newEntity = localWorld.Create(_entityGroup);

        Assert.AreEqual(entity.EntityId, recycledEntity.EntityId);
        Assert.AreNotEqual(recycledEntity.EntityId, newEntity.EntityId);

        World.Destroy(localWorld);
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
    public void DestroyAll()
    {
        var query = new QueryDescription { All = new[] { typeof(Transform) } };

        var entities = new List<Entity>();
        _world.GetEntities(query, entities);

        for (var i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];
            _world.Destroy(in entity);
        }

        Assert.AreEqual(0, _world.Size);
        Assert.AreEqual(0, _world.Archetypes[0].Size);
        Assert.AreEqual(0, _world.Archetypes[1].Size);
    }

    [Test]
    public void MultipleArchetypesTest()
    {
        var archTypes1 = new[] { typeof(Transform) };
        var archTypes2 = new[] { typeof(Transform) };

        var entity1 = _world.Create(archTypes1);
        var entity2 = _world.Create(archTypes2);

        Assert.AreEqual(entity1.GetArchetype(), entity2.GetArchetype());
    }
    
    [Test]
    public void Remove()
    {

        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Remove<Transform>(in entity);
        _world.Remove<Transform>(in entity2);
        
        Assert.AreEqual(entity.GetArchetype(), entity2.GetArchetype());
        Assert.AreEqual(1, entity.GetArchetype().Size);
        Assert.AreEqual(2, entity.GetArchetype().Chunks[0].Size);
    }
    
    [Test]
    public void Add()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Add<Ai>(in entity);
        _world.Add<Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        Assert.AreEqual(entity.GetArchetype(), entity2.GetArchetype());
        Assert.AreEqual(entity.GetArchetype(), arch);
    }
}