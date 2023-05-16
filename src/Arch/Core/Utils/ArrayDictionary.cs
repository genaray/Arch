using Arch.Core.Extensions.Internal;

namespace Arch.Core.Utils;

internal class ArrayDictionary<TValue>
{
    /// <summary>
    ///     Stores <see cref="TValue"/>s with an index smaller than
    ///     <see cref="_maxArraySize"/>.
    ///     When the index used to access one is equal to or bigger than
    ///     <see cref="_maxArraySize"/>, <see cref="_dictionary"/> is used instead.
    /// </summary>
    private TValue[] _array;

    /// <summary>
    ///     Stores <see cref="TValue"/>s with an index equal to or bigger than
    ///     <see cref="_maxArraySize"/>.
    ///     When the index used to access one is smaller than <see cref="_maxArraySize"/>,
    ///     <see cref="_dictionary"/> is used instead.
    /// </summary>
    private readonly Dictionary<int, TValue> _dictionary;

    /// <summary>
    ///     The maximum size that <see cref="_array"/> will grow to.
    ///     Any elements outside this range are indexed by <see cref="_dictionary"/>.
    /// </summary>
    private readonly int _maxArraySize;

    internal ArrayDictionary(int maxArraySize)
    {
        _array = Array.Empty<TValue>();
        _dictionary = new Dictionary<int, TValue>(0);
        _maxArraySize = maxArraySize;
    }

#if NET5_0_OR_GREATER
    /// <summary>
    ///     Gets a reference to an element in this collection by its index.
    ///     If the index is smaller than <see cref="_maxArraySize"/>
    ///     <see cref="_array"/> is used, otherwise <see cref="_dictionary"/> is used.
    /// </summary>
    /// <param name="index">The index of the value to get.</param>
    /// <param name="exists">Whether or not the value existed in this collection.</param>
    /// <returns>A ref to the value, with a default value if it did not exist.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ref TValue AddOrGet(int index, [UnscopedRef] out bool exists)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            ref var value = ref ArrayExtensions.GetOrResize(ref _array, index, _maxArraySize);
            exists = !Equals(value, default(TValue));
            return ref value;
        }

        return ref CollectionsMarshal.GetValueRefOrAddDefault(_dictionary, index, out exists)!;
    }
#else
    /// <summary>
    ///     Gets an element in this collection by its index.
    ///     If the index is smaller than <see cref="_maxArraySize"/>
    ///     <see cref="_array"/> is used, otherwise <see cref="_dictionary"/> is used.
    /// </summary>
    /// <param name="index">The index of the value to get.</param>
    /// <param name="exists">Whether or not the value existed in this collection.</param>
    /// <returns>The value, with a default value if it did not exist.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal TValue AddOrGet(int index, out bool exists)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            ref var value = ref ArrayExtensions.GetOrResize(ref _array, index, _maxArraySize);
            exists = !Equals(value, default(TValue));
            return value;
        }
        else
        {
            exists = _dictionary.TryGetValue(index, out var value);
            return value;
        }
    }
#endif

    /// <summary>
    ///     Sets an element in this collection by <see cref="index"/>.
    /// </summary>
    /// <param name="index">The index of the <see cref="value"/> to set.</param>
    /// <param name="value">The value to set at <see cref="index"/></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Set(int index, in TValue value)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            ref var arrayValue = ref ArrayExtensions.GetOrResize(ref _array, index, _maxArraySize);
            arrayValue = value;
        }
        else
        {
            _dictionary[index] = value;
        }
    }
}
