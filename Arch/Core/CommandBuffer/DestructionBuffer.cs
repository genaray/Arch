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
    
    /// <summary>
    /// The world this buffer playbacks to.
    /// </summary>
    public World World { get; }
    
    /// <summary>
    /// The internal list of entities to destroy upon playback. 
    /// </summary>
    internal List<Entity> _destroy;

    /// <summary>
    /// Creates a destruction buffer.
    /// </summary>
    /// <param name="world">The world this playbacks to.</param>
    /// <param name="initCapacity">The initial capacity.</param>
    public DestructionBuffer(World world, int initCapacity = 64)
    {
        World = world;
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
            World.Destroy(in entity);
        
        Dispose();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Dispose()
    {
        _destroy.Clear();
    }
}