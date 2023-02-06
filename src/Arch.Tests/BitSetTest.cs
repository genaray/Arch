using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

/// <summary>
///     Checks <see cref="BitSet"/> and HashCode related methods.
/// </summary>
[TestFixture]
public class BitSetTest
{

    /// <summary>
    ///     Checks if <see cref="ComponentType"/>-Arrays with same elements but in different order result in the same hash.
    /// </summary>
    [Test]
    public void ComponentHashOrder()
    {

        ComponentType[] array1 = { typeof(int), typeof(byte) };
        ComponentType[] array2 = { typeof(int), typeof(byte) };
        ComponentType[] array3 = { typeof(byte), typeof(int) };

        That(Component.GetHashCode(array2), Is.EqualTo(Component.GetHashCode(array1)));
        That(Component.GetHashCode(array3), Is.EqualTo(Component.GetHashCode(array1)));
    }

    /// <summary>
    ///     Checks whether different sized <see cref="BitSet"/>'s work correctly.
    /// </summary>
    [Test]
    public void AllWithDifferentLengthBitSet()
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(5);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(33);

        bool result = bitSet2.All(bitSet1);

        That(result, Is.EqualTo(false));
    }
}
