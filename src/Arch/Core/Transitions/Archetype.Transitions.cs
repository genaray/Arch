namespace Arch.Core;

public partial class Archetype
{
    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by adding components.
    /// </summary>
    internal TransitionCaches AddTransitions { get; } = new();

    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by removing components.
    /// </summary>
    internal TransitionCaches RemoveTransitions { get; } = new();

    /// <summary>
    ///     Removes all cached transitions that lead to a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove transitions to.</param>
    internal void RemoveTransitionsTo(Archetype archetype)
    {
        AddTransitions.RemoveTransitionsTo(archetype);
        RemoveTransitions.RemoveTransitionsTo(archetype);
    }
}
