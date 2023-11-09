using System.Text.RegularExpressions;

namespace Arch.SourceGen;

/// <summary>
///     <para>
///         The <see cref="CopyParamsAlgorithm"/> is a special algorithm for more complicated method headers that can't be handled by the basic <see cref="DefaultAlgorithm"/>.
///         In particular, it handles cases of a type other than directly the variadic type, that needs to be transformed and copied. It also handles variable initializers.
///         For example, it will expand <c>ref Span&lt;T0&gt;? component_T0 = default</c> to
///         <c>ref Span&lt;T0&gt;? component_T0 = default, ref Span&lt;T1&gt;? component_T1 = default ...</c>.
///     </para>
///     <para>
///         Basically, use this algorithm if your method has any of these: Initializers (<c>= default</c>), nullables (<c>T0?</c>), or wrapped type arguments
///         (<c>MyWrapper&lt;T, T0&gt;</c>).
///     </para>
///     <para>
///         Of note, it won't expand generic parameters within arguments like the default algorithm will, which is often not desired.
///     </para>
/// </summary>
[VariadicAlgorithm]
internal class CopyParamsAlgorithm : LineAlgorithm
{
    public override int ExpectedParameterCount { get => 1; }
    public override string Name { get => "CopyParams"; }

    public override string Transform(string line, string type, int lastVariadic, string[] parameters)
    {
        // Expand the "where" constraints (like where T0 : ISomething)
        line = Utils.ExpandConstraints(line, type, lastVariadic);

        // Expand params in header (i.e. Span<T0> component_T0 -> Span<T0> component_T0, Span<T1> component_T1...
        line = Utils.ExpandTypedParameters(line, type, parameters[0], lastVariadic, out int transformedStart, out int transformedEnd);

        // Expand any generic type groups (like <A, T0>). We need to explicitly ONLY do this on the part of the header outside of the params, or Span<T0>
        // will turn into Span<T0...> which we don't want.
        // So, we exclude the transformed range from the typed parameter extension.

        var beforeParams = line.Substring(0, transformedStart);
        var @params = line.Substring(transformedStart, transformedEnd - transformedStart);
        var afterParams = line.Substring(transformedEnd, line.Length - transformedEnd);

        // Composite the parts back together
        return Utils.ExpandGenericTypes(beforeParams, type, lastVariadic) + @params
            + Utils.ExpandGenericTypes(afterParams, type, lastVariadic);
    }
}
