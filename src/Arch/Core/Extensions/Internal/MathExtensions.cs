namespace Arch.Core.Extensions.Internal;

/// <summary>
///     The <see cref="MathExtensions"/>
///     contains several methods for math operations.
/// </summary>
internal static class MathExtensions
{
    /// <summary>
    ///     Returns the max of two ints by bit operation without branching.
    /// </summary>
    /// <param name="a">The first int.</param>
    /// <param name="b">The second int.</param>
    /// <returns>The highest of both ints.</returns>

    public static int Max(int a, int b)
    {
        return a - ((a - b) & ((a - b) >> 31));
    }
}
