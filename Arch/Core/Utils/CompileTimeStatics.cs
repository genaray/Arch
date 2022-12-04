using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core.Utils;

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
    public static Dictionary<Type, int> TypeIds { get; } = new(256);

    /// <summary>
    ///     The amount of registered components.
    /// </summary>
    public static int Size { get; set; }

    /// <summary>
    ///     Adds a component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Add<T>()
    {
        return Add(typeof(T));
    }

    /// <summary>
    ///     Adds a component.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Add(Type type)
    {
        if (TryGet(type, out var id)) return id;

        // Register and assign component id
        id = Size;
        TypeIds.Add(type, id);

        Size++;
        return id;
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
        return TypeIds.ContainsKey(type);
    }

    /// <summary>
    ///     Returns a component type id.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(out int index)
    {
        return TryGet(typeof(T), out index);
    }

    /// <summary>
    ///     Returns a component type id.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out int id)
    {
        return TypeIds.TryGetValue(type, out id);
    }
}

/// <summary>
///     A class which provides information about a component.
///     Gets created once during its first use and than provides informations like a compile static class.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ComponentMeta<T>
{
    public static readonly int Id;

    //FNV-1 64 bit hash
    static ComponentMeta()
    {
        Id = ComponentRegistry.Add<T>();
    }
}

/// <summary>
///     A class which provides information about a component.
///     Gets created once during its first use and than provides informations like a compile static class.
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ComponentMeta
{
    
    /// <summary>
    /// Returns the components id, based on its type. 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Id(Type type)
    {
        return !ComponentRegistry.TryGet(type, out var index) ? ComponentRegistry.Add(type) : index;
    }
    
    /// <summary>
    ///     Calculates the Hash Code of a Type Array by using its component ids and ignores different orders. 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(params Type[] obj)
    {
        
        // From https://stackoverflow.com/questions/28326965/good-hash-function-for-list-of-integers-where-order-doesnt-change-value
        unchecked
        {
            int hash = 0;
            foreach(var type in obj)
            {
                int x = Id(type)+1;

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

