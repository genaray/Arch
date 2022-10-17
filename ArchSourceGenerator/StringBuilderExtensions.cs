using System.Text;
using CodeGenHelpers;
using Microsoft.CodeAnalysis;

namespace ArchSourceGenerator;

public struct DelegateInfo {

    public string Name { get; set; }
    public List<string> Generics { get; set; }
    public string[] Params { get; set; }
    public string ReturnType { get; set; }
}

public static class StringBuilderExtensions {

    public static StringBuilder Tab(this StringBuilder sb) {

        sb.Append(" ");
        return sb;
    }

    public static StringBuilder Generic(this StringBuilder sb, int amount) {
        
        sb.Append("<");
        for (var localIndex = 0; localIndex <= amount; localIndex++) sb.Append($"T{localIndex},");
        sb.Length--;
        sb.Append(">");
        return sb;
    }

    public static StringBuilder Append(this StringBuilder sb, ref DelegateInfo delegateInfo) {

        sb.AppendLine();
        sb.Tab();
        sb.Append($"public delegate void {delegateInfo.Name}");

        if (delegateInfo.Generics.Count > 0) {
            sb.Append("<");
            foreach (var gen in delegateInfo.Generics) sb.Append(gen).Append(",");
            sb.Length--;
            sb.Append(">");
        }
        
        sb.Append("(");
        if (delegateInfo.Params is { Length: > 0 }) {
            foreach (var param in delegateInfo.Params) sb.Append(param).Append(",");
            sb.Length--;
        }
        
        if (delegateInfo.Generics.Count > 0) {
            if (delegateInfo.Params is { Length: > 0 }) sb.Append(",");
            foreach (var gen in delegateInfo.Generics) sb.Append("ref ").Append(gen).Append(" ").Append(gen.ToLower()).Append(",");
            sb.Length--;
        }
        sb.Append(")");
        
        sb.Append(";");
        sb.AppendLine();

        return sb;
    }

    public static StringBuilder AppendForEachDelegates(this StringBuilder sb, int amount) {
        
        var generics = new List<string>();
        for (var index = 0; index <= 10; index++) {

            generics.Add($"T{index}");
            var delegateInfo = new DelegateInfo { Name = "ForEach", Generics = generics, ReturnType = "void" };
            sb.Append(ref delegateInfo);
        }

        return sb;
    }
    
    public static StringBuilder AppendForEachEntityDelegates(this StringBuilder sb, int amount) {
        
        var generics = new List<string>();
        for (var index = 0; index <= 10; index++) {

            generics.Add($"T{index}");
            var delegateInfo = new DelegateInfo { Name = "ForEachWithEntity", Generics = generics, Params = new []{"in Entity entity"}, ReturnType = "void" };
            sb.Append(ref delegateInfo);
        }

        return sb;
    }

    public static void AppendQueryMethods(this ClassBuilder builder, int amount) {

        for (var index = 0; index <= amount; index++) {

            var methodBuilder = builder.AddMethod("Query").MakePublicMethod().WithReturnType("void");
            methodBuilder.AddParameter("QueryDescription", "description");

            var generics = new StringBuilder().Generic(index).ToString();
            methodBuilder.AddParameter($"ForEach{generics}", "forEach");

            for (var localIndex = 0; localIndex <= index; localIndex++)
                methodBuilder.AddGeneric($"T{localIndex}");

            var index1 = index;
            methodBuilder.WithBody(writer => {

                var getArrays = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getArrays.AppendLine($"var t{localIndex}Array = chunk.GetArray<T{localIndex}>();");
                
                var getComponents = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getComponents.AppendLine($"ref var t{localIndex}Component = ref t{localIndex}Array[entityIndex];");
                
                var insertParams = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    insertParams.Append($"ref t{localIndex}Component,");
                insertParams.Length--;
                
                var template = 
$@"
var query = new Query(description);
for (var index = 0; index < Archetypes.Count; index++) {{

    var archetype = Archetypes[index];
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    var chunks = archetype.Chunks;
    for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {{

        ref var chunk = ref chunks[chunkIndex];
        {getArrays}

        for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++) {{

            {getComponents}
            forEach({insertParams});
        }}
    }}
}}
";
                writer.AppendLine(template);
            });
        }
    }
    
    public static void AppendEntityQueryMethods(this ClassBuilder builder, int amount) {

        for (var index = 0; index <= amount; index++) {

            var methodBuilder = builder.AddMethod("Query").MakePublicMethod().WithReturnType("void");
            methodBuilder.AddParameter("QueryDescription", "description");

            var generics = new StringBuilder().Generic(index).ToString();
            methodBuilder.AddParameter($"ForEachWithEntity{generics}", "forEach");

            for (var localIndex = 0; localIndex <= index; localIndex++)
                methodBuilder.AddGeneric($"T{localIndex}");

            var index1 = index;
            methodBuilder.WithBody(writer => {
                
                var getArrays = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getArrays.AppendLine($"var t{localIndex}Array = chunk.GetArray<T{localIndex}>();");
                
                var getComponents = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getComponents.AppendLine($"ref var t{localIndex}Component = ref t{localIndex}Array[entityIndex];");
                
                var insertParams = new StringBuilder();
                insertParams.Append("in entity,");
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    insertParams.Append($"ref t{localIndex}Component,");
                insertParams.Length--;
                
                var template = 
                    $@"
var query = new Query(description);
for (var index = 0; index < Archetypes.Count; index++) {{

    var archetype = Archetypes[index];
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    var chunks = archetype.Chunks;
    for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {{

        ref var chunk = ref chunks[chunkIndex];
        var entities = chunk.Entities;
        {getArrays}

        for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++) {{

            ref var entity = ref entities[entityIndex];
            {getComponents}
            forEach({insertParams});
        }}
    }}
}}
";
                writer.AppendLine(template);
            });
        }
    }
}