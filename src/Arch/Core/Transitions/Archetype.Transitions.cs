namespace Arch.Core;

public partial class Archetype
{
    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by adding a component.
    /// </summary>
    internal TransitionCache AddTransitions { get; } = new();

    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by removing a component.
    /// </summary>
    internal TransitionCache RemoveTransitions { get; } = new();

    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by adding several components at once, by their <see cref="Signature"/>.
    /// </summary>
    internal Dictionary<Signature, ArchetypeTransition> AddSignatureTransitions { get; } = new();

    /// <summary>
    ///     Caches the <see cref="ArchetypeTransition"/>s that are reached by removing several components at once, by their <see cref="Signature"/>.
    /// </summary>
    internal Dictionary<Signature, ArchetypeTransition> RemoveSignatureTransitions { get; } = new();

    /// <summary>
    ///     Removes all cached transitions that lead to a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove transitions to.</param>
    internal void RemoveTransitionsTo(Archetype archetype)
    {
        AddTransitions.RemoveTransitionsTo(archetype);
        RemoveTransitions.RemoveTransitionsTo(archetype);

        // Archetypes are rarely removed, so dropping all signature transitions is simpler than filtering them.
        AddSignatureTransitions.Clear();
        RemoveSignatureTransitions.Clear();
    }
}