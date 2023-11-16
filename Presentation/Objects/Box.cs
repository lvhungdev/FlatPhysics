using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics.Core;

namespace Presentation.Objects;

public class Box
{
    public FlatBody Body { get; }
    public bool IsColliding { get; set; }

    public Box(FlatVector position)
    {
        Body = new FlatBody
        {
            Position = position,
            Velocity = new FlatVector(0, 0),
            Acceleration = new FlatVector(0, 0),
            InversedMass = 1.0f,
            Shape = new FlatShape(new FlatVector(0.4f, 0.4f)),
        };
    }

    public void Update(float delta)
    {
        Body.Update(delta);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Texture2D texture = TextureManager.Instance.BoxTexture;

        Point size = new((int)(Body.Shape.Size.X * Converter.PixelsPerUnit),
            (int)(Body.Shape.Size.Y * Converter.PixelsPerUnit));

        Point location = Body.Position.ToPoint();

        Rectangle dest = new(location, size);
        Vector2 origin = new(texture.Width / 2.0f, texture.Height / 2.0f);

        spriteBatch.Draw(texture, dest, null, IsColliding ? Color.Red : Color.White, -Body.Rotation, origin, SpriteEffects.None,
            default);
    }
}
