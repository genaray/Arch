using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

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

        That(_world.Size, Is.EqualTo(size + 1));
        True(_world.IsAlive(in entity));
    }

    [Test]
    public void Destroy()
    {
        var worldSizeBefore = _world.Size;
        var entity = _world.Create(_entityGroup);
        var worldSizeAfter = _world.Size;
        _world.Destroy(in entity);

        That(_world.Size, Is.EqualTo(worldSizeBefore));
        Less(worldSizeBefore, worldSizeAfter);
        False(_world.IsAlive(in entity));
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

        That(_world.Size, Is.EqualTo(0));
        That(_world.Archetypes[0].Size, Is.EqualTo(1));
        That(_world.Archetypes[1].Size, Is.EqualTo(1));
    }

    [Test]
    public void Recycle()
    {
        using var localWorld = World.Create();

        // Destroy & create new entity to see if it was recycled
        var entity = localWorld.Create(_entityGroup);
        localWorld.Destroy(in entity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var newEntity = localWorld.Create(_entityGroup);

        That(recycledEntity.Id, Is.EqualTo(entity.Id));           // Id was recycled
        That(localWorld.Version(recycledEntity), Is.EqualTo(1));  // Version was increased
        That(newEntity.Id, Is.Not.EqualTo(recycledEntity.Id));
    }

    [Test]
    public void Reference()
    {
        using var localWorld = World.Create();

        // Destroy & create new entity to see if it was recycled
        var entity = localWorld.Create(_entityGroup);
        var reference = localWorld.Reference(entity);

        var otherEntity = localWorld.Create(_entityGroup);
        var otherReference = localWorld.Reference(otherEntity);

        // Check if references point to same entity
        That(reference == otherReference, Is.EqualTo(false));
        That(reference != otherReference, Is.EqualTo(true));


        // Destroy entity and create a new one which will have the same ID but different version
        localWorld.Destroy(otherEntity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var recycledReference = localWorld.Reference(recycledEntity);

        // Check if reference takes care of wrong version
        That(recycledReference != otherReference, Is.EqualTo(true));
        That(otherReference.IsAlive, Is.EqualTo(false));

        // Entity reference null is NOT alive.
        EntityReference cons = new EntityReference{};
        EntityReference refs = EntityReference.Null;
        That(refs.IsAlive, Is.EqualTo(false));
        That(cons.IsAlive, Is.EqualTo(false));
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

        That(after, Is.EqualTo(before));
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

        Greater(_world.Size, beforeSize);
        That(_world.Size, Is.EqualTo(beforeSize + 10000));
        That(_world.Capacity, Is.EqualTo(beforeCapacity + 10000));
    }

    [Test]
    public void MultipleArchetypesTest()
    {
        var archTypes1 = new ComponentType[] { typeof(Transform) };
        var archTypes2 = new ComponentType[] { typeof(Transform) };

        var entity1 = _world.Create(archTypes1);
        var entity2 = _world.Create(archTypes2);

        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity1)));
    }

    [Test]
    public void GetEntitesTest()
    {
        // Query
        var archTypes = new ComponentType[] { typeof(Transform) };
        var query = new QueryDescription { All = archTypes };

        // World
        using var world = World.Create();
        var entity = world.Create(archTypes);

        // Get entities
        var entites = new List<Entity>();
        world.GetEntities(query, entites);

        That(entites.Count, Is.EqualTo(1));

        // Destroy the one entity
        entites.Clear();
        world.Destroy(entity);
        world.GetEntities(query, entites);

        That(entites.Count, Is.EqualTo(0));
    }

    [Test]
    public void CountEntitiesTest()
    {
        // Query
        var archTypes = new ComponentType[] { typeof(Transform) };
        var query = new QueryDescription { All = archTypes };

        // World
        using var world = World.Create();

        for (int i = 0; i < 2222; i++)
            world.Create(archTypes);

        // count entities
        var count = world.CountEntities(query);

        That(count, Is.EqualTo(2222));
    }
}

// Destroy, Set, Add, Remove operations based on querydescriptions
public partial class WorldTest
{

    [Test]
    public void SetByQueryDescription()
    {

        var queryDesc = new QueryDescription().WithAll<Transform>();
        _world.Set(in queryDesc, new Transform{ X = 100, Y = 100});
        _world.Query(in queryDesc, (ref Transform transform) =>
        {
            That(transform.X, Is.EqualTo(100));
            That(transform.Y, Is.EqualTo(100));
        });
    }

    [Test]
    public void DestroyByQueryDescription()
    {

        var queryDesc = new QueryDescription().WithAll<Transform>();
        using var world = World.Create();
        for (int index = 0; index < 1000; index++)
        {
            world.Create(_entityGroup);
        }

        world.Destroy(in queryDesc);
        That(world.Size, Is.EqualTo(0));
    }

    [Test]
    public void AddByQueryDescription()
    {
        var withAIQueryDesc = new QueryDescription().WithAll<Ai>();
        var withoutAIQueryDesc = new QueryDescription().WithNone<Ai>();

        using var world = World.Create();
        for (int index = 0; index < 1000; index++)
        {
            world.Create(_entityGroup);
        }

        world.Add<Ai>(in withoutAIQueryDesc);
        That(world.CountEntities(in withAIQueryDesc), Is.EqualTo(1000));
        That(world.CountEntities(in withoutAIQueryDesc), Is.EqualTo(0));
    }

    [Test]
    public void RemoveByQueryDescription()
    {
        var withAIQueryDesc = new QueryDescription().WithAll<Ai>();
        var withoutAIQueryDesc = new QueryDescription().WithNone<Ai>();

        using var world = World.Create();
        for (int index = 0; index < 1000; index++)
        {
            world.Create(_entityAiGroup);
        }

        world.Remove<Ai>(in withAIQueryDesc);
        That(world.CountEntities(in withAIQueryDesc), Is.EqualTo(0));
        That(world.CountEntities(in withoutAIQueryDesc), Is.EqualTo(1000));
    }
}


// Get, Set, Has, Remove, Add
public partial class WorldTest
{

    [Test]
    public void SetGetAndHas()
    {

        var entity = _world.Create(_entityGroup);
        True(_world.Has<Transform>(in entity));

        _world.Set(entity, new Transform { X = 10, Y = 10 });
        ref var transform = ref _world.Get<Transform>(in entity);

        That(transform.X, Is.EqualTo(10));
        That(transform.Y, Is.EqualTo(10));
    }

    [Test]
    public void Remove()
    {

        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Remove<Transform>(in entity);
        _world.Remove<Transform>(in entity2);

        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(_world.GetArchetype(in entity).Size, Is.EqualTo(1));
        That(_world.GetArchetype(in entity).Chunks[0].Size, Is.EqualTo(2));
    }

    [Test]
    public void Add()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Add<Ai>(in entity);
        _world.Add<Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(in entity)));
    }
}


// Get, Set, Has, Remove, Add non generic
public partial class WorldTest
{

    [Test]
    public void SetGetAndHas_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        True(_world.Has(in entity, typeof(Transform)));

        _world.Set(entity, (object)new Transform { X = 10, Y = 10 });
        var transform = (Transform)_world.Get(in entity, typeof(Transform));

        That(transform.X, Is.EqualTo(10));
        That(transform.Y, Is.EqualTo(10));
    }

    [Test]
    public void Remove_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.RemoveRange(in entity, typeof(Transform));
        _world.RemoveRange(in entity2, typeof(Transform));

        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(_world.GetArchetype(in entity).Size, Is.EqualTo(1));
        That(_world.GetArchetype(in entity).Chunks[0].Size, Is.EqualTo(2));
    }

    [Test]
    public void Add_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.AddRange(in entity, new Ai());
        _world.AddRange(in entity2, new Ai());

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(in entity)));
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

        That(_world.Size, Is.EqualTo(size + 1));
        True(_world.IsAlive(in entity));
    }

    [Test]
    public void GeneratedSetGetAndHas()
    {
        var entity = _world.Create(new Transform { X = 10, Y = 10 }, new Rotation { X = 10, Y = 10 });
        True(_world.Has<Transform, Rotation>(in entity));

        _world.Set(entity, new Transform { X = 20, Y = 20 }, new Rotation { X = 20, Y = 20 });
        var references = _world.Get<Transform, Rotation>(in entity);
        That(references.t0.X, Is.EqualTo(20));
        That(references.t0.Y, Is.EqualTo(20));
        That(references.t1.X, Is.EqualTo(20));
        That(references.t1.Y, Is.EqualTo(20));
    }

    [Test]
    public void GeneratedRemove()
    {

        var entity = _world.Create(new Transform(), new Rotation(), new Ai());
        var entity2 = _world.Create(new Transform(), new Rotation(), new Ai());
        _world.Remove<Rotation, Ai>(in entity);
        _world.Remove<Rotation, Ai>(in entity2);

        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(_world.GetArchetype(in entity).Size, Is.EqualTo(1));
        That(_world.GetArchetype(in entity).Chunks[0].Size, Is.EqualTo(2));
    }

    [Test]
    public void GeneratedAdd()
    {
        var entity = _world.Create<Transform>();
        var entity2 = _world.Create<Transform>();
        _world.Add<Rotation, Ai>(in entity);
        _world.Add<Rotation, Ai>(in entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(in entity2), Is.EqualTo(_world.GetArchetype(in entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(in entity)));
    }
}
