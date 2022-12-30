using System.Diagnostics.Contracts;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using CommunityToolkit.HighPerformance;

namespace Arch.Core;

// TODO: Documentation.
/// <summary>
///     The <see cref="Chunk"/> struct
///     ...
/// </summary>
public partial struct Chunk
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Chunk"/> struct
    ///     ...
    /// </summary>
    /// <param name="capacity"></param>
    /// <param name="types"></param>
    internal Chunk(int capacity, params ComponentType[] types)
        : this(capacity, types.ToLookupArray(), types) { }

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="Chunk"/> struct
    ///     ...
    /// </summary>
    /// <param name="capacity"></param>
    /// <param name="componentIdToArrayIndex"></param>
    /// <param name="types"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int Add(in Entity entity)
    {
        Entities[Size] = entity;
        Size++;

        return Size - 1;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <param name="cmp"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set<T>(in int index, in T cmp)
    {
        var array = GetSpan<T>();
        array[index] = cmp;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public bool Has<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return id < ComponentIdToArrayIndex.Length && ComponentIdToArrayIndex[id] != 1;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="index"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T Get<T>(scoped in int index)
    {
        var array = GetSpan<T>();
        return ref array[index];
    }

    // TODO: Documentation.
    /// <summary>
    /// 
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

    // TODO: Documentation.
    public readonly Entity[] Entities { [Pure] get; }
    public readonly Array[] Components { [Pure] get; }
    public readonly int[] ComponentIdToArrayIndex { [Pure] get; }
    public int Size { [Pure] get; private set; }
    public int Capacity { [Pure] get; }
}

public partial struct Chunk
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    private int Index<T>()
    {
        var id = Component<T>.ComponentType.Id;
        return ComponentIdToArrayIndex[id];
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public T[] GetArray<T>()
    {
        var index = Index<T>();
        return Unsafe.As<T[]>(Components[index]);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpan<T>()
    {
        return new Span<T>(GetArray<T>(), 0, Size);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirst<T>()
    {
        return ref GetSpan<T>()[0]; // Span, to avoid bound checking for the [] operation.
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public T[] GetArrayUnsafe<T>()
    {
        var index = Index<T>();
        ref var array = ref Components.DangerousGetReferenceAt(index);
        return Unsafe.As<T[]>(array);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Span<T> GetSpanUnsafe<T>()
    {
        return new Span<T>(GetArrayUnsafe<T>());
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public ref T GetFirstUnsafe<T>()
    {
        return ref GetArrayUnsafe<T>().DangerousGetReference();
    }
}

public partial struct Chunk
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public Array GetArray(Type type)
    {
        var index = Index(type);
        return Components[index];
    }
}

public partial struct Chunk
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="toChunk"></param>
    /// <param name="toIndex"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="toChunk"></param>
    /// <param name="index"></param>
    /// <param name="toIndex"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="chunk"></param>
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
