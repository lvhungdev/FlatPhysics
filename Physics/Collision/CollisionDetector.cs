using Physics.Core;

namespace Physics.Collision;

public static class CollisionDetector
{
    public static bool Detect(FlatBody bodyA, FlatBody bodyB, out FlatVector normal, out float depth)
    {
        switch (bodyA.Shape, bodyB.Shape)
        {
            case ({ Type: ShapeType.Circle } circleA, { Type: ShapeType.Circle } circleB):
            {
                return DetectCircleCircle(bodyA.Position, circleA.Radius, bodyB.Position, circleB.Radius,
                    out normal, out depth);
            }

            case ({ Type: ShapeType.Rectangle }, { Type: ShapeType.Rectangle }):
            {
                return DetectPolygonPolygon(bodyA.Vertices, bodyB.Vertices, out normal, out depth);
            }

            default:
            {
                normal = FlatVector.Zero;
                depth = 0.0f;
                return false;
            }
        }
    }

    private static bool DetectCircleCircle(FlatVector positionA, float radiusA, FlatVector positionB, float radiusB,
        out FlatVector normal, out float depth)
    {
        float distance = (positionA - positionB).Length;
        float radiusSum = radiusA + radiusB;

        if (distance >= radiusSum)
        {
            normal = FlatVector.Zero;
            depth = 0.0f;
            return false;
        }
        else
        {
            normal = (positionB - positionA).GetNormalized();
            depth = radiusSum - distance;
            return true;
        }
    }

    private static bool DetectPolygonPolygon(FlatVector[] verticesA, FlatVector[] verticesB,
        out FlatVector normal, out float depth)
    {
        normal = FlatVector.Zero;
        depth = float.MaxValue;

        for (int i = 0; i < verticesA.Length; i++)
        {
            FlatVector axis = (verticesA[i] - verticesA[(i + 1) % verticesA.Length])
                .GetNormalized()
                .GetPerpendicular();

            (float minA, float maxA) = GetMinMaxFromProjection(verticesA, axis);
            (float minB, float maxB) = GetMinMaxFromProjection(verticesB, axis);

            if (minA >= maxB || minB >= maxA) return false;

            float axisDepth = MathF.Min(maxA - minB, maxB - minA);
            if (axisDepth < depth)
            {
                normal = axis;
                depth = axisDepth;
            }
        }

        for (int i = 0; i < verticesB.Length; i++)
        {
            FlatVector axis = (verticesB[i] - verticesB[(i + 1) % verticesB.Length])
                .GetNormalized()
                .GetPerpendicular();

            (float minA, float maxA) = GetMinMaxFromProjection(verticesA, axis);
            (float minB, float maxB) = GetMinMaxFromProjection(verticesB, axis);

            if (minA >= maxB || minB >= maxA) return false;

            float axisDepth = MathF.Min(maxA - minB, maxB - minA);
            if (axisDepth < depth)
            {
                normal = axis;
                depth = axisDepth;
            }
        }

        if (normal.DotProduct(GetCenterPoint(verticesB) - GetCenterPoint(verticesA)) < 0)
        {
            normal = -normal;
        }

        return true;
    }

    private static (float, float) GetMinMaxFromProjection(FlatVector[] vertices, FlatVector axis)
    {
        float min = float.MaxValue;
        float max = float.MinValue;

        foreach (FlatVector vertex in vertices)
        {
            float dot = vertex.DotProduct(axis);
            min = MathF.Min(min, dot);
            max = MathF.Max(max, dot);
        }

        return (min, max);
    }

    private static FlatVector GetCenterPoint(FlatVector[] vertices)
    {
        float x = 0.0f;
        float y = 0.0f;

        foreach (FlatVector vertex in vertices)
        {
            x += vertex.X;
            y += vertex.Y;
        }

        return new FlatVector(x / vertices.Length, y / vertices.Length);
    }
}
