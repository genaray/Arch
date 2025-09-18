namespace Arch.Core.Utils;

public static class AssertionUtils
{
    [Conditional("DEBUG"), Conditional("CHANGED_FLAGS")]
    public static void AssertNoChangedFilter(QueryDescription queryDescription, [CallerMemberName] string? callerName = null)
    {
        Debug.Assert(queryDescription.Changed.Count == 0, $"The method {callerName} does not support queries with dirty filters.");
    }
}
