using Arch.Core;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public class ChunkTest
{
    private Chunk _chunk;
    private readonly ComponentType[] _types = { typeof(Transform), typeof(Rotation) };

    [Test]
    public void Chunk()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(in entity);

            var t = new Transform{ X = 10};
            var r = new Rotation{Y = 10};
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        // Make sure the amount fits
        That(_chunk.Size, Is.EqualTo(_chunk.Capacity));
    }

    [Test]
    public void ArchetypeSet()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(in entity);

            var t = new Transform();
            var r = new Rotation();
            _chunk.Set(index, t);
            _chunk.Set(index, r);
        }

        // Make sure the amount fits
        That(_chunk.Size, Is.EqualTo(_chunk.Capacity));
    }

    /*
    [Test]
    public void ArchetypeRemove()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < _chunk.Capacity; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(in entity);

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

    [Test]
    public void ArchetypeRemoveAll()
    {
        _chunk = new Chunk(1000, _types);

        for (var index = 0; index < 5; index++)
        {
            var entity = new Entity(index, 0);
            _chunk.Add(in entity);

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

    [Test]
    public void ArchetypeRemoveAndSetAgain()
    {
        _chunk = new Chunk(1000, _types);

        var newEntity = new Entity(1, 0);
        var newEntityTwo = new Entity(2, 0);

        var firstIndex = _chunk.Add(in newEntity);
        _chunk.Add(in newEntityTwo);

        _chunk.Remove(firstIndex);
        _chunk.Add(in newEntity);

        // Check if the first one was replaced with the last one correctly
        That(_chunk.Size, Is.EqualTo(2));
        That(_chunk.Entities[0].Id, Is.EqualTo(2)); // Needs to be 1, because it will be the last one getting removed and being moved to that position
        That(_chunk.Entities[1].Id, Is.EqualTo(1)); // Needs to be 1, because it will be the last one getting removed and being moved to that position
    }*/
}
