using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics.Collision;
using Physics.Core;
using Presentation.Objects;

namespace Presentation;

public class MyGame : Game
{
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch = null!;

    private readonly List<Box> boxes = new();
    // private readonly List<Ball> balls = new();

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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        for (int i = 0; i < 5; i++)
        {
            boxes.Add(new Box(new FlatVector(1.5f + i * 1, 4)));
            // balls.Add(new Ball(new FlatVector(1.5f + i * 1, 4)));
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
            boxes[0].Body.Position += new FlatVector(-1f, 0) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.D))
        {
            boxes[0].Body.Position += new FlatVector(1f, 0) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.W))
        {
            boxes[0].Body.Position += new FlatVector(0, 1f) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.S))
        {
            boxes[0].Body.Position += new FlatVector(0, -1f) * delta;
        }

        if (keyboardState.IsKeyDown(Keys.Q))
        {
            boxes[0].Body.Rotation += MathF.PI / 2 * delta;
        }

        if (keyboardState.IsKeyDown(Keys.E))
        {
            boxes[0].Body.Rotation -= MathF.PI / 2 * delta;
        }

        foreach (Box box in boxes)
        {
            box.Update(delta);
            box.IsColliding = false;
        }


        for (int i = 0; i < boxes.Count; i++)
        {
            for (int j = i + 1; j < boxes.Count; j++)
            {
                Box boxA = boxes[i];
                Box boxB = boxes[j];

                bool isCollided = CollisionDetector.Detect(boxA.Body, boxB.Body, out FlatVector normal, out float depth);
                if (!isCollided) continue;

                boxA.IsColliding = true;
                boxB.IsColliding = true;
                CollisionResoluter.Resolve(boxA.Body, boxB.Body, normal, depth);
            }
        }

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
