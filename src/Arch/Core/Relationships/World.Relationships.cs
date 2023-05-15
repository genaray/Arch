using System.Diagnostics.Contracts;
using Arch.Core.Relationships;

namespace Arch.Core;

public partial class World
{
    private bool _handlingRelationshipCleanup;

    public void HandleRelationshipCleanup()
    {
        if (_handlingRelationshipCleanup)
        {
            return;
        }

        SubscribeEntityDestroyed(CleanupRelationships);

        _handlingRelationshipCleanup = true;
    }

    private void CleanupRelationships(in Entity entity)
    {
        ref var relationships = ref TryGetRefRelationships<ArchRelationshipComponent>(entity, out var exists);

        if (!exists)
        {
            return;
        }

        foreach (var (target, relationship) in relationships.Elements)
        {
            var buffer = relationship.Relationships;
            buffer.Remove(entity);

            if (buffer.Count == 0)
            {
                buffer.Destroy(this, target);
            }

            ref var targetRelationships = ref TryGetRefRelationships<ArchRelationshipComponent>(target, out exists);

            if (!exists)
            {
                continue;
            }

            targetRelationships.Remove(entity);
        }
    }

    /// <summary>
    ///     Ensures the existence of a buffer of relationships on an <see cref="Entity"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Entity"/> of the relationships.</param>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <returns>The relationships.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref EntityRelationshipBuffer<T> AddOrGetRelationships<T>(Entity source)
    {
        ref var component = ref TryGetRef<EntityRelationshipBuffer<T>>(source, out var exists);
        if (exists)
        {
            return ref component;
        }

        Add(source, new EntityRelationshipBuffer<T>());
        return ref Get<EntityRelationshipBuffer<T>>(source);
    }

    /// <summary>
    ///     Adds a new relationship to the <see cref="Entity"/>.
    /// </summary>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="relationship">The relationship instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddRelationship<T>(Entity source, Entity target, in T relationship = default)
    {
        ref var buffer = ref AddOrGetRelationships<T>(source);
        buffer.Add(in relationship, target);

        var targetComp = new ArchRelationshipComponent(buffer);
        ref var targetBuffer = ref AddOrGetRelationships<ArchRelationshipComponent>(target);
        targetBuffer.Add(in targetComp, source);
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
    public T AddOrGetRelationship<T>(Entity source, Entity target, in T relationship = default)
    {
        ref var relationships = ref TryGetRefRelationships<T>(source, out var exists);
        if (exists)
        {
            return relationships.Elements[target];
        }

        AddRelationship(source, target, in relationship);
        return GetRelationship<T>(source, target);
    }

    /// <summary>
    ///     Returns all relationships of the given type of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <returns>A reference to the relationships.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal ref EntityRelationshipBuffer<T> GetRelationships<T>(Entity source)
    {
        return ref Get<EntityRelationshipBuffer<T>>(source);
    }

    /// <summary>
    ///     Returns a relationship of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <returns>The relationship.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public T GetRelationship<T>(Entity source, Entity target)
    {
        ref var relationships = ref GetRelationships<T>(source);
        return relationships.Elements[target];
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain relationship.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The source <see cref="Entity"/> of the relationship.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    /// <returns>True if it has the desired relationship, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool HasRelationship<T>(Entity source, Entity target)
    {
        ref var relationships = ref TryGetRefRelationships<T>(source, out var exists);
        if (!exists)
        {
            return false;
        }

        return relationships.Elements.ContainsKey(target);
    }

    /// <summary>
    ///     Removes a relationship from an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The <see cref="Entity"/> to remove the relationship from.</param>
    /// <param name="target">The target <see cref="Entity"/> of the relationship.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveRelationship<T>(Entity source, Entity target)
    {
        ref var buffer = ref GetRelationships<T>(source);
        buffer.Remove(target);

        if (buffer.Count == 0)
        {
            Remove<EntityRelationshipBuffer<T>>(source);
        }

        ref var targetBuffer = ref GetRelationships<ArchRelationshipComponent>(target);
        targetBuffer.Remove(source);

        if (targetBuffer.Count == 0)
        {
            Remove<EntityRelationshipBuffer<ArchRelationshipComponent>>(target);
        }
    }

    /// <summary>
    ///     Tries to return an <see cref="Entity"/>s relationships of the specified type.
    /// </summary>
    /// <typeparam name="T">The relationship type.</typeparam>
    /// <param name="source">The <see cref="Entity"/>.</param>
    /// <param name="relationships">The found relationships.</param>
    /// <returns>True if it exists, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal bool TryGetRelationships<T>(Entity source, out EntityRelationshipBuffer<T> relationships)
    {
        return TryGet(source, out relationships);
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
    public bool TryGetRelationship<T>(Entity source, Entity target, out T relationship)
    {
        ref var relationships = ref TryGetRefRelationships<T>(source, out var exists);
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
    internal ref EntityRelationshipBuffer<T> TryGetRefRelationships<T>(Entity source, out bool exists)
    {
        return ref TryGetRef<EntityRelationshipBuffer<T>>(source, out exists);
    }
}
