using Arch.Core.Utils;

namespace Arch.Core.CommandBuffer;

// NOTE: Does this really need to be nested?
/// <summary>
///     The <see cref="SparseEntity"/> struct
///    represents an <see cref="Entity"/> with its index in the <see cref="SparseSet"/>.
/// </summary>
internal readonly struct SparseEntity
{

    internal readonly Entity Entity;
    internal readonly int Index;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SparseEntity"/> struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/></param>
    /// <param name="index">The index inside the <see cref="SparseSet"/>.</param>
    public SparseEntity(Entity entity, int index)
    {
        Entity = entity;
        Index = index;
    }
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `SparseArray` sounds too generic for something that's only for `ComponentType`s.
/// <summary>
///     The see <see cref="SparseArray"/> class
///     stores components of a certain type in a sparse array.
/// </summary>
internal class SparseArray
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SparseArray"/> class
    ///     with the specified <see cref="ComponentType"/> and an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/>.</param>
    /// <param name="capacity">The initial capacity.</param>
    public SparseArray(ComponentType type, int capacity = 64)
    {
        Type = type;

        Capacity = capacity;
        Size = 0;
        Entities = new int[Capacity];
        Array.Fill(Entities, -1);
        Components = Array.CreateInstance(type, Capacity);
    }

    /// <summary>
    ///     Gets the <see cref="ComponentType"/> the <see cref="SparseArray"/> stores.
    /// </summary>
    public ComponentType Type { get; }

    /// <summary>
    ///     Gets the total number of elements the <see cref="SparseArray"/> can hold without resizing.
    /// </summary>
    public int Capacity { get; private set; }

    // NOTE: Should this be `Length` to follow the existing `Array` API?
    /// <summary>
    ///     Gets the total number of elements in the <see cref="SparseArray"/>.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     Gets or sets the indices of the stored <see cref="Entity"/> instances.
    /// </summary>
    public int[] Entities;

    /// <summary>
    ///     Gets an array of components contained by the <see cref="SparseArray"/>.
    /// </summary>
    public Array Components { get; private set; }

    /// <summary>
    ///     Adds an item to the array.
    /// </summary>
    /// <param name="index">Its index in the array.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int index)
    {
        lock (this)
        {
            // Resize entities
            if (index >= Entities.Length)
            {
                var length = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, length, index - length);
            }

            Entities[index] = Size;
            Size++;

            // Resize components
            if (Size < Components.Length)
            {
                return;
            }

            Capacity = Capacity <= 0 ? 1 : Capacity;
            var array = Array.CreateInstance(Type, Capacity * 2);

            Components.CopyTo(array, 0);
            Components = array;

            Capacity *= 2;
        }
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    /// <summary>
    ///     Checks if an component exists at the index.
    /// </summary>
    /// <param name="index">The index in the array.</param>
    /// <returns>True if an component exists there, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int index)
    {
        return index < Entities.Length && Entities[index] != -1;
    }

    // NOTE: If `SparseArray` were generic, this wouldn't have to exist, perhaps?
    /// <summary>
    ///     Return an array of the given type.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The array instance if it exists.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T[] GetArray<T>()
    {
        return Unsafe.As<T[]>(Components);
    }

    // NOTE: If `SparseArray` were generic, this could perhaps be an indexer (T this[int index]).
    /// <summary>
    ///     Sets a component at the index.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="index">The index.</param>
    /// <param name="component">The component instance.</param>
    public void Set<T>(int index, in T component)
    {
        lock (this)
        {
            GetArray<T>()[Entities[index]] = component;
        }
    }

    // NOTE: Should this be `ElementAt` to follow other existing .NET APIs (Enumerable.ElementAt)?
    // NOTE: If `SparseArray` were generic, this could perhaps be an indexer (T this[int index]).
    /// <summary>
    ///     Returns a reference to the component at the index.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="index">The index.</param>
    /// <returns>A reference to the component.</returns>
    public ref T Get<T>(int index)
    {
        return ref GetArray<T>()[Entities[index]];
    }

    /// <summary>
    ///     Clears this <see cref="SparseArray"/> instance and sets its <see cref="Size"/> to 0.
    /// </summary>
    public void Clear()
    {
        Size = 0;
    }
}


// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `SparseSet` sounds too generic for something that's only for `Entity`s.
// TODO: Tight array like in the structural `SparseSet` to avoid unnecessary iterations!!
/// <summary>
///     The <see cref="SparseSet"/> class
///     Stores a series of <see cref="SparseArray"/>'s and their associated components.
/// </summary>
internal class SparseSet : IDisposable
{
    private readonly object _createLock = new(); // Lock for create operations
    private readonly object _setLock = new();    // Lock for set operations

    /// <summary>
    ///     Initializes a new instance of the <see cref="SparseSet"/> class
    ///     with an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="capacity">The initial capacity.</param>
    public SparseSet(int capacity = 64)
    {
        Capacity = capacity;
        Entities = new List<SparseEntity>();

        UsedSize = 0;
        Used = Array.Empty<int>();
        Components = Array.Empty<SparseArray>();
    }

    /// <summary>
    ///     Gets the total number of elements the <see cref="SparseSet"/> initially can hold.
    /// </summary>
    public int Capacity { get; }

    // NOTE: Should this be `Count` to follow the existing `ICollection` API?
    /// <summary>
    ///     Gets the total number of elements in the <see cref="SparseSet"/>.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Gets a <see cref="List{T}"/> of all <see cref="SparseEntity"/> instances in the <see cref="SparseSet"/>.
    /// </summary>
    public List<SparseEntity> Entities { get; }

    /// <summary>
    ///     Gets the total number of <see cref="SparseArray"/> instances in the <see cref="SparseSet"/>.
    /// </summary>
    public int UsedSize { get; private set; }

    /// <summary>
    ///     Gets or sets an array containing used <see cref="SparseArray"/> indices.
    /// </summary>
    public int[] Used;

    /// <summary>
    ///     Gets or sets an array containing <see cref="SparseArray"/> instances.
    /// </summary>
    public SparseArray[] Components;

    /// <summary>
    ///     Adds an <see cref="Entity"/> to the <see cref="SparseSet"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>The index in the <see cref="SparseSet"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        lock (_createLock)
        {
            var id = Count;
            Entities.Add(new SparseEntity(entity, id));

            Count++;

            return id;
        }
    }

    // NOTE: If `SparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    /// <summary>
    ///     Sets a component at the index.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="index">The index.</param>
    /// <param name="component">The component instance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index, in T component)
    {
        var id = Component<T>.ComponentType.Id;
        lock (_setLock)
        {
            // Allocate new `SparseArray` for new component type.
            if (id >= Components.Length)
            {
                Array.Resize(ref Components, id + 1);
                Components[id] = new SparseArray(typeof(T), Capacity);

                Array.Resize(ref Used, UsedSize + 1);

                Used[UsedSize] = id;
                UsedSize++;
            }
        }

        // Add and set to `SparseArray`.
        var array = Components[id];
        lock (array)
        {
            if (!array.Contains(index))
            {
                array.Add(index);
            }
        }

        array.Set(index, in component);
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    /// <summary>
    ///     Checks if an component exists at the index.
    /// </summary>
    /// <param name="index">The index in the array.</param>
    /// <returns>True if an component exists there, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return array.Contains(index);
    }

    // NOTE: Should this be `ElementAt` to follow other existing .NET APIs (Enumerable.ElementAt)?
    // NOTE: If `SparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    /// <summary>
    ///     Returns a reference to the component at the index.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="index">The index.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T Get<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return ref array.Get<T>(index);
    }

    /// <summary>
    ///     Disposes the <see cref="SparseSet"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        Count = 0;
        Entities.Clear();

        foreach (var sparset in Components)
        {
            sparset?.Clear();
        }
    }
}
