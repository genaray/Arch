using Arch.Core.Extensions;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core;

// TODO: Documentation.
/// <summary>
///     The <see cref="QueryDescription"/> struct
///     ...
/// </summary>
public partial struct QueryDescription : IEquatable<QueryDescription>
{
    // TODO: Documentation.
    public ComponentType[] All = Array.Empty<ComponentType>();
    public ComponentType[] Any = Array.Empty<ComponentType>();
    public ComponentType[] None = Array.Empty<ComponentType>();
    public ComponentType[] Exclusive = Array.Empty<ComponentType>();

    /// <summary>
    ///     Initializes a new instance of the <see cref="QueryDescription"/> struct.
    /// </summary>
    public QueryDescription() { }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [UnscopedRef]
    public ref QueryDescription WithAll<T>()
    {
        All = Group<T>.Types;
        return ref this;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [UnscopedRef]
    public ref QueryDescription WithAny<T>()
    {
        Any = Group<T>.Types;
        return ref this;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [UnscopedRef]
    public ref QueryDescription WithNone<T>()
    {
        None = Group<T>.Types;
        return ref this;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [UnscopedRef]
    public ref QueryDescription WithExclusive<T>()
    {
        Exclusive = Group<T>.Types;
        return ref this;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(QueryDescription other)
    {
        var allHash = Component.GetHashCode(All);
        var anyHash = Component.GetHashCode(Any);
        var noneHash = Component.GetHashCode(None);
        return allHash == Component.GetHashCode(other.All) && anyHash == Component.GetHashCode(other.Any) && noneHash == Component.GetHashCode(other.None);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return obj is QueryDescription other && Equals(other);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap{
            var hash = 17;
            hash = (hash * 23) + All.GetHashCode();
            hash = (hash * 23) + Any.GetHashCode();
            hash = (hash * 23) + None.GetHashCode();
            return hash;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(QueryDescription left, QueryDescription right)
    {
        return left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(QueryDescription left, QueryDescription right)
    {
        return !left.Equals(right);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="Query"/> struct
///     ...
/// </summary>
public readonly struct Query : IEquatable<Query>
{
    private readonly PooledList<Archetype> _archetypes;

    private readonly BitSet _any;
    private readonly BitSet _all;
    private readonly BitSet _none;
    private readonly BitSet _exclusive;

    private readonly bool _isExclusive;

    private readonly QueryDescription _queryDescription;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Query"/> struct
    ///     ...
    /// </summary>
    /// <param name="archetypes"></param>
    /// <param name="description"></param>
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

        // Convert to `BitSet`s.
        _all = description.All.ToBitSet();
        _any = description.Any.ToBitSet();
        _none = description.None.ToBitSet();
        _exclusive = description.Exclusive.ToBitSet();

        // Handle exclusive.
        if (description.Exclusive.Length != 0)
        {
            _isExclusive = true;
        }

        // Otherwise an Any value of 0 always returns false somehow.
        if (description.Any.Length == 0)
        {
            _any.SetAll();
        }

        _queryDescription = description;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bitset"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Valid(BitSet bitset)
    {
        return _isExclusive ? _exclusive.Exclusive(bitset) : _all.All(bitset) && _any.Any(bitset) && _none.None(bitset);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryArchetypeIterator GetArchetypeIterator()
    {
        return new QueryArchetypeIterator(this, _archetypes.Span);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public QueryChunkIterator GetChunkIterator()
    {
        return new QueryChunkIterator(this, _archetypes.Span);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Query other)
    {
        return Equals(_any, other._any) && Equals(_all, other._all) && Equals(_none, other._none) && Equals(_exclusive, other._exclusive) && _queryDescription.Equals(other._queryDescription);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return obj is Query other && Equals(other);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = _any is not null ? _any.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (_all is not null ? _all.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (_none is not null ? _none.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (_exclusive?.GetHashCode() ?? 0);
            hashCode = (hashCode * 397) ^ _queryDescription.GetHashCode();

            return hashCode;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(Query left, Query right)
    {
        return left.Equals(right);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(Query left, Query right)
    {
        return !left.Equals(right);
    }
}
