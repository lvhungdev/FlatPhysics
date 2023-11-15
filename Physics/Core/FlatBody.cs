namespace Physics.Core;

public class FlatBody
{
    public FlatParticle Particle { get; set; } = null!;
    public FlatShape Shape { get; set; }

    public void Update(float deltaTime)
    {
        Particle.Update(deltaTime);
    }
}
