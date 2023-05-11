using CommunityToolkit.HighPerformance;

namespace Arch.Core.Buffers;

public struct Buffer<T>
{
    public List<T> Elements;

    public Buffer()
    {
        Elements = new List<T>();
    }

#if NET5_0_OR_GREATER
    public Span<T> AsSpan()
    {
        return Elements.AsSpan();
    }
#endif
}
