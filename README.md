# Arch
A C# based Archetype [Entity Component System](https://www.wikiwand.com/en/Entity_component_system) (ECS).  

Each Archetype stores their entities within 16KB sized chunks perfectly fitting into L1 Caches for maximum iteration performance.  
This technique has two main advantages, first of all it provides an great entity allocation speed and second it lowers the cache misses to the best possible minimum. 
Its incredible fast, especially for well architectured component structures. 

Its a bare minimum ECS, following the guideline of "Performance as a feature".  
New features will be added regulary, feel free to contribute ! 

Supports .NetStandard 2.1, .Net Core 6 and 7.  
Since .NetStandard is supported, you may also use it with Unity. 

Download the [package](https://github.com/genaray/Arch/packages/1697222) and get started today ! 
```sh
dotnet add PROJECT package Arch --version 1.0.5
```

# Example
## Creating Entities

```csharp
var archetype = new []{ typeof(Transform), typeof(Rotation) };

var world = World.Create();
world.Reserve(archetype, 100000); // Optional, provides bulk adding of entities
for (var index = 0; index < 100; index++)
    world.Create(archetype);
```

## Querying Entities

> ! Queries perform faster the smaller your components are or the less components you query !

```csharp

// Define a description of which entities you want to query
var query = new QueryDescription {
    All = new []{ typeof(Transform) },
    Any = new []{ typeof(Rotation) },
    None = new []{ typeof(AI) }
};

// Execute the query
world.Query(in query, entity => { /* Do something */ });

// Execute the query and modify components in the same step, up to 10 generic components at the same time. 
world.Query(in query, (in Entity entity, ref Transform transform) => {
    transform.x++;
    transform.y++;
});
```

## Modifying Entities

```csharp
var entity = world.Create(archetype);
entity.Set(new Transform());
ref var transform = entity.Get<Transform(); 
```
> ! Structural changes are not added yet, but can be simulated by yourself by giving each removeable component an flag !

## Utility methods

```csharp

entity.IsAlive();      // Checks if the entity is alive in the current world
entity.Has<T>();       // Returns whether the entity has an component
entity.GetArchetype(); // Returns the archetype of an entity

// To provide flexibility and user support
var filteredEntities = new List<Entity>();
var filteredArchetypes = new List<Archetype>();
var filteredChunks = new List<Chunk>();

world.GetEntities(query, filteredEntities);
world.GetArchetypes(query, filteredArchetypes);
world.GetChunks(query, filteredChunks);
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

### Different Iteration & Acess Techniques

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
