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
    /// <param name="lastVariadic">The last variadic to generate. If 1, for example, would generate T0, T1.</param>
    /// <param name="parameters">The parameters provided to the variadic comment, if any.</param>
    /// <returns>The transformed string according to the algorithm.</returns>
    public abstract string Transform(string line, string type, int lastVariadic, string[] parameters);
}
