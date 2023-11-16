using Physics.Core;

namespace Physics.Collision;

public static class CollisionResoluter
{
    public static void Resolve(FlatBody bodyA, FlatBody bodyB, FlatVector normal, float depth)
    {
        bodyA.Position -= normal * (depth / 2) * bodyA.InversedMass;
        bodyB.Position += normal * (depth / 2) * bodyB.InversedMass;
    }
}
