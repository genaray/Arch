using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="ChunkTest"/> class
///     checks if the <see cref="Chunk"/>s work correctly.
/// </summary>
[TestFixture]
public class ChunkTest
{
    private Chunk _chunk;
    private readonly ComponentType[] _types = { typeof(Transform), typeof(Rotation) };

    /// <summary>
    ///     Checks if data inside the chunk is being set correctly.
    /// </summary>
    [Test]
    public void ChunkSet()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(entity);

            var t = new Transform();
            var r = new Rotation();
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        // Make sure the amount fits
        That(_chunk.Size, Is.EqualTo(_chunk.Capacity));
    }

    /// <summary>
    ///     Checks if removing of entities form the chunk works correctly. 
    /// </summary>
    [Test]
    public void ChunkRemove()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(entity);

            var t = new Transform();
            var r = new Rotation();
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        // Get last one, remove first one
        var last = _chunk.Entities[_chunk.Size - 1];
        _chunk.Remove(0);

        // Check if the first one was replaced with the last one correctly
        That(last.Id, Is.EqualTo(_chunk.Entities[0].Id));
    }

    /// <summary>
    ///     Checks if the removal of all entities works correctly. 
    /// </summary>
    [Test]
    public void ChunkRemoveAll()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < 5; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(entity);

            var t = new Transform();
            var r = new Rotation();
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        // Backward delete all since forward does not work while keeping the array dense
        for (var index = _chunk.Size - 1; index >= 0; index--)
        {
            _chunk.Remove(index);
        }

        // Check if the first one was replaced with the last one correctly
        That(_chunk.Size, Is.EqualTo(0));
        That(_chunk.Entities[0].Id, Is.EqualTo(0)); // Needs to be 1, because it will be the last one getting removed and being moved to that position
    }

    /// <summary>
    ///     Checks if removing and setting an entity works correctly. 
    /// </summary>
    [Test]
    public void ChunkRemoveAndSetAgain()
    {
        _chunk = new Chunk(1000, _types);

        var newEntity = new Entity(1, 0);
        var newEntityTwo = new Entity(2, 0);

        var firstIndex = _chunk.Add(newEntity);
        _chunk.Add(newEntityTwo);

        _chunk.Remove(firstIndex);
        _chunk.Add(newEntity);

        // Check if the first one was replaced with the last one correctly
        That(_chunk.Size, Is.EqualTo(2));
        That(_chunk.Entities[0].Id, Is.EqualTo(2)); // Needs to be 1, because it will be the last one getting removed and being moved to that position
        That(_chunk.Entities[1].Id, Is.EqualTo(1)); // Needs to be 1, because it will be the last one getting removed and being moved to that position
    }

    /// <summary>
    ///     Checks if chunk has a component.
    /// </summary>
    [Test]
    public void ChunkHas()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(entity);

            var t = new Transform();
            var r = new Rotation();
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        That(_chunk.Has<Transform>(), Is.True);
        That(_chunk.Has<Ai>(), Is.False);
        That(_chunk.Has<Rotation>(), Is.True);
    }
}
