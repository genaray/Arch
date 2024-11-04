namespace Arch.SourceGen;

public static class StructuralChangesExtensions
{

    public static StringBuilder AppendEntityAdds(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityAdd(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityAdd(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        var inParameters = new StringBuilder().InsertGenericInParams(amount);

        var template =
            $$"""

              public static void Add<{{generics}}>(this Entity entity, {{parameters}})
              {
                  var world = World.Worlds[entity.WorldId];
                  world.Add<{{generics}}>(entity, {{inParameters}});
              }
              """;

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendEntityRemoves(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendEntityRemove(index);
        }

        return sb;
    }

    public static StringBuilder AppendEntityRemove(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""

            public static void Remove<{{generics}}>(this Entity entity)
            {
                var world = World.Worlds[entity.WorldId];
                world.Remove<{{generics}}>(entity);
            }
            """;

        return sb.AppendLine(template);
    }
}
