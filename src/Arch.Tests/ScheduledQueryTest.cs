using System.Diagnostics;
using Arch.Core;
using Arch.Core.Extensions;
using Schedulers;

namespace Arch.Tests;
[TestFixture]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0061:Use block body for local function", Justification = "Space efficiency")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use block body for method", Justification = "Space efficiency")]
internal class ScheduledQueryTest
{
    public enum InlineMode
    {
        NotInline,
        Inline,
        InlineWithStructRef
    }

    [Test]
    public void ParallelQueriesPoolCorrectly()
    {
        var world = World.Create();
        world.AttachScheduler(new JobScheduler(new()
        {
            ThreadPrefixName = nameof(ScheduledQueryTest)
        }));
        var dep = new SleepJob();
        for (int i = 0; i < 1000; i++)
        {
            // entity to query
            world.Create<S1, S2>();
            // entity to ignore
            world.Create<S1, S2, N>();
        }

        int nCounter = 0;
        int counter = 0;

        // Grab from pool, ensure it actually allocates memory
        var mem = GC.GetAllocatedBytesForCurrentThread();
        var job1 = ChunkIterationJobPool<ForEachJob<S1>>.Get();
        var job2 = ChunkIterationJobPool<ForEachJob<S1>>.Get();
        var mem2 = GC.GetAllocatedBytesForCurrentThread();
        Assert.That(mem, Is.Not.EqualTo(mem2));

        // Since we never return, grab 2 more new ones from the pool.
        // They should pool themselves.
        var handle1 = world.ParallelQuery(new QueryDescription().WithAll<S1, S2>().WithNone<N>(), (ref S1 s1) => Interlocked.Increment(ref counter));
        var handle2 = world.ParallelQuery(new QueryDescription().WithAll<S1, S2, N>(), (ref S1 s1) => Interlocked.Increment(ref nCounter));

        world.Scheduler?.Flush();
        handle1.Complete();
        handle2.Complete();

        // Ensure they actually ran
        Assert.Multiple(() =>
        {
            Assert.That(nCounter, Is.EqualTo(1000));
            Assert.That(counter, Is.EqualTo(1000));
        });

        // Ensure that grabbing from the pool is now free
        mem = GC.GetAllocatedBytesForCurrentThread();
        job1 = ChunkIterationJobPool<ForEachJob<S1>>.Get();
        job2 = ChunkIterationJobPool<ForEachJob<S1>>.Get();
        mem2 = GC.GetAllocatedBytesForCurrentThread();
        Assert.That(mem, Is.EqualTo(mem2));
    }

    /// <summary>
    /// Test all the various parallel queries and their overloads, including dependency validation.
    /// Only chooses a subset of the many generic overloads: up to 3 generics.
    /// </summary>
    /// <param name="genericCount"></param>
    /// <param name="inlineMode"></param>
    /// <param name="includeEntity"></param>
    [Test, Combinatorial]
    public void GenericParallelQueriesFunction(
        [Values(-1, 0, 1, 2)] int genericCount,
        [Values(InlineMode.NotInline, InlineMode.Inline, InlineMode.InlineWithStructRef)] InlineMode inlineMode,
        [Values(true, false)] bool includeEntity)
    {
        // skip this one, it doesn't mean anything
        if (!includeEntity && genericCount == -1)
        {
            return;
        }

        var world = World.Create();
        world.AttachScheduler(new JobScheduler(new()
        {
            ThreadPrefixName = nameof(ScheduledQueryTest)
        }));

        var dep = new SleepJob();

        for (int i = 0; i < 1000; i++)
        {
            // entity to query
            world.Create<S0, S1, S2>(new() { SleepJob = dep });
            // entity to ignore
            world.Create<S0, S1, S2, N>(new() { SleepJob = dep });
        }

        static void fe0(ref S0 s0) => s0.Update();
        static void fe1(ref S0 s0, ref S1 s1) => s0.Update();
        static void fe2(ref S0 s0, ref S1 s1, ref S2 s2) => s0.Update();
        static void fee(Entity e)
        {
            ref var s0 = ref e.Get<S0>();
            s0.Update();
        }

        static void fee0(Entity e, ref S0 s0) => s0.Update();
        static void fee1(Entity e, ref S0 s0, ref S1 s1) => s0.Update();
        static void fee2(Entity e, ref S0 s0, ref S1 s1, ref S2 s2) => s0.Update();

        FE0 sfe0 = new();
        FE1 sfe1 = new();
        FE2 sfe2 = new();
        FEE sfee = new();
        FEE0 sfee0 = new();
        FEE1 sfee1 = new();
        FEE2 sfee2 = new();

        QueryDescription qd = genericCount switch
        {
            -1 => new QueryDescription().WithAll<S0>().WithNone<N>(),
            0 => new QueryDescription().WithAll<S0>().WithNone<N>(),
            1 => new QueryDescription().WithAll<S0, S1>().WithNone<N>(),
            _ => new QueryDescription().WithAll<S0, S1, S2>().WithNone<N>(),
        };

        // setup a dependency
        var dependency = world.Scheduler?.Schedule(dep);

        JobHandle? handle = null;

        if (inlineMode == InlineMode.NotInline && includeEntity)
        {
            handle = genericCount switch
            {
                -1 => world.ParallelQuery(qd, fee, dependency),
                0 => world.ParallelQuery(qd, (ForEachWithEntity<S0>)fee0, dependency),
                1 => world.ParallelQuery(qd, (ForEachWithEntity<S0, S1>)fee1, dependency),
                _ => world.ParallelQuery(qd, (ForEachWithEntity<S0, S1, S2>)fee2, dependency),
            };
        }
        else if (inlineMode == InlineMode.NotInline && !includeEntity)
        {

            handle = genericCount switch
            {
                0 => world.ParallelQuery(qd, (ForEach<S0>)fe0, dependency),
                1 => world.ParallelQuery(qd, (ForEach<S0, S1>)fe1, dependency),
                _ => world.ParallelQuery(qd, (ForEach<S0, S1, S2>)fe2, dependency),
            };
        }
        else if (inlineMode == InlineMode.InlineWithStructRef && includeEntity)
        {
            handle = genericCount switch
            {
                -1 => world.InlineParallelQuery<FEE>(qd, ref sfee, dependency),
                0 => world.InlineParallelEntityQuery<FEE0, S0>(qd, ref sfee0, dependency),
                1 => world.InlineParallelEntityQuery<FEE1, S0, S1>(qd, ref sfee1, dependency),
                _ => world.InlineParallelEntityQuery<FEE2, S0, S1, S2>(qd, ref sfee2, dependency),
            };

        }
        else if (inlineMode == InlineMode.Inline && includeEntity)
        {
            handle = genericCount switch
            {
                -1 => world.InlineParallelQuery<FEE>(qd, dependency),
                0 => world.InlineParallelEntityQuery<FEE0, S0>(qd, dependency),
                1 => world.InlineParallelEntityQuery<FEE1, S0, S1>(qd, dependency),
                _ => world.InlineParallelEntityQuery<FEE2, S0, S1, S2>(qd, dependency),
            };
        }
        else if (inlineMode == InlineMode.InlineWithStructRef && !includeEntity)
        {
            handle = genericCount switch
            {
                0 => world.InlineParallelQuery<FE0, S0>(qd, ref sfe0, dependency),
                1 => world.InlineParallelQuery<FE1, S0, S1>(qd, ref sfe1, dependency),
                _ => world.InlineParallelQuery<FE2, S0, S1, S2>(qd, ref sfe2, dependency),
            };

        }
        else if (inlineMode == InlineMode.Inline && !includeEntity)
        {
            handle = genericCount switch
            {
                0 => world.InlineParallelQuery<FE0, S0>(qd, dependency),
                1 => world.InlineParallelQuery<FE1, S0, S1>(qd, dependency),
                _ => world.InlineParallelQuery<FE2, S0, S1, S2>(qd, dependency),
            };
        }

        Debug.Assert(handle is not null);

        world.Scheduler?.Flush();
        handle.Value.Complete();

        world.Query(qd, (ref S0 s0) => Assert.That(s0.Counter, Is.EqualTo(1)));
        world.Query(new QueryDescription().WithAll<N, S0>(), (ref S0 s0) => Assert.That(s0.Counter, Is.EqualTo(0)));
    }

    private class SleepJob : IJob
    {
        public bool Complete { get; private set; } = false;
        public void Execute()
        {
            Thread.Sleep(5);
            Complete = true;
        }
    }

    private struct N { }
    private struct S0
    {
        public int Counter;
        public SleepJob SleepJob;

        public void Update()
        {
            // we can't use NUnit.Assert because it's slow
            if (!SleepJob.Complete)
            {
                throw new InvalidOperationException("Dependency didn't work!");
            }

            Interlocked.Increment(ref Counter);
        }
    }
    private struct S1 { }
    private struct S2 { }

    private struct FE0 : IForEach<S0>
    {
        public readonly void Update(ref S0 s0) => s0.Update();
    }
    private struct FE1 : IForEach<S0, S1>
    {
        public readonly void Update(ref S0 s0, ref S1 s1) => s0.Update();
    }
    private struct FE2 : IForEach<S0, S1, S2>
    {
        public readonly void Update(ref S0 s0, ref S1 s1, ref S2 s2) => s0.Update();
    }
    private struct FEE : IForEach
    {
        public readonly void Update(Entity e)
        {
            ref var s0 = ref e.Get<S0>();
            s0.Update();
        }
    }
    private struct FEE0 : IForEachWithEntity<S0>
    {
        public readonly void Update(Entity e, ref S0 s0) => s0.Update();
    }
    private struct FEE1 : IForEachWithEntity<S0, S1>
    {
        public readonly void Update(Entity e, ref S0 s0, ref S1 s1) => s0.Update();
    }
    private struct FEE2 : IForEachWithEntity<S0, S1, S2>
    {
        public readonly void Update(Entity e, ref S0 s0, ref S1 s1, ref S2 s2) => s0.Update();
    }
}
