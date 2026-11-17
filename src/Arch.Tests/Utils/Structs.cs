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

public struct RenderComponent
{
    public string MeshId;
    public string MaterialId;
    public bool IsVisible;
    public int RenderLayer;
    public bool CastsShadows;
    public bool ReceivesShadows;

    public static RenderComponent Default(string meshId, string materialId)
    {
        return new RenderComponent
        {
            MeshId = meshId,
            MaterialId = materialId,
            IsVisible = true,
            RenderLayer = 0,
            CastsShadows = true,
            ReceivesShadows = true
        };
    }
}
