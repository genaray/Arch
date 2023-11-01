using System.Text.RegularExpressions;

namespace Arch.SourceGen;

/// <summary>
/// Base class for an algorithm that processes a given line.
/// Tag subclasses with <see cref="VariadicAlgorithmAttribute"/> to automatically add them to the generator.
/// </summary>
internal abstract class LineAlgorithm
{
    /// <summary>
    /// The name of the algorithm, as specified in the marking comment.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    ///     The exact count of parameters to require for <see cref="Transform"/>.
    /// </summary>
    public abstract int ExpectedParameterCount { get; }

    /// <summary>
    ///     Transform a string based on the algorithm's behavior.
    /// </summary>
    /// <param name="line">The input line.</param>
    /// <param name="type">The variadic type provided in the variadic attribute, e.g. <c>T0</c></param>
    /// <param name="start">The first extra variadic to generate, e.g. `2` if one variadic is provided in the template method.</param>
    /// <param name="variations">How many variadics to generate. This will be called with various numbers of variations to generate the full variadic spectrum.</param>
    /// <param name="parameters">The parameters provided to the variadic comment, if any.</param>
    /// <returns>The transformed string according to the algorithm.</returns>
    public abstract string Transform(string line, string type, int start, int variations, string[] parameters);

    protected static string VaryType(string typeName, int i)
    {
        var match = Regex.Match(typeName, @"(?<PrunedName>\w+)[0-9]+");
        if (!match.Success)
        {
            throw new InvalidOperationException("Variadic type must be of TypeName{N}");
        }

        return $"{match.Groups["PrunedName"]}{i}";
    }
}
