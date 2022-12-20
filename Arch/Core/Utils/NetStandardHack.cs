#if NETSTANDARD2_1_OR_GREATER
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }

    /// <summary>
    /// Used to indicate to the compiler that the <c>.locals init</c> flag should not be set in method headers.
    /// </summary>
    /// <remarks>Internal copy of the .NET 5 attribute.</remarks>
    [AttributeUsage(
        AttributeTargets.Module |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Interface |
        AttributeTargets.Constructor |
        AttributeTargets.Method |
        AttributeTargets.Property |
        AttributeTargets.Event,
        Inherited = false)]
    internal sealed class SkipLocalsInitAttribute : Attribute
    {
    }
}
#endif