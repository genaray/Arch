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

    public static void AppendQueryMethods(this ClassBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
            builder.AppendQueryMethod(index);
    }

    public static void AppendQueryMethod(this ClassBuilder builder, int amount)
    {
        var methodBuilder = builder.AddMethod("Query").MakePublicMethod().WithReturnType("void");
        methodBuilder.AddParameter("in QueryDescription", "description");
        methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");

        var generics = new StringBuilder().Generic(amount).ToString();
        methodBuilder.AddParameter($"ForEach{generics}", "forEach");

        for (var index = 0; index <= amount; index++)
            methodBuilder.AddGeneric($"T{index}");

        methodBuilder.WithBody(writer =>
        {
            var getArrays = new StringBuilder().GetGenericArrays(amount);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
            var getComponents = new StringBuilder().GetGenericComponents(amount);
            var insertParams = new StringBuilder().InsertGenericParams(amount);

            var template =
                $@"
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
";
            writer.AppendLine(template);
        });
    }

    public static void AppendEntityQueryMethods(this ClassBuilder builder, int amount)
    {
        for (var index = 0; index < amount; index++)
            builder.AppendEntityQueryMethod(index);
    }

    public static void AppendEntityQueryMethod(this ClassBuilder builder, int amount)
    {
        var methodBuilder = builder.AddMethod("Query").MakePublicMethod().WithReturnType("void");
        methodBuilder.AddParameter("in QueryDescription", "description");
        methodBuilder.AddAttribute("MethodImpl(MethodImplOptions.AggressiveInlining)");

        var generics = new StringBuilder().Generic(amount).ToString();
        methodBuilder.AddParameter($"ForEachWithEntity{generics}", "forEach");

        for (var index = 0; index <= amount; index++)
            methodBuilder.AddGeneric($"T{index}");

        methodBuilder.WithBody(writer =>
        {
            var getArrays = new StringBuilder().GetGenericArrays(amount);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
            var getComponents = new StringBuilder().GetGenericComponents(amount);
            var insertParams = new StringBuilder().InsertGenericParams(amount);

            var template =
                $@"
var query = Query(in description);
foreach (ref var chunk in query.GetChunkIterator()) {{ 

    var chunkSize = chunk.Size;
    {getArrays}

    ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
    {getFirstElement}

    for (var entityIndex = chunkSize - 1; entityIndex >= 0; --entityIndex) {{

        ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
        {getComponents}
        forEach(in entity, {insertParams});
    }}
}}
";
            writer.AppendLine(template);
        });
    }
}