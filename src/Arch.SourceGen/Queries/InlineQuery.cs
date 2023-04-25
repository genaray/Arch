namespace Arch.SourceGen;

public struct InterfaceInfo
{
    public string Name { get; set; }
    public List<string> Generics { get; set; }
    public List<string> Params { get; set; }
}

public static class StringBuilderHpQueryExtensions
{
    public static StringBuilder Append(this StringBuilder sb, ref InterfaceInfo interfaceInfo)
    {
        var genericSb = new StringBuilder();
        foreach (var generic in interfaceInfo.Generics)
        {
            genericSb.Append(generic).Append(",");
        }

        genericSb.Length--;

        var paramSb = new StringBuilder();
        foreach (var param in interfaceInfo.Params)
        {
            paramSb.Append(param).Append(",");
        }

        paramSb.Length--;

        var template =
            $$"""
            public interface {{interfaceInfo.Name}}<{{genericSb}}>
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                void Update({{paramSb}});
            }
            """;

        sb.Append(template);
        return sb;
    }

    public static StringBuilder AppendInterfaces(this StringBuilder sb, int amount)
    {
        var generics = new List<string>();
        var parameters = new List<string>();

        for (var index = 0; index <= amount; index++)
        {
            generics.Add($"T{index}");
            parameters.Add($"ref T{index} t{index}");
            var interfaceInfo = new InterfaceInfo { Name = "IForEach", Generics = generics, Params = parameters };
            sb.Append(ref interfaceInfo);
        }

        return sb;
    }

    public static StringBuilder AppendEntityInterfaces(this StringBuilder sb, int amount)
    {
        var generics = new List<string>();
        var parameters = new List<string>
        {
            "in Entity entity"
        };

        for (var index = 0; index <= amount; index++)
        {
            generics.Add($"T{index}");
            parameters.Add($"ref T{index} t{index}");

            var interfaceInfo = new InterfaceInfo { Name = "IForEachWithEntity", Generics = generics, Params = parameters };
            sb.Append(ref interfaceInfo);
        }

        return sb;
    }

    public static StringBuilder AppendQueryInterfaceMethods(this StringBuilder builder, int amount)
    {
        for (var index = 0; index <= amount; index++)
        {
            var generics = new StringBuilder().GenericWithoutBrackets(index);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void InlineQuery<T,{{generics}}>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<{{generics}}>
                {
                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        {{getFirstElement}}

                        foreach(var entityIndex in chunk)
                        {
                            {{getComponents}}
                            iForEach.Update({{insertParams}});
                        }
                    }
                }
                """;

            builder.AppendLine(template);
        }

        // Methods with default T
        for (var index = 0; index <= amount; index++)
        {
            var generics = new StringBuilder().GenericWithoutBrackets(index);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void InlineQuery<T,{{generics}}>(in QueryDescription description) where T : struct, IForEach<{{generics}}>
                {
                    var t = new T();

                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Size;
                        {{getFirstElement}}

                        foreach(var entityIndex in chunk)
                        {
                            {{getComponents}}
                            t.Update({{insertParams}});
                        }
                    }
                }
                """;

            builder.AppendLine(template);
        }

        return builder;
    }

    public static StringBuilder AppendEntityQueryInterfaceMethods(this StringBuilder builder, int amount)
    {
        for (var index = 0; index <= amount; index++)
        {
            var generics = new StringBuilder().GenericWithoutBrackets(index);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void InlineEntityQuery<T,{{generics}}>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<{{generics}}>
                {
                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Size;
                        ref var entityFirstElement = ref chunk.Entity(0);
                        {{getFirstElement}}

                        foreach(var entityIndex in chunk)
                        {
                            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                            {{getComponents}}
                            iForEach.Update(in entity, {{insertParams}});
                        }
                    }
                }
                """;

            builder.AppendLine(template);
        }

        // Methods with default T
        for (var index = 0; index <= amount; index++)
        {
            var generics = new StringBuilder().GenericWithoutBrackets(index);
            var getFirstElement = new StringBuilder().GetFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public void InlineEntityQuery<T,{{generics}}>(in QueryDescription description) where T : struct, IForEachWithEntity<{{generics}}>
                {
                    var t = new T();

                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Size;
                        ref var entityFirstElement = ref chunk.Entity(0);
                        {{getFirstElement}}

                        foreach (var entityIndex in chunk)
                        {
                            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                            {{getComponents}}
                            t.Update(in entity, {{insertParams}});
                        }
                    }
                }
                """;

            builder.AppendLine(template);
        }

        return builder;
    }
}
