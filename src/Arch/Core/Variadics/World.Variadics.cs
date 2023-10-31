namespace Arch.Core;
public partial class World
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T0), 1, 25)]
    // [Variadic: CopyParams(T0)]
    public Entity Create<T0>(in T0 componentValue__T0 = default)
    {
        var types = Group<T0>.Types;

        // Recycle id or increase
        var recycle = RecycledIds.TryDequeue(out var recycledId);
        var recycled = recycle ? recycledId : new RecycledEntity(Size, 1);

        // Create new entity and put it to the back of the array
        var entity = new Entity(recycled.Id, Id);

        // Add to archetype & mapping
        var archetype = GetOrCreate(types);
        var createdChunk = archetype.Add(entity, out var slot);

        // [Variadic: CopyArgs(componentValue)]
        archetype.Set<T0>(ref slot, in componentValue__T0);

        // Resize map & Array to fit all potential new entities
        if (createdChunk)
        {
            Capacity += archetype.EntitiesPerChunk;
            EntityInfo.EnsureCapacity(Capacity);
        }

        // Map
        EntityInfo.Add(entity.Id, recycled.Version, archetype, slot);

        Size++;
        OnEntityCreated(entity);

        // [Variadic: CopyLines]
        OnComponentAdded<T0>(entity);
        return entity;
    }
}
