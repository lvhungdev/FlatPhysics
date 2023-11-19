using Physics.Core;

namespace Physics.Collision;

public static class CollisionResolver
{
    public static void Resolve(FlatBody bodyA, FlatBody bodyB, FlatVector normal, float depth)
    {
        if (bodyA.IsStatic && bodyB.IsStatic || bodyA.InversedMass + bodyB.InversedMass == 0) return;

        ResolvePenetration(bodyA, bodyB, normal, depth);
        FlatVector _ = ResolveLinearVelocity(bodyA, bodyB, normal);
        // ResolveAngularVelocity(bodyA, bodyB, linearImpulse);
    }

    private static void ResolvePenetration(FlatBody bodyA, FlatBody bodyB, FlatVector normal, float depth)
    {
        bodyA.Position += -normal * (depth / 2) * bodyA.InversedMass;
        bodyB.Position += normal * (depth / 2) * bodyB.InversedMass;
    }

    private static FlatVector ResolveLinearVelocity(FlatBody bodyA, FlatBody bodyB, FlatVector normal)
    {
        const float constitution = 0.8f;

        FlatVector relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;
        float impulseAlongNormal = -(1 + constitution) * relativeVelocity.DotProduct(normal) /
                                   (bodyA.InversedMass + bodyB.InversedMass);
        FlatVector impulse = normal * impulseAlongNormal;

        bodyA.LinearVelocity -= impulse * bodyA.InversedMass;
        bodyB.LinearVelocity += impulse * bodyB.InversedMass;

        return impulse;
    }

    // TODO Wrong
    private static void ResolveAngularVelocity(FlatBody bodyA, FlatBody bodyB, FlatVector velocityImpulse)
    {
        FlatVector normal = bodyB.Position - bodyA.Position;
        float angularVelocity = velocityImpulse.DotProduct(normal.GetPerpendicular());

        bodyA.AngularVelocity -= angularVelocity;
        bodyB.AngularVelocity += angularVelocity;
    }
}
