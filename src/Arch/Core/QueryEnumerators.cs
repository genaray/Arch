using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

[SkipLocalsInit]
public ref struct QueryEntityEnumerator
{
#if CHANGED_FLAGS
    private readonly bool _checkChanged;
    private readonly BitSet _changedMask;
#endif

    private QueryChunkEnumerator _chunkEnumerator;
    private Ref<Entity> _entityPtr;
    private Ref<Chunk> _chunk;
    private int _entityIndex = -1;

    public QueryEntityEnumerator(Query query)
    {
#if CHANGED_FLAGS
        _checkChanged = query.HasChangedFilter;
        _changedMask = query.Changed;
#endif
        _chunkEnumerator = query.GetEnumerator();
    }

    public readonly ref Entity Current
    {
        get => ref Unsafe.Add(ref _entityPtr.Value, _entityIndex);
    }

    public bool MoveNext()
    {
#if CHANGED_FLAGS
        if (_checkChanged)
        {
            while (true)
            {
                // Move to the next entity in the current chunk
                if (MoveNextChangedEntity())
                {
                    return true; // Return true if the next entity is valid
                }

                // Move to the next chunk in the query
                if (!MoveNextChangedChunk())
                {
                    return false; // Return false if there's no more chunks
                }
            }
        }
#endif

        while (true)
        {
            // Move to the next entity in the current chunk
            if (MoveNextEntity())
            {
                return true; // Return true if the next entity is valid
            }

            // Move to the next chunk in the query
            if (!MoveNextChunk())
            {
                return false; // Return false if there's no more chunks
            }
        }
    }

    public QueryEntityEnumerator GetEnumerator()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChunk()
    {
        while (_chunkEnumerator.MoveNext())
        {
            ref var chunk = ref _chunkEnumerator.Current;

            // Skip empty chunks
            if (chunk.Count <= 0)
            {
                continue;
            }

            _chunk = new Ref<Chunk>(ref chunk);
            _entityIndex = chunk.Count;
            _entityPtr = new Ref<Entity>(ref chunk.Entity(0));
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextEntity()
    {
        unchecked
        {
            return --_entityIndex >= 0;
        }
    }

#if CHANGED_FLAGS

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChangedChunk()
    {
        while (_chunkEnumerator.MoveNext())
        {
            ref var chunk = ref _chunkEnumerator.Current;

            // Skip empty and non-changed chunks
            if (chunk.Count <= 0 || !chunk.IsAnyChanged(_changedMask))
            {
                continue;
            }

            _chunk = new Ref<Chunk>(ref chunk);
            _entityIndex = chunk.Count;
            _entityPtr = new Ref<Entity>(ref chunk.Entity(0));
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChangedEntity()
    {
        unchecked
        {
            // Skip non-changed entities
            return --_entityIndex >= 0 && _chunk.Value.IsChanged(_entityIndex, _changedMask);
        }
    }

#endif
}

[SkipLocalsInit]
public ref struct QueryComponentEnumerator<T>
{
#if CHANGED_FLAGS
    private readonly bool _checkChanged;
    private readonly BitSet _changedMask;
#endif

    private QueryChunkEnumerator _chunkEnumerator;
    private Ref<T> _cmpPtr;
    private Ref<Chunk> _chunkPtr;
    private int _entityIndex = -1;

    public QueryComponentEnumerator(Query query)
    {
#if CHANGED_FLAGS
        _checkChanged = query.HasChangedFilter;
        _changedMask = query.Changed;
#endif
        _chunkEnumerator = query.GetEnumerator();
    }

    public readonly ref T Current
    {
        get => ref Unsafe.Add(ref _cmpPtr.Value, _entityIndex);
    }

    public bool MoveNext()
    {
#if CHANGED_FLAGS
        if (_checkChanged)
        {
            while (true)
            {
                // Move to the next entity in the current chunk
                if (MoveNextChangedEntity())
                {
                    return true; // Return true if the next entity is valid
                }

                // Move to the next chunk in the query
                if (!MoveNextChangedChunk())
                {
                    return false; // Return false if there's no more chunks
                }
            }
        }
#endif

        while (true)
        {
            // Move to the next entity in the current chunk
            if (MoveNextEntity())
            {
                return true; // Return true if the next entity is valid
            }

            // Move to the next chunk in the query
            if (!MoveNextChunk())
            {
                return false; // Return false if there's no more chunks
            }
        }
    }

    public QueryComponentEnumerator<T> GetEnumerator()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChunk()
    {
        while (_chunkEnumerator.MoveNext())
        {
            ref var chunk = ref _chunkEnumerator.Current;

            // Skip empty chunks
            if (chunk.Count <= 0)
            {
                continue;
            }

            _chunkPtr = new Ref<Chunk>(ref chunk);
            _entityIndex = chunk.Count;
            _cmpPtr = new Ref<T>(ref chunk.GetFirst<T>());
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextEntity()
    {
        unchecked
        {
            return --_entityIndex >= 0;
        }
    }

#if CHANGED_FLAGS

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChangedChunk()
    {
        while (_chunkEnumerator.MoveNext())
        {
            ref var chunk = ref _chunkEnumerator.Current;

            // Skip empty and non-changed chunks
            if (chunk.Count <= 0 || !chunk.IsAnyChanged(_changedMask))
            {
                continue;
            }

            _chunkPtr = new Ref<Chunk>(ref chunk);
            _entityIndex = chunk.Count;
            _cmpPtr = new Ref<T>(ref chunk.GetFirst<T>());
            return true;
        }

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool MoveNextChangedEntity()
    {
        unchecked
        {
            // Skip non-changed entities
            return --_entityIndex >= 0 && _chunkPtr.Value.IsChanged(_entityIndex, _changedMask);
        }
    }

#endif
}


