using Arch.Buffer;

namespace Arch.Tests;

[TestFixture]
public sealed partial class StructuralSparseArrayTest
{
    private static void TestEquivalent(StructuralSparseArray test, HashSet<int> control)
    {
        // Brute force test every index
        for (int i = 0; i < 128; i++)
        {
            bool contains = control.Contains(i);
            Assert.That(test.Contains(i), Is.EqualTo(contains));
        }
    }

    [Test]
    public void ClearAndAccessMany()
    {
        var test = new StructuralSparseArray(new(1, 0));
        var control = new HashSet<int>();

        for (int i = 0; i < 10; i++)
        {
            control.Add(52 + i);
            test.Add(52 + i);

            TestEquivalent(test, control);

            control.Add(3 + i);
            test.Add(3 + i);

            TestEquivalent(test, control);

            control.Clear();
            test.Clear();

            TestEquivalent(test, control);

            control.Add(3);
            test.Add(3);

            TestEquivalent(test, control);

            control.Add(3);
            test.Add(3);

            TestEquivalent(test, control);

            control.Clear();
            test.Clear();

            TestEquivalent(test, control);
        }
    }
}
