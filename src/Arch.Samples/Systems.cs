using System.Diagnostics;
using Arch.Core;
using Arch.Core.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Samples;

/// <summary>
///     The <see cref="SystemBase{T}"/> class
///     is a rudimentary basis for all systems with some important methods and properties.
/// </summary>
/// <typeparam name="T">The generic type passed to the <see cref="Update"/> method.</typeparam>
public abstract class SystemBase<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SystemBase{T}"/> class.
    /// </summary>
    /// <param name="world">Its <see cref="World"/>.</param>
    protected SystemBase(World world)
    {
        World = world;
    }

    /// <summary>
    ///     The <see cref="World"/> for which this system works and must access.
    /// </summary>
    public World World { get; private set; }

    /// <summary>
    ///     Should be called within the update loop to update this system and executes its logic.
    /// </summary>
    /// <param name="state">A external state being passed to this method to be used.</param>
    public abstract void Update(in T state);
}

/// <summary>
///     The <see cref="MovementSystem"/> class
///     ensures that all <see cref="Entity"/>s move and stay within the screen and bounce off it.
/// </summary>
public sealed class MovementSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToMove = new QueryDescription().WithAll<Position, Velocity>();
    private readonly Rectangle _viewport;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MovementSystem"/> class.
    /// </summary>
    /// <param name="world">Its <see cref="World"/>.</param>
    /// <param name="viewport">The games viewport represented as a <see cref="Rectangle"/>.</param>
    public MovementSystem(World world, Rectangle viewport)
        : base(world)
    {
        _viewport = viewport;
    }

    /// <summary>
    ///     The <see cref="Move"/> struct
    ///     acts as an implementation of the movement logic to move <see cref="Entity"/>s.
    ///     Can be inlined, the great performance advantage of this approach.
    /// </summary>
    private readonly struct Move : IForEach<Position, Velocity>
    {
        private readonly float _deltaTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Move"/> struct.
        /// </summary>
        /// <param name="deltaTime">The games delta-time.</param>
        public Move(float deltaTime)
        {
            _deltaTime = deltaTime;
        }

        /// <summary>
        ///     Makes one <see cref="Entity"/> move.
        ///     Gets called automatically for all entities fitting a <see cref="QueryDescription"/>.
        /// </summary>
        /// <param name="pos">A reference to the <see cref="Entity"/>s <see cref="Position"/>.</param>
        /// <param name="vel">A reference to the <see cref="Entity"/>s <see cref="Velocity"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position pos, ref Velocity vel)
        {
            pos.Vec2 += _deltaTime * vel.Vec2;
        }
    }

    /// <summary>
    ///     The <see cref="Bounce"/> struct
    ///     acts as an implementation of the bounce logic to make <see cref="Entity"/>s stay in the viewport.
    ///     Can be inlined, the great performance advantage of this approach.
    /// </summary>
    private struct Bounce : IForEach<Position, Velocity>
    {
        private Rectangle _viewport;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Bounce"/> struct.
        /// </summary>
        /// <param name="viewport">The games viewport as a <see cref="Rectangle"/>.</param>
        public Bounce(Rectangle viewport)
        {
            _viewport = viewport;
        }

        /// <summary>
        ///     Makes one <see cref="Entity"/> bounce of the gamescreen.
        ///     Gets called automatically for all entities fitting a <see cref="QueryDescription"/>.
        /// </summary>
        /// <param name="pos">A reference to the <see cref="Entity"/>s <see cref="Position"/>.</param>
        /// <param name="vel">A reference to the <see cref="Entity"/>s <see cref="Velocity"/>.</param>
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
    ///     Gets called to execute the movement systems logic and make the <see cref="Entity"/>s move and bounce.
    /// </summary>
    /// <param name="time">The <see cref="GameTime"/> being passed from outside the system.</param>
    public override void Update(in GameTime time)
    {
        // Iterates over all entities ( based on the passed QueryDescription ), accesses their Position and Velocity Components and updates them.
        // Highperformance and inlined calls for maximum effiency.
        var movementJob = new Move((float)time.ElapsedGameTime.TotalMilliseconds);
        World.InlineParallelQuery<Move, Position, Velocity>(in _entitiesToMove, ref movementJob);

        // Iterates over the same entities, accesses the same components. But executes the "Bounce" struct.
        // Checks whether the entity hit the viewport bounds and inverts its velocity to make it bounce.
        var bounceJob = new Bounce(_viewport);
        World.InlineParallelQuery<Bounce, Position, Velocity>(in _entitiesToMove, ref bounceJob);
    }
}


/// <summary>
///     The <see cref="ColorSystem"/> class
///     ensures that all <see cref="Entity"/>s update their color.
/// </summary>
public sealed class ColorSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToChangeColor = new QueryDescription().WithAll<Sprite>();
    private static GameTime? _gameTime;
    private static Random _random;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ColorSystem"/> class.
    /// </summary>
    /// <param name="world">Its <see cref="World"/>.</param>
    public ColorSystem(World world)
        : base(world)
    {
        _random = new Random();
    }

    /// <summary>
    ///     Gets called to execute the color systems logic and to change the <see cref="Entity"/>s color.
    /// </summary>
    /// <param name="time">The <see cref="GameTime"/> being passed from outside the system.</param>
    public override void Update(in GameTime time)
    {
        _gameTime = time;

        // Modifies the color of all entities fitting the entitiesToChangeColor query.
        World.Query(in _entitiesToChangeColor, (ref Sprite sprite) =>
        {
            sprite.Color.R += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
            sprite.Color.G += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
            sprite.Color.B += (byte)(_gameTime.ElapsedGameTime.TotalMilliseconds * 0.08);
        });

        // A demonstration of bulk adding and removing components.
        World.Add(in _entitiesToChangeColor, _random.Next());
        World.Remove<int>(in _entitiesToChangeColor);
    }
}

/// <summary>
///     The <see cref="DrawSystem"/> class
///     ensures that all <see cref="Entity"/>s are drawn to the screen.
/// </summary>
public sealed class DrawSystem : SystemBase<GameTime>
{
    private readonly QueryDescription _entitiesToDraw = new QueryDescription().WithAll<Position, Sprite>();
    private readonly SpriteBatch _batch;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DrawSystem"/> class.
    /// </summary>
    /// <param name="world">Its <see cref="World"/>.</param>
    /// <param name="batch">The <see cref="SpriteBatch"/> used to draw all <see cref="Entity"/>s.</param>
    public DrawSystem(World world, SpriteBatch batch)
        : base(world)
    {
        _batch = batch;
    }

    /// <summary>
    ///     Gets called to execute the draw systems logic and to draw the <see cref="Entity"/>s.
    /// </summary>
    /// <param name="time">The <see cref="GameTime"/> being passed from outside the system.</param>
    public override void Update(in GameTime time)
    {
        _batch.Begin();

        // Get query for the description, targets all entities with "Positions" and "Sprite".
        var query = World.Query(in _entitiesToDraw);
        foreach (ref var chunk in query)   // Iterate over each chunk that has entities that fit the query.
        {
            // Receive raw arrays of positions and sprites from the chunk.
            chunk.GetSpan<Position, Sprite>(out var positions, out var sprites);

            // Loop over the chunk
            foreach(var index in chunk)
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
