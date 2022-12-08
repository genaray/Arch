# Arch
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg?style=for-the-badge)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)
[![Nuget](https://img.shields.io/nuget/v/Arch?style=for-the-badge)](https://www.nuget.org/packages/Arch/)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg?style=for-the-badge)](https://opensource.org/licenses/Apache-2.0)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

A highperformance C# based Archetype & Chunks [Entity Component System](https://www.wikiwand.com/en/Entity_component_system) (ECS) for game development and data oriented programming.    

- 🚀 **_FAST_** > Best cache efficiency, iteration and allocation speed. It plays in the same league as C++/Rust ECS Libs ! 
- 🚀🚀 **_FASTER_** > Arch is on average quite faster than other ECS implemented in C#. Check out this [Benchmark](https://github.com/Doraku/Ecs.CSharp.Benchmark) !
- 🤏 **_BARE MINIMUM_** >  Not bloated, its small and only provides the essentials for you ! 
- ☕️ **_SIMPLE_** >  Promotes a clean, minimal and self-explanatory API that is simple by design. Check out the [Wiki](https://github.com/genaray/Arch/wiki) !
- 💪 _**MAINTAINED**_ > Its actively being worked on, maintained and supported ! 
- 🚢 _**SUPPORT**_ > Supports .NetStandard 2.1, .Net Core 6 and 7 and therefore you may use it with Unity or Godot !

Download the [package](https://github.com/genaray/Arch/packages/1697222) and get started today ! 
```console
dotnet add PROJECT package Arch --version 1.0.16
```

# Code Sample

Enough spoken, lets take a look at some code. Arch is bare minimum, easy to use and efficient. Lets say we want to create some game entities and make them move based on their velocity, sounds complicated ?

Its not ! Arch does everything for you, you only need to define the entities and the logic.

```csharp
public class Game {

    public struct Position { public float x, y; }
    public struct Velocity { public float dx, dy; }
    
    public static void Main(string[] args) {
        
        var world = World.Create();
        var query = new QueryDescription{ All = { typeof(Position), typeof(Velocity) } };  // Query all entities with Position AND Velocity components

        // Create entities
        for (var index = 0; index < 1000; index++) 
            var entity = world.Create(new Position{ x = 0, y = 0}, new Velocity{ dx = 1, dy = 1});
        
        // Query and modify entities 
        world.Query(in query, (ref Position pos, ref Velocity vel) => {
            pos.x += vel.dx;
            pos.y += vel.dy;
        });
    }
}
```

# Contents

- [Quickstart](#quickstart)
  * [ECS](#ecs)
  * [World](#world)
  * [Entity](#entity)
  * [Component](#component)
  * [System aka. Query](#system-aka-query)
  * [Outlook](#outlook)
- [Performance](#performance)
  * [Benchmark](#benchmark)
- [Contributing](#contributing)

# Quickstart

I bet you dont wanna read tons of documentations, theory and other boring stuff right ?  
Lets just ignore all that deep knowledge and jump in directly to get something done. 
> For more detailed API and features, check out the [wiki](https://github.com/genaray/Arch/wiki) !

## ECS

Entity Component System (ECS) is a software architectural pattern mostly used for the representation of game world objects or data oriented design in general. An ECS comprises entities composed from components of data, with systems or queries which operate on entities' components.  

ECS follows the principle of composition over inheritance, meaning that every entity is defined not by a type hierarchy, but by the components that are associated with it.

## World

The world acts as a management class for all its entities, it contains methods to create, destroy and query them and handles all the internal mechanics.  
Therefore it is the most important class, you will use the world heavily.  
Multiple worlds can be used in parallel, each instance and its entities are completly encapsulated from other worlds. Currently worlds and their content can not interact with each other, however this feature is already planned. 

Worlds are created and destroyed like this...

```csharp
var world = World.Create();
World.Destroy(world);
```

There can be up to 255 worlds in total. 

## Entity

A entity represents your game entity.   
It is a simple struct with some metadata acting as a key to acess and manage its components.  

Entities are being created by a world and will "live" in the world in which they were created.  
When an entity is being created, you need to specify the components it will have. Components are basically the additional data or structure the entity will have. This is called "Archetype". 

```csharp
var otherEntity = world.Create<Transform, Collider, PowerUp>(... optional);

or

var archetype = new []{ typeof(Position), typeof(Velocity), ... };
var entity = world.Create(archetype);

world.Destroy(in entity);
```
> Entity creation/deletion should not happen during a Query ! [CommandBuffers](https://github.com/genaray/Arch/wiki/Quickstart#command-buffers) can be used for this ! :) 

## Component

Components are data assigned to your entity. With them you define how an entity looks and behaves, they basically define the gamelogic with pure data.   
Its recommended to use struct components since they offer better speed. 

To ease writing code, you can acess the entity directly to modify its components or to check its metadata.  
A small example could look like this...

```csharp
var entity = world.Create<Position, Velocity>();

ref var position = ref entity.Get<Position>();    // Get reference to the position
position.x++;                                     // Update x
position.y++;                                     // Update y

if(entity.Has<Position>())                        // Make sure that entity has a position ( Optional )
    entity.Set(new Position{ x = 10, y = 10 };    // Replaces the old position 

entity.Remove<Velocity>();                         // Removes an velocity component and moves it to a new archetype.
entity.Add<Velocity>(new Velocity{ x = 1, y = 1);  // Adds an velocity component and moves the entity back to the previous archetype. 
```

> Structural entity changes should not happen during a Query or Iteration ! [CommandBuffers](https://github.com/genaray/Arch/wiki/Quickstart#command-buffers) can be used for this ! :) 

## System aka. Query

Queries aka. Systems are used to iterate over a set of entities to apply logic and behaviour based on their components. 

This is performed by using the world ( remember, it manages your created entities ) and by defining a description of which entities we want to iterate over. 

```csharp
// Define a description of which entities you want to query
var query = new QueryDescription {
    All = new []{ typeof(Position), typeof(Velocity) },   // Should have all specified components
    Any = new []{ typeof(Player), typeof(Projectile) },   // Should have any of those
    None = new []{ typeof(AI) }                           // Should have none of those
};

// Execute the query
world.Query(in query, entity => { /* Do something */ });

// Execute the query and modify components in the same step, up to 10 generic components at the same time. 
world.Query(in query, (ref Position pos, ref Velocity vel) => {
    pos.x += vel.dx;
    pos.y += vel.dy;
});
```

In the example above we want to move our entities based on their `Position` and `Velocity` components. 
To perform this operation we need to iterate over all entities having both a `Position` and `Velocity` component (`All`). We also want that our entity either is a `Player` or a `Projectile` (`Any`). However, we do not want to iterate and perform that calculation on entities which are controlled by an `AI` (`None`). 

The `world.Query` method than smartly searches for entities having both a `Position` and `Velocity`, either a `Player` or `Projectile` and no `AI` component and executes the defined logic for all of those fitting entities. 

Besides `All`, `Any` and `None`, `QueryDescription` can also target a exclusive set of components via `Exclusive`. If thats set, it will ignore `All`, `Any` and `None` and only target entities with a exactly defined set of components. Its also important to know that there are multiple different overloads to perform such a query.
> The less you query in terms of components and the size of components... the faster the query is !

## Outlook

This is all you need to know, with this little knowledge you are already able to bring your worlds to life.  
However, if you want to take a closer look at Arch's features and performance techniques, check out the [Wiki](https://github.com/genaray/Arch/wiki) ! 
Theres more to explore, for example...

- Bulk Entity Adding
- Highperformance Queries
- Archetypes
- Chunks
- Parallel / Multithreaded Queries
- Enumerators
- CommandBuffers
- More api 


# Performance
Well... its fast, like REALLY fast.  
However the iteration speed depends, the less you query, the faster it is.  
This rule targets the amount of queried components aswell as their size.  

Based on https://github.com/Doraku/Ecs.CSharp.Benchmark - Benchmark, it is among the fastest ecs frameworks in terms of allocation and iteration. 

## Benchmark
The current Benchmark tested a bunch of different iterations and acess techniques. However the most interesting one is the `QueryBenchmark`. 
It tests `world.Query` against `world.HPQuery` and a `world.Query(in desc, (in Entity) => { entity.Get<T>... }` variant. 

```CSHARP
public struct Transform{ float x; float y; float z; }
public struct Velocity { float x; float y; }
```

The used structs are actually quite big, the smaller the components, the faster the query. However i wanted to create a realistic approach and therefore used a combination of Transform and Velocity. 

|            Method |  Amount |          Mean |         Error |      StdDev | Allocated |
|------------------ |-------- |--------------:|--------------:|------------:|----------:|
|             Query |   10000 |     20.648 us |    16.4985 us |   0.9043 us |         - |
|       EntityQuery |   10000 |     17.791 us |     1.1502 us |   0.0630 us |         - |
|       StructQuery |   10000 |      7.517 us |     5.6294 us |   0.3086 us |         - |
| StructEntityQuery |   10000 |      7.851 us |     0.9644 us |   0.0529 us |         - |
|   PureEntityQuery |   10000 |    513.765 us |   297.3252 us |  16.2974 us |         - |
|             Query |  100000 |    199.354 us |     0.8275 us |   0.0454 us |         - |
|       EntityQuery |  100000 |    200.223 us |    23.0012 us |   1.2608 us |         - |
|       StructQuery |  100000 |     75.616 us |    46.9128 us |   2.5714 us |         - |
| StructEntityQuery |  100000 |     79.390 us |     8.5673 us |   0.4696 us |         - |
|   PureEntityQuery |  100000 |  5,032.846 us | 2,116.4383 us | 116.0091 us |         - |
|             Query | 1000000 |  2,257.803 us |    52.4331 us |   2.8740 us |         - |
|       EntityQuery | 1000000 |  2,661.399 us |    74.9702 us |   4.1094 us |         - |
|       StructQuery | 1000000 |  1,556.662 us |   734.0822 us |  40.2375 us |         - |
| StructEntityQuery | 1000000 |  1,746.782 us |   627.7881 us |  34.4112 us |         - |
|   PureEntityQuery | 1000000 | 50,894.500 us | 1,944.0534 us | 106.5601 us |         - |

# Contributing

I will accept contributions, especially bugfixes, performance improvements and new features.
New features however should not harm its performance, if they do they should be wrapped within predecessor variables for enabling/disabling them. 
