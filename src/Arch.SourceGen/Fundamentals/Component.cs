namespace Arch.SourceGen;

public static class ComponentExtensions
{
    public static StringBuilder AppendComponents(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendComponent(index);
        }

        return sb;
    }

    public static StringBuilder AppendComponent(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var types = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            types.Append($"Component<T{index}>.ComponentType,");
        }

        var template =
            $$"""
            /// <inheritdoc cref="Component"/>
            public static class Component<{{generics}}>
            {
                internal static readonly int Id;

                /// <summary>
                ///     An <see cref="Signature"/> for this given set of components.
                /// </summary>
                public static readonly Signature Signature;

                /// <summary>
                ///     The hash code for this given set of components.
                /// </summary>
                public static readonly int Hash;

                static Component()
                {
                    Id = Interlocked.Increment(ref Component.Id);
                    Signature = new Signature(new [] { {{types}} });
                    Hash = Signature.GetHashCode();
                }
            }
            """;

        return sb.AppendLine(template);
    }
}
