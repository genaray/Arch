using System;
using System.Runtime.CompilerServices;

namespace Arch.Core.Utils;

/// <summary>
///     A resizable collection of bits.
/// </summary>
public class BitSet
{
    private const int BitSize = sizeof(uint) * 8 - 1;
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
    ///     Determines whether the given bit is set.
    /// </summary>
    /// <param name="index">The index of the bit to check.</param>
    /// <returns><c>true</c> if the bit is set; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSet(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
            return false;

        return (_bits[b] & (1 << (index & BitSize))) != 0;
    }

    /// <summary>
    ///     Sets the bit at the given index.
    /// </summary>
    /// <param name="index">The bit to set.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetBit(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
            Array.Resize(ref _bits, b + 1);

        _bits[b] |= 1u << (index & BitSize);
    }

    /// <summary>
    ///     Clears the bit at the given index.
    /// </summary>
    /// <param name="index">The bit to clear.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearBit(int index)
    {
        var b = index >> ByteSize;
        if (b >= _bits.Length)
            return;

        _bits[b] &= ~(1u << (index & BitSize));
    }

    /// <summary>
    ///     Sets all bits.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetAll()
    {
        var count = _bits.Length;
        for (var i = 0; i < count; i++)
            _bits[i] = 0xffffffff;
    }

    /// <summary>
    ///     Clears all bits.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ClearAll()
    {
        Array.Clear(_bits, 0, _bits.Length);
    }


    /// <summary>
    ///     Determines whether all of the bits in this instance are also set in the given bitset.
    /// </summary>
    /// <param name="other">The bitset to check.</param>
    /// <returns><c>true</c> if all of the bits in this instance are set in <paramref name="other" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool All(BitSet other)
    {
        if (other == null)
            throw new ArgumentNullException("other");

        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) != bit)
                return false;
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
            if (_bits[i] != 0)
                return false;

        return true;
    }

    /// <summary>
    ///     Determines whether all of the bits in this instance are also set in the given bitset.
    /// </summary>
    /// <param name="other">The bitset to check.</param>
    /// <returns><c>true</c> if all of the bits in this instance are set in <paramref name="other" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Any(BitSet other)
    {
        if (other == null)
            throw new ArgumentNullException("other");

        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) != 0)
                return true;
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
            if (_bits[i] != 0)
                return false;

        return false;
    }

    /// <summary>
    ///     Determines whether all of the bits in this instance are also set in the given bitset.
    /// </summary>
    /// <param name="other">The bitset to check.</param>
    /// <returns><c>true</c> if all of the bits in this instance are set in <paramref name="other" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool None(BitSet other)
    {
        if (other == null)
            throw new ArgumentNullException("other");

        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit & otherBits[i]) == 0)
                return true;
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
            if (_bits[i] != 0)
                return true;

        return false;
    }
    
    /// <summary>
    ///     Determines whether all of the bits in this instance are also set in the given bitset.
    /// </summary>
    /// <param name="other">The bitset to check.</param>
    /// <returns><c>true</c> if all of the bits in this instance are set in <paramref name="other" />; otherwise, <c>false</c>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Exclusive(BitSet other)
    {
        if (other == null)
            throw new ArgumentNullException("other");

        var otherBits = other._bits;
        var count = Math.Min(_bits.Length, otherBits.Length);

        for (var i = 0; i < count; i++)
        {
            var bit = _bits[i];
            if ((bit ^ otherBits[i]) != 0)
                return false;
        }

        // handle extra bits on our side that might just be all zero
        var extra = _bits.Length - count;
        for (var i = count; i < extra; i++)
            if (_bits[i] != 0)
                return false;

        return true;
    }

    /// <summary>
    /// Returns a span to read the bitset.
    /// </summary>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> to read the bitset.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<uint> AsSpan()
    {
        return new ReadOnlySpan<uint>(_bits);
    }
}