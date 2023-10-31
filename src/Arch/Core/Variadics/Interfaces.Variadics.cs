namespace Arch.Core;

/// <inheritdoc cref="IForEach"/>
[Variadic(nameof(T0), 1, 26)] // TODO change this to 25???
public interface IForEach<T0>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Update(ref T0 component__T0);
}

/// <inheritdoc cref="IForEach"/>
[Variadic(nameof(T0), 1, 26)]
public interface IForEachWithEntity<T0>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Update(Entity entity, ref T0 component__T0);
}
