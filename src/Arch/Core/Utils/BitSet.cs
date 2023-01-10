namespace Arch.Core.Utils;

// NOTE: Can this be replaced with `System.Collections.BitArray`?
// NOTE: If not, can it at least mirror that type's API?
/// <summary>
///     The <see cref="BitSet"/> class
///     represents a resizable collection of bits.
/// </summary>
public class BitSet
{
    private const int BitSize = (sizeof(uint) * 8) - 1; // 31
    // NOTE: Is a byte not 8 bits?
    private const int ByteSize = 5; // log_2(BitSize + 1)

    private uint[] _bits;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BitSet" /> class.
    /// </summary>
    public BitSet()
    {
        _bits = new uint[1];
    }

    /// <summary>
    ///     Checks whether a bit is set at the index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>True if it is, otherwhise false</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSet(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
        {
            return false;
        }

        return (_bits[b] & (1 << (index & BitSize))) != 0;
    }

    /// <summary>
    ///     Sets a bit at the given index.
    ///     Resizes its internal array if necessary.
    /// </summary>
    /// <param name="index">The index.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetBit(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
        {
            Array.Resize(ref _bits, b + 1);
        }

        _bits[b] |= 1u << (index & BitSize);
    }

    /// <summary>
    ///     Clears the bit at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearBit(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
        {
            return;
        }

        _bits[b] &= ~(1u << (index & BitSize));
    }

    /// <summary>
    ///
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetAll()
    {
        var count = _bits.Length;
        for (var i = 0; i < count; i++)
        {
            _bits[i] = 0xffffffff;
        }
    }

    /// <summary>
    ///     Clears all set bits.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearAll()
    {
        Array.Clear(_bits, 0, _bits.Length);
    }

    /// <summary>
    ///     Checks if all bits from this instance match those of the other instance.
    /// </summary>
    /// <param name="other">The other <see cref="BitSet"/>.</param>
    /// <returns>True if they match, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool All(BitSet other)
    {
        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) != bit)
            {
                return false;
            }
        }

        // Handle extra bits on our side that might just be all zero.
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
        {
            if (_bits[i] != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks if any bits from this instance match those of the other instance.
    /// </summary>
    /// <param name="other">The other <see cref="BitSet"/>.</param>
    /// <returns>True if they match, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Any(BitSet other)
    {
        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) != 0)
            {
                return true;
            }
        }

        // Handle extra bits on our side that might just be all zero.
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
        {
            if (_bits[i] != 0)
            {
                return false;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if none bits from this instance match those of the other instance.
    /// </summary>
    /// <param name="other">The other <see cref="BitSet"/>.</param>
    /// <returns>True if none match, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool None(BitSet other)
    {
        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) == 0)
            {
                return true;
            }
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
        {
            if (_bits[i] != 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     Checks if exactly all bits from this instance match those of the other instance.
    /// </summary>
    /// <param name="other">The other <see cref="BitSet"/>.</param>
    /// <returns>True if they match, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exclusive(BitSet other)
    {

        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit ^ otherBits[i]) != 0)
            {
                return false;
            }
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
        {
            if (_bits[i] != 0)
            {
                return false;
            }
        }

        return true;
    }
}
