using Arch.Core;

namespace System.Diagnostics.CodeAnalysis
{
    
    /// <summary>
    /// Implements a attribute which allows refs to return a reference to themself.
    /// Used for the <see cref="QueryDescription"/> and the builder API. 
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Method | System.AttributeTargets.Parameter | System.AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class UnscopedRefAttribute : Attribute
    {
    }
}