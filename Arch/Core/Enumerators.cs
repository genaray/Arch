using System;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;

namespace Arch.Core;

/// <summary>
///     A basic enumerator for arrays or spans.
/// </summary>
/// <typeparam name="T">The generic type</typeparam>
public ref struct Enumerator<T>
{
    private readonly Span<T> _span;

    private int _index;
    private readonly int _size;

    public Enumerator(Span<T> span)
    {
        this._span = span;

        _index = -1;
        _size = span.Length;
    }

    public Enumerator(Span<T> span, int length)
    {
        this._span = span;

        _index = -1;
        _size = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    public readonly ref T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _span[_index];
    }
}

/// <summary>
///     A enumerator which accepts a <see cref="Query" /> and will only iterate over fitting <see cref="Archetype" />'s.
/// </summary>
public ref struct QueryArchetypeEnumerator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    internal int _index;
    private readonly int _size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator(Query query, Span<Archetype> archetypes)
    {
        this._query = query;
        this._archetypes = archetypes;
        _index = -1;
        _size = archetypes.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            while (++_index < _size)
            {
                ref var archetype = ref Current;
                if (archetype.Size > 0 && _query.Valid(archetype.BitSet))
                    return true;
            }

            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    public readonly ref Archetype Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetypes[_index];
    }
}

/// <summary>
///     A iterator for the <see cref="QueryArchetypeEnumerator" /> to use it in a foreach loop.
/// </summary>
public readonly ref struct QueryArchetypeIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    public QueryArchetypeIterator(Query query, Span<Archetype> archetypes)
    {
        this._query = query;
        this._archetypes = archetypes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator GetEnumerator()
    {
        return new QueryArchetypeEnumerator(_query, _archetypes);
    }
}

/// <summary>
///     A enumerator to iterate over <see cref="Chunk" />'s fitting the <see cref="Query" />.
/// </summary>
public ref struct QueryChunkEnumerator
{
    private QueryArchetypeEnumerator _archetypeEnumerator;
    private int _index;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            --_index;

            // We reached the end, next archetype
            if (_index >= 0) return true;
            if (!_archetypeEnumerator.MoveNext()) return false;
            _index = _archetypeEnumerator.Current.Size-1;

            return true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();
    }

    public readonly ref Chunk Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetypeEnumerator.Current.GetChunk(_index);
    }
}

/// <summary>
///     A implementation of the <see cref="QueryChunkEnumerator" /> in order to use it in a foreach loop.
/// </summary>
public readonly ref struct QueryChunkIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator(Query query, Span<Archetype> archetypes)
    {
        this._query = query;
        this._archetypes = archetypes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_query, _archetypes);
    }
}

/// <summary>
///     A enumerator to iterate over <see cref="Chunk" />'s fitting the <see cref="Query" />.
/// </summary>
public ref struct QueryEntityEnumerator
{
    private QueryArchetypeEnumerator _archetypeEnumerator;
    private Span<Chunk> _chunks;

    private int _index;
    private int _size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            _index++;

            // We reached the end, next archetype
            if (_index < _size) return true;
            if (!_archetypeEnumerator.MoveNext()) return false;

            ref var current = ref _archetypeEnumerator.Current;
            _chunks = new Span<Chunk>(current.Chunks);
            _index = 0;
            _size = current.Size;

            return true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _archetypeEnumerator.Reset();
    }

    public readonly ref Chunk Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _chunks[_index];
    }
}

/// <summary>
///     A implementation of the <see cref="QueryChunkEnumerator" /> in order to use it in a foreach loop.
/// </summary>
public readonly ref struct QueryEntityIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityIterator(Query query, Span<Archetype> archetypes)
    {
        this._query = query;
        this._archetypes = archetypes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator()
    {
        return new QueryChunkEnumerator(_query, _archetypes);
    }
}

/// <summary>
///     A struct used to split an array into multiple <see cref="Range" />'s.
/// </summary>
public ref struct RangeEnumerator
{
    private readonly int _size;

    private readonly int _jobs;
    private readonly int _perJob;

    private int _index;

    public RangeEnumerator(int threads, int size)
    {
        this._size = size;
        _index = -1;

        JobExtensions.PartionateArray(threads, size, out _jobs, out _perJob);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int AmountForJob(int i)
    {
        if (i <= 0) return _perJob;
        if (i == _jobs - 1)
        {
            var amount = (int)Math.Ceiling((float)(_size % _jobs))+_perJob;
            return amount > 0 ? amount : 1;
        }
        return _perJob;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _jobs;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    public Range Current => new(_index * _perJob, AmountForJob(_index));
}

/// <summary>
///     A implementation of the <see cref="RangeEnumerator" /> for being used in a foreach loop.
/// </summary>
public readonly ref struct RangePartitioner
{
    private readonly int _threads;
    private readonly int _size;

    public RangePartitioner(int threads, int size)
    {
        this._threads = threads;
        this._size = size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RangeEnumerator GetEnumerator()
    {
        return new RangeEnumerator(_threads, _size);
    }
}

