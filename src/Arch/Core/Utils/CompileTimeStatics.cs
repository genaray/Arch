using Microsoft.Extensions.ObjectPool;

namespace Arch.Core.Utils;

/// <summary>
///     The <see cref="ComponentType"/> struct, represents a component with some information about it.
///     A component labels an <see cref="Entity"/> as possessing a particular aspect, and holds the data needed to model that aspect.
///     For example, every game object that can take damage might have a Health component associated with its <see cref="Entity"/>.
///     Is created by compile time static or during runtime, look at the <see cref="ComponentRegistry"/>.
/// </summary>
public readonly struct ComponentType
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentType"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="type">Its type.</param>
    /// <param name="byteSize">Its size in bytes.</param>
    /// <param name="zeroSized">True if its zero sized ( empty struct).</param>
    public ComponentType(int id, Type type, int byteSize, bool zeroSized)
    {
        Id = id;
        Type = type;
        ByteSize = byteSize;
        ZeroSized = zeroSized;
    }

    /// <summary>
    ///     Represents a unique Id for this component.
    /// </summary>
    public readonly int Id;

    /// <summary>
    ///     Its type.
    /// </summary>
    public readonly Type Type;

    /// <summary>
    ///     Its size in bytes.
    /// </summary>
    public readonly int ByteSize;

    /// <summary>
    ///     If its zero sized.
    /// </summary>
    public readonly bool ZeroSized;

    /// <summary>
    ///     Converts a <see cref="Type"/> to its <see cref="ComponentType"/>.
    /// </summary>
    /// <param name="value">The type that is being converted.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator ComponentType(Type value)
    {
        return Component.GetComponentType(value);
    }

    /// <summary>
    ///     Converts the <see cref="ComponentType"/> to its original <see cref="Type"/>.
    /// </summary>
    /// <param name="value">The type that is being converted.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Type(ComponentType value)
    {
        return value.Type;
    }
}

// TODO: Components should start at 1 instead, since the hash and `Chunk.Has` would work smoother that way.
/// <summary>
///     The <see cref="ComponentRegistry"/> class, tracks all used components in the project.
///     Those are represented by <see cref="ComponentType"/>'s.
/// </summary>
public static class ComponentRegistry
{

    /// <summary>
    ///     All registered components, maps their <see cref="Type"/> to their <see cref="ComponentType"/>.
    /// </summary>
    private static readonly Dictionary<Type, ComponentType> _types = new(128);

    /// <summary>
    ///     All registered components mapped to their <see cref="Type"/> as a <see cref="Dictionary{TKey,TValue}"/>.
    /// </summary>
    public static Dictionary<Type, ComponentType> Types
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _types;
    }

    /// <summary>
    ///     TODO: Store array somewhere and update it to reduce allocations.
    ///     All registered components as an <see cref="ComponentType"/> array.
    /// </summary>
    public static ComponentType[] TypesArray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _types.Values.ToArray();
    }

    /// <summary>
    ///     Gets or sets the total number of registered components in the project.
    /// </summary>
    public static int Size { get; private set; }

    /// <summary>
    ///     Adds a new component and registers it.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add<T>()
    {
        return Add(typeof(T));
    }

    /// <summary>
    ///     Adds a new component and registers it.
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add(Type type)
    {
        if (TryGet(type, out var meta))
        {
            return meta;
        }

        // Register and assign component id
        var size = type.IsValueType ? Marshal.SizeOf(type) : IntPtr.Size;
        meta = new ComponentType(Size, type, size, type.GetFields().Length == 0);
        _types.Add(type, meta);

        Size++;
        return meta;
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    /// <summary>
    ///     Checks if a component is registered.
    /// </summary>
    /// <typeparam name="T">Its generic type.</typeparam>
    /// <returns>True if it is, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has<T>()
    {
        return Has(typeof(T));
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    /// <summary>
    ///      Checks if a component is registered.
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <returns>True if it is, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has(Type type)
    {
        return _types.ContainsKey(type);
    }

    /// <summary>
    ///     Removes a registered component by its <see cref="Type"/> from the <see cref="ComponentRegistry"/>.
    /// </summary>
    /// <typeparam name="T">The component to remove.</typeparam>
    /// <returns>True if it was sucessfull, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Remove<T>()
    {
        return _types.Remove(typeof(T));
    }

    /// <summary>
    ///     Removes a registered component by its <see cref="Type"/> from the <see cref="ComponentRegistry"/>.
    /// </summary>
    /// <param name="type">The component <see cref="Type"/> to remove.</param>
    /// <returns>True if it was sucessfull, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Remove(Type type)
    {
        return _types.Remove(type);
    }

    /// <summary>
    ///     Replaces a registered component by its <see cref="Type"/> with another one.
    ///     The new <see cref="Type"/> will receive the id from the old one.
    ///     <remarks>Use with caution, might cause undefined behaviour if you do not know what exactly you are doing.</remarks>
    /// </summary>
    /// <typeparam name="T0">The old component to be replaced.</typeparam>
    /// <typeparam name="T1">The new component that replaced the old one.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Replace<T0,T1>()
    {
        var oldType = typeof(T0);
        var newType = typeof(T1);
        Replace(oldType, newType);
    }

    /// <summary>
    ///     Replaces a registered component by its <see cref="Type"/> with another one.
    ///     The new <see cref="Type"/> will receive the id from the old one.
    ///     <remarks>Use with caution, might cause undefined behaviour if you do not know what exactly you are doing.</remarks>
    /// </summary>
    /// <param name="oldType">The old component <see cref="Type"/> to be replaced.</param>
    /// <param name="newType">The new component <see cref="Type"/> that replaced the old one.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Replace(Type oldType, Type newType)
    {
        var id = 0;
        if (TryGet(oldType, out var oldComponentType))
        {
            id = oldComponentType.Id;
            _types.Remove(oldType);
        }
        else
        {
            id = Size;
            Size++;
        }

        var size = newType.IsValueType ? Marshal.SizeOf(newType) : IntPtr.Size;
        _types.Add(newType, new ComponentType(id, newType, size, newType.GetFields().Length == 0));
    }

    /// <summary>
    ///     Trys to get a component if it is registered.
    /// </summary>
    /// <typeparam name="T">Its generic type.</typeparam>
    /// <param name="componentType">Its <see cref="ComponentType"/>, if it is registered.</param>
    /// <returns>True if it registered, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet<T>(out ComponentType componentType)
    {
        return TryGet(typeof(T), out componentType);
    }

    /// <summary>
    ///     Trys to get a component if it is registered.
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <param name="componentType">Its <see cref="ComponentType"/>, if it is registered.</param>
    /// <returns>True if it registered, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out ComponentType componentType)
    {
        return _types.TryGetValue(type, out componentType);
    }
}

/// <summary>
///     The <see cref="Component{T}"/> class, provides compile time static information about a component.
/// </summary>
/// <typeparam name="T">Its generic type.</typeparam>
/// <remarks>
///     A <see cref="Component{T}"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class Component<T>
{
    /// <summary>
    ///     Creates the compile time static class for acessing its information.
    ///     Registers the component.
    /// </summary>
    static Component()
    {
        ComponentType = ComponentRegistry.Add<T>();
    }

    /// <summary>
    ///     A static reference to information about the compile time static registered class.
    /// </summary>
    public static readonly ComponentType ComponentType;
}

/// <summary>
///     The <see cref="Component"/> class provides information about a component during runtime.
/// </summary>
/// <remarks>
///     A <see cref="Component"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class Component
{
    /// <summary>
    ///     Searches a <see cref="ComponentType"/> by its <see cref="Type"/>. If it does not exist, it will be added.
    /// </summary>
    /// <param name="type">The <see cref="Type"/>.</param>
    /// <returns>The <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType GetComponentType(Type type)
    {
        return !ComponentRegistry.TryGet(type, out var index) ? ComponentRegistry.Add(type) : index;
    }

    /// <summary>
    ///     Calculates the hash code of a <see cref="ComponentType"/> array, which is unique for the elements contained in the array.
    ///     The order of the elements does not change the hashcode, so it depends on the elements themselves.
    /// </summary>
    /// <param name="obj">The <see cref="ComponentType"/> array.</param>
    /// <returns>A unique hashcode for the contained elements, regardless of their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(Span<ComponentType> obj)
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

    /// <summary>
    ///     Calculates the hash code of a <see cref="ComponentType"/> Id array, which is unique for the elements contained in the array.
    ///     The order of the elements does not change the hashcode, so it depends on the elements themselves.
    /// </summary>
    /// <param name="obj">The <see cref="ComponentType"/> array.</param>
    /// <returns>A unique hashcode for the contained elements, regardless of their order.</returns>
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

    /// <summary>
    ///     Calculates the hash code of a <see cref="BitSet"/>, which is unique for the elements contained in the array.
    ///     The order of the elements does not change the hashcode, so it depends on the elements themselves.
    /// </summary>
    /// <param name="obj">The <see cref="BitSet"/>.</param>
    /// <returns>A unique hashcode for the contained elements, regardless of their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(BitSet obj)
    {
        // From https://stackoverflow.com/a/52172541.
        unchecked
        {
            var span = obj.AsSpan();
            int hash = 0;
            for (var index = 0; index < span.Length; index++)
            {
                var value = span[index];
                for (var i = 0; i < BitSet.BitSize; i++)
                {
                    if ((value & 1) != 1)
                    {
                        continue;
                    }

                    int x = (index*BitSet.BitSize)+i + 1;

                    x ^= x >> 17;
                    x *= 830770091;   // 0xed5ad4bb
                    x ^= x >> 11;
                    x *= -1404298415; // 0xac4c1b51
                    x ^= x >> 15;
                    x *= 830770091;   // 0x31848bab
                    x ^= x >> 14;

                    hash += x;
                    value >>= 1;
                }
            }

            return hash;
        }
    }

    /// <summary>
    ///     Calculates the hash code of a <see cref="SpanBitSet"/>, which is unique for the elements contained in the array.
    ///     The order of the elements does not change the hashcode, so it depends on the elements themselves.
    /// </summary>
    /// <param name="obj">The <see cref="SpanBitSet"/>.</param>
    /// <returns>A unique hashcode for the contained elements, regardless of their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(ref SpanBitSet obj)
    {
        // From https://stackoverflow.com/a/52172541.
        unchecked
        {
            var span = obj.AsSpan();
            int hash = 0;
            for (var index = 0; index < span.Length; index++)
            {
                var value = span[index];
                for (var i = 0; i < BitSet.BitSize; i++)
                {
                    if ((value & 1) != 1)
                    {
                        value >>= 1;
                        continue;
                    }

                    int x = (index*BitSet.BitSize)+i + 1;

                    x ^= x >> 17;
                    x *= 830770091;   // 0xed5ad4bb
                    x ^= x >> 11;
                    x *= -1404298415; // 0xac4c1b51
                    x ^= x >> 15;
                    x *= 830770091;   // 0x31848bab
                    x ^= x >> 14;

                    hash += x;
                    value >>= 1;
                }
            }

            return hash;
        }
    }
}

// NOTE: Rename or reimplement this? An entire class just for counting something seems overkill.
/// <summary>
///     The <see cref="JobMeta"/> class counts Id's for internally registered jobs during compile time.
/// </summary>
public static class JobMeta
{
    internal static int Id;
}

/// <summary>
///     The <see cref="JobMeta{T}"/> class registers each job during compiletime to ensure static access to some information, which is more efficient.
/// </summary>
/// <typeparam name="T">The job struct generic type..</typeparam>
/// /// <remarks>
///     A <see cref="JobMeta{T}"/> is created once during its first use.
///     Subsequent uses access statically stored information.
/// </remarks>
public static class JobMeta<T> where T : class, new()
{
    /// <summary>
    ///     Creates a compiletime static instance of this job.
    /// </summary>
    static JobMeta()
    {
        Id = JobMeta.Id++;
        Policy = new DefaultObjectPolicy<T>();
        Pool = new DefaultObjectPool<T>(Policy);
    }

    /// <summary>
    ///     The unique Id of the job.
    /// </summary>
    public static readonly int Id;

    /// <summary>
    ///     The pool policy of the registered job.
    ///     Used for <see cref="Pool"/>.
    /// </summary>
    public static readonly DefaultObjectPolicy<T> Policy;

    /// <summary>
    ///     The pool of the job.
    ///     So that during multithreading new jobs are not permanently associated, which is better for efficiency.
    /// </summary>
    public static readonly DefaultObjectPool<T> Pool;
}

// TODO: Based on the hash of each `Group` we can easily Map a `Group<T, T, T, ...>` to another `Group`.
//       E.g.: `Group<int, byte>` to `Group<byte, int>`, as they return the same hash.
/// <summary>
///     The <see cref="Group"/> class counts the Ids of registered groups in an compiletime static way.
/// </summary>
public static class Group
{
    internal static int Id;
}
