namespace Arch.SourceGen;

/// <summary>
///     <see cref="CopyLinesAlgorithm"/> is a very simple algorithm that will simply repeat the line, replacing any occurances of the variadic type with its variant for each iteration.
/// </summary>
/// <example>
///     With type parameter T0:
///     <code>
///         /// [Variadic: CopyLines]
///         somethingSomething_T0 = new T0(); // look! T0!
///     </code>
///     ... will expand to:
///     <code>
///         somethingSomething_T0 = new T0(); // look! T0!
///         somethingSomething_T1 = new T1(); // look! T1!
///         ...
///     </code>
/// </example>
[VariadicAlgorithm]
internal class CopyLinesAlgorithm : LineAlgorithm
{
    public override string Name { get => "CopyLines"; }
    public override int ExpectedParameterCount { get => 0; }

    public override string Transform(string line, string type, int start, int variations, string[] parameters)
    {
        var transformed = new StringBuilder();
        transformed.AppendLine(line);

        for (int i = start; i < variations; i++)
        {
            var next = new StringBuilder();
            next.AppendLine(line);
            var variadic = VaryType(type, i);
            next.Replace(type, variadic);
            transformed.AppendLine(next.ToString());
        }

        return transformed.ToString();
    }
}
