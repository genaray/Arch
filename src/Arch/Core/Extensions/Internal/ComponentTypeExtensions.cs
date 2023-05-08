using Arch.Core.Utils;

namespace Arch.Core.Extensions.Internal;

// NOTE: Should this really be an extension class? Why not simply add these methods to the `ComponentType` type directly?
/// <summary>
///     The <see cref="ComponentTypeExtensions"/> class
///     adds several extension methods for <see cref="ComponentType"/>.
/// </summary>
internal static class ComponentTypeExtensions
{

    /// <summary>
    ///     Calculates the byte size of all components inside the <see cref="ComponentType"/> array.
    /// </summary>
    /// <param name="types">The <see cref="ComponentType"/> array.</param>
    /// <returns>Their combined byte size.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int ToByteSize(this ComponentType[] types)
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

    /// <summary>
    ///     Converts a <see cref="ComponentType"/> array into a lookup array where each <see cref="ComponentType"/> Id points towards its index.
    /// </summary>
    /// <param name="types">The <see cref="ComponentType"/> array.</param>
    /// <returns>The lookup array.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static int[] ToLookupArray(this ComponentType[] types)
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
