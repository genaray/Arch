namespace Arch.Core;

/// <summary>
///     The cached result of adding or removing components to an <see cref="Archetype"/>:
///     The <see cref="Target"/> archetype and how the components of an entity are copied into it.
/// </summary>
internal sealed class ArchetypeTransition
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ArchetypeTransition"/> class.
    /// </summary>
    /// <param name="source">The <see cref="Archetype"/> the entities come from.</param>
    /// <param name="target">The <see cref="Archetype"/> the entities move to, it differs from the source by the added or removed components.</param>
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
