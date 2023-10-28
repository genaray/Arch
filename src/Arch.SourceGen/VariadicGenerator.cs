using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Arch.SourceGen;

[Generator]
public class VariadicGenerator : IIncrementalGenerator
{
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
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //if (!Debugger.IsAttached)
        //{
        //    //Debugger.Launch();
        //}

        var infos = context.SyntaxProvider.ForAttributeWithMetadataName("Arch.Core.VariadicAttribute",
            (node, token) =>
            {
                return true;
            },
            (ctx, token) =>
            {
                VariadicInfo info = new();

                // note: doesn't support mulilevel nesting
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
                info.Code = ctx.TargetNode.ToString();
                foreach (var use in (ctx.TargetNode.SyntaxTree.GetRoot() as CompilationUnitSyntax)!.Usings)
                {
                    info.Usings += use.ToString() + "\n";
                }

                // default count is 26
                info.Count = 26;
                foreach (var attr in ctx.TargetSymbol.GetAttributes())
                {
                    if (attr.AttributeClass?.Name == "VariadicAttribute")
                    {
                        info.Type = attr.ConstructorArguments[0].Value as string ?? throw new InvalidOperationException();
                        info.Count = Convert.ToInt32(attr.ConstructorArguments[1].Value);
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
            foreach (var info in infos)
            {
                var text = CSharpSyntaxTree.ParseText(MakeVariadic(info)).GetRoot().NormalizeWhitespace().ToFullString();
                if (info.EnclosingType != string.Empty)
                {
                    ctx.AddSource($"{info.EnclosingTypeName}.{info.Name}.Variadic.g.cs", text);
                }
                else
                {
                    ctx.AddSource($"{info.Name}.Variadic.g.cs", text);
                }
            }
        });
    }

    private string MakeVariadic(VariadicInfo info)
    {
        var tokens = Tokenize(info).ToList();
        StringBuilder combined = new();

        for (var i = 1; i < info.Count; i++)
        {
            foreach (var token in tokens)
            {
                combined.AppendLine(token.Transform(i, info.Type));
            }
        }

        return $$"""
            {{info.Usings}}

            namespace {{info.Namespace}};

            {{info.EnclosingType}}
            {
                {{combined}}
            }
            """;
    }

    private struct LineInfo
    {
        public LineInfo() { }
        public enum Operation
        {
            None,
            // [Variadic: CopyLines(variableName)]
            CopyLines,
            // [Variadic: CopyParams(variableName)]
            CopyParams
        }

        public Operation Op { get; set; } = Operation.None;
        public string Line { get; set; } = string.Empty;
        public string Variable { get; set; } = string.Empty;

        public readonly string Transform(int variations, string type)
        {
            if (Op == Operation.None)
            {
                // apply type constraints
                StringBuilder transformed = new();
                var constraints = Regex.Match(Line, @$"where\s+{type}\s*:\s*(?<Constraints>.*?)(?:where|{{|$)");
                if (constraints.Success)
                {
                    transformed.Append(Line.Substring(0, constraints.Index));
                    for (int i = 1; i < variations; i++)
                    {
                        transformed.Append($" where {VaryType(type, i)} : {constraints.Groups["Constraints"].Value} ");
                    }

                    transformed.Append(Line.Substring(constraints.Index, Line.Length - constraints.Index));
                }
                else
                {
                    transformed.Append(Line);
                }

                StringBuilder variadics = new();

                for (int i = 0; i < variations; i++)
                {
                    variadics.Append(VaryType(type, i));
                    if (i != variations - 1)
                    {
                        variadics.Append(", ");
                    }
                }

                variadics.Append(">");

                // apply generics: expand T0> to T0, T1...>
                transformed.Replace(type + ">", variadics.ToString());
                return transformed.ToString();
            }
            if (Op == Operation.CopyLines)
            {
                StringBuilder transformed = new();
                transformed.AppendLine(Line);

                for (int i = 1; i < variations; i++)
                {
                    StringBuilder next = new();
                    next.AppendLine(Line);
                    var variadic = VaryType(type, i);
                    next.Replace(Variable + "__" + type, Variable + "__" + variadic);
                    next.Replace(type, variadic);
                    transformed.AppendLine(next.ToString());
                }
                return transformed.ToString();
            }
            if (Op == Operation.CopyParams)
            {
                StringBuilder transformed = new();
                transformed.AppendLine(Line);
                StringBuilder newVariables = new();
                newVariables.Append(Variable + "__" + type);
                for (int i = 1; i < variations; i++)
                {
                    var variadic = VaryType(type, i);
                    newVariables.Append(", " + Variable + "__ " + variadic);
                }

                transformed.Replace(Variable + "__" + type, newVariables.ToString());
                return transformed.ToString();
            }

            throw new NotImplementedException();
        }
    }

    private static string VaryType(string typeName, int i)
    {
        var match = Regex.Match(typeName, @"(?<PrunedName>\w+)0");
        if (!match.Success)
        {
            throw new InvalidOperationException("Variadic type must be of TypeName0");
        }
        return $"{match.Groups["PrunedName"]}{i}";
    }

    private IEnumerable<LineInfo> Tokenize(VariadicInfo info)
    {
        // NOTE: does not support multiline operations, for example this would break:
        //  // [Variadic: CopyParams(a)]
        //  MyMethod(
        //      a_T0);
        var lines = SplitLines(info.Code);

        LineInfo.Operation nextOperation = LineInfo.Operation.None;
        string nextVariable = string.Empty;
        foreach (var line in lines)
        {
            if (line.StartsWith("//"))
            {
                var match = Regex.Match(line, @"\[Variadic:\s*(?<Operation>\w+)\((?<Variable>\w+)\)\]");
                if (!match.Success)
                {
                    continue;
                }

                nextOperation = match.Groups["Operation"].Value switch
                {
                    "CopyLines" => LineInfo.Operation.CopyLines,
                    "CopyParams" => LineInfo.Operation.CopyParams,
                    _ => throw new NotImplementedException()
                };
                nextVariable = match.Groups["Variable"].Value;

                continue;
            }

            var lineInfo = new LineInfo()
            {
                Line = line,
                Variable = nextVariable,
                Op = nextOperation
            };
            nextVariable = string.Empty;
            nextOperation = LineInfo.Operation.None;
            yield return lineInfo;
        }
    }

    private IEnumerable<string> SplitLines(string code)
    {
        return code.Split('\n').Select(line => line.Trim());
    }
}
