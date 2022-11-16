using System.Diagnostics;
using System.Text;
using CodeGenHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace ArchSourceGenerator;

[Generator]
public class QueryGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        if (!Debugger.IsAttached)
        {
            //Debugger.Launch();
        }

        context.RegisterPostInitializationOutput(initializationContext =>
        {
            var delegates = new StringBuilder();
            delegates.AppendLine("using System;");
            delegates.AppendLine("namespace Arch.Core;");
            delegates.AppendForEachDelegates(10);
            delegates.AppendForEachEntityDelegates(10);

            var interfaces = new StringBuilder();
            interfaces.AppendLine("using System;");
            interfaces.AppendLine("using System.Runtime.CompilerServices;");
            interfaces.AppendLine("namespace Arch.Core;");
            interfaces.AppendInterfaces(10);
            interfaces.AppendEntityInterfaces(10);

            var jobs = new StringBuilder();
            jobs.AppendLine("using System;");
            jobs.AppendLine("using System.Runtime.CompilerServices;");
            jobs.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            jobs.AppendLine("namespace Arch.Core;");
            jobs.AppendForEachJobs(10);
            jobs.AppendEntityForEachJobs(10);
            jobs.AppendIForEachJobs(10);
            jobs.AppendIForEachWithEntityJobs(10);

            var world = CodeBuilder.Create("Arch.Core")
                .AddNamespaceImport("System")
                .AddNamespaceImport("System.Runtime.CompilerServices")
                .AddNamespaceImport("CommunityToolkit.HighPerformance;")
                .AddNamespaceImport("JobScheduler")
                .AddClass("World").MakePublicClass();
            world.AppendQueryMethods(10);
            world.AppendEntityQueryMethods(10);
            world.AppendParallelQuerys(10);
            world.AppendParallelEntityQuerys(10);

            var queryInterfaces = new StringBuilder();
            queryInterfaces.AppendLine("using System;");
            queryInterfaces.AppendLine("using System.Runtime.CompilerServices;");
            queryInterfaces.AppendLine("using JobScheduler;");
            queryInterfaces.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            queryInterfaces.AppendLine("namespace Arch.Core;");
            queryInterfaces.AppendQueryInterfaceMethods(10);
            queryInterfaces.AppendEntityQueryInterfaceMethods(10);
            queryInterfaces.AppendHpParallelQuerys(10);
            queryInterfaces.AppendHpeParallelQuerys(10);

            initializationContext.AddSource("Delegates.g.cs",
                CSharpSyntaxTree.ParseText(delegates.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Interfaces.g.cs",
                CSharpSyntaxTree.ParseText(interfaces.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Jobs.g.cs",
                CSharpSyntaxTree.ParseText(jobs.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("World.g.cs",
                CSharpSyntaxTree.ParseText(world.Build()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("QueryInterfacesWorld.g.cs",
                CSharpSyntaxTree.ParseText(queryInterfaces.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
        });
    }
}