namespace Physics.Core;

public readonly struct FlatShape
{
    public readonly ShapeType Type;
    public readonly float Radius = 0.0f;
    public readonly FlatVector Size = FlatVector.Zero;

    public FlatShape(float radius)
    {
        Type = ShapeType.Circle;
        Radius = radius;
    }

    public FlatShape(FlatVector size)
    {
        Type = ShapeType.Square;
        Size = size;
    }
}

public enum ShapeType
{
    Circle,
    Square,
}
