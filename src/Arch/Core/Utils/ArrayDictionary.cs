using Arch.Core.Extensions.Internal;

namespace Arch.Core.Utils;

/// <summary>
///     The <see cref="ArrayDictionary{TValue}"/> class
///     represents an hybrid collection that uses arrays and dictionarys.
///     Arrays are used for indexes below <see cref="_maxArraySize"/> and for higher indexes
/// </summary>
/// <typeparam name="TValue"></typeparam>
internal class ArrayDictionary<TValue>
{
    /// <summary>
    ///     Stores <see cref="TValue"/>s with an index smaller than
    ///     <see cref="_maxArraySize"/>.
    ///     When the index used to access one is equal to or bigger than
    ///     <see cref="_maxArraySize"/>, <see cref="_dictionary"/> is used instead.
    /// </summary>
    internal TValue[] _array;

    /// <summary>
    ///     Stores <see cref="TValue"/>s with an index equal to or bigger than
    ///     <see cref="_maxArraySize"/>.
    ///     When the index used to access one is smaller than <see cref="_maxArraySize"/>,
    ///     <see cref="_dictionary"/> is used instead.
    /// </summary>
    internal readonly Dictionary<int, TValue> _dictionary;

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

    /// <summary>
    ///     Adds an element in this collection by its index.
    ///     If the index is smaller than <see cref="_maxArraySize"/>
    ///     <see cref="_array"/> is used, otherwise <see cref="_dictionary"/> is used.
    /// </summary>
    /// <param name="index">The index of the value to get.</param>
    /// <param name="item">The item being added.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Add(int index, TValue item)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            Array.Resize(ref _array, _maxArraySize);
            _array[index] = item;
        }
        else
        {
            _dictionary[index] = item;
        }
    }

    /// <summary>
    ///     Removes an item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    internal void Remove(int index)
    {
        if (index < _maxArraySize)
        {
            Array.Resize(ref _array, _maxArraySize);
            _array[index] = default!;
        }
        else
        {
            _dictionary.Remove(index);
        }
    }

#if !NET5_0_OR_GREATER

    /// <summary>
    ///     Trys to return an item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The item.</param>
    /// <returns>True if it exists, false if it does not.</returns>
    internal bool TryGet(int index, out TValue item)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            Array.Resize(ref _array, _maxArraySize);
            item = _array[index];
            var exists = !Equals(item, default(TValue));
            return exists;
        }

        return _dictionary.TryGetValue(index, out item);
    }

#else

    /// <summary>
    ///     Trys to return an item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="exists">If the item exists..</param>
    /// <returns>A reference to the existing item.</returns>
    internal ref TValue TryGet(int index, [UnscopedRef] out bool exists)
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            Array.Resize(ref _array, _maxArraySize);
            ref var item = ref _array[index];
            exists = !Equals(item, default(TValue));
            return ref item;
        }

        return ref  CollectionsMarshal.GetValueRefOrAddDefault(_dictionary, index, out exists)!;
    }
#endif
}
