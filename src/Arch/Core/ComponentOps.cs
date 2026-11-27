using System.Threading;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

/// <summary>
///     The <see cref="ComponentOps"/> class
///     creates component arrays and moves single elements within them without reflection and without the overhead of <see cref="Array.Copy(Array,int,Array,int,int)"/>.
///     Typed instances are registered by <see cref="Component{T}"/>, components that were only registered by their <see cref="Type"/> fall back to untyped operations.
/// </summary>
internal abstract class ComponentOps
{
    private static readonly object RegistrationLock = new();
    private static ComponentOps?[] _registered = new ComponentOps?[64];

    /// <summary>
    ///     The type of the components this instance operates on.
    /// </summary>
    internal abstract Type ElementType { get; }

    /// <summary>
    ///     Registers the <see cref="ComponentOps{T}"/> of a component, readers never take the lock.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> of <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The component type.</typeparam>
    internal static void Register<T>(ComponentType type)
    {
        lock (RegistrationLock)
        {
            if (type.Id >= _registered.Length)
            {
                var resized = new ComponentOps?[Math.Max(type.Id + 1, _registered.Length * 2)];
                _registered.CopyTo(resized, 0);
                Volatile.Write(ref _registered, resized);
            }

            _registered[type.Id] = ComponentOps<T>.Instance;
        }
    }

    /// <summary>
    ///     Returns the <see cref="ComponentOps"/> of a component, an untyped one if its type was not registered.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/>.</param>
    /// <returns>The <see cref="ComponentOps"/>.</returns>
    internal static ComponentOps Get(ComponentType type)
    {
        var registered = Volatile.Read(ref _registered);
        var ops = (uint)type.Id < (uint)registered.Length ? registered[type.Id] : null;

        // The id might have been replaced by another type.
        // Components that were never used with a generic Component<T> get a new untyped instance per call, so callers keep it, like the Chunk does.
        return ops is not null && ops.ElementType == type.Type ? ops : new UntypedComponentOps(type.Type);
    }

    /// <summary>
    ///     Returns the <see cref="ComponentOps"/> of several components, at the same index as the component.
    /// </summary>
    /// <param name="types">The <see cref="ComponentType"/>s.</param>
    /// <returns>The <see cref="ComponentOps"/>.</returns>
    internal static ComponentOps[] Get(Span<ComponentType> types)
    {
        var ops = new ComponentOps[types.Length];
        for (var index = 0; index < types.Length; index++)
        {
            ops[index] = Get(types[index]);
        }

        return ops;
    }

    /// <summary>
    ///     Creates a component array.
    /// </summary>
    /// <param name="capacity">Its length.</param>
    /// <returns>The array.</returns>
    internal abstract Array Create(int capacity);

    /// <summary>
    ///     Copies one component.
    /// </summary>
    internal abstract void Copy(Array source, int sourceIndex, Array destination, int destinationIndex);

    /// <summary>
    ///     Copies one component and clears the source if it holds references, so it does not keep them alive.
    ///     The source and the destination may be the same slot.
    /// </summary>
    internal abstract void Move(Array source, int sourceIndex, Array destination, int destinationIndex);

    /// <summary>
    ///     Returns one component as an <see cref="object"/>, instead of <see cref="Array.GetValue(int)"/>.
    /// </summary>
    internal abstract object? GetBoxed(Array array, int index);

    /// <summary>
    ///     Sets one component from an <see cref="object"/>, instead of <see cref="Array.SetValue(object, int)"/>.
    /// </summary>
    internal abstract void SetBoxed(Array array, int index, object value);
}

/// <summary>
///     The <see cref="ComponentOps{T}"/> class
///     is the typed <see cref="ComponentOps"/> of a component, it assumes all arrays it gets are <typeparamref name="T"/>[].
/// </summary>
/// <typeparam name="T">The component type.</typeparam>
internal sealed class ComponentOps<T> : ComponentOps
{
    internal static readonly ComponentOps<T> Instance = new();

    internal override Type ElementType
    {
        get { return typeof(T); }
    }

    internal override Array Create(int capacity)
    {
        return capacity == 0 ? Array.Empty<T>() : new T[capacity];
    }

    internal override void Copy(Array source, int sourceIndex, Array destination, int destinationIndex)
    {
        Unsafe.As<T[]>(destination).DangerousGetReferenceAt(destinationIndex) = Unsafe.As<T[]>(source).DangerousGetReferenceAt(sourceIndex);
    }

    internal override void Move(Array source, int sourceIndex, Array destination, int destinationIndex)
    {
        ref var item = ref Unsafe.As<T[]>(source).DangerousGetReferenceAt(sourceIndex);
        Unsafe.As<T[]>(destination).DangerousGetReferenceAt(destinationIndex) = item;

        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            item = default!;
        }
    }

    internal override object? GetBoxed(Array array, int index)
    {
        return Unsafe.As<T[]>(array).DangerousGetReferenceAt(index);
    }

    internal override void SetBoxed(Array array, int index, object value)
    {
        Unsafe.As<T[]>(array).DangerousGetReferenceAt(index) = (T)value;
    }
}

/// <summary>
///     The <see cref="UntypedComponentOps"/> class
///     is the <see cref="ComponentOps"/> for components that are only known by their <see cref="Type"/> at runtime.
/// </summary>
internal sealed class UntypedComponentOps : ComponentOps
{
    private readonly Type _type;

    internal UntypedComponentOps(Type type)
    {
        _type = type;
    }

    internal override Type ElementType
    {
        get { return _type; }
    }

    internal override Array Create(int capacity)
    {
        return Array.CreateInstance(_type, capacity);
    }

    internal override void Copy(Array source, int sourceIndex, Array destination, int destinationIndex)
    {
        Array.Copy(source, sourceIndex, destination, destinationIndex, 1);
    }

    internal override void Move(Array source, int sourceIndex, Array destination, int destinationIndex)
    {
        Array.Copy(source, sourceIndex, destination, destinationIndex, 1);
        Array.Clear(source, sourceIndex, 1);
    }

    internal override object? GetBoxed(Array array, int index)
    {
        return array.GetValue(index);
    }

    internal override void SetBoxed(Array array, int index, object value)
    {
        array.SetValue(value, index);
    }
}
