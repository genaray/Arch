using System.Text.RegularExpressions;

namespace Arch.SourceGen;
internal static class Utils
{
    /// <summary>
    ///     Within a method call, expand untyped arguments into their generic version. For example, <c>MyMethod(foo, out T0 component_T0)</c>
    ///     would expand to <c>MyMethod(foo, out T0 component_T0, out T1 component_T1)</c> given a <paramref name="type"/> of <c>T0</c>,
    ///     a <paramref name="endIndex"/> of 1, and a <paramref name="variable"/> of <c>"component"</c>.
    /// </summary>
    /// <param name="line">The line to transform.</param>
    /// <param name="type">The basic type to expand, for example T0.</param>
    /// <param name="endIndex">The last variadic to generate, incremented from the number of <paramref name="type"/>.</param>
    /// <param name="variable">The variable to expand, with a _0 suffix. For example, "component" would expand "component_T0" into its types.</param>
    /// <returns>The transformed line.</returns>
    public static string ExpandUntypedArguments(string line, string type, int endIndex, string variable)
    {
        var (typeName, typeNumber) = ExtractTypeInfo(type);

        // Modifiers capture group contains ref out, out var, etc
        var modifiersMatch = Regex.Match(line, $@"[(,]\s*(?<Modifiers>(ref|out ref|out var|out {type}\??|in|ref {type}\??))?\s*{variable}_{type}");
        if (!modifiersMatch.Success)
        {
            throw new InvalidOperationException($"Can't find variable {variable}_{type} in a parameter list.");
        }

        var modifiers = modifiersMatch.Groups["Modifiers"].Value;

        // Collect the new arguments
        var arguments = new List<string>();
        for (int i = typeNumber; i <= endIndex; i++)
        {
            arguments.Add($"{modifiers} {variable}_{typeName}{i}");
        }

        // Remove the original arguments, and replace them with our variadic version.
        // +1 to remove the preceding , or (
        var transformed = new StringBuilder();
        transformed.AppendLine(line);
        transformed.Remove(modifiersMatch.Index + 1, modifiersMatch.Length - 1);
        transformed.Insert(modifiersMatch.Index + 1, string.Join(", ", arguments));

        return transformed.ToString();
    }

    /// <param name="line">The method header line.</param>
    /// <param name="type">The basic type to expand, for example T0.</param>
    /// <param name="fullType">The full type to copy, for example Span<T0>.</param>
    /// <param name="endIndex">The last variadic to generate, incremented from the number of <paramref name="type"/>.</param>
    /// <param name="transformedStart">The inclusive start index of the transformed region of the original string.</param>
    /// <param name="transformedStart">The exclusive end index of the transformed region of the original string.</param>
    /// <returns></returns>
    public static string ExpandTypedParameters(string line, string type, string fullType, int endIndex, out int transformedStart, out int transformedEnd)
    {
        var (typeName, typeNumber) = ExtractTypeInfo(type);

        // This matches, specifically, a method header.
        // This grabs the first param that matches the passed in type (in Variables[0]), like "Span<T0>? paramName_T0 = default"
        // it extracts paramName, the initializer if it exists, any ref/out/in modifiers, and repeats it according to the type params.
        // Captured groups:
        //  -   Modifiers: holds ref, out, ref readonly, etc
        //  -   ParamName: holds the parameter name that matches fullType and type. For example, if fullType is Span<T0> and type is T0, and the parameters includes
        //      "component_T0", this will match "component".
        //  -   Assignment: holds the = assignment if present. Currently only supports default assignment.
        var headerMatch = Regex.Match(line, $@"[(,]\s*(?<Modifiers>(ref|out|ref readonly|in)\s+)?{Regex.Escape(fullType)}\s+(?<ParamName>\w+)_{type}\s*(?<Assignment>=\s*default)?");

        // If we didn't find anything to transform, we give up.
        if (!headerMatch.Success)
        {
            transformedStart = 0;
            transformedEnd = 0;
            return line;
        }

        bool hasDefault = headerMatch.Groups["Assignment"].Success;
        string modifiers = headerMatch.Groups["Modifiers"].Success ? headerMatch.Groups["Modifiers"].Value : string.Empty;
        string param = headerMatch.Groups["ParamName"].Value;

        // Here, we build up the parameter string. So T0 component_T0 would become "T0 component_T0, T1 component_T1..."
        var @params = new List<string>();
        for (int i = typeNumber; i <= endIndex; i++)
        {
            // One issue with this approach... if we had a wrapper type of SomethingT1<T1> (which is bad name but whatever) then this would break.
            // but so far we don't have anything like that.
            var variedFullType = fullType.Replace(type, $"{typeName}{i}");
            @params.Add($"{modifiers} {variedFullType} {param}_{typeName}{i} {(hasDefault ? "= default" : "")}");
        }

        var transformed = new StringBuilder();
        transformed.Append(line);

        // Remove the parameters we found
        transformed.Remove(headerMatch.Index + 1, headerMatch.Length - 1);

        // Add in our newly constructed params
        var paramsString = string.Join(", ", @params);
        transformed.Insert(headerMatch.Index + 1, string.Join(", ", paramsString));

        transformedStart = headerMatch.Index + 1;
        transformedEnd = headerMatch.Index + 1 + paramsString.Length;
        return transformed.ToString();
    }

    /// <summary>
    ///     Given a line and a type string, convert all instances of type constraints to the variadic version.
    /// </summary>
    /// <param name="line">The line of code to transform.</param>
    /// <param name="type">The type to expand, in format TypeName{N}. For example, T0.</param>
    /// <param name="endIndex">The last type to generate. For example, to expand T2 to T2, T3, T4, this parameter should be 4.</param>
    /// <returns>The transformed line.</returns>
    public static string ExpandConstraints(string line, string type, int endIndex)
    {
        var (typeName, typeNumber) = ExtractTypeInfo(type);

        // copy type constraints for our selected type
        var transformed = new StringBuilder();
        var constraints = Regex.Match(line, $@"where\s+{type}\s*:\s*(?<Constraints>.*?)(?:where|{{|$)");
        if (!constraints.Success)
        {
            // no constraints, just return the line
            return line;
        }

        // append anything prior to the original constraint
        transformed.Append(line.Substring(0, constraints.Index));

        // append extra constraints as needed
        for (int i = typeNumber; i <= endIndex; i++)
        {
            transformed.Append($" where {typeName}{i} : {constraints.Groups["Constraints"].Value} ");
        }

        // add in the rest of the line, including the original constraint
        transformed.Append(line.Substring(constraints.Index, line.Length - constraints.Index));
        return transformed.ToString();
    }

    /// <summary>
    ///     Given a line and a type string, convert all instances of angle brackets in the line to variadic version,
    ///     where the last type is the given type.
    ///     For example, &lt;A, T0&gt; would expand to &lt;A, T0, T1, T2&gt;.
    /// </summary>
    /// <param name="line">The line of code to transform.</param>
    /// <param name="type">The type to expand, in format TypeName{N}. For example, T0.</param>
    /// <param name="endIndex">The last type to generate. For example, to expand T2 to T2, T3, T4, this parameter should be 4.</param>
    /// <returns>The transformed line.</returns>
    public static string ExpandGenericTypes(string line, string type, int endIndex)
    {
        var (typeName, typeNumber) = ExtractTypeInfo(type);

        // build a string like "T0, T1, ...>"
        var variadics = new List<string>();
        for (int i = typeNumber; i <= endIndex; i++)
        {
            variadics.Add($"{typeName}{i}");
        }

        // Replace any occurances of T0> with our generated T0, ...>
        return line.Replace($"{type}>", $"{string.Join(", ", variadics)}>");
    }

    /// <summary>
    ///     Replace a type with one of its variadics in a line.
    /// </summary>
    /// <param name="line">The line to transform</param>
    /// <param name="type">A type. For example, T0.</param>
    /// <param name="variadic">The variadic to replace the type with. For example, 1 for T1.</param>
    /// <returns>The transformed line</returns>
    public static string ReplaceType(string line, string type, int variadic)
    {
        var (typeName, _) = ExtractTypeInfo(type);

        var transformed = new StringBuilder();
        transformed.AppendLine(line);
        transformed.Replace(type, $"{typeName}{variadic}");
        return transformed.ToString();
    }

    /// <summary>
    ///     Given a type string, extracts the type name and number. For example, T0 would return ("T", 0).
    /// </summary>
    /// <param name="type">The type string.</param>
    /// <returns>A tuple with the type name and number</returns>
    public static (string TypeName, int TypeNumber) ExtractTypeInfo(string type)
    {
        var match = Regex.Match(type, @"(?<Name>\w+?)(?<Number>[0-9]+)");
        if (!match.Success)
        {
            throw new InvalidOperationException($"{type} doesn't match format TypeName{{N}} (for example T0)");
        }

        return (match.Groups["Name"].Value, int.Parse(match.Groups["Number"].Value));
    }
}
