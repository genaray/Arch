using System;
using System.Collections.Generic;
using System.Linq;
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
    public static T[] Add<T>(this T[] target, params T[] items)
    {
        
        if (target == null)
            return null;
        
        var result = new T[target.Length + items.Length];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Length; index++)
            result[target.Length+index] = items[index];
        
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
    public static T[] Add<T>(this T[] target, IList<T> items)
    {
        
        if (target == null)
            return null;
        
        var result = new T[target.Length + items.Count];
        target.CopyTo(result, 0);

        for (var index = 0; index < items.Count; index++)
            result[target.Length+index] = items[index];
        
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
    public static T[] Remove<T>(this T[] target, params T[] items) 
    {
        
        if (target == null)
            return null;
        
        var result = new T[target.Length - items.Length];
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
    /// Removes an element to an array and shifts the other values.
    /// </summary>
    /// <param name="target">The target array</param>
    /// <param name="item">The new item</param>
    /// <typeparam name="T">The types</typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Remove<T>(this T[] target, IList<T> items) 
    {
        
        if (target == null)
            return null;
        
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
    /// Removes an element from an array and moves the other ones to its position. 
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