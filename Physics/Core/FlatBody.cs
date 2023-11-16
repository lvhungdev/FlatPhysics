namespace Physics.Core;

public class FlatBody
{
    public FlatVector Position { get; set; } = FlatVector.Zero;
    public FlatVector Velocity { get; set; } = FlatVector.Zero;
    public FlatVector Acceleration { get; set; } = FlatVector.Zero;
    public float InversedMass { get; set; } = 1.0f;

    public float Rotation { get; set; }
    public float Inertia { get; set; }

    public FlatShape Shape { get; set; } = new(0.0f);

    public FlatVector[] Vertices => Shape.Vertices.Select(m => m.GetRotated(Rotation) + Position).ToArray();

    private FlatVector forceAccumulator = FlatVector.Zero;

    public void Update(float deltaTime)
    {
        Velocity += (Acceleration + forceAccumulator * InversedMass) * deltaTime;
        Position += Velocity * deltaTime;

        forceAccumulator = FlatVector.Zero;
    }

    public void AddForce(FlatVector force) => forceAccumulator += force;
}
