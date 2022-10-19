using System;
using System.Runtime.CompilerServices;
using Arch.Core.Extensions;
using Arch.Core.Utils;

namespace Arch.Core; 

/// <summary>
/// A query describtion of which entities we wanna query for.
/// </summary>
public struct QueryDescription {

    public Type[] All = Array.Empty<Type>();
    public Type[] Any = Array.Empty<Type>();
    public Type[] None = Array.Empty<Type>();
    
    public QueryDescription() {}
}

/// <summary>
/// A constructed query used for translating the <see cref="QueryDescription"/> and validating <see cref="BitSet"/>'s to find the right chunks. 
/// </summary>
internal readonly struct Query {
    
    private readonly BitSet Any;
    private readonly BitSet All;
    private readonly BitSet None;
    
    private QueryDescription QueryDescription { get; }

    internal Query(QueryDescription description) : this() {
        
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
}