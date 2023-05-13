namespace Arch.Core.Extensions.Dangerous;

/// <summary>
///     The <see cref="DangerousWorldExtensions"/> class
///     contains several <see cref="World"/> related extension methods which give acess to underlaying data structures that should only be modified when you exactly know what you are doing.
/// </summary>
public static class DangerousWorldExtensions
{
    
    /// <summary>
    ///     Returns 
    /// </summary>
    /// <param name="world"></param>
    /// <returns></returns>
    public static int[][] GetVersions(this World world)
    {
        return (int[][])world.EntityInfo.Versions;
    }
}
