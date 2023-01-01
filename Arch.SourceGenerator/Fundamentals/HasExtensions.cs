using System.Text;

namespace ArchSourceGenerator;

public static class HasExtensions
{
    
    public static StringBuilder AppendChunkHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendChunkHas(index);
        
        return sb;
    }
    
    public static StringBuilder AppendChunkHas(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        
        var getIds = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            getIds.AppendLine($"var t{index}ComponentId = Component<T{index}>.ComponentType.Id;");
        
        var boundChecks = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            boundChecks.AppendLine($"if (t{index}ComponentId >= ComponentIdToArrayIndex.Length) return false;");

        var ifs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            ifs.AppendLine($"if (ComponentIdToArrayIndex[t{index}ComponentId] != 1) return false;");

        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
[Pure]
public bool Has<{generics}>() {whereT}
{{
    {getIds}
    {boundChecks}
    {ifs}
    return true;
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendArchetypeHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendArchetypeHas(index);
        
        return sb;
    }
    
    public static StringBuilder AppendArchetypeHas(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        
        var getIds = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            getIds.AppendLine($"var t{index}ComponentId = Component<T{index}>.ComponentType.Id;");
        
        var isSet = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            isSet.AppendLine($"BitSet.IsSet(t{index}ComponentId) &&");
        isSet.Length -= 4;

        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool Has<{generics}>() {whereT}
{{ 
    {getIds}
    return {isSet};
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendWorldHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendWorldHas(index);
        
        return sb;
    }
    
    public static StringBuilder AppendWorldHas(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public bool Has<{generics}>(in Entity entity) {whereT}
{{
    var archetype = EntityInfo[entity.Id].Archetype;
    return archetype.Has<{generics}>();
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendEntityHases(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendEntityHas(index);
        
        return sb;
    }
    
    public static StringBuilder AppendEntityHas(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var whereT = new StringBuilder().GenericWhereStruct(amount);

        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static bool Has<{generics}>(this in Entity entity) {whereT}
{{
    var world = World.Worlds[entity.WorldId];
    return world.Has<{generics}>(in entity);
}}
";

        return sb.AppendLine(template);
    }
}