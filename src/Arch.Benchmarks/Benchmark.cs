namespace Arch.Benchmarks;

public class Benchmark
{
    private static void Main(string[] args)
    {
        // NOTE: Can this be replaced with ManualConfig.CreateEmpty()?
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        // NOTE: Is `-- --job` a typo?
        // Use: dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }
}
