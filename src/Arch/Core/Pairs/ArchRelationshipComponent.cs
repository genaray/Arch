namespace Arch.Core;

internal readonly struct ArchRelationshipComponent
{
    internal readonly IBuffer Buffer;

    internal ArchRelationshipComponent(IBuffer buffer)
    {
        Buffer = buffer;
    }
}
