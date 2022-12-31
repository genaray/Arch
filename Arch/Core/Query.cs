using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;
using CommunityToolkit.HighPerformance.Buffers;

namespace Arch.Core;

/// <summary>
///     A query describtion of which entities we wanna query for.
/// </summary>
public partial struct QueryDescription : IEquatable<QueryDescription>
{
    public ComponentType[] All = Array.Empty<ComponentType>();
    public ComponentType[] Any = Array.Empty<ComponentType>();
    public ComponentType[] None = Array.Empty<ComponentType>();
    public ComponentType[] Exclusive = Array.Empty<ComponentType>();

    /// <summary>
    /// Creates a new instance. 
    /// </summary>
    public QueryDescription()
    {
    }
    
    /// <summary>
    /// Includes the passed generic into the description.
    /// Replaces previous set ones. 
    /// </summary>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>Reference to this instance for chaining calls.</returns>
    [UnscopedRef]
    public ref QueryDescription WithAll<T>()
    {
        All = Group<T>.Types;
        return ref this;
    }

    /// <summary>
    /// Includes the passed generic into the description.
    /// Replaces previous set ones. 
    /// </summary>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>Reference to this instance for chaining calls.</returns>
    [UnscopedRef]
    public ref QueryDescription WithAny<T>()
    {
        Any = Group<T>.Types;
        return ref this;
    }
    
    /// <summary>
    /// Includes the passed generic into the description.
    /// Replaces previous set ones. 
    /// </summary>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>Reference to this instance for chaining calls.</returns>
    [UnscopedRef]
    public ref QueryDescription WithNone<T>()
    {
        None = Group<T>.Types;
        return ref this;
    }
    
    /// <summary>
    /// Includes the passed generic into the description.
    /// Replaces previous set ones. 
    /// </summary>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>Reference to this instance for chaining calls.</returns>
    [UnscopedRef]
    public ref QueryDescription WithExclusive<T>()
    {
        Exclusive = Group<T>.Types;
        return ref this;
    }

    public bool Equals(QueryDescription other)
    {
        var allHash = Component.GetHashCode(All);
        var anyHash = Component.GetHashCode(Any);
        var noneHash = Component.GetHashCode(None);
        return allHash == Component.GetHashCode(other.All) && anyHash == Component.GetHashCode(other.Any) && noneHash == Component.GetHashCode(other.None);
    }

    public override bool Equals(object obj)
    {
        return obj is QueryDescription other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap{
            var hash = 17;
            hash = hash * 23 + All.GetHashCode();
            hash = hash * 23 + Any.GetHashCode();
            hash = hash * 23 + None.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(QueryDescription left, QueryDescription right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(QueryDescription left, QueryDescription right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
///     A constructed query used for translating the <see cref="QueryDescription" /> and validating <see cref="BitSet" />'s to find the right chunks.
/// </summary>
public readonly struct Query : IEquatable<Query>
{
    private readonly PooledList<Archetype> _archetypes;

    private readonly BitSet _any;
    private readonly BitSet _all;
    private readonly BitSet _none;
    private readonly BitSet _exclusive;

    private readonly bool _isExclusive;

    private QueryDescription QueryDescription { get; }

    internal Query(PooledList<Archetype> archetypes, QueryDescription description) : this()
    {
        _archetypes = archetypes;

        Debug.Assert(
            !((description.Any.Length != 0 ||
            description.All.Length != 0 ||
            description.None.Length != 0) &&
            description.Exclusive.Length != 0),
            "If Any, All or None have items then Exclusive may not have any items"
        );

        // Conver to bitsets
        _all = description.All.ToBitSet();
        _any = description.Any.ToBitSet();
        _none = description.None.ToBitSet();
        _exclusive = description.Exclusive.ToBitSet();

        // Handle exclusive
        if (description.Exclusive.Length != 0)
            _isExclusive = true;

        // Otherwhise a any value of 0 always returns false somehow
        if (description.Any.Length == 0) _any.SetAll();
        QueryDescription = description;
    }

    /// <summary>
    ///     Checks if this query is in a <see cref="BitSet" />
    /// </summary>
    /// <param name="bitset"></param>
    /// <returns>True if the passed bitset is valid for this query</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Valid(BitSet bitset)
    {
        return _isExclusive ? _exclusive.Exclusive(bitset) : _all.All(bitset) && _any.Any(bitset) && _none.None(bitset);
    }

    /// <summary>
    ///     Returns a <see cref="QueryArchetypeIterator" /> which can be used to iterate over all fitting <see cref="Archetype" />'s for this query.
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeIterator GetArchetypeIterator()
    {
        return new QueryArchetypeIterator(this, _archetypes.Span);
    }

    /// <summary>
    ///     Returns a <see cref="QueryChunkIterator" /> which can be used to iterate over all fitting <see cref="Chunk" />'s for this query.
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator GetChunkIterator()
    {
        return new QueryChunkIterator(this, _archetypes.Span);
    }
    
    /// <summary>
    ///     Returns a <see cref="QueryChunkIterator" /> which can be used to iterate over all fitting <see cref="Chunk" />'s for this query.
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryEntityIterator GetEntityIterator()
    {
        return new QueryEntityIterator(this, _archetypes.Span);
    }

    public bool Equals(Query other)
    {
        return Equals(_any, other._any) && Equals(_all, other._all) && Equals(_none, other._none) && Equals(_exclusive, other._exclusive) && QueryDescription.Equals(other.QueryDescription);
    }

    public override bool Equals(object obj)
    {
        return obj is Query other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = _any != null ? _any.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (_all != null ? _all.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (_none != null ? _none.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (_exclusive?.GetHashCode() ?? 0);
            hashCode = (hashCode * 397) ^ QueryDescription.GetHashCode();
            return hashCode;
        }
    }

    public static bool operator ==(Query left, Query right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Query left, Query right)
    {
        return !left.Equals(right);
    }
}