using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Samples;

// TODO: Documentation.
/// <summary>
///     The <see cref="SystemBase{T}"/> class
///     ...
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SystemBase<T>
{
    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="SystemBase{T}"/> class
    ///     ...
    /// </summary>
    /// <param name="world"></param>
    protected SystemBase(World world)
    {
        World = world;
    }

    // TODO: Documentation.
    public World World { get; private set; }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public abstract void Update(in T state);
}

// TODO: Documentation.
/// <summary>
///     The <see cref="MovementSystem"/> class
///     ...
/// </summary>
public class MovementSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToMove = new QueryDescription().WithAll<Position, Velocity>();
    private readonly Rectangle _viewport;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="MovementSystem"/> class
    ///     ...
    /// </summary>
    /// <param name="world"></param>
    /// <param name="viewport"></param>
    public MovementSystem(World world, Rectangle viewport)
        : base(world)
    {
        _viewport = viewport;
    }

    // TODO: Documentation.
    /// <summary>
    ///     The <see cref="Move"/> struct
    ///     ...
    /// </summary>
    private readonly struct Move : IForEach<Position, Velocity>
    {
        private readonly float _deltaTime;

        // TODO: Documentation.
        /// <summary>
        ///     Initializes a new instance of the <see cref="Move"/> struct
        ///     ...
        /// </summary>
        /// <param name="deltaTime"></param>
        public Move(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        // TODO: Documentation.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position pos, ref Velocity vel)
        {
            pos.Vec2 += _deltaTime * vel.Vec2;
        }
    }

    // TODO: Documentation.
    /// <summary>
    ///     The <see cref="Bounce"/> struct
    ///     ...
    /// </summary>
    private struct Bounce : IForEach<Position, Velocity>
    {
        private Rectangle _viewport;

        // TODO: Documentation.
        /// <summary>
        ///     Initializes a new instance of the <see cref="Bounce"/> struct
        ///     ...
        /// </summary>
        /// <param name="viewport"></param>
        public Bounce(Rectangle viewport)
        {
            _viewport = viewport;
        }

        // TODO: Documentation.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
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

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    public override void Update(in GameTime time)
    {
        // Iterates over all entities ( based on the passed QueryDescription ), accesses their Position and Velocity Components and updates them.
        // Highperformance and inlined calls for maximum effiency. 
        var movementJob = new Move((float)time.ElapsedGameTime.TotalMilliseconds);
        World.HPParallelQuery<Move, Position, Velocity>(in _entitiesToMove, ref movementJob);

        // Iterates over the same entities, accesses the same components. But executes the "Bounce" struct. 
        // Checks whether the entity hit the viewport bounds and inverts its velocity to make it bounce. 
        var bounceJob = new Bounce(_viewport);
        World.HPParallelQuery<Bounce, Position, Velocity>(in _entitiesToMove, ref bounceJob);
    }
}

// TODO: Documentation.
/// <summary>
///     The <see cref="ColorSystem"/> class
///     ...
/// </summary>
public class ColorSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToChangeColor = new QueryDescription().WithAll<Sprite>();
    private static GameTime? _gameTime;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="ColorSystem"/> class
    ///     ...
    /// </summary>
    /// <param name="world"></param>
    public ColorSystem(World world)
        : base(world) { }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
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

// TODO: Documentation.
/// <summary>
///     The <see cref="DrawSystem"/> class
///     ...
/// </summary>
public class DrawSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToDraw = new QueryDescription().WithAll<Position, Sprite>();
    private readonly SpriteBatch _batch;

    // TODO: Documentation.
    /// <summary>
    ///     Initializes a new instance of the <see cref="DrawSystem"/> class
    ///     ...
    /// </summary>
    /// <param name="world"></param>
    /// <param name="batch"></param>
    public DrawSystem(World world, SpriteBatch batch)
        : base(world)
    {
        _batch = batch;
    }

    // TODO: Documentation.
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
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
