using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Physics.Core;

namespace Presentation;

public class Renderer
{
    public Renderer(SpriteBatch spriteBatch, ContentManager contentManager)
    {
        this.spriteBatch = spriteBatch;
        this.contentManager = contentManager;

        LoadContent();
    }

    private readonly SpriteBatch spriteBatch;
    private readonly ContentManager contentManager;

    private Texture2D ballTexture = null!;

    public void DrawBall(FlatVector position, float radius)
    {
        Point size = new((int)(radius * 2 * VectorConverter.PixelsPerUnit), (int)(radius * 2 * VectorConverter.PixelsPerUnit));

        Point location = position.ToPoint();
        location.X -= size.X / 2;
        location.Y -= size.Y / 2;

        Rectangle dest = new(location, size);

        spriteBatch.Draw(ballTexture, dest, Color.White);
    }

    private void LoadContent()
    {
        ballTexture = contentManager.Load<Texture2D>("ball");
    }
}
