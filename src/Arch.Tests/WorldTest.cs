using System.Text;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Dangerous;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="WorldTest"/> class
///     tests basic <see cref="World"/> operations.
/// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> is being recycled correctly.
    /// </summary>
    [Test]
    public void WorldRecycle()
    {
        var firstWorld = World.Create();
        World.Destroy(firstWorld);

        var secondWorld = World.Create();
        That(secondWorld.Id, Is.EqualTo(firstWorld.Id));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> creates <see cref="Entity"/> correctly.
    /// </summary>
    [Test]
    public void Create()
    {
        var size = _world.Size;
        var entity = _world.Create(_entityGroup);

        That(_world.Size, Is.EqualTo(size + 1));
        True(_world.IsAlive(entity));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> destroys <see cref="Entity"/> correctly.
    /// </summary>
    [Test]
    public void Destroy()
    {
        var worldSizeBefore = _world.Size;
        var entity = _world.Create(_entityGroup);
        var worldSizeAfter = _world.Size;
        _world.Destroy(entity);

        That(_world.Size, Is.EqualTo(worldSizeBefore));
        Less(worldSizeBefore, worldSizeAfter);
        False(_world.IsAlive(entity));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> is capable of destroying all <see cref="Entity"/> correctly.
    /// </summary>
    [Test]
    public void DestroyAll()
    {
        var query = new QueryDescription { All = new ComponentType[] { typeof(Transform) } };

        var entities = new List<Entity>();
        _world.GetEntities(query, entities);

        for (var i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];
            _world.Destroy(entity);
        }

        That(_world.Size, Is.EqualTo(0));
        That(_world.Archetypes[0].ChunkCount, Is.EqualTo(1));
        That(_world.Archetypes[1].ChunkCount, Is.EqualTo(1));
    }

    /// <summary>
    ///     Tests an edge case where entities are being bulk moved between archetypes and destroyed at the same time.
    /// </summary>
    [Test]
    public void DestroyEdgeCase()
    {
        var entitiesToChangeColor = new QueryDescription().WithAll<Transform>();
        var entities = new List<Entity>();
        for (var i = 0; i < 1000; i++)
        {
            var ent = _world.Create(_entityGroup);
            entities.Add(ent);
        }

        for (var i = 8; i < entities.Count; i++)
        {
            var ent = entities[i];
            if (i % 3 != 0)
            {
                continue;
            }

            // A demonstration of bulk adding and removing components.
            _world.Add(in entitiesToChangeColor, 1);
            _world.Remove<int>(in entitiesToChangeColor);

            _world.Destroy(ent);
        }
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> recycles destroyed <see cref="Entity"/> correctly.
    /// </summary>
    [Test]
    public void Recycle()
    {
        using var localWorld = World.Create();

        // Destroy & create new entity to see if it was recycled
        var entity = localWorld.Create(_entityGroup);
        localWorld.Destroy(entity);
        var recycledEntity = localWorld.Create(_entityGroup);
        var newEntity = localWorld.Create(_entityGroup);

        That(recycledEntity.Id, Is.EqualTo(entity.Id));           // Id was recycled
        That(localWorld.Version(recycledEntity), Is.EqualTo(2));  // Version was increased
        That(newEntity.Id, Is.Not.EqualTo(recycledEntity.Id));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> references <see cref="Entity"/> with <see cref="EntityReference"/> correctly.
    /// </summary>
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
#if PURE_ECS
        That(otherReference.IsAlive(localWorld), Is.EqualTo(false));
#else
        That(otherReference.IsAlive(), Is.EqualTo(false));
#endif

        // Entity reference null is NOT alive.
        EntityReference cons = new EntityReference{};
        EntityReference refs = EntityReference.Null;

#if PURE_ECS
        That(refs.IsAlive(localWorld), Is.EqualTo(false));
        That(cons.IsAlive(localWorld), Is.EqualTo(false));
#else
        That(refs.IsAlive(), Is.EqualTo(false));
        That(cons.IsAlive(), Is.EqualTo(false));
#endif
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> adjusts its capacity when creating <see cref="Entity"/>s correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> reserves memory for <see cref="Entity"/>s correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> trims its content correctly.
    /// </summary>
    [Test]
    public void TrimExcess()
    {
        // Fill world
        var amount = 10000;
        using var world = World.Create();
        for (int index = 0; index < amount; index++)
        {
            world.Create<HeavyComponent>();
        }

        // Destroy all but one
        var counter = 0;
        var query = new QueryDescription().WithAll<HeavyComponent>();
        world.Query(in query, (Entity entity) =>
        {
            if (counter < amount - 1)
            {
                world.Destroy(entity);
            }
            counter++;
        });

        // Trim
        world.TrimExcess();

        var archetype = world.Archetypes[0];
        That(world.Size, Is.EqualTo(1));
        That(world.Capacity, Is.EqualTo(archetype.EntitiesPerChunk));
        That(archetype.ChunkCount, Is.EqualTo(1));
        That(archetype.ChunkCapacity, Is.EqualTo(1));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> trims its archetypes correctly and removes them when empty.
    /// </summary>
    [Test]
    public void TrimExcessEmptyArchetypes()
    {
        // Fill world
        var amount = 10000;
        using var world = World.Create();
        for (int index = 0; index < amount; index++)
        {
            world.Create<int>();
            world.Create<byte>();
        }

        // Destroy all of the world entities
        var query = new QueryDescription().WithAll<int>();
        world.Destroy(query);

        // Trim
        world.TrimExcess();

        var archetype = world.Archetypes[0];
        That(world.Archetypes.Count, Is.EqualTo(1));
        That(world.Capacity, Is.EqualTo(archetype.ChunkCount * archetype.EntitiesPerChunk));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> clears itself correctly.
    /// </summary>
    [Test]
    public void Clear()
    {
        // Fill world
        var amount = 1000;
        using var world = World.Create();

        for (int index = 0; index < amount; index++)
        {
            world.Create<int>();
        }

        // Trim
        world.Clear();

        That(world.Size, Is.EqualTo(0));
        That(world.Capacity, Is.EqualTo(0));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> creates different <see cref="Entity"/> with different <see cref="Archetype"/>s correctly.
    /// </summary>
    [Test]
    public void MultipleArchetypesTest()
    {
        var archTypes1 = new ComponentType[] { typeof(Transform) };
        var archTypes2 = new ComponentType[] { typeof(Transform) };

        var entity1 = _world.Create(archTypes1);
        var entity2 = _world.Create(archTypes2);

        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity1)));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> gets <see cref="Entity"/>s correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> counts <see cref="Entity"/> correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> bulk set by using a <see cref="QueryDescription"/> works correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> bulk destroy by using a <see cref="QueryDescription"/> works correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> bulk add by using a <see cref="QueryDescription"/> works correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/> bulk add by using a <see cref="QueryDescription"/> works correctly and sets the value correctly too.
    /// </summary>
    [Test]
    public void AddByQueryDescriptionValue()
    {
        var withIntQueryDesc = new QueryDescription().WithAll<int>();
        var withoutIntQueryDesc = new QueryDescription().WithNone<int>();

        using var world = World.Create();
        for (int index = 0; index < 1000; index++)
        {
            world.Create(_entityGroup);
        }

        // Create entities with int
        for (int index = 0; index < 1000; index++)
        {
            var entity = world.Create(_entityGroup);
            world.Add(entity,10);
        }

        // Add int to all entities without int
        world.Add(in withoutIntQueryDesc, 100);

        var previousCounter = 0;
        var counter = 0;
        world.Query(in withIntQueryDesc, (ref int i) =>
        {
            if (i == 10) previousCounter++;
            if (i == 100) counter++;
        });

        That(world.CountEntities(in withIntQueryDesc), Is.EqualTo(2000));
        That(world.CountEntities(in withoutIntQueryDesc), Is.EqualTo(0));
        That(previousCounter, Is.EqualTo(1000));
        That(counter, Is.EqualTo(1000));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/> bulk remove by using a <see cref="QueryDescription"/> works correctly.
    /// </summary>
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

    /// <summary>
    ///     Checks if the <see cref="World"/>s set get and has operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void SetGetAndHas()
    {

        var entity = _world.Create(_entityGroup);
        True(_world.Has<Transform>(entity));

        _world.Set(entity, new Transform { X = 10, Y = 10 });
        ref var transform = ref _world.Get<Transform>(entity);

        That(transform.X, Is.EqualTo(10));
        That(transform.Y, Is.EqualTo(10));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s remove operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void Remove()
    {

        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Remove<Transform>(entity);
        _world.Remove<Transform>(entity2);

        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(_world.GetArchetype(entity).ChunkCount, Is.EqualTo(1));
        That(_world.GetArchetype(entity).Chunks[0].Size, Is.EqualTo(2));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s add operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void Add()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.Add<Ai>(entity);
        _world.Add<Ai>(entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(entity)));
    }
}


// Get, Set, Has, Remove, Add non generic
public partial class WorldTest
{

    /// <summary>
    ///     Checks if the <see cref="World"/>s non generic set get and has operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void SetGetAndHas_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        True(_world.Has(entity, typeof(Transform)));

        _world.Set(entity, (object)new Transform { X = 10, Y = 10 });
        var transform = (Transform)_world.Get(entity, typeof(Transform));

        That(transform.X, Is.EqualTo(10));
        That(transform.Y, Is.EqualTo(10));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s non generic remove operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void Remove_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.RemoveRange(entity, typeof(Transform));
        _world.RemoveRange(entity2, typeof(Transform));

        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(_world.GetArchetype(entity).ChunkCount, Is.EqualTo(1));
        That(_world.GetArchetype(entity).Chunks[0].Size, Is.EqualTo(2));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s non generic add operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void Add_NonGeneric()
    {
        var entity = _world.Create(_entityGroup);
        var entity2 = _world.Create(_entityGroup);
        _world.AddRange(entity, new Ai());
        _world.AddRange(entity2, new Ai());

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(entity)));
    }
}

/// <summary>
/// Testing generated methods
/// </summary>
public partial class WorldTest
{

    /// <summary>
    ///     Checks if the <see cref="World"/>s source generated create operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void GeneratedCreate()
    {
        var size = _world.Size;
        var entity = _world.Create(new Transform(), new Rotation());

        That(_world.Size, Is.EqualTo(size + 1));
        True(_world.IsAlive(entity));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s source generated set get has operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void GeneratedSetGetAndHas()
    {
        var entity = _world.Create(new Transform { X = 10, Y = 10 }, new Rotation { X = 10, Y = 10 });
        True(_world.Has<Transform, Rotation>(entity));

        _world.Set(entity, new Transform { X = 20, Y = 20 }, new Rotation { X = 20, Y = 20 });
        var references = _world.Get<Transform, Rotation>(entity);
        That(references.t0.X, Is.EqualTo(20));
        That(references.t0.Y, Is.EqualTo(20));
        That(references.t1.X, Is.EqualTo(20));
        That(references.t1.Y, Is.EqualTo(20));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s source generated remove operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void GeneratedRemove()
    {

        var entity = _world.Create(new Transform(), new Rotation(), new Ai());
        var entity2 = _world.Create(new Transform(), new Rotation(), new Ai());
        _world.Remove<Rotation, Ai>(entity);
        _world.Remove<Rotation, Ai>(entity2);

        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(_world.GetArchetype(entity).ChunkCount, Is.EqualTo(1));
        That(_world.GetArchetype(entity).Chunks[0].Size, Is.EqualTo(2));
    }

    /// <summary>
    ///     Checks if the <see cref="World"/>s source generated add operations on <see cref="Entity"/>s works correctly.
    /// </summary>
    [Test]
    public void GeneratedAdd()
    {
        var entity = _world.Create<Transform>();
        var entity2 = _world.Create<Transform>();
        _world.Add<Rotation, Ai>(entity);
        _world.Add<Rotation, Ai>(entity2);

        _world.TryGetArchetype(_entityAiGroup, out var arch);
        That(_world.GetArchetype(entity2), Is.EqualTo(_world.GetArchetype(entity)));
        That(arch, Is.EqualTo(_world.GetArchetype(entity)));
    }
}
