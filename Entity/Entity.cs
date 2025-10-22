using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity
{
    private string _textureName;
    protected Sprite Sprite;
    protected Animation[] Animations;
    
    public int ZIndex;
    public bool DontDestroyOnLoad;
    public bool IsDead;

    public Vector2f Position
    {
        get { return Sprite.Position; }
        set { Sprite.Position = value; }
    }

    public virtual FloatRect Bounds => Sprite.GetGlobalBounds();

    public virtual bool IsSolid => false;

    public Entity(string textureName)
    {
        _textureName = textureName;
        Sprite = new Sprite();
    }

    public static void CenterOrigin(Sprite sprite)
    {
        FloatRect bounds = sprite.GetLocalBounds();
        sprite.Origin = new Vector2f(
            bounds.Width / 2f, bounds.Height / 2f);
    }
    
    public static void CenterOrigin(Text text)
    {
        FloatRect bounds = text.GetLocalBounds();
        text.Origin = new Vector2f(
            bounds.Width / 2f, bounds.Height / 2f);
    }

    public virtual void Create(Scene scene)
    {
        Sprite.Texture = scene.AssetManager.LoadTexture(_textureName);
    }
    
    public virtual void Destroy(Scene scene)
    {
        
    }
    
    public virtual void Update(Scene scene, float deltaTime) 
    {
        foreach (Entity found in scene.FindIntersects(Bounds)) 
        {
            CollideWith(scene, found);
        }
    }
    
    public virtual void Render(RenderTarget target)
    {
        target.Draw(Sprite);
    }

    protected virtual void CollideWith(Scene scene, Entity other)
    {
        
    }
}