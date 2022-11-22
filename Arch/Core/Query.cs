using System;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core;

/// <summary>
///     A query describtion of which entities we wanna query for.
/// </summary>
public struct QueryDescription : IEquatable<QueryDescription>
{
    public Type[] All = Array.Empty<Type>();
    public Type[] Any = Array.Empty<Type>();
    public Type[] None = Array.Empty<Type>();

    public QueryDescription()
    {
    }

    public bool Equals(QueryDescription other)
    {
        var allHash = ComponentMeta.GetHashCode(All);
        var anyHash = ComponentMeta.GetHashCode(Any);
        var noneHash = ComponentMeta.GetHashCode(None);
        return allHash == ComponentMeta.GetHashCode(other.All) && anyHash == ComponentMeta.GetHashCode(other.Any) && noneHash == ComponentMeta.GetHashCode(other.None);
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

    private QueryDescription QueryDescription { get; }

    internal Query(PooledList<Archetype> archetypes, QueryDescription description) : this()
    {
        _archetypes = archetypes;

        _all = description.All.ToBitSet();
        _any = description.Any.ToBitSet();
        _none = description.None.ToBitSet();
        QueryDescription = description;

        // Otherwhise a any value of 0 always returns false somehow
        if (description.Any.Length == 0) _any.SetAll();
    }

    /// <summary>
    ///     Checks if this query is in a <see cref="BitSet" />
    /// </summary>
    /// <param name="bitset"></param>
    /// <returns>True if the passed bitset is valid for this query</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Valid(BitSet bitset)
    {
        return _all.All(bitset) && _any.Any(bitset) && _none.None(bitset);
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

    public bool Equals(Query other)
    {
        return Equals(_any, other._any) && Equals(_all, other._all) && Equals(_none, other._none) && QueryDescription.Equals(other.QueryDescription);
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