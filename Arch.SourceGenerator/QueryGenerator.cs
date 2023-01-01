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
            Debugger.Launch();
        }

        context.RegisterPostInitializationOutput(initializationContext =>
        {
            const int METHOD_COUNT = 25;

            var compileTimeStatics = new StringBuilder();
            compileTimeStatics.AppendLine("using System;");
            compileTimeStatics.AppendLine("namespace Arch.Core.Utils;");
            compileTimeStatics.AppendGroups(METHOD_COUNT);
            
            var delegates = new StringBuilder();
            delegates.AppendLine("using System;");
            delegates.AppendLine("namespace Arch.Core;");
            delegates.AppendForEachDelegates(METHOD_COUNT);
            delegates.AppendForEachEntityDelegates(METHOD_COUNT);

            var interfaces = new StringBuilder();
            interfaces.AppendLine("using System;");
            interfaces.AppendLine("using System.Runtime.CompilerServices;");
            interfaces.AppendLine("namespace Arch.Core;");
            interfaces.AppendInterfaces(METHOD_COUNT);
            interfaces.AppendEntityInterfaces(METHOD_COUNT);
            
            var references = new StringBuilder();
            references.AppendLine("using System;");
            references.AppendLine("using System.Runtime.CompilerServices;");
            references.AppendLine("using CommunityToolkit.HighPerformance;");
            references.AppendLine("namespace Arch.Core;");
            references.AppendReferences(METHOD_COUNT);

            var jobs = new StringBuilder();
            jobs.AppendLine("using System;");
            jobs.AppendLine("using System.Runtime.CompilerServices;");
            jobs.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            jobs.AppendLine("namespace Arch.Core;");
            jobs.AppendForEachJobs(METHOD_COUNT);
            jobs.AppendEntityForEachJobs(METHOD_COUNT);
            jobs.AppendIForEachJobs(METHOD_COUNT);
            jobs.AppendIForEachWithEntityJobs(METHOD_COUNT);


            var queries = new StringBuilder();
            queries.AppendLine("using System;");
            queries.AppendLine("using System.Runtime.CompilerServices;");
            queries.AppendLine("using JobScheduler;");
            queries.AppendLine("using Arch.Core.Utils;");
            queries.AppendLine("using CommunityToolkit.HighPerformance;");
            queries.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            queries.AppendLine("namespace Arch.Core;");
            queries.AppendLine($@"
                
                public partial class World
                {{
                    {new StringBuilder().AppendQueryMethods(METHOD_COUNT)}
                    {new StringBuilder().AppendEntityQueryMethods(METHOD_COUNT)}
                    {new StringBuilder().AppendParallelQuerys(METHOD_COUNT)}
                    {new StringBuilder().AppendParallelEntityQuerys(METHOD_COUNT)}
                }}

            ");


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
                    {new StringBuilder().AppendChunkHases(METHOD_COUNT)}
                    {new StringBuilder().AppendChunkIndexGets(METHOD_COUNT)}        
                    {new StringBuilder().AppendChunkIndexSets(METHOD_COUNT)}
                }}

                public partial class Archetype{{
                    {new StringBuilder().AppendArchetypeHases(METHOD_COUNT)}
                    {new StringBuilder().AppendArchetypeGets(METHOD_COUNT)}
                    {new StringBuilder().AppendArchetypeSets(METHOD_COUNT)}
                }}
            
                public partial class World{{
                    {new StringBuilder().AppendCreates(METHOD_COUNT)}
                    {new StringBuilder().AppendWorldHases(METHOD_COUNT)}
                    {new StringBuilder().AppendWorldGets(METHOD_COUNT)}
                    {new StringBuilder().AppendWorldSets(METHOD_COUNT)}
                    {new StringBuilder().AppendWorldAdds(METHOD_COUNT)}    
                    {new StringBuilder().AppendWorldRemoves(METHOD_COUNT)}
                }}

               public static partial class EntityExtensions{{
                #if !PURE_ECS
                    {new StringBuilder().AppendEntityHases(METHOD_COUNT)}
                    {new StringBuilder().AppendEntitySets(METHOD_COUNT)}
                    {new StringBuilder().AppendEntityGets(METHOD_COUNT)}
                    {new StringBuilder().AppendEntityAdds(METHOD_COUNT)}
                    {new StringBuilder().AppendEntityRemoves(METHOD_COUNT)}
                #endif
                }}
            ");
            
            var hpQueries = new StringBuilder();
            hpQueries.AppendLine("using System;");
            hpQueries.AppendLine("using System.Runtime.CompilerServices;");
            hpQueries.AppendLine("using JobScheduler;");
            hpQueries.AppendLine("using ArrayExtensions = CommunityToolkit.HighPerformance.ArrayExtensions;");
            hpQueries.AppendLine("using Arch.Core.Utils;");
            hpQueries.AppendLine("namespace Arch.Core;");
            hpQueries.AppendQueryInterfaceMethods(METHOD_COUNT);
            hpQueries.AppendEntityQueryInterfaceMethods(METHOD_COUNT);
            hpQueries.AppendHpParallelQuerys(METHOD_COUNT);
            hpQueries.AppendHpeParallelQuerys(METHOD_COUNT);

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
                CSharpSyntaxTree.ParseText(queries.ToString()).GetRoot().NormalizeWhitespace().ToFullString());

            initializationContext.AddSource("Acessors.g.cs",
                CSharpSyntaxTree.ParseText(acessors.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
            
            initializationContext.AddSource("QueryInterfacesWorld.g.cs",
                CSharpSyntaxTree.ParseText(hpQueries.ToString()).GetRoot().NormalizeWhitespace().ToFullString());
        });
    }
}