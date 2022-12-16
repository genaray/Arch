using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

public static class TypeExtensions
{

    /// <summary>
    /// Converts the types to their component ids and writes them into the span.
    /// </summary>
    /// <param name="types">The types array.</param>
    /// <param name="ids">The span in which the type component ids should be written.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteComponentIds(this ComponentType[] types, Span<int> ids)
    {
        var typeSpan = new Span<ComponentType>(types);
        for (var index = 0; index < typeSpan.Length; index++)
        {
            ref var t = ref typeSpan[index];
            ids[index] = t.Id;
        }
    }
    
    /// <summary>
    ///     Calculates the byte sum of the types.
    /// </summary>
    /// <param name="types">The types array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if one type is not a value type</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToByteSize(this ComponentType[] types)
    {
        var size = 0;
        foreach (var type in types)
        {
            var typeSize = type.ByteSize;
            typeSize = typeSize != 1 ? typeSize : 0;  // Ignore tag components 
            size += typeSize;
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
    public static int[] ToLookupArray(this ComponentType[] types)
    {
        // Get max component id
        var max = 0;
        foreach (var type in types)
        {
            var componentId = type.Id;
            if (componentId >= max) max = componentId;
        }

        // Create lookup table where the component-id points to the component index. 
        var array = new int[max+1];
        Array.Fill(array, -1);  // -1 Since that indicates no component is in that index since components start at zero we can not use zero here. 
        for(var index = 0; index < types.Length; index++)
        {
            ref var type = ref types[index];
            var componentId = type.Id;
            array[componentId] = index;
        }
        return array;
    }
}