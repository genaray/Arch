namespace Arch.Core.Extensions;

// TODO: Documentation.
/// <summary>
///     The <see cref="ArrayExtensions"/> class
///     ...
/// </summary>
public static class ArrayExtensions
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[]? Add<T>(this T[] target, params T[] items)
    {
        if (target is null)
        {
            return null;
        }

        var result = new T[target.Length + items.Length];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Length; index++)
        {
            result[target.Length + index] = items[index];
        }

        return result;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[]? Add<T>(this T[] target, IList<T> items)
    {
        if (target is null)
        {
            return null;
        }

        var result = new T[target.Length + items.Count];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Count; index++)
        {
            result[target.Length + index] = items[index];
        }

        return result;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[]? Remove<T>(this T[] target, params T[] items)
    {
        if (target is null)
        {
            return null;
        }

        // NOTE: Why the `ToArray` call? Is `target` not already an array?
        var result = new List<T>(target.ToArray());
        var targetSpan = target.AsSpan();

        for (var index = 0; index < targetSpan.Length; index++)
        {
            ref var currentItem = ref targetSpan[index];
            if (items.Contains(currentItem))
            {
                result.Remove(currentItem);
            }
        }

        return result.ToArray();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[]? Remove<T>(this T[] target, IList<T> items)
    {
        if (target is null)
        {
            return null;
        }

        var result = new T[target.Length - items.Count];
        var targetSpan = target.AsSpan();
        var resultSpan = result.AsSpan();

        var offset = 0;
        for (var index = 0; index < targetSpan.Length; index++)
        {
            ref var currentItem = ref targetSpan[index];
            if (items.Contains(currentItem))
            {
                offset++;
                continue;
            }

            resultSpan[index - offset] = currentItem;
        }

        return result;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target"></param>
    /// <param name="item"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Remove<T>(this ref Span<T> target, T item)
    {
        var offset = 0;
        for (var index = 0; index < target.Length; index++)
        {
            ref var currentItem = ref target[index];
            if (currentItem.Equals(item))
            {
                offset++;
                continue;
            }

            target[index - offset] = currentItem;
        }
    }
}
