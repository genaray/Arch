using Arch.Core.Extensions.Internal;

namespace Arch.Tests.Extensions;

[TestFixture]
public class ArrayExtensionsTests
{
    [Test]
    public void Add_InRange()
    {
        var arr = new[] { 1, 2, 3 };
        arr.Add(0, 7);

        CollectionAssert.AreEqual(new[] { 7, 2, 3 }, arr);
    }

    [Test]
    public void Add_OutOfRange()
    {
        var arr = new[] { 1, 2, 3 };
        arr = arr.Add(3, 7);

        // The array might be any size now.
        // All we need to check if that the first 4 elements are correct.
        CollectionAssert.AreEqual(new[] { 1, 2, 3, 7 }, arr.Take(4));
    }

    [Test]
    public void Add_FarOutOfRange()
    {
        var arr = new[] { 1, 2, 3 };
        arr = arr.Add(30, 7);

        // The array might be any size now.
        // All we need to check if that the first 3 elements are correct and the 30th is 7.
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, arr.Take(3));
        Assert.That(arr[30], Is.EqualTo(7));
    }

    [Test]
    public void Add_VeryFarOutOfRange()
    {
        var arr = new[] { 1, 2, 3 };
        arr = arr.Add(3000, 7);

        // The array might be any size now.
        // All we need to check if that the first 3 elements are correct and the 30th is 7.
        CollectionAssert.AreEqual(new[] { 1, 2, 3 }, arr.Take(3));
        Assert.That(arr[3000], Is.EqualTo(7));
    }

    [Test]
    public void Add_NegativeIndex()
    {
        var arr = new[] { 1, 2, 3 };

        Assert.Throws<ArgumentOutOfRangeException>(() => arr.Add(-1, 7));
    }
}
