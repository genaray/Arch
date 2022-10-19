
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Test;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Microsoft.Diagnostics.Tracing.Analysis;

namespace Arch.Benchmark; 


[HtmlExporter]
[MemoryDiagnoser(true)]
[HardwareCounters(HardwareCounter.CacheMisses)]
//[DisassemblyDiagnoser(printSource: true)]
//[RyuJitX64Job]
public unsafe class ArchetypeIterationTechniquesBenchmark {

    [Params( 10000, 100000, 1000000, 10000000)]
    public int amount;

    private Type[] group = { typeof(Transform), typeof(Rotation) };
    
    private Archetype globalArchetype;
    private Consumer consumer;

    [GlobalSetup]
    public void Setup() {

        consumer = new Consumer();
        
        globalArchetype = new Archetype(group);
        globalArchetype.AllocateFor(amount);
        for (var index = 0; index < amount; index++) {

            var entity = new Entity(index, 0, 0);
            globalArchetype.Add(in entity);
                
            var t = new Transform();
            var r = new Rotation();
            globalArchetype.Set(entity.EntityId, t);
            globalArchetype.Set(entity.EntityId, r);
        }
    }
    
    [Benchmark]
    public void IterationNormalTwoComponents() {

        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunk.Size; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponents() {

        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {
            
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
    
    [Benchmark]
    public void IterationNormalTwoComponentsSpan() {

        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

            for (var index = 0; index < chunk.Size; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponentsSpan() {

        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {
            
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetSpan<Transform>();
            var rotations = currentChunk.GetSpan<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
    
    [Benchmark]
    public void IterationNormalTwoComponentsUnsafeArray() {

        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetArrayUnsafe<Transform>();
            var rotations = chunk.GetArrayUnsafe<Rotation>();

            for (var index = 0; index < chunk.Size; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponentsUnsafeArray() {

        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {
            
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetArrayUnsafe<Transform>();
            var rotations = currentChunk.GetArrayUnsafe<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
    
    [Benchmark]
    public void IterationNormalTwoComponentsUnsafeSpan() {

        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetSpanUnsafe<Transform>();
            var rotations = chunk.GetSpanUnsafe<Rotation>();

            for (var index = 0; index < chunk.Size; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponentsUnsafeSpan() {

        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {
            
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var transforms = currentChunk.GetSpanUnsafe<Transform>();
            var rotations = currentChunk.GetSpanUnsafe<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < currentChunk.Size; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
    
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponentsCompleteUnsafe() {

        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < globalArchetype.Size; chunkIndex++) {
            
            ref var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            ref var transform = ref currentChunk.GetFirstUnsafe<Transform>();
            ref var rotation = ref currentChunk.GetFirstUnsafe<Rotation>();

            for (var index = 0; index < currentChunk.Size; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
}