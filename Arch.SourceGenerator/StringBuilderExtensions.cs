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
            methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");
            
            var generics = new StringBuilder().Generic(index).ToString();
            methodBuilder.AddParameter($"ForEach{generics}", "forEach");

            for (var localIndex = 0; localIndex <= index; localIndex++)
                methodBuilder.AddGeneric($"T{localIndex}");

            var index1 = index;
            methodBuilder.WithBody(writer => {

                var getArrays = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getArrays.AppendLine($"var t{localIndex}Array = chunk.GetArray<T{localIndex}>();");
                
                var getFirstElement = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getFirstElement.AppendLine($"ref var t{localIndex}FirstElement = ref t{localIndex}Array[0];");
                
                var getComponents = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getComponents.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");
                
                var insertParams = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    insertParams.Append($"ref t{localIndex}Component,");
                insertParams.Length--;
                
                var template = 
$@"
if (!QueryCache.TryGetValue(description, out var query)) {{
    query = new Query(description);
    QueryCache[description] = query;
}}

for (var index = 0; index < Archetypes.Count; index++) {{

    var archetype = Archetypes[index];
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    ref var chunkFirstElement = ref archetype.Chunks[0];
    for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {{

        ref var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
        {getArrays}
        
        {getFirstElement}

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
            methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");

            var generics = new StringBuilder().Generic(index).ToString();
            methodBuilder.AddParameter($"ForEachWithEntity{generics}", "forEach");

            for (var localIndex = 0; localIndex <= index; localIndex++)
                methodBuilder.AddGeneric($"T{localIndex}");

            var index1 = index;
            methodBuilder.WithBody(writer => {
                
                var getArrays = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getArrays.AppendLine($"var t{localIndex}Array = chunk.GetArray<T{localIndex}>();");

                var getFirstElement = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getFirstElement.AppendLine($"ref var t{localIndex}FirstElement = ref t{localIndex}Array[0];");
                
                var getComponents = new StringBuilder();
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    getComponents.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");
                
                var insertParams = new StringBuilder();
                insertParams.Append("in entity,");
                for (var localIndex = 0; localIndex <= index1; localIndex++)
                    insertParams.Append($"ref t{localIndex}Component,");
                insertParams.Length--;
                
                var template = 
                    $@"
if (!QueryCache.TryGetValue(description, out var query)) {{
    query = new Query(description);
    QueryCache[description] = query;
}}

for (var index = 0; index < Archetypes.Count; index++) {{

    var archetype = Archetypes[index];
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    ref var chunkFirstElement = ref archetype.Chunks[0];
    for (var chunkIndex = 0; chunkIndex < archetype.Size; chunkIndex++) {{

        ref var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
        {getArrays}

        ref var entityFirstElement = ref chunk.Entities[0];
        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunk.Size; entityIndex++) {{

            ref var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
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