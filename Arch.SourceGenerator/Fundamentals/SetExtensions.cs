using System.Text;
using CodeGenHelpers;
using Microsoft.CodeAnalysis;

namespace ArchSourceGenerator;

public static class SetExtensions
{
    
    public static StringBuilder AppendChunkIndexSets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendChunkIndexSet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendChunkIndexSet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var arrays = new StringBuilder().GetChunkArrays(amount);
        
        var sets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            sets.AppendLine($"t{index}Array[index] = t{index}Component;");

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Set<{generics}>(in int index, {parameters})
{{
    {arrays}
    {sets}
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendChunkSets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendChunkSet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendChunkSet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var arrays = new StringBuilder().GetChunkArrays(amount);
        
        var sets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            sets.AppendLine($"t{index}Array[entityIndex] = t{index}Component;");

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Set<{generics}>(in Entity entity, {parameters})
{{
    var entityIndex = EntityIdToIndex[entity.EntityId];
    {arrays}
    {sets}
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendArchetypeSets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendArchetypeSet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendArchetypeSet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var insertParameters = new StringBuilder().InsertGenericInParams(amount);
            
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Set<{generics}>(in Entity entity, {parameters})
{{
    var chunkIndex = EntityIdToChunkIndex[entity.EntityId];
    ref var chunk = ref Chunks[chunkIndex];
    chunk.Set<{generics}>(in entity, {insertParameters});
}}
";

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendWorldSets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendWorldSet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendWorldSet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var insertParams = new StringBuilder().InsertGenericInParams(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Set<{generics}>(in Entity entity, {parameters})
{{
    var archetype = EntityToArchetype[entity.EntityId];
    archetype.Set<{generics}>(in entity, {insertParams});
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendEntitySets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendEntitySet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendEntitySet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInParams(amount);
        var insertParams = new StringBuilder().InsertGenericInParams(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void Set<{generics}>(this in Entity entity, {parameters})
{{
    var world = World.Worlds[entity.WorldId];
    world.Set<{generics}>(in entity, {insertParams});
}}
";

        return sb.AppendLine(template);
    }
}