namespace Arch.SourceGen;

public static class CreateExtensions
{
    public static StringBuilder AppendCreates(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendCreate(index);
        }

        return sb;
    }

    public static StringBuilder AppendCreate(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        var inParameters = new StringBuilder().InsertGenericInParams(amount);

        var addEvents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            addEvents.AppendLine($"OnComponentAdded<T{index}>(entity);");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Entity Create<{{generics}}>({{parameters}})
            {
                var types = Group<{{generics}}>.Types;

                // Recycle id or increase
                var recycle = RecycledIds.TryDequeue(out var recycledId);
                var recycled = recycle ? recycledId : new RecycledEntity(Size, 1);

                // Create new entity and put it to the back of the array
                var entity = new Entity(recycled.Id, Id);

                // Add to archetype & mapping
                var archetype = GetOrCreate(types);
                var createdChunk = archetype.Add(entity, out var slot);

                archetype.Set<{{generics}}>(ref slot, {{inParameters}});

                // Resize map & Array to fit all potential new entities
                if (createdChunk)
                {
                    Capacity += archetype.EntitiesPerChunk;
                    EntityInfo.EnsureCapacity(Capacity);
                }

                // Map
                EntityInfo.Add(entity.Id, recycled.Version, archetype, slot);

                Size++;
                OnEntityCreated(entity);

                {{addEvents}}
                return entity;
            }
            """;

        return sb.AppendLine(template);
    }
}
