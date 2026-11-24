"""Compares the BenchmarkDotNet json results of two runs and prints the difference as a markdown table.

Usage: compare-benchmarks.py <baseline directory> <current directory>

It is a report, not a gate: shared runners are too noisy to fail a build on, so the exit code is always 0.
A missing or empty baseline directory only prints the current results.
"""
import glob
import json
import os
import sys

THRESHOLD = 0.10  # Changes of at least 10 % are marked.
UNITS = [(1e9, "s"), (1e6, "ms"), (1e3, "us"), (1.0, "ns")]
FASTER = "🟢"
SLOWER = "🔴"


def load(directory):
    """Returns ({benchmark: (mean in ns, allocated bytes)}, processor name) of all reports in a directory."""
    benchmarks = {}
    processor = None
    for path in sorted(glob.glob(os.path.join(directory, "**", "*-report*.json"), recursive=True)):
        with open(path, encoding="utf-8-sig") as file:
            report = json.load(file)

        processor = processor or report["HostEnvironmentInfo"]["ProcessorName"]
        for benchmark in report["Benchmarks"]:
            statistics = benchmark.get("Statistics")
            if statistics is None:  # The benchmark failed.
                continue

            memory = benchmark.get("Memory") or {}
            name = benchmark["FullName"].replace("Arch.Benchmarks.", "")
            benchmarks[name] = (statistics["Mean"], memory.get("BytesAllocatedPerOperation", 0))

    return benchmarks, processor


def format_time(nanoseconds):
    for factor, unit in UNITS:
        if nanoseconds >= factor:
            return f"{nanoseconds / factor:,.2f} {unit}"
    return f"{nanoseconds:,.2f} ns"


def get_marker(change):
    if change <= -THRESHOLD:
        return FASTER
    return SLOWER if change >= THRESHOLD else ""


def print_header(baseline, baseline_processor, current_processor):
    if not baseline:
        print("No baseline from a previous run found, showing the current results only.\n")
    elif baseline_processor != current_processor:
        print(f"> **Careful:** the baseline ran on `{baseline_processor}`, this run on `{current_processor}`. "
              "Differences can come from the hardware.\n")
    else:
        print(f"Compared to the last successful run on the default branch, both on `{current_processor}`. "
              f"{FASTER} faster and {SLOWER} slower mark changes of {THRESHOLD:.0%} or more. "
              "Smaller ones are within the noise of a shared runner.\n")


def main(baseline_directory, current_directory):
    sys.stdout.reconfigure(encoding="utf-8")
    baseline, baseline_processor = load(baseline_directory)
    current, current_processor = load(current_directory)

    print("## Benchmarks\n")
    if not current:
        print("No benchmark results found.")
        return

    print_header(baseline, baseline_processor, current_processor)
    print("| Benchmark | Baseline | Current | Change | Allocated |")
    print("| --- | ---: | ---: | ---: | ---: |")

    markers = []
    for name in sorted(current):
        mean, allocated = current[name]
        if name not in baseline:
            print(f"| {name} | {'new' if baseline else '-'} | {format_time(mean)} | | {allocated:,} B |")
            continue

        baseline_mean, baseline_allocated = baseline[name]
        change = mean / baseline_mean - 1 if baseline_mean else 0.0
        marker = get_marker(change)
        markers.append(marker)

        change_text = f"{change:+.1%} {marker}".strip()
        allocation = f"{allocated:,} B" if allocated == baseline_allocated else f"{baseline_allocated:,} B → {allocated:,} B"
        print(f"| {name} | {format_time(baseline_mean)} | {format_time(mean)} | {change_text} | {allocation} |")

    if markers:
        print(f"\n{markers.count(FASTER)} faster, {markers.count(SLOWER)} slower, {markers.count('')} unchanged.")

    removed = sorted(baseline.keys() - current.keys())
    if removed:
        print("\nNo longer measured: " + ", ".join(f"`{name}`" for name in removed))


if __name__ == "__main__":
    if len(sys.argv) != 3:
        sys.exit(__doc__)
    main(sys.argv[1], sys.argv[2])
