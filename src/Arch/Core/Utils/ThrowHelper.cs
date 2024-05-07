using Arch.Core.Utils;

namespace Arch;

internal static class ThrowHelper
{
    [DoesNotReturn]
    public static void Throw_ComponentDoesNotExists(ComponentType type)
    {
        throw new ArgumentException($"Component {type.Type} does not exists");
    }

    [DoesNotReturn]
    public static void Throw_EntityDoesNotExists()
    {
        throw new ArgumentException($"Entity does not exists");
    }
}