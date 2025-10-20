using SFML.System;

namespace Invaders;

public class Bullet : Movable
{
    public int Damage = 1;
    public readonly Type Shooter;
    
    public Bullet(Type shooter, Vector2f position, float speed, Vector2f direction) : base("Bullet")
    {
        Position = position;
        Direction = direction;
        Speed = speed;
        Shooter = shooter;
        
        Sprite.Rotation = MathF.Atan2(direction.Y, direction.X) * 180f / MathF.PI - 90;
    }

    protected override void OnTouchingWall(Scene scene)
    {
        IsDead = true;
    }
    
}