# Arch
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg?style=for-the-badge)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)
[![Nuget](https://img.shields.io/nuget/v/Arch?style=for-the-badge)](https://www.nuget.org/packages/Arch/)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg?style=for-the-badge)](https://opensource.org/licenses/Apache-2.0)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

A high-performance C# based Archetype & Chunks [Entity Component System](https://www.wikiwand.com/en/Entity_component_system) (ECS) for game development and data-oriented programming.     

- 🚀 **_FAST_** > Best cache efficiency, iteration, and allocation speed. Plays in the same league as C++/Rust ECS Libs! 
- 🚀🚀 **_FASTER_** > Arch is on average quite faster than other ECS implemented in C#. Check out this [Benchmark](https://github.com/Doraku/Ecs.CSharp.Benchmark)!
- 🤏 **_BARE MINIMUM_** >  Not bloated, it's small and only provides the essentials for you! 
- ☕️ **_SIMPLE_** >  Promotes a clean, minimal, and self-explanatory API that is simple by design. Check out the [Wiki](https://github.com/genaray/Arch/wiki)!
- 💪 _**MAINTAINED**_ > It's actively being worked on, maintained, and comes along several [Extensions](https://github.com/genaray/Arch.Extended)! 
- 🚢 _**SUPPORT**_ > Supports .NetStandard 2.1, .Net Core 6 and 7, and therefore you may use it with Unity or Godot!

Download the [package](https://github.com/genaray/Arch/packages/1697222), get started today and join the [Discord](https://discord.gg/htc8tX3NxZ)!
```console
dotnet add PROJECT package Arch --version 1.2.7.1-alpha
```

# Code Sample

Arch is bare minimum, easy to use, and efficient. Let's say you want to create some game entities and make them move based on their velocity... sounds complicated?
It's not! Arch does everything for you, you only need to define the entities and the logic.

```cs
// Components ( ignore the formatting, this saves space )
public struct Position{ float X, Y };
public struct Velocity{ float Dx, Dy };

public class Game 
{
    public static void Main(string[] args) 
    {     
        // Create a world and entities with position and velocity.
        var world = World.Create();
        for (var index = 0; index < 1000; index++) 
            world.Create(new Position{ X = 0, Y = 0}, new Velocity{ Dx = 1, Dy = 1});
        
        // Query and modify entities ( There are also alternatives without lambdas ;) ) 
        var query = new QueryDescription().WithAll<Position,Velocity>(); // Targets entities with Position AND Velocity.
        world.Query(in query, (ref Position pos, ref Velocity vel) => {
            pos.X += vel.Dx;
            pos.Y += vel.Dy;
        });
    }
}
```

# Contents

- [Quick start](#quick-start)
  * [ECS](#ecs)
  * [World](#world)
  * [Entity](#entity)
  * [Component](#component)
  * [System aka. Query](#system-aka-query)
  * [More Features and Outlook](#more-features-and-outlook)
- [Performance](#performance)
  * [Benchmark](#benchmark)
- [Extensions](#extensions)
- [Projects using Arch](#projects-using-arch)

# Quick start

I bet you don't want to read tons of documentation, theory, and other boring stuff right?  
Let's just ignore all that deep knowledge and jump in directly to get something done. 
> For more detailed API and features, check out the [wiki](https://github.com/genaray/Arch/wiki)!

## ECS

Entity Component System (ECS) is a software architectural pattern mostly used for the representation of game world objects or data-oriented design in general. An ECS comprises entities composed of components of data, with systems or queries which operate on entities' components.  

ECS follows the principle of composition over inheritance, meaning that every entity is defined not by a type hierarchy, but by the components that are associated with it.

## World

The world acts as a management class for all its entities, it contains methods to create, destroy and query them and handles all the internal mechanics.  
Therefore, it is the most important class, you will use the world heavily.  
Multiple worlds can be used in parallel, each instance and its entities are completely encapsulated from other worlds. Currently, worlds and their content can not interact with each other, however, this feature is already planned. 

Worlds are created and destroyed like this...

```csharp
var world = World.Create();
World.Destroy(world);
```

There can be up to `2,147,483,647` possible worlds with up to `2,147,483,647` entities each. 

## Entity

An entity represents your game entity.   
It is a simple struct with some metadata acting as a key to access and manage its components.  

Entities are being created by a world and will "live" in the world in which they were created.  
When an entity is created, you need to specify the components it will have. Components are the additional data or structure the entity will have. This is called "Archetype". 

```csharp
var otherEntity = world.Create<Transform, Collider, PowerUp>(... optional);

or

var archetype = new ComponentType[]{ typeof(Position), typeof(Velocity), ... };
var entity = world.Create(archetype);

world.Destroy(entity);
```

## Component

Components are data assigned to your entity. With them you define how an entity looks and behaves, they define the game logic with pure data.   
It's recommended to use struct components since they offer better speed. 

To ease writing code, you can access the entity directly to modify its components or to check its metadata.  
A small example could look like this...

```csharp
var entity = world.Create<Position, Velocity>();

ref var position = ref entity.Get<Position>();    // Get reference to the position.
position.X++;                                     // Update x.
position.Y++;                                     // Update y.

if(entity.Has<Position>())                        // Make sure that entity has a position (Optional).
    entity.Set(new Position{ X = 10, Y = 10 };    // Replaces the old position .

entity.Remove<Velocity>();                         // Removes a velocity component and moves it to a new archetype.
entity.Add<Velocity>(new Velocity{ X = 1, Y = 1);  // Adds a velocity component and moves the entity back to the previous archetype. 
```

## System aka. Query

Queries aka. Systems are used to iterate over a set of entities to apply logic and behavior based on their components. 

This is performed by using the world (remember, it manages your created entities) and by defining a description of which entities we want to iterate over. 

```csharp
// Define a description of which entities you want to query
var query = new QueryDescription().    
            WithAll<Position,Velocity>().      // Should have all specified components
            WithAny<Player,Projectile>().      // Should have any of those
            WithNone<AI>();                    // Should have none of those

// Execute the query
world.Query(in query, (Entity entity) => { /* Do something */ });

// Execute the query and modify components in the same step, up to 10 generic components at the same time. 
world.Query(in query, (ref Position pos, ref Velocity vel) => {
    pos.X += vel.Dx;
    pos.Y += vel.Dy;
});
```

In the example above, we want to move our entities based on their `Position` and `Velocity` components. 
To perform this operation, we need to iterate over all entities having both a `Position` and `Velocity` component (`All`). We also want that our entity either is a `Player` or a `Projectile` (`Any`). However, we do not want to iterate and perform that calculation on entities that are controlled by an `AI` (`None`). 

The `world.Query` method then smartly searches for entities having both a `Position` and `Velocity`, either a `Player` or `Projectile`, and no `AI` component and executes the defined logic for all of those fitting entities. 

Besides `All`, `Any`, and `None`, `QueryDescription` can also target an exclusive set of components via `Exclusive`. If that's set, it will ignore `All`, `Any`, and `None` and only target entities with an exactly defined set of components. It's also important to know that there are multiple different overloads to perform such a query.
> The less you query in terms of components and the size of components... the faster the query is!

## More features and Outlook

This is all you need to know, with this little knowledge you are already able to bring your worlds to life.  
However, if you want to take a closer look at Arch's features and performance techniques, check out the [Wiki](https://github.com/genaray/Arch/wiki)! 
There's more to explore, for example...

- Bulk Entity Creation
- Batch Operations
- High-performance Queries
- Archetypes
- Chunks
- Parallel / Multithreaded Queries
- Enumerators
- CommandBuffers
- Pure ECS
- Events
- Monogame, Unity, Godot Integration guides
- More API 

# Extensions

Arch has some extensions that add more features and tools. Among them for example : 
- 🛠️ **_[Arch.Extended](https://github.com/genaray/Arch.Extended)_** >  Adds a set of tools and features to save boilerplate code!
- 🔎 *_[Godot Entity Debugger](https://github.com/RoadTurtleGames/ArchGodotEntityDebugger)_* > An Arch Entity debugger for the Godot engine!
- 🔎 *_[Stride Entity Debugger](https://github.com/Doprez/stride-arch-ecs)_* > An example of Arch in the Stride engine, with additional entity and system inspector!
- ❓ **_Your Tool-Library?_** > If you develop more tools and features for Arch, let us know and we'll list them here!

# Performance
Well... it's fast, like REALLY fast.  
However, the iteration speed depends, the less you query, the faster it is.  
This rule targets the amount of queried components as well as their size.  

Based on https://github.com/Doraku/Ecs.CSharp.Benchmark - Benchmark, it is among the fastest ECS frameworks in terms of allocation and iteration. 

## Benchmark
The current Benchmark tested a bunch of different iterations and access techniques. However, the most interesting one is the `QueryBenchmark`. 
In the benchmark, a set of entities was iterated over using the framework to access their transform and velocity and calculate a new position each iteration.

Their components looked like this:

```CSHARP
public struct Transform{ float x; float y; float z; }
public struct Velocity { float x; float y; float z; }
```

The following performance was achieved with Arch for the scenario under heavy load and different amount of entities. 

|            Method |  Amount |          Mean |         Error |      StdDev | CacheMisses/Op | Allocated |
|------------------ |-------- |--------------:|--------------:|------------:|---------------:|----------:|
|  WorldEntityQuery |   10000 |    111.003 us |    13.2838 us |   0.7281 us |            409 |         - |
|             Query |   10000 |     20.159 us |     1.4188 us |   0.0778 us |            103 |         - |
|       EntityQuery |   10000 |     17.711 us |     1.1311 us |   0.0620 us |             49 |         - |
|       StructQuery |   10000 |      7.767 us |     0.1572 us |   0.0086 us |              7 |         - |
| StructEntityQuery |   10000 |      7.338 us |     1.7188 us |   0.0942 us |             12 |         - |
|  WorldEntityQuery |  100000 |  1,326.959 us | 3,058.5935 us | 167.6518 us |          5,753 |         - |
|             Query |  100000 |    203.555 us |     4.6038 us |   0.2523 us |          2,977 |         - |
|       EntityQuery |  100000 |    228.222 us |    17.4030 us |   0.9539 us |          2,708 |         - |
|       StructQuery |  100000 |    115.466 us |     8.8355 us |   0.4843 us |          2,726 |         - |
| StructEntityQuery |  100000 |     76.823 us |     2.1875 us |   0.1199 us |          2,544 |         - |
|  WorldEntityQuery | 1000000 | 12,519.798 us | 4,491.2760 us | 246.1820 us |         45,604 |         - |
|             Query | 1000000 |  2,679.153 us |    35.1696 us |   1.9278 us |         28,579 |         - |
|       EntityQuery | 1000000 |  2,462.296 us |   322.4767 us |  17.6760 us |         28,113 |         - |
|       StructQuery | 1000000 |  1,514.479 us |   296.5311 us |  16.2539 us |         29,723 |         - |
| StructEntityQuery | 1000000 |  1,483.142 us |   329.9446 us |  18.0854 us |         31,272 |         - |


# Projects using Arch
Arch is already used in some projects, for a more detailed look, take a look at the wiki!
> https://github.com/genaray/Arch/wiki/Projects-using-Arch

## [Space Station 14](https://spacestation14.io/)
Space Station 14 is inspired by the cult classic Space Station 13 and tells the extraordinary story of everything that can go wrong on a shift at a space station. You take on a role and complete your tasks so that the space station doesn't go to the dogs... or do the exact opposite. Prepare yourself for chaos and the finest roleplay. Best of all, SS14 is open-source and anyone can play!

## [Roguelite-Survivor](https://github.com/proc-gen/roguelite-survivor)
An action-packed c# clone of the hit "vampire survivor" based on monogame and arch!
Fight your way through hordes of different enemies, level up your character, collect permanent items and explore various maps!
Try it out!

## [EquilibriumEngine-CSharp](https://github.com/clibequilibrium/EquilibriumEngine-CSharp)
Equilibrium Engine is a data-oriented C# game engine that takes advantage of ECS pattern followed by Hot-Reloading of your libraries which allows you to quickly iterate on different aspects of your projects.
