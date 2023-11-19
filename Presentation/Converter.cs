using Microsoft.Xna.Framework;
using Physics.Core;

namespace Presentation;

public static class Converter
{
    public const int PixelsPerUnit = 100;

    public static Vector2 ToVector2(this FlatVector vector) =>
        new(vector.X * PixelsPerUnit, GameSettings.Instance.WindowHeight - vector.Y * PixelsPerUnit);

    public static FlatVector ToFlatVector(this Vector2 vector) =>
        new(vector.X / PixelsPerUnit, (GameSettings.Instance.WindowHeight - vector.Y) / PixelsPerUnit);

    public static Point ToPoint(this FlatVector vector) =>
        new((int)(vector.X * PixelsPerUnit), (int)(GameSettings.Instance.WindowHeight - vector.Y * PixelsPerUnit));

    public static FlatVector ToFlatVector(this Point point) =>
        new(point.X / (float)PixelsPerUnit, (GameSettings.Instance.WindowHeight - point.Y) / (float)PixelsPerUnit);
}
