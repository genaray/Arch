namespace Arch.SourceGen;

public static class HasExtensions
{
    public static StringBuilder AppendEntityHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityHas(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityHas(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""

            [Pure]
            public static bool Has<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                return world.Has<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
