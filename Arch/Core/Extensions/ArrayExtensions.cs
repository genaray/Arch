using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arch.Core.Extensions; 

public static class ArrayExtensions {

    public static void EnsureCapacity(this Array array, in int newCapacity) {
            
        var oldSize = array.Length;
        var elementType = array.GetType().GetElementType();
        var newArray = Array.CreateInstance(elementType, newCapacity);
        var preserveLength = Math.Min(oldSize, newCapacity);
                
        if (preserveLength > 0)
            Array.Copy (array,newArray,preserveLength);
    }
    
    public static int ToByteSize(this Type[] types) {

        var size = 0;
        foreach (var type in types) {

            if (!type.IsValueType)
                throw new ArgumentException("Chunk is only allowed to contain value types");

            size += Marshal.SizeOf(type);
        }

        return size;
    }
}