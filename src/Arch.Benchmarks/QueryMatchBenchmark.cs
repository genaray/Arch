using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Benchmarks;

[HtmlExporter]
[MemoryDiagnoser]
public class QueryMatchBenchmark
{
    private const int AddedPerIteration = 64;

    private static readonly ComponentType[] _types =
    {
        typeof(byte), typeof(sbyte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long),
        typeof(ulong), typeof(float), typeof(double), typeof(decimal), typeof(Guid), typeof(DateTime), typeof(TimeSpan)
    };

    [Params(1000, 5000)] public int ArchetypeAmount;
    [Params(100, 500)] public int QueryAmount;

    private World _world;
    private Query[] _queries;
    private ComponentType[][] _addedTypes;
    private Entity[] _added;
    private int _addedCount;

    private static ComponentType[] Subset(int bits)
    {
        return _types.Where((_, index) => (bits & (1 << index)) != 0).ToArray();
    }

    private static QueryDescription CreateDescription(Random random)
    {
        var all = random.Next(_types.Length);
        var none = (all + 1 + random.Next(_types.Length - 1)) % _types.Length;
        return new QueryDescription(all: Subset(1 << all), any: Subset(random.Next(1 << _types.Length)), none: Subset(1 << none));
    }

    [GlobalSetup]
    public void Setup()
    {
        _world = World.Create();
        for (var index = 1; index <= ArchetypeAmount; index++)
        {
            _world.Create(Subset(index));
        }

        var random = new Random(0);
        var descriptions = new HashSet<QueryDescription>();
        while (descriptions.Count < QueryAmount)
        {
            descriptions.Add(CreateDescription(random));
        }

        _queries = descriptions.Select(description => _world.Query(in description)).ToArray();
        _addedTypes = Enumerable.Range(ArchetypeAmount + 1, AddedPerIteration).Select(Subset).ToArray();
        _added = new Entity[AddedPerIteration];
        MatchAll();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        World.Destroy(_world);
    }

    [IterationSetup(Target = nameof(AddArchetypeAndMatch))]
    public void RemoveAdded()
    {
        for (var index = 0; index < _addedCount; index++)
        {
            var archetype = _world.GetArchetype(_added[index]);
            _world.Destroy(_added[index]);
            _world.DestroyArchetype(archetype);
        }

        _addedCount = 0;
        MatchAll();
    }

    private void MatchAll()
    {
        foreach (var query in _queries)
        {
            _ = query.GetArchetypeIterator();
        }
    }

    [Benchmark]
    [InvocationCount(AddedPerIteration)]
    public void AddArchetypeAndMatch()
    {
        _added[_addedCount] = _world.Create(_addedTypes[_addedCount]);
        _addedCount++;
        MatchAll();
    }

    [Benchmark]
    public void Match()
    {
        MatchAll();
    }
}
