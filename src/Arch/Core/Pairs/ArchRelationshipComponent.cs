namespace Arch.Core;

/// <summary>
///     Component holding a reference to the buffer that its owning entity is being
///     targeted in.
/// </summary>
internal readonly struct ArchRelationshipComponent
{
    /// <summary>
    ///     The buffer holding the owning entity of this component.
    /// </summary>
    internal readonly IBuffer Buffer;

    internal ArchRelationshipComponent(IBuffer buffer)
    {
        Buffer = buffer;
    }
}
