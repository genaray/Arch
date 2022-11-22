using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

public static class ArrayExtensions
{
    /// <summary>
    /// Adds an element to an array and basically copies it. 
    /// </summary>
    /// <param name="target">The target array</param>
    /// <param name="item">The new item</param>
    /// <typeparam name="T">The types</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Add<T>(this T[] target, T item)
    {
        
        if (target == null)
            return null;
        
        var result = new T[target.Length + 1];
        target.CopyTo(result, 0);
        result[target.Length] = item;
        return result;
    }
    
    /// <summary>
    /// Removes an element to an array and shifts the other values.
    /// </summary>
    /// <param name="target">The target array</param>
    /// <param name="item">The new item</param>
    /// <typeparam name="T">The types</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Remove<T>(this T[] target, T item) 
    {
        
        if (target == null)
            return null;
        
        var result = new T[target.Length - 1];
        var targetSpan = target.AsSpan();
        var resultSpan = result.AsSpan();

        var offset = 0;
        for (var index = 0; index < targetSpan.Length; index++)
        {
            ref var currentItem = ref targetSpan[index];
            if (currentItem.Equals(item))
            {
                offset++;
                continue;
            }
            resultSpan[index - offset] = currentItem;
        }
      
        return result;
    }
    
    /// <summary>
    /// Adds an element to an array and basically copies it. 
    /// </summary>
    /// <param name="target">The target array</param>
    /// <param name="item">The new item</param>
    /// <typeparam name="T">The types</typeparam>
    /// <returns></returns>
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