#if !PURE_ECS
using Arch.Core.Extensions;

namespace Arch.Core.Utils;

/// <summary>
///     A debug view exclusive for the <see cref="Entity"/> to ease the debugging.
///     <remarks>Not available for #define pure_ecs.</remarks>
/// </summary>
internal sealed class EntityDebugView
{

    /// <summary>
    ///     The <see cref="Entity"/> to debug.
    /// </summary>
    private readonly Entity _entity;

    /// <summary>
    ///     Constructs an <see cref="EntityDebugView"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> to debug.</param>
    public EntityDebugView(Entity entity)
    {
        _entity = entity;
        Components = IsAlive ? entity.GetAllComponents() : null;
    }

    /// <summary>
    ///     The id of this <see cref="Entity"/>.
    /// </summary>
    public int Id => _entity.Id;

    /// <summary>
    ///     The status of this <see cref="Entity"/>.
    /// </summary>
    public bool IsAlive => _entity.IsAlive();

    /// <summary>
    ///     The version of this <see cref="Entity"/>.
    /// </summary>
    public int Version => IsAlive ? World.Worlds[_entity.WorldId].Version(_entity) : -1;

    /// <summary>
    ///     The <see cref="Entity"/>s components.
    /// </summary>
    public object[]? Components { get; }

    /// <summary>
    ///     The <see cref="World"/> this <see cref="Entity"/> lives in.
    /// </summary>
    public World? World => IsAlive ? World.Worlds[_entity.WorldId] : null;

    /// <summary>
    ///     The <see cref="Archetype"/> this <see cref="Entity"/> lives in.
    /// </summary>
    public Archetype? Archetype => IsAlive ? World.Worlds[_entity.WorldId].GetArchetype(_entity) : null;

    /// <summary>
    ///     The <see cref="Archetype"/> this <see cref="Entity"/> lives in.
    /// </summary>
    public Chunk Chunk => IsAlive ? World.Worlds[_entity.WorldId].GetChunk(_entity) : default;

    /// <summary>
    ///     The stored <see cref="EntityInfo"/> for this <see cref="Entity"/>.
    /// </summary>
    public EntityInfo EntityInfo => IsAlive ? World?.EntityInfo[_entity.Id] ?? new EntityInfo() : new EntityInfo();
}

#endif
