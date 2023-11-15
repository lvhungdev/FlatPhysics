namespace Physics.Core;

public class FlatParticle
{
    public FlatVector Position { get; set; } = FlatVector.Zero;
    public FlatVector Velocity { get; set; } = FlatVector.Zero;
    public FlatVector Acceleration { get; set; } = FlatVector.Zero;
    public float InversedMass { get; set; } = 1.0f;

    private FlatVector forceAccumulator = FlatVector.Zero;

    public void Update(float deltaTime)
    {
        Velocity += (Acceleration + forceAccumulator * InversedMass) * deltaTime;
        Position += Velocity * deltaTime;

        forceAccumulator = FlatVector.Zero;
    }

    public void AddForce(FlatVector force) => forceAccumulator += force;
}
