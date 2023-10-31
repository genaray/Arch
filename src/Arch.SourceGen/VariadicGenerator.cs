using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

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
        public int Start { get; internal set; }
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        //Debugger.Launch();
        var infos = context.SyntaxProvider.ForAttributeWithMetadataName("Arch.Core.VariadicAttribute",
            // TODO: slow, make better filter
            (node, token) => true,
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

        for (var i = info.Start; i < info.Count; i++)
        {
            foreach (var token in tokens)
            {
                combined.AppendLine(token.Transform(info.Start, i + 1, info.Type));
            }
        }

        if (info.EnclosingType != string.Empty)
        {
            combined.Insert(0, info.EnclosingType + "{");
            combined.AppendLine("}");
        }

        return $$"""
            {{info.Usings}}

            namespace {{info.Namespace}};

            {{combined}}
            """;
    }

    private struct LineInfo
    {
        public LineInfo() { }
        public enum Operation
        {
            None,
            // [Variadic: CopyLines]
            CopyLines,
            // [Variadic: CopyArgs(variableName)]
            CopyArgs,
            // [Variadic: CopyParams(enclosingTypeName)]
            // necessary for params like Span<T0> span__T0...
            // or nullables, or defaults.
            // But the default behavior Operation.None works for most methods.
            CopyParams
        }

        public Operation Op { get; set; } = Operation.None;
        public string Line { get; set; } = string.Empty;
        public string[] Variables { get; set; } = Array.Empty<string>();

        public readonly string Transform(int start, int variations, string type)
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

                // Expand params in header (i.e. T0 component__T0 -> T0 component__T0, T1 component__t1...
                // This is the 90% case. Occasionally there needs to be special handling for a method header.
                // Those cases are handled by Operation.CopyParams
                var exp = $@"[(,]\s*(?<Modifiers>(?:in|out|ref|ref\s+readonly)\s+)?{type}\s+(?<ParamName>\w+)__{type}";
                var paramMatch = Regex.Match(transformed.ToString(), exp);
                if (paramMatch.Success)
                {
                    var name = paramMatch.Groups["ParamName"].Value;
                    var modifiers = paramMatch.Groups["Modifiers"].Value;

                    StringBuilder newParams = new();
                    for (int i = start - 1; i < variations; i++)
                    {
                        var varied = VaryType(type, i);
                        newParams.Append($"{modifiers} {varied} {name}__{varied}");
                        if (i !=  variations - 1)
                        {
                            newParams.Append(", ");
                        }
                    }

                    transformed.Remove(paramMatch.Index + 1, paramMatch.Length - 1);
                    transformed.Insert(paramMatch.Index + 1, newParams);
                }

                return transformed.ToString();
            }

            if (Op == Operation.CopyLines)
            {
                if (Variables.Length != 0)
                {
                    throw new InvalidOperationException($"{nameof(Operation.CopyLines)} does not support variables.");
                }

                StringBuilder transformed = new();
                transformed.AppendLine(Line);

                for (int i = start; i < variations; i++)
                {
                    StringBuilder next = new();
                    next.AppendLine(Line);
                    var variadic = VaryType(type, i);
                    next.Replace(type, variadic);
                    transformed.AppendLine(next.ToString());
                }

                return transformed.ToString();
            }

            if (Op == Operation.CopyArgs)
            {
                if (Variables.Length != 1)
                {
                    throw new InvalidOperationException($"{nameof(Operation.CopyArgs)} only supports 1 variable.");
                }

                StringBuilder transformed = new();
                transformed.AppendLine(Line);
                StringBuilder newVariables = new();

                // match ref, in, out
                var modifiersMatch = Regex.Match(Line, $@"[(,]\s*(?<Modifiers>(ref|out ref|out var|out {type}\??|in|ref {type}\??))?\s*{Variables[0]}__{type}");
                if (!modifiersMatch.Success)
                {
                    throw new InvalidOperationException($"Can't find variable {Variables[0]}__{type} in a parameter list.");
                }

                var modifiers = modifiersMatch.Groups["Modifiers"].Value;

                newVariables.Append($"{modifiers} {Variables[0]}__{type}");
                for (int i = start; i < variations; i++)
                {
                    var variadic = VaryType(type, i);
                    newVariables.Append($", {modifiers} {Variables[0]}__{variadic}");
                }

                transformed.Remove(modifiersMatch.Index + 1, modifiersMatch.Length - 1);
                transformed.Insert(modifiersMatch.Index + 1, newVariables.ToString());

                // expand any remaining generics
                // note that this'll break if the user uses Span<T> instead of var or something.
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

            if (Op == Operation.CopyParams)
            {
                if (Variables.Length != 1)
                {
                    throw new InvalidOperationException($"{nameof(Operation.CopyParams)} only supports 1 type variable.");
                }

                // This is a special algorithm denotion for method headers.
                // This grabs the first param that matches the passed in type (in Variables[0]), like "Span<T0>? paramName__T0 = default"
                // it extracts paramName, the initializer if it exists, any ref/out/in modifiers, and repeats it according to the type params.
                var headerMatch = Regex.Match(Line, $@"[(,]\s*(?<Modifiers>(ref|out|ref readonly|in)\s+)?{Regex.Escape(Variables[0])}\s+(?<ParamName>\w+)__{type}\s*(?<Assignment>=\s*default)?");

                if (!headerMatch.Success)
                {
                    throw new InvalidOperationException($"Malformed method header for {nameof(Operation.CopyParams)}.");
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
                    var fullType = Variables[0].Replace(type, variadic);
                    additionalParams.Add($"{modifiers} {fullType} {param}__{variadic} {(hasDefault ? "= default" : "")}");
                }

                StringBuilder transformed = new();
                transformed.Append(Line);

                StringBuilder @params = new();
                // +1 to exclude the opening parenthesis or comma
                @params.Append(Line.Substring(headerMatch.Index + 1, headerMatch.Length - 1));
                foreach (var additionalParam in additionalParams)
                {
                    @params.Append(", " + additionalParam);
                }

                // For the rest of the line, we do a trick: we want to apply the regular op on the rest of the header, so we remove all args and re-add them after.
                // We do this so we don't have to process constraints separately.
                transformed.Remove(headerMatch.Index + 1, headerMatch.Length - 1);
                var fakeLine = new LineInfo()
                {
                    Line = transformed.ToString(),
                    Op = Operation.None
                };
                transformed.Clear();
                var fakeLineTf = fakeLine.Transform(start, variations, type);
                transformed.Append(fakeLineTf);

                // find where we left off; we either left a hanging comma or an ()
                var match = Regex.Match(transformed.ToString(), @"(,\s*\)|\(\s*\))");
                Debug.Assert(match.Success);
                // stick our params back in there
                transformed.Insert(match.Index + 1, @params);

                return transformed.ToString();
            }

            throw new NotImplementedException();
        }
    }

    private static string VaryType(string typeName, int i)
    {
        var match = Regex.Match(typeName, @"(?<PrunedName>\w+)[0-9]+");
        if (!match.Success)
        {
            throw new InvalidOperationException("Variadic type must be of TypeName{N}");
        }

        return $"{match.Groups["PrunedName"]}{i}";
    }

    private IEnumerable<LineInfo> Tokenize(VariadicInfo info)
    {
        // NOTE: does not support multiline operations, for example this would break:
        //  // [Variadic: CopyArgs(a)]
        //  MyMethod(
        //      a_T0);
        var lines = SplitLines(info.Code);

        LineInfo.Operation nextOperation = LineInfo.Operation.None;
        List<string> nextVariables = new();
        foreach (var line in lines)
        {
            if (line.StartsWith("[Variadic("))
            {
                // don't include variadic attrs
                continue;
            }

            if (line.StartsWith("//"))
            {
                var match = Regex.Match(line, @"\[Variadic:\s*(?<Operation>\w+)(?:\((?:(?<Variable>[?\w\[\]<>]+),?\s*)+\)\])?");
                if (!match.Success)
                {
                    continue;
                }

                nextOperation = match.Groups["Operation"].Value switch
                {
                    "CopyLines" => LineInfo.Operation.CopyLines,
                    "CopyArgs" => LineInfo.Operation.CopyArgs,
                    "CopyParams" => LineInfo.Operation.CopyParams,
                    _ => throw new NotImplementedException()
                };

                if (match.Groups["Variable"].Success)
                {
                    foreach (Capture capture in match.Groups["Variable"].Captures)
                    {
                        nextVariables.Add(capture.Value);
                    }
                }

                continue;
            }

            var lineInfo = new LineInfo()
            {
                Line = line,
                Variables = nextVariables.ToArray(),
                Op = nextOperation
            };
            nextVariables.Clear();
            nextOperation = LineInfo.Operation.None;
            yield return lineInfo;
        }
    }

    private IEnumerable<string> SplitLines(string code)
    {
        return code.Split('\n').Select(line => line.Trim());
    }
}
