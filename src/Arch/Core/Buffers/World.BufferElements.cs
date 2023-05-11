using System.Diagnostics.Contracts;
using Arch.Core.Buffers;

namespace Arch.Core;

public partial class World
{
    /// <summary>
    ///     Adds an element to the buffer of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="value">The buffer instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddToBuffer<T>(Entity entity, in T value = default)
    {
        AddOrGetBuffer<T>(entity).Elements.Add(value);
    }

    /// <summary>
    ///     Ensures the existence of a value in a buffer on an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="value">The value to ensure the existence of.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddOrGetFromBuffer<T>(Entity entity, in T value = default)
    {
        ref var elements = ref AddOrGetBuffer<T>(entity).Elements;
        if (!elements.Contains(value))
        {
            elements.Add(value);
        }
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/>'s buffer has a certain element.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the buffer has the desired element, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool HasInBuffer<T>(Entity entity, in T value)
    {
        ref var buffer = ref TryGetRefBuffer<T>(entity, out var exists);
        if (!exists)
        {
            return false;
        }

        return buffer.Elements.Contains(value);
    }

    /// <summary>
    ///     Removes an element from a buffer in a <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="value">The value to remove from the buffer.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveFromBuffer<T>(Entity entity, in T value)
    {
        ref var buffer = ref TryGetRef<Buffer<T>>(entity, out var exists);
        if (!exists)
        {
            return;
        }

        buffer.Elements.Remove(value);
    }
}
