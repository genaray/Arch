using System.Text.RegularExpressions;

namespace Arch.SourceGen;

/// <summary>
///     <para>
///         The <see cref="DefaultAlgorithm"/> runs by default on any lines where a variadic comment like "// [Variadic: MethodName(param1...)]" is NOT present.
///     </para>
///     <para>
///         The behavior of the algorithm is as such:
///     </para>
///     <para>
///         Anywhere in the string, if there is a type sequence ending in the specified variadic type, it fills in the variadics. For example, <c>&lt;T, T0&gt;</c> will
///         expand to <c>&lt;T, T0, T1...&gt;</c>. The generic sequence must end in the specified variadic type, otherwise nothing will be changed.
///     </para>
///     <para>
///         If any type constraints are found with the specified variadic type, it copies the constraints to each new type. For example, with type <c>T0</c>,
///         <c>where T0 : struct, new()</c> will expand to <c>where T0 : struct, new() where T1 : struct, new() ...</c>. Of note, nested constraints of the variadic
///         types are not currently processed correctly, i.e. <c>where T0 : ISomething&lt;T0&gt;</c>. If that behavior is needed, either edit this algorithm or introduce a new one.
///     </para>
///     <para>
///         For method headers, it copies simple type parameters of the given variadic type, with the form <c>{paramName}_{type}</c>. For example, <c>in T0 component_T0</c>
///         would expand to <c>in T0 component_T0, in T1 component_T1</c>. Note that this algorithm isn't smart enough to recognize wrapped parameters, such as
///         <c>in Span&lt;T0&gt; component_T0</c>; it will expand it to an invalid <c>Span&lt;T0, T1, ...&gt;</c>. To use the former behavior, or if initializers like
///         <c>component_T0 = default</c> is needed, or nullables are needed, specify <see cref="CopyParamsAlgorithm"/> instead with an explicit type to copy.
///     </para>
///     <para>
///         Nothing else is changed.
///     </para>
/// </summary>
[VariadicAlgorithm]
internal class DefaultAlgorithm : LineAlgorithm
{
    public override int ExpectedParameterCount { get => 0; }
    public override string Name { get => string.Empty; }
    public override string Transform(string line, string type, int start, int variations, string[] parameters)
    {
        var transformed = new StringBuilder();

        // copy type constraints for our selected variadic
        var constraints = Regex.Match(line, @$"where\s+{type}\s*:\s*(?<Constraints>.*?)(?:where|{{|$)");
        if (constraints.Success)
        {
            // append anything prior to the original constraint
            transformed.Append(line.Substring(0, constraints.Index));

            // append extra constraints as needed
            for (int i = 1; i < variations; i++)
            {
                transformed.Append($" where {VaryType(type, i)} : {constraints.Groups["Constraints"].Value} ");
            }

            // add in the rest of the line, plus the original constraint
            transformed.Append(line.Substring(constraints.Index, line.Length - constraints.Index));
        }
        else
        {
            // no constraints, just add the line
            transformed.Append(line);
        }

        // build a string like "T0, T1, ...>"
        var variadics = new StringBuilder();
        for (int i = start - 1; i < variations; i++)
        {
            variadics.Append(VaryType(type, i));
            if (i != variations - 1)
            {
                variadics.Append(", ");
            }
        }

        variadics.Append(">");

        // Apply generics: expand T0> -> T0, T1...>
        // Applied everywhere, wherever generics appear!
        transformed.Replace(type + ">", variadics.ToString());

        // Expand params in header (i.e. T0 component_T0 -> T0 component_T0, T1 component_T1...
        // This is the 90% case. Occasionally there needs to be special handling for a method header.
        // Those cases are handled by CopyParamsAlgorithm
        // Modifiers is ref, in, out etc.
        // ParamName is the paramname we need to copy,
        var exp = $@"[(,]\s*(?<Modifiers>(?:in|out|ref|ref\s+readonly)\s+)?{type}\s+(?<ParamName>\w+)_{type}";
        var paramMatch = Regex.Match(transformed.ToString(), exp);
        if (paramMatch.Success)
        {
            var name = paramMatch.Groups["ParamName"].Value;
            var modifiers = paramMatch.Groups["Modifiers"].Value;

            var newParams = new StringBuilder();
            for (int i = start - 1; i < variations; i++)
            {
                var varied = VaryType(type, i);
                newParams.Append($"{modifiers} {varied} {name}_{varied}");
                if (i != variations - 1)
                {
                    newParams.Append(", ");
                }
            }

            transformed.Remove(paramMatch.Index + 1, paramMatch.Length - 1);
            transformed.Insert(paramMatch.Index + 1, newParams);
        }

        return transformed.ToString();

    }
}
