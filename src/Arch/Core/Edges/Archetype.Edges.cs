using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;

namespace Arch.Core;

public partial class Archetype
{
    /// <summary>
    ///     The max <see cref="ComponentType.Id"/> that <see cref="_addEdges"/>
    ///     will use for its array storage before using a dictionary.
    /// </summary>
    private const int EdgesArrayMaxSize = 256;

    /// <summary>
    ///     Caches other <see cref="Archetype"/>s indexed by the
    ///     <see cref="ComponentType.Id"/> that needs to be added in order to reach them.
    ///     Those with a <see cref="ComponentType.Id"/> equal to or lower than
    ///     <see cref="EdgesArrayMaxSize"/> are accessed through an array lookup,
    ///     otherwise a dictionary is used.
    /// </summary>
    /// <remarks>The index used is <see cref="ComponentType.Id"/> minus one.</remarks>
    private readonly ArrayDictionary<Archetype> _addEdges;

    /// <summary>
    ///     Tries to get a cached archetype that is reached through adding a component
    ///     type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <param name="archetype">The archetype to cache.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void CreateAddEdge(int index, Archetype archetype)
    {
        _addEdges.Set(index, archetype);
    }

#if NET5_0_OR_GREATER
    /// <summary>
    ///     Gets a reference to a cached archetype that is reached through adding a
    ///     component type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <param name="exists">True if the cached archetype existed, false otherwise.</param>
    /// <returns>
    ///     A reference to the archetype, or a null reference to the created slot in the
    ///     cache if it did not exist.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref Archetype CreateOrGetAddEdge(int index, [UnscopedRef] out bool exists)
    {
        return ref _addEdges.AddOrGet(index, out exists);
    }
#endif

    /// <summary>
    ///     Tries to get a cached archetype that is reached through adding a component
    ///     type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <param name="archetype">The cached archetype if it exists, null otherwise.</param>
    /// <returns>True if the archetype exists, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool TryGetAddEdge(int index, [NotNullWhen(true)] out Archetype? archetype)
    {
        archetype = _addEdges.AddOrGet(index, out var exists);
        return exists;
    }
}
