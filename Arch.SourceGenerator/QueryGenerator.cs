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
                
                var template = new StringBuilder();
                template.AppendLine("using System;");
                template.AppendLine("namespace Arch.Core;");

                template.AppendForEachDelegates(10);
                template.AppendForEachEntityDelegates(10);

                var builder = CodeBuilder.Create("Arch.Core").AddNamespaceImport("System.Runtime.CompilerServices").AddClass("World").MakePublicClass();
                builder.AppendQueryMethods(10);
                builder.AppendEntityQueryMethods(10);
                
                initializationContext.AddSource(
                    "Delegates.g.cs",
                    template.ToString()
                );
                
                initializationContext.AddSource(
                    "World.g.cs",
                    CSharpSyntaxTree.ParseText(builder.Build()).GetRoot().NormalizeWhitespace().ToFullString()
                );
            });
        }
    }
}