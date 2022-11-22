using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class ComparerTest
{

    [Test]
    public void Test()
    {

        var array1 = new Type[] { typeof(int), typeof(byte) };
        var array2 = new Type[] { typeof(int), typeof(byte) };
        var array3 = new Type[] { typeof(byte), typeof(int) };
        
        Assert.AreEqual(ComponentMeta.GetHashCode(array1), ComponentMeta.GetHashCode(array2));
        Assert.AreEqual(ComponentMeta.GetHashCode(array1), ComponentMeta.GetHashCode(array3));
    }
}