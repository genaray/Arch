using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.LowLevel.Jagged;

namespace Arch.Core;

/// <summary>
///     The <see cref="EntityData"/> struct
///     stores information about an <see cref="Entity"/> to quickly access its data and location.
/// </summary>
[SkipLocalsInit]
public struct EntityData
{
    /// <summary>
    ///     A reference to its <see cref="Archetype"/>.
    /// </summary>
    public Archetype Archetype;

    /// <summary>
    ///     A reference to its <see cref="Slot"/>.
    /// </summary>
    public Slot Slot;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityData"/> struct.
    /// </summary>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    public EntityData(Archetype archetype, Slot slot)
    {
        Archetype = archetype;
        Slot = slot;
    }
}

/// <summary>
///     The <see cref="EntityInfoStorage"/> class
///     acts as an API and Manager to access all <see cref="Entity"/> meta data and information like its version, its <see cref="Archetype"/> or the <see cref="Chunk"/> it is in.
/// </summary>
internal class EntityInfoStorage
{
    /// <summary>
    ///     The <see cref="Entity"/> <see cref="Archetype"/> and <see cref="Slot"/>s in an jagged array.
    /// <remarks>Because usually both are needed and thus an array access can be saved.</remarks>
    /// </summary>
    internal JaggedArray<EntityData> EntityData {  get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityInfoStorage"/> class.
    /// </summary>
    /// <param name="baseChunkSize">The minimum <see cref="Chunk"/> size in bytes, used to calculate buckets fitting in the L1 cache.</param>
    /// <param name="capacity">The initial capacity.</param>
    internal EntityInfoStorage(int baseChunkSize, int capacity)
    {
        EntityData = new JaggedArray<EntityData>(
            baseChunkSize / Unsafe.SizeOf<EntityData>(),
            new EntityData(null!, new Slot(-1,-1)),
            capacity
        );
    }

    /// <summary>
    ///     Adds meta data of an <see cref="Entity"/> to the internal structure.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    public void Add(int id, Archetype archetype, Slot slot)
    {
        EntityData.Add(id,new EntityData(archetype, slot));
    }

    /// <summary>
    ///     Checks whether an <see cref="Entity"/>s data exists in this <see cref="EntityInfoStorage"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>True if its data exists in here, false if not.</returns>
    public bool Has(int id)
    {
        return EntityData.TryGetValue(id, out EntityData _);
    }

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    public Archetype GetArchetype(int id)
    {
        return EntityData[id].Archetype;
    }

    /// <summary>
    ///     Returns the <see cref="Slot"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Slot"/>.</returns>
    public ref Slot GetSlot(int id)
    {
        return ref EntityData[id].Slot;
    }

    /// <summary>
    ///     Returns the <see cref="Core.EntityData"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Core.EntityData"/>.</returns>
    public EntityData GetEntitySlot(int id)
    {
        return EntityData[id];
    }

    /// <summary>
    ///     Removes an enlisted <see cref="Entity"/> from this <see cref="EntityInfoStorage"/>.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    public void Remove(int id)
    {
        EntityData.Remove(id);
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> to a new <see cref="Slot"/>, updates that reference.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="slot">Its new <see cref="Slot"/>.</param>
    public void Move(int id, Slot slot)
    {
        EntityData[id].Slot = slot;
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> to a new <see cref="Archetype"/> and a new <see cref="Slot"/>, updates that reference.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="archetype">Its new <see cref="Archetype"/>.</param>
    /// <param name="slot">Its new <see cref="Slot"/>.</param>
    public void Move(int id, Archetype archetype, Slot slot)
    {
        ref var data = ref EntityData[id];
        data.Archetype = archetype;
        data.Slot = slot;
    }

    /// <summary>
    ///     Updates the <see cref="EntityData"/> and all entities that moved/shifted between the archetypes.
    ///     <remarks>Use and modify with caution, one small logical issue and the whole framework stops working.</remarks>
    /// </summary>
    /// <param name="archetype">The old <see cref="Archetype"/>.</param>
    /// <param name="archetypeSlot">The old <see cref="Slot"/> where the shift operation started.</param>
    /// <param name="newArchetype">The new <see cref="Archetype"/>.</param>
    /// <param name="newArchetypeSlot">The new <see cref="Slot"/> where the entities were shifted to.</param>
    public void Shift(Archetype archetype, Slot archetypeSlot, Archetype newArchetype, Slot newArchetypeSlot)
    {
        // Update the entityInfo of all copied entities.
        for (var chunkIndex = 0; chunkIndex <= archetypeSlot.ChunkIndex; chunkIndex++)
        {
            // Get data
            ref var chunk = ref archetype.GetChunk(chunkIndex);
            ref var entityFirstElement = ref chunk.Entity(0);

            // Only move within the range, depening on which chunk we are at.
            var isStart = chunkIndex == archetypeSlot.ChunkIndex;
            var upper = isStart ? archetypeSlot.Index : chunk.Count-1;

            for(var index = 0; index <= upper; index++)
            {
                var entity = Unsafe.Add(ref entityFirstElement, index);

                // Update entity info
                Move(entity.Id, newArchetype, newArchetypeSlot);
                newArchetypeSlot++;

                if (newArchetypeSlot.Index >= newArchetype.EntitiesPerChunk)
                {
                    newArchetypeSlot.Index = 0;
                    newArchetypeSlot.ChunkIndex++;
                }
            }
        }
    }

    /// <summary>
    ///     Ensures the capacity of the underlaying arrays and resizes them properly.
    /// </summary>
    /// <param name="capacity"></param>
    public void EnsureCapacity(int capacity)
    {
        EntityData.EnsureCapacity(capacity);
    }

    /// <summary>
    ///     Trims the <see cref="EntityInfoStorage"/> and all of its underlaying arrays.
    ///     Releases memory.
    /// </summary>
    public void TrimExcess()
    {
        EntityData.TrimExcess();
    }

    /// <summary>
    ///     Clears the <see cref="EntityInfoStorage"/> and all of its underlaying arrays.
    /// </summary>
    public void Clear()
    {
        EntityData.Clear();
    }

    /// <summary>
    ///     Returns a <see cref="EntityData"/> at an given index.
    /// </summary>
    /// <param name="id">The index.</param>
    internal ref EntityData this[int id]
    {
        get
        {
            return ref EntityData[id];
        }
    }
}



