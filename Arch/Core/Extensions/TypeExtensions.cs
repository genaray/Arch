using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

public static class TypeExtensions
{
    
    public static Dictionary<Type, Func<bool>> IsReferenceOrContainsReferenceCache { get; set; } = new(256);

    /// <summary>
    /// Converts the types to their component ids and writes them into the span.
    /// </summary>
    /// <param name="types">The types array.</param>
    /// <param name="ids">The span in which the type component ids should be written.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteComponentIds(this Type[] types, Span<int> ids)
    {
        var typeSpan = new Span<Type>(types);
        for (var index = 0; index < typeSpan.Length; index++)
        {
            var t = typeSpan[index];
            ids[index] = ComponentMeta.Id(t);
        }
    }
    
    /// <summary>
    ///     Calculates the byte sum of the types.
    /// </summary>
    /// <param name="types">The types array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if one type is not a value type</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToByteSize(this Type[] types)
    {
        var size = 0;
        foreach (var type in types)
        {
            if (!type.IsValueType)
                throw new ArgumentException("Cant determine size of non value type.");

            size += Marshal.SizeOf(type);
        }

        return size;
    }
    
    /// <summary>
    ///     Calculates the byte sum of the types.
    /// </summary>
    /// <param name="types">The types array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if one type is not a value type</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int[] ToLookupArray(this Type[] types)
    {
        
        // Get max component id
        var max = 0;
        foreach (var type in types)
        {
            var componentId = ComponentMeta.Id(type);
            if (componentId >= max) max = componentId;
        }

        // Create lookup table where the component-id points to the component index. 
        var array = new int[max+1];
        Array.Fill(array, -1);  // -1 Since that indicates no component is in that index since components start at zero we can not use zero here. 
        for(var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            var componentId = ComponentMeta.Id(type);
            array[componentId] = index;
        }
        return array;
    }
}