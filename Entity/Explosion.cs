using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Explosion : Entity
{
    public Explosion(Vector2f position) : base("Explosion")
    {
        ZIndex = 1;
        
        Sprite.Position = position;
        Sprite.Scale = new Vector2f(3.4f, 3.4f);
        
        Animations = new Animation[1]
        {
            new Animation(
                new IntRect[]
                {
                    new IntRect(0, 0, 64, 64),
                    new IntRect(64, 0, 64, 64),
                    new IntRect(128, 0, 64, 64),
                    new IntRect(192, 0, 64, 64),
                    new IntRect(256, 0, 64, 64),
                    new IntRect(320, 0, 64, 64),
                    new IntRect(384, 0, 64, 64),
                    new IntRect(448, 0, 64, 64),
                    new IntRect(512, 0, 64, 64),
                    new IntRect(576, 0, 64, 64),
                    new IntRect(640, 0, 64, 64),

                }
            , 45f)
        };
    }
    
    public override void Create(Scene scene)
    {
        base.Create(scene);
        Sprite.Origin = new Vector2f(32, 32);
        scene.EventManager.PublishPlaySound(SoundType.Explosion);
    }

    public override void Update(Scene scene, float deltaTime) 
    {
        if (Animations[0].LoopedCount != 0) 
        {
            IsDead = true;
        }
    }

    public override void Render(RenderTarget target)
    {
        Sprite.TextureRect = Animations[0].GetCurrentTextureRect();
        base.Render(target);
    }
}