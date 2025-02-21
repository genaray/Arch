namespace Arch.SourceGen;

public static class StringBuilderQueryExtensions
{
    public static StringBuilder AppendQueryMethods(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendQueryMethod(index);
        }

        return sb;
    }

    public static StringBuilder AppendQueryMethod(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""

            public void Query<{{generics}}>(in QueryDescription description, ForEach<{{generics}}> forEach)
            {
                var query = Query(in description);
                foreach (ref var chunk in query)
                {
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        {{getComponents}}
                        forEach({{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
        return sb;
    }

    public static StringBuilder AppendEntityQueryMethods(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendEntityQueryMethod(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityQueryMethod(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirstElement = new StringBuilder().GetChunkFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);

        var template =
            $$"""

            public void Query<{{generics}}>(in QueryDescription description, ForEachWithEntity<{{generics}}> forEach)
            {
                var query = Query(in description);
                foreach (ref var chunk in query)
                {
                    ref var entityFirstElement = ref chunk.Entity(0);
                    {{getFirstElement}}

                    foreach(var entityIndex in chunk)
                    {
                        var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                        {{getComponents}}
                        forEach(entity, {{insertParams}});
                    }
                }
            }
            """;

        sb.AppendLine(template);
        return sb;
    }
}
