using System.Diagnostics.Contracts;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;
using JobScheduler;

namespace Arch.Core;
public partial class World
{
    /// <inheritdoc cref="Create(Span{ComponentType})"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T0), 1, 25)]
    // [Variadic: CopyParams(T0?)]
    public Entity Create<T0>(in T0? componentValue__T0 = default)
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

    /// <inheritdoc cref="Has{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 2, 25)]
    public bool Has<T0, T1>(Entity entity)
    {
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Has<T0, T1>();
    }

    /// <inheritdoc cref="Get{T}"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 2, 25)]
    public Components<T0, T1> Get<T0, T1>(Entity entity)
    {
        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);
        return archetype.Get<T0, T1>(ref slot);
    }

    /// <inheritdoc cref="Set{T}(Entity, in T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    [Variadic(nameof(T1), 2, 25)]
    public void Set<T0, T1>(Entity entity, in T0 component__T0, in T1 component__T1)
    {
        var slot = EntityInfo.GetSlot(entity.Id);
        var archetype = EntityInfo.GetArchetype(entity.Id);
        // [Variadic: CopyArgs(component)]
        archetype.Set(ref slot, in component__T0, in component__T1);
        OnComponentSet<T0>(entity);
        // [Variadic: CopyLines]
        OnComponentSet<T1>(entity);
    }

    /// <inheritdoc cref="Add{T}(Entity)"/>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T1), 2, 25)]
    // [Variadic: CopyParams(T1?)]
    public void Add<T0, T1>(Entity entity, in T0? component__T0 = default, in T1? component__T1 = default)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.SetBit(Component<T0>.ComponentType.Id);
        // [Variadic: CopyLines]
        spanBitSet.SetBit(Component<T1>.ComponentType.Id);

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var type__T0 = typeof(T0);
            // [Variadic: CopyLines]
            var type__T1 = typeof(T1);
            // [Variadic: CopyArgs(type)]
            newArchetype = GetOrCreate(oldArchetype.Types.Add(type__T0, type__T1));
        }

        Move(entity, oldArchetype, newArchetype, out var newSlot);

        // [Variadic: CopyArgs(component)]
        newArchetype.Set(ref newSlot, in component__T0, in component__T1);

        OnComponentAdded<T0>(entity);
        // [Variadic: CopyLines]
        OnComponentAdded<T1>(entity);
    }

    /// <inheritdoc cref="Remove{T}(Entity)"/>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T1), 2, 25)]
    public void Remove<T0, T1>(Entity entity)
    {
        var oldArchetype = EntityInfo.GetArchetype(entity.Id);

        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[oldArchetype.BitSet.Length];
        oldArchetype.BitSet.AsSpan(stack);

        // Create a span bitset, doing it local saves us headache and gargabe
        var spanBitSet = new SpanBitSet(stack);
        spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
        // [Variadic: CopyLines]
        spanBitSet.ClearBit(Component<T1>.ComponentType.Id);

        if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
        {
            var type__T0 = typeof(T0);
            // [Variadic: CopyLines]
            var type__T1 = typeof(T1);
            // [Variadic: CopyArgs(type)]
            newArchetype = GetOrCreate(oldArchetype.Types.Remove(type__T0, type__T1));
        }

        OnComponentRemoved<T0>(entity);
        // [Variadic: CopyLines]
        OnComponentRemoved<T1>(entity);
        Move(entity, oldArchetype, newArchetype, out _);
    }

    /// <inheritdoc cref="Query(in QueryDescription, ForEach)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void Query<T0>(in QueryDescription description, ForEach<T0> forEach)
    {
        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                forEach(ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="Query(in QueryDescription, ForEach)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void Query<T0>(in QueryDescription description, ForEachWithEntity<T0> forEach)
    {
        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            ref var entityFirstElement = ref chunk.Entity(0);
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                forEach(entity, ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="ParallelQuery(in QueryDescription, ForEach)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void ParallelQuery<T0>(in QueryDescription description, ForEach<T0> forEach)
    {
        var innerJob = new ForEachJob<T0>
        {
            ForEach = forEach
        };

        var pool = JobMeta<ChunkIterationJob<ForEachJob<T0>>>.Pool;
        var query = Query(in description);
        foreach (var archetype in query.GetArchetypeIterator())
        {

            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = pool.Get();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                JobsCache.Add(job);
            }

            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
            {
                var job = Unsafe.As<ChunkIterationJob<ForEachJob<T0>>>(JobsCache[jobIndex]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }

    /// <inheritdoc cref="ParallelQuery(in QueryDescription, ForEach)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void ParallelQuery<T0>(in QueryDescription description, ForEachWithEntity<T0> forEach)
    {
        var innerJob = new ForEachWithEntityJob<T0>
        {
            ForEach = forEach
        };

        var pool = JobMeta<ChunkIterationJob<ForEachWithEntityJob<T0>>>.Pool;
        var query = Query(in description);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = pool.Get();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                JobsCache.Add(job);
            }

            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
            {
                var job = Unsafe.As<ChunkIterationJob<ForEachWithEntityJob<T0>>>(JobsCache[jobIndex]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }

    /// <inheritdoc cref="InlineQuery{T}(in QueryDescription, ref T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineQuery<T, T0>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<T0>
    {
        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                iForEach.Update(ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="InlineQuery{T}(in QueryDescription)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineQuery<T, T0>(in QueryDescription description) where T : struct, IForEach<T0>
    {
        var t = new T();

        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            var chunkSize = chunk.Size;
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                t.Update(ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="InlineQuery{T}(in QueryDescription, ref T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineEntityQuery<T, T0>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0>
    {
        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref chunk.Entity(0);
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                iForEach.Update(entity, ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="InlineQuery{T}(in QueryDescription)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineEntityQuery<T, T0>(in QueryDescription description) where T : struct, IForEachWithEntity<T0>
    {
        var t = new T();

        var query = Query(in description);
        foreach (ref var chunk in query)
        {
            var chunkSize = chunk.Size;
            ref var entityFirstElement = ref chunk.Entity(0);
            // [Variadic: CopyLines]
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();

            foreach (var entityIndex in chunk)
            {
                var entity = Unsafe.Add(ref entityFirstElement, entityIndex);
                // [Variadic: CopyLines]
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyArgs(component)]
                t.Update(entity, ref component__T0);
            }
        }
    }

    /// <inheritdoc cref="InlineParallelQuery{T}(in QueryDescription, in IForEachJob{T})"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineParallelQuery<T, T0>(in QueryDescription description, ref T iForEach) where T : struct, IForEach<T0>
    {
        var innerJob = new IForEachJob<T, T0>
        {
            ForEach = iForEach
        };

        var pool = JobMeta<ChunkIterationJob<IForEachJob<T, T0>>>.Pool;
        var query = Query(in description);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = pool.Get();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                JobsCache.Add(job);
            }

            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
            {
                var job = Unsafe.As<ChunkIterationJob<IForEachJob<T, T0>>>(JobsCache[jobIndex]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }

    /// <inheritdoc cref="InlineParallelQuery{T}(in QueryDescription, in IForEachJob{T})"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T0), 1, 25)]
    public void InlineParallelEntityQuery<T, T0>(in QueryDescription description, ref T iForEach) where T : struct, IForEachWithEntity<T0>
    {
        var innerJob = new IForEachWithEntityJob<T, T0>
        {
            ForEach = iForEach
        };

        var pool = JobMeta<ChunkIterationJob<IForEachWithEntityJob<T, T0>>>.Pool;
        var query = Query(in description);
        foreach (var archetype in query.GetArchetypeIterator())
        {

            var archetypeSize = archetype.Size;
            var part = new RangePartitioner(Environment.ProcessorCount, archetypeSize);
            foreach (var range in part)
            {
                var job = pool.Get();
                job.Start = range.Start;
                job.Size = range.Length;
                job.Chunks = archetype.Chunks;
                job.Instance = innerJob;
                JobsCache.Add(job);
            }

            IJob.Schedule(JobsCache, JobHandles);
            JobScheduler.JobScheduler.Instance.Flush();
            JobHandle.Complete(JobHandles);
            JobHandle.Return(JobHandles);

            // Return jobs to pool
            for (var jobIndex = 0; jobIndex < JobsCache.Count; jobIndex++)
            {
                var job = Unsafe.As<ChunkIterationJob<IForEachWithEntityJob<T, T0>>>(JobsCache[jobIndex]);
                pool.Return(job);
            }

            JobHandles.Clear();
            JobsCache.Clear();
        }
    }

    /// <inheritdoc cref="Set{T}(in QueryDescription, in T)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Variadic(nameof(T1), 2, 25)]
    // [Variadic: CopyParams(T1?)]
    public void Set<T0, T1>(in QueryDescription queryDescription, in T0? componentValue__T0 = default, in T1? componentValue__T1 = default)
    {
        var query = Query(in queryDescription);
        foreach (ref var chunk in query)
        {
            ref var firstElement__T0 = ref chunk.GetFirst<T0>();
            // [Variadic: CopyLines]
            ref var firstElement__T1 = ref chunk.GetFirst<T1>();

            foreach (var entityIndex in chunk)
            {
                ref var component__T0 = ref Unsafe.Add(ref firstElement__T0, entityIndex);
                // [Variadic: CopyLines]
                ref var component__T1 = ref Unsafe.Add(ref firstElement__T1, entityIndex);
                component__T0 = componentValue__T0;
                // [Variadic: CopyLines]
                component__T1 = componentValue__T1;
#if EVENTS
                var entity = chunk.Entity(entityIndex);
                OnComponentSet<T0>(entity);
                // [Variadic: CopyLines]
                OnComponentSet<T1>(entity);
#endif
            }
        }
    }

    /// <inheritdoc cref="Add{T}(in QueryDescription, in T)"/>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T1), 2, 25)]
    // [Variadic: CopyParams(T1?)]
    public void Add<T0, T1>(in QueryDescription queryDescription, in T0? component__T0 = default, in T1? component__T1 = default)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype with T shouldnt be skipped to prevent undefined behaviour.
            if (archetype.Entities == 0 || archetype.Has<T0, T1>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            archetype.BitSet.AsSpan(stack);
            var spanBitSet = new SpanBitSet(stack);
            spanBitSet.SetBit(Component<T0>.ComponentType.Id);
            // [Variadic: CopyLines]
            spanBitSet.SetBit(Component<T1>.ComponentType.Id);

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var type__T0 = typeof(T0);
                // [Variadic: CopyLines]
                var type__T1 = typeof(T1);
                // [Variadic: CopyArgs(type)]
                newArchetype = GetOrCreate(archetype.Types.Add(type__T0, type__T1));
            }

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.LastSlot;
            var newArchetypeLastSlot = newArchetype.LastSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            // Copy, set and clear
            Archetype.Copy(archetype, newArchetype);
            var lastSlot = newArchetype.LastSlot;
            // [Variadic: CopyArgs(component)]
            newArchetype.SetRange(in lastSlot, in newArchetypeLastSlot, in component__T0, in component__T1);
            OnComponentAdded<T0>(archetype);
            // [Variadic: CopyLines]
            OnComponentAdded<T1>(archetype);
            archetype.Clear();
        }
    }

    /// <inheritdoc cref="Remove{T}(in QueryDescription)"/>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [StructuralChange]
    [Variadic(nameof(T1), 2, 25)]
    public void Remove<T0, T1>(in QueryDescription queryDescription)
    {
        // BitSet to stack/span bitset, size big enough to contain ALL registered components.
        Span<uint> stack = stackalloc uint[BitSet.RequiredLength(ComponentRegistry.Size)];

        var query = Query(in queryDescription);
        foreach (var archetype in query.GetArchetypeIterator())
        {
            // Archetype without T shouldnt be skipped to prevent undefined behaviour.
            if (archetype.Entities <= 0 || !archetype.Has<T0, T1>())
            {
                continue;
            }

            // Create local bitset on the stack and set bits to get a new fitting bitset of the new archetype.
            var bitSet = archetype.BitSet;
            var spanBitSet = new SpanBitSet(bitSet.AsSpan(stack));
            spanBitSet.ClearBit(Component<T0>.ComponentType.Id);
            // [Variadic: CopyLines]
            spanBitSet.ClearBit(Component<T1>.ComponentType.Id);

            // Get or create new archetype.
            if (!TryGetArchetype(spanBitSet.GetHashCode(), out var newArchetype))
            {
                var type__T0 = typeof(T0);
                // [Variadic: CopyLines]
                var type__T1 = typeof(T1);
                // [Variadic: CopyArgs(type)]
                newArchetype = GetOrCreate(archetype.Types.Remove(type__T0, type__T1));
            }

            OnComponentRemoved<T0>(archetype);
            // [Variadic: CopyLines]
            OnComponentRemoved<T1>(archetype);

            // Get last slots before copy, for updating entityinfo later
            var archetypeSlot = archetype.LastSlot;
            var newArchetypeLastSlot = newArchetype.LastSlot;
            Slot.Shift(ref newArchetypeLastSlot, newArchetype.EntitiesPerChunk);
            EntityInfo.Shift(archetype, archetypeSlot, newArchetype, newArchetypeLastSlot);

            Archetype.Copy(archetype, newArchetype);
            archetype.Clear();
        }
    }
}
