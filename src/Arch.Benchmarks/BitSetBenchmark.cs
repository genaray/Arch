using System.Numerics;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

public class BitSetBenchmark
{
    private BitSet _firstBitset;
    private BitSet _secondBitSet;

    [GlobalSetup]
    public void Setup()
    {
        _firstBitset = new BitSet();
        _firstBitset.SetBit(3);
        _firstBitset.SetBit(5);
        _firstBitset.SetBit(7);
        _firstBitset.SetBit(128);
        _firstBitset.SetBit(256);

        _secondBitSet = new BitSet();
        _secondBitSet.SetBit(3);
        _secondBitSet.SetBit(5);
        _secondBitSet.SetBit(7);
        _secondBitSet.SetBit(128);
        _secondBitSet.SetBit(256);
    }

    [Benchmark(Baseline = true)]
    public void Baseline()
    {
        _firstBitset.All(_secondBitSet);
    }

    [Benchmark]
    public void Vectorized()
    {
        //_firstBitset.AllVectorized(_secondBitSet);
    }
}
