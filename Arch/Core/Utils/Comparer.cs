using System;
using System.Collections.Generic;
using System.Linq;

namespace Arch.Core.Utils; 

/// <summary>
/// A equality comparer for a <see cref="Type"/> array. 
/// </summary>
public class TypeEqualityComparer : IEqualityComparer<Type[]> {

    public bool Equals(Type[] x, Type[] y) { return x != null && y != null && x.SequenceEqual(y); }
    public int GetHashCode(Type[] obj) { return obj.GetHashCode(); }

    /// <summary>
    /// Public instance which can be used by all collections requiring such a comparer. 
    /// </summary>
    public static TypeEqualityComparer Instance { get; } = new TypeEqualityComparer();
}