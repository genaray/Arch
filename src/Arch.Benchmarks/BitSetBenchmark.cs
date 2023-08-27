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
    }

    [Benchmark(Baseline = true)]
    public void Baseline()
    {
        _firstBitset.All(_secondBitSet);
    }

    [Benchmark]
    public void Vectorized()
    {/*
        var vectorSize = Vector<uint>.Count;
        var iterations = Math.Min(Length, other.Length) / vectorSize;

        // Iterate the arrays, vectorize and compare them
        for (var i = 0; i < iterations; i++)
        {
            var vector = new Vector<uint>(_bits, i * vectorSize);
            var otherVector = new Vector<uint>(other._bits, i * vectorSize);

            var resultVector = Vector.Equals(vector, otherVector);
            if (!Vector.EqualsAll(resultVector, Vector<uint>.One))
            {
                return false;
            }
        }

        // Handle extra bits on our side that might just be all zero.
        var bitCount = _bits.Length;
        for (var i = iterations; i < bitCount; i++)
        {
            if (_bits[i] != 0)
            {
                return false;
            }
        }

        return true;*/
    }
}
