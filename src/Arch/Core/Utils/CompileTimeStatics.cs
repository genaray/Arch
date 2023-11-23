using Arch.Core.Extensions.Internal;
using Arch.LowLevel.Jagged;
using Microsoft.Extensions.ObjectPool;

namespace Arch.Core.Utils;

/// <summary>
///     The <see cref="ComponentType"/> struct, represents a component with some information about it.
///     A component labels an <see cref="Entity"/> as possessing a particular aspect, and holds the data needed to model that aspect.
///     For example, every game object that can take damage might have a Health component associated with its <see cref="Entity"/>.
///     Is created by compile time static or during runtime, look at the <see cref="ComponentRegistry"/>.
/// </summary>
public readonly record struct ComponentType
{

    /// <summary>
    ///     Represents a unique Id for this component.
    /// </summary>
    public readonly int Id;

    /// <summary>
    ///     Its size in bytes.
    /// </summary>
    public readonly int ByteSize;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentType"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="byteSize">Its size in bytes.</param>
    public ComponentType(int id, int byteSize)
    {
        Id = id;
        ByteSize = byteSize;
    }

    /// <summary>
    ///     Its <see cref="Type"/>, resolves the given <see cref="Id"/>.
    ///     <remarks>The local <see cref="Id"/> is being resolved and acesses the <see cref="ComponentRegistry.Types"/> to return the <see cref="Type"/> for this <see cref="ComponentType"/>.</remarks>
    /// </summary>
    public Type Type
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ComponentRegistry.Types[Id]!;
    }

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
/// <remarks>
///     Simultaneous readers are supported, but simultaneous readers and writers are not.
///     Ensure that modification happens on an isolated thread.
///     In <see cref="World"/> this is implemented via marked structural-change methods.
/// </remarks>
public static class ComponentRegistry
{
    private static readonly Dictionary<Type, ComponentType> _typeToComponentType = new(64);
    private static Type?[] _types = new Type[64];

    /// <summary>
    ///     All registered components, maps their <see cref="Type"/> to their <see cref="ComponentType"/>.
    /// </summary>
    public static IReadOnlyDictionary<Type, ComponentType> TypeToComponentType
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _typeToComponentType;
    }

    /// <summary>
    ///     All registered components.
    /// </summary>
    public static ReadOnlySpan<Type?> Types
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _types;
    }

    /// <summary>
    ///     Gets or sets the total number of registered components in the project.
    /// </summary>
    public static int Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private set;
    }

    /// <summary>
    ///     Adds a new <see cref="ComponentType"/> manually and registers it.
    ///     <remarks>You should only be using this when you exactly know what you are doing.</remarks>
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <param name="typeSize">The size in bytes of <see cref="type"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ComponentType Add(Type type, int typeSize)
    {
        if (TryGet(type, out var meta))
        {
            return meta;
        }

        // Register and assign component id
        var id = Size + 1;
        meta = new ComponentType(id, typeSize);
        _typeToComponentType.Add(type, meta);
        _types = _types.Add(id, type);

        Size++;
        return meta;
    }

    /// <summary>
    ///     Adds a new <see cref="ComponentType"/> manually and registers it.
    ///     <remarks>You should only be using this when you exactly know what you are doing.</remarks>
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add(ComponentType type)
    {
        // Register and assign component id
        _typeToComponentType.Add(type, type);
        _types = _types.Add(type.Id, type.Type);

        Size++;
        return type;
    }

    /// <summary>
    ///     Adds a new component and registers it.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add<T>()
    {
        return Add(typeof(T), SizeOf<T>());
    }

    /// <summary>
    ///     Adds a new component and registers it.
    /// </summary>
    /// <param name="type">Its <see cref="Type"/>.</param>
    /// <returns>Its <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType Add(Type type)
    {
        return Add(type, SizeOf(type));
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    /// <summary>
    ///     Checks if a component is registered.
    /// </summary>
    /// <typeparam name="T">Its generic type.</typeparam>
    /// <returns>True if it is, otherwise false.</returns>
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
    /// <returns>True if it is, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Has(Type type)
    {
        return TypeToComponentType.ContainsKey(type);
    }

    /// <summary>
    ///     Removes a registered component by its <see cref="Type"/> from the <see cref="ComponentRegistry"/>.
    /// </summary>
    /// <typeparam name="T">The component to remove.</typeparam>
    /// <returns>True if it was successful, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Remove<T>()
    {
        var componentType = Component<T>.ComponentType;
        _types[componentType.Id] = null;
        return _typeToComponentType.Remove(componentType.Type);
    }

    /// <summary>
    ///     Removes a registered component by its <see cref="Type"/> from the <see cref="ComponentRegistry"/>.
    /// </summary>
    /// <param name="type">The component <see cref="Type"/> to remove.</param>
    /// <returns>True if it was successful, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Remove(Type type)
    {
        ComponentType componentType = type;
        _types[componentType.Id] = null;
        return _typeToComponentType.Remove(type);
    }

    /// <summary>
    ///     Removes a registered component by its <see cref="Type"/> from the <see cref="ComponentRegistry"/>.
    /// </summary>
    /// <param name="type">The component <see cref="Type"/> to remove.</param>
    /// <param name="compType">The removed <see cref="ComponentType"/>, if it existed.</param>
    /// <returns>True if it was successful, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Remove(Type type, out ComponentType compType)
    {
        var removed = _typeToComponentType.Remove(type, out compType);
        _types[compType.Id] = null;
        return removed;
    }

    /// <summary>
    ///     Replaces a registered component by its <see cref="Type"/> with another one.
    ///     The new <see cref="Type"/> will receive the id from the old one.
    ///     <remarks>Use with caution, might cause undefined behaviour if you do not know what exactly you are doing.</remarks>
    /// </summary>
    /// <param name="oldType">The old component <see cref="Type"/> to be replaced.</param>
    /// <param name="newType">The new component <see cref="Type"/> that replaced the old one.</param>
    /// <param name="newTypeSize">The size in bytes of <see cref="newType"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Replace(Type oldType, Type newType, int newTypeSize)
    {
        var id = Remove(oldType, out var oldComponentType) ? oldComponentType.Id : ++Size;

        _typeToComponentType.Add(newType, new ComponentType(id, newTypeSize));
        _types = _types.Add(id, newType);
    }

    /// <summary>
    ///     Replaces a registered component by its <see cref="Type"/> with another one.
    ///     The new <see cref="Type"/> will receive the id from the old one.
    ///     <remarks>Use with caution, might cause undefined behaviour if you do not know what exactly you are doing.</remarks>
    /// </summary>
    /// <typeparam name="T0">The old component to be replaced.</typeparam>
    /// <typeparam name="T1">The new component that replaced the old one.</typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Replace<T0, T1>()
    {
        Replace(typeof(T0), typeof(T1), SizeOf<T1>());
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
        Replace(oldType, newType, SizeOf(newType));
    }

    /// <summary>
    ///     Trys to get a component if it is registered.
    /// </summary>
    /// <typeparam name="T">Its generic type.</typeparam>
    /// <param name="componentType">Its <see cref="ComponentType"/>, if it is registered.</param>
    /// <returns>True if it registered, otherwise false.</returns>
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
    /// <returns>True if it registered, otherwise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGet(Type type, out ComponentType componentType)
    {
        return TypeToComponentType.TryGetValue(type, out componentType);
    }

    /// <summary>
    ///     Returns the size in bytes of the passed generic.
    /// </summary>
    /// <typeparam name="T">The generic.</typeparam>
    /// <returns>Its size.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int SizeOf<T>()
    {
        return typeof(T).IsValueType ? Unsafe.SizeOf<T>() : IntPtr.Size;
    }

    /// TODO: Check if this still AOT compatible?
    /// <summary>
    ///     Returns the size in bytes of the passed type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Its size in bytes.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int SizeOf(Type type)
    {
        if (type.IsValueType)
        {
            return (int)typeof(Unsafe)
                .GetMethod(nameof(Unsafe.SizeOf))!
                .MakeGenericMethod(type)
                .Invoke(null, null)!;
        }

        return IntPtr.Size;
    }
}

/// <summary>
///     Tracks all registered arrays in the project. Allows to create arrays of a specific type without reflection at runtime, if they are registered.
/// </summary>
public static class ArrayRegistry
{
    private static readonly JaggedArray<Func<int, Array>> _createFactories = new(128);

    /// <summary>
    ///     Adds a new array type and registers it.
    /// </summary>
    /// <typeparam name="T">The type of the array.</typeparam>
    public static void Add<T>()
    {
        _createFactories.Add(Component<T>.ComponentType.Id, ArrayFactory<T>.Create);
    }

    /// <summary>
    ///     Gets an array of the specified type and capacity. Will use the registered factory if it exists, otherwise it will create a new array using reflection.
    /// </summary>
    /// <param name="type">The type of the array.</param>
    /// <param name="capacity">The capacity of the array.</param>
    /// <returns>The created array.</returns>
    public static Array GetArray(ComponentType type, int capacity)
    {
        return _createFactories.TryGetValue(type.Id, out Func<int, Array> func) ? func(capacity) : Array.CreateInstance(type.Type, capacity);
    }

    /// <summary>
    ///     An array factory that creates arrays of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the array.</typeparam>
    private static class ArrayFactory<T>
    {
        public static readonly Func<int, Array> Create = capacity => capacity == 0 ? Array.Empty<T>() : new T[capacity];
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
    /// <remarks>
    ///     Not thread-safe; ensure no other threads are accessing or modifying the <see cref="ComponentRegistry"/>.
    /// </remarks>
    /// <param name="type">The <see cref="Type"/>.</param>
    /// <returns>The <see cref="ComponentType"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComponentType GetComponentType(Type type)
    {
        return !ComponentRegistry.TryGet(type, out var index) ? ComponentRegistry.Add(type) : index;
    }

    /// TODO : Find a nicer way? Probably cache hash somewhere in Query or Description instead to avoid calculating it every call?
    /// <summary>
    ///     Calculates the hash code of a <see cref="ComponentType"/> array, which is unique for the elements contained in the array.
    ///     The order of the elements does not change the hashcode, so it depends on the elements themselves.
    /// </summary>
    /// <param name="obj">The <see cref="ComponentType"/> array.</param>
    /// <returns>A unique hashcode for the contained elements, regardless of their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(Span<ComponentType> obj)
    {
          // Search for the highest id to determine how much uints we need for the stack.
          var highestId = 0;
          foreach (ref var cmp in obj)
          {
              if (cmp.Id > highestId)
              {
                  highestId = cmp.Id;
              }
          }

          // Allocate the stack and set bits to replicate a bitset
          var length = BitSet.RequiredLength(highestId + 1);
          Span<uint> stack = stackalloc uint[length];
          var spanBitSet = new SpanBitSet(stack);

          foreach (ref var type in obj)
          {
              var x = type.Id;
              spanBitSet.SetBit(x);
          }

          return GetHashCode(stack);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetHashCode(Span<uint> span)
    {
        var hashCode = new HashCode();
        hashCode.AddSpan(span);
        return hashCode.ToHashCode();
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
///     The <see cref="Group"/> class counts the IDs of registered <see cref="ComponentType"/> groups in an compile-time static way,
///     and stores an underlying array for dynamic access. In this way, its related classes (<see cref="Group{T0}"/>, <see cref="Group{T0, T1}"/>...)
///     can be used to statically track sets of components from generic calls with zero overhead.
/// </summary>
public static class Group
{
    internal static int Id;
}
