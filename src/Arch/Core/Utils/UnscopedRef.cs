using Arch.Core;

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Implements a attribute which allows refs to return a reference to themself.
/// Used for the <see cref="QueryDescription"/> and the builder API. 
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public sealed class UnscopedRefAttribute : Attribute { }
