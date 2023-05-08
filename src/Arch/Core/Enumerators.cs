using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
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
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
    /// </summary>
    /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
    /// <param name="length">Its length or size.</param>
    public Enumerator(Span<T> span, int length)
    {
#if NET7_0_OR_GREATER
        _ptr = ref MemoryMarshal.GetReference(span);
#else
        _ptr = new Ref<T>(ref span.DangerousGetReference());
#endif
        _length = length;
        _index = _length;
    }

    /// <summary>
    ///     Moves to the next item.
    /// </summary>
    /// <returns>True if there still items, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(--_index) >= 0;
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = _length;
    }

    /// <summary>
    ///     Returns a reference to the current item.
    /// </summary>
    public readonly ref T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    private readonly Query _query;
    private Enumerator<Archetype> _archetypes;

        /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeEnumerator"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Archetype"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which are checked using the <see cref="Query"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = new Enumerator<Archetype>(archetypes);
    }

    /// <summary>
    ///     Moves to the next <see cref="Archetype"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Archetype"/>, otherwhise false.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        while (_archetypes.MoveNext())
        {
            var archetype = _archetypes.Current;
            if (archetype.Size > 0 && _query.Valid(archetype.BitSet))
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeIterator"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryArchetypeEnumerator"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryArchetypeEnumerator"/> will use.</param>
    public QueryArchetypeIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryArchetypeEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryArchetypeEnumerator"/> instance.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator GetEnumerator()
    {
        return new QueryArchetypeEnumerator(_query, _archetypes);
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
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Chunk"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator(Query query, Span<Archetype> archetypes)
    {
        _archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);

        // Make it move once, otherwhise we can not check directly for Current.Size which results in bad behaviour
        if (_archetypeEnumerator.MoveNext())
        {
            _index = _archetypeEnumerator.Current.Size;
        }
    }

    /// <summary>
    ///     Moves to the next <see cref="Chunk"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Chunk"/>, otherwhise false.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            // Decrease chunk till its zero, skip empty chunks -> otherwhise entity query might fail since it tries to acess that chunk
            if (--_index >= 0 && Current.Size > 0)
            {
                return true;
            }

            // Return false if there no new archetypes
            if (!_archetypeEnumerator.MoveNext())
            {
                return false;
            }

            _index = _archetypeEnumerator.Current.Size-1;
            return true;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();

        // Make it move once, otherwhise we can not check directly for Current.Size which results in bad behaviour
        if (_archetypeEnumerator.MoveNext())
        {
            _index = _archetypeEnumerator.Current.Size;
        }
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Chunk"/>.
    /// </summary>
    public readonly ref Chunk Current
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkIterator"/> struct
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryChunkEnumerator"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryChunkEnumerator"/> will use.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryChunkEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryChunkEnumerator"/> instance.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_query, _archetypes);
    }
}

/// <summary>
///     The <see cref="QueryChunkEnumerator"/> struct
///     represents an enumerator with which one can iterate over all non empty <see cref="Chunk"/>'s that matches the given <see cref="Query"/>.
/// </summary>
[SkipLocalsInit]
public ref struct ChunkRangeEnumerator
{
    private Archetype _archetype;

    private int _chunkIndex;
    private int _toChunkIndex;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkEnumerator"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Chunk"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ChunkRangeEnumerator(Archetype archetype, int from, int to)
    {
        _archetype = archetype;
        _chunkIndex = from;
        _toChunkIndex = to;
    }

    /// <summary>
    ///     Moves to the next <see cref="Chunk"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Chunk"/>, otherwhise false.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetype.GetChunk(_chunkIndex);
    }
}

/// <summary>
///     The <see cref="QueryChunkIterator"/> struct
///     represents an iterator wich wraps the <see cref="QueryChunkEnumerator"/> for using it in foreach loops.
/// </summary>
[SkipLocalsInit]
public readonly ref struct ChunkRangeIterator
{
    private readonly Archetype _archetype;
    private readonly int _from;
    private readonly int _to;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkIterator"/> struct
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryChunkEnumerator"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryChunkEnumerator"/> will use.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ChunkRangeIterator(Archetype archetype, int from, int to)
    {
        _archetype = archetype;
        this._from = from;
        this._to = to;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryChunkEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryChunkEnumerator"/> instance.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ChunkRangeEnumerator GetEnumerator()
    {
        return new ChunkRangeEnumerator(_archetype, _from, _to);
    }
}

/// <summary>
///     The <see cref="EntityEnumerator"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Entity"/>'s in a given <see cref="Chunk"/>.
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityEnumerator(int length)
    {
        _length = length;
        _index = _length;
    }

    /// <summary>
    ///     Moves to the next <see cref="Entity"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Entity"/>, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(--_index >= 0);
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = _length;
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Entity"/>.
    /// </summary>
    public int Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityIterator(int length)
    {
        _length = length;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="EntityEnumerator"/>.
    /// </summary>
    /// <returns>The new <see cref="EntityEnumerator"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityEnumerator GetEnumerator()
    {
        return new EntityEnumerator(_length);
    }
}

/// <summary>
///     The <see cref="RangeEnumerator"/> struct
///     is sed to iterate over sections of a range to split them into pieces.
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _jobs;
    }

    /// <summary>
    ///     Resets the instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RangeEnumerator GetEnumerator()
    {
        return new RangeEnumerator(_threads, _size);
    }
}

