using System;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

public static class BitSetExtensions
{
    /// <summary>
    ///     Creates a bitset/bitmask from a array of components.
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    public static BitSet ToBitSet(this Type[] types)
    {
        if (types.Length == 0)
            return new BitSet();

        var bitSet = new BitSet();
        bitSet.SetBits(types);

        return bitSet;
    }

    /// <summary>
    ///     Sets all bits according to the passed component types.
    /// </summary>
    /// <param name="bitSet"></param>
    /// <param name="types"></param>
    public static void SetBits(this BitSet bitSet, Type[] types)
    {
        foreach (var type in types)
        {
            var id = ComponentMeta.Id(type);
            bitSet.SetBit(id);
        }
    }
}