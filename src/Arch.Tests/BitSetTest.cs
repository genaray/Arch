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
    ///     Checks if hash values are the same for <see cref="BitSet"/>s and <see cref="ComponentType"/> arrays.
    /// </summary>
    [Test]
    public void HashSimilarity()
    {
        var array = new ComponentType[]
        {
            new (1, null, 0, false),
            new (36, null, 0, false),
            new (65, null, 0, false),
            new (5, null, 0, false),
        };

        var bitSet = new BitSet();
        bitSet.SetBit(1);
        bitSet.SetBit(36);
        bitSet.SetBit(5);
        bitSet.SetBit(65);

        var hashCode = Component.GetHashCode(array);
        var otherHashCode = bitSet.GetHashCode();

        That(hashCode, Is.EqualTo(otherHashCode));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> all.
    /// </summary>
    [Test]
    public void BitsetAll()
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(5);
        bitSet1.SetBit(6);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(5);
        bitSet2.SetBit(6);

        // ALl fit
        var allResult = bitSet2.All(bitSet1);
        That(allResult, Is.EqualTo(true));

        bitSet2.SetBit(7);
        allResult = bitSet2.All(bitSet1);
        That(allResult, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> any.
    /// </summary>
    [Test]
    public void BitsetAny()
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(5);
        bitSet1.SetBit(6);
        bitSet1.SetBit(35);
        bitSet1.SetBit(36);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(5);
        bitSet2.SetBit(6);

        // Any fit
        var allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(true));

        bitSet2.ClearBit(5);
        allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(true));

        // No fit, since there no unions
        bitSet2.ClearAll();
        bitSet2.SetBit(37);
        allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> none.
    /// </summary>
    [Test]
    public void BitsetNone()
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(5);
        bitSet1.SetBit(6);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(25);
        bitSet2.SetBit(38);

        // None of bitset2 is in bitset1
        var allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(true));

        // One of bitset2 is in bitset1
        bitSet2.SetBit(5);
        allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(false));

        // Bitset2 and 1 are the same, so all off bitset2 are in 1 and therefore its invalid.
        bitSet1.ClearAll();
        bitSet2.ClearAll();

        bitSet1.SetBit(5);
        bitSet1.SetBit(4);
        bitSet2.SetBit(5);
        bitSet2.SetBit(4);

        allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(false));
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

        var allResult = bitSet2.All(bitSet1);
        var anyResult = bitSet2.Any(bitSet1);
        var noneResult = bitSet2.None(bitSet1);
        var exclusive = bitSet2.Exclusive(bitSet1);

        That(allResult, Is.EqualTo(false));
        That(anyResult, Is.EqualTo(false));
        That(noneResult, Is.EqualTo(true));
        That(exclusive, Is.EqualTo(false));
    }
}
