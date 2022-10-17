
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Test;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Microsoft.Diagnostics.Tracing.Analysis;

namespace Arch.Benchmark; 


[HtmlExporter]
[MemoryDiagnoser(true)]
[HardwareCounters(HardwareCounter.CacheMisses)]
public unsafe class ArchetypeIterationBenchmark {

    [Params( 10000, 100000, 1000000, 10000000)]
    public int amount;
    private Archetype archetype;
    private Consumer consumer;

    [GlobalSetup]
    public void Setup() {

        consumer = new Consumer();
        archetype = new Archetype(typeof(Transform), typeof(Rotation));
        archetype.AllocateFor(amount);
        for (var index = 0; index < amount; index++) {

            var entity = new Entity(index, 0, 0);
            archetype.Add(in entity);
                
            var t = new Transform();
            var r = new Rotation();
            archetype.Set(entity.EntityId, t);
            archetype.Set(entity.EntityId, r);
        }
    }


    [Benchmark]
    public void IterationNormal() {

        var chunks = archetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunk.Capacity; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                transform.x++;
                rotation.w++;
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAdd() {

        var chunks = archetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {
            
            ref var chunk = ref chunks[chunkIndex];
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunk.Capacity; index++) {

                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                currentTransform.x++;
                currentRotation.w++;
            }
        }
    }
    
    [Benchmark]
    public void IterationNormalWithEntity() {

        var chunks = archetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref chunks[chunkIndex];
            var entities = chunk.Entities;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            for (var index = 0; index < chunk.Capacity; index++) {

                ref var entity = ref entities[index];
                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];
                
                consumer.Consume(entity);
                transform.x++;
                rotation.w++;
            }
        }
    }
        
    [Benchmark]
    public void IterationUnsafeAddWithEntity() {

        var chunks = archetype.Chunks;
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {
            
            ref var chunk = ref chunks[chunkIndex];
            var entities = chunk.Entities;
            var transforms = chunk.GetArray<Transform>();
            var rotations = chunk.GetArray<Rotation>();

            ref var entity = ref entities[0];
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (var index = 0; index < chunk.Capacity; index++) {

                ref var currentEntity = ref Unsafe.Add(ref entity, index);
                ref var currentTransform = ref Unsafe.Add(ref transform, index);
                ref var currentRotation = ref Unsafe.Add(ref rotation, index);
                
                consumer.Consume(currentEntity);
                currentTransform.x++;
                currentRotation.w++;
            }
        }
    }
}