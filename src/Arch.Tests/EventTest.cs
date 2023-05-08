using Arch.Core;

namespace Arch.Tests;

[TestFixture]
public class EventTest
{
    private World _world = default!;

    [SetUp]
    public void SetUp()
    {
        _world = World.Create();
    }

    [Test]
    public void EntityEventsTest()
    {
#if !EVENTS
        Assert.Ignore("Events are not enabled");
        return;
#else
        var asserter = new EventAsserter();

        _world.SubscribeEntityCreated((in Entity entity) => asserter.Created.Add(entity));
        _world.SubscribeEntityDestroyed((in Entity entity) => asserter.Destroyed.Add(entity));
        _world.SubscribeComponentAdded<EventTestComponentOne>((in Entity entity) => asserter.CompOneAdded.Add(entity));
        _world.SubscribeComponentAdded<EventTestComponentTwo>((in Entity entity) => asserter.CompTwoAdded.Add(entity));
        _world.SubscribeComponentRemoved<EventTestComponentOne>((in Entity entity) => asserter.CompOneRemoved.Add(entity));
        _world.SubscribeComponentRemoved<EventTestComponentTwo>((in Entity entity) => asserter.CompTwoRemoved.Add(entity));
        _world.SubscribeComponentSet((in Entity entity, in EventTestComponentOne comp) => asserter.CompOneSet.Add((entity, comp)));
        _world.SubscribeComponentSet((in Entity entity, in EventTestComponentTwo comp) => asserter.CompTwoSet.Add((entity, comp)));

        // No events yet
        asserter.AssertEvents();

        var entity = _world.Create();

        asserter.AssertEvents(created: 1);
        Assert.That(asserter.Created, Does.Contain(entity));
        asserter.Clear();

        _world.Destroy(entity);

        asserter.AssertEvents(destroyed: 1);
        Assert.That(asserter.Destroyed, Does.Contain(entity));
        asserter.Clear();

        entity = _world.Create<EventTestComponentOne>();
        var comp = _world.Get<EventTestComponentOne>(entity);

        // World.Create<T> doesn't check if the component is default before raising a set event
        asserter.AssertEvents(created: 1, compOneAdded: 1, compOneSet: 1);
        Assert.That(asserter.Created, Does.Contain(entity));
        Assert.That(asserter.CompOneSet, Does.Contain((entity, comp)));
        asserter.Clear();

        _world.Add<EventTestComponentTwo>(entity);

        asserter.AssertEvents(compTwoAdded: 1);
        Assert.That(asserter.CompTwoAdded, Does.Contain(entity));
        asserter.Clear();

        _world.Remove<EventTestComponentOne, EventTestComponentTwo>(entity);

        asserter.AssertEvents(compOneRemoved: 1, compTwoRemoved: 1);
        Assert.That(asserter.CompOneRemoved, Does.Contain(entity));
        Assert.That(asserter.CompTwoRemoved, Does.Contain(entity));
        asserter.Clear();

        _world.Add<EventTestComponentOne, EventTestComponentTwo>(entity);
        asserter.AssertEvents(compOneAdded: 1, compTwoAdded: 1, compOneSet: 1, compTwoSet: 1);
        asserter.Clear();

        var query = new QueryDescription().WithAll<EventTestComponentOne, EventTestComponentTwo>();
        _world.Remove<EventTestComponentOne, EventTestComponentTwo>(query);
        asserter.AssertEvents(compOneRemoved: 1, compTwoRemoved: 1);
        asserter.Clear();

        _world.Add<EventTestComponentOne>(entity);
        asserter.AssertEvents(compOneAdded: 1);
        asserter.Clear();

        _world.Set(entity, new EventTestComponentOne());
        asserter.AssertEvents(compOneSet: 1);
        asserter.Clear();

        _world.Set(entity, (object) new EventTestComponentOne());
        asserter.AssertEvents(compOneSet: 1);
        asserter.Clear();

        _world.Add<EventTestComponentTwo>(entity);
        asserter.AssertEvents(compTwoAdded: 1);
        asserter.Clear();

        _world.Set(entity, new EventTestComponentOne(), new EventTestComponentTwo());
        asserter.AssertEvents(compOneSet: 1, compTwoSet: 1);
        asserter.Clear();

        _world.SetRange(entity, new object[] { new EventTestComponentOne(), new EventTestComponentTwo() });
        asserter.AssertEvents(compOneSet: 1, compTwoSet: 1);
        asserter.Clear();
#endif
    }

    private class EventAsserter
    {
        public readonly List<Entity> Created = new();
        public readonly List<Entity> Destroyed = new();
        public readonly List<Entity> CompOneAdded = new();
        public readonly List<Entity> CompTwoAdded = new();
        public readonly List<(Entity Entity, EventTestComponentOne Comp)> CompOneSet = new();
        public readonly List<(Entity Entity, EventTestComponentTwo Comp)> CompTwoSet = new();
        public readonly List<Entity> CompOneRemoved = new();
        public readonly List<Entity> CompTwoRemoved = new();

        public void AssertEvents(int created = 0,
            int destroyed = 0,
            int compOneAdded = 0,
            int compTwoAdded = 0,
            int compOneSet = 0,
            int compTwoSet = 0,
            int compOneRemoved = 0,
            int compTwoRemoved = 0)
        {
            Assert.That(Created, Has.Count.EqualTo(created));
            Assert.That(Destroyed, Has.Count.EqualTo(destroyed));
            Assert.That(CompOneAdded, Has.Count.EqualTo(compOneAdded));
            Assert.That(CompTwoAdded, Has.Count.EqualTo(compTwoAdded));
            Assert.That(CompOneRemoved, Has.Count.EqualTo(compOneRemoved));
            Assert.That(CompTwoRemoved, Has.Count.EqualTo(compTwoRemoved));
            Assert.That(CompOneSet, Has.Count.EqualTo(compOneSet));
            Assert.That(CompTwoSet, Has.Count.EqualTo(compTwoSet));
        }

        public void Clear()
        {
            Created.Clear();
            Destroyed.Clear();
            CompOneAdded.Clear();
            CompTwoAdded.Clear();
            CompOneRemoved.Clear();
            CompTwoRemoved.Clear();
            CompOneSet.Clear();
            CompTwoSet.Clear();
        }
    }

    private struct EventTestComponentOne
    {
    }

    private struct EventTestComponentTwo
    {
    }
}
