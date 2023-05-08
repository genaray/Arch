namespace Arch.Core.Extensions.Internal;

/// <summary>
///     The <see cref="JobExtensions"/> class
///     contains some job related methods.
/// </summary>
internal static class JobExtensions
{
    /// <summary>
    ///     Calculates how an array should be partionated based on the threadcount.
    ///     For multithreading.
    /// </summary>
    /// <param name="threadCount">The thread count.</param>
    /// <param name="arraySize">The array size.</param>
    /// <param name="requiredThreads">The amount of required threads.</param>
    /// <param name="perThread">How many items per thread should be processed.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void PartionateArray(int threadCount, int arraySize, out int requiredThreads, out int perThread)
    {
        requiredThreads = Math.Min(arraySize, threadCount);
        perThread = (int)Math.Floor(arraySize / (float)requiredThreads);
    }
}
