#if !NET7_0_OR_GREATER
namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///     Used to indicate a byref escapes and is not scoped.
/// </summary>
/// <remarks>
///     There are several cases where the C# compiler treats a <see langword="ref"/> as implicitly
///     <see langword="scoped"/> - where the compiler does not allow the <see langword="ref"/> to escape the method.
///     <br/>
///     For example:
///     <list type="number">
///         <item><see langword="this"/> for <see langword="struct"/> instance methods.</item>
///         <item><see langword="ref"/> parameters that refer to <see langword="ref"/> <see langword="struct"/> types.</item>
///         <item><see langword="out"/> parameters.</item>
///     </list>
///     This attribute is used in those instances where the <see langword="ref"/> should be allowed to escape.
/// </remarks>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class UnscopedRefAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="UnscopedRefAttribute"/> class.
    /// </summary>
    public UnscopedRefAttribute() { }
}
#endif
