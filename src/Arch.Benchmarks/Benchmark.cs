using Arch.Core;

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

        /*
        using var world = World.Create();
        var list = new List<Entity>(100000);
        for (int index = 0; index < 100000; index++)
        {
            list.Add(world.Create(new Transform(), new Velocity()));
        }

        foreach (var entity in list)
        {
            world.Destroy(entity);
        }*/


        /*
        var b = new ChunkBenchmark { Amount = 1000000 };
        b.Setup();
        b.Transfer();
        b.CoolerTransfer();*/

        // NOTE: Is `-- --job` a typo?
        // Use: dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }
}
