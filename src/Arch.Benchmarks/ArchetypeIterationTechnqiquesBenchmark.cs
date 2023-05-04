using Arch.Core;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
// [DisassemblyDiagnoser(printSource: true)]
// [RyuJitX64Job]
public class ArchetypeIterationTechniquesBenchmark
{
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };

    [Params(10000, 100000, 1000000, 10000000)]
    public int Amount;

    private Consumer? _consumer;
    private Archetype? _globalArchetype;

    [GlobalSetup]
    public void Setup()
    {
        _consumer = new Consumer();

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
        var chunks = _globalArchetype.Chunks;

        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunk.Size; index++)
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
        ref var chunk = ref _globalArchetype.Chunks[0];

        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();

            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++)
            {
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);

                _consumer.Consume(currentTransform);
                _consumer.Consume(currentRotation);
            }
        }
    }

    [Benchmark]
    public void IterationNormalTwoComponentsSpan()
    {
        var chunks = _globalArchetype.Chunks;

        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

            for (var index = 0; index < chunk.Size; index++)
            {
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                _consumer.Consume(transform);
                _consumer.Consume(rotation);
            }
        }
    }

    [Benchmark]
    public void IterationUnsafeAddTwoComponentsSpan()
    {
        ref var chunk = ref _globalArchetype.Chunks[0];

        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetSpan<Transform>();
            var rotations = currentChunk.GetSpan<Rotation>();

            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++)
            {
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);

                _consumer.Consume(currentTransform);
                _consumer.Consume(currentRotation);
            }
        }
    }


    [Benchmark]
    public void IterationBackwardsUnsafeAdd()
    {
        ref var chunk = ref _globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = currentChunk.Size;
            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(currentChunk.Entities);

            for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                _consumer.Consume(entity);
            }
        }
    }

    [Benchmark]
    public void IterationBackwardsUnsafeSubstract()
    {
        ref var chunk = ref _globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = currentChunk.Size;
            ref var entityLastElement = ref ArrayExtensions.DangerousGetReferenceAt(currentChunk.Entities, chunkSize - 1);

            for (var entityIndex = 0; entityIndex < chunkSize; ++entityIndex)
            {
                ref readonly var entity = ref Unsafe.Subtract(ref entityLastElement, entityIndex);
                _consumer.Consume(entity);
            }
        }
    }

    [Benchmark]
    public void IterationBackwardsLoop()
    {
        ref var chunk = ref _globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < _globalArchetype.Size; chunkIndex++)
        {
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = currentChunk.Size;

            ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(currentChunk.Entities);
            ref var entityLastElement = ref ArrayExtensions.DangerousGetReferenceAt(currentChunk.Entities, chunkSize - 1);

            while (Unsafe.IsAddressGreaterThan(ref entityLastElement, ref entityFirstElement))
            {
                _consumer.Consume(in entityLastElement);
                entityLastElement = ref Unsafe.Subtract(ref entityLastElement, 1);
            }
        }
    }
}
