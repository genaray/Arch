using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     Simulates a heavy/large component for testing purposes.
/// </summary>
internal unsafe struct HeavyComponent
{
    private fixed double _items[18];
}

[TestFixture]
public sealed class ArchetypeTest
{
    private readonly int _baseChunkSize = 16_382;
    private readonly int _baseChunkEntityCount = 100;
    private static readonly Signature _group = new(typeof(Transform), typeof(Rotation));
    private static readonly Signature _otherGroup = new(typeof(Transform), typeof(Rotation), typeof(Ai));
    private static readonly Signature _heavyGroup = new(typeof(Transform), typeof(Rotation), typeof(HeavyComponent));

    /// <summary>
    ///     Tests if <see cref="Archetype"/>s and their <see cref="Chunk"/> are created correctly.
    /// </summary>
    [Test]
    public void CreateChunk()
    {
        // Create archetype
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var entities = Archetype.GetEntityCountFor(archetype.ChunkSize, _group);

        // Fill archetype
        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        That(archetype.EntityCount, Is.EqualTo(entities));
        That(archetype.EntityCapacity, Is.EqualTo(entities));
        That(archetype.ChunkCount, Is.EqualTo(1));  // Since we filled it with n entities, it must have one single chunk.
        That(archetype.Count, Is.EqualTo(0));  // Since we filled it with n entities, it must have one single chunk.
        That(archetype.GetChunk(0).Count, Is.EqualTo(entities));
    }

    /// <summary>
    ///     Tests if <see cref="Archetype"/>s and their <see cref="Chunk"/> are created correctly.
    /// </summary>
    [Test]
    public void CreateAllChunk()
    {
        // Create archetype
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var count = Archetype.GetEntityCountFor(archetype.ChunkSize, _group);
        Span<Entity> entities = stackalloc Entity[count];

        // Fill archetype
        for (var index = 0; index < count; index++)
        {
            var entity = new Entity(index, 0);
            entities[index] = entity;
        }

        archetype.AddAll(entities, count);

        That(archetype.EntityCount, Is.EqualTo(count));
        That(archetype.EntityCapacity, Is.EqualTo(count));
        That(archetype.ChunkCount, Is.EqualTo(1));
        That(archetype.Count, Is.EqualTo(0));
        That(archetype.GetChunk(0).Count, Is.EqualTo(count));
    }

    /// <summary>
    ///     Checks if the <see cref="Archetype.ChunkSize"/> increases when <see cref="Entity"/>s and their components become too large.
    /// </summary>
    [Test]
    public void ScaleChunkCapacity()
    {
        var archetype = new Archetype(_heavyGroup, _baseChunkSize, _baseChunkEntityCount);
        That(archetype.ChunkSize, Is.EqualTo(_baseChunkSize * 2)); // heavyGroup should be large enough to force the chunk to pick a 32KB chunk instead of 16KB
    }

    /// <summary>
    ///     Checks if an <see cref="Archetype"/> successfully creates multiple <see cref="Chunk"/>.
    /// </summary>
    [Test]
    public void CreateMultipleChunk()
    {
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var entities =  Archetype.GetEntityCountFor(archetype.ChunkSize, _group) * 2;

        // Add entities
        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        That(archetype.EntityCount, Is.EqualTo(entities));
        That(archetype.EntityCapacity, Is.EqualTo(entities));
        That(archetype.ChunkCount, Is.EqualTo(2));
        That(archetype.Count, Is.EqualTo(1));
    }

    /// <summary>
    ///     Checks if an <see cref="Archetype"/> is able to reserve enough memory for a number of <see cref="Entity"/>s and their components.
    /// </summary>
    [Test]
    public void Ensure()
    {
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var entitiesPerChunk = Archetype.GetEntityCountFor(archetype.ChunkSize, _group);
        var entities = entitiesPerChunk * 10;
        archetype.EnsureEntityCapacity(entities);

        // Add entities
        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        That(archetype.EntityCount, Is.EqualTo(entities));
        That(archetype.EntityCapacity, Is.EqualTo(entities));
        That(archetype.ChunkCount, Is.EqualTo(archetype.EntityCount/entitiesPerChunk));
        That(archetype.ChunkCapacity, Is.EqualTo(archetype.EntityCount/entitiesPerChunk));
        That(archetype.Count, Is.EqualTo((archetype.EntityCount/entitiesPerChunk)-1));
    }

    /// <summary>
    ///     Checks whether empty <see cref="Chunk"/>s are deleted and their effect on <see cref="Archetype"/> capacity.
    /// </summary>
    [Test]
    public void RemoveChunk()
    {
        var archetype = new Archetype(_group,  _baseChunkSize, _baseChunkEntityCount);
        var entities =  Archetype.GetEntityCountFor(archetype.ChunkSize, _group) + 1;

        // Add entities
        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        var slot = new Slot(0, 0);
        archetype.Remove(slot, out _);

        That(archetype.Count, Is.EqualTo(0));
        That(archetype.ChunkCount, Is.EqualTo(2));
        That(archetype.ChunkCapacity, Is.EqualTo(2));
        That(archetype.Chunks[0].Count, Is.EqualTo(entities - 1));
        That(archetype.Chunks[0].Entities[0].Id, Is.EqualTo( Archetype.GetEntityCountFor(archetype.ChunkSize, _group))); // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }

    /// <summary>
    ///     Checks if removing an <see cref="Entity"/> from the <see cref="Archetype"/> causes another <see cref="Entity"/> to move to that position.
    /// </summary>
    [Test]
    public void RemoveFromChunkWithReplacement()
    {
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var entities =  Archetype.GetEntityCountFor(archetype.ChunkSize, _group) + 50;

        // Add entities
        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        var slot = new Slot(0, 0);
        archetype.Remove(slot, out _);

        That(archetype.Count, Is.EqualTo(1));
        That(archetype.ChunkCount, Is.EqualTo(2));
        That(archetype.ChunkCapacity, Is.EqualTo(2));
        That(archetype.Chunks[0].Count, Is.EqualTo(entities - 50));
        That(archetype.Chunks[1].Count, Is.EqualTo(49));
        That(archetype.Chunks[0].Entities[0].Id, Is.EqualTo( Archetype.GetEntityCountFor(archetype.ChunkSize, _group) + 50 - 1)); // Last entity from second chunk now replaced the removed entity and is in the first chunk
    }

    /// <summary>
    ///     Checks if moving an <see cref="Entity"/> between two <see cref="Archetype"/>s was successful.
    /// </summary>
    [Test]
    public void Move()
    {
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var otherArchetype = new Archetype(_otherGroup, _baseChunkSize, _baseChunkEntityCount);

        // Add two entities into different archetypes to move one to the other later.
        var entity = new Entity(1, 0);
        var otherEntity = new Entity(2, 0);
        archetype.Add(entity, out _, out var entityOneSlot);
        otherArchetype.Add(otherEntity, out _, out _);

        archetype.Set(ref entityOneSlot, new Transform { X = 10, Y = 10 });
        archetype.Set(ref entityOneSlot, new Rotation { X = 10, Y = 10 });

        // Move entity from first archetype to second, copy its components and remove it from the first.
        otherArchetype.Add(entity, out _, out var newSlot);
        Archetype.CopyComponents(archetype, ref entityOneSlot,otherArchetype, ref newSlot);
        archetype.Remove(entityOneSlot, out _);

        That(archetype.Chunks[0].Count, Is.EqualTo(0));
        That(otherArchetype.Chunks[0].Count, Is.EqualTo(2));
        That(otherArchetype.Get<Transform>(ref newSlot).X, Is.EqualTo(10));
        That(otherArchetype.Get<Transform>(ref newSlot).Y, Is.EqualTo(10));
        That(otherArchetype.Get<Rotation>(ref newSlot).X, Is.EqualTo(10));
        That(otherArchetype.Get<Rotation>(ref newSlot).Y, Is.EqualTo(10));
    }

    /// <summary>
    ///     Checks if a copy operation between <see cref="Archetype"/> was successful.
    ///     This is checked by value equality of the items and their correct order.
    /// </summary>
    /// <param name="sourceAmount">Different test entity amounts.</param>
    /// <param name="destinationAmount">Different test entity amounts.</param>
    [Test]
    public void CopyTo([Values(1111,2222,3333)] int sourceAmount, [Values(1111,2222,3333)] int destinationAmount)
    {
        var source = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var destination = new Archetype(_heavyGroup, _baseChunkSize, _baseChunkEntityCount);

        // Fill chunks with data to copy
        for (int index = 0; index < sourceAmount; index++)
        {
            var entity = new Entity(index, 0);
            source.Add(entity, out _, out var entityOneSlot);
            source.Set(ref entityOneSlot, new Transform { X = 10, Y = 10 });
            source.Set(ref entityOneSlot, new Rotation { X = 10, Y = 10 });
        }

        // Fill chunks with data to copy
        for (int index = 0; index < destinationAmount; index++)
        {
            var entity = new Entity(index, 0);
            destination.Add(entity, out _, out var entityOneSlot);
            destination.Set(ref entityOneSlot, new Transform { X = 100, Y = 100 });
            destination.Set(ref entityOneSlot, new Rotation { X = 100, Y = 100 });
        }

        // Copy from one chunk into other.
        Archetype.Copy(source, destination);
        source.Clear();

        var sourceCounter = sourceAmount;
        var destinationCounter = destinationAmount;
        var countedSourceItems = 0;
        var countedDestinationItems = 0;

        // Check if the first n items match the data from the destination chunk correctly
        foreach (ref var chunk in destination)
        {
            chunk.GetSpan<Transform, Rotation>(out var transforms, out var rotations);
            foreach (var index in chunk)
            {
                ref var entity = ref chunk.Entity(index);
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                // All source items done, break loop
                if (sourceCounter == 0)
                {
                    break;
                }

                // Verify that all source items were correctly copied
                That(entity.Id == sourceCounter-1 && transform.X == 10 && transform.Y == 10 && rotation.X == 10 && rotation.Y == 10, Is.True);

                countedSourceItems++;
                sourceCounter--;
            }
        }

        // Check if the copied values are attached to the destination archetype correctly.
        sourceCounter = sourceAmount;
        foreach (ref var chunk in destination)
        {
            chunk.GetSpan<Transform, Rotation>(out var transforms, out var rotations);
            foreach (var index in chunk)
            {
                ref var entity = ref chunk.Entity(index);
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                // Skip all entrys till we are at the end of the old destination archetype where the new values should begin
                if (sourceCounter > 0)
                {
                    sourceCounter--;
                    continue;
                }

                That(entity.Id == destinationCounter-1 && transform.X == 100 && transform.Y == 100 && rotation.X == 100 && rotation.Y == 100, Is.True);

                countedDestinationItems++;
                destinationCounter--;
            }
        }

        // Make sure that EVERY single entity was copied correctly
        That(destination.EntityCount, Is.EqualTo(sourceAmount+destinationAmount));
        That(countedSourceItems, Is.EqualTo(sourceAmount));
        That(countedDestinationItems, Is.EqualTo(destinationAmount));

        var requiredChunksForSource = Archetype.GetChunkCapacityFor(source.EntitiesPerChunk, sourceAmount);
        var requiredChunksForDestination = Archetype.GetChunkCapacityFor(destination.EntitiesPerChunk, sourceAmount+destinationAmount);

        That(source.EntityCount, Is.EqualTo(0));
        That(source.ChunkCount, Is.EqualTo(requiredChunksForSource));
        That(source.ChunkCapacity, Is.EqualTo(requiredChunksForSource));
        That(source.Count, Is.EqualTo(0));

        That(destination.EntityCount, Is.EqualTo(sourceAmount+destinationAmount));
        That(destination.ChunkCount, Is.EqualTo(requiredChunksForDestination));
        That(destination.ChunkCapacity, Is.EqualTo(requiredChunksForDestination));
        That(destination.Count, Is.EqualTo(requiredChunksForDestination - 1));  // -1 Since its the index that points towards the Chunk, not the count
    }

    /// <summary>
    ///     Checks if a copy operation between <see cref="Archetype"/> was successful.
    ///     This is checked by the <see cref="Entity"/> shift through slots.
    /// </summary>
    /// <param name="sourceAmount">Different test entity amounts.</param>
    /// <param name="destinationAmount">Different test entity amounts.</param>
    [Test]
    public void CopyToShift([Values(1111,2222,3333)] int sourceAmount, [Values(1111,2222,3333)] int destinationAmount)
    {
        var source = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var destination = new Archetype(_heavyGroup, _baseChunkSize, _baseChunkEntityCount);

        // Fill chunks with data to copy
        for (int index = 0; index < sourceAmount; index++)
        {
            var entity = new Entity(index, 0);
            source.Add(entity,out _, out var entityOneSlot);
            source.Set(ref entityOneSlot, new Transform { X = 10, Y = 10 });
            source.Set(ref entityOneSlot, new Rotation { X = 10, Y = 10 });
        }

        // Fill chunks with data to copy
        for (int index = 0; index < destinationAmount; index++)
        {
            var entity = new Entity(index, 0);
            destination.Add(entity, out _, out var entityOneSlot);
            destination.Set(ref entityOneSlot, new Transform { X = 10, Y = 10 });
            destination.Set(ref entityOneSlot, new Rotation { X = 10, Y = 10 });
        }

        // Calculate their slots and position of copied entity.
        var sourceSlot = source.CurrentSlot;
        var destinationSlot = destination.CurrentSlot;
        destinationSlot++;
        var resultSlot = Slot.Shift(sourceSlot, source.EntitiesPerChunk, destinationSlot, destination.EntitiesPerChunk);

        // Copy from one chunk into other.
        Archetype.Copy(source, destination);
        source.Clear();

        var requiredChunksForSource = Archetype.GetChunkCapacityFor(source.EntitiesPerChunk, sourceAmount);
        var requiredChunksForDestination = Archetype.GetChunkCapacityFor(destination.EntitiesPerChunk, sourceAmount + destinationAmount);

        That(source.Entity(ref sourceSlot), Is.EqualTo(destination.Entity(ref resultSlot)));  // Make sure entities were copied correctly.
        That(source.EntityCount, Is.EqualTo(0));
        That(source.ChunkCount, Is.EqualTo(requiredChunksForSource));
        That(source.ChunkCapacity, Is.EqualTo(requiredChunksForSource));
        That(source.Count, Is.EqualTo(0));

        That(destination.EntityCount, Is.EqualTo(sourceAmount+destinationAmount));
        That(destination.ChunkCount, Is.EqualTo(requiredChunksForDestination));
        That(destination.ChunkCapacity, Is.EqualTo(requiredChunksForDestination));
        That(destination.Count, Is.EqualTo(requiredChunksForDestination - 1));
    }

    /// <summary>
    ///     Checks whether the next available slots of the archetype can be calculated correctly.
    /// </summary>
    [Test]
    public void GetNextSlots()
    {
        var archetype = new Archetype(_group, _baseChunkSize, _baseChunkEntityCount);
        var entitiesPerChunk = Archetype.GetEntityCountFor(archetype.ChunkSize, _group);
        var entities =  entitiesPerChunk/2;

        for (var index = 0; index < entities; index++)
        {
            var entity = new Entity(index, 0);
            archetype.Add(entity, out _, out _);
        }

        Span<Slot> slots = stackalloc Slot[archetype.EntitiesPerChunk];
        var created = Archetype.GetNextSlots(archetype, slots, archetype.EntitiesPerChunk);

        // Create next n entities in the chunk to see if they are created correctly
        for (var index = 0; index < created; index++)
        {
            var entity = new Entity(entities+index, 0);
            archetype.Add(entity, out _, out var createdIn);
            That(slots[index], Is.EqualTo(createdIn));
        }

        var requiredChunksForSource = Archetype.GetChunkCapacityFor(archetype.EntitiesPerChunk, entities+created);

        That(archetype.EntityCount, Is.EqualTo(entities+created));
        That(archetype.EntityCapacity, Is.EqualTo(entities+created));
        That(archetype.ChunkCount, Is.EqualTo(requiredChunksForSource));
        That(archetype.ChunkCapacity, Is.EqualTo(requiredChunksForSource));
        That(archetype.Count, Is.EqualTo(requiredChunksForSource - 1));
    }
}
