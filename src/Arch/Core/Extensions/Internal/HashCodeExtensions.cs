namespace Arch.Core.Extensions.Internal;

internal static class HashCodeExtensions
{
#if !NET5_0_OR_GREATER
    public static void AddBytes(ref this HashCode hashCode, ReadOnlySpan<byte> bytes)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            hashCode.Add(bytes[i]);
        }
    }
#endif

    public static void AddSpan<T>(ref this HashCode hashCode, ReadOnlySpan<T> items)
        where T : unmanaged
    {
        hashCode.AddBytes(MemoryMarshal.AsBytes(items));
    }

    public static void AddSpan<T>(ref this HashCode hashCode, Span<T> items)
        where T : unmanaged
    {
        hashCode.AddBytes(MemoryMarshal.AsBytes(items));
    }
}
