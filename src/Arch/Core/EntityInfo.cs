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
public struct EntityData : IEquatable<EntityData>
{
    /// <summary>
    ///     A reference to its <see cref="Archetype"/>.
    /// </summary>
    public Archetype Archetype;

    /// <summary>
    ///     Its <see cref="Slot"/>.
    /// </summary>
    public Slot Slot;

    /// <summary>
    ///     Its version.
    /// </summary>
    public readonly int Version;

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityData"/> struct.
    /// </summary>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    /// <param name="version">Its version.</param>
    public EntityData(Archetype archetype, Slot slot, int version)
    {
        Archetype = archetype;
        Slot = slot;
        Version = version;
    }

    /// <summary>
    ///     Returns the <see cref="Entity"/> associated with this instance.
    /// </summary>
    public readonly Entity Entity
    {
        get => Archetype.GetChunk(Slot.ChunkIndex).Entity(Slot.Index);
    }

    /// <summary>
    ///     Returns the <see cref="Chunk"/> associated with this instance.
    /// </summary>
    public readonly ref Chunk Chunk
    {
        get => ref Archetype.GetChunk(Slot.ChunkIndex);
    }

    /// <summary>
    ///     Returns a reference to the component of the given type.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>A reference.</returns>
    public ref T Get<T>()
    {
        return ref Archetype.GetChunk(Slot.ChunkIndex).Get<T>(Slot.Index);
    }

    /// <summary>
    ///     Sets a component of the given type.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>A reference.</returns>
    public void Set<T>(in T value)
    {
        Archetype.GetChunk(Slot.ChunkIndex).Copy<T>(Slot.Index, in value);
    }

    /// <summary>
    ///     Moves an <see cref="Entity"/> to a new <see cref="Archetype"/> and a new <see cref="Slot"/>, updates that reference.
    /// </summary>
    /// <param name="archetype">Its new <see cref="Archetype"/>.</param>
    /// <param name="slot">Its new <see cref="Slot"/>.</param>
    internal void Move(Archetype archetype, Slot slot)
    {
        Archetype = archetype;
        Slot = slot;
    }

    /// <summary>
    ///     Returns true if its equal to the passed instance.
    /// </summary>
    /// <param name="other">The other instance.</param>
    /// <returns>True or false.</returns>
    public bool Equals(EntityData other)
    {
        return Version == other.Version && Archetype != null && Archetype.Equals(other.Archetype) && Slot.Equals(other.Slot);
    }

    /// <summary>
    ///     Returns true if its equal to the passed instance.
    /// </summary>
    /// <param name="obj">The other instance.</param>
    /// <returns>True or false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is EntityData other && Equals(other);
    }

    /// <summary>
    ///     Returns the hashcode of this instance.
    /// </summary>
    /// <returns>The hashcode.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Archetype.GetHashCode();
            hashCode = (hashCode * 397) ^ Slot.GetHashCode();
            hashCode = (hashCode * 397) ^ Version;
            return hashCode;
        }
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
            new EntityData(null!, new Slot(-1,-1), 0),
            capacity
        );
    }

    /// <summary>
    ///     Adds metadata of an <see cref="Entity"/> to the internal structure.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/> id.</param>
    /// <param name="archetype">Its <see cref="Archetype"/>.</param>
    /// <param name="slot">Its <see cref="Slot"/>.</param>
    /// <param name="version">Its version.</param>
    public void Add(int id, Archetype archetype, Slot slot, int version)
    {
        EntityData.Add(id,new EntityData(archetype, slot, version));
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
    ///     Returns the version of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its version.</returns>
    public int GetVersion(int id)
    {
        return EntityData[id].Version;
    }

    /// <summary>
    ///     Returns the <see cref="Core.EntityData"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <returns>Its <see cref="Core.EntityData"/>.</returns>
    public ref EntityData GetEntityData(int id)
    {
        return ref EntityData[id];
    }

    /// <summary>
    ///     Returns the <see cref="Core.EntityData"/> of an <see cref="Entity"/> by its id.
    /// </summary>
    /// <param name="id">The <see cref="Entity"/>s id.</param>
    /// <param name="exists">If it exists or not</param>
    /// <returns>Its <see cref="Core.EntityData"/>.</returns>
    public ref EntityData TryGetEntityData(int id, out bool exists)
    {
        return ref EntityData.TryGetValue(id, out exists);
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
        data.Move(archetype, slot);
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
}



