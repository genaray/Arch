using System;
using System.Collections.Generic;
using System.Drawing;
using Arch.Core.Utils;
using Collections.Pooled;

namespace Arch.Core.CommandBuffer;



public struct CreateCommand
{
    public int Index;
    public ComponentType[] Types;
}

public struct BufferedEntityInfo
{
    public int Index;
    public int SetIndex;
    public int AddIndex;
    public int RemoveIndex;
}

/// <summary>
/// A command buffer to rule them all. 
/// </summary>
public class CommandBuffer : IDisposable
{

    /// <summary>
    /// The world
    /// </summary>
    public World World { get; }
    
    /// <summary>
    /// Amount of entities targeted by this buffer.
    /// </summary>
    public int Size { get; private set; }
    
    /// <summary>
    /// Entities targeted by this buffer.
    /// </summary>
    internal PooledList<Entity> _entities;
    
    /// <summary>
    /// Lookup
    /// </summary>
    internal PooledDictionary<int, BufferedEntityInfo> _bufferedEntityInfo;
    
    /// <summary>
    /// Create commands
    /// </summary>
    internal PooledList<CreateCommand> _creates;
    
    /// <summary>
    /// Set commands
    /// </summary>
    internal SparseSet _sets;
    
    /// <summary>
    /// Add commands
    /// </summary>
    internal SparseSet _adds;
    
    /// <summary>
    /// Remove commands
    /// </summary>
    internal StructuralSparseSet _removes;
    
    /// <summary>
    /// Destroy commands 
    /// </summary>
    internal PooledList<int> _destroys;
    
    private PooledList<ComponentType> _addTypes;
    private PooledList<ComponentType> _removeTypes;

    public CommandBuffer(World world, int initialCapacity = 128)
    {
        World = world;
        _entities = new PooledList<Entity>(initialCapacity);
        _bufferedEntityInfo = new PooledDictionary<int, BufferedEntityInfo>(initialCapacity);
        _creates = new PooledList<CreateCommand>(initialCapacity);
        _sets = new SparseSet(initialCapacity);
        _adds = new SparseSet(initialCapacity);
        _removes = new StructuralSparseSet(initialCapacity);
        _destroys = new PooledList<int>(initialCapacity);
        _addTypes = new PooledList<ComponentType>(16);
        _removeTypes = new PooledList<ComponentType>(16);
    }

    internal void Register(in Entity entity, out BufferedEntityInfo info)
    {
        var setIndex = _sets.Create(in entity);
        var addIndex = _adds.Create(in entity);
        var removeIndex = _removes.Create(in entity);
        info = new BufferedEntityInfo{ Index = Size, SetIndex = setIndex, AddIndex = addIndex, RemoveIndex = removeIndex};
        
        _entities.Add(entity);
        _bufferedEntityInfo.Add(entity.Id, info);
        Size++;
    }
    
    public Entity Create(ComponentType[] types)
    {
        var entity = new Entity(-Math.Abs(Size-1), World.Id);
        Register(entity, out _);
        
        var command = new CreateCommand { Index = Size-1, Types = types };
        _creates.Add(command);
        return entity;
    }

    public void Destroy(in Entity entity)
    {
        if(!_bufferedEntityInfo.TryGetValue(entity.Id, out var info))
            Register(entity, out info);
        
        _destroys.Add(info.Index);
    }

    public void Set<T>(in Entity entity, in T component)
    {
        if(!_bufferedEntityInfo.TryGetValue(entity.Id, out var info))
            Register(entity, out info);

        _sets.Set(info.SetIndex, in component);
    }

    public void Add<T>(in Entity entity, in T component)
    {
        if(!_bufferedEntityInfo.TryGetValue(entity.Id, out var info))
            Register(entity, out info);

        _adds.Set(info.AddIndex, in component);
    }

    public void Remove<T>(in Entity entity)
    {
        if(!_bufferedEntityInfo.TryGetValue(entity.Id, out var info))
            Register(entity, out info);

        _removes.Set<T>(info.RemoveIndex);
    }

    public void Playback()
    {
        
        // Create recorded entities
        foreach (var cmd in _creates)
        {
            var entity = World.Create(cmd.Types);
            _entities[cmd.Index] = entity;
        }

        // Playback adds
        for (var index = 0; index < _adds.Size; index++)
        {
            var wrappedEntity = _adds.Entities[index];
            for (var i = 0; i < _adds.UsedSize; i++)
            {
                ref var usedIndex = ref _adds.Used[i];
                ref var sparseSet = ref _adds.Components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _addTypes.Add(sparseSet.Type);
            }
            
            if(_addTypes.Count <= 0) continue;

            var entityIndex = _bufferedEntityInfo[wrappedEntity._entity.Id].Index;
            var entity = _entities[entityIndex];
            World.Add(in entity, (IList<ComponentType>)_addTypes);
            _addTypes.Clear();
        }
        
        // Playback removes 
        for (var index = 0; index < _removes.Size; index++)
        {
            var wrappedEntity = _removes.Entities[index];
            for (var i = 0; i < _removes.UsedSize; i++)
            {
                ref var usedIndex = ref _removes.Used[i];
                ref var sparseSet = ref _removes.Components[usedIndex];
                if(!sparseSet.Has(wrappedEntity._index)) continue;

                _removeTypes.Add(sparseSet.Type);
            }
            if(_removeTypes.Count <= 0) continue;
            
            var entityIndex = _bufferedEntityInfo[wrappedEntity._entity.Id].Index;
            var entity = _entities[entityIndex];
            World.Remove(in entity, _removeTypes);
            _removeTypes.Clear();
        }
        
        // Loop over all sparset entities
        for (var index = 0; index < _sets.Size; index++)
        {
            // Get wrapped entity
            var wrappedEntity = _sets.Entities[index];
            var entityIndex = _bufferedEntityInfo[wrappedEntity._entity.Id].Index;
            var entity = _entities[entityIndex];
            ref readonly var id = ref wrappedEntity._index;
            
            // Get entity chunk
            var entityInfo = World.EntityInfo[entity.Id];
            var archetype = entityInfo.Archetype;
            ref readonly var chunk = ref archetype.GetChunk(entityInfo.Slot.ChunkIndex);
            var chunkIndex = entityInfo.Slot.Index;
            
            // Loop over all sparset component arrays and if our entity is in one, copy the set component to its chunk 
            for (var i = 0; i < _sets.UsedSize; i++)
            {
                var used = _sets.Used[i];
                var sparseArray = _sets.Components[used];
                if(!sparseArray.Has(id)) continue;

                var chunkArray = chunk.GetArray(sparseArray.Type);
                Array.Copy(sparseArray.Components, id, chunkArray, chunkIndex, 1);
            }
        }
        
        // Create recorded entities
        foreach (var cmd in _destroys)
        {
            World.Destroy(_entities[cmd]);
        }

        Size = 0;
        _entities?.Clear();
        _bufferedEntityInfo?.Clear();
        _creates?.Clear();
        _sets?.Dispose();
        _adds?.Dispose();
        _removes?.Dispose();
        _destroys?.Clear();
        _addTypes?.Clear();
        _removeTypes?.Clear();
    }

    public void Dispose()
    {
        _entities?.Dispose();
        _bufferedEntityInfo?.Dispose();
        _creates?.Dispose();
        _sets?.Dispose();
        _adds?.Dispose();
        _removes?.Dispose();
        _destroys?.Dispose();
        _addTypes?.Dispose();
        _removeTypes?.Dispose();
    }
}