namespace Arch.SourceGen;

public struct InterfaceInfo
{
    public string Name { get; set; }
    public List<string> Generics { get; set; }
    public List<string> Params { get; set; }
}

public static class StringBuilderHpQueryExtensions
{

    public static StringBuilder AppendQueryInterfaceMethods(this StringBuilder builder, int amount)
    {
        for (var index = 0; index <= amount; index++)
        {
            var generics = new StringBuilder().GenericWithoutBrackets(index);
            var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""

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
            var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""

                public void InlineQuery<T,{{generics}}>(in QueryDescription description) where T : struct, IForEach<{{generics}}>
                {
                    var t = new T();

                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Count;
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
            var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""

                public void InlineEntityQuery<T,{{generics}}>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<{{generics}}>
                {
                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Count;
                        ref var entityFirstElement = ref chunk.Entity(0);
                        {{getFirstElement}}

                        foreach(var entityIndex in chunk)
                        {
                            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                            {{getComponents}}
                            iForEach.Update(entity, {{insertParams}});
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
            var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(index);
            var getComponents = new StringBuilder().GetGenericComponents(index);
            var insertParams = new StringBuilder().InsertGenericParams(index);

            var template =
                $$"""

                public void InlineEntityQuery<T,{{generics}}>(in QueryDescription description) where T : struct, IForEachWithEntity<{{generics}}>
                {
                    var t = new T();

                    var query = Query(in description);
                    foreach (ref var chunk in query)
                    {
                        var chunkSize = chunk.Count;
                        ref var entityFirstElement = ref chunk.Entity(0);
                        {{getFirstElement}}

                        foreach (var entityIndex in chunk)
                        {
                            var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                            {{getComponents}}
                            t.Update(entity, {{insertParams}});
                        }
                    }
                }
                """;

            builder.AppendLine(template);
        }

        return builder;
    }
}
