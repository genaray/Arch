namespace Arch.SourceGen;

public static class SetExtensions
{
    public static StringBuilder AppendChunkIndexSets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendChunkIndexSet(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkIndexSet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var arrays = new StringBuilder().GetChunkFirstGenericElements(amount, "");

        var sets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            sets.AppendLine($"Unsafe.Add(ref t{index}FirstElement, index) = t{index}Component;");
        }

        var template =
            $$"""

            public void Set<{{generics}}>(int index, {{parameters}})
            {
                {{arrays}}
                {{sets}}
            }
            """;

        return sb.AppendLine(template);
    }


    public static StringBuilder AppendEntitySets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntitySet(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntitySet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var insertParams = new StringBuilder().InsertGenericInParams(amount);

        var template =
            $$"""

            public static void Set<{{generics}}>(this Entity entity, {{parameters}})
            {
                var world = World.Worlds[entity.WorldId];
                world.Set<{{generics}}>(entity, {{insertParams}});
            }
            """;

        return sb.AppendLine(template);
    }
}
