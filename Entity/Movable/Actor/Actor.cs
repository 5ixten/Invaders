using SFML.System;

namespace Invaders;

public abstract class Actor : Movable
{
    protected float LastShot;
    protected float ReloadTime;

    public int MaxHealth { get; protected set; }
    public int Health { get; protected set; }
    
    public Actor(string textureName) : base(textureName)
    {
        MaxHealth = 1;
        Health = MaxHealth;
    }

    public abstract void Shoot(Scene scene);

    public virtual void TakeDamage(Scene scene, int amount)
    {
        Health = Math.Max(Health - amount, 0);
        if (Health <= 0)
        {
            IsDead = true;
        }
    }

    protected override void CollideWith(Scene scene, Entity other)
    {
        if (other is Bullet == false) return;
        
        Bullet bullet = (Bullet)other;
        if (bullet.Shooter == GetType()) return;
        
        TakeDamage(scene, bullet.Damage);
        bullet.IsDead = true;
    }

    public override void Destroy(Scene scene)
    {
        scene.QueueSpawn(new Explosion(Position));
    }
}