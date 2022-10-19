# Arch
A C# &amp; .NET 6.0 based Archetype Entity Component System ( ECS ).  
Each Archetype stores their entities within 16KB sized chunks perfectly fitting into L1 Caches for maximum iteration performance. 

Since its still work in progress it is not yet finished and there still a lot of features missing. 

# Example
## Creating Entities

```csharp
var archetype = new []{ typeof(Transform), typeof(Rotation) };

var world = World.Create();
for (var index = 0; index < 100; index++)
    world.Create(archetype);
```

## Querying Entities

```csharp

// Define a description of which entities you want to query
var query = new QueryDescription {
    All = new []{ typeof(Transform) },
    Any = new []{ typeof(Rotation) },
    None = new []{ typeof(AI) }
};

// Execute the query
world.Query(query, entity => { /* Do something */ });

// Execute the query and modify components in the same step, up to 10 generic components at the same time. 
world.Query(query, (in Entity entity, ref Transform transform) => {
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

# Benchmark
The current Benchmark only tests it Archetype/Chunk iteration performance.
Two different iteration techniques, 2 Components ( Transform & Rotation ) modification and Entity + 2 Components modification.  

## NET.6

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22622 <br>
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores <br>
.NET SDK=6.0.202 <br>
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT <br>
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT <br>

|                       Method |   amount |         Mean |      Error |     StdDev | CacheMisses/Op |
|----------------------------- |--------- |-------------:|-----------:|-----------:|---------------:|
|              IterationNormal |    10000 |     30.62 us |   0.519 us |   0.656 us |             49 |
|           IterationUnsafeAdd |    10000 |     30.15 us |   0.112 us |   0.105 us |             28 |
|    IterationNormalWithEntity |    10000 |     42.61 us |   0.144 us |   0.135 us |             49 | 
| IterationUnsafeAddWithEntity |    10000 |     44.92 us |   0.200 us |   0.177 us |             28 |
|              IterationNormal |   100000 |    298.32 us |   2.185 us |   2.044 us |            641 |
|           IterationUnsafeAdd |   100000 |    297.39 us |   1.587 us |   1.484 us |            611 |    
|    IterationNormalWithEntity |   100000 |    421.69 us |   2.129 us |   1.991 us |          1,041 |     
| IterationUnsafeAddWithEntity |   100000 |    442.95 us |   1.749 us |   1.551 us |            946 |    
|              IterationNormal |  1000000 |  3,044.35 us |  38.524 us |  34.151 us |         25,502 |  
|           IterationUnsafeAdd |  1000000 |  3,017.99 us |  13.019 us |  12.178 us |         24,087 |    
|    IterationNormalWithEntity |  1000000 |  4,246.82 us |  22.290 us |  20.850 us |         26,561 |    
| IterationUnsafeAddWithEntity |  1000000 |  4,490.07 us |  23.775 us |  22.239 us |         28,689 |    
|              IterationNormal | 10000000 | 39,867.92 us | 221.959 us | 207.621 us |        275,041 |    
|           IterationUnsafeAdd | 10000000 | 35,501.86 us | 177.274 us | 148.032 us |        251,094 |   
|    IterationNormalWithEntity | 10000000 | 47,379.65 us | 223.633 us | 198.244 us |        305,686 |   
| IterationUnsafeAddWithEntity | 10000000 | 47,517.72 us | 444.653 us | 394.173 us |        321,151 |  

## NET.7

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

Legends
- amount    : Value of the 'amount' parameter  
- Mean      : Arithmetic mean of all measurements  
- Error     : Half of 99.9% confidence interval  
- StdDev    : Standard deviation of all measurements  
- Median    : Value separating the higher half of all measurements (50th percentile)  
- Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)  
- 1 μs      : 1 Microsecond (0.000001 sec)  
