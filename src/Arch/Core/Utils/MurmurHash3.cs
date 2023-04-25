namespace Arch.Core.Utils;

// From https://github.com/odinmillion/MurmurHash.Net/blob/master/src/MurmurHash.Net/MurmurHash3.cs
/// <summary>
///     The <see cref="MurmurHash3"/> class
///     represents a utility to calculate a MurMurHash3 used to map querys and descriptions to archetypes.
/// </summary>
public class MurmurHash3
{
    /// <summary>
    ///     Calculates the hash as a 32 bit integer and returns it.
    /// </summary>
    /// <param name="bytes">The <see cref="Span{T}"/> of bytes to hash.</param>
    /// <param name="seed">The seed.</param>
    /// <returns>The 32 bit integer hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Hash32(ReadOnlySpan<byte> bytes, uint seed)
    {
        var length = bytes.Length;
        var h1 = seed;
        var remainder = length & 3;
        var position = length - remainder;

        for (var start = 0; start < position; start += 4)
        {
            h1 = (uint) ((int) RotateLeft(h1 ^ RotateLeft(BitConverter.ToUInt32(bytes.Slice(start, 4)) * 3432918353U,15) * 461845907U, 13) * 5 - 430675100);
        }

        if (remainder > 0)
        {
            uint num = 0;
            switch (remainder)
            {
                case 1:
                    num ^= (uint) bytes[position];
                    break;
                case 2:
                    num ^= (uint) bytes[position + 1] << 8;
                    goto case 1;
                case 3:
                    num ^= (uint) bytes[position + 2] << 16;
                    goto case 2;
            }

            h1 ^= RotateLeft(num * 3432918353U, 15) * 461845907U;
        }

        h1 = FMix(h1 ^ (uint) length);

        return h1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint RotateLeft(uint x, byte r)
    {
        return x << (int) r | x >> 32 - (int) r;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static uint FMix(uint h)
    {
        h = (uint) (((int) h ^ (int) (h >> 16)) * -2048144789);
        h = (uint) (((int) h ^ (int) (h >> 13)) * -1028477387);
        return h ^ h >> 16;
    }
}
