namespace Invaders;

public class GameOverGUI : GUI
{
    public GameOverGUI() : base("PlayerShip")
    {
        SceneState = SceneState.GAME_OVER;
    }
}