using Arch.Core.Utils;

namespace Arch.Core.Extensions.Internal;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `BitSet` type directly?
/// <summary>
///     The <see cref="BitSetExtensions"/> class
///     adds several extension methods to the <see cref="BitSet"/> class.
/// </summary>
internal static class BitSetExtensions
{

    /// <summary>
    ///     Sets bits in a <see cref="BitSet"/> from the <see cref="ComponentType"/> ids.
    /// </summary>
    /// <param name="bitSet">The <see cref="BitSet"/>.</param>
    /// <param name="types">The <see cref="ComponentType"/>'s array.</param>

    internal static void SetBits(this BitSet bitSet, Span<ComponentType> types)
    {
        foreach (var type in types)
        {
            var id = type.Id;
            bitSet.SetBit(id);
        }
    }
}
