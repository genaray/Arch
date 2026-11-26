using Arch.Core;
using Schedulers;

namespace Arch.Benchmarks;

/// <summary>
///     Compares scheduling overhead and load balancing for uniform and clustered expensive work.
/// </summary>
[MemoryDiagnoser]
public class SchedulerWorkloadBenchmark
{
    private static readonly QueryDescription _description = new QueryDescription().WithAll<Workload>();
    private JobScheduler _scheduler = null!;
    private World _uniform = null!;
    private World _uneven = null!;

    [Params(10_000, 100_000, 1_000_000)]
    public int Amount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _scheduler = new JobScheduler();
        World.SharedJobScheduler = _scheduler;
        _uniform = CreateWorld(false);
        _uneven = CreateWorld(true);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.SharedJobScheduler = null;
        _scheduler.Dispose();
        World.Destroy(_uniform);
        World.Destroy(_uneven);
    }

    [Benchmark]
    public void Uniform() => Run(_uniform);

    [Benchmark]
    public void Uneven() => Run(_uneven);

    private static void Run(World world)
    {
        var update = new UpdateWorkload();
        world.InlineParallelQuery<UpdateWorkload, Workload>(in _description, ref update);
    }

    private World CreateWorld(bool uneven)
    {
        var world = World.Create();
        world.Create<Workload>(Amount);
        var index = 0;
        world.Query(in _description, (ref Workload workload) =>
        {
            // Cluster heavy entities together to put them in the same chunks.
            workload.Iterations = uneven && index < Amount / 20 ? 400 : 20;
            index++;
        });
        return world;
    }

    public struct Workload
    {
        public float Value;
        public int Iterations;
    }

    public struct UpdateWorkload : IForEach<Workload>
    {
        public void Update(ref Workload workload)
        {
            var value = workload.Value;
            for (var iteration = 0; iteration < workload.Iterations; iteration++)
            {
                value = value * 0.9999f + 0.001f;
            }
            workload.Value = value;
        }
    }
}
