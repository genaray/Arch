namespace Arch.SourceGen;

public static class GetExtensions
{

    public static StringBuilder AppendArchetypeGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendArchetypeGet(index);
        }

        return sb;
    }

    public static StringBuilder AppendArchetypeGet(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var template =
            $$"""

            internal unsafe Components<{{generics}}> Get<{{generics}}>(scoped ref Slot slot)
            {
                ref var chunk = ref GetChunk(slot.ChunkIndex);
                return chunk.Get<{{generics}}>(slot.Index);
            }
            """;

        return sb.AppendLine(template);
    }

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
