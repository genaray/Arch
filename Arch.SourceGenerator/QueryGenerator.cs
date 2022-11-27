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

            var compileTimeStatics = new StringBuilder();
            compileTimeStatics.AppendLine("using System;");
            compileTimeStatics.AppendLine("namespace Arch.Core.Utils;");
            compileTimeStatics.AppendGroups(10);
            
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
            
            var references = new StringBuilder();
            references.AppendLine("using System;");
            references.AppendLine("using System.Runtime.CompilerServices;");
            references.AppendLine("using CommunityToolkit.HighPerformance;");
            references.AppendLine("namespace Arch.Core;");
            references.AppendReferences(10);

            var jobs = new StringBuilder();
            jobs.AppendLine("using System;");
            jobs.AppendLine("using System.Runtime.CompilerServices;");
            jobs.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            jobs.AppendLine("namespace Arch.Core;");
            jobs.AppendForEachJobs(10);
            jobs.AppendEntityForEachJobs(10);
            jobs.AppendIForEachJobs(10);
            jobs.AppendIForEachWithEntityJobs(10);

            var queries = CodeBuilder.Create("Arch.Core")
                .AddNamespaceImport("System")
                .AddNamespaceImport("System.Runtime.CompilerServices")
                .AddNamespaceImport("CommunityToolkit.HighPerformance;")
                .AddNamespaceImport("JobScheduler")
                .AddNamespaceImport("Arch.Core.Utils")
                .AddClass("World").MakePublicClass();
            queries.AppendQueryMethods(10);
            queries.AppendEntityQueryMethods(10);
            queries.AppendParallelQuerys(10);
            queries.AppendParallelEntityQuerys(10);

            var acessors = new StringBuilder();
            acessors.AppendLine("using System;");
            acessors.AppendLine("using System.Runtime.CompilerServices;");
            acessors.AppendLine("using JobScheduler;");
            acessors.AppendLine("using Arch.Core.Utils;");
            acessors.AppendLine("using System.Diagnostics.Contracts;");
            acessors.AppendLine("using Arch.Core.Extensions;");
            acessors.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            acessors.AppendLine("namespace Arch.Core;");
            acessors.AppendLine($@"
               
                public partial struct Chunk{{
                    {new StringBuilder().AppendChunkHases(10)}
                    {new StringBuilder().AppendChunkIndexGets(10)}
                    {new StringBuilder().AppendChunkGets(10)}
                    {new StringBuilder().AppendChunkIndexSets(10)}
                    {new StringBuilder().AppendChunkSets(10)}
                }}

                public partial class Archetype{{
                    {new StringBuilder().AppendArchetypeHases(10)}
                    {new StringBuilder().AppendArchetypeGets(10)}
                    {new StringBuilder().AppendArchetypeSets(10)}
                }}
            
                public partial class World{{
                    {new StringBuilder().AppendCreates(10)}
                    {new StringBuilder().AppendWorldHases(10)}
                    {new StringBuilder().AppendWorldGets(10)}
                    {new StringBuilder().AppendWorldSets(10)}
                    {new StringBuilder().AppendWorldAdds(10)}    
                    {new StringBuilder().AppendWorldRemoves(10)}
                }}

               public static partial class EntityExtensions{{
                    {new StringBuilder().AppendEntityHases(10)}
                    {new StringBuilder().AppendEntitySets(10)}
                    {new StringBuilder().AppendEntityGets(10)}
                }}
            ");
            
            var hpQueries = new StringBuilder();
            hpQueries.AppendLine("using System;");
            hpQueries.AppendLine("using System.Runtime.CompilerServices;");
            hpQueries.AppendLine("using JobScheduler;");
            hpQueries.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            hpQueries.AppendLine("using Arch.Core.Utils;");
            hpQueries.AppendLine("namespace Arch.Core;");
            hpQueries.AppendQueryInterfaceMethods(10);
            hpQueries.AppendEntityQueryInterfaceMethods(10);
            hpQueries.AppendHpParallelQuerys(10);
            hpQueries.AppendHpeParallelQuerys(10);

            initializationContext.AddSource("CompileTimeStatics.g.cs",
                CSharpSyntaxTree.ParseText(compileTimeStatics.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
            
            initializationContext.AddSource("Delegates.g.cs",
                CSharpSyntaxTree.ParseText(delegates.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Interfaces.g.cs",
                CSharpSyntaxTree.ParseText(interfaces.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
            
            initializationContext.AddSource("References.g.cs",
                CSharpSyntaxTree.ParseText(references.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Jobs.g.cs",
                CSharpSyntaxTree.ParseText(jobs.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("World.g.cs",
                CSharpSyntaxTree.ParseText(queries.Build()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Acessors.g.cs",
                CSharpSyntaxTree.ParseText(acessors.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
            
            initializationContext.AddSource("QueryInterfacesWorld.g.cs",
                CSharpSyntaxTree.ParseText(hpQueries.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
        });
    }
}