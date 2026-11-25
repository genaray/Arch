using Arch.Core;
using Schedulers;

namespace Arch.Benchmarks;

/// <summary>
///     Measures parallel queries against the serial ones for different layouts of a world:
///     one large archetype, a large archetype with many unused chunks, many small archetypes, and a world too small to be worth the threads.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class ParallelQueryBenchmark
{
    private static readonly QueryDescription _description = new QueryDescription().WithAll<Transform, Velocity>();
    private static readonly Type[] _tags = [typeof(Health), typeof(Rotation), typeof(Position2D), typeof(Position3D), typeof(TagA), typeof(TagB)];

    private struct TagA { }
    private struct TagB { }

    private JobScheduler _jobScheduler;
    private World _large;
    private World _small;
    private World _withUnusedChunks;
    private World _manyArchetypes;

    [GlobalSetup]
    public void Setup()
    {
        _jobScheduler = new JobScheduler(new JobScheduler.Config
        {
            ThreadPrefixName = "Arch.Benchmarks",
            ThreadCount = 0,
            MaxExpectedConcurrentJobs = 64,
            StrictAllocationMode = false
        });
        World.SharedJobScheduler = _jobScheduler;

        _large = World.Create();
        _large.Create<Transform, Velocity>(1_000_000);

        _small = World.Create();
        _small.Create<Transform, Velocity>(2_000);

        // Capacity for ten times the entities that exist, so most chunks are empty.
        _withUnusedChunks = World.Create();
        _withUnusedChunks.EnsureCapacity<Transform, Velocity>(1_000_000);
        _withUnusedChunks.Create<Transform, Velocity>(100_000);

        // One archetype for every combination of the tags, each with a few entities.
        _manyArchetypes = World.Create();
        for (var combination = 1; combination < 1 << _tags.Length; combination++)
        {
            var types = new List<ComponentType> { typeof(Transform), typeof(Velocity) };
            for (var tag = 0; tag < _tags.Length; tag++)
            {
                if ((combination & (1 << tag)) != 0)
                {
                    types.Add(_tags[tag]);
                }
            }

            _manyArchetypes.Create(new Span<Entity>(new Entity[1600]), new Signature(types.ToArray()), 1600);
        }
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.SharedJobScheduler = null;
        _jobScheduler.Dispose();
        World.Destroy(_large);
        World.Destroy(_small);
        World.Destroy(_withUnusedChunks);
        World.Destroy(_manyArchetypes);
    }

    [Benchmark]
    public void LargeSerial() => Run(_large);

    [Benchmark]
    public void LargeParallel() => RunParallel(_large);

    [Benchmark]
    public void SmallSerial() => Run(_small);

    [Benchmark]
    public void SmallParallel() => RunParallel(_small);

    [Benchmark]
    public void UnusedChunksSerial() => Run(_withUnusedChunks);

    [Benchmark]
    public void UnusedChunksParallel() => RunParallel(_withUnusedChunks);

    [Benchmark]
    public void ManyArchetypesSerial() => Run(_manyArchetypes);

    [Benchmark]
    public void ManyArchetypesParallel() => RunParallel(_manyArchetypes);

    private static void Run(World world)
    {
        var update = new Work();
        world.InlineQuery<Work, Transform, Velocity>(in _description, ref update);
    }

    private static void RunParallel(World world)
    {
        var update = new Work();
        world.InlineParallelQuery<Work, Transform, Velocity>(in _description, ref update);
    }

    /// <summary>
    ///     Does some work per entity, so the time is not only spent waiting for the memory.
    /// </summary>
    public struct Work : IForEach<Transform, Velocity>
    {
        public void Update(ref Transform transform, ref Velocity velocity)
        {
            var x = transform.X;
            for (var index = 0; index < 20; index++)
            {
                x = (x * 1.0001f) + velocity.X;
            }

            transform.X = x;
        }
    }
}
