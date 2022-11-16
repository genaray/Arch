using System.Text;

namespace ArchSourceGenerator;

public static class StringBuilderExtensions
{
    public static StringBuilder Tab(this StringBuilder sb)
    {
        sb.Append(" ");
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
            sb.AppendLine($"ref var t{localIndex}FirstElement = ref ArrayExtensions.DangerousGetReference(t{localIndex}Array);");

        return sb;
    }

    public static StringBuilder GetGenericComponents(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");

        return sb;
    }

    public static StringBuilder InsertGenericParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
            sb.Append($"ref t{localIndex}Component,");
        sb.Length--;
        return sb;
    }
}