using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Samples;

/// <summary>
/// The 3D position of our entity.
/// </summary>
public struct Position
{
    public Vector2 Vec2;
}

/// <summary>
/// The Velocity, its movement in 3D space. 
/// </summary>
public struct Velocity
{
    public Vector2 Vec2;
}

/// <summary>
/// The sprite/texture of an entity. 
/// </summary>
public struct Sprite
{
    public Texture2D Texture2D;
    public Color Color;
}
