using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Arch.SourceGen;

/// <summary>
///     The <see cref="VariadicGenerator"/> finds assembly members with <see cref="Arch.Core.VariadicAttribute"/>, and generates variadic types.
///     See the attribute's documentation for information on usage.
/// </summary>
[Generator]
public class VariadicGenerator : IIncrementalGenerator
{
    // A note on implementation:
    // This class is implemented on a line-by-line basis making heavy use of regular expressions to parse C# code.
    // Clearly, this isn't ideal. The "correct" way of implementing something like this would be a CSharpSyntaxRewriter.
    // However, CSharpSyntaxRewriter is poorly documented and can lead to a lot of very very difficult code.
    // For now, since this is limited to Arch, it's enough to use this.

    /// <summary>
    /// Stores useful info about a member marked with <see cref="Arch.Core.VariadicAttribute"/>.
    /// </summary>
    private class VariadicInfo
    {
        /// <summary>
        /// The generic type to look for, as passed to <see cref="Arch.Core.VariadicAttribute"/>. For example, T0.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// The amount of total variadics that should be generated, as passed to <see cref="Arch.Core.VariadicAttribute"/>.
        /// </summary>
        public int Count { get; set; } = 0;

        /// <summary>
        /// How many variadics have already been specified and should be skipped during generation, as passed to <see cref="Arch.Core.VariadicAttribute"/>.
        /// </summary>
        public int Start { get; internal set; }

        /// <summary>
        /// The full code of the member the attribute is on, including any preceding documentation and attributes.
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// The name of the member the attribute is on.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The type declaration that the member is nested in, or an empty string if not nested. For example, <c>public partial class MyClass</c>
        /// </summary>
        public string EnclosingType { get; set; } = string.Empty;

        /// <summary>
        /// The name of the type declaration that the member is nested in, or an empty string if not nested. For example, <c>MyClass</c>
        /// </summary>
        public string EnclosingTypeName { get; set; } = string.Empty;

        /// <summary>
        /// The namespace the type is in.
        /// </summary>
        public string Namespace { get; set; } = string.Empty;

        /// <summary>
        /// Any usings from the file of the attribute definition.
        /// </summary>
        public string Usings { get; set; } = string.Empty;
    }

    /// <summary>
    /// Initialize the incremental generator.
    /// </summary>
    /// <param name="context"></param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Uncomment to help debug with breakpoints:
        // Debugger.Launch();

        var infos = context.SyntaxProvider.ForAttributeWithMetadataName("Arch.Core.VariadicAttribute",
            // We want to grab everything with the attribute, always!
            (node, token) => true,
            // Convert into an array of VariadicInfo objects, for each attribute
            (ctx, token) => ParseVariadicInfo(ctx))
            // Collect into a single array so we can process the whole thing in one go
            .Collect();

        context.RegisterSourceOutput(infos, GenerateVariadics);
    }

    /// <summary>
    ///     Generate all of the files from a <see cref="ImmutableArray{T}"/> of <see cref="VariadicInfo"/>.
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="infos"></param>
    private void GenerateVariadics(SourceProductionContext ctx, ImmutableArray<VariadicInfo> infos)
    {
        Dictionary<string, int> filenames = new();
        foreach (var info in infos)
        {
            // Generate the code
            var convertedCode = MakeVariadic(info);

            // Format properly
            var text = CSharpSyntaxTree.ParseText(convertedCode).GetRoot().NormalizeWhitespace().ToFullString();

            // Calculate the filename
            var filename = info.EnclosingType != string.Empty ? $"{info.EnclosingTypeName}.{info.Name}.Variadic" : $"{info.Name}.Variadic";

            // If we accidentally created a duplicate filename, we need to append an integer.
            int filenameIndex;
            if (!filenames.ContainsKey(filename))
            {
                filenames[filename] = 0;
                filenameIndex = 0;
            }
            else
            {
                filenames[filename]++;
                filenameIndex = filenames[filename];
            }

            // We add a .1, .2 etc to repeat filenames, but leave .0 off.
            string index = filenameIndex != 0 ? $".{filenameIndex}" : string.Empty;

            // Finally, add the source as well as the index
            ctx.AddSource($"{filename}{index}.g.cs", text);
        }
    }

    /// <summary>
    ///     Given a <see cref="GeneratorAttributeSyntaxContext"/>, produces a <see cref="VariadicInfo"/> object describing the method to be generated.
    /// </summary>
    /// <param name="ctx">The context.</param>
    /// <returns>A new <see cref="VariadicInfo"/> instance.</returns>
    private VariadicInfo ParseVariadicInfo(GeneratorAttributeSyntaxContext ctx)
    {
        var info = new VariadicInfo();

        // Note: doesn't support mulilevel type nesting, like struct Something { struct AnotherThing { struct AThirdThing {} } }. Only 1 nesting layer is supported.
        if (ctx.TargetSymbol.ContainingType is not null)
        {
            info.EnclosingTypeName = ctx.TargetSymbol.ContainingType.Name;
            var accessibility = ctx.TargetSymbol.ContainingType.DeclaredAccessibility switch
            {
                Accessibility.Public => "public",
                _ => "internal",
            };

            var reference = ctx.TargetSymbol.ContainingType.IsReferenceType ? "class" : "struct";

            var record = ctx.TargetSymbol.ContainingType.IsRecord ? "record" : "";

            var typeName = ctx.TargetSymbol.ContainingType.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);

            info.EnclosingType = $"{accessibility} partial {record} {reference} {typeName}";
        }

        // Loop through the namespaces to get the full namespace path
        string ns = string.Empty;
        var containingNamespace = ctx.TargetSymbol.ContainingNamespace;
        while (containingNamespace is not null && !string.IsNullOrEmpty(containingNamespace.Name))
        {
            ns = ns != string.Empty ? containingNamespace.Name + "." + ns : containingNamespace.Name;
            containingNamespace = containingNamespace.ContainingNamespace;
        }

        info.Namespace = ns;
        info.Name = ctx.TargetSymbol.Name;

        // Leading documentation (trivia) + code
        info.Code = ctx.TargetNode.GetLeadingTrivia().ToString() + ctx.TargetNode.ToString();
        foreach (var use in (ctx.TargetNode.SyntaxTree.GetRoot() as CompilationUnitSyntax)!.Usings)
        {
            info.Usings += use.ToString() + "\n";
        }

        // Grab the arguments of the variadic attribute
        foreach (var attr in ctx.TargetSymbol.GetAttributes())
        {
            if (attr.AttributeClass?.Name == "VariadicAttribute")
            {
                info.Type = attr.ConstructorArguments[0].Value as string ?? throw new InvalidOperationException();
                info.Start = Convert.ToInt32(attr.ConstructorArguments[1].Value);
                info.Count = Convert.ToInt32(attr.ConstructorArguments[2].Value);
                break;
            }
        }

        if (info.Type == string.Empty)
        {
            throw new InvalidOperationException();
        }

        return info;
    }

    // stores the algorithms from reflection
    private readonly static Dictionary<string, LineAlgorithm> _algorithms = new();

    /// <summary>
    ///     Collect any methods tagged with <see cref="VariadicAlgorithmAttribute"/> to register them as potential algorithms for the generation.
    /// process.
    /// </summary>
    static VariadicGenerator()
    {
        foreach (var type in typeof(VariadicGenerator).Assembly.GetTypes())
        {
            if (type.GetCustomAttributes(typeof(VariadicAlgorithmAttribute), true).Length > 0)
            {
                var algorithm = (LineAlgorithm)Activator.CreateInstance(type);
                if (_algorithms.ContainsKey(algorithm.Name))
                {
                    throw new InvalidOperationException($"Two {nameof(LineAlgorithm)}s cannot have the same name!");
                }

                _algorithms[algorithm.Name] = algorithm;
            }
        }
    }

    /// <summary>
    ///     Generate a full string of generated variadic methods given a <see cref="VariadicInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="VariadicInfo"/> instance with information on how to generate the code.</param>
    /// <returns>A C# string containing all the generated methods.</returns>
    private string MakeVariadic(VariadicInfo info)
    {
        // Grab a list of code tokens to help us generate
        var lines = ProcessLines(info.Code).ToList();

        var combined = new StringBuilder();

        // Run for each variadic requested, starting after Start (so if start is 1, it means we already defined 1 variadic <T0>,
        // and must start by generating <T0, T1>).
        for (var i = info.Start; i < info.Count; i++)
        {
            // Process line by line, and run the algorithm on each
            foreach (var line in lines)
            {
                if (!_algorithms.ContainsKey(line.Algorithm))
                {
                    throw new InvalidOperationException($"Algorithm {line.Algorithm} is unknown.");
                }

                // Grab our algorithm class from the reflection dictionary. If line.Algorithm is empty, it gets the default algorithm.
                var algo = _algorithms[line.Algorithm];

                // Validate algorithm params
                if (algo.ExpectedParameterCount != line.Parameters.Length)
                {
                    throw new InvalidOperationException($"Algorithm {line.Algorithm} supports only exactly {algo.ExpectedParameterCount} parameters, " +
                        $"but {line.Parameters.Length} were provided.");
                }

                // Run the transformation algorithm on the line
                combined.AppendLine(algo.Transform(line.Line, info.Type, info.Start, i + 1, line.Parameters));
            }
        }

        // If we're inside a type (like a method, for example, or an inner class), we have to put it in its parent.
        if (info.EnclosingType != string.Empty)
        {
            combined.Insert(0, info.EnclosingType + "{");
            combined.AppendLine("}");
        }

        return $$"""
            #nullable enable
            {{info.Usings}}

            namespace {{info.Namespace}};

            {{combined}}
            """;
    }

    /// <summary>
    ///     Represents a line "token" from the original code,
    /// </summary>
    private struct LineInfo
    {
        /// <summary>
        ///     The user-specified algorithm to use. For example, [Variadic: CopyLines] would use <see cref="CopyLinesAlgorithm"/>.
        ///     If blank, <see cref="DefaultAlgorithm"/> is assumed.
        /// </summary>
        public string Algorithm;

        /// <summary>
        ///     The parameters passed into the algorrithm. For example, [Variadic: CopyArgs(component)] would pass in component as
        ///     a singular param to <see cref="CopyArgsAlgorithm"/>.
        /// </summary>
        public string[] Parameters;

        /// <summary>
        ///     The unprocessed C# line, ready to go into an algorithm.
        /// </summary>
        public string Line;
    }

    /// <summary>
    ///     "Tokenize" the input code into a series of lines, assigning them processing algorithms where necessary.
    ///     Processing algorithms are determined by a comment on the preceding lines, like <c>// [Variadic: MyAlgorithm(param1...)]</c>
    /// </summary>
    /// <param name="info">The info to process the code of</param>
    /// <returns></returns>
    private IEnumerable<LineInfo> ProcessLines(string code)
    {
        var lines = code.Split('\n').Select(line => line.Trim());

        string nextAlgorithm = string.Empty;
        List<string> nextParameters = new();
        foreach (var line in lines)
        {
            if (line.StartsWith("[Variadic("))
            {
                // Don't include variadic attributes; skip over them
                continue;
            }

            // Never process documentation, just leave it as-is
            bool isDocumentation = line.StartsWith("///");

            // If it's a normal comment, check for variadic.
            // If not, discard it. If so, include it in the line token.
            if (!isDocumentation && line.StartsWith("//"))
            {
                // Match an algorithm specifier, like "// [Variadic: AlgorithmName(param1, param2...)]"
                var match = Regex.Match(line, @"\[Variadic:\s*(?<Operation>\w+)(?:\((?:(?<Variable>[?\w\[\]<>]+),?\s*)+\)\])?");
                if (!match.Success)
                {
                    // discard the comment
                    continue;
                }

                // The next line will have this algorithm!
                nextAlgorithm = match.Groups["Operation"].Value;

                // Add parameters as specified
                if (match.Groups["Variable"].Success)
                {
                    foreach (Capture capture in match.Groups["Variable"].Captures)
                    {
                        nextParameters.Add(capture.Value);
                    }
                }

                // Since it's a comment, we want to discard anyways.
                continue;
            }

            // At this point, we know that the line is a meaningful line, either documentation or code.
            var lineInfo = new LineInfo()
            {
                Line = line,
                // The algorithm, as previously specified, or if it's documentation a directive to completely ignore it!
                Algorithm = !isDocumentation ? nextAlgorithm : "Ignore",
                Parameters = !isDocumentation ? nextParameters.ToArray() : Array.Empty<string>()
            };

            // If this was meaningful code, we used up the algorithm and reset to default.
            if (!isDocumentation)
            {
                nextAlgorithm = string.Empty;
                nextParameters.Clear();
            }

            yield return lineInfo;
        }
    }
}
