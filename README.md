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
Since .NetStandard is supported, you may also use it with Unity. 

Download the [package](https://github.com/genaray/Arch/packages/1697222) and get started today ! 
```sh
dotnet add PROJECT package Arch --version 1.0.7
```

# Code Sample

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

# Content
- [Quickstart](#quickstart)
  * [ECS](#ecs)
  * [World](#world)
  * [Entity](#entity)
  * [Querying and Filtering](#querying-and-filtering)
  * [Bulk adding](#bulk-adding)
- [Internal Structure and Memory layout](#internal-structure-and-memory-layout)
  * [Archetype](#archetype)
  * [Chunks](#chunks)
  * [Archetype and Chunk usage](#archetype-and-chunk-usage)
- [Performance](#performance)
  * [Benchmark](#benchmark)
    + [NET.7](#net7)
    + [Different Iteration & Acess Techniques](#different-iteration-and-acess-techniques)
    + [Legend](#legend)

# Quickstart
## ECS

Entity Component System (ECS) is a software architectural pattern mostly used for the representation of game world objects or data oriented design in general. An ECS comprises entities composed from components of data, with systems or queries which operate on entities' components.  

ECS follows the principle of composition over inheritance, meaning that every entity is defined not by a type hierarchy, but by the components that are associated with it.

## World

The world acts as a management class for all its entities, it contains methods to create, destroy and query for them and handles all the internal mechanics.  
Therefore it is the most important class, you will use the world heavily.  
Multiple worlds can be used in parallel, each instance and its entities is completly encapsulated from other worlds. 

Worlds are created and destroyed like this...

```csharp
var world = World.Create();
World.Destroy(world);
```

There can be up to 255 worlds in total. 

## Entity

A entity represents your game entity.   
It is a simple struct with some metadata acting as a key to acess and manage its components.  

```csharp
public readonly struct Entity : IEquatable<Entity> {
        
    public readonly int EntityId;    // Its id/key in the world
    public readonly byte WorldId;    // The world the entity lives in
    public readonly ushort Version;  // Its version, how often the entity or its id was recycled
    ....
}
```

Entities are being created by a world and will "live" in the world in which they were created.  
When an entity is being created, you need to specify the components it will have. Components are basically the additional data or structure the entity will have. This is called "Archetype". 

```csharp
var archetype = new []{ typeof(Position), typeof(Velocity), ... };
var entity = world.Create(archetype);
world.Destroy(in entity);
```

To ease writing code, you can acess the entity directly to modify its data or to check its metadata.  
Lets take a look at the most important methods. 

```csharp
entity.IsAlive();                     // True if the entity is still existing in its world
entity.Has<Position>();               // True if the entity has a position component
entity.Set(new Position( x = 10 ));   // Replaces the position component and updates it data
entity.Get<Position>();               // Returns a reference to the entity position, can directly acess and update position attributes

entity.GetComponentTypes();           // Returns an array of its component types. Should be treated as readonly 
entity.GetComponents();               // Returns an array of all its components, allocates memory. 
```

With those utility methods you are able to implement your game logic.  
A small example looks like this...

```csharp
var archetype = new []{ typeof(Position), typeof(Velocity) };
var entity = world.Create(archetype);

ref var position = ref entity.Get<Position>();    // Get reference to the position
position.x++;                                     // Update x
position.y++;                                     // Update y

if(entity.Has<Position>())                        // Make sure that entity has a position ( Optional )
    entity.Set(new Position{ x = 10, y = 10 };    // Replaces the old position 
```

## Querying and Filtering

To performs operations and to define your game logic, queries are used to iterate over entities.  
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

```csharp

world.Query(in query, entity => {});                                     // Passes the fitting entity
world.Query(in query, (ref T1 t1, T2 t2, ...) => {})                     // Passes the defined components of the fitting entity, up to 10 components
world.Query(in query, (in Entity en, ref T1 t1, ref T2 t2, ...) => {})   // Passed the fitting entity and its defined components, up to 10 components 

var filteredEntities = new List<Entity>();
var filteredArchetypes = new List<Archetype>();
var filteredChunks = new List<Chunk>();

world.GetEntities(query, filteredEntities);                             // Fills all fitting entities into the passed list
world.GetArchetypes(query, filteredArchetypes);                         // Fills all fitting archetypes into the list
world.GetChunks(query, filteredChunks);                                 // Fills all fitting chunks into the list 
```

Archetype's and Chunk's are internal structures of the world and store entities with the same component types. You will mostly never use them directly, therefore more on them later. 

## Bulk adding

Arch supports bulk adding of entities, this is incredible fast since it allows us to allocate enough space for a certain set of entities in one go. This reservation happens on top of the already existing entities in an archetype. You only need to reserve space once and than it will be filled later or sooner. 

```csharp
var archetype = new []{ typeof(Position), typeof(Velocity) };

// Create 1k entities
for(var index = 0; index < 1000; index++)
    world.Create(archetype)

world.Reserve(archetype, 1000000);              // Reserves space for additional 1mil entities
for(var index = 0; index < 1000000; index++)    // Create additional 1 mil entities
    world.Create(archetype)

// In total there now 1mil and 1k entities in that certain archetype. 
```

# Internal Structure and Memory layout

Arch is an archetype ecs. An archetype ecs groups entities with the same set of components in tightly packed arrays for the fastest possible iteration performance. 
This has no direct effect on its API useage or the way you develop your game. But understanding the internal structure can help you to improve your game performance even more. 
However it has an big impact on the internal structures being used and is the secret to its incredible performance. 

## Archetype

An archetype manages all entities with a common set of components. Like the world is used to manage ALL entities, the archetype is only used to manage a specific set of entities... all entities with the same component structure. The world stores those archetypes and acesses them to iterate, create, update and remove entities. 

```csharp
// Creates one entity, one archetype where all entities with Position and Velocity will be stored
var archetype = new []{ typeof(Position), typeof(Velocity) };
var entity = world.Create(archetype);                            

// Creates another entity, another archetype where all entities with Position, Velocity AND Rotation will be stored
var secondArchetype = new []{ typeof(Position), typeof(Velocity), typeof(Rotation) } ; 
var secondEntity = world.Create(secondArchetype);
```

You may probably now ask : `"Why do this create two seperate archtypes ? Both entities share position and velocity, so they could be stored together."`... Shared subsets of component do NOT matter... all what matters is the exact structure of an entity. Why is that ? Because this way we can utilize the cache during iterations, however explaining this would probably break the scope of this documentation. 

## Chunks

Chunks are were the entities and their components are stored. They utilize dense packed contiguous arrays ( [SoA](https://www.wikiwand.com/en/AoS_and_SoA#:~:text=Structure%20of%20arrays%20(SoA)%20is,one%20parallel%20array%20per%20field.) ) to store and acess entities with the same group of components. Its internal arrays are always 16KB in total, this is intended since 16kb fits perfectly into the L1 CPU Cache which gives us insane iteration speeds. 

The internal structure simplified looks like this...
```
Chunk
[
    [Entity, Entity, Entity],
    [Position, Position, Position],
    [Velocity, Velocity, Velocity]
    ...
]
```

This way they are fast to (de)allocate which also reduces memory useage to a minimum. 
Each archetype contains multiple chunks and will create and destroy chunks based on worlds need. 

## Archetype and Chunk usage

Arch gives you acess to the internal structures aswell. You will mostly do not need this feature, however it can be usefull to leverage the performance, writing custom queries or add new features. 

```csharp
var archetypes = world.Archetypes;  // Returns direct acess to all archetypes of the world
var chunks = archetypes[0].Chunks   // Returns direct acess to all chunks of a archetype
 
world.GetArchetypes(in queryDescription, myArchetypeList);  // Fills all archetypes fitting the query description into the passed list
world.GetChunks(in queryDescription, myChunkList);          // Fills all chunks fitting the query description into the passed list
```

Acessing or using those methods, you will have the power to write and optimize queries yourself. If you want you can even implement new features using them.  
Lets take a look at how the `world.Query` methods are implemented using those.

```csharp
// Inside the world
public void Query<T0>(in QueryDescription description, ForEach<T0> forEach)
{

    // Cache query
    if (!QueryCache.TryGetValue(description, out var query))
    {
        query = new Query(description);
        QueryCache[description] = query;
    }

    var size = Archetypes.Count;
    for (var index = 0; index < size; index++)
    {
        var archetype = Archetypes[index];      
        var archetypeSize = archetype.Size;
        var bitset = archetype.BitSet;
        
        if (!query.Valid(bitset)) continue;  // Process archetype & chunks if it fits the query description
        
        // Loop over all its chunks in an unsafe manner
        ref var chunkFirstElement = ref archetype.Chunks[0];
        for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++)
        {
            ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
            var chunkSize = chunk.Size;

            var t0Array = chunk.GetArray<T0>();        // Acess component array
            ref var t0FirstElement = ref t0Array[0];

            // Loop over all components in an unsafe manner
            for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++)
            {
                ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
                forEach(ref t0Component);
            }
        }
    }
}
```

With the power of acessing `Archetype` and `Chunk` directly from the world, you can easily write such high performance queries yourself. Great ! Isnt it ? :)  
Since this is pretty dangerous you should only do this when you are already familiar with archetypes and chunks. Those features should be handled as readonly as long as you do not really know what you are doing. However entities should ONLY be created and removed using the world directly. 

Lets look at an small example of how you could utilize those features...

```csharp
var archetype = new[]{ typeof(Position); }
var queryDesc = new QueryDescription{ All = archetype };

var allChunks = new List<Chunk>();
world.Query(in queryDesc, allChunks); // Get all chunks for entities with a position component

foreach(var chunk in allChunks){

    // Acess the position component array and loop over each position
    var positions = chunk.GetArray<Position>();
    for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++){
    
        ref var pos = ref positions[entityIndex];
        Console.WriteLine($"{pos.x}/{pos.y}");

        pos.x++;
        pos.y++;
    }
}
```

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


### NET.7

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

### Different Iteration and Acess Techniques

We have been testing different strategies and techniques to iterate over the archetype itself and all its chunks for providing the best overall performance.
Suprisingly all of them are great but especially the Unsafe.Add Iterations were quite faster. Thats why we picked the techniques of `IterationUnsafeAddTwoComponents` and `IterationUnsafeAddTwoComponentsUnsafeArray` and decided that `IterationUnsafeAddTwoComponents` is the best overall since it comes along the least CacheMisses. We will run this benchmark regulary in the future to adjust the ECS performance based on new .NET improvements. 


|                                        Method |   amount |         Mean |      Error |     StdDev |       Median | CacheMisses/Op |
|---------------------------------------------- |--------- |-------------:|-----------:|-----------:|-------------:|---------------:|
|                  IterationNormalTwoComponents |    10000 |     27.92 us |   0.121 us |   0.107 us |     27.96 us |             30 |   
|               IterationUnsafeAddTwoComponents |    10000 |     25.33 us |   0.090 us |   0.080 us |     25.36 us |             13 |      
|              IterationNormalTwoComponentsSpan |    10000 |     27.96 us |   0.080 us |   0.071 us |     27.93 us |             22 |      
|           IterationUnsafeAddTwoComponentsSpan |    10000 |     25.50 us |   0.104 us |   0.087 us |     25.51 us |             22 |    
|       IterationNormalTwoComponentsUnsafeArray |    10000 |     27.98 us |   0.179 us |   0.168 us |     27.99 us |             23 |      
|    IterationUnsafeAddTwoComponentsUnsafeArray |    10000 |     25.32 us |   0.123 us |   0.102 us |     25.33 us |             27 |      
|        IterationNormalTwoComponentsUnsafeSpan |    10000 |     27.92 us |   0.158 us |   0.148 us |     27.95 us |             26 |       
|     IterationUnsafeAddTwoComponentsUnsafeSpan |    10000 |     25.36 us |   0.126 us |   0.118 us |     25.34 us |             29 |       
| IterationUnsafeAddTwoComponentsCompleteUnsafe |    10000 |     25.35 us |   0.082 us |   0.077 us |     25.34 us |             37 |       
|                  IterationNormalTwoComponents |   100000 |    277.82 us |   3.237 us |   2.870 us |    276.88 us |            943 |     
|               IterationUnsafeAddTwoComponents |   100000 |    249.18 us |   0.717 us |   0.670 us |    249.02 us |            353 |     
|              IterationNormalTwoComponentsSpan |   100000 |    276.43 us |   2.455 us |   2.050 us |    276.19 us |            544 |     
|           IterationUnsafeAddTwoComponentsSpan |   100000 |    249.42 us |   0.552 us |   0.489 us |    249.54 us |            364 |     
|       IterationNormalTwoComponentsUnsafeArray |   100000 |    273.32 us |   0.634 us |   0.529 us |    273.17 us |            311 |        
|    IterationUnsafeAddTwoComponentsUnsafeArray |   100000 |    248.47 us |   0.684 us |   0.534 us |    248.32 us |            609 |    
|        IterationNormalTwoComponentsUnsafeSpan |   100000 |    284.59 us |   5.645 us |  12.856 us |    277.44 us |            799 |     
|     IterationUnsafeAddTwoComponentsUnsafeSpan |   100000 |    249.78 us |   0.889 us |   0.788 us |    249.90 us |            452 |      
| IterationUnsafeAddTwoComponentsCompleteUnsafe |   100000 |    249.76 us |   1.407 us |   1.175 us |    249.68 us |            367 |     
|                  IterationNormalTwoComponents |  1000000 |  2,803.56 us |  12.563 us |  11.137 us |  2,798.01 us |         22,476 |    
|               IterationUnsafeAddTwoComponents |  1000000 |  2,560.23 us |   8.799 us |   8.230 us |  2,559.64 us |         24,377 |      
|              IterationNormalTwoComponentsSpan |  1000000 |  2,817.89 us |  10.937 us |  10.231 us |  2,813.88 us |         24,749 |     
|           IterationUnsafeAddTwoComponentsSpan |  1000000 |  2,568.29 us |  18.498 us |  17.303 us |  2,567.17 us |         23,454 |     
|       IterationNormalTwoComponentsUnsafeArray |  1000000 |  2,799.62 us |  14.872 us |  13.912 us |  2,802.79 us |         21,873 |  
|    IterationUnsafeAddTwoComponentsUnsafeArray |  1000000 |  2,550.32 us |  13.997 us |  13.093 us |  2,555.53 us |         22,839 |     
|        IterationNormalTwoComponentsUnsafeSpan |  1000000 |  2,819.96 us |  31.745 us |  28.141 us |  2,818.11 us |         22,059 |    
|     IterationUnsafeAddTwoComponentsUnsafeSpan |  1000000 |  2,551.21 us |   6.009 us |   5.018 us |  2,549.80 us |         20,806 |    
| IterationUnsafeAddTwoComponentsCompleteUnsafe |  1000000 |  2,555.63 us |  19.448 us |  18.191 us |  2,553.00 us |         22,238 | 
|                  IterationNormalTwoComponents | 10000000 | 32,851.15 us | 169.640 us | 158.682 us | 32,919.60 us |        263,697 |    
|               IterationUnsafeAddTwoComponents | 10000000 | 30,272.00 us | 126.307 us | 111.968 us | 30,294.22 us |        258,406 |     
|              IterationNormalTwoComponentsSpan | 10000000 | 32,899.92 us |  78.192 us |  65.294 us | 32,884.78 us |        266,462 |  
|           IterationUnsafeAddTwoComponentsSpan | 10000000 | 30,363.23 us | 114.287 us | 106.904 us | 30,324.14 us |        263,339 |    
|       IterationNormalTwoComponentsUnsafeArray | 10000000 | 32,703.78 us |  50.003 us |  41.755 us | 32,691.33 us |        251,597 |   
|    IterationUnsafeAddTwoComponentsUnsafeArray | 10000000 | 30,148.39 us | 115.400 us | 102.299 us | 30,125.48 us |        246,588 |     
|        IterationNormalTwoComponentsUnsafeSpan | 10000000 | 32,790.12 us |  95.293 us |  84.475 us | 32,804.19 us |        259,823 |    
|     IterationUnsafeAddTwoComponentsUnsafeSpan | 10000000 | 30,214.06 us | 151.878 us | 142.067 us | 30,254.44 us |        246,144 |   
| IterationUnsafeAddTwoComponentsCompleteUnsafe | 10000000 | 30,420.64 us | 307.392 us | 272.495 us | 30,429.62 us |        253,005 |     

### Legend

Legends
- amount    : Value of the 'amount' parameter  
- Mean      : Arithmetic mean of all measurements  
- Error     : Half of 99.9% confidence interval  
- StdDev    : Standard deviation of all measurements  
- Median    : Value separating the higher half of all measurements (50th percentile)  
- Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
- 1 μs      : 1 Microsecond (0.000001 sec)  
