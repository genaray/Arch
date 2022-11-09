# Arch
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg?style=for-the-badge)](https://GitHub.com/Naereen/StrapDown.js/graphs/commit-activity)
[![Nuget](https://img.shields.io/nuget/v/Arch?style=for-the-badge)](https://www.nuget.org/packages/Arch/)
[![License](https://img.shields.io/badge/License-Apache_2.0-blue.svg?style=for-the-badge)](https://opensource.org/licenses/Apache-2.0)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

A C# based Archetype [Entity Component System](https://www.wikiwand.com/en/Entity_component_system) (ECS).  

Each Archetype stores their entities within 16KB sized chunks perfectly fitting into L1 Caches for maximum iteration performance.  
This technique has two main advantages, first of all it provides an great entity allocation speed and second it lowers the cache misses to the best possible minimum. 
Its incredible fast, especially for well architectured component structures. 

Supports .NetStandard 2.1, .Net Core 6 and 7.  
Since .NetStandard is supported, you may also use it with Unity or Godot. 

Download the [package](https://github.com/genaray/Arch/packages/1697222) and get started today ! 
```sh
dotnet add PROJECT package Arch --version 1.0.8
```

# Code Sample

Enough spoken, lets take a look at some code. Arch is bare minimum, easy to use and efficient. Lets say we want to create some game entities and make them move based on their velocity, sounds complicated ?

Its not ! Arch does everything for you, you only need to define the entities and the logic.

```csharp
public class Game {

    public struct Position { public float x, y; }
    public struct Velocity { public float dx, dy; }
    
    // The entity structure and or filter/query
    public static Type[] archetype = { typeof(Position), typeof(Velocity) };
    
    public static void Main(string[] args) {
        
        var world = World.Create();
        var query = new QueryDescription{ All = archetype };  // Query all entities with Position AND Velocity components

        // Create entities
        for (var index = 0; index < 1000; index++) {

            var entity = world.Create(archetype);
            entity.Set(new Position{ x = 0, y = 0});
            entity.Set(new Velocity{ dx = 1, dy = 1});
        }

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
var archetype = new []{ typeof(Position), typeof(Velocity), ... };
var entity = world.Create(archetype);
world.Destroy(in entity);
```

## Component

Components are data assigned to your entity. With them you define how an entity looks and behaves, they basically define the gamelogic with pure data.   
Its recommended to use struct components since they offer better speed. 

To ease writing code, you can acess the entity directly to modify its components or to check its metadata.  
A small example could look like this...

```csharp
var archetype = new []{ typeof(Position), typeof(Velocity) };
var entity = world.Create(archetype);

ref var position = ref entity.Get<Position>();    // Get reference to the position
position.x++;                                     // Update x
position.y++;                                     // Update y

if(entity.Has<Position>())                        // Make sure that entity has a position ( Optional )
    entity.Set(new Position{ x = 10, y = 10 };    // Replaces the old position 
```

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

Its also important to know that there are multiple different overloads to perform such a query.
> The less you query in terms of components and the size of components... the faster the query is !

## Outlook

This is all you need to know, with this little knowledge you are already able to bring your worlds to life.  
However, if you want to take a closer look at Arch's features and performance techniques, check out the [Wiki](https://github.com/genaray/Arch/wiki) ! 
Theres more to explore, for example...

- Bulk Entity Adding
- Highperformance Queries
- Archetypes
- Chunks
- More api 


# Performance
Well... its fast, like REALLY fast.  
However the iteration speed depends, the less you query, the faster it is.  
This rule targets the amount of queried components aswell as their size.  

Based on https://github.com/Doraku/Ecs.CSharp.Benchmark - Benchmark, it is among the fastest ecs frameworks in terms of allocation and iteration. 

## Benchmark
The current Benchmark only tests it Archetype/Chunk iteration performance.  
Two different iteration techniques, 2 Components ( Transform & Rotation ) modification and Entity + 2 Components modification. 

```CSHARP
public struct Transform{ float x; float y; float z; }
public struct Rotation{ float x; float y; float z; float w; }
```

The used structs are actually quite big, the smaller the components, the faster the query. However i wanted to create a realistic approach and therefore used a combination of Transform and Rotation. 

|                                Method |   amount |         Mean |      Error |     StdDev | CacheMisses/Op |
|-------------------------------------- |--------- |-------------:|-----------:|-----------:|---------------:|
|          IterationNormalTwoComponents |    10000 |     27.94 us |   0.146 us |   0.129 us |             41 | 
|       IterationUnsafeAddTwoComponents |    10000 |     25.46 us |   0.112 us |   0.105 us |             18 |
|    IterationNormalEntityTwoComponents |    10000 |     40.44 us |   0.191 us |   0.179 us |             35 | 
| IterationUnsafeAddEntityTwoComponents |    10000 |     37.95 us |   0.164 us |   0.146 us |             41 | 
|          IterationNormalTwoComponents |   100000 |    275.20 us |   2.149 us |   2.010 us |            540 | 
|       IterationUnsafeAddTwoComponents |   100000 |    250.88 us |   1.380 us |   1.223 us |            549 | 
|    IterationNormalEntityTwoComponents |   100000 |    397.54 us |   1.935 us |   1.810 us |            842 | 
| IterationUnsafeAddEntityTwoComponents |   100000 |    373.28 us |   1.713 us |   1.519 us |            695 | 
|          IterationNormalTwoComponents |  1000000 |  2,810.57 us |  16.850 us |  15.762 us |         24,009 | 
|       IterationUnsafeAddTwoComponents |  1000000 |  2,573.61 us |  13.632 us |  12.752 us |         24,724 |
|    IterationNormalEntityTwoComponents |  1000000 |  4,050.63 us |  36.415 us |  34.063 us |         28,637 |
| IterationUnsafeAddEntityTwoComponents |  1000000 |  3,804.67 us |  29.850 us |  23.305 us |         28,960 |
|          IterationNormalTwoComponents | 10000000 | 32,790.69 us | 176.802 us | 165.381 us |        257,843 |
|       IterationUnsafeAddTwoComponents | 10000000 | 30,275.69 us | 261.629 us | 244.728 us |        271,411 | 
|    IterationNormalEntityTwoComponents | 10000000 | 45,073.30 us | 365.498 us | 341.887 us |        323,789 | 
| IterationUnsafeAddEntityTwoComponents | 10000000 | 43,000.07 us | 205.964 us | 192.659 us |        304,333 |

# Contributing

I will accept contributions, especially bugfixes, performance improvements and new features.
New features however should not harm its performance, if they do they should be wrapped within predecessor variables for enabling/disabling them. 
