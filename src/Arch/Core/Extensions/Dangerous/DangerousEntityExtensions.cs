namespace Arch.Core.Extensions.Dangerous;

/// <summary>
///     The <see cref="DangerousEntityExtensions"/> class
///     contains several <see cref="Entity"/> related extension methods which give acess to underlaying data structures that should only be modified when you exactly know what you are doing.
/// </summary>
public static class DangerousEntityExtensions
{
    /// <summary>
    ///     Creates an <see cref="Entity"/> struct and returns it.
    ///     Does not create an <see cref="Entity"/> in the world, just the plain struct. 
    /// </summary>
    /// <param name="id">Its id.</param>
    /// <param name="world">Its world id.</param>
    /// <returns>The new <see cref="Entity"/>.</returns>
    public static Entity CreateEntityStruct(int id, int world)
    {
#if PURE_ECS
        return new Entity(id, 0);
#else
        return new Entity(id, world);
#endif
    }
}
