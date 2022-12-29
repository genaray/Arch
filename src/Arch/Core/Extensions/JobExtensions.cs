namespace Arch.Core.Extensions;

// TODO: Documentation.
/// <summary>
///     The <see cref="JobExtensions"/> class
///     ...
/// </summary>
public class JobExtensions
{
    // TODO: Documentation.
    /// <summary>
    /// 
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
