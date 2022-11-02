using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Arch.Test;

namespace Arch.Benchmark; 

[HtmlExporter]
[MemoryDiagnoser(true)]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class ArchetypeIterationBenchmark {
    
    [Params( 10000, 100000, 1000000)]
    public int amount;

    private Type[] group = { typeof(Transform), typeof(Rotation) };
    
    private Archetype globalArchetype;
    private Consumer consumer;
    
    [GlobalSetup]
    public void Setup() {

        consumer = new Consumer();
        
        globalArchetype = new Archetype(group);
        globalArchetype.Reserve(amount);
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

        var size = globalArchetype.Size;
        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++) {

            ref readonly var chunk = ref chunks[chunkIndex];
            var chunkSize = chunk.Size;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunkSize; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddTwoComponents() {

        var size = globalArchetype.Size;
        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++) {
            
            ref readonly var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = chunk.Size;
            
            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunkSize; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
    
    [Benchmark]
    public void IterationNormalEntityTwoComponents() {
        
        var size = globalArchetype.Size;
        var chunks = globalArchetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++) {

            ref readonly var chunk = ref chunks[chunkIndex];
            var chunkSize = chunk.Size;
            
            var entities = chunk.Entities;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunkSize; index++) {

                ref readonly var entity = ref entities[index];
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];
                
                consumer.Consume(entity);
                consumer.Consume(transform);
                consumer.Consume(rotation);
            }
        }
    }

    [Benchmark]
    public void IterationUnsafeAddEntityTwoComponents() {

        var size = globalArchetype.Size;
        ref var chunk = ref globalArchetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < size; chunkIndex++) {

            ref readonly var currentChunk = ref Unsafe.Add(ref chunk, chunkIndex);
            var chunkSize = chunk.Size;
            
            var entities = currentChunk.Entities;
            var transforms = currentChunk.GetArray<Transform>();
            var rotations = currentChunk.GetArray<Rotation>();

            ref var entity = ref entities[0];
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunkSize; index++) {

                ref readonly var currentEntity = ref Unsafe.Add(ref entity, index);
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentEntity);
                consumer.Consume(currentTransform);
                consumer.Consume(currentRotation);
            }
        }
    }
}