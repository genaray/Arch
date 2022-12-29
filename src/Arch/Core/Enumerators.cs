using Arch.Core.Extensions;

namespace Arch.Core;

// NOTE: Should this have a different name to avoid confusion with existing .NET `Enumerator` APIs?
// TODO: Documentation.
/// <summary>
///     The <see cref="Enumerator{T}"/> struct
///     ...
/// </summary>
/// <typeparam name="T"></typeparam>
public ref struct Enumerator<T>
{
    private readonly Span<T> _span;

    private int _index;
    private readonly int _size;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct
    ///     ...
    /// </summary>
    /// <param name="span"></param>
    public Enumerator(Span<T> span)
    {
        _span = span;
        _index = -1;
        _size = span.Length;
    }

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct
    ///     ...
    /// </summary>
    /// <param name="span"></param>
    /// <param name="length"></param>
    public Enumerator(Span<T> span, int length)
    {
        _span = span;
        _index = -1;
        _size = length;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _size;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public readonly ref T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _span[_index];
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryArchetypeEnumerator"/> struct
///     ...
/// </summary>
public ref struct QueryArchetypeEnumerator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    private readonly int _size;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeEnumerator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
        Index = -1;
        _size = archetypes.Length;
    }

    internal int Index;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            while (++Index < _size)
            {
                ref var archetype = ref Current;
                if (archetype.Size > 0 && _query.Valid(archetype.BitSet))
                {
                    return true;
                }
            }

            return false;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        Index = -1;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public readonly ref Archetype Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetypes[Index];
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryArchetypeIterator"/> struct
///     ...
/// </summary>
public readonly ref struct QueryArchetypeIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeIterator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    public QueryArchetypeIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator GetEnumerator()
    {
        return new QueryArchetypeEnumerator(_query, _archetypes);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryChunkEnumerator"/> struct
///     ...
/// </summary>
public ref struct QueryChunkEnumerator
{
    private QueryArchetypeEnumerator _archetypeEnumerator;
    private int _index;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkEnumerator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            --_index;

            // We reached the end, next archetype
            if (_index >= 0)
            {
                return true;
            }

            if (!_archetypeEnumerator.MoveNext())
            {
                return false;
            }

            _index = _archetypeEnumerator.Current.Size - 1;

            return true;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public readonly ref Chunk Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetypeEnumerator.Current.GetChunk(_index);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryChunkIterator"/> struct
///     ...
/// </summary>
public readonly ref struct QueryChunkIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryChunkIterator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_query, _archetypes);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryEntityEnumerator"/> struct
///     ...
/// </summary>
public ref struct QueryEntityEnumerator
{
    private QueryArchetypeEnumerator _archetypeEnumerator;
    private Span<Chunk> _chunks;

    private int _index;
    private int _size;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityEnumerator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            _index++;

            // We reached the end, next archetype
            if (_index < _size)
            {
                return true;
            }

            if (!_archetypeEnumerator.MoveNext())
            {
                return false;
            }

            ref var current = ref _archetypeEnumerator.Current;
            _chunks = new Span<Chunk>(current.Chunks);
            _index = 0;
            _size = current.Size;

            return true;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public readonly ref Chunk Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _chunks[_index];
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryEntityIterator"/> struct
///     ...
/// </summary>
public readonly ref struct QueryEntityIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityIterator"/> struct
    ///     ...
    /// </summary>
    /// <param name="query"></param>
    /// <param name="archetypes"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_query, _archetypes);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="RangeEnumerator"/> struct
///     ...
/// </summary>
public ref struct RangeEnumerator
{
    private readonly int _size;

    private readonly int _jobs;
    private readonly int _perJob;

    private int _index;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="RangeEnumerator"/> struct
    ///     ...
    /// </summary>
    /// <param name="threads"></param>
    /// <param name="size"></param>
    public RangeEnumerator(int threads, int size)
    {
        _size = size;
        _index = -1;

        JobExtensions.PartionateArray(threads, size, out _jobs, out _perJob);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _jobs;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public Range Current
    {
        get => new(_index * _perJob, AmountForJob(_index));
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="RangePartitioner"/> struct
///     ...
/// </summary>
public readonly ref struct RangePartitioner
{
    private readonly int _threads;
    private readonly int _size;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="RangePartitioner"/> struct
    ///     ...
    /// </summary>
    /// <param name="threads"></param>
    /// <param name="size"></param>
    public RangePartitioner(int threads, int size)
    {
        _threads = threads;
        _size = size;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RangeEnumerator GetEnumerator()
    {
        return new RangeEnumerator(_threads, _size);
    }
}

