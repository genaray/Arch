using System.Text;

namespace ArchSourceGenerator;

public static class CreateExtensions
{
    
    public static StringBuilder AppendCreates(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
            sb.AppendCreate(index);
        
        return sb;
    }
    
    public static StringBuilder AppendCreate(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        var inParameters = new StringBuilder().InsertGenericInParams(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public Entity Create<{generics}>({parameters})
{{
    
    var types = Group<{generics}>.Types;

    // Recycle id or increase
    var recycle = RecycledIds.TryDequeue(out var recycledId);
    var id = recycle ? recycledId : Size;

    // Create new entity and put it to the back of the array
    var entity = new Entity(id, Id);

    // Add to archetype & mapping
    var archetype = GetOrCreate(types);
    var createdChunk = archetype.Add(in entity);

    archetype.Set<{generics}>(in entity, {inParameters});

    // Resize map & Array to fit all potential new entities
    if (createdChunk)
    {{
        Capacity += archetype.EntitiesPerChunk;
        EntityInfo.EnsureCapacity(Capacity);
    }}

    // Map
    EntityInfo[id] = new EntityInfo{{ Version = 0, Archetype = archetype, ChunkIndex = 0}};

    Size++;
    return entity;
}}
";

        return sb.AppendLine(template);
    }
}