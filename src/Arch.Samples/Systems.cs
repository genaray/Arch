using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Samples;

/// <summary>
/// Represents a base for systems which process entities. 
/// </summary>
/// <typeparam name="T">The passed type to the update method.</typeparam>
public abstract class SystemBase<T>
{
    protected SystemBase(World world)
    {
        World = world;
    }

    public World World { get; private set; }

    /// <summary>
    /// Updates the system. 
    /// </summary>
    /// <param name="state"></param>
    public abstract void Update(in T state);
}

/// <summary>
/// The movement system makes the entities move and bounce properly. 
/// </summary>
public class MovementSystem : SystemBase<GameTime>
{
    /// <summary>
    /// Defines a query, a description of which entitys we target.
    /// This is based upon their component composition. To update all entity positions, we only need entities with Position AND Velocit components. 
    /// </summary>
    private readonly QueryDescription _entitiesToMove = new QueryDescription().WithAll<Position, Velocity>();

    private readonly Rectangle _viewport;

    public MovementSystem(World world, Rectangle viewport)
        : base(world)
    {
        _viewport = viewport;
    }

    /// <summary>
    /// A inlined struct implementation of movement logic.
    /// Updates the position by adding the velocity to it, can be inlined. 
    /// </summary>
    private readonly struct Move : IForEach<Position, Velocity>
    {
        private readonly float _deltaTime;

        public Move(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position pos, ref Velocity vel)
        {
            pos.Vec2 += _deltaTime * vel.Vec2;
        }
    }

    /// <summary>
    /// Checks for each entity if it hits the viewport bounds and makes it bounce. 
    /// </summary>
    private struct Bounce : IForEach<Position, Velocity>
    {
        private Rectangle _viewport;
        public Bounce(Rectangle viewport)
        {
            _viewport = viewport;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position pos, ref Velocity vel)
        {
            if (pos.Vec2.X >= _viewport.X + _viewport.Width)
            {
                vel.Vec2.X = -vel.Vec2.X;
            }

            if (pos.Vec2.Y >= _viewport.Y + _viewport.Height)
            {
                vel.Vec2.Y = -vel.Vec2.Y;
            }

            if (pos.Vec2.X <= _viewport.X)
            {
                vel.Vec2.X = -vel.Vec2.X;
            }

            if (pos.Vec2.Y <= _viewport.Y)
            {
                vel.Vec2.Y = -vel.Vec2.Y;
            }
        }
    }

    /// <summary>
    /// Gets called once per frame
    /// </summary>
    public override void Update(in GameTime time)
    {
        // Iterates over all entities ( based on the passed QueryDescription ), acesses their Position and Velocity Components and updates them.
        // Highperformance and inlined calls for maximum effiency. 
        var movementJob = new Move((float)time.ElapsedGameTime.TotalMilliseconds);
        World.HPParallelQuery<Move, Position, Velocity>(in _entitiesToMove, ref movementJob);

        // Iterates over the same entities, acesses the same components. But executes the "Bounce" struct. 
        // Checks whether the entity hit the viewport bounds and inverts its velocity to make it bounce. 
        var bounceJob = new Bounce(_viewport);
        World.HPParallelQuery<Bounce, Position, Velocity>(in _entitiesToMove, ref bounceJob);
    }
}

/// <summary>
/// Color system, modifies each entities color slowly. 
/// </summary>
public class ColorSystem : SystemBase<GameTime>
{
    /// <summary>
    /// Targeting all entities with a sprite component. 
    /// </summary>
    private readonly QueryDescription _entitiesToChangeColor = new QueryDescription().WithAll<Sprite>();

    private static GameTime? _gameTime;

    public ColorSystem(World world)
        : base(world) { }

    /// <summary>
    /// Gets called once per frame
    /// </summary>
    public override void Update(in GameTime time)
    {
        _gameTime = time;
        World.Query(in _entitiesToChangeColor, (ref Sprite sprite) =>
        {
            sprite.Color.R += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
            sprite.Color.G += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
            sprite.Color.B += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
        });
    }
}

/// <summary>
/// The draw system, handles the drawing of entity sprites at their position. 
/// </summary>
public class DrawSystem : SystemBase<GameTime>
{
    /// <summary>
    /// Targets all entities with a position and sprite
    /// </summary>
    private readonly QueryDescription _entitiesToDraw = new QueryDescription().WithAll<Position, Sprite>();

    private readonly SpriteBatch _batch;

    public DrawSystem(World world, SpriteBatch batch)
        : base(world)
    {
        _batch = batch;
    }

    /// <summary>
    /// Gets called once per frame
    /// </summary>
    public override void Update(in GameTime time)
    {
        _batch.Begin();

        // Get query for the description, targets all entities with "Positions" and "Sprite".
        var query = World.Query(in _entitiesToDraw);
        foreach (ref var chunk in query.GetChunkIterator())   // Iterate over each chunk that has entities that fit the query.
        {
            // Receive raw arrays of positions and sprites from the chunk.
            var positions = chunk.GetSpan<Position>();
            var sprites = chunk.GetSpan<Sprite>();

            // Loop over the chunk
            for (var index = 0; index < chunk.Size; index++)
            {
                // Get refs to position and sprite.
                ref var position = ref positions[index];
                ref var sprite = ref sprites[index];
                _batch.Draw(sprite.Texture2D, position.Vec2, sprite.Color);  // Draw
            }
        }

        _batch.End();
    }
}
