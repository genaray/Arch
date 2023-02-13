using Microsoft.Xna.Framework;

namespace Arch.Samples;

/// <summary>
///     The <see cref="RandomExtensions"/> class
///     is an extension class which contains several methods for <see cref="Random"/>.
/// </summary>
public static class RandomExtensions
{

    /// <summary>
    ///     Creates a random <see cref="Vector2"/> in a predefined area.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> instance.</param>
    /// <param name="rectangle">The predefined area as a <see cref="Rectangle"/>.</param>
    /// <returns>A random <see cref="Vector2"/> inside the area.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, in Rectangle rectangle)
    {
        return new(random.Next(rectangle.X, rectangle.X + rectangle.Width), random.Next(rectangle.Y, rectangle.Y + rectangle.Height));
    }

    /// <summary>
    ///     Creates a random <see cref="Vector2"/> by a minimum and maximum value.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> instance.</param>
    /// <param name="min">The minimum possible x and y value.</param>
    /// <param name="max">The maximum possible x and y value.</param>
    /// <returns>A random <see cref="Vector2"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, float min, float max)
    {
        return new((float)((random.NextDouble() * (max - min)) + min), (float)((random.NextDouble() * (max - min)) + min));
    }

    /// <summary>
    ///     Creates a random <see cref="Color"/> in the valid RGB colorspace.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> instance.</param>
    /// <returns>A random <see cref="Color"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color NextColor(this Random random)
    {
        return new(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
    }
}
