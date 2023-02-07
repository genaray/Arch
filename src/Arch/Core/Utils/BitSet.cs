using System.Drawing;
using System.Text;

namespace Arch.Core.Utils;

// NOTE: Can this be replaced with `System.Collections.BitArray`?
// NOTE: If not, can it at least mirror that type's API?
/// <summary>
///     The <see cref="BitSet"/> class
///     represents a resizable collection of bits.
/// </summary>
public class BitSet
{
    internal const int BitSize = (sizeof(uint) * 8) - 1; // 31
    // NOTE: Is a byte not 8 bits?
    internal const int ByteSize = 5; // log_2(BitSize + 1)

    public static int RequiredLength(int id)
    {
        return (int)Math.Ceiling((float)id / (float)32);
    }

    private uint[] _bits;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BitSet" /> class.
    /// </summary>
    public BitSet()
    {
        _bits = new uint[1];
    }

    /// <summary>
    ///     Returns the length of the bitset, how many ints it consists of.
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _bits.Length;
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
        var bitCount = _bits.Length;
        for (var i = count; i < bitCount; i++)
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
        var bitCount = _bits.Length;
        for (var i = count; i < bitCount; i++)
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
        var bitCount = _bits.Length;
        for (var i = count; i < bitCount; i++)
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
        var bitCount = _bits.Length;
        for (var i = count; i < bitCount; i++)
        {
            if (_bits[i] != 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Creates a <see cref="Span{T}"/> to access the <see cref="_bits"/>.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<uint> AsSpan()
    {
        return _bits.AsSpan();
    }

    /// <summary>
    ///     Copies the bits into a <see cref="Span{T}"/> and returns a slice containing the copied <see cref="_bits"/>.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<uint> AsSpan(Span<uint> span)
    {
        // Prevent exception because target array is to small for copy operation
        if(_bits.Length > span.Length)
        {
            return Span<uint>.Empty;
        }

        _bits.CopyTo(span);
        return span[.._bits.Length];
    }

    /// <summary>
    ///     Calculates the hash, this is unique for the set bits. Two <see cref="BitSet"/> with the same set bits, result in the same hash.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Component.GetHashCode(this);
    }

    /// <summary>
    ///     Prints the content of this instance.
    /// </summary>
    /// <returns>The string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        // Convert uint to binary form for pretty printing
        var binaryBuilder = new StringBuilder();
        foreach (var bit in _bits)
        {
            binaryBuilder.Append(Convert.ToString(bit, 2)).Append(',');
        }

        return $"{nameof(_bits)}: {binaryBuilder}, {nameof(Length)}: {Length}";
    }
}

/// <summary>
///     The <see cref="SpanBitSet"/> struct
///     represents a non resizable collection of bits.
///     Used to set, check and clear bits on a allocated <see cref="BitSet"/> or on the stack.
/// </summary>
public ref struct SpanBitSet
{
    private const int BitSize = (sizeof(uint) * 8) - 1; // 31
    // NOTE: Is a byte not 8 bits?
    private const int ByteSize = 5; // log_2(BitSize + 1)

    private Span<uint> _bits;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BitSet" /> class.
    /// </summary>
    public SpanBitSet(Span<uint> bits)
    {
        _bits = bits;
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
            return;
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
        _bits.Clear();
    }

    /// <summary>
    ///     Creates a <see cref="Span{T}"/> to access the <see cref="_bits"/>.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<uint> AsSpan()
    {
        return _bits;
    }

    /// <summary>
    ///     Copies the bits into a <see cref="Span{T}"/> and returns a slice containing the copied <see cref="_bits"/>.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<uint> AsSpan(Span<uint> span)
    {
        // Prevent exception because target array is to small for copy operation
        if(_bits.Length > span.Length)
        {
            return Span<uint>.Empty;
        }

        _bits.CopyTo(span);
        return span[.._bits.Length];
    }

    /// <summary>
    ///     Calculates the hash, this is unique for the set bits. Two <see cref="BitSet"/> with the same set bits, result in the same hash.
    /// </summary>
    /// <returns>The hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return Component.GetHashCode(ref this);
    }

    /// <summary>
    ///     Prints the content of this instance.
    /// </summary>
    /// <returns>The string.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {

        // Convert uint to binary form for pretty printing
        var binaryBuilder = new StringBuilder();
        foreach (var bit in _bits)
        {
            binaryBuilder.Append(Convert.ToString(bit, 2)).Append(',');
        }

        return $"{nameof(_bits)}: {string.Join(",", binaryBuilder)}";
    }
}
