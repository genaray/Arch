using System;
using System.Collections.Generic;

namespace Arch.Core.Utils;

/// <summary>
/// A class which tracks all used components in this project. 
/// </summary>
public static class ComponentRegistry {

    /// <summary>
    /// Adds a component.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int Add<T>() {
        return Add(typeof(T));
    }
    
    /// <summary>
    /// Adds a component.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int Add(Type type) {

        if (Has(type)) return Get(type);
        
        // Register and assign component id
        var id = Size;
        TypeIds.Add(type, id);
        
        Size++;
        return id;
    }

    /// <summary>
    /// Checks if a component exists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool Has<T>() {
        return Has(typeof(T));
    }
    
    /// <summary>
    /// Checks if a component exists.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool Has(Type type) {
        return TypeIds.ContainsKey(type);
    }

    /// <summary>
    /// Returns a component type id.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int Get<T>() {
        return Get(typeof(T));
    }

    /// <summary>
    /// Returns a component type id.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static int Get(Type type) {
        return TypeIds[type];
    }

    /// <summary>
    /// A list of all registered components.
    /// </summary>
    public static Dictionary<Type, int> TypeIds { get; } = new(256);
    
    /// <summary>
    /// The amount of registered components. 
    /// </summary>
    public static int Size { get; set; }
}

/// <summary>
/// A class which provides information about a component.
/// Gets created once during its first use and than provides informations like a compile static class. 
/// </summary>
/// <typeparam name="T"></typeparam>
public static partial class Component<T> {
    
    public static readonly int Id;

    //FNV-1 64 bit hash
    static Component() {
        Id = ComponentRegistry.Add<T>();
    }
}

/// <summary>
/// A class which provides information about a component.
/// Gets created once during its first use and than provides informations like a compile static class. 
/// </summary>
/// <typeparam name="T"></typeparam>
public static partial class Component {

    public static int Id(Type type) {

        if (ComponentRegistry.Has(type))
            return ComponentRegistry.Get(type);

        return ComponentRegistry.Add(type);
    }
}
