using Arch.Core.Utils;

namespace Arch;

internal static class ThrowHelper
{
    [DoesNotReturn]
    public static void Throw_ComponentDoesNotExists(ComponentType type)
    {
        throw new InvalidOperationException($"You are trying to access a component({type}) that does not exist in the entity or chunk.");
    }

    [DoesNotReturn]
    public static void Throw_SameArchetype()
    {
        throw new InvalidOperationException($"From-Archetype is the same as the To-Archetype. Entities cannot move within the same archetype using this function. Probably an attempt was made to attach already existing components to the entity or to remove non-existing ones.");
    }
}