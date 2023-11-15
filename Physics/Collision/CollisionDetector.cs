using Physics.Core;

namespace Physics.Collision;

public static class CollisionDetector
{
    public static bool Detect(FlatBody bodyA, FlatBody bodyB, out FlatVector normal, out float depth)
    {
        return DetectCircleCircle(bodyA.Particle.Position, bodyA.Shape.Radius, bodyB.Particle.Position, bodyB.Shape.Radius,
            out normal, out depth);
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
}
