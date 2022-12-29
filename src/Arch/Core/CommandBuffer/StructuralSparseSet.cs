using Arch.Core.Utils;

namespace Arch.Core.CommandBuffer;

/// <summary>
/// Represents an entity, combined to a internal sparset index id for fast lookups.
/// </summary>
public readonly struct StructuralEntity
{
    internal readonly Entity Entity;
    internal readonly int Index;

    public StructuralEntity(Entity entity, int index)
    {
        Entity = entity;
        Index = index;
    }
}

/// <summary>
/// A sparse array, an alternative to archetypes.
/// </summary>
internal class StructuralSparseArray : IDisposable
{
    public StructuralSparseArray(ComponentType type, int capacity = 64)
    {
        Type = type;
        Size = 0;
        Entities = new int[capacity];
        Array.Fill(Entities, -1);
    }

    /// <summary>
    /// The type this array stores.
    /// </summary>
    public ComponentType Type { get; }

    /// <summary>
    /// The total size.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// The entities / indexes. 
    /// </summary>
    public int[] Entities;

    /// <summary>
    /// Adds an entity to this sparse array.
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

    /// <summary>
    /// Returns true if this sparsearray contains an entity. 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int index)
    {
        return index < Entities.Length && Entities[index] != -1;
    }

    /// <summary>
    /// Disposes this array. 
    /// </summary>
    public void Dispose()
    {
        Size = 0;
    }
}

/// <summary>
/// A sparset which is an alternative to an archetype. Has some advantages, for example less copy around stuff and easier archetype changes. 
/// </summary>
internal class StructuralSparseSet : IDisposable
{
    private readonly object _createLock = new();
    private readonly object _setLock = new();

    public StructuralSparseSet(int capacity = 64)
    {
        InitialCapacity = capacity;
        Entities = new List<StructuralEntity>(capacity);
        Components = Array.Empty<StructuralSparseArray>();
    }

    /// <summary>
    /// The initial capacity of this set. 
    /// </summary>
    public int InitialCapacity { get; }

    /// <summary>
    /// The amount of entities.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// All entities within this set. 
    /// </summary>
    public List<StructuralEntity> Entities { get; private set; }

    /// <summary>
    /// Stores all used component indexes in a tightly packed array [5,1,10]
    /// </summary>
    public int[] Used;

    /// <summary>
    /// How many sparse arrays are actually in here. 
    /// </summary>
    public int UsedSize { get; private set; }

    /// <summary>
    /// The components / sparse arrays. 
    /// </summary>
    public StructuralSparseArray[] Components; // The components as a sparset so we can easily acess them via component ids

    /// <summary>
    /// Creates an entity inside this sparset. 
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

    /// <summary>
    /// Sets a component for an index. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
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

    /// <summary>
    /// Returns whether this index has a certain component or not. 
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has<T>(int index)
    {
        var id = Component<T>.ComponentType.Id;
        var array = Components[id];

        return array.Has(index);
    }

    /// <summary>
    /// Disposes this set. 
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
