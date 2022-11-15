using Arch.Core.Utils;
using JobScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch.Test
{
    [TestFixture]
    public class SequenceEuqalsArchetypeComparerTest
    {
        private SequenceEqualsArchetypeComparer _comparer;

        [OneTimeSetUp]
        public void Setup()
        {
            _comparer = new SequenceEqualsArchetypeComparer();
        }

        [Test]
        public void Equals()
        {
            var typeArr1 = new Type[] { typeof(Transform), typeof(Rotation) };
            var typeArr2 = new Type[] { typeof(Transform), typeof(Rotation) };

            Assert.That(_comparer.Equals(typeArr1, typeArr2), Is.True);

            var typeArr3 = new Type[] { typeof(Transform), typeof(Rotation), typeof(AI) };

            Assert.That(_comparer.Equals(typeArr1, typeArr3), Is.False);
        }

        [Test]
        public void GetHashCode()
        {
            var typeArr1 = new Type[] { typeof(Transform), typeof(Rotation) };
            var typeArr2 = new Type[] { typeof(Transform), typeof(Rotation) };

            var hashCode1 = _comparer.GetHashCode(typeArr1);
            var hashCode2 = _comparer.GetHashCode(typeArr2);

            Assert.That(hashCode1, Is.EqualTo(hashCode2));

            var typeArr3 = new Type[] { typeof(Transform), typeof(Rotation), typeof(AI) };
            var hashCode3 = _comparer.GetHashCode(typeArr3);

            Assert.That(hashCode1, Is.Not.EqualTo(hashCode3));
        }
    }
}
