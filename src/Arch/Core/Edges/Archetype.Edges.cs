using System.Buffers;
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
    /// TODO : Kill me and replace me with a better jaggedarray.
    private readonly ArrayDictionary<Archetype> _addEdges;

#if !NET5_0_OR_GREATER

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
        _addEdges.Add(index, archetype);
    }

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
        return _addEdges.TryGet(index, out archetype);
    }

#else

    /// <summary>
    ///     Tries to get a cached archetype that is reached through adding a component
    ///     type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <param name="exists">True if it exists, false if not.</param>
    /// <returns>The cached archetype if it exists, null otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref Archetype TryGetAddEdge(int index, [UnscopedRef] out bool exists)
    {
        return ref _addEdges.TryGet(index, out exists!);
    }

#endif

    /// <summary>
    ///     Removes an Edge at the given index.
    /// </summary>
    /// <param name="index">The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveAddEdge(int index)
    {
        _addEdges.Remove(index);
    }

    /// <summary>
    ///     Removes an edge for a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove edges for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveAddEdge(Archetype archetype)
    {
        // Scan array for the archetype and remove it where it exists.
        for (var index = 0; index < _addEdges._array.Length; index++)
        {
            var edge = _addEdges._array[index];
            if (edge == archetype)
            {
                RemoveAddEdge(index);
            }
        }

        // Scan dictionary and remove it where it exists.
        var keys = ArrayPool<int>.Shared.Rent(_addEdges._dictionary.Count);
        var count = 0;
        foreach (var kvp in _addEdges._dictionary)
        {
            if (kvp.Value == archetype)
            {
                keys[count] = kvp.Key;
                count++;
            }
        }

        for (var index = 0; index < count; index++)
        {
            var key = keys[index];
            _addEdges._dictionary.Remove(key);
        }
        ArrayPool<int>.Shared.Return(keys, true);
    }
}
