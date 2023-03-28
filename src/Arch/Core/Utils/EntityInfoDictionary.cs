using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core;
using Arch.Core.Extensions;

namespace Arch.Core.Utils;

internal class EntityInfoDictionary
{
    /// <summary>
    /// How large a chunk should be. This value will be a power of 2.
    /// </summary>
    private static readonly int _chunkSize;
    private static readonly int _chunkSizeMinusOne;

    private EntityInfo[][] _entityInfos = new EntityInfo[0][];
    private int _largestId;

    static EntityInfoDictionary()
    {
        var cpuL1CacheSize = 16_000; // In bytes
        var idealSize = cpuL1CacheSize / Unsafe.SizeOf<EntityInfo>();

        _chunkSize = MathExtensions.RoundToPowerOfTwo(idealSize);
        _chunkSizeMinusOne = _chunkSize - 1;
    }

    public EntityInfoDictionary() : this(256)
    {

    }

    public EntityInfoDictionary(int capacity)
    {
        EnsureCapacity(capacity);
    }

    public void Add(int id, EntityInfo entityInfo)
    {
        this[id] = entityInfo;
    }

    public void Remove(int id)
    {
        this[id] = default;
    }

    public void EnsureCapacity(int capacity)
    {
        if (capacity < _largestId)
        {
            return;
        }

        var currentSize = _entityInfos.Length;
        var desiredSize = (capacity / _chunkSize) + 1;

        Array.Resize(ref _entityInfos, desiredSize);

        // Create the new arrays. 
        for (int i = currentSize; i < desiredSize; i++)
        {
            _entityInfos[i] = new EntityInfo[_chunkSize];
        }

        UpdateLargestId();
    }

    public bool TryGetValue(int id, out EntityInfo entityInfo)
    {
        // If the id is negative
        if (id < 0)
        {
            entityInfo = default;
            return false;
        }

        GetIndexesFromId(id, out var outerIndex, out var innerIndex);

        // If the item is outside the array. Then it definetly doesn't exist
        if (outerIndex > _entityInfos.Length)
        {
            entityInfo = default;
            return false;
        }

        ref var item = ref _entityInfos[outerIndex][innerIndex];

        // If the item is the default then the nobody set its value.
        if (item == default)
        {
            entityInfo = default;
            return false;
        }

        entityInfo = item;
        return true;
    }

    public void TrimExcess()
    {
        var lastIndexWithNonDefaultValues = _entityInfos.Length - 1;

        for (var i = lastIndexWithNonDefaultValues; i >= 0; i--)
        {
            if (ArrayContainsNonDefaultValues(_entityInfos[i]))
            {
                break;
            }

            lastIndexWithNonDefaultValues = i - 1;
        }

        Array.Resize(ref _entityInfos, lastIndexWithNonDefaultValues + 1);

        UpdateLargestId();
    }

    public void Clear()
    {
        Array.Clear(_entityInfos, 0, _entityInfos.Length);
    }

    public ref EntityInfo this[int id]
    {
        get
        {
            Debug.Assert(id >= 0, "Id cannot be negative");

            EnsureCapacity(id);
            GetIndexesFromId(id, out var outerIndex, out var innerIndex);
            return ref _entityInfos[outerIndex][innerIndex];
        }
    }

    private void GetIndexesFromId(int id, out int outerIndex, out int innerIndex)
    {
        Debug.Assert(id >= 0, "Id cannot be negative.");

        outerIndex = id / _chunkSize;
        //innerIndex = id % _chunkCount;
        //innerIndex = id - (outerIndex * _chunkSize);

        /* Instead of the '%' operator we can use logical '&' operator which is faster. But it requires the chunk size to be a power of 2. */
        innerIndex = id & _chunkSizeMinusOne;
    }

    private void UpdateLargestId()
    {
        _largestId = _entityInfos.Length * _chunkSize;
    }

    private static bool ArrayContainsNonDefaultValues(EntityInfo[] array)
    {
        foreach (var item in array)
        {
            if (item != default)
            {
                return true;
            }
        }

        return false;
    }
}
