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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreateArchetypeByAddEdge(in ComponentType type, Archetype oldArchetype)
    {
        Archetype archetype;
        var edgeIndex = type.Id - 1;

        if (!oldArchetype.HasAddEdgde(edgeIndex))
        {
            archetype = GetOrCreate(oldArchetype.Types.Add(type));
            oldArchetype.AddAddEdgde(edgeIndex, archetype);
        }
        else
        {
            archetype = oldArchetype.GetAddEdge(edgeIndex);
        }

        return archetype;
    }

    /// <summary>
    ///     Creates or returns an <see cref="Archetype"/> based on the old one with one additional component.
    ///     Automatically creates a link between them for quick access.
    /// </summary>
    /// <param name="type">The new <see cref="ComponentType"/> that additionally forms a new <see cref="Archetype"/> with the old components of the old archetype.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="Archetype"/> with that additional component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Archetype GetOrCreateArchetypeByRemoveEdge(in ComponentType type, Archetype oldArchetype)
    {
        Archetype archetype;
        var edgeIndex = type.Id - 1;

        if (!oldArchetype.HasRemoveEdgde(edgeIndex))
        {
            archetype = GetOrCreate(oldArchetype.Types.Remove(type));
            oldArchetype.AddRemoveEdgde(edgeIndex, archetype);
        }
        else
        {
            archetype = oldArchetype.GetRemoveEdge(edgeIndex);
        }

        return archetype;
    }
}
