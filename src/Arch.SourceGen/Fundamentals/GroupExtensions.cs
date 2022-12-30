namespace Arch.SourceGen;

public static class GroupExtensions
{
    public static StringBuilder AppendGroups(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
        {
            sb.AppendGroup(index);
        }

        return sb;
    }

    public static StringBuilder AppendGroup(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var types = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            types.Append($"Component<T{index}>.ComponentType,");
        }

        var template =
            $$"""
            public static class Group<{{generics}}>
            {
                internal static readonly int Id;
                internal static readonly ComponentType[] Types;
                internal static readonly int Hash;
            
                static Group()
                {
                    Id = Group.Id++;
                    Types = new ComponentType[] { {{types}} };
                    Hash = Component.GetHashCode(Types);
                }
            }
            """;

        return sb.AppendLine(template);
    }
}
