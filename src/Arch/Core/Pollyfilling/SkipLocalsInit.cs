#if !NET5_0_OR_GREATER
namespace System.Runtime.CompilerServices;

/// <summary>
///     Forwards the SkipLocalInit to .NetStandard2.1.
/// </summary>
[AttributeUsage(
    AttributeTargets.Module
    | AttributeTargets.Class
    | AttributeTargets.Struct
    | AttributeTargets.Interface
    | AttributeTargets.Constructor
    | AttributeTargets.Method
    | AttributeTargets.Property
    | AttributeTargets.Event, Inherited = false)]
internal sealed class SkipLocalsInitAttribute : Attribute
{
}
#endif
