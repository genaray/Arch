using System.Text.RegularExpressions;

namespace Arch.SourceGen;

/// <summary>
///     <see cref="CopyArgsAlgorithm"/> repeats a given argument of structure <c>{name}_{variadicType}</c>, separated by commas. It properly copies any ref, in, out, etc modifiers.
///     It will additionally copy generic type arguments matching the variadic type, only if the variadic type is the last in the type parameter list.
/// </summary>
/// <example>
///     With type parameter T0:
///     <code>
///         MyMethod&lt;T0&gt;(in component_T0)
///     </code>
///     ... will convert to:
///     <code>
///         MyMethod&lt;T0, T1&gt;(in component_T0, in component_T1)
///     </code>
/// </example>
[VariadicAlgorithm]
internal class CopyArgsAlgorithm : LineAlgorithm
{
    public override string Name { get => "CopyArgs"; }
    public override int ExpectedParameterCount { get => 1; }

    public override string Transform(string line, string type, int lastVariadic, string[] parameters)
    {
        // Expand the args
        line = Utils.ExpandUntypedArguments(line, type, lastVariadic, parameters[0]);

        // Expand any remaining generics
        // Note that this'll break if the user uses Span<T> instead of var or something....
        line = Utils.ExpandGenericTypes(line, type, lastVariadic);

        return line;
    }
}
