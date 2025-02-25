namespace Arch.SourceGen;

public static class GetExtensions
{

    public static StringBuilder AppendEntityGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""

            [Pure]
            public static Components<{{generics}}> Get<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                return world.Get<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
