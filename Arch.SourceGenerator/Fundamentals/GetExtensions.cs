using System.Text;

namespace ArchSourceGenerator;

public static class GetExtensions
{
    
    public static StringBuilder AppendReferences(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
            sb.AppendReference(index);
        
        return sb;
    }
    
    public static StringBuilder AppendReference(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericRefParams(amount);
        
        var refStructs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            refStructs.AppendLine($"public Ref<T{index}> t{index};");
        
        var references = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            references.AppendLine($"public ref T{index} t{index};");
        
        var assignRefStructs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            assignRefStructs.AppendLine($"t{index} = new Ref<T{index}>(ref t{index}Component);");
        
        var assignRefs = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            assignRefs.AppendLine($"t{index} = ref t{index}Component;");
        

        var template = $@"
public ref struct References<{generics}>
{{

#if NETSTANDARD2_1 || NET6_0
    {refStructs}
#else
    {references}
#endif

    public References({parameters}){{

#if NETSTANDARD2_1 || NET6_0
    {assignRefStructs}
#else
    {assignRefs}
#endif        

    }}
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendChunkIndexGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendChunkIndexGet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendChunkIndexGet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetChunkArrays(amount);
        var inParams = new StringBuilder().InsertGenericParams(amount);

        var gets = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            gets.AppendLine($"ref var t{index}Component = ref t{index}Array[index];");
        
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
[Pure]
public References<{generics}> Get<{generics}>(scoped in int index)
{{
    {getArrays}
    {gets}
    return new References<{generics}>({inParams});
}}
";

        return sb.AppendLine(template);
    }

    public static StringBuilder AppendArchetypeGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendArchetypeGet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendArchetypeGet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
internal unsafe References<{generics}> Get<{generics}>(scoped ref Slot slot)
{{
    ref var chunk = ref GetChunk(slot.ChunkIndex);
    return chunk.Get<{generics}>(slot.Index);
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendWorldGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendWorldGet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendWorldGet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public References<{generics}> Get<{generics}>(in Entity entity)
{{
    var entityInfo = EntityInfo[entity.Id];
    var archetype = entityInfo.Archetype;
    return archetype.Get<{generics}>(ref entityInfo.Slot);
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendEntityGets(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendEntityGet(index);
        
        return sb;
    }
    
    public static StringBuilder AppendEntityGet(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static References<{generics}> Get<{generics}>(this in Entity entity)
{{
    var world = World.Worlds[entity.WorldId];
    return world.Get<{generics}>(entity);
}}
";

        return sb.AppendLine(template);
    }
}