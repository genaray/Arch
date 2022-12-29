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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
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

    // TODO: Documentation.
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearAll()
    {
        Array.Clear(_bits, 0, _bits.Length);
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool All(BitSet other)
    {
        if (other is null)
        {
            throw new ArgumentNullException("other");
        }

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Any(BitSet other)
    {
        if (other is null)
        {
            throw new ArgumentNullException("other");
        }

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool None(BitSet other)
    {
        if (other is null)
        {
            throw new ArgumentNullException("other");
        }

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exclusive(BitSet other)
    {
        if (other is null)
        {
            throw new ArgumentNullException("other");
        }

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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<uint> AsSpan()
    {
        return new ReadOnlySpan<uint>(_bits);
    }
}
