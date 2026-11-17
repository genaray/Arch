using System.Text;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     Test class for reproducing the SparseJaggedArray memory corruption bug.
///     This bug occurs during high-load operations with frequent archetype transitions.
/// </summary>
[TestFixture]
public sealed class SparseJaggedArrayBugTest
{
    /// <summary>
    ///     Reproduces the System.AccessViolationException that occurs when adding components
    ///     with reference types (strings) during high-load archetype transitions.
    ///     This test should fail with the current implementation due to memory corruption.
    /// </summary>
    [Test]
    public void HighLoad_RenderComponent_Addition_Reproduces_Memory_Corruption_Bug()
    {
        // Reproduction steps from bug report:
        // Create a test that adds 10,000+ entities with multiple components
        // Include components with reference types (strings) like this struct:
        // Add components in a way that triggers archetype transitions

        const int entityCount = 10000;

        using var world = World.Create();

        // Create entities with base components first
        var entities = new Entity[entityCount];
        for (int i = 0; i < entityCount; i++)
        {
            entities[i] = world.Create(new Transform { X = i, Y = i });
        }

        // Add render components (this triggers archetype transitions and the crash)
        // This is where the SparseJaggedArray.get_Capacity() crash occurs
        for (int i = 0; i < entityCount; i++)
        {
            var meshId = $"mesh_{i}";
            var materialId = $"material_{i}";
            var renderComponent = RenderComponent.Default(meshId, materialId);

            // This Add operation should trigger archetype edge checking via HasAddEdge
            // which calls SparseJaggedArray.ContainsKey() -> get_Capacity()
            world.Add(entities[i], renderComponent);
        }

        // Verify all entities have both components (if no crash)
        var query = new QueryDescription().WithAll<Transform, RenderComponent>();
        var count = world.CountEntities(query);

        That(count, Is.EqualTo(entityCount), "All entities should have both Transform and RenderComponent");

        // Additional verification that the components are correctly set
        world.Query(query, (Entity entity, ref Transform transform, ref RenderComponent render) =>
        {
            var expectedId = (int)transform.X;
            That(transform.X, Is.EqualTo(expectedId));
            That(transform.Y, Is.EqualTo(expectedId));
            That(render.MeshId, Is.EqualTo($"mesh_{expectedId}"));
            That(render.MaterialId, Is.EqualTo($"material_{expectedId}"));
            That(render.IsVisible, Is.True);
        });
    }

    /// <summary>
    ///     Test with fewer entities to verify the operation works at smaller scale.
    ///     This should pass and helps verify the test setup is correct.
    /// </summary>
    [Test]
    public void LowLoad_RenderComponent_Addition_Works()
    {
        const int entityCount = 100;

        using var world = World.Create();

        // Create entities with base components first
        var entities = new Entity[entityCount];
        for (int i = 0; i < entityCount; i++)
        {
            entities[i] = world.Create(new Transform { X = i, Y = i });
        }

        // Add render components
        for (int i = 0; i < entityCount; i++)
        {
            var meshId = $"mesh_{i}";
            var materialId = $"material_{i}";
            var renderComponent = RenderComponent.Default(meshId, materialId);

            world.Add(entities[i], renderComponent);
        }

        // Verify all entities have both components
        var query = new QueryDescription().WithAll<Transform, RenderComponent>();
        var count = world.CountEntities(query);

        That(count, Is.EqualTo(entityCount), "All entities should have both Transform and RenderComponent");
    }

    /// <summary>
    ///     Test that specifically exercises archetype edge checking during high load.
    ///     This isolates the SparseJaggedArray.ContainsKey() operation that triggers the bug.
    /// </summary>
    [Test]
    public void HighLoad_ArchetypeEdgeChecking_StressTest()
    {
        const int entityCount = 10000;

        using var world = World.Create();

        // Create entities and immediately add render components to trigger edge creation
        for (int i = 0; i < entityCount; i++)
        {
            var entity = world.Create(new Transform { X = i, Y = i });
            var meshId = $"mesh_{i}";
            var materialId = $"material_{i}";
            var renderComponent = RenderComponent.Default(meshId, materialId);

            // This triggers HasAddEdge() calls which stress the SparseJaggedArray
            world.Add(entity, renderComponent);
        }

        // Create additional entities with the same archetype to stress capacity
        for (int i = 0; i < entityCount; i++)
        {
            var entity = world.Create(new Transform { X = i + entityCount, Y = i + entityCount });
            var meshId = $"mesh_duplicate_{i}";
            var materialId = $"material_duplicate_{i}";
            var renderComponent = RenderComponent.Default(meshId, materialId);

            // This re-uses existing archetype edges
            world.Add(entity, renderComponent);
        }

        // Verify total count
        var query = new QueryDescription().WithAll<Transform, RenderComponent>();
        var count = world.CountEntities(query);

        That(count, Is.EqualTo(entityCount * 2), "All entities should have both components");
    }

    /// <summary>
    ///     Tests that our bounds checking safely handles any component ID, even those
    ///     that far exceed the SparseJaggedArray capacity, preventing memory corruption crashes.
    /// </summary>
    [Test]
    public void BoundsChecking_Handles_Any_ComponentId_Safely()
    {
        using var world = World.Create();

        // Create an entity to generate an archetype
        var entity = world.Create(new Transform { X = 1, Y = 1 });
        var archetype = world.Archetypes[0];

        // Test with component IDs that are much higher than any realistic scenario
        // These IDs would definitely exceed SparseJaggedArray capacity and cause crashes without bounds checking
        var extremeHighIds = new[] { 1000, 10000, 100000, 1000000, int.MaxValue };

        foreach (var extremeId in extremeHighIds)
        {
            // These calls should return false safely instead of crashing
            // Without bounds checking, these would cause System.AccessViolationException
            var hasAddEdge = archetype.HasAddEdge(extremeId);
            var hasRemoveEdge = archetype.HasRemoveEdge(extremeId);

            That(hasAddEdge, Is.False, $"HasAddEdge should return false safely for extreme ID {extremeId}");
            That(hasRemoveEdge, Is.False, $"HasRemoveEdge should return false safely for extreme ID {extremeId}");

            // Get calls should return null safely
            var getAddEdge = archetype.GetAddEdge(extremeId);
            var getRemoveEdge = archetype.GetRemoveEdge(extremeId);

            That(getAddEdge, Is.Null, $"GetAddEdge should return null safely for extreme ID {extremeId}");
            That(getRemoveEdge, Is.Null, $"GetRemoveEdge should return null safely for extreme ID {extremeId}");
        }
    }

    /// <summary>
    ///     Simple test to verify that HasAddEdge returns false for high component IDs
    ///     that exceed SparseJaggedArray capacity, preventing crashes.
    /// </summary>
    [Test]
    public void HasAddEdge_ReturnsFalse_ForHighComponentIds()
    {
        using var world = World.Create();

        // Create an entity to generate an archetype
        var entity = world.Create(new Transform { X = 1, Y = 1 });
        var archetype = world.Archetypes[0]; // Get the archetype with Transform

        // Test with component IDs that would exceed SparseJaggedArray capacity
        // These IDs are way beyond the BucketSize of 64 we set
        const int veryHighId = 10000;

        // This should return false safely instead of crashing
        var result = archetype.HasAddEdge(veryHighId);
        That(result, Is.False, "HasAddEdge should return false for IDs exceeding capacity");
    }

    /// <summary>
    ///     Test adding a component that triggers archetype edge operations
    ///     without causing memory corruption.
    /// </summary>
    [Test]
    public void AddComponent_DoesNotCrash_WithBasicTypes()
    {
        using var world = World.Create();

        var entity = world.Create(new Transform { X = 1, Y = 1 });

        // Add render component - this should create archetype edges
        var renderComponent = RenderComponent.Default("test_mesh", "test_material");
        world.Add(entity, renderComponent);

        // Verify it worked
        var query = new QueryDescription().WithAll<Transform, RenderComponent>();
        var count = world.CountEntities(query);
        That(count, Is.EqualTo(1));
    }
}
