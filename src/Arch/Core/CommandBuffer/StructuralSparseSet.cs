using Arch.Core.Utils;

namespace Arch.Core.CommandBuffer;

// TODO: Documentation.
/// <summary>
///     The <see cref="StructuralEntity"/> struct
///     ...
/// </summary>
public readonly struct StructuralEntity
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralEntity"/> struct
    ///     ...
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="index"></param>
    public StructuralEntity(Entity entity, int index)
    {
        Entity = entity;
        Index = index;
    }

    // TODO: Documentation.
    internal readonly Entity Entity;
    internal readonly int Index;
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `StructuralSparseArray` sounds too generic for something that's only for `ComponentType`s.
// TODO: Documentation.
/// <summary>
///     The see <see cref="StructuralSparseArray"/> class
///     ...
/// </summary>
internal class StructuralSparseArray : IDisposable
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralSparseArray"/> class
    ///     with the specified <see cref="ComponentType"/> and an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="type"></param>
    /// <param name="capacity"></param>
    public StructuralSparseArray(ComponentType type, int capacity = 64)
    {
        Type = type;
        Size = 0;
        Entities = new int[capacity];
        Array.Fill(Entities, -1);
    }

    /// <summary>
    ///     Gets the <see cref="ComponentType"/> the <see cref="StructuralSparseArray"/> stores.
    /// </summary>
    public ComponentType Type { get; }

    // NOTE: Should this be `Length` to follow the existing `Array` API?
    /// <summary>
    ///     Gets the total number of elements in the <see cref="StructuralSparseArray"/>.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     Gets or sets the indices of the stored <see cref="Entity"/> instances.
    /// </summary>
    public int[] Entities;

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
                var lenght = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, lenght, index - lenght);
            }

            Entities[index] = Size;
            Size++;
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

    // NOTE: Does this even need to exist? It doesn't release any resources or anything. Not really what `IDisposable` is for.
    /// <summary>
    ///     Disposes the <see cref="StructuralSparseArray"/>.
    /// </summary>
    public void Dispose()
    {
        Size = 0;
    }
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `StructuralSparseSet` sounds too generic for something that's only for `Entity`s.
// TODO: Documentation.
/// <summary>
///     The <see cref="StructuralSparseSet"/> class
///     ...
/// </summary>
internal class StructuralSparseSet : IDisposable
{
    private readonly object _createLock = new();
    private readonly object _setLock = new();

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralSparseSet"/> class
    ///     with an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="capacity"></param>
    public StructuralSparseSet(int capacity = 64)
    {
        InitialCapacity = capacity;
        Entities = new List<StructuralEntity>(capacity);
        Components = Array.Empty<StructuralSparseArray>();
    }

    // NOTE: Should this be just `Capacity`?
    /// <summary>
    ///     Gets the total number of elements the <see cref="StructuralSparseSet"/> can hold.
    /// </summary>
    public int InitialCapacity { get; }

    // NOTE: Should this be `Count` to follow the existing `ICollection` API?
    /// <summary>
    ///     Gets the total number of elements in the <see cref="StructuralSparseSet"/>.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    ///     Gets a <see cref="List{T}"/> of all <see cref="StructuralEntity"/> instances in the <see cref="StructuralSparseSet"/>.
    /// </summary>
    public List<StructuralEntity> Entities { get; private set; }

    /// <summary>
    ///     Gets the total number of <see cref="StructuralSparseArray"/> instances in the <see cref="StructuralSparseSet"/>.
    /// </summary>
    public int UsedSize { get; private set; }

    /// <summary>
    ///     Gets or sets an array containing used <see cref="StructuralSparseArray"/> indices.
    /// </summary>
    public int[] Used;

    /// <summary>
    ///     Gets or sets an array containing <see cref="StructuralSparseArray"/> instances.
    /// </summary>
    public StructuralSparseArray[] Components; // The components as a `SparseSet` so we can easily access them via component IDs.

    // TODO: Documentation.
    /// <summary>
    ///     Adds an <see cref="Entity"/> to the <see cref="StructuralSparseSet"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        lock (_createLock)
        {
            var id = Size;
            Entities.Add(new StructuralEntity(entity, id));

            Size++;

            return id;
        }
    }

    // NOTE: If `StructuralSparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;

        lock (_setLock)
        {
            // Allocate new sparsearray for component and resize arrays 
            if (id >= Components.Length)
            {
                Array.Resize(ref Used, UsedSize + 1);
                Array.Resize(ref Components, id + 1);

                Components[id] = new StructuralSparseArray(typeof(T), InitialCapacity);

                Used[UsedSize] = id;
                UsedSize++;
            }
        }

        var array = Components[id];
        lock (array)
        {
            if (!array.Has(index))
            {
                array.Add(index);
            }
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
    public bool Has<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return array.Has(index);
    }

    /// <summary>
    ///     Disposes the <see cref="StructuralSparseSet"/>.
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
