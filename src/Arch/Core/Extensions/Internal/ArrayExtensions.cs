namespace Arch.Core.Extensions.Internal;

/// <summary>
///     The <see cref="ArrayExtensions"/> class
///     adds several extensions methods for arrays and array related types.
/// </summary>
internal static class ArrayExtensions
{
    /// <summary>
    ///     Adds a list of items to an array.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The array of items which will be added.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T[] Add<T>(this T[] target, params T[] items) {

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
    internal static T[] Add<T>(this T[] target, IList<T> items)
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
    /// <param name="array">The target array.</param>
    /// <param name="toRemove">The <see cref="IList"/> of items which will be removed.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T[] Remove<T>(this T[] array, params T[] toRemove)
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
    ///     Gets the element at an index in the array, or resizes it to fit an element
    ///     at that index.
    /// </summary>
    /// <param name="array">The array to get the element from.</param>
    /// <param name="index">The index of the element.</param>
    /// <param name="exists">
    ///     Whether or not the index was within the bounds of the array.
    ///     When false, the array has been resized.
    /// </param>
    /// <param name="maxSize">The max size to grow the array to.</param>
    /// <typeparam name="T">The element type of the array.</typeparam>
    /// <returns>The element at that index. May be null.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ref T GetOrResize<T>(ref T[] array, int index, int? maxSize = null)
    {
        if (index < array.Length)
        {
            return ref array[index];
        }

        if (maxSize == null)
        {
            Array.Resize(ref array, (index * 2) + 1);
        }
        else
        {
            Array.Resize(ref array, Math.Min((index * 2) + 1, maxSize.Value));
        }

        return ref array[index];
    }

    /// <summary>
    ///     Removes a list of items from an array by value equality.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="target">The target array.</param>
    /// <param name="items">The <see cref="IList"/> of items which will be removed.</param>
    /// <returns>The new array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static T[] Remove<T>(this T[] array, IList<T> toRemove)
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
