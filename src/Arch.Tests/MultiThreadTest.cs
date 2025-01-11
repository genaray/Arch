using Arch.Core;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public class MultiThreadTest
{
    /// <summary>
    /// Checks if the <see cref="World.WorldSize"/> is correct when creating World multithreaded.
    /// </summary>
    [Test]
    public void MultiThreadedCreateAndDestroy()
    {
        int originalWorldSize = World.WorldSize;
        const int testCount = 10;
        for (int i = 0; i < testCount; ++i)
        {
            var threads = new List<Thread>();
            for (var j = 0; j < Environment.ProcessorCount; j++)
            {
                var thread = new Thread(() =>
                {
                    for (var j = 0; j < 1000; j++)
                    {
                        var world = World.Create();
                        World.Destroy(world);
                    }
                });
                threads.Add(thread);
            }

            threads.ForEach(t => t.Start());

            foreach (var thread in threads)
            {
                thread.Join();
            }

            That(World.WorldSize, Is.EqualTo(originalWorldSize));
        }
    }
}
