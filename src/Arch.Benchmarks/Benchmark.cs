using System.Numerics;
using Arch.Core;
using Arch.Core.Extensions;

using Arch.Core.Utils;

namespace Arch.Benchmarks;

public class Benchmark
{
    public static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args);
        //BenchmarkSwitcher.FromAssembly(typeof(Benchmark).Assembly).Run(args, new DebugInProcessConfig());
    }
}
