using Arch.Buffer;
using Arch.Core;

namespace Arch.Benchmarks;

/// <summary>
///     Measures recording and playing back commands, and the same changes done directly on the world.
///     Every benchmark leaves the world as it found it, so all of them can be repeated.
/// </summary>
[HtmlExporter]
[MemoryDiagnoser]
public class CommandBufferBenchmark
{
    [Params(10000, 100000)] public int Amount;

    private static readonly ComponentType[] _group = [typeof(Transform), typeof(Velocity)];
    private static readonly QueryDescription _created = new QueryDescription().WithAll<Transform, Velocity>();

    private World _world;
    private CommandBuffer _commandBuffer;
    private Entity[] _entities;

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        _commandBuffer = new CommandBuffer(Amount);

        _entities = new Entity[Amount];
        _world.Create(_entities.AsSpan(), new Signature(typeof(Transform)), Amount);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _commandBuffer.Dispose();
        World.Destroy(_world);
    }

    [Benchmark]
    public void RecordAndPlaybackSet()
    {
        for (var index = 0; index < Amount; index++)
        {
            _commandBuffer.Set(_entities[index], new Transform { X = index });
        }

        _commandBuffer.Playback(_world);
    }

    [Benchmark]
    public void RecordAndPlaybackAddAndRemove()
    {
        for (var index = 0; index < Amount; index++)
        {
            _commandBuffer.Add(_entities[index], new Velocity { X = index });
            _commandBuffer.Add(_entities[index], new Health { Amount = index });
        }

        _commandBuffer.Playback(_world);

        for (var index = 0; index < Amount; index++)
        {
            _commandBuffer.Remove<Velocity>(_entities[index]);
            _commandBuffer.Remove<Health>(_entities[index]);
        }

        _commandBuffer.Playback(_world);
    }

    [Benchmark(Baseline = true)]
    public void AddAndRemoveDirectly()
    {
        for (var index = 0; index < Amount; index++)
        {
            _world.Add(_entities[index], new Velocity { X = index }, new Health { Amount = index });
        }

        for (var index = 0; index < Amount; index++)
        {
            _world.Remove<Velocity, Health>(_entities[index]);
        }
    }

    [Benchmark]
    public void RecordAndPlaybackCreate()
    {
        for (var index = 0; index < Amount; index++)
        {
            _commandBuffer.Create(_group);
        }

        _commandBuffer.Playback(_world);
        _world.Destroy(in _created);
    }
}
