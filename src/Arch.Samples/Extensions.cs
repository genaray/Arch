using Microsoft.Xna.Framework;

namespace Arch.Samples;

// TODO: Documentation.
/// <summary>
///     The <see cref="RandomExtensions"/> class
///     ...
/// </summary>
public static class RandomExtensions
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="random"></param>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, in Rectangle rectangle)
    {
        return new(random.Next(rectangle.X, rectangle.X + rectangle.Width), random.Next(rectangle.Y, rectangle.Y + rectangle.Height));
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, float min, float max)
    {
        return new((float)((random.NextDouble() * (max - min)) + min), (float)((random.NextDouble() * (max - min)) + min));
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="random"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color NextColor(this Random random)
    {
        // FIXME: The `maxValue` of `Random.Next` is exclusive. These will never generate the valid value `255`.
        return new(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}
