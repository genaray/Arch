
using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using Arch.Test;
using BenchmarkDotNet.Attributes;

namespace Arch.Benchmark; 


[HtmlExporter]
[MemoryDiagnoser(true)]
public class ArchetypeIterationBenchmark {

    [Params( 10000, 100000)]
    public int amount;
    private Archetype archetype;
        
    [GlobalSetup]
    public void Setup(){
            
        archetype = new Archetype(typeof(Transform), typeof(Rotation));
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
    public void IterationNormalFast() {

        for (var index = 0; index < amount; index++) {
            int i = 0;
        }
    }
    
    [Benchmark]
    public void IterationNormal() {

        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref archetype.Chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

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

        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref archetype.Chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();
            
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
    public unsafe void IterationFixedPtrIteration() {
        
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref archetype.Chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

            fixed (Transform* tPtr = &transforms[0]) {
                fixed (Rotation* rPtr = &rotations[0]) {
                
                    for (var index = 0; index < chunk.Capacity; index++) {

                        var currenTransform = &tPtr[index];
                        var currenRotation = &rPtr[index];
                
                        currenTransform->x++;
                        currenRotation->w++;
                    }
                }
            }
        }
    }
        
    [Benchmark]
    public void IterationManualRangeCheck() {
            
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref archetype.Chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

            if (transforms.Length == rotations.Length) {

                for (var index = 0; index < chunk.Capacity; index++) {

                    ref var transform = ref transforms[index];
                    ref var rotation = ref rotations[index];

                    transform.x++;
                    rotation.w++;
                }
            }
        }
    }
        
    [Benchmark]
    public void IterationMultipleLoops() {
            
        for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {

            ref var chunk = ref archetype.Chunks[chunkIndex];
            var transforms = chunk.GetSpan<Transform>();
            var rotations = chunk.GetSpan<Rotation>();

            for (var index = 0; index < transforms.Length; index++) {

                ref var transform = ref transforms[index];
                transform.x++;
            }
            
            for (var index = 0; index < rotations.Length; index++) {
                
                ref var rotation = ref rotations[index];
                rotation.w++;
            }
        }
    }
}