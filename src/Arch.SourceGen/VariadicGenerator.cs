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

    private class VariadicInfo
    {
        public string Type { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string EnclosingType { get; set; } = string.Empty;
        public string EnclosingTypeName { get; set; } = string.Empty;
        public string Namespace { get; set; } = string.Empty;
        public string Usings { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
        public int Start { get; internal set; }
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Uncomment to help debug with breakpoints:
        // Debugger.Launch();

        var infos = context.SyntaxProvider.ForAttributeWithMetadataName("Arch.Core.VariadicAttribute",
            // We want to grab everything with the attribute, always!
            (node, token) => true,
            (ctx, token) =>
            {
                VariadicInfo info = new();

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

                    info.EnclosingType = $"{accessibility} partial {reference} {ctx.TargetSymbol.ContainingType.Name}";
                }

                string ns = string.Empty;
                var containingNamespace = ctx.TargetSymbol.ContainingNamespace;
                while (containingNamespace is not null && !string.IsNullOrEmpty(containingNamespace.Name))
                {
                    ns = ns != string.Empty ? containingNamespace.Name + "." + ns : containingNamespace.Name;
                    containingNamespace = containingNamespace.ContainingNamespace;
                }

                info.Namespace = ns;
                info.Name = ctx.TargetSymbol.Name;
                info.Code = ctx.TargetNode.GetLeadingTrivia().ToString() + ctx.TargetNode.ToString();
                foreach (var use in (ctx.TargetNode.SyntaxTree.GetRoot() as CompilationUnitSyntax)!.Usings)
                {
                    info.Usings += use.ToString() + "\n";
                }

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
            }).Collect();

        context.RegisterSourceOutput(infos, (ctx, infos) =>
        {
            Dictionary<string, int> filenames = new();
            foreach (var info in infos)
            {
                var text = CSharpSyntaxTree.ParseText(MakeVariadic(info)).GetRoot().NormalizeWhitespace().ToFullString();
                var filename = info.EnclosingType != string.Empty ? $"{info.EnclosingTypeName}.{info.Name}.Variadic" : $"{info.Name}.Variadic";
                // if we accidentally created a duplicate filename, we need to append an integer.
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

                string index = filenameIndex != 0 ? $".{filenameIndex}" : string.Empty;
                ctx.AddSource($"{filename}{index}.g.cs", text);
            }
        });
    }

    // stores the algorithms from reflection
    private readonly static Dictionary<string, LineAlgorithm> _algorithms = new();

    // collect the algorithms available with reflection on the attribute
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

    private string MakeVariadic(VariadicInfo info)
    {
        var lines = ProcessLines(info).ToList();
        StringBuilder combined = new();

        for (var i = info.Start; i < info.Count; i++)
        {
            foreach (var line in lines)
            {
                if (!_algorithms.ContainsKey(line.Algorithm))
                {
                    throw new InvalidOperationException($"Algorithm {line.Algorithm} is unknown.");
                }

                var algo = _algorithms[line.Algorithm];
                if (algo.ExpectedParameterCount != line.Parameters.Length)
                {
                    throw new InvalidOperationException($"Algorithm {line.Algorithm} supports only exactly {algo.ExpectedParameterCount} parameters, " +
                        $"but {line.Parameters.Length} were provided.");
                }

                combined.AppendLine(algo.Transform(line.Line, info.Type, info.Start, i + 1, line.Parameters));
            }
        }

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

    private struct LineInfo
    {
        public string Algorithm;
        public string Line;
        public string[] Parameters;
    }

    private IEnumerable<LineInfo> ProcessLines(VariadicInfo info)
    {
        var lines = SplitLines(info.Code);

        string nextAlgorithm = string.Empty;
        List<string> nextParameters = new();
        foreach (var line in lines)
        {
            if (line.StartsWith("[Variadic("))
            {
                // don't include variadic attrs
                continue;
            }

            bool isDocumentation = false;
            // never process documentation
            if (line.StartsWith("///"))
            {
                isDocumentation = true;
            }

            else if (line.StartsWith("//"))
            {
                // match algorithm, like "// [Variadic: AlgorithmName(param1, param2...)]"
                var match = Regex.Match(line, @"\[Variadic:\s*(?<Operation>\w+)(?:\((?:(?<Variable>[?\w\[\]<>]+),?\s*)+\)\])?");
                if (!match.Success)
                {
                    continue;
                }

                nextAlgorithm = match.Groups["Operation"].Value;

                if (match.Groups["Variable"].Success)
                {
                    foreach (Capture capture in match.Groups["Variable"].Captures)
                    {
                        nextParameters.Add(capture.Value);
                    }
                }

                continue;
            }

            var lineInfo = new LineInfo()
            {
                Line = line,
                Algorithm = !isDocumentation ? nextAlgorithm : "Ignore",
                Parameters = !isDocumentation ? nextParameters.ToArray() : Array.Empty<string>()
            };
            if (!isDocumentation)
            {
                nextAlgorithm = string.Empty;
                nextParameters.Clear();
            }

            yield return lineInfo;
        }
    }

    private IEnumerable<string> SplitLines(string code)
    {
        return code.Split('\n').Select(line => line.Trim());
    }
}
