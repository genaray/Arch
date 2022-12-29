using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Arch.Samples;

public static class RandomExtensions
{
    
    /// <summary>
    /// Creates a random vec2 inside the rectangle and returns it
    /// </summary>
    /// <param name="random"></param>
    /// <param name="rectangle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, in Rectangle rectangle)
    {
        return new Vector2(random.Next(rectangle.X, rectangle.X+rectangle.Width), random.Next(rectangle.Y, rectangle.Y+rectangle.Height));
    }
    
    /// <summary>
    /// Creates a random vec2 between two floats.
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 NextVector2(this Random random, float min, float max)
    {
        return new Vector2((float)(random.NextDouble() * (max - min) + min), (float)(random.NextDouble() * (max - min) + min));
    }
    
    /// <summary>
    /// Creates a random vec2 between two floats.
    /// </summary>
    /// <param name="random"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color NextColor(this Random random)
    {
        return new Color(random.Next(0,255),random.Next(0,255),random.Next(0,255));
    }
}