using System.Numerics;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

public class Benchmark
{
    private static void Main(string[] args)
    {
  /*
        // NOTE: Can this be replaced with ManualConfig.CreateEmpty()?
#pragma warning disable HAA0101 // Array allocation for params parameter
        var config = new ManualConfig()
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddValidator(JitOptimizationsValidator.DontFailOnError)
            .AddLogger(ConsoleLogger.Default)
            .AddColumnProvider(DefaultColumnProviders.Instance);
#pragma warning restore HAA0101 // Array allocation for params parameter
*/


        var world = World.Create();
        world.Create(1_000_00, 10);
        /*world.Reserve(in Component<int>.Signature, 1_000_00);
        for (var index = 0; index <= 1_000_00; index++)
        {
            world.Create<int>();
        }*/

        /*
        var desc = new QueryDescription().WithAll<int>();
        for (var index = 0; index <= 100000; index++)
        {
            world.Query(in desc, (ref int i) =>
            {
            });
        }*/



        // NOTE: Is `-- --job` a typo?
        // Use: dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        //BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }
}
