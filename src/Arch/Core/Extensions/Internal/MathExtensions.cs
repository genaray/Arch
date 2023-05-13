namespace Arch.Core.Extensions.Internal;

/// <summary>
///     The <see cref="MathExtensions"/>
///     contains several methods for math operations.
/// </summary>
internal static class MathExtensions
{
    /// <summary>
    /// This method will round down to the nearest power of 2 number. If the supplied number is a power of 2 it will return it.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int RoundToPowerOfTwo(int num)
    {
        // If num is a power of 2, return it
        if (num > 0 && (num & (num - 1)) == 0)
        {
            return num;
        }

        // Find the exponent of the nearest power of 2 (rounded down)
        int exponent = (int)Math.Floor(Math.Log(num) / Math.Log(2));

        // Calculate the nearest power of 2
        int result = (int)Math.Pow(2, exponent);

        return result;
    }
}
