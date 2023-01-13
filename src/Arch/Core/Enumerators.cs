using Arch.Core.Extensions;

namespace Arch.Core;

// NOTE: Should this have a different name to avoid confusion with existing .NET `Enumerator` APIs?
/// <summary>
///     The <see cref="Enumerator{T}"/> struct
///     represents an enumerator with which one can iterate over all items of an array or span.
/// </summary>
/// <typeparam name="T">The generic type.</typeparam>
public ref struct Enumerator<T>
{
    private readonly Span<T> _span;

    private int _index;
    private readonly int _size;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
    /// </summary>
    /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
    public Enumerator(Span<T> span)
    {
        _span = span;
        _index = -1;
        _size = span.Length;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Enumerator{T}"/> struct.
    /// </summary>
    /// <param name="span">The <see cref="Span{T}"/> with items to iterate over.</param>
    /// <param name="length">Its length or size.</param>
    public Enumerator(Span<T> span, int length)
    {
        _span = span;
        _index = -1;
        _size = length;
    }

    /// <summary>
    ///     Moves to the next item.
    /// </summary>
    /// <returns>True if there still items, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        return unchecked(++_index) < _size;
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    /// <summary>
    ///     Returns a reference to the current item.
    /// </summary>
    public readonly ref T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _span[_index];
    }
}

/// <summary>
///     The <see cref="QueryArchetypeEnumerator"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Archetype"/>'s that matches the given <see cref="Query"/>.
/// </summary>
[SkipLocalsInit]
public ref struct QueryArchetypeEnumerator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    private int _index;
    private readonly int _size;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryArchetypeEnumerator"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Archetype"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which are checked using the <see cref="Query"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
        _index = -1;
        _size = archetypes.Length;
    }

    /// <summary>
    ///     Moves to the next <see cref="Archetype"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Archetype"/>, otherwhise false.</returns>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            while (++_index < _size)
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

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Archetype"/>.
    /// </summary>
    public readonly ref Archetype Current
    {
        [SkipLocalsInit]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _archetypes[_index];
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
///     The <see cref="QueryEntityEnumerator"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Chunk"/>'s that matches the given <see cref="Query"/>.
/// </summary>
public ref struct QueryEntityEnumerator
{
    private QueryChunkEnumerator _chunkEnumerator;
    private int _index;
    private Span<Entity> _entities;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityEnumerator"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Chunk"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
    }

    /// <summary>
    ///     Moves to the next <see cref="Entity"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Entity"/>, otherwhise false.</returns>
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

            if (!_chunkEnumerator.MoveNext())
            {
                return false;
            }

            ref var chunk = ref _chunkEnumerator.Current;
            _index = chunk.Size - 1;
            _entities = chunk.Entities;
            return true;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _chunkEnumerator.Reset();
    }

    /// <summary>
    ///     Returns a reference to the current <see cref="Entity"/>.
    /// </summary>
    public readonly ref Entity Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _entities[_index];
    }
}

/// <summary>
///     The <see cref="QueryEntityIterator"/> struct
///     represents an iterator wich iterates over all <see cref="Entity"/>'s within its given <see cref="Archetype"/> array.
/// </summary>
public readonly ref struct QueryEntityIterator
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityIterator"/> struct
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryEntityIterator"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryEntityIterator"/> will use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryEntityEnumerator"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryEntityEnumerator"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityEnumerator GetEnumerator()
    {
        return new QueryEntityEnumerator(_query, _archetypes);
    }
}

/// <summary>
///     The <see cref="QueryReferenceEnumerator{T}"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Chunk"/>'s that matches the given <see cref="Query"/>.
/// </summary>
public ref struct QueryReferenceEnumerator<T>
{
    private QueryChunkEnumerator _chunkEnumerator;

    private int _index;
    private Span<T> _components;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryReferenceEnumerator{T}"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Chunk"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryReferenceEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
    }

    /// <summary>
    ///     Moves to the next <see cref="Entity"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Entity"/>, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            // We reached the end, next archetype
            if (--_index >= 0)
            {
                return true;
            }

            if (!_chunkEnumerator.MoveNext())
            {
                return false;
            }

            ref var current = ref _chunkEnumerator.Current;
            _index = current.Size - 1;
            _components = current.GetSpan<T>();
            return true;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _chunkEnumerator.Reset();
    }

    /// <summary>
    ///     Returns a reference to the current component instance.
    /// </summary>
    public ref T Current
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _components[_index];
    }
}

/// <summary>
///     The <see cref="QueryReferenceIterator{T}"/> struct
///     represents an iterator wich iterates over all <see cref="Entity"/> component's within its given <see cref="Archetype"/> array.
/// </summary>
public readonly ref struct QueryReferenceIterator<T>
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryReferenceIterator{T}"/> struct
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryReferenceIterator{T}"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryReferenceIterator{T}"/> will use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryReferenceIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryReferenceIterator{T}"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryReferenceEnumerator{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryReferenceEnumerator<T> GetEnumerator()
    {
        return new QueryReferenceEnumerator<T>(_query, _archetypes);
    }
}

/// <summary>
///     The <see cref="QueryEntityReferenceEnumerator{T}"/> struct
///     represents an enumerator with which one can iterate over all <see cref="Entity"/>'s and their components that matches the given <see cref="Query"/>.
/// </summary>
public ref struct QueryEntityReferenceEnumerator<T>
{
    private QueryChunkEnumerator _chunkEnumerator;
    private int _index;
    private Entity[] _entities;
    private Span<T> _components;
    private EntityReferences<T> _entityReferences;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityReferenceEnumerator{T}"/> struct.
    /// </summary>
    /// <param name="query">The <see cref="Query"/> which contains a description and tells which <see cref="Chunk"/>'s fit.</param>
    /// <param name="archetypes">A <see cref="Span{T}"/> of <see cref="Archetype"/>'s which <see cref="Chunk"/>'s are checked using the <see cref="Query"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityReferenceEnumerator(Query query, Span<Archetype> archetypes)
    {
        _index = -1;
        _chunkEnumerator = new QueryChunkEnumerator(query, archetypes);
    }

    /// <summary>
    ///     Moves to the next <see cref="Entity"/>.
    /// </summary>
    /// <returns>True if theres a next <see cref="Entity"/>, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext()
    {
        unchecked
        {
            // We reached the end, next archetype
            if (--_index >= 0)
            {
                return true;
            }

            if (!_chunkEnumerator.MoveNext())
            {
                return false;
            }

            ref var current = ref _chunkEnumerator.Current;
            _index = current.Size - 1;
            _entities = current.Entities;
            _components = current.GetSpan<T>();
            return true;
        }
    }

    /// <summary>
    ///     Resets this instance.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
    {
        _index = -1;
        _chunkEnumerator.Reset();
    }

    /// <summary>
    ///     Returns a reference to the current component instance.
    /// </summary>
    public EntityReferences<T> Current
    {
        [UnscopedRef]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            ref readonly var entity = ref _entities[_index];
            ref var component = ref _components[_index];
            return new EntityReferences<T>(in entity, ref component);
        }
    }
}

/// <summary>
///     The <see cref="QueryEntityReferenceIterator{T}"/> struct
///     represents an iterator wich iterates over all <see cref="Entity"/> component's within its given <see cref="Archetype"/> array.
/// </summary>
public readonly ref struct QueryEntityReferenceIterator<T>
{
    private readonly Query _query;
    private readonly Span<Archetype> _archetypes;

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryEntityReferenceIterator{T}"/> struct
    /// </summary>
    /// <param name="query">The <see cref="Query"/> each <see cref="QueryEntityReferenceIterator{T}"/> will use.</param>
    /// <param name="archetypes">The <see cref="Archetype"/>'s each <see cref="QueryEntityReferenceIterator{T}"/> will use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityReferenceIterator(Query query, Span<Archetype> archetypes)
    {
        _query = query;
        _archetypes = archetypes;
    }

    /// <summary>
    ///     Creates a new instance of <see cref="QueryEntityReferenceEnumerator{T}"/> with the given <see cref="_query"/> and <see cref="_archetypes"/>.
    /// </summary>
    /// <returns>The new <see cref="QueryEntityReferenceEnumerator{T}"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityReferenceEnumerator<T> GetEnumerator()
    {
        return new QueryEntityReferenceEnumerator<T>(_query, _archetypes);
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

