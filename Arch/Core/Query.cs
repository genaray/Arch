using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;

namespace Arch.Core; 

/// <summary>
/// A query describtion of which entities we wanna query for.
/// </summary>
public struct QueryDescription : IEquatable<QueryDescription> {

    public Type[] All = Array.Empty<Type>();
    public Type[] Any = Array.Empty<Type>();
    public Type[] None = Array.Empty<Type>();
    
    public QueryDescription() {}

    public bool Equals(QueryDescription other) {
        return Equals(All, other.All) && Equals(Any, other.Any) && Equals(None, other.None);
    }

    public override bool Equals(object obj) {
        return obj is QueryDescription other && Equals(other);
    }

    public override int GetHashCode() {
        
        unchecked{ // Overflow is fine, just wrap{
            int hash = 17;
            hash = hash * 23 + All.GetHashCode();
            hash = hash * 23 + Any.GetHashCode();
            hash = hash * 23 + None.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(QueryDescription left, QueryDescription right) { return left.Equals(right); }
    public static bool operator !=(QueryDescription left, QueryDescription right) { return !left.Equals(right); }
}

/// <summary>
/// A constructed query used for translating the <see cref="QueryDescription"/> and validating <see cref="BitSet"/>'s to find the right chunks. 
/// </summary>
public readonly struct Query : IEquatable<Query> {

    private readonly PooledList<Archetype> Archetypes;
    
    private readonly BitSet Any;
    private readonly BitSet All;
    private readonly BitSet None;
    
    private QueryDescription QueryDescription { get; }

    internal Query(PooledList<Archetype> archetypes, QueryDescription description) : this() {

        this.Archetypes = archetypes;
        
        All = description.All.ToBitSet();
        Any = description.Any.ToBitSet();
        None = description.None.ToBitSet();
        QueryDescription = description;
        
        // Otherwhise a any value of 0 always returns false somehow
        if(description.Any.Length == 0) Any.SetAll();
    }

    /// <summary>
    /// Checks if this query is in a <see cref="BitSet"/>
    /// </summary>
    /// <param name="bitset"></param>
    /// <returns>True if the passed bitset is valid for this query</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Valid(BitSet bitset) {
        return All.All(bitset) && Any.Any(bitset) && None.None(bitset);
    }
    
    /// <summary>
    /// Returns a <see cref="QueryArchetypeIterator"/> which can be used to iterate over all fitting <see cref="Archetype"/>'s for this query. 
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeIterator GetArchetypeIterator() {
        return new QueryArchetypeIterator( this, Archetypes.Span);
    }
    
    /// <summary>
    /// Returns a <see cref="QueryChunkIterator"/> which can be used to iterate over all fitting <see cref="Chunk"/>'s for this query. 
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator GetChunkIterator() {
        return new QueryChunkIterator(this, Archetypes.Span);
    }

    public bool Equals(Query other) {
        return Equals(Any, other.Any) && Equals(All, other.All) && Equals(None, other.None) && QueryDescription.Equals(other.QueryDescription);
    }

    public override bool Equals(object obj) {
        return obj is Query other && Equals(other);
    }

    public override int GetHashCode() {
        unchecked {
            var hashCode = (Any != null ? Any.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (All != null ? All.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (None != null ? None.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ QueryDescription.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(Query left, Query right) { return left.Equals(right); }
    public static bool operator !=(Query left, Query right) { return !left.Equals(right); }
}