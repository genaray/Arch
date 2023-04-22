namespace Arch.SourceGen;

/// <summary>
///     The <see cref="StringBuilderExtensions"/> class
///     contains several static extensions for the <see cref="StringBuilder"/> to ease code generation.
/// </summary>
public static class StringBuilderExtensions
{


    /// <summary>
    ///     Appends a comma seperated list of generics like in between diamong operators mostly. But without the diamonds.
    ///     <example>
    ///         <code>
    ///             T0,T1,T2...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GenericWithoutBrackets(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"T{localIndex},");
        }

        sb.Length--;

        return sb;
    }


    // Queries, set, has & get

    /// TODO : Probably use chunk.GetFirst<...>(); overloads instead?
    /// <summary>
    ///     Gets all generic first elements from an chunk and lists them up under each other.
    ///     <example>
    ///         <code>
    ///             ref var t0FirstElement = ref chunk.GetFirst&lt;T0&gt;();
    ///             ref var t1FirstElement = ref chunk.GetFirst&lt;T1&gt;();
    ///             ...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GetFirstGenericElements(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref var t{localIndex}FirstElement = ref chunk.GetFirst<T{localIndex}>();");
        }
        return sb;
    }

    /// <summary>
    ///     Gets generics components from the first element and lists them under each other.
    ///     <example>
    ///         <code>
    ///             ref var t0Component = ref Unsafe.Add(ref t0FirstElement, entityIndex);
    ///             ref var t1Component = ref Unsafe.Add(ref t1FirstElement, entityIndex);
    ///             ...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GetGenericComponents(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.AppendLine($"ref var t{localIndex}Component = ref Unsafe.Add(ref t{localIndex}FirstElement, entityIndex);");
        }

        return sb;
    }

    /// <summary>
    ///     Lists ref params in a row as parameters.
    ///     <example>
    ///         <code>
    ///             ref T0 t0Component, ref T1 t1Component,...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GenericRefParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"ref T{localIndex} t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    /// <summary>
    ///     Lists in params in a row as parameters.
    ///     <example>
    ///         <code>
    ///             in T0 t0Component, in T1 t1Component,...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GenericInDefaultParams(this StringBuilder sb, int amount, string name = "Component")
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in T{localIndex} t{localIndex}{name} = default,");
        }

        sb.Length--;
        return sb;
    }

    /// <summary>
    ///     Lists ref params in a row as parameters with in.
    ///     <example>
    ///         <code>
    ///             in T0 t0Component, in T1 t1Component,...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in T{localIndex} t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    /// <summary>
    ///     Inserts ref params in a row as parameters.
    ///     <example>
    ///         <code>
    ///             ref t0Component, ref t1Component,...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder InsertGenericParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"ref t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }

    /// <summary>
    ///     Inserts ref params in a row as parameters.
    ///     <example>
    ///         <code>
    ///             in t0Component, in t1Component,...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder InsertGenericInParams(this StringBuilder sb, int amount)
    {
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            sb.Append($"in t{localIndex}Component,");
        }

        sb.Length--;
        return sb;
    }


    /// <summary>
    ///     Gets out params and appends them after each other
    ///     <example>
    ///         <code>
    ///             out var t1Array, out var t2Array
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="appendix">The appendix.</param>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder InsertGenericOutParams(this StringBuilder sb, string appendix, int amount)
    {
        var arrays = new StringBuilder();
        for (var localIndex = 0; localIndex <= amount; localIndex++)
        {
            arrays.Append($"out var t{localIndex}{appendix},");
        }
        arrays.Length--;

        return arrays;
    }

    /// <summary>
    ///     Lists the types of generics in a row.
    ///     <example>
    ///         <code>
    ///             typeof(T0), typeof(T1),...
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GenericTypeParams(this StringBuilder sb, int amount)
    {
        for (var index = 0; index <= amount; index++)
        {
            sb.Append($"typeof(T{index}),");
        }
        sb.Length--;
        return sb;
    }


    /// <summary>
    ///     Gets the chunk arrays in one line and appends them.
    ///     <example>
    ///         <code>
    ///             GetArray&lt;T, T1, ...&gt;(out var t0Array, out var t1Array,...);
    ///         </code>
    ///     </example>
    /// </summary>
    /// <param name="sb"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static StringBuilder GetChunkArrays(this StringBuilder sb, int amount)
    {
        var generics = new StringBuilder().GenericWithoutBrackets(amount);
        var arrays = new StringBuilder().InsertGenericOutParams("Array", amount);

        sb.Append($"GetArray<{generics}>({arrays});");
        return sb;
    }
}
