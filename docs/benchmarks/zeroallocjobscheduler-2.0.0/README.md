# ZeroAllocJobScheduler 2.0.0 in Arch

Measured on 2026-11-26 against the unchanged performance branch at
[`584b8cce561fe67b898968d8183325a6cfb990af`](https://github.com/genaray/Arch/commit/584b8cce561fe67b898968d8183325a6cfb990af)
([PR #325](https://github.com/genaray/Arch/pull/325)). The baseline uses NuGet `ZeroAllocJobScheduler` 1.1.2;
the integration uses the published NuGet package **2.0.0**.

## Method

- Debian 13, AMD EPYC 9V74, 9 logical processors exposed to the VM.
- .NET SDK 8.0.426, .NET runtime 8.0.32, Release, BenchmarkDotNet 0.15.2.
- Default scheduler configuration in both variants: one worker per available processor.
- Run order: baseline, 2.0.0, baseline, 2.0.0 for the original twelve cases.
  The two 10,000-entity cases were subsequently run in the same alternating order, with identical settings.
  No simultaneous tests or builds ran during measurements.
- Each run: one launch, three warmups, eight requested measurement iterations, 200 ms target iteration time.
  BenchmarkDotNet's default outlier handling applies.
- Each displayed time is the arithmetic mean of the two BenchmarkDotNet run means, with equal weight.
  Change is `(new / baseline - 1) * 100`; negative means less time. No fastest-run selection.
- Allocations are managed bytes per operation after warmup. The table uses the maximum reported in either run.
- `ParallelQueryBenchmark` is identical apart from scheduler construction. The new `SchedulerWorkloadBenchmark`
  was copied into the baseline checkout, with only its scheduler construction changed to the 1.1.2 API.
- This is a shared virtual machine. Values are local measurements, not expected speedups on every machine.
  `*` marks a row where the two runs of either variant differ by more than 15%.

## Findings

The largest repeatable improvement is the 10,000-entity uniform query (29.3% less time), followed by
`LargeParallel` at one million entities (22.2% less time). The 100,000-entity uneven query uses 7.6% less time.
`ManyArchetypesParallel` uses 8.0% more time in this environment. Several other cases change by only
about 0–2%, while the one-million-entity uniform workload varies substantially between runs.
Both schedulers remain allocation-free in all measured synchronous cases after warmup.
The upgrade therefore improves some workloads substantially; it is not a uniform speedup over the performance branch.

## Results

| Benchmark | Entities | 1.1.2 mean (µs) | 2.0.0 mean (µs) | Time change | Alloc 1.1.2 | Alloc 2.0.0 |
|---|---:|---:|---:|---:|---:|---:|
| `LargeSerial` | 1,000,000 | 9,632.91 | 9,510.16 | -1.3% | 0 B | 0 B |
| `LargeParallel` | 1,000,000 | 2,195.56 | 1,707.94 | -22.2% | 0 B | 0 B |
| `SmallSerial` | 2,000 | 18.31 | 18.51 | +1.1% | 0 B | 0 B |
| `SmallParallel` | 2,000 | 18.65 | 18.46 | -1.0% | 0 B | 0 B |
| `UnusedChunksSerial` | 100,000 | 933.45 | 945.59 | +1.3% | 0 B | 0 B |
| `UnusedChunksParallel` | 100,000 | 189.68 | 192.12 | +1.3% | 0 B | 0 B |
| `ManyArchetypesSerial` | 100,800 | 958.45 | 954.92 | -0.4% | 0 B | 0 B |
| `ManyArchetypesParallel` | 100,800 | 183.96 | 198.76 | +8.0% | 0 B | 0 B |
| `Uniform` | 100000 | 186.53 | 183.48 | -1.6% | 0 B | 0 B |
| `Uneven` | 100000 | 999.62 | 923.21 | -7.6% | 0 B | 0 B |
| `Uniform` * | 1000000 | 2,002.58 | 2,016.66 | +0.7% | 0 B | 0 B |
| `Uneven` | 1000000 | 9,895.20 | 9,843.85 | -0.5% | 0 B | 0 B |
| `Uniform` | 10000 | 56.97 | 40.28 | -29.3% | 0 B | 0 B |
| `Uneven` | 10000 | 286.13 | 263.03 | -8.1% | 0 B | 0 B |

`SmallParallel` stays on the calling thread because it has fewer than 4,096 entities.
Its result and the serial methods are controls, rather than measurements of the scheduler.
`UnusedChunks*` reserves capacity for 1,000,000 entities while using only 100,000.
`ManyArchetypes*` uses 63 archetypes with 1,600 entities each.
`Uniform` performs 20 arithmetic iterations per entity; `Uneven` gives a contiguous 5% of entities
400 iterations and the remaining 95% 20 iterations. Both use the performance branch's existing chunk dispatcher.

Individual run means, exact nanosecond values and allocation maxima are in [summary.csv](summary.csv).
The per-run BenchmarkDotNet CSV reports are in the four run subdirectories.
[measurements.json](measurements.json) preserves every actual measurement's operation count and elapsed
nanoseconds, plus each BenchmarkDotNet mean, standard deviation and managed allocation result.
Raw elapsed times include measurement overhead; the reported means use BenchmarkDotNet's processing.

## Integration

- Both package references now require `ZeroAllocJobScheduler` 2.0.0.
- Scheduler construction in tests, benchmarks and the sample uses the new constructor.
- A parent job represents query completion. All helper jobs are attached before anything is flushed;
  waiting on the parent therefore includes every helper.
- Synchronous queries wait with `scheduler.Wait(handle)` and return pooled jobs and dispatchers even after an exception.
- Scheduled chunk queries still return a `JobHandle`; callers must now use `scheduler.Wait(handle)`.
- Scheduler 2.0.0 does not expose its worker count. Arch bounds helper jobs with `Environment.ProcessorCount`;
  a scheduler constructed with fewer workers executes those jobs on its configured workers. Tests cover one and four workers.
- The existing dispatcher, its 4,096-entity helper threshold and its batching rules are unchanged.
  Async queries retain their existing allocation behavior; they are not part of the allocation-free synchronous measurements.

## Validation

Arch builds for `net8.0`, `net6.0` and `netstandard2.1`. The sample and benchmark projects build.
The existing build warnings remain. All tests pass in each CI configuration:

| Configuration | Passed | Failed |
|---|---:|---:|
| Debug | 228 | 0 |
| Debug-Events | 246 | 0 |
| Debug-PureECS | 214 | 0 |
| Release | 228 | 0 |
| Release-Events | 246 | 0 |
| Release-PureECS | 214 | 0 |

Additional regression coverage checks scheduled completion, empty queries, repeated helper/job reuse,
and reuse after a throwing query, with both one and four workers.

## Reproduce

Build each checkout using the same .NET 8 SDK:

```sh
dotnet build src/Arch.Benchmarks/Arch.Benchmarks.csproj -c Release -m:1
```

The final benchmark source includes all fourteen cases. In this measurement, the 10,000-entity cases
were added afterward and selected separately with `--filter '*SchedulerWorkloadBenchmark*(Amount: 10000)'`.

For the baseline, check out commit `584b8cce561fe67b898968d8183325a6cfb990af`, copy
`src/Arch.Benchmarks/SchedulerWorkloadBenchmark.cs` from this branch and replace its scheduler construction with:

```csharp
_scheduler = new JobScheduler(new JobScheduler.Config
{
    ThreadCount = 0,
    MaxExpectedConcurrentJobs = 64,
    StrictAllocationMode = false
});
```

In each checkout run the following command, alternating baseline and integration twice and selecting a
separate artifact directory each time:

```sh
dotnet src/Arch.Benchmarks/bin/Release/net8.0/Arch.Benchmarks.dll \
  --filter '*ParallelQueryBenchmark*' '*SchedulerWorkloadBenchmark*' \
  --job short --launchCount 1 --iterationCount 8 --warmupCount 3 \
  --iterationTime 200 --exporters json csv markdown --artifacts /absolute/path/to/run
```
