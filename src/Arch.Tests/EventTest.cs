using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using static NUnit.Framework.Assert;

namespace Arch.Tests;

#if EVENTS

/// <summary>
///     The <see cref="EventTest"/> class
///     adds several methods for checking if events are fired correctly upon entity modifcation. 
/// </summary>
[TestFixture]
public class EventTest
{
    private EventAsserter _asserter;

    [SetUp]
    public void SetUp()
    {
        _asserter = new EventAsserter();
    }

    [Test]
    public void CreatedAndDestroyed()
    {
        using var world = World.Create();
        world.SubscribeEntityCreated((in Entity entity) => _asserter.Created.Add(entity));
        world.SubscribeEntityDestroyed((in Entity entity) => _asserter.Destroyed.Add(entity));
        
        // No events yet
        _asserter.AssertEvents();

        // Create entity and listen for created events
        var entity = world.Create();
        _asserter.AssertEvents(created: 1);
        That(_asserter.Created, Does.Contain(entity));
        _asserter.Clear();

        // Destroy entity and listen for destruction events
        world.Destroy(entity);
        _asserter.AssertEvents(destroyed: 1);
        That(_asserter.Destroyed, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void AddSingle()
    {
        using var world = World.Create();
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentOne _) => _asserter.CompOneAdded.Add(entity));
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentTwo _) => _asserter.CompTwoAdded.Add(entity));

        // Create entity to check if created and add event were fired
        var entity = world.Create<EventTestComponentOne>();
    
        _asserter.AssertEvents(compOneAdded: 1);
        That(_asserter.CompOneAdded, Does.Contain(entity));
        _asserter.Clear();
        
        // Add another component to see if add was fired
        world.Add<EventTestComponentTwo>(entity);
        
        _asserter.AssertEvents(compTwoAdded: 1);
        That(_asserter.CompTwoAdded, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void AddSingleObject()
    {
        using var world = World.Create();
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentTwo _) => _asserter.CompTwoAdded.Add(entity));

        // Create entity to check if created and add event were fired
        var entity = world.Create();
        world.Add(entity, (object)new EventTestComponentTwo());
        
        _asserter.AssertEvents(compTwoAdded: 1);
        That(_asserter.CompTwoAdded, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void AddMultiple()
    {
        using var world = World.Create();
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentOne _) => _asserter.CompOneAdded.Add(entity));
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentTwo _) => _asserter.CompTwoAdded.Add(entity));
        
        // Create entity to check if created and add event were fired
        var entity = world.Create<EventTestComponentOne, EventTestComponentTwo>();
    
        _asserter.AssertEvents(compOneAdded: 1, compTwoAdded:1);
        That(_asserter.CompOneAdded, Does.Contain(entity));
        That(_asserter.CompTwoAdded, Does.Contain(entity));
        _asserter.Clear();
        
        // Add another component to see if add was fired
        world.Remove<EventTestComponentOne, EventTestComponentTwo>(entity);
        world.Add<EventTestComponentOne, EventTestComponentTwo>(entity);
        
        _asserter.AssertEvents(compOneAdded: 1, compTwoAdded:1);
        That(_asserter.CompOneAdded, Does.Contain(entity));
        That(_asserter.CompTwoAdded, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void AddMultipleObject()
    {
        using var world = World.Create();
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentOne _) => _asserter.CompOneAdded.Add(entity));
        world.SubscribeComponentAdded((in Entity entity, ref EventTestComponentTwo _) => _asserter.CompTwoAdded.Add(entity));

        // Create entity to check if created and add event were fired
        var entity = world.Create();
        world.AddRange(entity, new object[] { new EventTestComponentOne(), new EventTestComponentTwo()});
        
        _asserter.AssertEvents(compOneAdded:1, compTwoAdded: 1);
        That(_asserter.CompOneAdded, Does.Contain(entity));
        That(_asserter.CompTwoAdded, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void SetSingle()
    {
        using var world = World.Create();
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentOne cmp) => _asserter.CompOneSet.Add((entity,cmp)));

        // Create entity to check if created and add event were fired
        var cmp = new EventTestComponentOne();
        var entity = world.Create(cmp);
        world.Set(entity, cmp);
    
        _asserter.AssertEvents(compOneSet:1);
        That(_asserter.CompOneSet, Does.Contain((entity,cmp)));
        _asserter.Clear();
    }
    
    [Test]
    public void SetSingleObject()
    {
        using var world = World.Create();
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentOne cmp) => _asserter.CompOneSet.Add((entity,cmp)));

        // Create entity to check if created and add event were fired
        var cmp = new EventTestComponentOne();
        var entity = world.Create(cmp);
        world.Set(entity, (object)cmp);
    
        _asserter.AssertEvents(compOneSet:1);
        That(_asserter.CompOneSet, Does.Contain((entity,cmp)));
        _asserter.Clear();
    }
    
    [Test]
    public void SetMultiple()
    {
        using var world = World.Create();
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentOne cmp) => _asserter.CompOneSet.Add((entity,cmp)));
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentTwo cmp) => _asserter.CompTwoSet.Add((entity,cmp)));
        
        // Create entity to check if created and add event were fired
        var cmpOne = new EventTestComponentOne();
        var cmpTwo = new EventTestComponentTwo();
        
        var entity = world.Create(cmpOne,cmpTwo);
        world.Set(entity, cmpOne, cmpTwo);
    
        _asserter.AssertEvents(compOneSet:1, compTwoSet:1);
        That(_asserter.CompOneSet, Does.Contain((entity,cmpOne)));
        That(_asserter.CompTwoSet, Does.Contain((entity,cmpTwo)));
        _asserter.Clear();
    }
    
    [Test]
    public void SetMultipleObject()
    {
        using var world = World.Create();
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentOne cmp) => _asserter.CompOneSet.Add((entity,cmp)));
        world.SubscribeComponentSet((in Entity entity, ref EventTestComponentTwo cmp) => _asserter.CompTwoSet.Add((entity,cmp)));
        
        // Create entity to check if created and add event were fired
        var cmpOne = new EventTestComponentOne();
        var cmpTwo = new EventTestComponentTwo();
        
        var entity = world.Create(cmpOne,cmpTwo);
        world.SetRange(entity,new object[]{cmpOne, cmpTwo});
    
        _asserter.AssertEvents(compOneSet:1, compTwoSet:1);
        That(_asserter.CompOneSet, Does.Contain((entity,cmpOne)));
        That(_asserter.CompTwoSet, Does.Contain((entity,cmpTwo)));
        _asserter.Clear();
    }
    
    [Test]
    public void RemoveSingle()
    {
        using var world = World.Create();
        world.SubscribeComponentRemoved((in Entity entity,ref EventTestComponentOne _) => _asserter.CompOneRemoved.Add(entity));

        var entity = world.Create<EventTestComponentOne>();
        world.Remove<EventTestComponentOne>(entity);

        _asserter.AssertEvents(compOneRemoved: 1);
        That(_asserter.CompOneRemoved, Does.Contain(entity));
        _asserter.Clear();
    }
    
    [Test]
    public void RemoveMultiple()
    {
        using var world = World.Create();
        world.SubscribeComponentRemoved((in Entity entity,ref EventTestComponentOne _) => _asserter.CompOneRemoved.Add(entity));
        world.SubscribeComponentRemoved((in Entity entity, ref EventTestComponentTwo _) => _asserter.CompTwoRemoved.Add(entity));
        
        var entity = world.Create<EventTestComponentOne, EventTestComponentTwo>();
        world.Remove<EventTestComponentOne, EventTestComponentTwo>(entity);

        _asserter.AssertEvents(compOneRemoved: 1, compTwoRemoved:1);
        That(_asserter.CompOneRemoved, Does.Contain(entity));
        That(_asserter.CompTwoRemoved, Does.Contain(entity));
        _asserter.Clear();
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
            That(Created, Has.Count.EqualTo(created));
            That(Destroyed, Has.Count.EqualTo(destroyed));
            That(CompOneAdded, Has.Count.EqualTo(compOneAdded));
            That(CompTwoAdded, Has.Count.EqualTo(compTwoAdded));
            That(CompOneRemoved, Has.Count.EqualTo(compOneRemoved));
            That(CompTwoRemoved, Has.Count.EqualTo(compTwoRemoved));
            That(CompOneSet, Has.Count.EqualTo(compOneSet));
            That(CompTwoSet, Has.Count.EqualTo(compTwoSet));
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

#endif
