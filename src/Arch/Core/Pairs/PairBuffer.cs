using CommunityToolkit.HighPerformance;

namespace Arch.Core;

internal interface IBuffer
{
    int Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Destroy(World world, Entity source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Remove(Entity target);
}

internal class PairBuffer<T> : IBuffer
{
    internal readonly List<(T Relationship, Entity Target)> Elements;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PairBuffer()
    {
        Elements = new List<(T Relationship, Entity Target)>();
    }

    int IBuffer.Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Elements.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int FindEntityIndex(Entity target)
    {
#if NET5_0_OR_GREATER
        var span = Elements.AsSpan();
        for (int i = 0; i < span.Length; i++)
        {
            ref var pairEntity = ref span[i].Target;
            if (pairEntity == target)
            {
                return i;
            }
        }
#else
        for (var i = 0; i < Elements.Count; i++)
        {
            if (Elements[i].Target == target)
            {
                return i;
            }
        }
#endif

        return -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Add(T relationship, Entity target)
    {
        Debug.Assert(FindEntityIndex(target) == -1,
            $"Relationship with type {typeof(T)} and entity {target} already exists");
        Elements.Add((relationship, target));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBuffer.Destroy(World world, Entity source)
    {
        world.Remove<PairBuffer<T>>(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Destroy(World world, Entity source)
    {
        ((IBuffer) this).Destroy(world, source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IBuffer.Remove(Entity target)
    {
        var index = FindEntityIndex(target);
        Elements.RemoveAt(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Remove(Entity target)
    {
        ((IBuffer) this).Remove(target);
    }
};
