namespace Arch.SourceGen;

/// <summary>
/// Processes nothing on a line.
/// </summary>
[VariadicAlgorithm]
internal class IgnoreAlgorithm : LineAlgorithm
{
    public override string Name { get => "Ignore"; }
    public override int ExpectedParameterCount { get => 0; }

    public override string Transform(string line, string type, int lastVariadic, string[] parameters)
    {
        return line;
    }
}
