using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using CodeGenHelpers;
using Microsoft.CodeAnalysis.CSharp;

namespace ArchSourceGenerator {
    
    [Generator]
    public class QueryGenerator : IIncrementalGenerator {
        
        public void Initialize(IncrementalGeneratorInitializationContext context) {
            
            
            if (!Debugger.IsAttached) {
               //Debugger.Launch();
            }
            
            context.RegisterPostInitializationOutput(initializationContext => {
                
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

                var world = CodeBuilder.Create("Arch.Core").AddNamespaceImport("System.Runtime.CompilerServices").AddClass("World").MakePublicClass();
                world.AppendQueryMethods(10);
                world.AppendEntityQueryMethods(10);
                
                var queryInterfaces = new StringBuilder();
                queryInterfaces.AppendLine("using System;");
                queryInterfaces.AppendLine("using System.Runtime.CompilerServices;");
                queryInterfaces.AppendLine("namespace Arch.Core;");
                queryInterfaces.AppendQueryInterfaceMethods(10);
                queryInterfaces.AppendEntityQueryInterfaceMethods(10);
                
                
                initializationContext.AddSource(
                    "Delegates.g.cs",
                    delegates.ToString()
                );
                
                initializationContext.AddSource(
                    "Interfaces.g.cs",
                    interfaces.ToString()
                );
                
                initializationContext.AddSource(
                    "World.g.cs",
                    CSharpSyntaxTree.ParseText(world.Build()).GetRoot().NormalizeWhitespace().ToFullString()
                );
                
                initializationContext.AddSource(
                    "QueryInterfacesWorld.g.cs",
                    CSharpSyntaxTree.ParseText(queryInterfaces.ToString()).GetRoot().NormalizeWhitespace().ToFullString()
                );
            });
        }
    }
}