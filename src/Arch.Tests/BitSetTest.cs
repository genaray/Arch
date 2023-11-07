using System.Numerics;
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
        var array = new ComponentType[] { new(1, 0), new(36, 0), new(65, 0), new(5, 0), };

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
    ///     Checks <see cref="BitSet"/> set all.
    /// </summary>
    [Test]
    public void BitsetSetAll()
    {
        var bitSet = new BitSet();
        bitSet.SetAll();

        var count = 0;
        foreach (var byt in bitSet)
        {
            count += 32; // 32 Bits in each uint
        }

        count--; // Minus one because we start at 0

        // ALl fit
        That(bitSet.HighestBit, Is.EqualTo(count));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> all.
    /// <param name="values">The values being set.</param>
    /// <param name="multiplier">The multiplier for the passed values. Mainly for vectorization-testing to increase the set bits.</param>
    /// </summary>
    [Test]
    [TestCase(new[] { 5, 6, 7 }, 1)] // Sets bit 5,6,7
    [TestCase(new[] { 5, 6, 7 }, 100)] // Sets bit 500,600,700
    public void BitsetAll(int[] values, int multiplier)
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(values[0] * multiplier);
        bitSet1.SetBit(values[1] * multiplier);

        var bitSet2 = new BitSet();
        bitSet2.SetBit(values[0] * multiplier);
        bitSet2.SetBit(values[1] * multiplier);

        // ALl fit
        var allResult = bitSet2.All(bitSet1);
        That(allResult, Is.EqualTo(true));

        bitSet2.SetBit(values[2] * multiplier);
        allResult = bitSet2.All(bitSet1);
        That(allResult, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> any.
    /// <param name="values">The values being set or cleared.</param>
    /// <param name="multiplier">The multiplier for the passed values. Mainly for vectorization-testing to increase the set bits.</param>
    /// </summary>
    [Test]
    [TestCase(new[] { 5, 6, 35, 36, 37 }, 1)]
    [TestCase(new[] { 5, 6, 35, 36, 37 }, 100)]
    public void BitsetAny(int[] values, int multiplier)
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(values[0] * multiplier);
        bitSet1.SetBit(values[1] * multiplier);
        bitSet1.SetBit(values[2] * multiplier);
        bitSet1.SetBit(values[3] * multiplier);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(values[0] * multiplier);
        bitSet2.SetBit(values[1] * multiplier);

        // Any fit
        var allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(true));

        bitSet2.ClearBit(values[0] * multiplier);
        allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(true));

        // No fit, since there no unions
        bitSet2.ClearAll();
        bitSet2.SetBit(values[4] * multiplier);
        allResult = bitSet2.Any(bitSet1);
        That(allResult, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> none.
    /// <param name="values">The values being set or cleared.</param>
    /// <param name="multiplier">The multiplier for the passed values. Mainly for vectorization-testing to increase the set bits.</param>
    /// </summary>
    [Test]
    [TestCase(new[] { 5, 6, 25, 38, 4 }, 1)]
    [TestCase(new[] { 5, 6, 25, 38, 4 }, 100)]
    public void BitsetNone(int[] values, int multiplier)
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(values[0] * multiplier);
        bitSet1.SetBit(values[1] * multiplier);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(values[2] * multiplier);
        bitSet2.SetBit(values[3] * multiplier);

        // None of bitset2 is in bitset1
        var allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(true));

        // One of bitset2 is in bitset1
        bitSet2.SetBit(values[0] * multiplier);
        allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(false));

        // Bitset2 and 1 are the same, so all off bitset2 are in 1 and therefore its invalid.
        bitSet1.ClearAll();
        bitSet2.ClearAll();

        bitSet1.SetBit(values[0] * multiplier);
        bitSet1.SetBit(values[4] * multiplier);
        bitSet2.SetBit(values[0] * multiplier);
        bitSet2.SetBit(values[4] * multiplier);

        allResult = bitSet2.None(bitSet1);
        That(allResult, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks <see cref="BitSet"/> exclusive.
    /// <param name="values">The values being set or cleared.</param>
    /// <param name="multiplier">The multiplier for the passed values. Mainly for vectorization-testing to increase the set bits.</param>
    /// </summary>
    [Test]
    [TestCase(new[] { 5, 6, 25 }, 1)]
    [TestCase(new[] { 5, 6, 25 }, 100)]
    public void BitsetExclusive(int[] values, int multiplier)
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(values[0] * multiplier);
        bitSet1.SetBit(values[1] * multiplier);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(values[0] * multiplier);
        bitSet2.SetBit(values[1] * multiplier);

        // Both are totally equal
        var exclusive = bitSet2.Exclusive(bitSet1);
        That(exclusive, Is.EqualTo(true));

        // Bitset2 is not equal anymore
        bitSet2.SetBit(values[2] * multiplier);
        exclusive = bitSet2.Exclusive(bitSet1);
        That(exclusive, Is.EqualTo(false));

        // Bitset2 is back to default, but bitset1 is now different -> both differ, should be false
        bitSet2.ClearBit(values[2] * multiplier);
        bitSet1.SetBit(values[2] * multiplier);
        exclusive = bitSet2.Exclusive(bitSet1);
        That(exclusive, Is.EqualTo(false));

        // Bitset2 is now the same as bitset 1 -> should be true
        bitSet2.SetBit(values[2] * multiplier);
        exclusive = bitSet2.Exclusive(bitSet1);
        That(exclusive, Is.EqualTo(true));
    }

    /// <summary>
    ///     Checks whether different sized <see cref="BitSet"/>'s work correctly.
    /// <param name="values">The values being set or cleared.</param>
    /// <param name="multiplier">The multiplier for the passed values. Mainly for vectorization-testing to increase the set bits.</param>
    /// </summary>
    [Test]
    [TestCase(new[] { 5, 33 }, 1)]
    [TestCase(new[] { 5, 33 }, 100)]
    public void AllWithDifferentLengthBitSet(int[] values, int multiplier)
    {
        var bitSet1 = new BitSet();
        bitSet1.SetBit(values[0] * multiplier);
        var bitSet2 = new BitSet();
        bitSet2.SetBit(values[1] * multiplier);

        var allResult = bitSet2.All(bitSet1);
        var anyResult = bitSet2.Any(bitSet1);
        var noneResult = bitSet2.None(bitSet1);
        var exclusive = bitSet2.Exclusive(bitSet1);

        That(allResult, Is.EqualTo(false));
        That(anyResult, Is.EqualTo(false));
        That(noneResult, Is.EqualTo(true));
        That(exclusive, Is.EqualTo(false));
    }

    /// <summary>
    ///     Checks if <see cref="ComponentType"/> Id borders a bitset size correctly.
    /// <param name="borderComponentId">The component Id bordering a bitset size</param>
    /// </summary>
    [Theory]
    [TestCase(32)]
    [TestCase(64)]
    [TestCase(96)]
    [TestCase(128)]
    public void Component32Hash(int borderComponentId)
    {
        var componentTypeNotOnBorder = new ComponentType(borderComponentId - 10, 0);
        var componentTypeOnBorder = new ComponentType(borderComponentId, 0);

        ComponentType[] array1 = { componentTypeNotOnBorder, componentTypeOnBorder };
        ComponentType[] array2 = { componentTypeNotOnBorder };

        That(Component.GetHashCode(array1), Is.Not.EqualTo(Component.GetHashCode(array2)));
    }
}
