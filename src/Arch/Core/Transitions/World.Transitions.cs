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
        var transition = oldArchetype.AddTransitions.ById.Get(type.Id);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Add(oldArchetype.Signature, type));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.AddTransitions.ById.Set(type.Id, transition);
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
        var transition = oldArchetype.RemoveTransitions.ById.Get(type.Id);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Remove(oldArchetype.Signature, type));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.RemoveTransitions.ById.Set(type.Id, transition);
        }

        return transition;
    }

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with several additional components.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    /// </summary>
    /// <param name="types">The <see cref="Signature"/> of the new components that additionally form a new <see cref="Archetype"/> with the old components of the old archetype.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    private ArchetypeTransition GetOrCreateAddTransition(Signature types, Archetype oldArchetype)
    {
        var transition = oldArchetype.AddTransitions.BySignature.Get(types);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Add(oldArchetype.Signature, types));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.AddTransitions.BySignature.Set(types, transition);
        }

        return transition;
    }

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with several components less.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    /// </summary>
    /// <param name="types">The <see cref="Signature"/> of the components that are removed from the old archetype to form the new one.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    private ArchetypeTransition GetOrCreateRemoveTransition(Signature types, Archetype oldArchetype)
    {
        var transition = oldArchetype.RemoveTransitions.BySignature.Get(types);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Remove(oldArchetype.Signature, types));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.RemoveTransitions.BySignature.Set(types, transition);
        }

        return transition;
    }

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with several additional components.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    ///     Unlike the overload with a <see cref="Signature"/> it does not allocate if the transition is cached, which suits types that are only known at runtime.
    /// </summary>
    /// <param name="types">The new <see cref="ComponentType"/>s that additionally form a new <see cref="Archetype"/> with the old components of the old archetype.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    internal ArchetypeTransition GetOrCreateAddTransition(Span<ComponentType> types, Archetype oldArchetype)
    {
        var transition = oldArchetype.AddTransitions.ByTypes.Get(types);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Add(oldArchetype.Signature, types.ToArray()));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.AddTransitions.ByTypes.Set(types, transition);
        }

        return transition;
    }

    /// <summary>
    ///     Creates or returns the <see cref="ArchetypeTransition"/> from an <see cref="Archetype"/> to the one with several components less.
    ///     Automatically caches it in the old <see cref="Archetype"/> for quick access.
    ///     Unlike the overload with a <see cref="Signature"/> it does not allocate if the transition is cached, which suits types that are only known at runtime.
    /// </summary>
    /// <param name="types">The <see cref="ComponentType"/>s that are removed from the old archetype to form the new one.</param>
    /// <param name="oldArchetype">The old <see cref="Archetype"/>.</param>
    /// <returns>The cached or newly created <see cref="ArchetypeTransition"/>.</returns>
    internal ArchetypeTransition GetOrCreateRemoveTransition(Span<ComponentType> types, Archetype oldArchetype)
    {
        var transition = oldArchetype.RemoveTransitions.ByTypes.Get(types);
        if (transition is null)
        {
            var archetype = GetOrCreate(Signature.Remove(oldArchetype.Signature, types));
            transition = new ArchetypeTransition(oldArchetype, archetype);
            oldArchetype.RemoveTransitions.ByTypes.Set(types, transition);
        }

        return transition;
    }
}
