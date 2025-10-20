namespace Invaders;

public class MainMenuGUI : GUI
{
    public MainMenuGUI() : base("PlayerShip")
    {
        SceneState = SceneState.MAIN_MENU;
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
        
        Console.WriteLine("ACTIVE 2");
    }
}