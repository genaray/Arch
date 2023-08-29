using System.Numerics;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

public class BitSetBenchmark
{
    private BitSet _firstBitset;
    private BitSet _secondBitSet;

    private BitSet _firstBitSetWithPadding;
    private BitSet _secondBitSetWithPadding;

    /*
    [GlobalSetup]
    public void Setup()
    {
        _firstBitset = new BitSet(0);
        _firstBitset.EnablePadding = false;
        _firstBitset.SetBit(3);
        _firstBitset.SetBit(5);
        //_firstBitset.SetBit(60);
        //_firstBitset.SetBit(128);
        _firstBitset.SetBit(256);

        _secondBitSet = new BitSet(0);
        _secondBitSet.EnablePadding = false;
        _secondBitSet.SetBit(3);
        _secondBitSet.SetBit(5);
        //_secondBitSet.SetBit(60);
        //_secondBitSet.SetBit(128);
        _secondBitSet.SetBit(256);

        _firstBitSetWithPadding = new BitSet();
        _firstBitSetWithPadding.EnablePadding = true;
        _firstBitSetWithPadding.SetBit(3);
        _firstBitSetWithPadding.SetBit(5);
        //_firstBitSetWithPadding.SetBit(60);
        //_firstBitSetWithPadding.SetBit(128);
        _firstBitSetWithPadding.SetBit(256);

        _secondBitSetWithPadding = new BitSet();
        _secondBitSetWithPadding.EnablePadding = true;
        _secondBitSetWithPadding.SetBit(3);
        _secondBitSetWithPadding.SetBit(5);
        //_secondBitSetWithPadding.SetBit(60);
        //_secondBitSetWithPadding.SetBit(128);
        _secondBitSetWithPadding.SetBit(256);
    }

    [Benchmark(Baseline = true)]
    public void Baseline()
    {
        _firstBitset.All(_secondBitSet);
    }

    [Benchmark]
    public void Vectorized()
    {
        _firstBitSetWithPadding.AllVectorized(_secondBitSetWithPadding);
    }*/
}
