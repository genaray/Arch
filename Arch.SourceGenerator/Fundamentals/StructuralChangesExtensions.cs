using System.Text;

namespace ArchSourceGenerator;

public static class StructuralChangesExtensions
{
    
    public static StringBuilder AppendWorldAdds(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendWorldAdd(index);
        
        return sb;
    }

    public static StringBuilder AppendWorldAdd(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        var inParameters = new StringBuilder().InsertGenericInParams(amount);

        var setIds = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            setIds.AppendLine($"ids[^{index+1}] = ComponentMeta<T{index}>.Id;");
        
        var types = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            types.AppendLine($"typeof(T{index}),");
        types.Length -= 3;
        
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Add<{generics}>(in Entity entity, {parameters})
{{
    var oldArchetype = EntityToArchetype[entity.EntityId];

    // Create a stack array with all component we now search an archetype for. 
    Span<int> ids = stackalloc int[oldArchetype.Types.Length + {amount+1}];
    oldArchetype.Types.WriteComponentIds(ids);
    {setIds}

    if (!TryGetArchetype(ids, out var newArchetype))
        newArchetype = GetOrCreate(oldArchetype.Types.Add({types}));

    Move(in entity, oldArchetype, newArchetype);
    newArchetype.Set<{generics}>(in entity, {inParameters});
}}
";

    return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendWorldRemoves(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendWorldRemove(index);
        
        return sb;
    }

    public static StringBuilder AppendWorldRemove(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var removes = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            removes.AppendLine($"ids.Remove(ComponentMeta<T{index}>.Id);");
        
        var types = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            types.AppendLine($"typeof(T{index}),");
        types.Length -= 3;
        
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void Remove<{generics}>(in Entity entity)
{{
    var oldArchetype = EntityToArchetype[entity.EntityId];

    // Create a stack array with all component we now search an archetype for. 
    Span<int> ids = stackalloc int[oldArchetype.Types.Length];
    oldArchetype.Types.WriteComponentIds(ids);
    {removes}
    ids = ids[..^{amount+1}];

    if (!TryGetArchetype(ids, out var newArchetype))
        newArchetype = GetOrCreate(oldArchetype.Types.Remove({types}));

    Move(in entity, oldArchetype, newArchetype);
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendEntityAdds(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendEntityAdd(index);
        
        return sb;
    }

    public static StringBuilder AppendEntityAdd(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount);
        
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void Add<{generics}>(this in Entity entity, {parameters})
{{
    var world = World.Worlds[entity.WorldId];
    world.Add<{generics}>(in entity);
}}
";

        return sb.AppendLine(template);
    }
    
    public static StringBuilder AppendEntityRemoves(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
            sb.AppendEntityRemove(index);
        
        return sb;
    }

    public static StringBuilder AppendEntityRemove(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var template = $@"
[MethodImpl(MethodImplOptions.AggressiveInlining)]
public static void Remove<{generics}>(this in Entity entity)
{{
    var world = World.Worlds[entity.WorldId];
    world.Remove<{generics}>(in entity);
}}
";

        return sb.AppendLine(template);
    }
}