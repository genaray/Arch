namespace Arch.Core.Relationships;

/// <summary>
///     Component holding a reference to the buffer that its owning entity is being
///     targeted in.
/// </summary>
internal readonly struct ArchRelationshipComponent
{
    /// <summary>
    ///     The buffer holding a relationship with the owning entity of this component.
    /// </summary>
    internal readonly IBuffer Relationships;

    internal ArchRelationshipComponent(IBuffer relationships)
    {
        Relationships = relationships;
    }
}
