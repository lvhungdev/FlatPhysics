using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics.Collision;
using Physics.Core;

namespace Presentation;

public class MyGame : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch = null!;

    private Renderer renderer = null!;
    private readonly List<FlatBody> bodies = new();

    public MyGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = GameSettings.Instance.WindowWidth;
        graphics.PreferredBackBufferHeight = GameSettings.Instance.WindowHeight;
        graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        renderer = new Renderer(spriteBatch, Content);

        for (int i = 0; i < 4; i++)
        {
            FlatParticle particle = new()
            {
                Position = new FlatVector(1.5f + i * 1, 4),
                Velocity = new FlatVector(0, 0),
                Acceleration = new FlatVector(0, 0),
                InversedMass = 1.0f,
            };

            bodies.Add(new FlatBody { Particle = particle, Shape = new FlatShape(0.2f) });
        }
    }

    protected override void Update(GameTime gameTime)
    {
        float delta = gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (keyboardState.IsKeyDown(Keys.A))
        {
            bodies[0].Particle.Position += new FlatVector(-1f, 0) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.D))
        {
            bodies[0].Particle.Position += new FlatVector(1f, 0) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.W))
        {
            bodies[0].Particle.Position += new FlatVector(0, 1f) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.S))
        {
            bodies[0].Particle.Position += new FlatVector(0, -1f) * delta;
        }

        foreach (FlatBody body in bodies)
        {
            body.Update(delta);
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            for (int j = i + 1; j < bodies.Count; j++)
            {
                FlatBody bodyA = bodies[i];
                FlatBody bodyB = bodies[j];

                bool isCollided = CollisionDetector.Detect(bodyA, bodyB, out FlatVector normal, out float depth);
                if (!isCollided) continue;

                bodyA.Particle.Position -= normal * depth / 2;
                bodyB.Particle.Position += normal * depth / 2;
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();

        foreach (FlatBody body in bodies)
        {
            renderer.DrawBall(body.Particle.Position, body.Shape.Radius);
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
