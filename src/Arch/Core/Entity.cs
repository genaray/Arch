#if !PURE_ECS
using Arch.Core.Extensions;
using Arch.Core.Utils;
#endif

namespace Arch.Core;


#if PURE_ECS

/// <summary>
///     The <see cref="Entity"/> struct
///     represents a general-purpose object and can be assigned a set of components that act as data.
/// </summary>
[SkipLocalsInit]
public readonly struct Entity : IEquatable<Entity>, IComparable<Entity>
{
    /// <summary>
    ///     Its Id, unique in its <see cref="World"/>.
    /// </summary>
    public readonly int Id = -1;

    /// <summary>
    ///     A null entity, used for comparison.
    /// </summary>
    public static readonly Entity Null = new(-1, 0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="worldId">Its world id, not used for this entity since its pure ecs.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity(int id, int worldId)
    {
        Id = id;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Entity other)
    {
        return Id == other.Id;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another object..
    /// </summary>
    /// <param name="obj">The other <see cref="Entity"/> object.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is Entity other && Equals(other);
    }

    /// <summary>
    ///     Compares this <see cref="Entity"/> instace to another one for sorting and ordering.
    ///     <remarks>Orders them by id. Ascending.</remarks>
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>A int indicating their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Entity other)
    {
        return Id.CompareTo(other.Id);
    }

    /// <summary>
    ///     Calculates the hash of this <see cref="Entity"/>.
    /// </summary>
    /// <returns>Its hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap
            var hash = 17;
            hash = hash * 23 + Id;
            return hash;
        }
    }

    /// <summary>
    ///     Checks the left <see cref="Entity"/> for equality with the right one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if both are equal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///     Checks the left <see cref="Entity"/> for unequality with the right one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if both are unequal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Converts this entity to a string.
    /// </summary>
    /// <returns>A string.</returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}";
    }
}
#else

/// <summary>
///     The <see cref="Entity"/> struct
///     represents a general-purpose object and can be assigned a set of components that act as data.
/// </summary>
[DebuggerTypeProxy(typeof(EntityDebugView))]
[SkipLocalsInit]
public readonly struct Entity : IEquatable<Entity>, IComparable<Entity>
{
    /// <summary>
    ///      Its Id, unique in its <see cref="World"/>.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Its <see cref="World"/> id.
    /// </summary>
    public readonly int WorldId;

    /// <summary>
    ///     A null <see cref="Entity"/> used for comparison.
    /// </summary>
    public static Entity Null = new(-1, 0);

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct with default values.
    /// </summary>
    public Entity()
    {
        Id = -1;
        WorldId = 0;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Entity"/> struct.
    /// </summary>
    /// <param name="id">Its unique id.</param>
    /// <param name="worldId">Its <see cref="World"/> id.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal Entity(int id, int worldId)
    {
        Id = id;
        WorldId = worldId;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Entity other)
    {
        return Id == other.Id && WorldId == other.WorldId;
    }

    /// <summary>
    ///     Checks the <see cref="Entity"/> for equality with another <see cref="object"/>.
    /// </summary>
    /// <param name="obj">The other <see cref="Entity"/> object.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object obj)
    {
        return obj is Entity other && Equals(other);
    }


    /// <summary>
    ///     Compares this <see cref="Entity"/> instace to another one for sorting and ordering.
    ///     <remarks>Orders them by id and world. Ascending.</remarks>
    /// </summary>
    /// <param name="other">The other <see cref="Entity"/>.</param>
    /// <returns>A int indicating their order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Entity other)
    {
        return WorldId != other.WorldId ? WorldId.CompareTo(other.WorldId) : Id.CompareTo(other.Id);
    }

    /// <summary>
    ///     Calculates the hash of this <see cref="Entity"/>.
    /// </summary>
    /// <returns>Its hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        unchecked
        {
            // Overflow is fine, just wrap
            var hash = 17;
            hash = (hash * 23) + Id;
            hash = (hash * 23) + WorldId;
            return hash;
        }
    }

    /// <summary>
    ///      Checks the <see cref="Entity"/> for equality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if equal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Entity left, Entity right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///      Checks the <see cref="Entity"/> for unequality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="Entity"/>.</param>
    /// <param name="right">The right <see cref="Entity"/>.</param>
    /// <returns>True if unequal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Entity left, Entity right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Converts this <see cref="Entity"/> to a string.
    /// </summary>
    /// <returns>Its string.</returns>
    public override string ToString()
    {
        return $"Entity = {{ {nameof(Id)} = {Id}, {nameof(WorldId)} = {WorldId} }}";
    }
}
#endif

/// <summary>
///     The <see cref="EntityReference"/> struct
///     represents a reference to an <see cref="Entity"/> and its Version.
/// </summary>
[SkipLocalsInit]
public readonly struct EntityReference
{

    /// <summary>
    ///     The referenced <see cref="Entity"/>.
    /// </summary>
    public readonly Entity Entity;

    /// <summary>
    ///     Its version.
    /// </summary>
    public readonly int Version;

    /// <summary>
    ///     A null reference.
    /// </summary>
    public static readonly EntityReference Null = new(Entity.Null, -1);

    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityReference"/> struct.
    /// </summary>
    /// <param name="entity">The referenced <see cref="Entity"/>.</param>
    /// <param name="version">Its version.</param>
    internal EntityReference(in Entity entity, in int version)
    {
        Entity = entity;
        Version = version;
    }

    /// <summary>
    ///     Initializes a new null instance of the <see cref="EntityReference"/> struct.
    /// </summary>
    public EntityReference()
    {
        Entity = Entity.Null;
        Version = -1;
    }

#if PURE_ECS

    /// <summary>
    ///     Checks if the referenced <see cref="Entity"/> is still valid and alife.
    /// </summary>
    /// <param name="world">The <see cref="Entity"/> <see cref="World"/>..</param>
    /// <returns>True if its alive, otherwhise false.</returns>
    public bool IsAlive(World world)
    {
        if (this == Null)
        {
            return false;
        }

        var reference = world.Reference(Entity);
        return this == reference;
    }
#else
    /// <summary>
    ///     Checks if the referenced <see cref="Entity"/> is still valid and alife.
    /// </summary>
    /// <returns>True if its alive, otherwhise false.</returns>
    public bool IsAlive()
    {
        if (this == Null)
        {
            return false;
        }

        var reference = Entity.Reference();
        return this == reference;
    }
#endif


    /// <summary>
    ///     Checks the <see cref="EntityReference"/> for equality with another one.
    /// </summary>
    /// <param name="other">The other <see cref="EntityReference"/>.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(EntityReference other)
    {
        return Entity.Equals(other.Entity) && Version == other.Version;
    }

    /// <summary>
    ///     Checks the <see cref="EntityReference"/> for equality with another <see cref="object"/>.
    /// </summary>
    /// <param name="obj">The other <see cref="EntityReference"/> object.</param>
    /// <returns>True if equal, false if not.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return obj is EntityReference other && Equals(other);
    }

    /// <summary>
    ///     Calculates the hash of this <see cref="Entity"/>.
    /// </summary>
    /// <returns>Its hash.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        unchecked
        {
            return (Entity.GetHashCode() * 397) ^ Version;
        }
    }

    /// <summary>
    ///      Checks the <see cref="EntityReference"/> for equality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="EntityReference"/>.</param>
    /// <param name="right">The right <see cref="EntityReference"/>.</param>
    /// <returns>True if equal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(EntityReference left, EntityReference right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///      Checks the <see cref="EntityReference"/> for inequality with another one.
    /// </summary>
    /// <param name="left">The left <see cref="EntityReference"/>.</param>
    /// <param name="right">The right <see cref="EntityReference"/>.</param>
    /// <returns>True if inequal, otherwhise false.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(EntityReference left, EntityReference right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///     Implicitly converts an <see cref="EntityReference"/> into the
    ///     <see cref="Entity"/> that it is referencing.
    /// </summary>
    /// <param name="reference">The <see cref="EntityReference"/> to convert.</param>
    /// <returns>
    ///     The <see cref="Entity"/> referenced by this <see cref="EntityReference"/>.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Entity(EntityReference reference)
    {
        return reference.Entity;
    }

    /// <summary>
    ///     Converts this <see cref="EntityReference"/> to a string.
    /// </summary>
    /// <returns>Its string.</returns>
    public override string ToString()
    {
        return $"EntityReference = {{ {nameof(Entity)} = {Entity}, {nameof(Version)} = {Version} }}";
    }
}
