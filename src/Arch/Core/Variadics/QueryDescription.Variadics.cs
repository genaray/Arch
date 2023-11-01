namespace Arch.Core;
public partial struct QueryDescription
{
    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    public ref QueryDescription WithAll<T0, T1>()
    {
        All = Group<T0, T1>.Types;
        return ref this;
    }

    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    public ref QueryDescription WithAny<T0, T1>()
    {
        Any = Group<T0, T1>.Types;
        return ref this;
    }

    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    public ref QueryDescription WithNone<T0, T1>()
    {
        None = Group<T0, T1>.Types;
        return ref this;
    }

    [UnscopedRef]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    public ref QueryDescription WithExclusive<T0, T1>()
    {
        Exclusive = Group<T0, T1>.Types;
        return ref this;
    }
}
