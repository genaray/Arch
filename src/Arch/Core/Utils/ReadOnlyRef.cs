using CommunityToolkit.HighPerformance.Helpers;

namespace Arch.Core.Utils;

#if NETSTANDARD2_1_OR_GREATER || NET6_0

/// <summary>
/// The <see langword="struct"/> struct
/// can store a readonly reference to a value of a specified type.
/// </summary>
/// <typeparam name="T">The type of value to reference.</typeparam>
public readonly ref struct ReadOnlyRef<T>
{
    /// <summary>
    /// The 1-length <see cref="ReadOnlySpan{T}"/> instance used to track the target <typeparamref name="T"/> value.
    /// </summary>
    internal readonly ReadOnlySpan<T> Span;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyRef{T}"/> struct.
    /// </summary>
    /// <param name="value">The readonly reference to the target <typeparamref name="T"/> value.</param>

    public ReadOnlyRef(in T value)
    {
        ref T r0 = ref Unsafe.AsRef(value);

        this.Span = MemoryMarshal.CreateReadOnlySpan(ref r0, 1);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyRef{T}"/> struct.
    /// </summary>
    /// <param name="pointer">The pointer to the target value.</param>

    public unsafe ReadOnlyRef(void* pointer)
        : this(in Unsafe.AsRef<T>(pointer))
    {
    }

    /// <summary>
    /// Gets the readonly <typeparamref name="T"/> reference represented by the current <see cref="Ref{T}"/> instance.
    /// </summary>
    public ref readonly T Value
    {

        get => ref MemoryMarshal.GetReference(this.Span);
    }

    /// <summary>
    /// Implicitly converts a <see cref="Ref{T}"/> instance into a <see cref="ReadOnlyRef{T}"/> one.
    /// </summary>
    /// <param name="reference">The input <see cref="Ref{T}"/> instance.</param>

    public static implicit operator ReadOnlyRef<T>(Ref<T> reference)
    {
        return new(in reference.Value);
    }

    /// <summary>
    /// Implicitly gets the <typeparamref name="T"/> value from a given <see cref="ReadOnlyRef{T}"/> instance.
    /// </summary>
    /// <param name="reference">The input <see cref="ReadOnlyRef{T}"/> instance.</param>

    public static implicit operator T(ReadOnlyRef<T> reference)
    {
        return reference.Value;
    }
}

#endif
