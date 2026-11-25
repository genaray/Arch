using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Buffer;

/// <summary>
///     The <see cref="ComponentValues"/> class
///     stores the values of all recorded commands for one component type, one after another.
/// </summary>
internal sealed class ComponentValues
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ComponentValues"/> class.
    /// </summary>
    /// <param name="type">The <see cref="ComponentType"/> of the stored values.</param>
    /// <param name="capacity">The amount of values it can hold without growing.</param>
    public ComponentValues(ComponentType type, int capacity)
    {
        Type = type;
        Ops = ComponentOps.Get(type);
        Items = Ops.Create(Math.Max(capacity, 1));
    }

    /// <summary>
    ///     The <see cref="ComponentType"/> of the stored values.
    /// </summary>
    public ComponentType Type { get; }

    /// <summary>
    ///     The <see cref="ComponentOps"/> to copy the values into a <see cref="Chunk"/>.
    /// </summary>
    public ComponentOps Ops { get; }

    /// <summary>
    ///     The array of values, its first <see cref="Count"/> items are in use.
    /// </summary>
    public Array Items { get; private set; }

    /// <summary>
    ///     The amount of stored values.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    ///     Stores a value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <typeparam name="T">The component type, must be the type this instance was created for.</typeparam>
    /// <returns>Its index.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Add<T>(in T value)
    {
        if (Count == Items.Length)
        {
            Grow();
        }

        Unsafe.As<T[]>(Items)[Count] = value;
        return Count++;
    }

    /// <summary>
    ///     Removes all values and clears them, since they might hold references that must not outlive the recorded commands.
    /// </summary>
    public void Clear()
    {
        Array.Clear(Items, 0, Count);
        Count = 0;
    }

    private void Grow()
    {
        var larger = Ops.Create(Items.Length * 2);
        Array.Copy(Items, larger, Count);
        Items = larger;
    }
}
