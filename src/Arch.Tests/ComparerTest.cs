using Arch.Core.Utils;
using static NUnit.Framework.Assert;

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

        That(Component.GetHashCode(array2), Is.EqualTo(Component.GetHashCode(array1)));
        That(Component.GetHashCode(array3), Is.EqualTo(Component.GetHashCode(array1)));
    }
}
