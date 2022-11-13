using System.Text;

namespace ArchSourceGenerator; 

public static class StringBuilderChunkJobExtensions {

    public static void AppendForEachJobs(this StringBuilder sb, int amount) {
        
        for(var index = 0; index < amount; index++)
            sb.AppendForEachJob(index);
    }
    
    public static void AppendForEachJob(this StringBuilder sb, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);
        
        var template = $@"
public struct ForEachJob<{generics}> : IChunkJob {{

    public ForEach<{generics}> forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {{

        var chunkSize = chunk.Size;
        {getArrays}

        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

            {getComponents}
            forEach({insertParams});
        }}
    }}
}}
";
        sb.AppendLine(template);
    }
    
    public static void AppendEntityForEachJobs(this StringBuilder sb, int amount) {
        
        for(var index = 0; index < amount; index++)
            sb.AppendEntityForEachJob(index);
    }
    
    public static void AppendEntityForEachJob(this StringBuilder sb, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);
        
        var template = $@"
public struct ForEachWithEntityJob<{generics}> : IChunkJob {{

    public ForEachWithEntity<{generics}> forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {{

        var chunkSize = chunk.Size;
        {getArrays}

        ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            {getComponents}
            forEach(in entity, {insertParams});
        }}
    }}
}}
";
        sb.AppendLine(template);
    }
    
    public static void AppendIForEachJobs(this StringBuilder sb, int amount) {
        
        for(var index = 0; index < amount; index++)
            sb.AppendIForEachJob(index);
    }
    
    public static void AppendIForEachJob(this StringBuilder sb, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);
        
        var template = $@"
public struct IForEachJob<T,{generics}> : IChunkJob where T : struct, IForEach<{generics}>{{

    public T forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {{

        var chunkSize = chunk.Size;
        {getArrays}

        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

            {getComponents}
            forEach.Update({insertParams});
        }}
    }}
}}
";
        sb.AppendLine(template);
    }
    
    public static void AppendIForEachWithEntityJobs(this StringBuilder sb, int amount) {
        
        for(var index = 0; index < amount; index++)
            sb.AppendIForEachWithEntityJob(index);
    }
    
    public static void AppendIForEachWithEntityJob(this StringBuilder sb, int amount) {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getArrays = new StringBuilder().GetGenericArrays(amount);
        var getFirstElement = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var insertParams = new StringBuilder().InsertGenericParams(amount);
        
        var template = $@"
public struct IForEachWithEntityJob<T,{generics}> : IChunkJob where T : struct, IForEachWithEntity<{generics}>{{

    public T forEach;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Execute(int index, ref Chunk chunk) {{

        var chunkSize = chunk.Size;
        {getArrays}

        ref var entityFirstElement = ref ArrayExtensions.DangerousGetReference(chunk.Entities);
        {getFirstElement}

        for (var entityIndex = 0; entityIndex < chunkSize; entityIndex++) {{

            ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
            {getComponents}
            forEach.Update(in entity, {insertParams});
        }}
    }}
}}
";
        sb.AppendLine(template);
    }

}