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
    private readonly List<Ball> balls = new();
    private readonly PhysicWorld physicWorld = new();

    private MouseState lastMouseState;

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
        physicWorld.Iterations = 1;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        Box obstacle = new(new FlatVector(5, 2))
        {
            Body =
            {
                Shape = new FlatShape(new FlatVector(0.5f, 3)),
                IsStatic = true,
                InversedMass = 0,
                Rotation = -MathF.PI / 6,
            },
        };
        boxes.Add(obstacle);

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
        physicWorld.AddBody(obstacle.Body);
    }

    protected override void Update(GameTime gameTime)
    {
        float delta = gameTime.ElapsedGameTime.Ticks / (float)TimeSpan.TicksPerSecond;

        if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

        MouseState currentState = Mouse.GetState();
        if (currentState.LeftButton == ButtonState.Pressed &&
            lastMouseState.LeftButton == ButtonState.Released)
        {
            Ball ball = new(currentState.Position.ToFlatVector());
            balls.Add(ball);
            physicWorld.AddBody(ball.Body);
        }

        if (currentState.RightButton == ButtonState.Pressed &&
            lastMouseState.RightButton == ButtonState.Released)
        {
            Box box = new(currentState.Position.ToFlatVector());
            boxes.Add(box);
            physicWorld.AddBody(box.Body);
        }

        lastMouseState = currentState;

        physicWorld.Integrate(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();

        foreach (Ball ball in balls)
        {
            ball.Draw(spriteBatch);
        }

        foreach (Box box in boxes)
        {
            box.Draw(spriteBatch);
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
