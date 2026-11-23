using Arch.Core.Utils;

namespace Arch.Core;

public partial class World
{

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with one additional component.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    /// </summary>
    /// <param name="type">The new <see cref="ComponentType"/> that additionally forms a new <see cref="Archetype"/> with the old components of the old archetype.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    private ArchetypeTransition GetOrCreateAddTransition(in ComponentType type, Archetype oldArchetype)
    {
        var transition = oldArchetype.AddTransitions.Get(type.Id);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Add(oldArchetype.Signature, type));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.AddTransitions.Set(type.Id, transition);
        }

        return transition;
    }

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with one component less.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> that is removed from the old archetype to form the new one.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    private ArchetypeTransition GetOrCreateRemoveTransition(in ComponentType type, Archetype oldArchetype)
    {
        var transition = oldArchetype.RemoveTransitions.Get(type.Id);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Remove(oldArchetype.Signature, type));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.RemoveTransitions.Set(type.Id, transition);
        }

        return transition;
    }
}
