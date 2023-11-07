namespace Arch.Core;

/// <summary>
///     The <see cref="ForEach"/> delegate
///     provides a callback to execute logic on up to 25 component types.
/// </summary>
[Variadic(nameof(T0), 1, 25)]
public delegate void ForEach<T0>(ref T0 component_T0);

/// <summary>
///     The <see cref="ForEach"/> delegate
///     provides a callback to execute logic on an <see cref="Entity"/> and up to 25 component types.
/// </summary>
[Variadic(nameof(T0), 1, 25)]
public delegate void ForEachWithEntity<T0>(Entity entity, ref T0 component_T0);
