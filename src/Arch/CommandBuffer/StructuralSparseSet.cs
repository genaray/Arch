using Arch.Core;
using Arch.Core.Utils;

namespace Arch.CommandBuffer;

/// <summary>
///     The <see cref="StructuralEntity"/> struct
///     represents an <see cref="Entity"/> with its index in the <see cref="StructuralSparseSet"/>.
/// </summary>
public readonly struct StructuralEntity
{
    internal readonly Entity Entity;
    internal readonly int Index;

    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralEntity"/> struct.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="index">Its index in its <see cref="StructuralSparseSet"/>.</param>
    public StructuralEntity(Entity entity, int index)
    {
        Entity = entity;
        Index = index;
    }
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `StructuralSparseArray` sounds too generic for something that's only for `ComponentType`s.
/// <summary>
///     The see <see cref="StructuralSparseArray"/> class
///      stores components of a certain type in a sparse array.
///     It does not store its values however, its more like a registration mechanism.
/// </summary>
internal class StructuralSparseArray
{

    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralSparseArray"/> class
    ///     with the specified <see cref="ComponentType"/> and an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="type">Its <see cref="ComponentType"/>.</param>
    /// <param name="capacity">Its initial capacity.</param>
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
                var lenght = Entities.Length;
                Array.Resize(ref Entities, index + 1);
                Array.Fill(Entities, -1, lenght, index - lenght);
            }

            Entities[index] = Size;
            Size++;
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

    /// <summary>
    ///     Clears this <see cref="SparseArray"/> instance and sets its <see cref="Size"/> to 0.
    /// </summary>
    public void Clear()
    {
        for (var index = 0; index < Entities.Length; index++)
        {
            Entities[index] = -1;
        }
        Size = 0;
    }
}

// NOTE: Why not a generic type?
// NOTE: Should this have a more descriptive name? `StructuralSparseSet` sounds too generic for something that's only for `Entity`s.
/// <summary>
///     The <see cref="StructuralSparseSet"/> class
///     stores a series of <see cref="StructuralSparseArray"/>'s and their associated components.
/// </summary>
internal class StructuralSparseSet
{
    private readonly object _createLock = new();
    private readonly object _setLock = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="StructuralSparseSet"/> class
    ///     with an optional initial <paramref name="capacity"/> (default: 64).
    /// </summary>
    /// <param name="capacity">Its initial capacity.</param>
    public StructuralSparseSet(int capacity = 64)
    {
        Capacity = capacity;
        Entities = new List<StructuralEntity>(capacity);
        Used = Array.Empty<int>();
        Components = Array.Empty<StructuralSparseArray>();
    }

    /// <summary>
    ///     Gets the total number of elements the <see cref="StructuralSparseSet"/> initially can hold.
    /// </summary>
    public int Capacity { get; }

    /// <summary>
    ///     Gets the total number of elements in the <see cref="StructuralSparseSet"/>.
    /// </summary>
    public int Count { get; private set; }

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

    /// <summary>
    ///     Ensures the capacity for registered components types.
    ///     Resizes the existing <see cref="Components"/> array properly to fit the id in.
    ///     <remarks>Does not ensure the capacity in terms of how many operations or components are recorded.</remarks>
    /// </summary>
    /// <param name="capacity">The new capacity, the id of the component which will be ensured to fit into the arrays.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureTypeCapacity(int capacity)
    {
        // Resize arrays
        if (capacity < Components.Length)
        {
            return;
        }
        Array.Resize(ref Components, capacity + 1);
    }
    /// <summary>
    ///     Ensures the capacity for the <see cref="Used"/> array.
    /// </summary>
    /// <param name="capacity">The new capacity.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void EnsureUsedCapacity(int capacity)
    {
        // Resize UsedSize array.
        if (capacity < UsedSize)
        {
            return;
        }
        Array.Resize(ref Used, UsedSize + 1);
    }

    /// <summary>
    ///     Adds an <see cref="StructuralSparseArray"/> to the <see cref="Components"/> list and updates the <see cref="Used"/> properly.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> of the <see cref="StructuralSparseArray"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AddStructuralSparseArray(ComponentType type)
    {
        Components[type.Id] = new StructuralSparseArray(type, type.Id);

        Used[UsedSize] = type.Id;
        UsedSize++;
    }

    /// <summary>
    ///     Checks whether a <see cref="StructuralSparseArray"/> for a certain <see cref="ComponentType"/> exists in the <see cref="Components"/>.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> to check.</param>
    /// <returns>True if it does, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool HasStructuralSparseArray(ComponentType type)
    {
        return Components[type.Id] != null;
    }

    /// <summary>
    ///     Returns the existing <see cref="StructuralSparseArray"/> for the registered <see cref="ComponentType"/>.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/>.</param>
    /// <returns>The existing <see cref="StructuralSparseArray"/> instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StructuralSparseArray GetStructuralSparseArray(ComponentType type)
    {
        return Components[type.Id];
    }

    /// <summary>
    ///     Adds an <see cref="Entity"/> to the <see cref="StructuralSparseSet"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <returns>Its index in this <see cref="StructuralSparseSet"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Create(in Entity entity)
    {
        lock (_createLock)
        {
            var id = Count;
            Entities.Add(new StructuralEntity(entity, id));

            Count++;
            return id;
        }
    }

    // NOTE: If `StructuralSparseSet` were generic, this could perhaps be an indexer (T this[int index]).
    /// <summary>
    ///     Sets a component at the index.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <param name="index">The index.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(int index)
    {
        var componentType = Component<T>.ComponentType;
        lock (_setLock)
        {
            // Ensure that enough space for the additional component type array exists and add it if it does not exist yet.
            EnsureTypeCapacity(componentType.Id);
            if (!HasStructuralSparseArray(componentType))
            {
                EnsureUsedCapacity(UsedSize+1);
                AddStructuralSparseArray(componentType);
            }
        }

        // Add to array.
        var array = GetStructuralSparseArray(componentType);
        lock (array)
        {
            if (!array.Contains(index))
            {
                array.Add(index);
            }
        }
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

    /// <summary>
    ///     Clears the <see cref="StructuralSparseSet"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Count = 0;
        Entities.Clear();

        foreach (var sparset in Components)
        {
            sparset?.Clear();
        }
    }
}
