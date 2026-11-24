namespace Arch.Core;

/// <summary>
///     The <see cref="TransitionCaches"/> class
///     bundles the caches of one direction, adding or removing components, of an <see cref="Archetype"/>.
///     Every cache answers which <see cref="Archetype"/> is reached by changing certain components, but for another kind of key:
///     <list type="bullet">
///         <item><see cref="ById"/> for a single component, an array lookup and therefore the fastest.</item>
///         <item><see cref="BySignature"/> for several components known at compile time, like <c>Component&lt;T0, T1&gt;.Signature</c> whose hash is already cached.</item>
///         <item><see cref="ByTypes"/> for several components only known at runtime, like a <see cref="Span{T}"/> of <see cref="ComponentType"/>s, without allocating a <see cref="Signature"/>.</item>
///     </list>
/// </summary>
internal sealed class TransitionCaches
{
    /// <summary>
    ///     The transitions of a single component, by its id.
    /// </summary>
    internal IdTransitionCache ById { get; } = new();

    /// <summary>
    ///     The transitions of several components, by their <see cref="Signature"/>.
    /// </summary>
    internal SignatureTransitionCache BySignature { get; } = new();

    /// <summary>
    ///     The transitions of several components, by a <see cref="Span{T}"/> of their <see cref="ComponentType"/>s.
    /// </summary>
    internal TypeSetTransitionCache ByTypes { get; } = new();

    /// <summary>
    ///     Removes all cached transitions that lead to a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove the transitions to.</param>
    internal void RemoveTransitionsTo(Archetype archetype)
    {
        ById.RemoveTransitionsTo(archetype);

        // Archetypes are rarely removed, so dropping all transitions of the other caches is simpler than filtering them.
        BySignature.Clear();
        ByTypes.Clear();
    }
}

/// <summary>
///     The <see cref="IdTransitionCache"/> class
///     caches the <see cref="ArchetypeTransition"/>s of an <see cref="Archetype"/>, indexed by the id of the added or removed <see cref="ComponentType"/>.
/// </summary>
internal sealed class IdTransitionCache
{
    private ArchetypeTransition?[] _transitions = Array.Empty<ArchetypeTransition?>();

    /// <summary>
    ///     Returns the cached transition of a component, or null if there is none yet.
    /// </summary>
    /// <param name="id">The id of the <see cref="ComponentType"/>.</param>
    /// <returns>The <see cref="ArchetypeTransition"/> or null.</returns>
    internal ArchetypeTransition? Get(int id)
    {
        return (uint)id < (uint)_transitions.Length ? _transitions[id] : null;
    }

    /// <summary>
    ///     Caches the transition of a component.
    /// </summary>
    /// <param name="id">The id of the <see cref="ComponentType"/>.</param>
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

/// <summary>
///     The <see cref="SignatureTransitionCache"/> class
///     caches the <see cref="ArchetypeTransition"/>s of an <see cref="Archetype"/>, by the <see cref="Signature"/> of the added or removed <see cref="ComponentType"/>s.
///     A <see cref="Signature"/> caches its hash, which makes it a cheap key if it is kept, like the static signatures of the generic overloads.
/// </summary>
internal sealed class SignatureTransitionCache
{
    private readonly Dictionary<Signature, ArchetypeTransition> _transitions = new();

    /// <summary>
    ///     Returns the cached transition of a <see cref="Signature"/>, or null if there is none yet.
    /// </summary>
    /// <param name="signature">The <see cref="Signature"/> of the added or removed <see cref="ComponentType"/>s.</param>
    /// <returns>The <see cref="ArchetypeTransition"/> or null.</returns>
    internal ArchetypeTransition? Get(Signature signature)
    {
        return _transitions.TryGetValue(signature, out var transition) ? transition : null;
    }

    /// <summary>
    ///     Caches the transition of a <see cref="Signature"/>.
    /// </summary>
    /// <param name="signature">The <see cref="Signature"/> of the added or removed <see cref="ComponentType"/>s.</param>
    /// <param name="transition">The <see cref="ArchetypeTransition"/>.</param>
    internal void Set(Signature signature, ArchetypeTransition transition)
    {
        _transitions[signature] = transition;
    }

    /// <summary>
    ///     Removes all cached transitions.
    /// </summary>
    internal void Clear()
    {
        _transitions.Clear();
    }
}

/// <summary>
///     The <see cref="TypeSetTransitionCache"/> class
///     caches the <see cref="ArchetypeTransition"/>s of an <see cref="Archetype"/>, by the set of <see cref="ComponentType"/>s that are added or removed.
///     Unlike the <see cref="SignatureTransitionCache"/> it can be asked with any <see cref="Span{T}"/> of types, without allocating a <see cref="Signature"/> for the lookup.
/// </summary>
/// <remarks>
///     The key is a hash of the types which does not depend on their order. Two different sets with the same hash replace each other,
///     which only costs a lookup since the types of the cached set are always compared. Sets that contain a type twice never match and are looked up again every time.
/// </remarks>
internal sealed class TypeSetTransitionCache
{
    /// <summary>
    ///     A cached set of types and its <see cref="ArchetypeTransition"/>.
    /// </summary>
    private readonly record struct Entry(ComponentType[] Types, ArchetypeTransition Transition);

    // Multiplicative hashing constant, the 32 bit golden ratio.
    private const uint HashMultiplier = 2654435761u;

    private readonly Dictionary<int, Entry> _entries = new();

    /// <summary>
    ///     Returns the cached transition of a set of types, or null if there is none yet. The order of the types does not matter.
    /// </summary>
    /// <param name="types">The added or removed <see cref="ComponentType"/>s.</param>
    /// <returns>The <see cref="ArchetypeTransition"/> or null.</returns>
    internal ArchetypeTransition? Get(Span<ComponentType> types)
    {
        if (_entries.TryGetValue(HashOf(types), out var entry) && ContainSameTypes(entry.Types, types))
        {
            return entry.Transition;
        }

        return null;
    }

    /// <summary>
    ///     Caches the transition of a set of types.
    /// </summary>
    /// <param name="types">The added or removed <see cref="ComponentType"/>s.</param>
    /// <param name="transition">The <see cref="ArchetypeTransition"/>.</param>
    internal void Set(Span<ComponentType> types, ArchetypeTransition transition)
    {
        _entries[HashOf(types)] = new Entry(types.ToArray(), transition);
    }

    /// <summary>
    ///     Removes all cached transitions.
    /// </summary>
    internal void Clear()
    {
        _entries.Clear();
    }

    /// <summary>
    ///     Calculates a hash of a set of types that does not depend on their order, by adding up the hashes of their ids.
    ///     The ids are small and consecutive, so they are scattered first, otherwise sets like {1, 4} and {2, 3} would collide.
    /// </summary>
    private static int HashOf(Span<ComponentType> types)
    {
        var hash = 0;
        foreach (ref var type in types)
        {
            var scattered = (uint)type.Id * HashMultiplier;
            hash += (int)(scattered ^ (scattered >> 15));
        }

        return hash;
    }

    /// <summary>
    ///     Checks whether both contain the same types, in any order. Quadratic, but a set rarely has more than a few types.
    /// </summary>
    private static bool ContainSameTypes(Span<ComponentType> cached, Span<ComponentType> types)
    {
        if (cached.Length != types.Length)
        {
            return false;
        }

        foreach (ref var type in types)
        {
            if (cached.IndexOf(type) < 0)
            {
                return false;
            }
        }

        return true;
    }
}
