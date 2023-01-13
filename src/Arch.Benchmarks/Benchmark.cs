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
        var _world = World.Create();
        var _queryDescription = new QueryDescription().WithAll<Transform, Velocity>();

        for (int index = 0; index < 100000; index++)
        {
            _world.Create(new Transform{ X = 1, Y = 1}, new Velocity{ X = 1, Y = 1});
        }

        foreach (var refs in _world.Query(in _queryDescription).GetIterator<Transform, Velocity>())
        {
            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        }*/

/*
        var enumerator = _world.Query(in _queryDescription).GetIterator<Transform, Velocity>().GetEnumerator();
        while (enumerator.Next())
        {
            var refs = enumerator.GetCurrent();
            refs.t0.X += refs.t1.X;
            refs.t0.Y += refs.t1.Y;
        }*/


        // NOTE: Is `-- --job` a typo?
        // Use: dotnet run -c Release --framework net7.0 -- --job short --filter *IterationBenchmark*
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, config);
    }
}
