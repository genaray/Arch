using System;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;

namespace Arch.Benchmark; 
    
class Benchmark {
    
    static void Main(string[] args) {
        
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);
        
        BenchmarkRunner.Run<ArchetypeIterationBenchmark>(config);
        /*var b = new ArchetypeIterationBenchmark { amount = 1000000 };
        b.Setup();
        
        for(var index = 0; index < 100; index++)
            b.IterationNormal();*/
    }
}
