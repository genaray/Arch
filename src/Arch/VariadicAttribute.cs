using System;

namespace Arch.Core;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class | AttributeTargets.Struct)]
internal class VariadicAttribute : Attribute
{
    public string Name { get; }
    public int Count { get; }
    public VariadicAttribute(string name, int count = 26)
    {
        Name = name;
        Count = count;
    }
}
