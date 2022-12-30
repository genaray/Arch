using Arch.Core.Utils;

namespace Arch.Core.CommandBuffer;

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `SparseArray` sounds too generic for something that's only for `ComponentType`s.
// TODO: Documentation.
/// <summary>
///     The see <see cref="SparseArray"/> class
///     ...
/// </summary>
internal class SparseArray : IDisposable
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="SparseArray"/> class
    ///     with the specified <see cref="ComponentType"/> and an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="type"></param>
    /// <param name="capacity"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
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
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int index)
    {
        return index < Entities.Length && Entities[index] != -1;
    }

    // NOTE: If `SparseArray` were generic, this wouldn't have to exist, perhaps?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T[] GetArray<T>()
    {
        return Unsafe.As<T[]>(Components);
    }

    // NOTE: If `SparseArray` were generic, this could perhaps be an indexer (T this[int index]).
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="component"></param>
    public void Set<T>(int index, in T component)
    {
        lock (this)
        {
            GetArray<T>()[Entities[index]] = component;
        }
    }

    // NOTE: Should this be `ElementAt` to follow other existing .NET APIs (Enumerable.ElementAt)?
    // NOTE: If `SparseArray` were generic, this could perhaps be an indexer (T this[int index]).
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    public ref T Get<T>(int index)
    {
        return ref GetArray<T>()[Entities[index]];
    }

    // NOTE: Does this even need to exist? It doesn't release any resources or anything. Not really what `IDisposable` is for.
    /// <summary>
    ///     Disposes the <see cref="SparseArray"/>.
    /// </summary>
    public void Dispose()
    {
        Size = 0;
    }
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `SparseSet` sounds too generic for something that's only for `Entity`s.
// TODO: Tight array like in the structural `SparseSet` to avoid unnecessary iterations!!
// TODO: Documentation.
/// <summary>
///     The <see cref="SparseSet"/> class
///     ...
/// </summary>
internal class SparseSet : IDisposable
{
    // NOTE: Does this really need to be nested?
    // TODO: Documentation.
    /// <summary>
    ///     The <see cref="WrappedEntity"/> struct
    ///     ...
    /// </summary>
    internal readonly struct WrappedEntity
    {
        // TODO: Documentation.
        /// <summary>
        ///     Initializes a new instance of the <see cref="WrappedEntity"/> struct
        ///     ...
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="index"></param>
        public WrappedEntity(Entity entity, int index)
        {
            Entity = entity;
            Index = index;
        }

        // TODO: Documentation.
        internal readonly Entity Entity;
        internal readonly int Index;
    }

    private readonly object _createLock = new(); // Lock for create operations
    private readonly object _setLock = new();    // Lock for set operations

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="SparseSet"/> class
    ///     with an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="capacity"></param>
    public SparseSet(int capacity = 64)
    {
        InitialCapacity = capacity;
        Entities = new List<WrappedEntity>();

        UsedSize = 0;
        Used = Array.Empty<int>();
        Components = Array.Empty<SparseArray>();
    }

    // NOTE: Should this be just `Capacity`?
    /// <summary>
    ///     Gets the total number of elements the <see cref="SparseSet"/> can hold.
    /// </summary>
    public int InitialCapacity { get; }

    // NOTE: Should this be `Count` to follow the existing `ICollection` API?
    /// <summary>
    ///     Gets the total number of elements in the <see cref="SparseSet"/>.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     Gets a <see cref="List{T}"/> of all <see cref="WrappedEntity"/> instances in the <see cref="SparseSet"/>.
    /// </summary>
    public List<WrappedEntity> Entities { get; }

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

    // TODO: Documentation.
    /// <summary>
    ///     Adds an <see cref="Entity"/> to the <see cref="SparseSet"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        lock (_createLock)
        {
            var id = Size;
            Entities.Add(new WrappedEntity(entity, id));

            Size++;

            return id;
        }
    }

    // NOTE: If `SparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="component"></param>
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
                Components[id] = new SparseArray(typeof(T), InitialCapacity);

                Array.Resize(ref Used, UsedSize + 1);

                Used[UsedSize] = id;
                UsedSize++;
            }
        }

        // Add and set to `SparseArray`.
        var array = Components[id];
        lock (array)
        {
            if (!array.Has(index))
            {
                array.Add(index);
            }
        }

        array.Set(index, in component);
    }

    // NOTE: Should this be `Contains` to follow other existing .NET APIs (ICollection<T>.Contains(T))?
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return array.Has(index);
    }

    // NOTE: Should this be `ElementAt` to follow other existing .NET APIs (Enumerable.ElementAt)?
    // NOTE: If `SparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return array.Get<T>(index);
    }

    /// <summary>
    ///     Disposes the <see cref="SparseSet"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        Size = 0;
        Entities.Clear();

        foreach (var sparset in Components)
        {
            sparset?.Dispose();
        }
    }
}
