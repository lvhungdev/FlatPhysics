namespace Physics.Core;

public class FlatBody
{
    public FlatVector Position { get; set; } = FlatVector.Zero;
    public FlatVector LinearVelocity { get; set; } = FlatVector.Zero;
    public FlatVector LinearAcceleration { get; set; } = FlatVector.Zero;
    public float InversedMass { get; set; } = 1.0f;

    public float Rotation { get; set; }
    public float AngularVelocity { get; set; }
    public float Inertia { get; set; }

    private FlatVector forceAccumulator = FlatVector.Zero;

    public FlatShape Shape { get; set; } = new(0.0f);
    public FlatVector[] Vertices => Shape.Vertices.Select(m => m.GetRotated(Rotation) + Position).ToArray();

    public bool IsStatic { get; set; }

    public void Integrate(float deltaTime)
    {
        LinearVelocity += (LinearAcceleration + forceAccumulator * InversedMass) * deltaTime;
        Position += LinearVelocity * deltaTime;

        Rotation += AngularVelocity * deltaTime;

        forceAccumulator = FlatVector.Zero;
    }

    public void AddForce(FlatVector force) => forceAccumulator += force;
}
