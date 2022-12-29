using Arch.Core.Utils;

namespace Arch.Core.Extensions;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `BitSet` type directly?
// TODO: Documentation.
/// <summary>
///     The <see cref="BitSetExtensions"/> class
///     ...
/// </summary>
public static class BitSetExtensions
{
    // NOTE: Should this be in `TypeExtensions`?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static BitSet ToBitSet(this ComponentType[] types)
    {
        if (types.Length == 0)
        {
            return new BitSet();
        }

        var bitSet = new BitSet();
        bitSet.SetBits(types);

        return bitSet;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bitSet"></param>
    /// <param name="types"></param>
    public static void SetBits(this BitSet bitSet, ComponentType[] types)
    {
        foreach (var type in types)
        {
            var id = type.Id;
            bitSet.SetBit(id);
        }
    }
}
