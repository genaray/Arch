namespace Arch.Core.Extensions;

/// <summary>
///     The <see cref="ArrayExtensions"/> class
///     adds several extensions methods for arrays and array related types.
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    ///     Adds a list of items to an array.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The array of items which will be added.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Add<T>(this T[] target, params T[] items) {

        var result = new T[target.Length + items.Length];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Length; index++)
        {
            result[target.Length + index] = items[index];
        }

        return result;
    }

    /// <summary>
    ///     Adds a list of items to an array.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The <see cref="IList"/> of items which will be added.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Add<T>(this T[] target, IList<T> items)
    {
        var result = new T[target.Length + items.Count];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Count; index++)
        {
            result[target.Length + index] = items[index];
        }

        return result;
    }

    /// <summary>
    ///     Removes a list of items from an array by value equality.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The <see cref="IList"/> of items which will be removed.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Remove<T>(this T[] target, params T[] items)
    {
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

    /// <summary>
    ///     Removes a list of items from an array by value equality.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The <see cref="IList"/> of items which will be removed.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Remove<T>(this T[] target, IList<T> items)
    {
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

    /// <summary>
    ///     Removes an item from an <see cref="Span"/> by value equality.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target <see cref="Span"/>.</param>
    /// <param name="item">The item.</param>
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
