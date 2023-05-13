using Arch.Core.Utils;

namespace Arch.Core.Extensions.Internal;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `BitSet` type directly?
/// <summary>
///     The <see cref="BitSetExtensions"/> class
///     adds several extension methods to the <see cref="BitSet"/> class.
/// </summary>
internal static class BitSetExtensions
{
    // NOTE: Should this be in `TypeExtensions`?
    /// <summary>
    ///     Converts an array of <see cref="ComponentType"/>'s to its <see cref="BitSet"/>.
    /// </summary>
    /// <param name="types">The array of <see cref="ComponentType"/>'s.</param>
    /// <returns>Their newly created <see cref="BitSet"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static BitSet ToBitSet(this ComponentType[] types)
    {
        if (types.Length == 0)
        {
            return new BitSet();
        }

        var bitSet = new BitSet();
        bitSet.SetBits(types);

        return bitSet;
    }

    /// <summary>
    ///     Sets bits in a <see cref="BitSet"/> from the <see cref="ComponentType"/> ids.
    /// </summary>
    /// <param name="bitSet">The <see cref="BitSet"/>.</param>
    /// <param name="types">The <see cref="ComponentType"/>'s array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void SetBits(this BitSet bitSet, ComponentType[] types)
    {
        foreach (var type in types)
        {
            var id = type.Id;
            bitSet.SetBit(id);
        }
    }
}
