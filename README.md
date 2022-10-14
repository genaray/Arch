# Arch
A C# &amp; .NET 6.0 based Archetype Entity Component System ( ECS ).

# Benchmark
The current Benchmark only tests it Archetype iteration performance.

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22622
AMD Ryzen 5 3600X, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.202
  [Host]     : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT
  DefaultJob : .NET 6.0.4 (6.0.422.16404), X64 RyuJIT


|                     Method |  amount |         Mean |      Error |     StdDev |       Median | Allocated |
|--------------------------- |-------- |-------------:|-----------:|-----------:|-------------:|----------:|
|            IterationNormal |   10000 |     9.400 μs |  0.0120 μs |  0.0100 μs |     9.396 μs |         - |
|         IterationUnsafeAdd |   10000 |     9.016 μs |  0.1538 μs |  0.1831 μs |     8.919 μs |         - |
| IterationFixedPtrIteration |   10000 |     8.946 μs |  0.0631 μs |  0.0493 μs |     8.931 μs |         - |
|  IterationManualRangeCheck |   10000 |     8.910 μs |  0.0865 μs |  0.0676 μs |     8.902 μs |         - |
|     IterationMultipleLoops |   10000 |    11.065 μs |  0.0360 μs |  0.0319 μs |    11.051 μs |         - |
|            IterationNormal |  100000 |    99.432 μs |  1.5136 μs |  1.5544 μs |    98.825 μs |         - |
|         IterationUnsafeAdd |  100000 |    94.850 μs |  1.8829 μs |  4.4013 μs |    91.300 μs |         - |
| IterationFixedPtrIteration |  100000 |    92.077 μs |  1.6391 μs |  1.4530 μs |    91.408 μs |         - |
|  IterationManualRangeCheck |  100000 |    92.334 μs |  0.0598 μs |  0.0499 μs |    92.324 μs |         - |
|     IterationMultipleLoops |  100000 |   112.009 μs |  0.1135 μs |  0.0886 μs |   111.984 μs |         - |
|            IterationNormal | 1000000 | 3,077.169 μs | 14.2575 μs | 12.6389 μs | 3,076.809 μs |      10 B |
|         IterationUnsafeAdd | 1000000 | 2,984.150 μs | 14.0571 μs | 13.1490 μs | 2,987.090 μs |      10 B |
| IterationFixedPtrIteration | 1000000 | 3,039.060 μs | 34.9704 μs | 31.0003 μs | 3,038.806 μs |      10 B |
|  IterationManualRangeCheck | 1000000 | 3,014.951 μs |  8.9813 μs |  7.0120 μs | 3,013.975 μs |      10 B |
|     IterationMultipleLoops | 1000000 | 3,557.774 μs | 27.5206 μs | 22.9810 μs | 3,547.122 μs |      10 B |

// * Legends *
  amount    : Value of the 'amount' parameter
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Median    : Value separating the higher half of all measurements (50th percentile)
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 μs      : 1 Microsecond (0.000001 sec)

// * Diagnostic Output - MemoryDiagnoser *
