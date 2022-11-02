using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using CommandLine.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;

namespace Arch.Benchmark; 

public class Benchmark {

    static void Main(string[] args) {

        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        // Use : dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);

        /*
        var it = new ArchetypeIterationBenchmark { amount = 100000 };
        it.Setup();
        for(var index = 0; index < 100; index++)
            it.IterationUnsafeAddTwoComponents();*/
    }
}