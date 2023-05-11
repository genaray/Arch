using System.Diagnostics.Contracts;
using Arch.Core.Utils;

namespace Arch.Core;

public partial class World
{
    /// <summary>
    ///     Adds a new relationship to the <see cref="Entity"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="relationship">The relationship instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddPair<T>(Entity source, Entity target, in T relationship = default)
    {
        ref var buffer = ref AddOrGet(source, static () => new EntityPairBuffer<T>());
        buffer.Add(in relationship, target);

        var pairComponent = new ArchRelationshipComponent(buffer);
        ref var targetBuffer = ref AddOrGet(target, static () => new EntityPairBuffer<ArchRelationshipComponent>());
        targetBuffer.Add(in pairComponent, source);
    }

    /// <summary>
    ///     Ensures the existence of a relationship on an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <param name="relationship">The relationship value used if its being added.</param>
    /// <returns>The relationship.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T AddOrGetPair<T>(Entity source, Entity target, in T relationship = default)
    {
        ref var pair = ref TryGetRefPairs<T>(source, out var exists);
        if (exists)
        {
            return pair.Elements[target];
        }

        AddPair(source, target, in relationship);
        return GetPair<T>(source, target);
    }

    /// <summary>
    ///     Returns all relationships of the given type of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <returns>A reference to the relationships.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal ref EntityPairBuffer<T> GetPairs<T>(Entity source)
    {
        return ref Get<EntityPairBuffer<T>>(source);
    }

    /// <summary>
    ///     Returns a relationship of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <returns>The relationship.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public T GetPair<T>(Entity source, Entity target)
    {
        ref var pairs = ref GetPairs<T>(source);
        return pairs.Elements[target];
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain relationship.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <returns>True if it has the desired relationship, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool HasPair<T>(Entity source, Entity target)
    {
        ref var pairs = ref TryGetRefPairs<T>(source, out var exists);
        if (!exists)
        {
            return false;
        }

        return pairs.Elements.ContainsKey(target);
    }

    /// <summary>
    ///     Removes a relationship from an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The <see cref="Entity"/> to remove the relationship from.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemovePair<T>(Entity source, Entity target)
    {
        ref var buffer = ref GetPairs<T>(source);
        buffer.Remove(target);

        if (buffer.Count == 0)
        {
            Remove<EntityPairBuffer<T>>(source);
        }

        ref var targetBuffer = ref GetPairs<ArchRelationshipComponent>(target);
        targetBuffer.Remove(source);

        if (targetBuffer.Count == 0)
        {
            Remove<EntityPairBuffer<ArchRelationshipComponent>>(target);
        }
    }

    /// <summary>
    ///     Tries to return an <see cref="Entity"/>s relationships of the specified type.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The <see cref="Entity"/>.</param>
    /// <param name="pairs">The found relationships.</param>
    /// <returns>True if it exists, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal bool TryGetPairs<T>(Entity source, out EntityPairBuffer<T> pairs)
    {
        return TryGet(source, out pairs);
    }

    /// <summary>
    ///     Tries to return an <see cref="Entity"/>s relationship of the specified type.
    ///     Will copy the relationship if its a struct.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <param name="relationship">The found relationship.</param>
    /// <returns>True if it exists, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool TryGetPair<T>(Entity source, Entity target, out T relationship)
    {
        ref var relationships = ref TryGetRefPairs<T>(source, out var exists);
        if (!exists)
        {
            relationship = default;
            return false;
        }

        return relationships.Elements.TryGetValue(target, out relationship);
    }

    /// <summary>
    ///     Tries to return a reference to an <see cref="Entity"/>s relationships of the
    ///     specified type.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, otherwise false.</param>
    /// <returns>A reference to the relationships.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal ref EntityPairBuffer<T> TryGetRefPairs<T>(Entity source, out bool exists)
    {
        return ref TryGetRef<EntityPairBuffer<T>>(source, out exists);
    }
}
