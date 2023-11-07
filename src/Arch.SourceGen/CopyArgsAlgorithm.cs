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

    public override string Transform(string line, string type, int start, int variations, string[] parameters)
    {
        StringBuilder transformed = new();
        transformed.AppendLine(line);
        StringBuilder newVariables = new();

        // match ref, in, out
        var modifiersMatch = Regex.Match(line, $@"[(,]\s*(?<Modifiers>(ref|out ref|out var|out {type}\??|in|ref {type}\??))?\s*{parameters[0]}_{type}");
        if (!modifiersMatch.Success)
        {
            throw new InvalidOperationException($"Can't find variable {parameters[0]}_{type} in a parameter list.");
        }

        var modifiers = modifiersMatch.Groups["Modifiers"].Value;

        newVariables.Append($"{modifiers} {parameters[0]}_{type}");
        for (int i = start; i < variations; i++)
        {
            var variadic = VaryType(type, i);
            newVariables.Append($", {modifiers} {parameters[0]}_{variadic}");
        }

        transformed.Remove(modifiersMatch.Index + 1, modifiersMatch.Length - 1);
        transformed.Insert(modifiersMatch.Index + 1, newVariables.ToString());

        // expand any remaining generics
        // note that this'll break if the user uses Span<T> instead of var or something....
        StringBuilder variadics = new();
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
        transformed.Replace(type + ">", variadics.ToString());

        return transformed.ToString();
    }
}
