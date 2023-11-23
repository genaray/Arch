using System.Threading;
using Arch.Core.Utils;

namespace Arch.Core;

/// <inheritdoc cref="Group"/>
[Variadic(nameof(T0), 24)]
public static class Group<T0>
{
    internal static readonly int Id;
    /// <summary>
    ///     The global array of <see cref="ComponentType"/> for this given type group. Must not be modified in any way.
    /// </summary>
    public static readonly ComponentType[] Types;
    /// <summary>
    ///     The hash code for this given type group.
    /// </summary>
    public static readonly int Hash;

    static Group()
    {
        Id = Interlocked.Increment(ref Id);
        Types = new ComponentType[]
        {
            // [Variadic: CopyLines]
            Component<T0>.ComponentType,
        };
        Hash = Component.GetHashCode(Types);
    }
}
