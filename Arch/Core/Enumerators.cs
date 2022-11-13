using System;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;

namespace Arch.Core; 

/// <summary>
/// A basic enumerator for arrays or spans. 
/// </summary>
/// <typeparam name="T">The generic type</typeparam>
public ref struct Enumerator<T> {
    
    private readonly Span<T> span;

    private int index;
    private readonly int size;

    public Enumerator(Span<T> span) {

        this.span = span;
        
        index = -1;
        size = span.Length;
    }
    
    public Enumerator(Span<T> span, int length) {

        this.span = span;
        
        index = -1;
        size = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() {
        return unchecked(++index) < size;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() { index = -1; }

    public readonly ref T Current {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref span[index];
    }
}

/// <summary>
/// A enumerator which accepts a <see cref="Query"/> and will only iterate over fitting <see cref="Archetype"/>'s. 
/// </summary>
public ref struct QueryArchetypeEnumerator {

    private readonly Query query;
    private readonly Span<Archetype> archetypes;

    private int index;
    private readonly int size;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator(Query query, Span<Archetype> archetypes) {
        this.query = query;
        this.archetypes = archetypes;
        index = -1;
        size = archetypes.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() {

        unchecked {
            
            while (++index < size) {
                ref var archetype = ref Current;
                if (query.Valid(archetype.BitSet)) 
                    return true;
            }

            return false;
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() { index = -1; }

    public readonly ref Archetype Current {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref archetypes[index];
    }
}

/// <summary>
/// A iterator for the <see cref="QueryArchetypeEnumerator"/> to use it in a foreach loop. 
/// </summary>
public readonly ref struct QueryArchetypeIterator {

    private readonly Query query;
    private readonly Span<Archetype> archetypes;
    
    public QueryArchetypeIterator(Query query, Span<Archetype> archetypes) {
        this.query = query;
        this.archetypes = archetypes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeEnumerator GetEnumerator() {
        return new QueryArchetypeEnumerator(query, archetypes);
    }
}

/// <summary>
/// A enumerator to iterate over <see cref="Chunk"/>'s fitting the <see cref="Query"/>.
/// </summary>
public ref struct QueryChunkEnumerator {

    private QueryArchetypeEnumerator archetypeEnumerator;
    private Span<Chunk> chunks;

    private int index;
    private int size;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator(Query query, Span<Archetype> archetypes) {
        index = -1;
        archetypeEnumerator = new QueryArchetypeEnumerator(query, archetypes);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() {

        unchecked {

            index++;
            
            // We reached the end, next archetype
            if (index < size) return true;
            if (!archetypeEnumerator.MoveNext()) return false;

            ref var current = ref archetypeEnumerator.Current;
            chunks = new Span<Chunk>(current.Chunks);
            index = 0;
            size = current.Size;

            return true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() {
        
        index = -1; 
        archetypeEnumerator.Reset();
    }

    public readonly ref Chunk Current {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref chunks[index];
    }
}

/// <summary>
/// A implementation of the <see cref="QueryChunkEnumerator"/> in order to use it in a foreach loop. 
/// </summary>
public readonly ref struct QueryChunkIterator {

    private readonly Query query;
    private readonly Span<Archetype> archetypes;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator(Query query, Span<Archetype> archetypes) {
        this.query = query;
        this.archetypes = archetypes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkEnumerator GetEnumerator() {
        return new QueryChunkEnumerator(query, archetypes);
    }
}

/// <summary>
/// A struct used to split an array into multiple <see cref="Range"/>'s. 
/// </summary>
public ref struct RangeEnumerator {
    
    private readonly int size;

    private readonly int jobs;
    private readonly int perJob;

    private int index;
    
    public RangeEnumerator(int threads, int size) {
        
        this.size = size;
        index = -1;
        
        JobExtensions.PartionateArray(threads, size,  out jobs, out perJob);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int AmountForJob(int i) {
        
        if (i <= 0) return perJob;
        if(i == jobs-1) return (int)Math.Ceiling((float)(size % jobs));
        return perJob;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool MoveNext() {
        return (unchecked(++index) < jobs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() { index = -1; }
    
    public Range Current => new(index*perJob, AmountForJob(index));
}

/// <summary>
/// A implementation of the <see cref="RangeEnumerator"/> for being used in a foreach loop. 
/// </summary>
public readonly ref struct RangePartitioner {

    private readonly int threads;
    private readonly int size;
    
    public RangePartitioner(int threads, int size) {
        this.threads = threads;
        this.size = size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RangeEnumerator GetEnumerator() {
        return new RangeEnumerator(threads, size);
    }
}

