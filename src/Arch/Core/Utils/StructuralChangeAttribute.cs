namespace Arch.Core;

/// <summary>
///     Marks a particular public method on a <see cref="World"/> as causing a structural change.
///     Structural changes must never be invoked as another thread is accessing the <see cref="World"/> in any way.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class StructuralChangeAttribute : Attribute
{
}
