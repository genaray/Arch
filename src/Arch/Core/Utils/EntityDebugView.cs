#if !PURE_ECS
using Arch.Core.Extensions;

namespace Arch.Core.Utils;

/// <summary>
///     A debug view exclusive for the <see cref="Entity"/> to ease the debugging.
///     <remarks>Not available for #define pure_ecs.</remarks>
/// </summary>
internal sealed class EntityDebugView
{
    private readonly Entity _entity;

    /// <summary>
    /// Constructs an <see cref="EntityDebugView"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to debug.</param>
    public EntityDebugView(Entity entity)
    {
        _entity = entity;
        Components = entity.GetAllComponents();
    }

    /// <summary>
    /// The id of this <see cref="Entity"/>.
    /// </summary>
    public int Id => _entity.Id;

    /// <summary>
    /// The status of this <see cref="Entity"/>.
    /// </summary>
    public bool IsAlive => _entity.IsAlive();

    /// <summary>
    /// The <see cref="Entity"/>s components.
    /// </summary>
    public object[] Components { get; }

    /// <summary>
    /// The stored <see cref="EntityInfo"/> for this <see cref="Entity"/>.
    /// </summary>
    public EntityInfo EntityInfo => IsAlive ? World.EntityInfo[_entity.Id] : new EntityInfo();

    /// <summary>
    /// The <see cref="World"/> this <see cref="Entity"/> lives in.
    /// </summary>
    public World World => IsAlive ? World.Worlds[_entity.WorldId] : null;
}

#endif
