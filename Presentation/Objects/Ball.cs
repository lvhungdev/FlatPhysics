using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics.Core;

namespace Presentation.Objects;

public class Ball
{
    public FlatBody Body { get; }
    public bool IsColliding { get; set; } = false;

    public Ball(FlatVector position)
    {
        Body = new FlatBody
        {
            Position = position,
            LinearVelocity = new FlatVector(0, 0),
            LinearAcceleration = new FlatVector(0, 0),
            InversedMass = 1.0f,
            Shape = new FlatShape(0.2f),
        };
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Texture2D texture = TextureManager.Instance.BallTexture;

        Point size = new((int)(Body.Shape.Radius * 2 * Converter.PixelsPerUnit),
            (int)(Body.Shape.Radius * 2 * Converter.PixelsPerUnit));

        Point location = Body.Position.ToPoint();

        Rectangle dest = new(location, size);
        Vector2 origin = new(texture.Width / 2.0f, texture.Height / 2.0f);

        spriteBatch.Draw(texture, dest, null, IsColliding ? Color.Red : Color.White, -Body.Rotation, origin, SpriteEffects.None,
            default);
    }
}
