using System.Buffers;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using Arch.LowLevel.Jagged;

namespace Arch.Core;

public partial class Archetype
{
    /// <summary>
    ///     The bucket size of each bucket inside the <see cref="_addEdges"/>.
    /// </summary>
    private const int BucketSize = 16;

    /// <summary>
    ///     Caches other <see cref="Archetype"/>s indexed by the
    ///     <see cref="ComponentType.Id"/> that needs to be added in order to reach them.
    /// </summary>
    /// <remarks>The index used is <see cref="ComponentType.Id"/> minus one.</remarks>
    private readonly SparseJaggedArray<Archetype> _addEdges;

    /// <summary>
    ///     Caches other <see cref="Archetype"/>s indexed by the
    ///     <see cref="ComponentType.Id"/> that needs to be added in order to reach them.
    /// </summary>
    /// <remarks>The index used is <see cref="ComponentType.Id"/> minus one.</remarks>
    private readonly SparseJaggedArray<Archetype> _removeEdges;

    /// <summary>
    ///     Adds an add edge.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AddAddEdgde(int index, Archetype archetype)
    {
        _addEdges.Add(index, archetype);
    }

    /// <summary>
    ///     Adds an remove edge.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AddRemoveEdgde(int index, Archetype archetype)
    {
        _removeEdges.Add(index, archetype);
    }

    /// <summary>
    ///     Checks if an edge exists.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>True or false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool HasAddEdgde(int index)
    {
        return _addEdges.ContainsKey(index);
    }

    /// <summary>
    ///     Checks if an edge exists.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>True or false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal bool HasRemoveEdgde(int index)
    {
        return _removeEdges.ContainsKey(index);
    }

    /// <summary>
    ///     Tries to get a cached archetype that is reached through adding a component
    ///     type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <returns>The cached archetype if it exists, null otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Archetype GetAddEdge(int index)
    {
        return _addEdges[index];
    }

    /// <summary>
    ///     Tries to get a cached archetype that is reached through adding a component
    ///     type to this archetype.
    /// </summary>
    /// <param name="index">
    ///     The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1
    /// </param>
    /// <returns>The cached archetype if it exists, null otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Archetype GetRemoveEdge(int index)
    {
        return _removeEdges[index];
    }


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
    ///     Removes an Edge at the given index.
    /// </summary>
    /// <param name="index">The index of the archetype in the cache, <see cref="ComponentType.Id"/> - 1</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveRemoveEdge(int index)
    {
        _removeEdges.Remove(index);
    }

    /// TODO: API to return a bucket from a jagged array, empty ones can be skipped -> Super fast iteration
    /// <summary>
    ///     Removes an edge for a certain <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> to remove edges for.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemoveEdge(Archetype archetype)
    {
        for (var index = 0; index < _addEdges.Buckets; index++)
        {
            // Skip empty buckets
            ref var bucket = ref _addEdges.GetBucket(index);
            if (bucket.IsEmpty)
            {
                continue;
            }

            // Search bucket for edge and remove it if found
            for (var itemIndex = 0; itemIndex < bucket.Capacity; itemIndex++)
            {
                var edge = bucket[itemIndex];
                if (edge == archetype)
                {
                    RemoveAddEdge(index);
                }
            }
        }

        for (var index = 0; index < _removeEdges.Buckets; index++)
        {
            // Skip empty buckets
            ref var bucket = ref _removeEdges.GetBucket(index);
            if (bucket.IsEmpty)
            {
                continue;
            }

            // Search bucket for edge and remove it if found
            for (var itemIndex = 0; itemIndex < bucket.Capacity; itemIndex++)
            {
                var edge = bucket[itemIndex];
                if (edge == archetype)
                {
                    RemoveRemoveEdge(index);
                }
            }
        }
    }
}
