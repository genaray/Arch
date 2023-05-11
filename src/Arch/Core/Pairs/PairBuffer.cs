using CommunityToolkit.HighPerformance;

namespace Arch.Core;

internal interface IBuffer
{
    internal static readonly Comparer<Entity> Comparer = Comparer<Entity>.Create((a, b) => a.Id.CompareTo(b.Id));

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
    internal readonly SortedList<Entity, T> Elements;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal PairBuffer()
    {
        Elements = new SortedList<Entity, T>(IBuffer.Comparer);
    }

    int IBuffer.Count
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Elements.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Add(T relationship, Entity target)
    {
        Elements.Add(target, relationship);
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
        Elements.Remove(target);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Remove(Entity target)
    {
        ((IBuffer) this).Remove(target);
    }
};
