using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Extensions.Internal;
using Arch.LowLevel.Jagged;

namespace Arch.Core;

/// <summary>
///     The <see cref="EntityInfo"/> struct
///     stores information about an <see cref="Entity"/> to quickly access its data and location.
/// </summary>
[SkipLocalsInit]
internal struct EntityInfo
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
    ///     A reference to its version.
    /// </summary>
    public int Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityInfo"/> struct.
    /// </summary>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    /// <param name="version">Its version.</param>
    public EntityInfo(Archetype archetype, Slot slot, int version)
    {
        Archetype = archetype;
        Slot = slot;
        Version = version;
    }
}

/// <summary>
///     The <see cref="EntityInfo"/> struct
///     stores information about an <see cref="Entity"/> to quickly access its data and location.
/// </summary>
[SkipLocalsInit]
internal struct EntitySlot
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
    ///     Initializes a new instance of the <see cref="EntityInfo"/> struct.
    /// </summary>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    public EntitySlot(Archetype archetype, Slot slot)
    {
        Archetype = archetype;
        Slot = slot;
    }
}

/// <summary>
///     The <see cref="EntityInfoStorage"/> class
///     acts as an API and Manager to acess all <see cref="Entity"/> meta data and informations like its version, its <see cref="Archetype"/> or the <see cref="Chunk"/> it is in.
/// </summary>
internal class EntityInfoStorage
{

    /// <summary>
    ///     The <see cref="Entity"/> versions in an jagged array.
    /// </summary>
    internal JaggedArray<int> Versions { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

    /// <summary>
    ///     The <see cref="Entity"/> <see cref="Archetype"/> and <see cref="Slot"/>s in an jagged array.
    /// <remarks>Because usually both are needed and thus an array access can be saved.</remarks>
    /// </summary>
    internal JaggedArray<EntitySlot> EntitySlots { [MethodImpl(MethodImplOptions.AggressiveInlining)] get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityInfoStorage"/> class.
    /// </summary>
    internal EntityInfoStorage()
    {
        var cpuL1CacheSize = 16_000;

        Versions = new JaggedArray<int>(
            cpuL1CacheSize / Unsafe.SizeOf<int>(),
            -1,
            256
        );
        EntitySlots = new JaggedArray<EntitySlot>(
            cpuL1CacheSize / Unsafe.SizeOf<EntitySlot>(),
            new EntitySlot(null, new Slot(-1,-1)),
            256
        );
    }

    /// <summary>
    ///     Adds meta data of an <see cref="Entity"/> to the internal structure.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="version">Its version.</param>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(int id, int version, Archetype archetype, Slot slot)
    {
        Versions.Add(id, version);
        EntitySlots.Add(id,new EntitySlot(archetype, slot));
    }

    /// <summary>
    ///     Checks whether an <see cref="Entity"/>s data exists in this <see cref="EntityInfoStorage"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>True if its data exists in here, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Has(int id)
    {
        return Versions.TryGetValue(id, out int _);
    }

    /// <summary>
    ///     Returns the <see cref="Archetype"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Archetype"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Archetype GetArchetype(int id)
    {
        return EntitySlots[id].Archetype;
    }

    /// <summary>
    ///     Returns the <see cref="Slot"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Slot"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref Slot GetSlot(int id)
    {
        return ref EntitySlots[id].Slot;
    }

    /// <summary>
    ///     Returns the version of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Slot"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetVersion(int id)
    {
        return Versions[id];
    }

    /// <summary>
    ///     Trys to return the version of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <param name="version">The <see cref="Entity"/>s version.</param>
    /// <returns>True if it exists, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVersion(int id, out int version)
    {
        return Versions.TryGetValue(id, out version);
    }

    /// <summary>
    ///     Returns the <see cref="EntitySlot"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="EntitySlot"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public EntitySlot GetEntitySlot(int id)
    {
        return EntitySlots[id];
    }

    /// <summary>
    ///     Removes an enlisted <see cref="Entity"/> from this <see cref="EntityInfoStorage"/>.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(int id)
    {
        Versions.Remove(id);
        EntitySlots.Remove(id);
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> to a new <see cref="Slot"/>, updates that reference.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="slot">Its new <see cref="Slot"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Move(int id, Slot slot)
    {
        EntitySlots[id].Slot = slot;
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> to a new <see cref="Archetype"/> and a new <see cref="Slot"/>, updates that reference.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="archetype">Its new <see cref="Archetype"/>.</param>
    /// <param name="slot">Its new <see cref="Slot"/>.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Move(int id, Archetype archetype, Slot slot)
    {
        EntitySlots[id] = new EntitySlot(archetype,slot);
    }

    /// TODO : Find a cleaner way to break? One that does NOT require a branching?
    /// <summary>
    ///     Updates the <see cref="EntityInfo"/> and all entities that moved/shifted between the archetypes.
    ///     <remarks>Use and modify with caution, one small logical issue and the whole framework stops working.</remarks>
    /// </summary>
    /// <param name="archetype">The old <see cref="Archetype"/>.</param>
    /// <param name="archetypeSlot">The old <see cref="Slot"/> where the shift operation started.</param>
    /// <param name="newArchetype">The new <see cref="Archetype"/>.</param>
    /// <param name="newArchetypeSlot">The new <see cref="Slot"/> where the entities were shifted to.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Shift(Archetype archetype, Slot archetypeSlot, Archetype newArchetype, Slot newArchetypeSlot)
    {
        // Update the entityInfo of all copied entities.
        //for (var chunkIndex = archetypeSlot.ChunkIndex; chunkIndex >= 0; --chunkIndex)
        for (var chunkIndex = 0; chunkIndex <= archetypeSlot.ChunkIndex; chunkIndex++)
        {
            // Get data
            ref var chunk = ref archetype.GetChunk(chunkIndex);
            ref var entityFirstElement = ref chunk.Entity(0);

            // Only move within the range, depening on which chunk we are at.
            var isStart = chunkIndex == archetypeSlot.ChunkIndex;
            var upper = isStart ? archetypeSlot.Index : chunk.Size-1;

            //for (var index = upper; index >= 0; --index)
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void EnsureCapacity(int capacity)
    {
        Versions.EnsureCapacity(capacity);
        EntitySlots.EnsureCapacity(capacity);
    }

    /// <summary>
    ///     Trims the <see cref="EntityInfoStorage"/> and all of its underlaying arrays.
    ///     Releases memory.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void TrimExcess()
    {
        Versions.TrimExcess();
        EntitySlots.TrimExcess();
    }

    /// <summary>
    ///     Clears the <see cref="EntityInfoStorage"/> and all of its underlaying arrays.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Versions.Clear();
        EntitySlots.Clear();
    }

    /// <summary>
    ///     Returns a <see cref="EntityInfo"/> at an given index.
    /// </summary>
    /// <param name="id">The index.</param>
    internal EntityInfo this[int id]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var entitySlot = EntitySlots[id];
            return new EntityInfo(entitySlot.Archetype, entitySlot.Slot, Versions[id]);
        }
    }
}



