namespace Physics.Core;

public readonly struct FlatShape
{
    public readonly ShapeType Type;
    public readonly float Radius = 0.0f;
    public readonly FlatVector Size = FlatVector.Zero;
    public readonly FlatVector[] Vertices = Array.Empty<FlatVector>();

    public FlatShape(float radius)
    {
        Type = ShapeType.Circle;
        Radius = radius;
    }

    public FlatShape(FlatVector size)
    {
        Type = ShapeType.Rectangle;
        Size = size;

        Vertices = new[]
        {
            new FlatVector(-size.X / 2, size.Y / 2),
            new FlatVector(size.X / 2, size.Y / 2),
            new FlatVector(size.X / 2, -size.Y / 2),
            new FlatVector(-size.X / 2, -size.Y / 2),
        };
    }

    public FlatShape(FlatVector[] vertices)
    {
        Type = ShapeType.Polygon;
        Vertices = vertices;
    }
}

public enum ShapeType
{
    Circle,
    Rectangle,
    Polygon,
}
