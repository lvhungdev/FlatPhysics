using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Presentation;

public class TextureManager
{
    public static TextureManager Instance { get; private set; } = null!;

    public Texture2D BallTexture { get; private set; } = null!;
    public Texture2D BoxTexture { get; private set; } = null!;

    private TextureManager()
    {
    }

    public static void Initialize(ContentManager contentManager)
    {
        Instance = new TextureManager
        {
            BallTexture = contentManager.Load<Texture2D>("ball"),
            BoxTexture = contentManager.Load<Texture2D>("box"),
        };
    }
}
