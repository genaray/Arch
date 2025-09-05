namespace Arch.Core.Utils;

/// <summary>
///     The <see cref="NetStandardList{T}"/> class
///     is a list which can return a <see cref="Span{T}"/> to be compatible with .NetStandard2.1
/// </summary>
/// <typeparam name="T"></typeparam>
public class NetStandardList<T>
{
    private T[] _items;
    private int _count;

    /// <summary>
    ///     Creates a new instance.
    /// </summary>
    /// <param name="capacity">Its initial capacity.</param>
    public NetStandardList(int capacity = 8)
    {
        _items = new T[capacity];
        _count = 0;
    }

    /// <summary>
    ///     The amount of items in this instance.
    /// </summary>
    public int Count
    {
        get => _count;
    }

    /// <summary>
    ///     Adds an element to this instance.
    /// </summary>
    /// <param name="item">The item.</param>
    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            EnsureCapacity(_count + 1);
        }
        _items[_count++] = item;
    }

    /// <summary>
    ///     Adds all elements to this instance.
    /// </summary>
    /// <param name="collection">The collection of new items.</param>
    /// <exception cref="ArgumentNullException">Throws if the collection is null.</exception>
    public void AddRange(IEnumerable<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }

        // Increase capacity or just insert.
        if (collection is ICollection<T> coll)
        {
            int newCount = _count + coll.Count;
            EnsureCapacity(newCount);
            foreach (T item in coll)
            {
                _items[_count++] = item;
            }
        }
        else
        {
            foreach (T item in collection)
            {
                Add(item);
            }
        }
    }

    /// <summary>
    /// Removes the item from this instance.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>True if it was successfully.</returns>
    public bool Remove(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < _count; i++)
        {
            if (comparer.Equals(_items[i], item))
            {
                RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes an item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is out of range.</exception>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        // All elements to the left
        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }

        // Place default item
        #pragma warning disable CS8601 // Possible null reference assignment.
        _items[--_count] = default;
        #pragma warning restore CS8601 // Possible null reference assignment.
    }

    /// <summary>
    ///     Ensures the capacity.
    /// </summary>
    /// <param name="min">Its minimum.</param>
    private void EnsureCapacity(int min)
    {
        if (_items.Length < min)
        {
            int newCapacity = _items.Length == 0 ? 8 : _items.Length * 2;
            if (newCapacity < min)
            {
                newCapacity = min;
            }
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _count);
            _items = newArray;
        }
    }

    /// <summary>
    ///     Returns a <see cref="Span{T}"/> from this instance.
    /// </summary>
    /// <returns>A new <see cref="Span{T}"/>.</returns>
    public Span<T> AsSpan()
    {
        return new Span<T>(_items, 0, _count);
    }

    /// <summary>
    ///     Returns a new <see cref="Enumerator{T}"/>.
    /// </summary>
    /// <returns></returns>
    public Enumerator<T> GetEnumerator()
    {
        return new Enumerator<T>(AsSpan());
    }

    /// <summary>
    ///     Accesses an item at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <exception cref="ArgumentOutOfRangeException">Throws if the index was out of range.</exception>
    public ref T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            return ref _items[index];
        }
    }

    /// <summary>
    ///     Clears all items from this instance.
    /// </summary>
    public void Clear()
    {
        Array.Clear(_items, 0, _count);
        _count = 0;
    }
}
