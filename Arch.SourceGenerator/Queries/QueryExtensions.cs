using System.Reflection;
using System.Text;
using CodeGenHelpers;

namespace ArchSourceGenerator;

public struct DelegateInfo
{
    public string Name { get; set; }
    public List<string> Generics { get; set; }
    public string[] Params { get; set; }
    public string ReturnType { get; set; }
}

public static class StringBuilderQueryExtensions
{
    public static StringBuilder Append(this StringBuilder sb, ref DelegateInfo delegateInfo)
    {
        sb.AppendLine();
        sb.Tab();
        sb.Append($"public delegate void {delegateInfo.Name}");

        if (delegateInfo.Generics.Count > 0)
        {
            sb.Append("<");
            foreach (var gen in delegateInfo.Generics) sb.Append(gen).Append(",");
            sb.Length--;
            sb.Append(">");
        }

        sb.Append("(");

        if (delegateInfo.Params is { Length: > 0 })
        {
            foreach (var param in delegateInfo.Params) sb.Append(param).Append(",");
            sb.Length--;
        }

        if (delegateInfo.Generics.Count > 0)
        {
            if (delegateInfo.Params is { Length: > 0 }) sb.Append(",");
            foreach (var gen in delegateInfo.Generics) sb.Append("ref ").Append(gen).Append(" ").Append(gen.ToLower()).Append(",");
            sb.Length--;
        }

        sb.Append(")");

        sb.Append(";");
        sb.AppendLine();

        return sb;
    }

    public static StringBuilder AppendForEachDelegates(this StringBuilder sb, int amount)
    {
        var generics = new List<string>();

        for (var index = 0; index <= amount; index++)
        {
            generics.Add($"T{index}");
            var delegateInfo = new DelegateInfo { Name = "ForEach", Generics = generics, ReturnType = "void" };
            sb.Append(ref delegateInfo);
        }

        return sb;
    }

    public static StringBuilder AppendForEachEntityDelegates(this StringBuilder sb, int amount)
    {
        var generics = new List<string>();

        for (var index = 0; index <= amount; index++)
        {
            generics.Add($"T{index}");
            var delegateInfo = new DelegateInfo { Name = "ForEachWithEntity", Generics = generics, Params = new[] { "in Entity entity" }, ReturnType = "void" };
            sb.Append(ref delegateInfo);
        }

        return sb;
    }

    public static StringBuilder AppendQueryMethods(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
            sb.AppendQueryMethod(index);

        return sb;
    }

    public static void AppendQueryMethod(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount).ToString();
        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        sb.Append($@"
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Query<{generics}>(in QueryDescription description, ForEach<{generics}> forEach) {whereT} {{
        
                var query = Query(in description);
                foreach (ref var chunk in query.GetChunkIterator()) {{ 

                    var chunkSize = chunk.Size;
                    {getArrays}
            
                    {getFirstElement}

                    for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex) {{

                        {getComponents}
                        forEach({insertParams});
                    }}
                }}
            }}
        
        ");
    }

    public static StringBuilder AppendEntityQueryMethods(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
            sb.AppendEntityQueryMethod(index);

        return sb;
    }

    public static void AppendEntityQueryMethod(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount).ToString();
        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        sb.Append($@"
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Query<{generics}>(in QueryDescription description, ForEachWithEntity<{generics}> forEach) {whereT} {{
        
                var query = Query(in description);
                foreach (ref var chunk in query.GetChunkIterator()) {{ 

                    var chunkSize = chunk.Size;
                    {getArrays}
            
                    ref var entityFirstElement = ref chunk.Entities[0];
                    {getFirstElement}

                    for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex) {{
                        ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                        {getComponents}
                        forEach(in entity, {insertParams});
                    }}
                }}
            }}
        
        ");
    }
}