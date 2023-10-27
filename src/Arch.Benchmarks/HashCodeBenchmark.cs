using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
public class HashCodeBenchmark
{
    private byte[]? _bytes;

    [Params(1000, 10000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
        _bytes = new byte[N];
        new Random(42).NextBytes(_bytes);
    }

    [Benchmark]
    public uint MurmurHash()
    {
        return MurmurHash3.Hash32(_bytes!, 7);
    }

    [Benchmark]
    public uint HashCode()
    {
        var c = new HashCode();
        c.AddBytes(_bytes!);
        return unchecked((uint)c.ToHashCode());
    }
}
