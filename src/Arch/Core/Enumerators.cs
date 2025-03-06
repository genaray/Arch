using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

// NOTE: Should this have a different name to avoid confusion with existing .NET `Enumerator` APIs?
/// <summary>
///     The <see cref="Enumerator{T}"/> struct
///     represents a backward enumerator with which one can iterate over all items of an array or span.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
[SkipLocalsInit]
public ref struct Enumerator<T>
{

#if NET7_0_OR_GREATER
    private readonly ref T _ptr;
#else
    private readonly Ref<T> _ptr;
#endif

    private int _index;
    private readonly int _length;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
    /// </summary>
    /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
    public Enumerator(Span<T> span)
    {

#if NET7_0_OR_GREATER
        _ptr = ref MemoryMarshal.GetReference(span);
#else
        _ptr = new Ref<T>(ref span.DangerousGetReference());
#endif

        _length = span.Length;
        _index = _length;
    }

    /// <summary>
    ///     Moves to the next item.
    /// </summary>
    /// <returns>True if there still items, otherwise false.</returns>

    public bool MoveNext()
    {
        return unchecked(--_index) >= 0;
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>

    public void Reset()
    {
        _index = _length;
    }

    /// <summary>
    ///     Returns a reference to the current item.
    /// </summary>
    public readonly ref T Current
    {

        get
        {

#if NET7_0_OR_GREATER
            return ref Unsafe.Add(ref _ptr, _index);
#else
            return ref Unsafe.Add(ref _ptr.Value, _index);
#endif
        }
    }
}

/// <summary>
///     The <see cref="QueryArchetypeEnumerator"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Archetype"/>'s that matches the given <see cref="Query"/>.
///     <remarks>
///         Uses unsafe code and references internally to allow code to inline. Spans in enumerators are not inlined.
///     </remarks>
/// </summary>
[SkipLocalsInit]
public ref struct QueryArchetypeEnumerator
{
    private Enumerator<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeEnumerator"/> struct.
    /// </summary>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which are checked using the <see cref="Query"/>.</param>
    public QueryArchetypeEnumerator(Span<Archetype> archetypes)
    {
        _archetypes = new Enumerator<Archetype>(archetypes);
    }

    /// <summary>
    ///     Moves to the next <see cref="Archetype"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Archetype"/>, otherwise false.</returns>
    [SkipLocalsInit]
    public bool MoveNext()
    {
        // Caching query locally for less lookups, improved speed
        while (_archetypes.MoveNext())
        {
            var archetype = _archetypes.Current;
            if (archetype.EntityCount > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [SkipLocalsInit]
    public void Reset()
    {
        _archetypes.Reset();
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Archetype"/>.
    /// </summary>
    public readonly Archetype Current
    {
        [SkipLocalsInit]
        get => _archetypes.Current;
    }
}

/// <summary>
///     The <see cref="QueryArchetypeIterator"/> struct
///     represents an iterator wich wraps the <see cref="QueryArchetypeEnumerator"/> for using it in foreach loops.
/// </summary>
[SkipLocalsInit]
public readonly ref struct QueryArchetypeIterator
{
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeIterator"/> struct.
    /// </summary>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryArchetypeEnumerator"/> will use.</param>
    public QueryArchetypeIterator(Span<Archetype> archetypes)
    {
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryArchetypeEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryArchetypeEnumerator"/> instance.</returns>
    [SkipLocalsInit]
    public QueryArchetypeEnumerator GetEnumerator()
    {
        return new QueryArchetypeEnumerator(_archetypes);
    }
}

/// <summary>
///     The <see cref="QueryChunkEnumerator"/> struct
///     represents an enumerator with which one can iterate over all non empty <see cref="Chunk"/>'s that matches the given <see cref="Query"/>.
/// </summary>
[SkipLocalsInit]
public ref struct QueryChunkEnumerator
{
    private QueryArchetypeEnumerator _archetypeEnumerator;
    private int _index;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkEnumerator"/> struct.
    /// </summary>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [SkipLocalsInit]
    public QueryChunkEnumerator(Span<Archetype> archetypes)
    {
        _archetypeEnumerator = new QueryArchetypeEnumerator(archetypes);

        // Make it move once, otherwise we can not check directly for Current.Size which results in bad behaviour
        if (_archetypeEnumerator.MoveNext())
        {
            _index = _archetypeEnumerator.Current.Count+1;
        }
    }

    /// <summary>
    ///     Moves to the next <see cref="Chunk"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Chunk"/>, otherwise false.</returns>
    [SkipLocalsInit]
    public bool MoveNext()
    {
        unchecked
        {
            // Decrease chunk till its zero, skip empty chunks -> otherwise entity query might fail since it tries to acess that chunk
            if (--_index >= 0)
            {
                return true;
            }

            // Return false if there no new archetypes
            if (!_archetypeEnumerator.MoveNext())
            {
                return false;
            }

            _index = _archetypeEnumerator.Current.Count;
            return true;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [SkipLocalsInit]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();

        // Make it move once, otherwise we can not check directly for Current.Size which results in bad behaviour
        if (_archetypeEnumerator.MoveNext())
        {
            _index = _archetypeEnumerator.Current.Count + 1;
        }
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Chunk"/>.
    /// </summary>
    public readonly ref Chunk Current
    {
        [SkipLocalsInit]
        get => ref _archetypeEnumerator.Current.GetChunk(_index);
    }
}

/// <summary>
///     The <see cref="QueryChunkIterator"/> struct
///     represents an iterator wich wraps the <see cref="QueryChunkEnumerator"/> for using it in foreach loops.
/// </summary>
[SkipLocalsInit]
public readonly ref struct QueryChunkIterator
{
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkIterator"/> struct
    /// </summary>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryChunkEnumerator"/> will use.</param>
    [SkipLocalsInit]
    public QueryChunkIterator(Span<Archetype> archetypes)
    {
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryChunkEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryChunkEnumerator"/> instance.</returns>
    [SkipLocalsInit]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_archetypes);
    }
}

/// <summary>
///     The <see cref="ChunkRangeEnumerator"/> struct
///     represents an enumerator which can enumerate all the <see cref="Chunk"/>'s in an Archetype
/// </summary>
[SkipLocalsInit]
public ref struct ChunkRangeEnumerator
{
    private readonly Archetype _archetype;

    private int _chunkIndex;
    private readonly int _toChunkIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkEnumerator"/> struct.
    /// </summary>
    /// <param name="from">The index of the chunk to begin enumerating from</param>
    /// <param name="archetype">The <see cref="Archetype"/> to retrieve <see cref="Chunk"/>'s from.</param>
    /// <param name="to">The index of the last chunk to return</param>
    [SkipLocalsInit]
    internal ChunkRangeEnumerator(Archetype archetype, int from, int to)
    {
        _archetype = archetype;
        _chunkIndex = from;
        _toChunkIndex = to;
    }

    /// <summary>
    ///     Moves to the next <see cref="Chunk"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Chunk"/>, otherwise false.</returns>
    [SkipLocalsInit]
    public bool MoveNext()
    {
        unchecked
        {
            // Decrease chunk till its zero
            return --_chunkIndex >= _toChunkIndex;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [SkipLocalsInit]
    public void Reset()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Chunk"/>.
    /// </summary>
    public readonly ref Chunk Current
    {
        [SkipLocalsInit]

        get => ref _archetype.GetChunk(_chunkIndex);
    }
}

/// <summary>
///     The <see cref="ChunkRangeIterator"/> struct
///     represents an iterator wich wraps the <see cref="ChunkRangeEnumerator"/> for using it in foreach loops.
/// </summary>
[SkipLocalsInit]
public readonly ref struct ChunkRangeIterator
{
    private readonly Archetype _archetype;
    private readonly int _from;
    private readonly int _to;

    [SkipLocalsInit]
    public ChunkRangeIterator(Archetype archetype, int from, int to)
    {
        _archetype = archetype;
        _from = from;
        _to = to;
    }

    [SkipLocalsInit]
    public ChunkRangeEnumerator GetEnumerator()
    {
        return new ChunkRangeEnumerator(_archetype, _from, _to);
    }
}

/// <summary>
///     The <see cref="EntityEnumerator"/> struct
///     represents an enumerator which one can iterate over all <see cref="Entity"/>'s in a given <see cref="Chunk"/>.
///     Each <see cref="Entity"/> is represented by its index inside the <see cref="Chunk"/>.
/// </summary>
[SkipLocalsInit]
public ref struct EntityEnumerator
{
    private int _index;
    private readonly int _length;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityEnumerator"/> struct.
    /// </summary>
    /// <param name="length">The length/number of all <see cref="Entity"/>'s in the given <see cref="Chunk"/>.</param>
    public EntityEnumerator(int length)
    {
        _length = length;
        _index = _length;
    }

    /// <summary>
    ///     Moves to the next <see cref="Entity"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Entity"/>, otherwise false.</returns>
    public bool MoveNext()
    {
        return unchecked(--_index >= 0);
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    public void Reset()
    {
        _index = _length;
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Entity"/>.
    /// </summary>
    public int Current
    {
        get => _index;
    }
}

/// <summary>
///     The <see cref="EntityIterator"/> struct
///     represents an iterator wich iterates over all <see cref="Entity"/>'s within a <see cref="Chunk"/>.
/// </summary>
public readonly ref struct EntityIterator
{
    private readonly int _length;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityIterator"/> struct
    /// </summary>
    /// <param name="length">The length/number of all <see cref="Entity"/>'s in the given <see cref="Chunk"/>.</param>
    public EntityIterator(int length)
    {
        _length = length;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="EntityEnumerator"/>.
    /// </summary>
    /// <returns>The new <see cref="EntityEnumerator"/> instance.</returns>
    public EntityEnumerator GetEnumerator()
    {
        return new EntityEnumerator(_length);
    }
}

/// <summary>
///     The <see cref="RangeEnumerator"/> struct
///     is used to iterate over sections of a range to split them into pieces.
///     Mostly used to partition arrays.
/// </summary>
public ref struct RangeEnumerator
{
    private readonly int _size;

    private readonly int _jobs;
    private readonly int _perJob;

    private int _index;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RangeEnumerator"/> struct.
    /// </summary>
    /// <param name="threads">The amount of threads being used.</param>
    /// <param name="size">The total size of the array.</param>
    public RangeEnumerator(int threads, int size)
    {
        _size = size;
        _index = -1;

        JobExtensions.PartionateArray(threads, size, out _jobs, out _perJob);
    }

    /// <summary>
    ///     Calculates the amount for a job.
    /// </summary>
    /// <param name="i">Its index, basically the number of the job.</param>
    /// <returns>Its amount.</returns>
    public int AmountForJob(int i)
    {
        if (i <= 0)
        {
            return _perJob;
        }

        if (i == _jobs - 1)
        {
            var amount = (int)Math.Ceiling((float)(_size % _jobs)) + _perJob;
            return amount > 0 ? amount : 1;
        }

        return _perJob;
    }

    /// <summary>
    ///     Moves next.
    /// </summary>
    /// <returns>True if its still in the range, false if not.</returns>
    public bool MoveNext()
    {
        return unchecked(++_index) < _jobs;
    }

    /// <summary>
    ///     Resets the instance.
    /// </summary>
    public void Reset()
    {
        _index = -1;
    }

    /// <summary>
    ///     Returns the current range.
    /// </summary>
    public Range Current
    {
        get => new(_index * _perJob, AmountForJob(_index));
    }
}

/// <summary>
///     The <see cref="RangePartitioner"/> struct
///     represents an iterator wich wraps the <see cref="RangeEnumerator"/> for using it in foreach loops.
/// </summary>
public readonly ref struct RangePartitioner
{
    private readonly int _threads;
    private readonly int _size;

    /// <summary>
    ///     Initializes a new instance of the <see cref="RangePartitioner"/> struct
    /// </summary>
    /// <param name="threads">The amount of threads.</param>
    /// <param name="size">The size of the array.</param>
    public RangePartitioner(int threads, int size)
    {
        _threads = threads;
        _size = size;
    }

    /// <summary>
    ///     Returns a new instance of a <see cref="RangeEnumerator"/>.
    /// </summary>
    /// <returns>A new <see cref="RangeEnumerator"/>.</returns>

    public RangeEnumerator GetEnumerator()
    {
        return new RangeEnumerator(_threads, _size);
    }
}

