using Arch.Core.Utils;

namespace Arch.Core.Extensions.Dangerous;

/// <summary>
///     The <see cref="DangerousChunkExtensions"/> class
///     contains several <see cref="Chunk"/> related extension methods which give acess to underlaying data structures that should only be modified when you exactly know what you are doing.
/// </summary>
public class DangerousChunkExtensions
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
}
