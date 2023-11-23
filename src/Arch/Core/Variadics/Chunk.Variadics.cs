using System.Diagnostics.Contracts;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

public partial struct Chunk
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 24)]
    [Pure]
    // [Variadic: CopyParams(int)]
    private readonly void Index<T0, T1>(out int index_T0, out int index_T1)
    {
        ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
        index_T0 = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T0>.ComponentType.Id);
        // [Variadic: CopyLines]
        index_T1 = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T1>.ComponentType.Id);
    }

    /// <inheritdoc cref="Has{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 24)]
    [SuppressMessage("Style", "IDE0011:Add braces", Justification = "Enables single-line statements")]
    [SuppressMessage("Style", "IDE2001:Embedded statements must be on their own line", Justification = "Enables single-line statements")]
    public readonly bool Has<T0, T1>()
    {
        var componentId_T0 = Component<T0>.ComponentType.Id;
        // [Variadic: CopyLines]
        var componentId_T1 = Component<T1>.ComponentType.Id;

        if (componentId_T0 >= ComponentIdToArrayIndex.Length) return false;
        // [Variadic: CopyLines]
        if (componentId_T1 >= ComponentIdToArrayIndex.Length) return false;

        if (ComponentIdToArrayIndex[componentId_T0] == -1) return false;
        // [Variadic: CopyLines]
        if (ComponentIdToArrayIndex[componentId_T1] == -1) return false;

        return true;
    }

    /// <inheritdoc cref="Get{T}(int)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 24)]
    public Components<T0, T1> Get<T0, T1>(int index)
    {
        // [Variadic: CopyArgs(array)]
        GetArray<T0, T1>(out var array_T0, out var array_T1);
        ref var component_T0 = ref array_T0[index];
        // [Variadic: CopyLines]
        ref var component_T1 = ref array_T1[index];

        // [Variadic: CopyArgs(component)]
        return new Components<T0, T1>(ref component_T0, ref component_T1);
    }

    /// <inheritdoc cref="GetRow{T}"/>
    [Variadic(nameof(T1), 24)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public EntityComponents<T0, T1> GetRow<T0, T1>(int index)
    {
        // [Variadic: CopyArgs(array)]
        GetArray<T0, T1>(out var array_T0, out var array_T1);

        ref var entity = ref Entities[index];
        ref var component_T0 = ref array_T0[index];
        // [Variadic: CopyLines]
        ref var component_T1 = ref array_T1[index];

        // [Variadic: CopyArgs(component)]
        return new EntityComponents<T0, T1>(ref entity, ref component_T0, ref component_T1);
    }

    /// <inheritdoc cref="Set{T}"/>
    [Variadic(nameof(T1), 24)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // [Variadic: CopyParams(T1?)]
    public void Set<T0, T1>(int index, in T0? component_T0, in T1? component_T1)
    {
        // [Variadic: CopyArgs(array)]
        GetArray<T0, T1>(out var array_T0, out var array_T1);

        array_T0[index] = component_T0!;
        // [Variadic: CopyLines]
        array_T1[index] = component_T1!;
    }

    /// <inheritdoc cref="GetArray{T}"/>
    [Variadic(nameof(T1), 24)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [SuppressMessage("Style", "IDE0251:Make member 'readonly'", Justification = "Not actually readonly due to unsafe get")]
    // [Variadic: CopyParams(T1[])]
    public void GetArray<T0, T1>(out T0[] array_T0, out T1[] array_T1)
    {
        // [Variadic: CopyArgs(index)]
        Index<T0, T1>(out var index_T0, out var index_T1);
        ref var arrays = ref Components.DangerousGetReference();
        array_T0 = Unsafe.As<T0[]>(Unsafe.Add(ref arrays, index_T0));

        // [Variadic: CopyLines]
        array_T1 = Unsafe.As<T1[]>(Unsafe.Add(ref arrays, index_T1));
    }

    /// <inheritdoc cref="GetSpan{T}"/>
    [Variadic(nameof(T1), 24)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    // [Variadic: CopyParams(Span<T1>)]
    public void GetSpan<T0, T1>(out Span<T0> span_T0, out Span<T1> span_T1)
    {
        // [Variadic: CopyArgs(array)]
        GetArray<T0, T1>(out var array_T0, out var array_T1);
        span_T0 = new Span<T0>(array_T0);

        // [Variadic: CopyLines]
        span_T1 = new Span<T1>(array_T1);
    }

    /// <inheritdoc cref="GetFirst{T}"/>
    [Variadic(nameof(T1), 24)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Components<T0, T1> GetFirst<T0, T1>()
    {
        // [Variadic: CopyArgs(array)]
        GetArray<T0, T1>(out var array_T0, out var array_T1);

        ref var arrayRef_T0 = ref array_T0.DangerousGetReference();
        // [Variadic: CopyLines];
        ref var arrayRef_T1 = ref array_T1.DangerousGetReference();

        // [Variadic: CopyArgs(arrayRef)]
        return new Components<T0, T1>(ref arrayRef_T0, ref arrayRef_T1);
    }
}
