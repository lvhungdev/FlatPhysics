using Physics.Collision;
using Physics.Core;

namespace Physics;

public class PhysicWorld
{
    public float Gravity { get; set; }
    public int Iterations { get; set; } = 2;

    private readonly List<FlatBody> bodies = new();

    public void AddBody(FlatBody body)
    {
        if (!body.IsStatic)
        {
            body.LinearAcceleration += new FlatVector(0, Gravity);
        }

        bodies.Add(body);
    }

    public void Integrate(float delta)
    {
        float deltaTime = delta / Iterations;

        for (int iteration = 0; iteration < Iterations; iteration++)
        {
            foreach (FlatBody body in bodies)
            {
                body.Integrate(deltaTime);
            }

            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = i + 1; j < bodies.Count; j++)
                {
                    FlatBody bodyA = bodies[i];
                    FlatBody bodyB = bodies[j];

                    bool isCollided = CollisionDetector.Detect(bodyA, bodyB, out FlatVector normal, out float depth);
                    if (!isCollided) continue;

                    CollisionResolver.Resolve(bodyA, bodyB, normal, depth);
                }
            }
        }
    }
}
