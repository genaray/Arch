
namespace Arch.SourceGen;

[Generator]
public sealed class QueryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        if (!Debugger.IsAttached)
        {
            //Debugger.Launch();
        }

        context.RegisterPostInitializationOutput(initializationContext =>
        {

            var accessors = new StringBuilder();
            accessors.AppendLine("using System;");
            accessors.AppendLine("using System.Runtime.CompilerServices;");
            accessors.AppendLine("using Schedulers;");
            accessors.AppendLine("using Arch.Core.Utils;");
            accessors.AppendLine("using System.Diagnostics.Contracts;");
            accessors.AppendLine("using Arch.Core.Extensions;");
            accessors.AppendLine("using Arch.Core.Extensions.Internal;");
            accessors.AppendLine("using System.Diagnostics.CodeAnalysis;");
            accessors.AppendLine("using CommunityToolkit.HighPerformance;");
            accessors.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            accessors.AppendLine("using System.Buffers;");
            accessors.AppendLine(
                $$"""
                namespace Arch.Core
                {
                    public partial class World
                    {
                        {{new StringBuilder().AppendParallelQuerys(25)}}
                        {{new StringBuilder().AppendParallelEntityQuerys(25)}}

                        {{new StringBuilder().AppendQueryInterfaceMethods(25)}}
                        {{new StringBuilder().AppendEntityQueryInterfaceMethods(25)}}
                        {{new StringBuilder().AppendHpParallelQuerys(25)}}
                        {{new StringBuilder().AppendHpeParallelQuerys(25)}}
                    }
                }

                namespace Arch.Core.Extensions
                {
                    public static partial class EntityExtensions
                    {
                    #if !PURE_ECS
                        {{new StringBuilder().AppendEntityHases(25)}}
                        {{new StringBuilder().AppendEntitySets(25)}}
                        {{new StringBuilder().AppendEntityGets(25)}}
                        {{new StringBuilder().AppendEntityAdds(25)}}
                        {{new StringBuilder().AppendEntityRemoves(25)}}
                    #endif
                    }

                }
                """
            );

            initializationContext.AddSource("Accessors.g.cs",
                CSharpSyntaxTree.ParseText(accessors.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
        });
    }
}
