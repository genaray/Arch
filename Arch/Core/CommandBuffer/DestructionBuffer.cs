using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Arch.Core.CommandBuffer;

/// <summary>
/// A buffer which can buffer destroy operations to play them back later.
/// Playback should ONLY happen on the mainthread and not during a Query ! 
/// </summary>
public struct DestructionBuffer : IDisposable
{
    internal World _world;
    internal List<Entity> _destroy;

    public DestructionBuffer(World world, int initCapacity = 64)
    {
        _world = world;
        _destroy = new List<Entity>(initCapacity);
    }

    /// <summary>
    /// Buffers a destruction request for the passed <see cref="Entity"/>.
    /// On <see cref="Playback"/> the entity will be destroyed. 
    /// </summary>
    /// <param name="entity"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Destroy(in Entity entity)
    {
        lock (_destroy)
        {
            _destroy.Add(entity);
        }
    }

    /// <summary>
    /// Plays back this buffer and executes all recorded operations. 
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Playback()
    {
        foreach (var entity in _destroy)
            _world.Destroy(in entity);
        
        Dispose();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _destroy.Clear();
    }
}