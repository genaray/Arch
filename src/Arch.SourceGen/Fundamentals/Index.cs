namespace Arch.SourceGen.Fundamentals;

public static class IndexExtensions
{
    public static StringBuilder AppendChunkIndexes(this StringBuilder sb, int amount)
    {
        for (int index = 1; index <= amount; index++)
        {
            sb.AppendChunkIndex(index);
        }

        return sb;
    }

    public static StringBuilder AppendChunkIndex(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);

        var outs = new StringBuilder();
        for (int i = 0; i <= amount; i++)
        {
            outs.Append($"out int t{i}Index,");
        }
        outs.Length--;

        var assignIds = new StringBuilder();
        for (int i = 0; i <= amount; i++)
        {
            assignIds.AppendLine($"t{i}Index = Unsafe.Add(ref componentIdToArrayFirstElement, Component<T{i}>.ComponentType.Id);");
        }

        var template = $$"""
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        private void Index<{{generics}}>({{outs}})
        {
            ref var componentIdToArrayFirstElement = ref ComponentIdToArrayIndex.DangerousGetReference();
            {{assignIds}}
        }
        """;

        sb.Append(template);
        return sb;
    }
}
