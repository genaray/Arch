using Microsoft.Extensions.ObjectPool;

namespace Arch.Core.Utils;

// TODO: Documentation. Be more specific about what a "component" truly is.
/// <summary>
///     The <see cref="ComponentType"/> struct
///     represents a component with its meta information.
/// </summary>
public readonly struct ComponentType
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentType"/> struct
    ///     ...
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <param name="byteSize"></param>
    /// <param name="zeroSized"></param>
    public ComponentType(int id, Type type, int byteSize, bool zeroSized)
    {
        Id = id;
        Type = type;
        ByteSize = byteSize;
        ZeroSized = zeroSized;
    }

    // TODO: Documentation.
    public readonly int Id;
    public readonly Type Type;
    public readonly int ByteSize;
    public readonly bool ZeroSized;

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComponentType(Type value)
    {
        return Component.GetComponentType(value);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Type(ComponentType value)
    {
        return value.Type;
    }
}

// TODO: Components should start at 1 instead, since the hash and `Chunk.Has` would work smoother that way.
/// <summary>
///     The <see cref="ComponentRegistry"/> class
///     tracks all used components in the project.
///     Component IDs start at 0 and increase by one for each new component.
/// </summary>
public static class ComponentRegistry
{
    private static readonly Dictionary<Type, ComponentType> _types = new(128);

    // NOTE: Could this be optimized by editing the array as values get added?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public static ComponentType[] Types
    {
        get => _types.Values.ToArray();
    }

    /// <summary>
    ///     Gets or sets the total number of registered components in the project.
    /// </summary>
    public static int Size { get; set; }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add<T>()
    {
        return Add(typeof(T));
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add(Type type)
    {
        Debug.Assert(type.IsValueType, $"Only value type components are useable, '{type.Name}' is not a primitive nor a struct.");
        if (TryGet(type, out var meta))
        {
            return meta;
        }

        // Register and assign component id
        var size = Marshal.SizeOf(type);
        meta = new ComponentType(Size, type, size, type.GetFields().Length == 0);
        _types.Add(type, meta);

        Size++;
        return meta;
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>()
    {
        return Has(typeof(T));
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has(Type type)
    {
        return _types.ContainsKey(type);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="componentType"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(out ComponentType componentType)
    {
        return TryGet(typeof(T), out componentType);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="componentType"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out ComponentType componentType)
    {
        return _types.TryGetValue(type, out componentType);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="Component{T}"/> class
///     provides information about a component.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
///     A <see cref="Component{T}"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class Component<T>
{
    // TODO: Documentation?
    /// <summary>
    /// 
    /// </summary>
    static Component()
    {
        ComponentType = ComponentRegistry.Add<T>();
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    public static readonly ComponentType ComponentType;
}

/// <summary>
///     The <see cref="Component"/> class
///     provides information about a component.
/// </summary>
/// <remarks>
///     A <see cref="Component"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class Component
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType GetComponentType(Type type)
    {
        return !ComponentRegistry.TryGet(type, out var index) ? ComponentRegistry.Add(type) : index;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(params ComponentType[] obj)
    {
        // From https://stackoverflow.com/a/52172541.
        unchecked
        {
            int hash = 0;
            foreach (var type in obj)
            {
                int x = type.Id + 1;

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(Span<int> obj)
    {
        // From https://stackoverflow.com/a/52172541.
        unchecked
        {
            int hash = 0;
            foreach (var type in obj)
            {
                int x = type + 1;

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

// NOTE: Rename or reimplement this? An entire class just for counting something seems overkill.
// TODO: Documentation.
/// <summary>
///     The <see cref="JobMeta"/> class
///     ...
/// </summary>
public static class JobMeta
{
    internal static int Id;
}

// TODO: Documentation.
/// <summary>
///     The <see cref="JobMeta{T}"/> class
///     ...
/// </summary>
/// <typeparam name="T"></typeparam>
public static class JobMeta<T> where T : class, new()
{
    // TODO: Documentation?
    /// <summary>
    /// 
    /// </summary>
    static JobMeta()
    {
        Id = JobMeta.Id++;
        Policy = new DefaultObjectPolicy<T>();
        Pool = new DefaultObjectPool<T>(Policy);
    }

    // TODO: Documentation.
    public static readonly int Id;
    public static readonly DefaultObjectPolicy<T> Policy;
    public static readonly DefaultObjectPool<T> Pool;
}

// TODO: Based on the hash of each `Group` we can easily Map a `Group<T, T, T, ...>` to another `Group`.
//       E.g.: `Group<int, byte>` to `Group<byte, int>`, as they return the same hash.
/// <summary>
///     The <see cref="Group"/> class
///     ...
/// </summary>
public static class Group
{
    internal static int Id;
}
