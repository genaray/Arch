using System.Text;

namespace ArchSourceGenerator;

public static class StringBuilderExtensions
{
    public static StringBuilder Tab(this StringBuilder sb)
    {
        sb.Append(" ");
        return sb;
    }

    public static StringBuilder GenericWhereStruct(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++) 
            sb.Append($" where T{localIndex}: struct ");
        return sb;
    }

    public static StringBuilder Generic(this StringBuilder sb, int amount)
    {
        sb.Append("<");
        for (var localIndex = 0; localIndex <= amount; localIndex++) sb.Append($"T{localIndex},");
        sb.Length--;
        sb.Append(">");
        return sb;
    }

    public static StringBuilder GenericWithoutBrackets(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"T{localIndex},");
        sb.Length--;

        return sb;
    }

    public static StringBuilder GetGenericArrays(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"var t{localIndex}Array = chunk.GetArray<T{localIndex}>();");

        return sb;
    }

    public static StringBuilder GetFirstGenericElements(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"ref var t{localIndex}FirstElement = ref System.Runtime.InteropServices.MemoryMarshal.GetReference(t{localIndex}Array);");

        return sb;
    }
    
    public static StringBuilder GetLastGenericElements(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"ref var t{localIndex}LastElement = ref System.Runtime.InteropServices.MemoryMarshal.GetReference(t{localIndex}Array.Slice(chunkSize-1));");

        return sb;
    }

    public static StringBuilder GetGenericComponents(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");

        return sb;
    }

    public static StringBuilder GenericRefParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"ref T{localIndex} t{localIndex}Component,");
        sb.Length--;
        return sb;
    }
    
    public static StringBuilder InsertGenericParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"ref t{localIndex}Component,");
        sb.Length--;
        return sb;
    }
    
    public static StringBuilder GenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"in T{localIndex} t{localIndex}Component,");
        sb.Length--;
        return sb;
    }
    
    public static StringBuilder GenericInDefaultParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"in T{localIndex} t{localIndex}Component = default,");
        sb.Length--;
        return sb;
    }
    
    public static StringBuilder InsertGenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"in t{localIndex}Component,");
        sb.Length--;
        return sb;
    }
    
    // World-----------
    
    public static StringBuilder GetChunkArrays(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"var t{localIndex}Array = GetSpan<T{localIndex}>();");
        sb.Length--;
        return sb;
    }
}