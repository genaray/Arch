using Arch.Core;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

[TestFixture]
public sealed class ComponentRegistryConcurrencyTest
{
    private struct ConcurrentRegistrationComponent { }

    [Test]
    public void ConcurrentAddOfSameTypeReturnsOneRegistration()
    {
        const int workerCount = 32;
        using var start = new ManualResetEventSlim();
        using var ready = new CountdownEvent(workerCount);
        var registrations = new ComponentType[workerCount];
        var errors = new Exception?[workerCount];
        var threads = new Thread[workerCount];

        for (var i = 0; i < workerCount; i++)
        {
            var index = i;
            threads[index] = new Thread(() =>
            {
                ready.Signal();
                start.Wait();
                try
                {
                    registrations[index] = Component.GetComponentType(typeof(ConcurrentRegistrationComponent));
                }
                catch (Exception exception)
                {
                    errors[index] = exception;
                }
            });
            threads[index].Start();
        }

        ready.Wait();
        start.Set();
        foreach (var thread in threads)
        {
            thread.Join();
        }

        That(errors.Count(exception => exception is not null), Is.Zero);
        That(registrations.Select(component => component.Id).Distinct().Count(), Is.EqualTo(1));
        That(ComponentRegistry.TypeToComponentType[typeof(ConcurrentRegistrationComponent)], Is.EqualTo(registrations[0]));
    }
}
