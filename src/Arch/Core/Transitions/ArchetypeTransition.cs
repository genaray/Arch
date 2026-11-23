namespace Arch.Core;

/// <summary>
///     The cached result of adding or removing a single component to an <see cref="Archetype"/>:
///     The <see cref="Target"/> archetype and how the components of an entity are copied into it.
/// </summary>
internal sealed class ArchetypeTransition
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ArchetypeTransition"/> class.
    /// </summary>
    /// <param name="source">The <see cref="Archetype"/> the entities come from.</param>
    /// <param name="target">The <see cref="Archetype"/> the entities move to, it differs from the source by one component.</param>
    internal ArchetypeTransition(Archetype source, Archetype target)
    {
        Target = target;

        // The component that is only in one of both archetypes is not copied.
        var sourceComponents = source.Signature.Components;
        Copies = new (int Source, int Destination)[Math.Min(sourceComponents.Length, target.Signature.Components.Length)];

        var copied = 0;
        for (var sourceIndex = 0; sourceIndex < sourceComponents.Length; sourceIndex++)
        {
            if (!target.TryIndex(sourceComponents[sourceIndex], out var destinationIndex))
            {
                continue;
            }

            Copies[copied] = (sourceIndex, destinationIndex);
            copied++;
        }
    }

    /// <summary>
    ///     The <see cref="Archetype"/> entities move to.
    /// </summary>
    internal Archetype Target { get; }

    /// <summary>
    ///     The components that are copied, as the index of their array in the source <see cref="Chunk"/>s and in the <see cref="Target"/> <see cref="Chunk"/>s.
    /// </summary>
    internal (int Source, int Destination)[] Copies { get; }
}

/// <summary>
///     Caches the <see cref="ArchetypeTransition"/>s of an <see cref="Archetype"/>, indexed by the id of the added or removed <see cref="Utils.ComponentType"/>.
/// </summary>
internal sealed class TransitionCache
{
    private ArchetypeTransition?[] _transitions = Array.Empty<ArchetypeTransition?>();

    /// <summary>
    ///     Returns the cached transition of a component, or null if there is none yet.
    /// </summary>
    /// <param name="id">The id of the <see cref="Utils.ComponentType"/>.</param>
    /// <returns>The <see cref="ArchetypeTransition"/> or null.</returns>
    internal ArchetypeTransition? Get(int id)
    {
        return (uint)id < (uint)_transitions.Length ? _transitions[id] : null;
    }

    /// <summary>
    ///     Caches the transition of a component.
    /// </summary>
    /// <param name="id">The id of the <see cref="Utils.ComponentType"/>.</param>
    /// <param name="transition">The <see cref="ArchetypeTransition"/>.</param>
    internal void Set(int id, ArchetypeTransition transition)
    {
        if (_transitions.Length <= id)
        {
            Array.Resize(ref _transitions, id + 1);
        }

        _transitions[id] = transition;
    }

    /// <summary>
    ///     Removes all cached transitions that lead to a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove the transitions to.</param>
    internal void RemoveTransitionsTo(Archetype archetype)
    {
        for (var index = 0; index < _transitions.Length; index++)
        {
            if (_transitions[index]?.Target == archetype)
            {
                _transitions[index] = null;
            }
        }
    }
}