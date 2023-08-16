using System.Buffers;
using Arch.Core;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;

/// <summary>
///     The <see cref="WorldExtensions"/> class
///     adds several usefull utility methods to the <see cref="World"/>.
/// </summary>
public static class WorldExtensions
{

    /// <summary>
    ///     Reserves space for a certain number of <see cref="Entity"/>'s of a given component structure/<see cref="Archetype"/>.
    /// </summary>
    /// <param name="types">The component structure/<see cref="Archetype"/>.</param>
    /// <param name="amount">The amount.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Reserve(this World world, ComponentType[] types, int amount)
    {
        world.Reserve(types, amount);
    }

    /// <summary>
    ///     Search all matching <see cref="Entity"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components or <see cref="Entity"/>'s are searched for.</param>
    /// <param name="list">The <see cref="IList{T}"/> receiving the found <see cref="Entity"/>'s.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetEntities(this World world, in QueryDescription queryDescription, IList<Entity> list)
    {
        var query = world.Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            foreach(var entityIndex in chunk)
            {
                ref readonly var entity = ref Unsafe.Add(ref entityFirstElement, entityIndex);
                list.Add(entity);
            }
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Archetype"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="archetypes">The <see cref="IList{T}"/> receiving <see cref="Archetype"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetArchetypes(this World world, in QueryDescription queryDescription, IList<Archetype> archetypes)
    {
        var query = world.Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            archetypes.Add(archetype);
        }
    }

    /// <summary>
    ///     Search all matching <see cref="Chunk"/>'s and put them into the given <see cref="IList{T}"/>.
    /// </summary>
    /// <param name="queryDescription">The <see cref="QueryDescription"/> which specifies which components are searched for.</param>
    /// <param name="chunks">The <see cref="IList{T}"/> receiving <see cref="Chunk"/>'s containing <see cref="Entity"/>'s with the matching components.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetChunks(this World world, in QueryDescription queryDescription, IList<Chunk> chunks)
    {
        var query = world.Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            chunks.Add(chunk);
        }
    }

    /// <summary>
    ///     Sets or replaces a <see cref="IList{T}"/> of components for an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The components <see cref="IList{T}"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetRange(this World world, Entity entity, params object[] components)
    {
        world.SetRange(entity, components);
    }

    /// <summary>
    ///     Checks if an <see cref="Entity"/> has a certain component.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>True if it has the desired component, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasRange(this World world, Entity entity, params ComponentType[] types)
    {
        return world.HasRange(entity, types);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static object[] GetRange(this World world, Entity entity, params ComponentType[] types)
    {
        return world.GetRange(entity, types);
    }

    /// <summary>
    ///     Returns an array of components of an <see cref="Entity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">The component <see cref="ComponentType"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> where the components are put it.</param>
    /// <returns>A reference to the component.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetRange(this World world, Entity entity, ComponentType[] types, IList<object> components)
    {
        var entitySlot = world.EntityInfo.GetEntitySlot(entity.Id);
        for (var index = 0; index < types.Length; index++)
        {
            var type = types[index];
            components.Add(entitySlot.Archetype.Get(ref entitySlot.Slot, type));
        }
    }

    /// <summary>
    ///     Adds a <see cref="IList{T}"/> of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">The component <see cref="IList{T}"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange(this World world, Entity entity, params object[] components)
    {
        world.AddRange(entity, components);
    }

    /// <summary>
    ///     Adds an list of new components to the <see cref="Entity"/> and moves it to the new <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="components">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are added to the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddRange(this World world, Entity entity, IList<ComponentType> components)
    {
        var oldArchetype = world.EntityInfo.GetArchetype(entity.Id);

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);

        for (var index = 0; index < components.Count; index++)
        {
            var type = components[index];
            spanBitSet.SetBit(type.Id);
        }

        if (!world.TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = world.GetOrCreate(oldArchetype.Types.Add(components));
        }

        world.Move(entity, oldArchetype, newArchetype, out _);
        
#if EVENTS
        for (var i = 0; i < components.Count; i++)
        {
            world.OnComponentAdded(entity, components[i]);
        }
#endif
    }

        /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveRange(this World world, Entity entity, params ComponentType[] types)
    {
        world.RemoveRange(entity, types);
    }

    /// <summary>
    ///     Removes a list of <see cref="ComponentType"/>'s from the <see cref="Entity"/> and moves it to a different <see cref="Archetype"/>.
    /// </summary>
    /// <param name="entity">The <see cref="Entity"/>.</param>
    /// <param name="types">A <see cref="IList{T}"/> of <see cref="ComponentType"/>'s, those are removed from the <see cref="Entity"/>.</param>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveRange(this World world, Entity entity, IList<ComponentType> types)
    {
        var oldArchetype = world.EntityInfo.GetArchetype(entity.Id);

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        for (var index = 0; index < types.Count; index++)
        {
            var cmp = types[index];
            spanBitSet.ClearBit(cmp.Id);
        }

        if (!world.TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            newArchetype = world.GetOrCreate(oldArchetype.Types.Remove(types));
        }

        world.Move(entity, oldArchetype, newArchetype, out _);
    }
}
