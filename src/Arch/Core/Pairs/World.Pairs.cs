using System.Diagnostics.Contracts;
using Arch.Core.Utils;

namespace Arch.Core;

public partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AddPair<T>(Entity source, Entity target, in T cmp = default)
    {
        ref var buffer = ref AddOrGet(source, static () => new PairBuffer<T>());
        buffer.Add(cmp, target);

        var pairComponent = new ArchRelationshipComponent(buffer);
        ref var targetBuffer = ref AddOrGet(target, static () => new PairBuffer<ArchRelationshipComponent>());
        targetBuffer.Add(pairComponent, source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal ref PairBuffer<T> GetPairs<T>(Entity source)
    {
        return ref Get<PairBuffer<T>>(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void RemovePair<T>(Entity source, Entity target)
    {
        ref var buffer = ref GetPairs<T>(source);
        buffer.Remove(target);

        ref var targetBuffer = ref GetPairs<ArchRelationshipComponent>(target);
        targetBuffer.Remove(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal bool TryGetPairs<T>(Entity source, out PairBuffer<T> pairs)
    {
        return TryGet(source, out pairs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), Pure]
    internal ref PairBuffer<T> TryGetRefPairs<T>(Entity source, out bool exists)
    {
        return ref TryGetRef<PairBuffer<T>>(source, out exists);
    }
}
