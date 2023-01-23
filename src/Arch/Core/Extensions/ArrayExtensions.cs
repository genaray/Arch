using CommunityToolkit.HighPerformance;

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
    ///     Removes a item from the <see cref="Span{T}"/> and returns a slice without the removed item.
    /// </summary>
    /// <param name="target">The target <see cref="Span{T}"/>.</param>
    /// <param name="item">The item to remove.</param>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>A <see cref="Span{T}"/> without the removed item.</returns>
    public static void Remove<T>(this ref Span<T> target, T item) where T : IEquatable<T>?
    {
        var index = target.IndexOf(item);
        if (index == -1)
        {
            return;
        }

        target[index] = target[^1];
        target = target[..index];
    }


    /// <summary>
    ///     Removes a list of items from an array by value equality.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="array">The target array.</param>
    /// <param name="toRemove">The <see cref="IList"/> of items which will be removed.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Remove<T>(this T[] array, params T[] toRemove)
    {
        // Count how many items exist in target array to remove
        var count = 0;
        var arraySpan = array.AsSpan();
        for (var index = 0; index < arraySpan.Length; index++)
        {
            ref var item = ref arraySpan[index];
            var itemIndex = Array.IndexOf<T>(toRemove, item);
            if (itemIndex > -1)
            {
                count++;
            }
        }

        // Walk over the array again and just copy items into result array which are NOT in the passed items array.
        var result = new T[arraySpan.Length - count];
        count = 0;
        for (var index = 0; index < arraySpan.Length; index++)
        {
            ref var item = ref arraySpan[index];
            var itemIndex = Array.IndexOf<T>(toRemove, item);
            if (itemIndex > -1)
            {
                continue;
            }

            result[count] = item;
            count++;
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
    public static T[] Remove<T>(this T[] array, IList<T> toRemove)
    {
        // Count how many items exist in target array to remove
        var count = 0;
        var arraySpan = array.AsSpan();
        for (var index = 0; index < arraySpan.Length; index++)
        {
            ref var item = ref arraySpan[index];
            var itemIndex = toRemove.IndexOf(item);
            if (itemIndex > -1)
            {
                count++;
            }
        }

        // Walk over the array again and just copy items into result array which are NOT in the passed items array.
        var result = new T[arraySpan.Length - count];
        count = 0;
        for (var index = 0; index < arraySpan.Length; index++)
        {
            ref var item = ref arraySpan[index];
            var itemIndex = toRemove.IndexOf(item);
            if (itemIndex > -1)
            {
                continue;
            }

            result[count] = item;
            count++;
        }

        return result;
    }
}
