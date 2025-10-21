using SFML.Graphics;

namespace Invaders;

public class QuitGUI : GUI
{
    public QuitGUI() : base("PlayerShip")
    {
        SceneState = SceneState.QUIT;
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
        
        Program.Window.Close();
    }
    
    public override void Render(RenderTarget target)
    {

    }
}