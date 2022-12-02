using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Arch.Core.CommandBuffer;

public struct StructuralBuffer : IDisposable
{

    internal World _world;
    internal ConcurrentDictionary<Entity, List<Type>> _adds;
    internal ConcurrentDictionary<Entity, List<Type>> _removes;

    public StructuralBuffer(World world)
    {
        _world = world;
        _adds = new ConcurrentDictionary<Entity, List<Type>>();
        _removes = new ConcurrentDictionary<Entity, List<Type>>();
    }
    
    /// <summary>
    /// Adds a component for a wrapped entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add<T>(in Entity entity)
    {
        if (!_adds.TryGetValue(entity, out var adds))
        {
            adds = new List<Type>(8);
            _adds[entity] = adds;
        }
        adds.Add(typeof(T));
    }

    /// <summary>
    /// Removes an component for a wrapped entity. 
    /// </summary>
    /// <param name="entity"></param>
    /// <typeparam name="T"></typeparam>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove<T>(in Entity entity)
    {
        if (!_removes.TryGetValue(entity, out var removes))
        {
            removes = new List<Type>(8);
            _removes[entity] = removes;
        }
        removes.Add(typeof(T));
    }
    
    /// <summary>
    /// Plays back all recorded operations. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        // Playback added
        foreach (var kvp in _adds)
        {
            var entity = kvp.Key;
            var addedComponents = kvp.Value;
            _world.Add(in entity, addedComponents);
        }
        
        // Playback removed
        foreach (var kvp in _removes)
        {
            var entity = kvp.Key;
            var removedComponents = kvp.Value;
            _world.Remove(in entity, removedComponents);
        }
        
        Dispose();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _adds.Clear();
        _removes.Clear();
    }
}