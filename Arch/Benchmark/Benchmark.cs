using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Arch.Test;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using CommandLine.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;

namespace Arch.Benchmark; 

public struct Mytest {

    public string data;

}

public class Benchmark {

    public static IntPtr ptr;
    public static Array[] arrays;
    public static Dictionary<int,int> idToArrayIndex = new();
    
    static void Main(string[] args) {

        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);
        
        var test = new[] { typeof(Rotation), typeof(Transform), typeof(Mytest) };
        test.Categorize(out var managed, out var unmanaged);
        
        ptr = Marshal.AllocHGlobal(unmanaged.ToByteSize() * 100);
        arrays = new Array[managed.Count];
        
        for (var index = 0; index < unmanaged.Count; index++) {
            
            var type = unmanaged[index];
            var id = ComponentMeta.Id(type);
            idToArrayIndex[id] = unmanaged.OffsetTo(type, 100);
        }
        
        for (var index = 0; index < managed.Count; index++) {

            var type = managed[index];
            var id = ComponentMeta.Id(type);
            idToArrayIndex[id] = index;
            arrays[index] = Array.CreateInstance(type, 100);
        }

        ref var myTest = ref GetFirstElement<Mytest>();
        ref var myTransform = ref GetFirstElement<Transform>();
        ref var myRotation = ref GetFirstElement<Rotation>();

        for (var index = 0; index < 100; index++) {

            ref var nextTest = ref Unsafe.Add(ref myTest, index);
            nextTest.data = index.ToString();
            
            ref var nextTransform = ref Unsafe.Add(ref myTransform, index);
            nextTransform.x = index;
            
            ref var nextRot = ref Unsafe.Add(ref myRotation, index);
            nextRot.x = index;
        }

        var myDataTen = Get<Mytest>(10);
        var myTransformTen = Get<Transform>(10);
        var myRotationTen = Get<Rotation>(10);
        
        // Use : dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }

    /// <summary>
    /// Returns the index of the component array inside the structure of arrays. 
    /// </summary>
    /// <typeparam name="T">The component</typeparam>
    /// <returns></returns>
    private static int IndexOrOffset<T>() {

        var id = ComponentMeta<T>.Id;
        if (idToArrayIndex.TryGetValue(id, out var index))
            return index;
        
        return -1;
    }
    
    public static T[] GetArrayUnsafe<T>() {
      
        var index = IndexOrOffset<T>();
        ref var first = ref MemoryMarshal.GetArrayDataReference(arrays);
        ref var current = ref Unsafe.Add(ref first, index);
        return Unsafe.As<T[]>(current);
    }
    
    public static unsafe ref T GetFirstElement<T>(){
        
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>()) {
            return ref MemoryMarshal.GetArrayDataReference(GetArrayUnsafe<T>());
        }
        else {

            var offset = IndexOrOffset<T>();
            var location = IntPtr.Add(ptr, offset);
            return ref Unsafe.AsRef<T>((void*)location);
        }
    }

    public static unsafe ref T Get<T>(int index) {
        
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>()) {
            return ref GetArrayUnsafe<T>()[index];
        }
        else {

            var offset = IndexOrOffset<T>();
            var structSize = Marshal.SizeOf<T>();
            var arrayStart = IntPtr.Add(ptr, offset);
            var element = IntPtr.Add(arrayStart, index * structSize);
            return ref Unsafe.AsRef<T>((void*)element);
        }
    }
}