using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class MainMenuGUI : GUI
{
    public MainMenuGUI() : base("PlayerShip")
    {
        SceneState = SceneState.MAIN_MENU;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        Buttons.Add(new Button(this, SceneState.IN_GAME, "Play", 
            new Vector2f(275, 200)));
        scene.QueueSpawn(Buttons[^1]);
        
        Buttons.Add(new Button(this, SceneState.HIGHSCORE_MENU, "Highscores", 
            new Vector2f(275, 300)));
        scene.QueueSpawn(Buttons[^1]);
        
        Buttons.Add(new Button(this, SceneState.QUIT, "Quit", 
            new Vector2f(275, 400)));
        scene.QueueSpawn(Buttons[^1]);
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
    }
}