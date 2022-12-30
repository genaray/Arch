using System.Diagnostics.Contracts;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

/// <summary>
///     The <see cref="Chunk"/> struct represents a contiguous block of memory in which various components are stored in Structure of Arrays.
///     Chunks are internally allocated and filled by <see cref="Archetype"/>'s.
///     Through them it is possible to efficiently provide or trim memory for additional entities.
/// </summary>
public partial struct Chunk
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Chunk"/> struct.
    ///     Automatically creates a lookup array for quick access to internal components.
    /// </summary>
    /// <param name="capacity">How many entities of the respective component structure fit into this <see cref="Chunk"/>.</param>
    /// <param name="types">The respective component structure of all entities in this <see cref="Chunk"/>.</param>
    internal Chunk(int capacity, params ComponentType[] types)
        : this(capacity, types.ToLookupArray(), types) { }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Chunk"/> struct
    /// </summary>
    /// <param name="capacity">How many entities of the respective component structure fit into this <see cref="Chunk"/>.</param>
    /// <param name="componentIdToArrayIndex">A lookup array which maps the component id to the array index of the component array.</param>
    /// <param name="types">The respective component structure of all entities in this <see cref="Chunk"/>.</param>
    internal Chunk(int capacity, int[] componentIdToArrayIndex, params ComponentType[] types)
    {
        // Calculate capacity and init arrays.
        Size = 0;
        Capacity = capacity;

        Entities = new Entity[Capacity];
        Components = new Array[types.Length];

        // Init mapping.
        ComponentIdToArrayIndex = componentIdToArrayIndex;
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            Components[index] = Array.CreateInstance(type, Capacity);
        }
    }


    /// <summary>
    ///     The <see cref="Entity"/>'s that are stored in this chunk.
    ///     Can be accessed during the iteration.
    /// </summary>
    public readonly Entity[] Entities { [Pure] get; }

    /// <summary>
    ///     The component arrays in which the components of the <see cref="Entity"/>'s are stored.
    ///     Represent the component structure.
    ///     They can be accessed quickly using the <see cref="ComponentIdToArrayIndex"/> or one of the chunk methods.
    /// </summary>
    public readonly Array[] Components { [Pure] get; }

    /// <summary>
    ///     The lookup array that maps component ids to component array indexes to quickly access them.
    /// </summary>
    public readonly int[] ComponentIdToArrayIndex { [Pure] get; }

    /// <summary>
    ///     The number of occupied <see cref="Entity"/> slots in this <see cref="Chunk"/>.
    /// </summary>
    public int Size { [Pure] get; private set; }

    /// <summary>
    ///     The number of possible <see cref="Entity"/>'s in this <see cref="Chunk"/>.
    /// </summary>
    public int Capacity { [Pure] get; }

    /// <summary>
    ///     Inserts an entity into the <see cref="Chunk"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/> that will be inserted.</param>
    /// <returns>The index occupied by the <see cref="Entity"/> in the chunk.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int Add(in Entity entity)
    {
        Entities[Size] = entity;
        Size++;

        return Size - 1;
    }

    /// <summary>
    ///     Sets or replaces a component for an index in the chunk.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="index">The index in the array.</param>
    /// <param name="cmp">The component value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in int index, in T cmp)
    {
        var array = GetSpan<T>();
        array[index] = cmp;
    }

    /// <summary>
    ///     Checks if a component is included in this <see cref="Chunk"/>.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>True if included, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return id < ComponentIdToArrayIndex.Length && ComponentIdToArrayIndex[id] != 1;
    }

    /// <summary>
    ///     Returns a component from an index within the <see cref="Chunk"/>.
    /// </summary>
    /// <typeparam name="T">The generic type.</typeparam>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T Get<T>(scoped in int index)
    {
        var array = GetSpan<T>();
        return ref array[index];
    }

    /// <summary>
    ///     Removes the <see cref="Entity"/> at an index with all its components.
    ///     Copies the last <see cref="Entity"/> in its place to ensure a uniform array.
    /// </summary>
    /// <param name="index"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Remove(in int index)
    {
        // Last entity in archetype.
        var lastIndex = Size - 1;

        // Copy last entity to replace the removed one.
        Entities[index] = Entities[lastIndex];
        for (var i = 0; i < Components.Length; i++)
        {
            var array = Components[i];
            Array.Copy(array, lastIndex, array, index, 1);
        }

        // Update the mapping.
        Size--;
    }
}

/// <summary>
/// Provides some methods to access internal arrays using generics.
/// </summary>
public partial struct Chunk
{

    /// <summary>
    ///     Returns the component array index of a component.
    /// </summary>
    /// <typeparam name="T">The componen type.</typeparam>
    /// <returns>The index in the <see cref="Components"/> array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    private int Index<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return ComponentIdToArrayIndex[id];
    }

    /// <summary>
    ///     Returns the component array for a given component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public T[] GetArray<T>()
    {
        var index = Index<T>();
        return Unsafe.As<T[]>(Components[index]);
    }

    /// <summary>
    ///     Returns the component array <see cref="Span{T}"/> for a given component.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The array <see cref="Span{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpan<T>()
    {
        return new Span<T>(GetArray<T>(), 0, Size);
    }

    /// <summary>
    ///     Returns a reference to the first element of a component from its component array.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>A reference to the first element.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirst<T>()
    {
        return ref GetSpan<T>()[0]; // Span, to avoid bound checking for the [] operation.
    }

    /// <summary>
    ///     Returns the component array for a given component in an unsafe manner.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public T[] GetArrayUnsafe<T>()
    {
        var index = Index<T>();
        ref var array = ref Components.DangerousGetReferenceAt(index);
        return Unsafe.As<T[]>(array);
    }


    /// <summary>
    ///     Returns the component array <see cref="Span{T}"/> for a given component in an unsafe manner.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>The array <see cref="Span{T}"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpanUnsafe<T>()
    {
        return new Span<T>(GetArrayUnsafe<T>());
    }

    /// <summary>
    ///     Returns a reference to the first element of a component from its component array in an unsafe manner.
    /// </summary>
    /// <typeparam name="T">The component type.</typeparam>
    /// <returns>A reference to the first element.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirstUnsafe<T>()
    {
        return ref GetArrayUnsafe<T>().DangerousGetReference();
    }
}

/// <summary>
/// Provides some methods to access internal arrays using types.
/// </summary>
public partial struct Chunk
{

    /// <summary>
    ///     Checks if a component is included in this <see cref="Chunk"/>.
    /// </summary>
    /// <param name="t">The type.</param>
    /// <returns>True if included, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool Has(Type t)
    {
        var id = Component.GetComponentType(t).Id;
        if (id >= ComponentIdToArrayIndex.Length)
        {
            return false;
        }

        return ComponentIdToArrayIndex[id] != -1;
    }

    /// <summary>
    ///     Returns the component array index of a component by its type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The index in the <see cref="Components"/> array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    private int Index(Type type)
    {
        var id = Component.GetComponentType(type).Id;
        if (id >= ComponentIdToArrayIndex.Length)
        {
            return -1;
        }

        return ComponentIdToArrayIndex[id];
    }

    /// <summary>
    ///      Returns the component array for a given component type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The <see cref="Array"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Array GetArray(Type type)
    {
        var index = Index(type);
        return Components[index];
    }
}


/// <summary>
/// Provides several methods to copy or transfer data.
/// </summary>
public partial struct Chunk
{
    /// <summary>
    ///     Copies an entity at one index to another <see cref="Chunk"/>-index.
    ///     Only works for similar structures <see cref="Chunk"/>'s.
    /// </summary>
    /// <param name="index">The index of the <see cref="Entity"/> we want to copy.</param>
    /// <param name="toChunk">The chunk we want to move it to.</param>
    /// <param name="toIndex">The index we want to move it to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    internal void CopyToSimilar(int index, ref Chunk toChunk, int toIndex)
    {
        // Move/Copy components to the new chunk
        for (var i = 0; i < Components.Length; i++)
        {
            var sourceArray = Components[i];
            var desArray = toChunk.Components[i];
            Array.Copy(sourceArray, toIndex, desArray, index, 1);
        }
    }

    /// <summary>
    ///     Copies an entity at one index to another <see cref="Chunk"/>-index.
    /// </summary>
    /// <param name="index">The index of the <see cref="Entity"/> we want to copy.</param>
    /// <param name="toChunk">The chunk we want to move it to.</param>
    /// <param name="toIndex">The index we want to move it to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    internal void CopyToDifferent(ref Chunk toChunk, int index, int toIndex)
    {
        // Move/Copy components to the new chunk
        for (var i = 0; i < Components.Length; i++)
        {
            var sourceArray = Components[i];
            var sourceType = sourceArray.GetType().GetElementType();

            if (!toChunk.Has(sourceType))
            {
                continue;
            }

            var desArray = toChunk.GetArray(sourceType);
            Array.Copy(sourceArray, index, desArray, toIndex, 1);
        }
    }

    /// <summary>
    ///     Transfers an entity at the index of this chunk to another chunk.
    /// </summary>
    /// <param name="index">The index of the <see cref="Entity"/> we want to copy.</param>
    /// <param name="chunk">The <see cref="Chunk"/> we want to transfer it to.</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    internal int Transfer(int index, ref Chunk chunk)
    {
        // Get last entity
        var lastIndex = chunk.Size - 1;
        var lastEntity = chunk.Entities[lastIndex];

        // Replace index entity with the last entity from the other chunk
        Entities[index] = lastEntity;
        for (var i = 0; i < Components.Length; i++)
        {
            var sourceArray = chunk.Components[i];
            var desArray = Components[i];
            Array.Copy(sourceArray, lastIndex, desArray, index, 1);
        }

        chunk.Size--;
        return lastEntity.Id;
    }
}
