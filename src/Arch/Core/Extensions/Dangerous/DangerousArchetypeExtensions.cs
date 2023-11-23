using Arch.Core.Utils;

namespace Arch.Core.Extensions.Dangerous;

/// <summary>
///     The <see cref="DangerousArchetypeExtensions"/> class
///     contains several <see cref="Archetype"/> related extension methods which give acess to underlaying data structures that should only be modified when you exactly know what you are doing.
/// </summary>
public static class DangerousArchetypeExtensions
{

    /// <summary>
    ///     Creates a new <see cref="Archetype"/> and returns it.
    /// </summary>
    /// <param name="types">The <see cref="ComponentType"/>s.</param>
    /// <returns></returns>
    public static Archetype CreateArchetype(ComponentType[] types)
    {
        return new Archetype(types);
    }

    /// <summary>
    ///     Sets the <see cref="Archetype.ChunkCount"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <param name="size">The size.</param>
    public static void SetSize(this Archetype archetype, int size)
    {
        archetype.ChunkCount = size;
    }

    /// <summary>
    ///     Sets the <see cref="Archetype.Chunks"/> and its capacity.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/> instance.</param>
    /// <param name="chunks">The list of <see cref="Chunk"/>s.</param>
    public static void SetChunks(this Archetype archetype, List<Chunk> chunks)
    {
        archetype.Chunks = chunks.ToArray();
        archetype.ChunkCapacity = chunks.Count;
    }

    /// <summary>
    ///     Sets the <see cref="Archetype.EntityCount"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <param name="entities">The size.</param>
    public static void SetEntities(this Archetype archetype, int entities)
    {
        archetype.EntityCount = entities;
    }

    /// <summary>
    ///     Returns the internal lookup array of a <see cref="Archetype"/>.
    /// </summary>
    /// <param name="archetype">The <see cref="Archetype"/>.</param>
    /// <returns>Its lookup array.</returns>
    public static int[] GetLookupArray(this Archetype archetype)
    {
        return archetype.LookupArray;
    }
}
