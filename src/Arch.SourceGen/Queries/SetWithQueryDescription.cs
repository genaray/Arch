namespace Arch.SourceGen;

/// <summary>
///     Adds extension methods for generating `World.Remove(in query);` methods.
/// </summary>
public static class SetWithQueryDesription
{
    /// <summary>
    ///     Appends `World.Set(in query)` methods.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount.</param>
    /// <returns></returns>
    public static StringBuilder AppendSetWithQueryDescriptions(this StringBuilder sb, int amount)
    {
        for (var index = 1; index < amount; index++)
        {
            sb.AppendSetWithQueryDescription(index);
        }

        return sb;
    }

    /// <summary>
    ///     Appends a `World.Set(in query)` method.
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder"/> instance.</param>
    /// <param name="amount">The amount of generic parameters.</param>
    public static void AppendSetWithQueryDescription(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var getFirsts = new StringBuilder().GetFirstGenericElements(amount);
        var getComponents = new StringBuilder().GetGenericComponents(amount);
        var parameters = new StringBuilder().GenericInDefaultParams(amount,"ComponentValue");

        var assignValues = new StringBuilder();
        var assignValuesEvents = new StringBuilder();
        for (var index = 0; index <= amount; index++)
        {
            assignValues.AppendLine($"t{index}Component = t{index}ComponentValue;");
            assignValuesEvents.AppendLine($"OnComponentSet<T{index}>(entity);");
        }

        var template =
            $$"""
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Set<{{generics}}>(in QueryDescription queryDescription, {{parameters}})
            {
                var query = Query(in queryDescription);
                foreach (ref var chunk in query)
                {
                    {{getFirsts}}
                    foreach (var entityIndex in chunk)
                    {
                        {{getComponents}}
                        {{assignValues}}
            #if EVENTS
                        var entity = chunk.Entity(entityIndex);
                        {{assignValuesEvents}}
            #endif
                    }
                }
            }
            """;

        sb.AppendLine(template);
    }
}
