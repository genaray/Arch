using Arch.Core.Utils;

namespace Arch.Core.Extensions.Dangerous;

/// <summary>
///     The <see cref="DangerousChunkExtensions"/> class
///     contains several <see cref="Chunk"/> related extension methods which give acess to underlaying data structures that should only be modified when you exactly know what you are doing.
/// </summary>
public static class DangerousChunkExtensions
{
    /// <summary>
    ///     Creates a new <see cref="Chunk"/>;
    /// </summary>
    /// <param name="capacity">The capacity.</param>
    /// <param name="lookupArray">The lookup array.</param>
    /// <param name="types">The types.</param>
    /// <returns></returns>
    public static Chunk CreateChunk(int capacity, int[] lookupArray, ComponentType[] types)
    {
        return new Chunk(capacity, lookupArray, types);
    }

    /// <summary>
    ///     Sets the size of a <see cref="Chunk"/>.
    /// </summary>
    /// <param name="chunk">The <see cref="Chunk"/>.</param>
    /// <param name="size">Its new size.</param>
    public static void SetSize(this ref Chunk chunk, int size)
    {
        chunk.Size = size;
    }
}
