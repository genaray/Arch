using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
[HardwareCounters(HardwareCounter.CacheMisses)]
public class ChunkBenchmark
{
    private readonly ComponentType[] _group = { typeof(Transform), typeof(Rotation) };

    [Params(10000,100000,1000000 )] public int Amount;

    private Chunk _firstChunk;
    private Chunk _secondChunk;

    private Chunk _thirdChunk;
    private Chunk _fourthChunk;

    [GlobalSetup]
    public void Setup()
    {

        _firstChunk = new Chunk(Amount, _group);
        _secondChunk = new Chunk(Amount, _group);

        _thirdChunk = new Chunk(Amount, _group);
        _fourthChunk = new Chunk(Amount, _group);

        for (var index = 0; index < Amount; index++)
        {
            _firstChunk.Add(new Entity());
            _firstChunk.Set(index, new Transform{ X = 1, Y = 1 }, new Rotation{ X = 1, Y = 1, W = 1, Z = 1});
            _secondChunk.Set(index, new Transform{ X = 2, Y = 2 }, new Rotation{ X = 2, Y = 2, W = 2, Z = 2});
        }

        for (var index = 0; index < Amount; index++)
        {
            _thirdChunk.Add(new Entity());
            _thirdChunk.Set(index, new Transform{ X = 1, Y = 1 }, new Rotation{ X = 1, Y = 1, W = 1, Z = 1});
            _fourthChunk.Set(index, new Transform{ X = 2, Y = 2 }, new Rotation{ X = 2, Y = 2, W = 2, Z = 2});
        }

        //Console.WriteLine("TEST");
    }

    /*
    [Benchmark]
    public void Transfer()
    {
        //Console.WriteLine("TRANSFER");
        for (var index = 0; index < Amount; index++)
        {
            _secondChunk.Transfer(index, ref _firstChunk);
        }
    }

    [Benchmark]
    public void CoolerTransfer()
    {
        //Console.WriteLine("Cooler TRANSFER");
        for (var index = 0; index < Amount; index++)
        {
            _fourthChunk.CoolerTransfer(index, ref _thirdChunk);
        }
    }*/
}
