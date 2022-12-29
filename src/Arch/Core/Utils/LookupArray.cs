namespace Arch.Core.Utils;

public class LookupArray<T>
{
    private readonly int _bucketSize;
    private T[][] _bucketArray;

    public LookupArray(int bucketSize, int capacity = 64)
    {
        _bucketSize = bucketSize;
        _bucketArray = new T[(int)Math.Ceiling((float)capacity / bucketSize)][];

        for (var i = 0; i < _bucketArray.Length; i++)
        {
            _bucketArray[i] = new T[_bucketSize];
        }
    }

    public int Count
    {
        get => _bucketArray.Length * _bucketSize;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureCapacity(int newCapacity)
    {
        var length = _bucketArray.Length;
        if (newCapacity < _bucketArray.Length * _bucketSize)
        {
            return;
        }

        var buckets = (int)Math.Ceiling((float)newCapacity / _bucketSize);
        Array.Resize(ref _bucketArray, buckets);

        for (var i = length; i < _bucketArray.Length; i++)
        {
            _bucketArray[i] = new T[_bucketSize];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TrimExcess(int newCapacity)
    {
        var length = _bucketArray.Length;
        var buckets = (int)Math.Ceiling((float)newCapacity / _bucketSize);
        Array.Resize(ref _bucketArray, buckets);

        for (var i = length; i < _bucketArray.Length; i++)
        {
            _bucketArray[i] = new T[_bucketSize];
        }
    }

    public ref T this[int i]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref _bucketArray[(int)Math.Floor((float)i / _bucketSize)][i % _bucketSize];
    }
}
