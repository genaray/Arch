using Arch.Core.Extensions.Internal;

namespace Arch.Core.Utils;

public class ArrayDictionary<TValue>
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

    /// <summary>
    ///     The delegate type to call when adding an element that does not already exist.
    /// </summary>
    /// <typeparam name="TData">The data to pass to the delegate.</typeparam>
    public delegate TValue Create<TData>(TData data);

    public ArrayDictionary(int maxArraySize)
    {
        _array = Array.Empty<TValue>();
        _dictionary = new Dictionary<int, TValue>(0);
        _maxArraySize = maxArraySize;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TValue GetOrAdd<TData>(int index, in Create<TData> create, in TData data) where TData : struct
    {
        Debug.Assert(index >= 0);

        if (index < _maxArraySize)
        {
            ref var value = ref ArrayExtensions.GetOrResize(ref _array, index, out var exists, _maxArraySize);
            if (!exists)
            {
                value = create(data);
            }

            return value;
        }
#if NET5_0_OR_GREATER
        else
        {
            ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(_dictionary, index, out var exists);
            if (!exists)
            {
                value = create(data);
            }

            return value!;
        }
#else
        else
        {
            if (!_dictionary.TryGetValue(index, out var value))
            {
                value = create(data);
                _dictionary[index] = value;
            }

            return value;
        }
#endif
    }
}
