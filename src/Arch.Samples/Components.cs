using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Samples;

// NOTE: Should this not be a `Vector3` to represent something in 3D space?
// NOTE: Should this even be wrapped in a struct at all? The `Vector` itself is the representation of the point already.
/// <summary>
///     The <see cref="Position"/> struct
///     contains the position of an entity in 3D space.
/// </summary>
public struct Position
{
    // TODO: Documentation?
    public Vector2 Vec2;
}

// NOTE: Should this not be a `Vector3` to represent something in 3D space?
// NOTE: Should this even be wrapped in a struct at all? The `Vector` itself is the representation of the point already.
/// <summary>
///     The <see cref="Velocity"/> struct
///     contains the velocity of an entity in 3D space.
/// </summary>
public struct Velocity
{
    // TODO: Documentation?
    public Vector2 Vec2;
}

/// <summary>
///     The <see cref="Sprite"/> struct
///     contains information about an entity's appearance.
/// </summary>
public struct Sprite
{
    // TODO: Documentation?
    public Texture2D Texture2D;
    public Color Color;
}
