using Arch.Core;
using Arch.Core.Utils;
using Schedulers;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     The <see cref="JobChunkDispatcherTest"/> class
///     checks that the <see cref="JobChunkDispatcher"/> hands out every used <see cref="Chunk"/> exactly once, also to several threads, and that the parallel queries which use it see all entities.
/// </summary>
[TestFixture]
public sealed class JobChunkDispatcherTest
{
    private static readonly ComponentType[] _group = [typeof(Transform), typeof(Rotation)];
    private static readonly ComponentType[] _otherGroup = [typeof(Transform), typeof(Rotation), typeof(Ai)];

    private JobScheduler _jobScheduler;
    private World _world;

    [OneTimeSetUp]
    public void SetupScheduler()
    {
        _jobScheduler = new JobScheduler(new JobScheduler.Config
        {
            ThreadPrefixName = "Arch.Tests.JobChunkDispatcher",
            ThreadCount = 0,
            MaxExpectedConcurrentJobs = 64,
            StrictAllocationMode = false,
        });

        World.SharedJobScheduler = _jobScheduler;
    }

    [OneTimeTearDown]
    public void TeardownScheduler()
    {
        World.SharedJobScheduler = null;
        _jobScheduler.Dispose();
    }

    /// <summary>
    ///     Creates a <see cref="World"/> with small chunks and spare capacity, so there are many chunks, several <see cref="Archetype"/>s and unused chunks at their end.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _world = World.Create(chunkSizeInBytes: 1024, minimumAmountOfEntitiesPerChunk: 8);
        _world.EnsureCapacity(_group, 2_000);
        _world.Create(new Span<Entity>(new Entity[500]), _group, 500);
        _world.Create(new Span<Entity>(new Entity[700]), _otherGroup, 700);
    }

    [TearDown]
    public void Teardown()
    {
        World.Destroy(_world);
    }

    [Test]
    public void RunHandsOutEveryUsedChunkOnceToAllThreads()
    {
        var query = new QueryDescription(all: [typeof(Transform)]);
        var dispatcher = new JobChunkDispatcher();
        var expectedChunks = 0;
        foreach (var archetype in _world.Query(in query).GetArchetypeIterator())
        {
            dispatcher.Add(archetype);
            expectedChunks += archetype.Count + 1;
        }

        const int threads = 8;
        dispatcher.Start(threads);

        var counter = new Counter();
        System.Threading.Tasks.Parallel.For(0, threads, _ => dispatcher.Run(new CountJob { Counter = counter }));

        That(dispatcher.ChunkCount, Is.EqualTo(expectedChunks));
        That(counter.Chunks, Is.EqualTo(expectedChunks));
        That(counter.Entities, Is.EqualTo(1_200));
    }

    [Test]
    public void ClearAllowsReusingTheDispatcher()
    {
        var query = new QueryDescription(all: [typeof(Transform)]);
        var dispatcher = new JobChunkDispatcher();
        foreach (var archetype in _world.Query(in query).GetArchetypeIterator())
        {
            dispatcher.Add(archetype);
        }

        dispatcher.Clear();
        That(dispatcher.ChunkCount, Is.EqualTo(0));
        That(dispatcher.EntityCount, Is.EqualTo(0));

        dispatcher.Add(_world.GetArchetype(_world.Create(_group)));
        dispatcher.Start(1);

        var counter = new Counter();
        dispatcher.Run(new CountJob { Counter = counter });
        That(counter.Entities, Is.EqualTo(501));
    }

    [TestCase(0, 8, 0)]
    [TestCase(4_095, 8, 0)]
    [TestCase(8_192, 8, 2)]
    [TestCase(1_000_000, 8, 8)]
    public void SmallQueriesGetNoHelpers(int entities, int availableThreads, int expectedHelpers)
    {
        using var world = World.Create();
        var archetype = world.EnsureCapacity<Transform>(entities);
        if (entities > 0)
        {
            world.Create(entities, new Transform());
        }

        var dispatcher = new JobChunkDispatcher();
        dispatcher.Add(archetype);

        That(dispatcher.GetHelperCount(availableThreads), Is.EqualTo(expectedHelpers));
    }

    [Test]
    public void ScheduledQueryProcessesAllChunks()
    {
        var query = new QueryDescription(all: [typeof(Transform)]);
        var counter = new Counter();

        var handle = _world.ScheduleInlineParallelChunkQuery(in query, new CountJob { Counter = counter });
        handle.Complete();

        That(counter.Entities, Is.EqualTo(1_200));
    }

    [Test]
    public void ParallelQueryProcessesAllChunks()
    {
        var query = new QueryDescription(all: [typeof(Transform)]);
        var counter = new Counter();

        _world.InlineParallelChunkQuery(in query, new CountJob { Counter = counter });

        That(counter.Entities, Is.EqualTo(1_200));
    }

    /// <summary>
    ///     Counts what the <see cref="CountJob"/>s see, shared by all copies of the job.
    /// </summary>
    private sealed class Counter
    {
        public int Chunks;
        public int Entities;
    }

    private struct CountJob : IChunkJob
    {
        public Counter Counter;

        public void Execute(ref Chunk chunk)
        {
            Interlocked.Increment(ref Counter.Chunks);
            Interlocked.Add(ref Counter.Entities, chunk.Count);
        }
    }
}
