using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Arch.Core.Extensions;

public static class ArrayExtensions {

    /// <summary>
    /// Calculates the byte sum of the types. 
    /// </summary>
    /// <param name="types">The types array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if one type is not a value type</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToByteSize(this IEnumerable<Type> types) {

        var size = 0;
        foreach (var type in types) {

            if (!type.IsValueType)
                throw new ArgumentException("Cant determine size of non value type.");

            size += Marshal.SizeOf(type);
        }

        return size;
    }
    
    /// <summary>
    /// Calculates the byte sum of the types. 
    /// </summary>
    /// <param name="types">The types array</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws an exception if one type is not a value type</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OffsetTo(this IEnumerable<Type> types, Type type, int capacity) {

        var offset = 0;
        foreach (var currentType in types) {

            if (!currentType.IsValueType)
                throw new ArgumentException("Cant determine size of non value type.");

            if (currentType == type) return offset;
            offset += Marshal.SizeOf(currentType) * capacity ;
        }

        return offset;
    }

    /// <summary>
    /// Checks wether a passed type is managed or not using a cached type version of <see cref="RuntimeHelpers.IsReferenceOrContainsReferences{T}"/>
    /// </summary>
    /// <param name="type">The type, must be struct.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsManaged(Type type) {

        // Use cache
        if (IsReferenceOrContainsReferenceCache.TryGetValue(type, out var func))
            return func();
        
        // Cache for type
        var methodInfo = typeof(RuntimeHelpers).GetMethod("IsReferenceOrContainsReferences");
        var genericMethod = methodInfo.MakeGenericMethod(type);
        var funcDelegate = (Func<bool>)genericMethod.CreateDelegate(typeof(Func<bool>));
        IsReferenceOrContainsReferenceCache[type] = funcDelegate;

        return funcDelegate();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Categorize(this Type[] types, out List<Type> managed, out List<Type> unmanaged) {

        managed = new List<Type>(types.Length);
        unmanaged = new List<Type>(types.Length);
        foreach (var type in types) {
            
            if(!IsManaged(type)) unmanaged.Add(type);
            else managed.Add(type);
        }
    }

    public static Dictionary<Type, Func<bool>> IsReferenceOrContainsReferenceCache { get; set; } = new(256);
}