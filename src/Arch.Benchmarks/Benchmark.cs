using System.Numerics;
using Arch.Core;
using Arch.Core.Extensions;

namespace Arch.Benchmarks;

public class Benchmark
{
    private static void Main(string[] args)
    {
        // NOTE: Can this be replaced with ManualConfig.CreateEmpty()?
#pragma warning disable HAA0101 // Array allocation for params parameter
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);
#pragma warning restore HAA0101 // Array allocation for params parameter

        // NOTE: Is `-- --job` a typo?
        // Use: dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }
}
