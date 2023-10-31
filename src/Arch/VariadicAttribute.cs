namespace Arch.Core;

/// <summary>
/// Tags a method or type as being variadic; i.e. generating many generic parameters.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Delegate | AttributeTargets.Interface)]
internal class VariadicAttribute : Attribute
{
    /// <summary>
    /// Tag a method or type as being variadic; i.e. generating many generic parameters.
    /// </summary>
    /// <param name="name">
    ///     The name of the type to begin. For example, if your method is <c>Add&lt;T0, T1&gt;</c>, it would be <c>nameof(T1)</c>.
    /// </param>
    /// <param name="start">
    ///     The current template after which to begin generating.
    ///     For example, if you've already defined signatures <c>T0</c> and <c>T0, T1</c>, and have the latter annotated as <c>[Variadic]</c>,
    ///     mark the current template as <c>start = 2</c>.
    /// </param>
    /// <param name="count">
    ///     The end template to generate. For example, if there should be up to <c>T0, ... T24</c> variadics, <paramref name="count"/> would be 25.
    /// </param>
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Instance params not necessary for sourcegen.")]
    public VariadicAttribute(string name, int start = 1, int count = 25) { }
}
