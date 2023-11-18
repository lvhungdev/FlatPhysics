using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics;
using Physics.Core;
using Presentation.Objects;

namespace Presentation;

public class MyGame : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch = null!;

    private readonly List<Box> boxes = new();
    private readonly PhysicWorld physicWorld = new();

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

        TextureManager.Initialize(Content);

        physicWorld.Gravity = -9.8f;
        physicWorld.Iterations = 14;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        for (int i = 0; i < 10; i++)
        {
            Box box = new(new FlatVector(1.5f, 1 + i * 1));

            boxes.Add(box);
            physicWorld.AddBody(box.Body);
        }

        FlatBody bottom = new()
        {
            Position = new FlatVector(0, -0.5f),
            InversedMass = 0,
            Shape = new FlatShape(new FlatVector(200, 1)),
            IsStatic = true,
        };

        FlatBody left = new()
        {
            Position = new FlatVector(-0.5f, 0),
            InversedMass = 0,
            Shape = new FlatShape(new FlatVector(1, 200)),
            IsStatic = true,
        };

        FlatBody right = new()
        {
            Position = new FlatVector((float)GameSettings.Instance.WindowWidth / Converter.PixelsPerUnit + 0.5f, 0),
            InversedMass = 0,
            Shape = new FlatShape(new FlatVector(1, 200)),
            IsStatic = true,
        };

        physicWorld.AddBody(bottom);
        physicWorld.AddBody(left);
        physicWorld.AddBody(right);
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
            boxes[0].Body.AddForce(new FlatVector(-100f, 0) * delta);
        }

        if (keyboardState.IsKeyDown(Keys.D))
        {
            boxes[0].Body.AddForce(new FlatVector(100f, 0) * delta);
        }

        if (keyboardState.IsKeyDown(Keys.W))
        {
            boxes[0].Body.AddForce(new FlatVector(0, 100f) * delta);
        }

        if (keyboardState.IsKeyDown(Keys.S))
        {
            boxes[0].Body.AddForce(new FlatVector(0, -100f) * delta);
        }

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            boxes[0].Body.AngularVelocity += 4 * MathF.PI / 2 * delta;
        }

        if (keyboardState.IsKeyDown(Keys.E))
        {
            boxes[0].Body.AngularVelocity -= 4 * MathF.PI / 2 * delta;
        }

        physicWorld.Integrate(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();

        foreach (Box box in boxes)
        {
            box.Draw(spriteBatch);
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
