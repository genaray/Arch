namespace Arch.Core;

// NOTE: we should consider making this internal, if it's possible to expose internals to Arch.Extended
/// <summary>
/// Tags a class as containing extensions for <see cref="World"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class WorldExtensionsAttribute : Attribute
{
}
