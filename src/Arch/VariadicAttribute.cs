namespace Arch.Core;

#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
/// <summary>
///     Tags a method or type as being variadic; i.e. generating many versions with any number of generic parameters.
/// </summary>
/// <remarks>
///     <para>
///         See <see cref="Arch.SourceGen.DefaultAlgorithm"/> for the default behavior.
///     </para>
///     <para>
///         Frequently, the default behavior of the generator will be inadequate. In those cases, you can precede an offending line with
///         "// [Variadic: AlgorithmName(...algorithmParams)]". Currently, the available algorithms are <see cref="Arch.SourceGen.CopyParamsAlgorithm"/> (for SOME method headers
///         with special params), <see cref="Arch.SourceGen.CopyLinesAlgorithm"/> (to simply copy a line for as many variadics are generating), and
///         <see cref="Arch.SourceGen.CopyArgsAlgorithm"/> (to insert a variable into a method call's arguments.). See their respective documentation for their usage information.
///     </para>
///     <para>
///         For examples, look at the existing implementations.
///     </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Delegate | AttributeTargets.Interface)]
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
internal class VariadicAttribute : Attribute
{
    /// <summary>
    ///     Tag a method or type as being variadic; i.e. generating many generic parameters.
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
