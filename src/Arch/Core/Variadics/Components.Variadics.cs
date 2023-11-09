using CommunityToolkit.HighPerformance;

namespace Arch.Core;

/// <summary>
///     Stores a reference group of up to 25 components.
/// </summary>
[SkipLocalsInit]
[Variadic(nameof(T0), 24)]
public ref struct Components<T0>
{
    /// <summary>
    ///     A component.
    /// </summary>
#if NETSTANDARD2_1 || NET6_0
    // [Variadic: CopyLines]
    public Ref<T0> Component_T0;
#else
    // [Variadic: CopyLines]
    public ref T0 Component_T0;
#endif

    /// <summary>
    ///     Creates a new instance of the <see cref="Components{T0}"/> struct.
    /// </summary>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Components(ref T0 component_T0)
    {
#if NETSTANDARD2_1 || NET6_0
        // [Variadic: CopyLines]
        Component_T0 = new Ref<T0>(ref component_T0);
#else
        // [Variadic: CopyLines]
        Component_T0 = ref component_T0;
#endif
    }
}

/// <summary>
///     Stores a reference group of up to 25 components, as well as an entity.
/// </summary>
[SkipLocalsInit]
[Variadic(nameof(T0), 24)]
public ref struct EntityComponents<T0>
{

#if NETSTANDARD2_1 || NET6_0
    public ReadOnlyRef<Entity> Entity;
    // [Variadic: CopyLines]
    public Ref<T0> Component_T0;
#else
    public ref readonly Entity Entity;
    // [Variadic: CopyLines]
    public ref T0 Component_T0;
#endif

    /// <summary>
    ///     Creates a new instance of the <see cref="EntityComponents{T0}"/> struct.
    /// </summary>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntityComponents(ref Entity entity, ref T0 component_T0)
    {
#if NETSTANDARD2_1 || NET6_0
        Entity = new ReadOnlyRef<Entity>(in entity);
        // [Variadic: CopyLines]
        Component_T0 = new Ref<T0>(ref component_T0);
#else
        Entity = ref entity;
        // [Variadic: CopyLines]
        Component_T0 = ref component_T0;
#endif
    }
}
