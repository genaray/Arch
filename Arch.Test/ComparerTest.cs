using Arch.Core.Utils;

namespace Arch.Test;

[TestFixture]
public class ComparerTest
{
    [OneTimeSetUp]
    public void Setup()
    {
        _comparer = new TypeArraySequenceEqualityComparer();
    }

    private TypeArraySequenceEqualityComparer _comparer;

    [Test]
    public void Equals()
    {
        var typeArr1 = new[] { typeof(Transform), typeof(Rotation) };
        var typeArr2 = new[] { typeof(Transform), typeof(Rotation) };

        Assert.That(_comparer.Equals(typeArr1, typeArr2), Is.True);

        var typeArr3 = new[] { typeof(Transform), typeof(Rotation), typeof(Ai) };

        Assert.That(_comparer.Equals(typeArr1, typeArr3), Is.False);
    }

    [Test]
    public void GetHashCode()
    {
        var typeArr1 = new[] { typeof(Transform), typeof(Rotation) };
        var typeArr2 = new[] { typeof(Transform), typeof(Rotation) };

        var hashCode1 = _comparer.GetHashCode(typeArr1);
        var hashCode2 = _comparer.GetHashCode(typeArr2);

        Assert.That(hashCode1, Is.EqualTo(hashCode2));

        var typeArr3 = new[] { typeof(Transform), typeof(Rotation), typeof(Ai) };
        var hashCode3 = _comparer.GetHashCode(typeArr3);

        Assert.That(hashCode1, Is.Not.EqualTo(hashCode3));
    }
}