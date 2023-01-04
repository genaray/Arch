using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core.Utils;

/// <summary>
/// Represents a component with its meta informations. 
/// </summary>
public readonly struct ComponentType
{
    public readonly int Id;
    public readonly Type Type;

    public readonly int ByteSize;
    public readonly bool ZeroSized;

    public ComponentType(int id, Type type, int byteSize, bool zeroSized)
    {
        Id = id;
        Type = type;
        ByteSize = byteSize;
        ZeroSized = zeroSized;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComponentType(Type value)
    {
        return Component.GetComponentType(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Type(ComponentType value)
    {
        return value.Type;
    }
}

/// <summary>
///     A class which tracks all used components in this project.
///     Component-Ids start at 0 and each new used component will get an increased id.
///     TODO : Probably components should start at 1 instead, since the hash and chunk.Has would work way smoother with it. 
/// </summary>
public static class ComponentRegistry
{
    /// <summary>
    ///     A list of all registered components.
    /// </summary>
    private static Dictionary<Type, ComponentType> _types { get; } = new(128);

    /// <summary>
    /// Returns all registered types as a newly allocated array.
    /// </summary>
    public static ComponentType[] Types => _types.Values.ToArray();
    
    /// <summary>
    /// The amount of registered components.
    /// </summary>
    public static int Size { get; set; }

    /// <summary>
    ///     Adds a component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add<T>()
    {
        return Add(typeof(T));
    }

    /// <summary>
    ///     Adds a component.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add(Type type)
    {
         if (TryGet(type, out var meta)) return meta;
        
        // Register and assign component id
        var size = Marshal.SizeOf(type);
        meta = new ComponentType(Size, type, size, size - 1 <= 0);
        _types.Add(type, meta);

        Size++;
        return meta;
    }

    /// <summary>
    ///     Checks if a component exists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>()
    {
        return Has(typeof(T));
    }

    /// <summary>
    ///     Checks if a component exists.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has(Type type)
    {
        return _types.ContainsKey(type);
    }

    /// <summary>
    ///     Returns a component type id.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(out ComponentType componentType)
    {
        return TryGet(typeof(T), out componentType);
    }

    /// <summary>
    ///     Returns a component type id.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out ComponentType componentType)
    {
        return _types.TryGetValue(type, out componentType);
    }
}

/// <summary>
///     A class which provides information about a component.
///     Gets created once during its first use and than provides informations like a compile static class.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Component<T>
{
    public static readonly ComponentType ComponentType = ComponentRegistry.Add<T>();
}

/// <summary>
///     A class which provides information about a component.
///     Gets created once during its first use and than provides informations like a compile static class.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class Component
{
    
    /// <summary>
    /// Returns the components id, based on its type. 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType GetComponentType(Type type)
    {
        return !ComponentRegistry.TryGet(type, out var index) ? ComponentRegistry.Add(type) : index;
    }
    
    /// <summary>
    ///     Calculates the Hash Code of a Type Array by using its component ids and ignores different orders. 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(params ComponentType[] obj)
    {
        
        // From https://stackoverflow.com/questions/28326965/good-hash-function-for-list-of-integers-where-order-doesnt-change-value
        unchecked
        {
            int hash = 0;
            foreach(var type in obj)
            {
                int x = type.Id+1;

                x ^= x >> 17;
                x *= 830770091;   // 0xed5ad4bb
                x ^= x >> 11;
                x *= -1404298415; // 0xac4c1b51
                x ^= x >> 15;
                x *= 830770091;   // 0x31848bab
                x ^= x >> 14;

                hash += x;
            }

            return hash;
        }
    }
    
    /// <summary>
    ///     Calculates the Hash Code of a Type Array by using its component ids and ignores different orders. 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(Span<int> obj)
    {
        
        // From https://stackoverflow.com/questions/28326965/good-hash-function-for-list-of-integers-where-order-doesnt-change-value
        unchecked
        {
            int hash = 0;
            foreach(var type in obj)
            {
                int x = type+1;

                x ^= x >> 17;
                x *= 830770091;   // 0xed5ad4bb
                x ^= x >> 11;
                x *= -1404298415; // 0xac4c1b51
                x ^= x >> 15;
                x *= 830770091;   // 0x31848bab
                x ^= x >> 14;

                hash += x;
            }

            return hash;
        }
    }
}


/// <summary>
/// Compile static class that acts as a counter. 
/// </summary>
public static class JobMeta
{
    internal static int Id;
}

/// <summary>
/// Compile static class that counts each generic overload, provides an id, a policy and a pool for it. 
/// </summary>
/// <typeparam name="T"></typeparam>
public static class JobMeta<T> where T : class, new()
{

    public static readonly int Id;
    public static readonly DefaultObjectPolicy<T> Policy;
    public static readonly DefaultObjectPool<T> Pool;

    static JobMeta()
    {
        Id = JobMeta.Id++;
        Policy = new DefaultObjectPolicy<T>();
        Pool = new DefaultObjectPool<T>(Policy);
    }
}

// TODO : Based on the hash of each Group we can easily Map a Group<T,T,T..> to another Group... Like Group<int, byte> to Group<byte,int> since they are the same based on the hash actually. 
public static class Group
{
    internal static int Id;
}

