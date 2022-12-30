using Arch.Core.Utils;

namespace Arch.Tests;

[TestFixture]
public class ComparerTest
{

    [Test]
    public void Test()
    {

        ComponentType[] array1 = { typeof(int), typeof(byte) };
        ComponentType[] array2 = { typeof(int), typeof(byte) };
        ComponentType[] array3 = { typeof(byte), typeof(int) };

        Assert.That(Component.GetHashCode(array2), Is.EqualTo(Component.GetHashCode(array1)));
        Assert.That(Component.GetHashCode(array3), Is.EqualTo(Component.GetHashCode(array1)));
    }
}
