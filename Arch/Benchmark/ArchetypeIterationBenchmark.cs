
using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using BenchmarkDotNet.Attributes;

namespace Arch.Test {
    
    /*
    [RPlotExporter]
    public class ArchetypeIterationBenchmark {

        [Params( 10000, 100000, 1000000, 10000000)]
        public int amount;
        private Archetype archetype;
        
        [IterationSetup]
        public void Setup(){
            
            archetype = new Archetype(amount,typeof(Transform), typeof(Rotation));
            for (var index = 0; index < amount; index++) {

                var entity = new Entity(index, 0, 0);
                archetype.Add(in entity);
                
                var t = new Transform();
                var r = new Rotation();
                archetype.Set(index, ref t);
                archetype.Set(index, ref r);
            }
        }
        
        [Benchmark]
        public void IterationNormal() {
            
            var transforms = archetype.GetArray<Transform>();
            var rotations = archetype.GetArray<Rotation>();

            for (int index = 0; index < archetype.Size; index++) {

                ref var transform = ref transforms[index];
                ref var rotation = ref rotations[index];

                transform.x++;
                rotation.w++;
            }
        }
        
        [Benchmark]
        public void IterationUnsafeAdd() {

            var transforms = archetype.GetArray<Transform>();
            var rotations = archetype.GetArray<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            for (int i = 0; i < archetype.Size; i++) {
                
                ref var currentTransform = ref Unsafe.Add(ref transform, i);
                ref var currentRotation = ref Unsafe.Add(ref rotation, i);
                
                currentTransform.x++;
                currentRotation.w++;
            }
        }
        
        [Benchmark]
        public void IterationUnsafeAddIncr() {
            
            var transforms = archetype.GetArray<Transform>();
            var rotations = archetype.GetArray<Rotation>();
            
            ref var transform = ref transforms[0];
            ref var rotation = ref rotations[0];

            transform.x++;
            rotation.w++;
            
            for (int i = 0; i < archetype.Size; i++) {
                
                transform = ref Unsafe.Add(ref transform, 1);
                rotation = ref Unsafe.Add(ref rotation, 1);
                
                transform.x++;
                rotation.w++;
            }
        }
        
        [Benchmark]
        public void IterationManualRangeCheck() {
            
            var transforms = archetype.GetArray<Transform>();
            var rotations = archetype.GetArray<Rotation>();

            if (transforms.Length == rotations.Length) {
                
                for (int index = 0; index < archetype.Size; index++) {

                    ref var transform = ref transforms[index];
                    ref var rotation = ref rotations[index];

                    transform.x++;
                    rotation.w++;
                }
            }
        }
        
        [Benchmark]
        public void IterationMultipleLoops() {
            
            var transforms = archetype.GetArray<Transform>();
            var rotations = archetype.GetArray<Rotation>();

            for (int index = 0; index < transforms.Length; index++) {

                ref var transform = ref transforms[index];
                transform.x++;
            }
            
            for (int index = 0; index < rotations.Length; index++) {
                
                ref var rotation = ref rotations[index];
                rotation.w++;
            }
        }
    }*/
}