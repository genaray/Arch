using System;
using System.Runtime.CompilerServices;

namespace Arch.Core.Extensions;

public class JobExtensions
{
    /// <summary>
    ///     Partionates an array and returns how many threads with how many elements each is required for processing it.
    /// </summary>
    /// <param name="threadCount"></param>
    /// <param name="arraySize"></param>
    /// <param name="requiredThreads"></param>
    /// <param name="perThread"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void PartionateArray(int threadCount, int arraySize, out int requiredThreads, out int perThread)
    {
        requiredThreads = Math.Min(arraySize, threadCount);
        perThread = (int)Math.Floor(arraySize / (float)requiredThreads);
    }
}