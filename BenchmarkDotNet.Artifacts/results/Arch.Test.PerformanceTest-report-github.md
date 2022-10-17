``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22494
Unknown processor
.NET SDK=5.0.400
  [Host]     : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT
  Job-LXLUGO : .NET 5.0.9 (5.0.921.35908), X64 RyuJIT

InvocationCount=1  UnrollFactor=1  

```
|                    Method |   amount |          Mean |       Error |        StdDev |        Median |
|-------------------------- |--------- |--------------:|------------:|--------------:|--------------:|
|           **IterationNormal** |    **10000** |     **12.467 μs** |   **0.1918 μs** |     **0.1497 μs** |     **12.500 μs** |
|        IterationUnsafeAdd |    10000 |      9.077 μs |   0.1633 μs |     0.1363 μs |      9.100 μs |
|    IterationUnsafeAddIncr |    10000 |            NA |          NA |            NA |            NA |
| IterationManualRangeCheck |    10000 |     10.740 μs |   0.1388 μs |     0.1298 μs |     10.700 μs |
|    IterationMultipleLoops |    10000 |      9.733 μs |   0.0834 μs |     0.0651 μs |      9.700 μs |
|           **IterationNormal** |   **100000** |    **123.808 μs** |   **2.0037 μs** |     **3.5616 μs** |    **122.800 μs** |
|        IterationUnsafeAdd |   100000 |     96.693 μs |   2.7476 μs |     7.4282 μs |     93.900 μs |
|    IterationUnsafeAddIncr |   100000 |            NA |          NA |            NA |            NA |
| IterationManualRangeCheck |   100000 |    108.805 μs |   2.1474 μs |     2.4729 μs |    108.300 μs |
|    IterationMultipleLoops |   100000 |    105.309 μs |   2.9815 μs |     8.0607 μs |    102.100 μs |
|           **IterationNormal** |  **1000000** |  **2,233.521 μs** |  **51.5263 μs** |   **147.0073 μs** |  **2,202.400 μs** |
|        IterationUnsafeAdd |  1000000 |  2,205.079 μs |  54.0627 μs |   157.7034 μs |  2,167.200 μs |
|    IterationUnsafeAddIncr |  1000000 |            NA |          NA |            NA |            NA |
| IterationManualRangeCheck |  1000000 |  2,234.618 μs |  52.8949 μs |   153.4576 μs |  2,204.400 μs |
|    IterationMultipleLoops |  1000000 |  2,511.602 μs |  76.3546 μs |   225.1333 μs |  2,442.600 μs |
|           **IterationNormal** | **10000000** | **24,681.573 μs** | **496.6296 μs** | **1,416.9122 μs** | **25,229.050 μs** |
|        IterationUnsafeAdd | 10000000 | 23,964.268 μs | 475.4773 μs | 1,244.2413 μs | 24,482.550 μs |
|    IterationUnsafeAddIncr | 10000000 | 23,288.387 μs | 463.3187 μs | 1,187.6631 μs | 23,864.900 μs |
| IterationManualRangeCheck | 10000000 | 24,253.003 μs | 483.6848 μs | 1,315.9001 μs | 24,747.900 μs |
|    IterationMultipleLoops | 10000000 | 26,710.366 μs | 648.9464 μs | 1,903.2474 μs | 27,679.500 μs |

Benchmarks with issues:
  PerformanceTest.IterationUnsafeAddIncr: Job-LXLUGO(InvocationCount=1, UnrollFactor=1) [amount=10000]
  PerformanceTest.IterationUnsafeAddIncr: Job-LXLUGO(InvocationCount=1, UnrollFactor=1) [amount=100000]
  PerformanceTest.IterationUnsafeAddIncr: Job-LXLUGO(InvocationCount=1, UnrollFactor=1) [amount=1000000]
