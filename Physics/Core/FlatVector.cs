namespace Physics.Core;

public readonly struct FlatVector
{
    private const float FloatTolerance = 0.00001f;

    public FlatVector(float x, float y)
    {
        X = x;
        Y = y;
    }

    public readonly float X;
    public readonly float Y;

    public float LengthSquared => X * X + Y * Y;
    public float Length => MathF.Sqrt(LengthSquared);

    public static FlatVector Zero => new(0, 0);

    public static FlatVector operator +(FlatVector a, FlatVector b) => new(a.X + b.X, a.Y + b.Y);
    public static FlatVector operator -(FlatVector a, FlatVector b) => new(a.X - b.X, a.Y - b.Y);
    public static FlatVector operator *(FlatVector a, float b) => new(a.X * b, a.Y * b);
    public static FlatVector operator /(FlatVector a, float b) => new(a.X / b, a.Y / b);
    public static FlatVector operator -(FlatVector a) => new(-a.X, -a.Y);

    public float GetDistanceFrom(FlatVector other) => (this - other).Length;
    public float GetDistanceSquaredFrom(FlatVector other) => (this - other).LengthSquared;

    public FlatVector GetNormalized() => this / Length;

    public float DotProduct(FlatVector other) => X * other.X + Y * other.Y;

    public float CrossProduct(FlatVector other) => X * other.Y - Y * other.X;

    public static bool operator ==(FlatVector a, FlatVector b) =>
        Math.Abs(a.X - b.X) < FloatTolerance && Math.Abs(a.Y - b.Y) < FloatTolerance;

    public static bool operator !=(FlatVector a, FlatVector b) =>
        Math.Abs(a.X - b.X) > FloatTolerance || Math.Abs(a.Y - b.Y) > FloatTolerance;

    public override bool Equals(object? obj) => obj is FlatVector other && this == other;

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
