using System.Text;

namespace ArchSourceGenerator;

public static class GroupExtensions
{
 
    public static StringBuilder AppendGroups(this StringBuilder sb, int amount)
    {
        for (var index = 0; index < amount; index++)
            sb.AppendGroup(index);
        
        return sb;
    }
    
    public static StringBuilder AppendGroup(this StringBuilder sb, int amount)
    {

        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var types = new StringBuilder();
        for (var index = 0; index <= amount; index++)
            types.Append($"typeof(T{index}),");

        var template = $@"
public static class Group<{generics}>
{{
    internal static readonly int Id;
    internal static readonly Type[] Types;

    static Group(){{
        Id = Group.Id++;
        Types = new Type[]{{{types}}};
    }}
}}
";

        return sb.AppendLine(template);
    }
}