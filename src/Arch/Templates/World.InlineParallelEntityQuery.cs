

// TODO: Move query with T0 into world.cs?

namespace Arch.Core;
public partial class World
{
    public void InlineParallelEntityQuery<T,T0>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0>
    {
        var innerJob = new IForEachWithEntityJob<T,T0>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
    public void InlineParallelEntityQuery<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>
    {
        var innerJob = new IForEachWithEntityJob<T,T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>();
        innerJob.ForEach = iForEach;

        InlineParallelChunkQuery(in description, innerJob);
    }
}
