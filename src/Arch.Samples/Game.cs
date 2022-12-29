using System.Numerics;
using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using JobScheduler = JobScheduler.JobScheduler;
using Vector2 = System.Numerics.Vector2;

namespace Arch.Samples;

public class Game : Microsoft.Xna.Framework.Game
{
    
    // The world and a job scheduler for multithreading
    private World _world;
    private global::JobScheduler.JobScheduler _jobScheduler;
    
    // Our systems processing entities
    private MovementSystem _movementSystem;
    private ColorSystem _colorSystem;
    private DrawSystem _drawSystem;
    
    // Monogame stuff
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture2D;

    private Random _random;
    public Game()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }
    
    protected override void Initialize()
    {
        // Setup texture and randomness
        _random = new Random();
        _texture2D = new Texture2D(GraphicsDevice, 10, 10);
        var data = new Color[10*10];
        for(var i=0; i < data.Length; ++i) data[i] = Color.White;
        _texture2D.SetData(data);

        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void BeginRun()
    {
        base.BeginRun();
        
        // Create world & systems
        _world = World.Create();
        _jobScheduler = new("SampleWorkerThreads");
        _movementSystem = new MovementSystem(_world, GraphicsDevice.Viewport.Bounds);
        _colorSystem = new ColorSystem(_world);
        _drawSystem = new DrawSystem(_world, _spriteBatch);

        // Spawn in entities with position, velocity and sprite
        for (var index = 0; index < 1000; index++)
        {
            _world.Create(
                new Position{ Vec2 = _random.NextVector2(GraphicsDevice.Viewport.Bounds) }, 
                new Velocity{ Vec2 = _random.NextVector2(-0.25f,0.25f) }, 
                new Sprite{ Texture2D = _texture2D, Color = _random.NextColor() }
            );
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
        if (Keyboard.GetState().IsKeyDown(Keys.Delete))
        {
            // Query for velocity entities and remove their velocity to make them stop moving. 
            var queryDesc = new QueryDescription().WithAll<Velocity>();
            _world.Query(in queryDesc, (in Entity entity) => entity.Remove<Velocity>());
        }
        
        _movementSystem.Update(in gameTime);
        _colorSystem.Update(in gameTime);
        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
        _drawSystem.Update(in gameTime);
        Console.WriteLine($"FPS : {(1 / gameTime.ElapsedGameTime.TotalSeconds)}");
        base.Draw(gameTime);
    }

    protected override void EndRun()
    {
        base.EndRun();
        
        // Destroy world and shutdown the jobscheduler 
        World.Destroy(_world);
        _jobScheduler.Dispose();
    }
}