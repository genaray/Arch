using Arch.Core.Utils;

namespace Arch.Core.Extensions;

// NOTE: Should this be `ComponentTypeExtensions`?
// NOTE: Should this really be an extension class? Why not simply add these methods to the `ComponentType` type directly?
// TODO: Documentation.
/// <summary>
///     The <see cref="TypeExtensions"/> class
///     ...
/// </summary>
public static class TypeExtensions
{
    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <param name="ids"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteComponentIds(this ComponentType[] types, Span<int> ids)
    {
        var typeSpan = new Span<ComponentType>(types);
        for (var index = 0; index < typeSpan.Length; index++)
        {
            ref var t = ref typeSpan[index];
            ids[index] = t.Id;
        }
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToByteSize(this ComponentType[] types)
    {
        var size = 0;
        foreach (var type in types)
        {
            var typeSize = type.ByteSize;
            typeSize = typeSize != 1 ? typeSize : 0; // Ignore tag components 
            size += typeSize;
        }

        return size;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int[] ToLookupArray(this ComponentType[] types)
    {
        // Get maximum component ID.
        var max = 0;
        foreach (var type in types)
        {
            var componentId = type.Id;
            if (componentId >= max)
            {
                max = componentId;
            }
        }

        // Create lookup table where the component ID points to the component index.
        var array = new int[max + 1];
        Array.Fill(array, -1);

        for (var index = 0; index < types.Length; index++)
        {
            ref var type = ref types[index];
            var componentId = type.Id;
            array[componentId] = index;
        }

        return array;
    }
}
