using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public abstract class GUI : Entity
{
    protected List<Text> Texts;
    protected List<Button> Buttons;
    public bool IsActive { get; private set; } = false;
    protected SceneState SceneState;
    
    public GUI(string textureName) : base(textureName)
    {
        Texts = new List<Text>();
        Buttons = new List<Button>();
        DontDestroyOnLoad = true;
    }
    
    public override void Create(Scene scene)
    {
        base.Create(scene);
        ZIndex = 10;
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        IsActive = scene.SceneState == SceneState;
        if (scene.JustLoadedState && scene.SceneState == SceneState)
        {
            JustLoaded(scene);
        }

        Vector2f MousePosition = (Vector2f)Mouse.GetPosition(Program.Window);
        foreach (var button in Buttons)
        {
            button.IsHovered = button.Bounds.Contains(MousePosition.X, MousePosition.Y);
        }
    }

    protected virtual void JustLoaded(Scene scene)
    {
        
    }
}