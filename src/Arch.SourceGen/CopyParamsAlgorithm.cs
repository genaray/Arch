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

    public override string Transform(string line, string type, int start, int variations, string[] parameters)
    {
        // This is a special algorithm denotion for method headers.
        // This grabs the first param that matches the passed in type (in Variables[0]), like "Span<T0>? paramName_T0 = default"
        // it extracts paramName, the initializer if it exists, any ref/out/in modifiers, and repeats it according to the type params.
        var headerMatch = Regex.Match(line, $@"[(,]\s*(?<Modifiers>(ref|out|ref readonly|in)\s+)?{Regex.Escape(parameters[0])}\s+(?<ParamName>\w+)_{type}\s*(?<Assignment>=\s*default)?");

        if (!headerMatch.Success)
        {
            throw new InvalidOperationException($"Malformed method header for {nameof(CopyParamsAlgorithm)}.");
        }

        bool hasDefault = headerMatch.Groups["Assignment"].Success;
        string modifiers = headerMatch.Groups["Modifiers"].Success ? headerMatch.Groups["Modifiers"].Value : string.Empty;
        string param = headerMatch.Groups["ParamName"].Value;

        List<string> additionalParams = new();
        for (int i = start; i < variations; i++)
        {
            var variadic = VaryType(type, i);
            // One issue with this approach... if we had a wrapper type of SomethingT1<T1> (which is bad name but whatever) then this would break.
            // but so far we don't have anything like that.
            var fullType = parameters[0].Replace(type, variadic);
            additionalParams.Add($"{modifiers} {fullType} {param}_{variadic} {(hasDefault ? "= default" : "")}");
        }

        var transformed = new StringBuilder();
        transformed.Append(line);

        var @params = new StringBuilder();
        // +1 to exclude the opening parenthesis or comma
        @params.Append(line.Substring(headerMatch.Index + 1, headerMatch.Length - 1));
        foreach (var additionalParam in additionalParams)
        {
            @params.Append(", " + additionalParam);
        }

        // For the rest of the line, we do a trick: we want to apply the regular op on the rest of the header, so we remove all args and re-add them after.
        // We do this so we don't have to process constraints/generics separately.

        // Remove all the args we just added
        transformed.Remove(headerMatch.Index + 1, headerMatch.Length - 1);
        var lineWithoutArgs = transformed.ToString();
        var defaultAlgorithm = new DefaultAlgorithm();
        transformed.Clear();
        // Do a default transform on everything but the args
        transformed.Append(defaultAlgorithm.Transform(lineWithoutArgs, type, start, variations, Array.Empty<string>()));

        // Find where we left off; we either left a hanging comma or an ()
        var match = Regex.Match(transformed.ToString(), @"(,\s*\)|\(\s*\))");
        Debug.Assert(match.Success);
        // Stick our params back in there
        transformed.Insert(match.Index + 1, @params);

        return transformed.ToString();
    }
}
