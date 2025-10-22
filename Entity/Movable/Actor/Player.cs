using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class Player : Actor
{
    private const float _damageCoolDown = 2000;
    private float _lastDamageTaken = -_damageCoolDown;
    private bool _isInvincible;
    
    public Player() : base("PlayerShip")
    {
        ZIndex = 5;
        Speed = 200f;
        ReloadTime = 500f;
        
        Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-150);
        
        MaxHealth = 3;
        Health = MaxHealth;
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        ReloadTime = Math.Min(ReloadTime, scene.EnemySpawnSpeed*2);
        
        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        _isInvincible = currentTime < _lastDamageTaken + _damageCoolDown;
        
        int xDir = 0;
        xDir += scene.InputManager.IsKeyDown(Keyboard.Key.A) ? -1 : 0;
        xDir += scene.InputManager.IsKeyDown(Keyboard.Key.D) ? 1 : 0;
        
        int yDir = 0;
        yDir += scene.InputManager.IsKeyDown(Keyboard.Key.W) ? -1 : 0;
        yDir += scene.InputManager.IsKeyDown(Keyboard.Key.S) ? 1 : 0;
        
        Direction = new Vector2f(xDir, yDir);

        if (scene.InputManager.MousePressed || scene.InputManager.IsKeyDown(Keyboard.Key.Space))
        {
            Shoot(scene);
        }
    }
    
    public override void Shoot (Scene scene){
        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        if (currentTime - LastShot <= ReloadTime) return;
        LastShot = currentTime;

        int cannonOffsetX = 22;
        int cannonOffsetY = 15;
        Vector2f pos1 = new Vector2f(Position.X - Bounds.Width / 2 + cannonOffsetX, Position.Y - Bounds.Height / 2 + cannonOffsetY);
        Vector2f pos2 = new Vector2f(Position.X + Bounds.Width / 2 - cannonOffsetX, Position.Y - Bounds.Height / 2 + cannonOffsetY);

        scene.QueueSpawn(new Bullet(GetType(), pos1, 500, new Vector2f(0, -1)));
        scene.QueueSpawn(new Bullet(GetType(), pos2, 500, new Vector2f(0, -1)));
        
        scene.EventManager.PublishPlaySound(SoundType.Shot);
    }

    public override void Render(RenderTarget target)
    {
        Sprite.Color = new Color(255, 255, 255, _isInvincible ? (byte)90 : (byte)255);
        base.Render(target);
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        Sprite.Rotation = 180;
    }
    
    public override void TakeDamage(Scene scene, int amount)
    {
        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        if (_isInvincible) return; // Invincible after taking damage for a short while
        _lastDamageTaken = currentTime;
        
        base.TakeDamage(scene, amount);
        if (IsDead)
        {
            scene.SetSceneState(SceneState.GAME_OVER);
        }
    }
    
    protected override void CollideWith(Scene scene, Entity other)
    {
        base.CollideWith(scene, other);
        if (other is Enemy == false) return;
        
        Enemy enemy = (Enemy)other;
        
        enemy.TakeDamage(scene, enemy.Health); // Kill enemy
        TakeDamage(scene, 1);
    }
    
    protected override void OnTouchingWall(Scene scene)
    {
        Position = new Vector2f(
            Math.Clamp(Position.X, 0 + Bounds.Width/2, Program.WindowSize.X - Bounds.Width/2),
            Math.Clamp(Position.Y, 0 + Bounds.Height/2, Program.WindowSize.Y - Bounds.Height/2));
    }
}