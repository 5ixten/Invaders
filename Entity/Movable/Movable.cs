using SFML.System;

namespace Invaders;

public abstract class Movable : Entity
{
    protected float Speed;
    protected Vector2f Direction;
    
    public Movable(string textureName) : base(textureName)
    {
        
    }
    
    public override void Create(Scene scene)
    {
        base.Create(scene);
        Sprite.Origin = new Vector2f(Bounds.Width / 2f, Bounds.Height / 2f);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        Move(deltaTime);

        if (Position.X - Bounds.Width/2 < 0 || Position.X + Bounds.Width / 2 > Program.WindowSize.X
                                            || Position.Y - Bounds.Height / 2 < 0 || Position.Y + Bounds.Height / 2 > Program.WindowSize.Y)
        {
            OnTouchingWall(scene);
        }
    }

    protected abstract void OnTouchingWall(Scene scene);

    private void Move(float deltaTime)
    {
        Sprite.Position += Direction * Speed * deltaTime;;
    }
}