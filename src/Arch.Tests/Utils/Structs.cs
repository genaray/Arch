namespace Arch.Tests;

public struct Transform
{
    public float X, Y;
}

public struct Rotation
{
    public float X, Y, Z, W;
}

public struct Ai { }

/// <summary>
///     A component holding a reference, used to cover managed component arrays.
/// </summary>
public struct Label
{
    public string Value;
}

public struct Health
{
    public int Value;
}

