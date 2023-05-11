using System.Diagnostics.Contracts;
using Arch.Core.Buffers;

namespace Arch.Core;

public partial class World
{
    /// <summary>
    ///     Adds an new buffer to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddBuffer<T>(Entity entity)
    {
        Add(entity, new Buffer<T>());
    }

    /// <summary>
    ///     Ensures the existence of an buffer on an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Buffer<T> AddOrGetBuffer<T>(Entity entity)
    {
        return ref AddOrGet(entity, static () => new Buffer<T>());
    }

    /// <summary>
    ///     Returns a reference to the buffer of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>A reference to the buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public ref Buffer<T> GetBuffer<T>(Entity entity)
    {
        return ref Get<Buffer<T>>(entity);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain buffer.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if it has the desired buffer, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool HasBuffer<T>(Entity entity)
    {
        return Has<Buffer<T>>(entity);
    }

    /// <summary>
    ///     Removes a buffer from an <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveBuffer<T>(Entity entity)
    {
        Remove<Buffer<T>>(entity);
    }

    /// <summary>
    ///     Trys to return a reference to the buffer of an <see cref="Entity"/>.
    ///     Will copy the buffer.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="buffer">The found buffer.</param>
    /// <returns>True if it exists, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool TryGetBuffer<T>(Entity entity, out Buffer<T> buffer)
    {
        return TryGet(entity, out buffer);
    }

    /// <summary>
    ///     Tries to return a reference to the buffer of an <see cref="Entity"/>.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="exists">True if it exists, otherwise false.</param>
    /// <returns>A reference to the buffer.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public ref Buffer<T> TryGetRefBuffer<T>(Entity entity, out bool exists)
    {
        return ref TryGetRef<Buffer<T>>(entity, out exists);
    }

    /// <summary>
    ///     Removes a buffer of the given type from an <see cref="Entity"/> if it is empty.
    /// </summary>
    /// <typeparam name="T">The buffer's element type.</typeparam>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>True if the buffer was empty and removed, false otherwise..</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    public bool TrimBuffer<T>(Entity entity)
    {
        ref var buffer = ref TryGetRefBuffer<T>(entity, out var exists);
        if (!exists)
        {
            return false;
        }

        if (buffer.Elements.Count != 0)
        {
            return false;
        }

        RemoveBuffer<T>(entity);
        return true;
    }
}
