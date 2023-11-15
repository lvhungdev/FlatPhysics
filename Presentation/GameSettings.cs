namespace Presentation;

public class GameSettings
{
    private GameSettings()
    {
    }

    public static GameSettings Instance { get; } = new();

    public int WindowWidth { get; set; } = 800;
    public int WindowHeight { get; set; } = 600;
}
