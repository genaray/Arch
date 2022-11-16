using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace Arch.Benchmark;

public class Benchmark
{
    private static void Main(string[] args)
    {
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);

        /*
        var it = new QueryBenchmark { amount = 10000 };
        it.Setup();
        for (var index = 0; index < 10000; index++) {
            it.StructEntityQuery();
            Console.WriteLine(index);
        }
        it.Cleanup();*/

        // Use : dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);

        /*
        var it = new ArchetypeIterationBenchmark { amount = 100000 };
        it.Setup();
        for (var index = 0; index < 100000; index++) {
            it.IterationJobSchedulerUnsafeAdd();
            Console.WriteLine(index);
        }
        Console.WriteLine("Cleanup");
        it.Cleanup();
        Console.WriteLine("YES");*/
    }
}