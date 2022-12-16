using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class ComparerTest
{

    [Test]
    public void Test()
    {

        ComponentType[] array1 = { typeof(int), typeof(byte) };
        ComponentType[] array2 = { typeof(int), typeof(byte) };
        ComponentType[] array3 = { typeof(byte), typeof(int) };
        
        Assert.AreEqual(Component.GetHashCode(array1), Component.GetHashCode(array2));
        Assert.AreEqual(Component.GetHashCode(array1), Component.GetHashCode(array3));
    }
}