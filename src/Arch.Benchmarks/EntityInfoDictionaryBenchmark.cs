using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core;
using Arch.Core.Utils;
using Collections.Pooled;
using BenchmarkDotNet.Attributes;

namespace Arch.Benchmarks;

[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class EntityInfoDictionaryBenchmark
{
    [Params(100, 1000, 2000)]
    public int N;

    private PooledDictionary<int, EntityInfo> _items1;
    private JaggedArray<EntityInfo> _items2;

    [GlobalSetup]
    public void Setup()
    {
        _items2 = new JaggedArray<EntityInfo>(N);
        _items1 = new PooledDictionary<int, EntityInfo>(N);
    }

    [Benchmark(Baseline = true)]
    public void PooledDictionary()
    {
        var items = _items1;
        for (int i = 0; i < N; i++)
        {
            items[i] = default;
        }
    }

    [Benchmark]
    public void CustomDictionary()
    {
        var items = _items2;
        for (var i = 0; i < N; i++)
        {
            items[i] = default;
        }
    }
}
