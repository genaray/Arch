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

public struct InterfaceInfo {

    public string Name { get; set; }
    public List<string> Generics { get; set; }
    public List<string> Params { get; set; }
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
    
    public static StringBuilder Append(this StringBuilder sb, ref InterfaceInfo interfaceInfo) {

        var genericSb = new StringBuilder();
        foreach (var generic in interfaceInfo.Generics) 
            genericSb.Append(generic).Append(",");
        genericSb.Length--;
        
        var paramSb = new StringBuilder();
        foreach (var param in interfaceInfo.Params) 
            paramSb.Append(param).Append(",");
        paramSb.Length--;
        
        var interfaceTemplate = $@"
public interface {interfaceInfo.Name}<{genericSb}>{{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Update({paramSb});
}}
";
        sb.Append(interfaceTemplate);
        return sb;
    }
    
    public static StringBuilder AppendInterfaces(this StringBuilder sb, int amount) {

        var generics = new List<string>();
        var parameters = new List<string>();
        
        for (var index = 0; index <= amount; index++) {

            generics.Add($"T{index}");
            parameters.Add($"ref T{index} t{index}");
            var interfaceInfo = new InterfaceInfo { Name = "IForEach", Generics = generics, Params = parameters};
            sb.Append(ref interfaceInfo);
        }

        return sb;
    }
    
    public static StringBuilder AppendEntityInterfaces(this StringBuilder sb, int amount) {

        var generics = new List<string>();
        var parameters = new List<string>();
        
        parameters.Add("in Entity entity");
        for (var index = 0; index <= amount; index++) {

            generics.Add($"T{index}");
            parameters.Add($"ref T{index} t{index}");
            var interfaceInfo = new InterfaceInfo { Name = "IForEachEntity", Generics = generics, Params = parameters};
            sb.Append(ref interfaceInfo);
        }

        return sb;
    }

    public static void AppendQueryMethods(this ClassBuilder builder, int amount) {

        for (var index = 0; index <= amount; index++) {

            var methodBuilder = builder.AddMethod("Query").MakePublicMethod().WithReturnType("void");
            methodBuilder.AddParameter("in QueryDescription", "description");
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

var size = Archetypes.Count;
for (var index = 0; index < size; index++) {{

    var archetype = Archetypes[index];
    var archetypeSize = archetype.Size;
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    ref var chunkFirstElement = ref archetype.Chunks[0];
    for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {{

        ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
        var chunkSize = chunk.Size;
        {getArrays}
        
        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

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
            methodBuilder.AddParameter("in QueryDescription", "description");
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

var size = Archetypes.Count;
for (var index = 0; index < size; index++) {{

    var archetype = Archetypes[index];
    var archetypeSize = archetype.Size;
    var bitset = archetype.BitSet;

    if (!query.Valid(bitset)) continue;

    ref var chunkFirstElement = ref archetype.Chunks[0];
    for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {{

        ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
        var chunkSize = chunk.Size;
        {getArrays}

        ref var entityFirstElement = ref chunk.Entities[0];
        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
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
    
    public static void AppendQueryInterfaceMethods(this StringBuilder builder, int amount) {
        
        for (var index = 0; index <= amount; index++) {

            var index1 = index;
            var generics = new StringBuilder();
            for (var localIndex = 0; localIndex <= index1; localIndex++)
                generics.Append($"T{localIndex},");
            generics.Length--;
            
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

            var methodTemplate = $@"
public partial class World{{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HPQuery<T,{generics}>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<{generics}>{{
        
        if (!QueryCache.TryGetValue(description, out var query)) {{
            query = new Query(description);
            QueryCache[description] = query;
        }}

        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {{

            var archetype = Archetypes[index];
            var archetypeSize = archetype.Size;
            var bitset = archetype.BitSet;

            if (!query.Valid(bitset)) continue;

            ref var chunkFirstElement = ref archetype.Chunks[0];
            for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {{

                ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
                var chunkSize = chunk.Size;
                {getArrays}
                
                {getFirstElement}

                for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

                    {getComponents}
                    iForEach.Update({insertParams});
                }}
            }}
        }}
    }}
}}
";
            builder.AppendLine(methodTemplate);
        }
    }
    
        public static void AppendEntityQueryInterfaceMethods(this StringBuilder builder, int amount) {
        
        for (var index = 0; index <= amount; index++) {

            var index1 = index;
            var generics = new StringBuilder();
            for (var localIndex = 0; localIndex <= index1; localIndex++)
                generics.Append($"T{localIndex},");
            generics.Length--;
            
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

            var methodTemplate = $@"
public partial class World{{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HPEQuery<T,{generics}>(in QueryDescription description, ref T iForEach) where T : struct, IForEachEntity<{generics}>{{
        
        if (!QueryCache.TryGetValue(description, out var query)) {{
            query = new Query(description);
            QueryCache[description] = query;
        }}

        var size = Archetypes.Count;
        for (var index = 0; index < size; index++) {{

            var archetype = Archetypes[index];
            var archetypeSize = archetype.Size;
            var bitset = archetype.BitSet;

            if (!query.Valid(bitset)) continue;

            ref var chunkFirstElement = ref archetype.Chunks[0];
            for (var chunkIndex = 0; chunkIndex < archetypeSize; chunkIndex++) {{

                ref readonly var chunk = ref Unsafe.Add(ref chunkFirstElement, chunkIndex);
                var chunkSize = chunk.Size;
                {getArrays}

                ref var entityFirstElement = ref chunk.Entities[0];
                {getFirstElement}

                for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

                    ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                    {getComponents}
                    iForEach.Update(in entity, {insertParams});
                }}
            }}
        }}
    }}
}}
";
            builder.AppendLine(methodTemplate);
        }
    }
}