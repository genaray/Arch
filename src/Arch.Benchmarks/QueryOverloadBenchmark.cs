using System.Numerics;
using System.Runtime.InteropServices;
using Arch.Core;

namespace Arch.Benchmarks;

/// <summary>
///     Compares the ways to run a query: with a <see cref="QueryDescription"/> or a cached <see cref="Query"/> on a tiny world,
///     where the lookup of the query is what costs, and entity by entity or chunk by chunk on a big world, where the iteration is.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class QueryOverloadBenchmark
{
    [Params(10, 10000, 1000000)] public int Amount;

    private static readonly QueryDescription _description = new QueryDescription().WithAll<Transform, Velocity>();

    private World _world;
    private Query _query;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        _world.Create<Transform, Velocity>(Amount, new Transform(), new Velocity { X = 1, Y = 1 });
        _query = _world.Query(in _description);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    [Benchmark(Baseline = true)]
    public void InlineQueryWithDescription()
    {
        var update = new VelocityUpdate();
        _world.InlineQuery<VelocityUpdate, Transform, Velocity>(in _description, ref update);
    }

    [Benchmark]
    public void InlineQueryWithCachedQuery()
    {
        var update = new VelocityUpdate();
        _world.InlineQuery<VelocityUpdate, Transform, Velocity>(_query, ref update);
    }

    [Benchmark]
    public void InlineChunkQueryWithCachedQuery()
    {
        var update = new VelocityChunkUpdate();
        _world.InlineChunkQuery<VelocityChunkUpdate, Transform, Velocity>(_query, ref update);
    }

    public struct VelocityUpdate : IForEach<Transform, Velocity>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Transform t, ref Velocity v)
        {
            t.X += v.X;
            t.Y += v.Y;
        }
    }

    /// <summary>
    ///     Does the same as <see cref="VelocityUpdate"/>, but adds the floats of whole chunks with SIMD.
    /// </summary>
    public struct VelocityChunkUpdate : IChunkForEach<Transform, Velocity>
    {
        public void Update(Span<Transform> transforms, Span<Velocity> velocities)
        {
            var positions = MemoryMarshal.Cast<Transform, float>(transforms);
            var deltas = MemoryMarshal.Cast<Velocity, float>(velocities);

            var index = 0;
            for (; index <= positions.Length - Vector<float>.Count; index += Vector<float>.Count)
            {
                var sum = new Vector<float>(positions.Slice(index)) + new Vector<float>(deltas.Slice(index));
                sum.CopyTo(positions.Slice(index));
            }

            for (; index < positions.Length; index++)
            {
                positions[index] += deltas[index];
            }
        }
    }
}
