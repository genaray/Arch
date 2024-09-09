using System.Buffers;
using CommunityToolkit.HighPerformance;

namespace Arch.Core.Utils;

public class Pool<T>
{
    public readonly record struct PooledArray(T[] Array, int Length) : IDisposable
    {
        public ref T this[int index]
        {
            get => ref Array.DangerousGetReferenceAt(index);
        }

        public Span<T> AsSpan()
        {
            return MemoryMarshal.CreateSpan(ref this[0], Length);
        }

        public void Dispose()
        {
            Return(this);
        }

        public static implicit operator T[](PooledArray array)
        {
            return array.Array;
        }
    }

    public static PooledArray Rent(int amount)
    {
        return new PooledArray(ArrayPool<T>.Shared.Rent(amount), amount);
    }

    public static void Return(PooledArray item)
    {
        ArrayPool<T>.Shared.Return(item);
    }
}
