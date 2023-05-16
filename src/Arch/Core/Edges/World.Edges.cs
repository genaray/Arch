using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;

namespace Arch.Core;

public partial class World
{

    /// <summary>
    ///     Creates or returns an <see cref="Archetype"/> based on the old one with one additional component.
    ///     Automatically creates a link between them for quick access. 
    /// </summary>
    /// <param name="type">The new <see cref="ComponentType"/> that additionally forms a new <see cref="Archetype"/> with the old components of the old archetype.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="Archetype"/> with that additional component.</returns>
    private Archetype GetOrCreateArchetypeByEdge(in ComponentType type, Archetype oldArchetype)
    {
        var edgeIndex = type.Id - 1;
        
#if NET5_0_OR_GREATER
        var newArchetype = oldArchetype.CreateOrGetAddEdge(edgeIndex, out var exists);
        if (!exists)
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(type));
        }
#else
        if (!oldArchetype.TryGetAddEdge(edgeIndex, out var newArchetype))
        {
            newArchetype = GetOrCreate(oldArchetype.Types.Add(type));
            oldArchetype.CreateAddEdge(edgeIndex, newArchetype);
        }
#endif
        return newArchetype;
    }
}
