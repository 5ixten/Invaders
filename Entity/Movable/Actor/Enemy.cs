using SFML.System;

namespace Invaders;

public class Enemy : Actor
{
    private float nextShot;
    
    public Enemy() : base("EnemyShip")
    {
        Random random = new Random();

        int xDir = random.Next(0, 2) == 0 ? -1 : 1;
        
        Direction = new Vector2f(xDir, 1);
        Sprite.Rotation = MathF.Atan2(Direction.Y, Direction.X) * 180f / MathF.PI - 90;
        
        Speed = 100f;
        ReloadTime = 1000;

        Position = GetRandomPosition();
    }

    private Vector2f GetRandomPosition()
    {
        return new Vector2f(new Random().Next((int)Bounds.Width, (int)(Program.WindowSize.X - Bounds.Width)), -Bounds.Height);
    }

    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        Shoot(scene);
    }
    
    public override void Shoot (Scene scene){
        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        if (currentTime < nextShot) return;
        nextShot = currentTime + ReloadTime + new Random().Next(0, 1000);
        
        Vector2f pos = new Vector2f(Position.X, Position.Y);
        scene.QueueSpawn(new Bullet(GetType(), pos, 300, Direction));
    }
    
    protected override void OnTouchingWall(Scene scene)
    {
        float lastXPos = Position.X;
        Position = new Vector2f(Math.Clamp(Position.X, 0 + Bounds.Width / 2, Program.WindowSize.X - Bounds.Width / 2),
            Position.Y);

        if (lastXPos != Position.X)
        {
            Direction = new Vector2f(-Direction.X, Direction.Y);
            Sprite.Rotation = MathF.Atan2(Direction.Y, Direction.X) * 180f / MathF.PI - 90;
        }

        if (Position.Y - Bounds.Height > Program.WindowSize.Y)
        {
            Position = GetRandomPosition();
        }
    }
}