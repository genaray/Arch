using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class ArchetypeIterationBenchmark
{
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };

    [Params(10000, 100000, 1000000)] public int Amount;

    private Consumer? _consumer;
    private Archetype? _globalArchetype;

    [GlobalSetup]
    public void Setup()
    {
        _consumer = new Consumer();
        // jobScheduler = new JobScheduler();

        _globalArchetype = new Archetype(_group);
        _globalArchetype.Reserve(Amount);

        for (var index = 0; index < Amount; index++)
        {
            var entity = new Entity(index, 0);
            _globalArchetype.Add(entity, out var slot);

            var t = new Transform();
            var r = new Rotation();
            _globalArchetype.Set(ref slot, t);
            _globalArchetype.Set(ref slot, r);
        }
    }

    [Benchmark]
    public void IterationNormalTwoComponents()
    {
        var size = _globalArchetype.ChunkCount;
        var chunks = _globalArchetype.Chunks;

        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++)
        {
            ref readonly var chunk = ref chunks[chunkIndex];
            var chunkSize = chunk.Size;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunkSize; index++)
            {
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                _consumer.Consume(transform);
                _consumer.Consume(rotation);
            }
        }
    }

    [Benchmark]
    public void IterationUnsafeAddTwoComponents()
    {
        var size = _globalArchetype.ChunkCount;
        ref var chunk = ref _globalArchetype.Chunks[0];

        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++)
        {
            ref readonly var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = chunk.Size;

            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();

            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunkSize; index++)
            {
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);

                _consumer.Consume(currentTransform);
                _consumer.Consume(currentRotation);
            }
        }
    }

    [Benchmark]
    public void IterationParallelUnsafeAdd()
    {
        // Partition the entire source array.
        var rangePartitioner = Partitioner.Create(0, _globalArchetype.ChunkCount);
        Parallel.ForEach(rangePartitioner, range =>
        {
            var start = range.Item1;
            var end = range.Item2;

            ref var chunk = ref _globalArchetype.Chunks[start];

            for (var chunkIndex = 0; chunkIndex < end - start; chunkIndex++)
            {
                ref readonly var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
                var chunkSize = chunk.Size;

                var transforms = currentChunk.GetArray<Transform>();
                var rotations = currentChunk.GetArray<Rotation>();

                ref var transform = ref transforms[0];
                ref var rotation = ref rotations[0];

                for (var index = 0; index < chunkSize; index++)
                {
                    ref var currentTransform = ref Unsafe.Add(ref transform, index);
                    ref var currentRotation = ref Unsafe.Add(ref rotation, index);

                    _consumer.Consume(currentTransform);
                    _consumer.Consume(currentRotation);
                }
            }
        });
    }

    [Benchmark]
    public void IterationNormalEntityTwoComponents()
    {
        var size = _globalArchetype.ChunkCount;
        var chunks = _globalArchetype.Chunks;

        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++)
        {
            ref readonly var chunk = ref chunks[chunkIndex];
            var chunkSize = chunk.Size;

            var entities = chunk.Entities;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunkSize; index++)
            {
                ref readonly var entity = ref entities[index];
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                _consumer.Consume(entity);
                _consumer.Consume(transform);
                _consumer.Consume(rotation);
            }
        }
    }

    [Benchmark]
    public void IterationUnsafeAddEntityTwoComponents()
    {
        var size = _globalArchetype.ChunkCount;
        ref var chunk = ref _globalArchetype.Chunks[0];

        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++)
        {
            ref readonly var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = chunk.Size;

            var entities = currentChunk.Entities;
            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();

            ref var entity = ref entities[0];
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunkSize; index++)
            {
                ref readonly var currentEntity = ref Unsafe.Add(ref entity, index);
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);

                _consumer.Consume(currentEntity);
                _consumer.Consume(currentTransform);
                _consumer.Consume(currentRotation);
            }
        }
    }
}
