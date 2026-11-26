# Performance: Arch 2.2.0 vs. this version

## Setup

- BenchmarkDotNet 0.15.2, net8.0, `--job short --iterationCount 10 --warmupCount 4`.
- WSL, 22 cores.
- Both variants ran the same benchmark sources. 2.2.0 is the NuGet package, the other is a project reference.
- Each variant ran twice, alternating (2.2.0, current, 2.2.0, current). The values are the faster of the two passes.
- 🟢 is 10% or more faster, 🔴 is 10% or more slower, ⚠️ means the two passes differed by more than 15%.
- "Alloc" is managed bytes per operation.

## Summary

| Area | Change |
|---|---|
| CommandBuffer (record + playback) | about 80–93% faster, allocation-free (was 1–56 MB per operation) |
| Multi-component and untyped add/remove | about 70–80% faster, allocation-free |
| Bulk create | about 21–28% faster |
| `Remove<T>` | about 18–22% faster |
| Add + remove pair | about 14–16% faster |
| Parallel queries | up to 88% faster on many small archetypes, 64–67% on uneven work, 34% with unused chunks, 72% on small worlds (now equal to serial), 7–15% on a large archetype or uniform work |
| Boxed `TryGet(entity, ComponentType)` | about 74% faster |
| Plain queries and iteration | unchanged within noise |

## Results

#### CommandBufferBenchmark

`AddAndRemoveDirectly` is not a CommandBuffer. It changes the world directly and shows what the library gains without any buffer.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `AddAndRemoveDirectly(Amount: 10000)` | 2.48 ms | 968.76 µs | 🟢 -61% | 3.5 MB | 0 |
| `AddAndRemoveDirectly(Amount: 100000)` | 24.51 ms | 10.27 ms | 🟢 -58% | 35.2 MB | 0 |
| `RecordAndPlaybackAddAndRemove(Amount: 10000)` | 10.35 ms | 2.00 ms | 🟢 -81% | 6.0 MB | 0 |
| `RecordAndPlaybackAddAndRemove(Amount: 100000)` | 105.44 ms | 21.35 ms | 🟢 -80% | 56.0 MB | 386 B |
| `RecordAndPlaybackCreate(Amount: 10000)` | 1.91 ms | 241.01 µs | 🟢 -87% | 1.0 MB | 0 |
| `RecordAndPlaybackCreate(Amount: 100000)` | 18.71 ms | 2.56 ms | 🟢 -86% | 8.4 MB | 0 |
| `RecordAndPlaybackSet(Amount: 10000)` | 2.17 ms | 146.43 µs | 🟢 -93% | 1.0 MB | 0 |
| `RecordAndPlaybackSet(Amount: 100000)` | 20.80 ms | 1.53 ms | 🟢 -93% | 8.4 MB | 0 |

#### UntypedStructuralChangeBenchmark

100 extra components are registered first, which is what exposes the slow lookup path in 2.2.0. `AddAndRemoveGeneric` is the generic `Add<Velocity,Health>`.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `AddAndRemoveGeneric(Amount: 10000)` | 3.35 ms | 807.95 µs | 🟢 -76% | 6.1 MB | 0 |
| `AddAndRemoveRange(Amount: 10000)` | 4.07 ms | 871.92 µs | 🟢 -79% | 6.9 MB | 0 |
| `AddRemoveSingleType(Amount: 10000)` | 2.06 ms | 640.77 µs | 🟢 -69% | 2.9 MB | 0 |

#### AddRemoveBenchmark

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `Add(Amount: 10000)` | 512.86 µs | 488.91 µs | -5% | 0 | 0 |
| `Add(Amount: 100000)` | 5.14 ms | 4.81 ms | -6% | 0 | 0 |
| `Add(Amount: 1000000)` | 53.23 ms | 49.78 ms | -6% | 0 | 0 |
| `Remove(Amount: 10000)` | 333.73 µs | 260.73 µs | 🟢 -22% | 0 | 0 |
| `Remove(Amount: 100000)` | 3.30 ms | 2.69 ms | 🟢 -18% | 0 | 0 |
| `Remove(Amount: 1000000)` | 34.24 ms | 27.61 ms | 🟢 -19% ⚠️ | 0 | 0 |

#### StructuralChangeBenchmark

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `AddAndRemove(Amount: 10000)` | 968.49 µs | 815.90 µs | 🟢 -16% | 0 | 0 |
| `AddAndRemove(Amount: 100000)` | 9.84 ms | 8.35 ms | 🟢 -15% | 0 | 0 |
| `AddAndRemove(Amount: 1000000)` | 100.53 ms | 86.06 ms | 🟢 -14% | 0 | 0 |
| `CreateAndDestroy(Amount: 10000)` | 512.42 µs | 484.82 µs | -5% | 0 | 0 |
| `CreateAndDestroy(Amount: 100000)` | 5.03 ms | 4.85 ms | -4% | 0 | 0 |
| `CreateAndDestroy(Amount: 1000000)` | 52.41 ms | 51.16 ms | -2% ⚠️ | 0 | 0 |
| `CreateAndDestroyInRandomOrder(Amount: 10000)` | 581.13 µs | 592.68 µs | +2% | 0 | 0 |
| `CreateAndDestroyInRandomOrder(Amount: 100000)` | 9.43 ms | 9.36 ms | -1% | 0 | 0 |
| `CreateAndDestroyInRandomOrder(Amount: 1000000)` | 423.99 ms | 456.31 ms | +8% ⚠️ | 0 | 0 |
| `DestroyAndCreateChurn(Amount: 10000)` | 495.12 µs | 449.95 µs | -9% | 0 | 0 |
| `DestroyAndCreateChurn(Amount: 100000)` | 4.98 ms | 4.76 ms | -4% | 0 | 0 |
| `DestroyAndCreateChurn(Amount: 1000000)` | 51.61 ms | 46.54 ms | -10% | 0 | 0 |

#### BulkCreateBenchmark

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `CreateAmountOfComponents(Amount: 10000)` | 165.43 µs | 128.00 µs | 🟢 -23% | 0 | 0 |
| `CreateAmountOfComponents(Amount: 100000)` | 1.80 ms | 1.29 ms | 🟢 -28% | 0 | 0 |
| `CreateAmountOfComponents(Amount: 1000000)` | 21.19 ms | 15.56 ms | 🟢 -27% | 0 | 0 |
| `CreateAmountOfSignature(Amount: 10000)` | 158.46 µs | 125.81 µs | 🟢 -21% | 0 | 0 |
| `CreateAmountOfSignature(Amount: 100000)` | 1.69 ms | 1.34 ms | 🟢 -21% | 0 | 0 |
| `CreateAmountOfSignature(Amount: 1000000)` | 20.16 ms | 14.75 ms | 🟢 -27% | 0 | 0 |
| `CreateOneByOne(Amount: 10000)` | 288.63 µs | 290.13 µs | +1% | 0 | 0 |
| `CreateOneByOne(Amount: 100000)` | 3.03 ms | 2.99 ms | -1% | 0 | 0 |
| `CreateOneByOne(Amount: 1000000)` | 33.45 ms | 32.89 ms | -2% | 0 | 0 |

#### ParallelQueryBenchmark

`Small*` is a world of 2,000 entities, which is too small to be worth the threads. The query now runs on the calling thread alone and costs the same as the serial one.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `LargeParallel` | 1.71 ms | 1.46 ms | 🟢 -15% ⚠️ | 0 | 0 |
| `LargeSerial` | 9.00 ms | 8.97 ms | 0% | 0 | 0 |
| `ManyArchetypesParallel` | 3.35 ms | 399.31 µs | 🟢 -88% ⚠️ | 0 | 0 |
| `ManyArchetypesSerial` | 806.15 µs | 809.41 µs | 0% | 0 | 0 |
| `SmallParallel` | 56.60 µs | 16.09 µs | 🟢 -72% | 0 | 0 |
| `SmallSerial` | 16.01 µs | 16.13 µs | +1% | 0 | 0 |
| `UnusedChunksParallel` | 633.76 µs | 421.10 µs | 🟢 -34% | 0 | 0 |
| `UnusedChunksSerial` | 814.01 µs | 801.81 µs | -1% | 0 | 0 |

#### Uniform and uneven work (parallel queries)

The uneven cases give a few entities 20× more work than the rest. Chunks are handed out dynamically, so a slow part of the world no longer leaves the other threads idle.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `Uniform(N: 100000)` | 414.95 µs | 383.88 µs | -7% | 0 | 0 |
| `Uniform(N: 1000000)` | 1.74 ms | 1.55 ms | 🟢 -11% | 0 | 0 |
| `Uneven(N: 100000)` | 950.59 µs | 310.06 µs | 🟢 -67% ⚠️ | 0 | 0 |
| `Uneven(N: 1000000)` | 4.32 ms | 1.57 ms | 🟢 -64% | 0 | 0 |

#### TryGetBenchmark

`TryGetSuccess` is the boxed, `ComponentType`-based overload. It still allocates 24 B per call because of the boxing.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `TryGetFail(Amount: 10000)` | 32.73 µs | 33.95 µs | +4% | 0 | 0 |
| `TryGetFail(Amount: 100000)` | 336.74 µs | 330.68 µs | -2% | 0 | 0 |
| `TryGetFail(Amount: 1000000)` | 4.39 ms | 4.36 ms | -1% | 0 | 0 |
| `TryGetGenericFail(Amount: 10000)` | 33.52 µs | 30.11 µs | 🟢 -10% | 0 | 0 |
| `TryGetGenericFail(Amount: 100000)` | 341.32 µs | 295.81 µs | 🟢 -13% | 0 | 0 |
| `TryGetGenericFail(Amount: 1000000)` | 4.26 ms | 4.22 ms | -1% | 0 | 0 |
| `TryGetGenericRefFail(Amount: 10000)` | 30.96 µs | 26.14 µs | 🟢 -16% | 0 | 0 |
| `TryGetGenericRefFail(Amount: 100000)` | 310.56 µs | 275.44 µs | 🟢 -11% | 0 | 0 |
| `TryGetGenericRefFail(Amount: 1000000)` | 4.18 ms | 4.21 ms | +1% | 0 | 0 |
| `TryGetGenericRefSuccess(Amount: 10000)` | 54.20 µs | 53.44 µs | -1% | 0 | 0 |
| `TryGetGenericRefSuccess(Amount: 100000)` | 556.91 µs | 539.16 µs | -3% | 0 | 0 |
| `TryGetGenericRefSuccess(Amount: 1000000)` | 6.02 ms | 6.08 ms | +1% | 0 | 0 |
| `TryGetGenericSuccess(Amount: 10000)` | 54.92 µs | 54.18 µs | -1% | 0 | 0 |
| `TryGetGenericSuccess(Amount: 100000)` | 550.79 µs | 555.49 µs | +1% | 0 | 0 |
| `TryGetGenericSuccess(Amount: 1000000)` | 6.15 ms | 6.25 ms | +2% | 0 | 0 |
| `TryGetSuccess(Amount: 10000)` | 420.74 µs | 106.97 µs | 🟢 -75% | 234.4 KB | 234.4 KB |
| `TryGetSuccess(Amount: 100000)` | 4.20 ms | 1.09 ms | 🟢 -74% | 2.3 MB | 2.3 MB |
| `TryGetSuccess(Amount: 1000000)` | 42.43 ms | 11.39 ms | 🟢 -73% | 22.9 MB | 22.9 MB |

#### QueryBenchmark

Iteration and queries were not changed, so these tables check that nothing regressed. The two 🔴 rows are at 10k entities, where the passes varied by ×1.05–×1.08. They are not significant, and the same benchmarks at 100k and 1M show no change.

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `EntityQuery(Amount: 10000)` | 10.78 µs | 11.87 µs | 🔴 +10% | 0 | 0 |
| `EntityQuery(Amount: 100000)` | 116.86 µs | 123.63 µs | +6% | 0 | 0 |
| `EntityQuery(Amount: 1000000)` | 2.00 ms | 2.03 ms | +1% | 0 | 0 |
| `Iterator(Amount: 10000)` | 6.86 µs | 7.51 µs | +10% | 0 | 0 |
| `Iterator(Amount: 100000)` | 73.85 µs | 72.95 µs | -1% | 0 | 0 |
| `Iterator(Amount: 1000000)` | 1.36 ms | 1.30 ms | -5% ⚠️ | 0 | 0 |
| `Query(Amount: 10000)` | 8.33 µs | 8.04 µs | -3% | 0 | 0 |
| `Query(Amount: 100000)` | 82.82 µs | 83.30 µs | +1% | 0 | 0 |
| `Query(Amount: 1000000)` | 1.52 ms | 1.39 ms | -8% | 0 | 0 |
| `StructEntityQuery(Amount: 10000)` | 8.23 µs | 8.24 µs | +0% | 0 | 0 |
| `StructEntityQuery(Amount: 100000)` | 82.46 µs | 83.11 µs | +1% | 0 | 0 |
| `StructEntityQuery(Amount: 1000000)` | 1.86 ms | 1.75 ms | -6% | 0 | 0 |
| `StructQuery(Amount: 10000)` | 7.47 µs | 7.38 µs | -1% | 0 | 0 |
| `StructQuery(Amount: 100000)` | 74.00 µs | 72.71 µs | -2% | 0 | 0 |
| `StructQuery(Amount: 1000000)` | 1.38 ms | 1.31 ms | -5% ⚠️ | 0 | 0 |
| `WorldEntityQuery(Amount: 10000)` | 73.52 µs | 73.07 µs | -1% | 0 | 0 |
| `WorldEntityQuery(Amount: 100000)` | 728.96 µs | 720.64 µs | -1% | 0 | 0 |
| `WorldEntityQuery(Amount: 1000000)` | 7.43 ms | 7.88 ms | +6% | 0 | 0 |

#### IterationBenchmark

| Benchmark | 2.2.0 | This PR | Change | Alloc 2.2.0 | Alloc PR |
|---|---:|---:|---:|---:|---:|
| `Delegate(N: 1000)` | 835.00 ns | 822.82 ns | -1% | 0 | 0 |
| `Delegate(N: 100000)` | 81.40 µs | 83.19 µs | +2% | 0 | 0 |
| `HandWritten_PerChunk(N: 1000)` | 821.16 ns | 838.86 ns | +2% | 0 | 0 |
| `HandWritten_PerChunk(N: 100000)` | 83.26 µs | 82.58 µs | -1% | 0 | 0 |
| `Inline(N: 1000)` | 731.71 ns | 725.50 ns | -1% | 0 | 0 |
| `Inline(N: 100000)` | 72.80 µs | 73.85 µs | +1% | 0 | 0 |
| `Tiny_ByDesc(N: 1000)` | 49.96 ns | 50.86 ns | +2% | 0 | 0 |
| `Tiny_ByDesc(N: 100000)` | 48.88 ns | 49.84 ns | +2% | 0 | 0 |

## Notes

- The empty `QueryBenchmark.EntityExtensionQuery` is left out.
- `CommandBufferBenchmark.AddAndRemoveDirectly` and the `Generic` row use the multi-component `Add<T0,T1>`/`Remove<T0,T1>`, which allocated a `Signature` per call in 2.2.0.
- Parallel benchmarks are the noisiest on a shared machine, so treat their exact percentages with care.
- The CommandBuffer and parallel query tables come from a second, later run than the other tables.

