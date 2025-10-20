using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class GUI : Entity
{
    protected List<Text> Texts;
    protected bool IsActive = false;
    protected SceneState SceneState;
    
    public GUI(string textureName) : base(textureName)
    {
        Texts = new List<Text>();
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
    }

    public virtual void JustLoaded(Scene scene)
    {
        
    }
}