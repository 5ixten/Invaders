using SFML.Graphics;

namespace Invaders;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;
    
    public AssetManager() 
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    }

    public Texture LoadTexture(string name)
    {
        if (textures.TryGetValue(name, out var foundTexture))
        {
            return foundTexture;
        }
        
        string filePath = $"Assets/{name}.png";
        Texture texture = new Texture(filePath);
        textures.Add(name, texture);
        
        return texture;
    }
    
    public Font LoadFont(string name)
    {
        if (fonts.TryGetValue(name, out var foundFont))
        {
            return foundFont;
        }
        
        string filePath = $"Assets/{name}.ttf";
        Font font = new Font(filePath);
        fonts.Add(name, font);
        
        return font;
    }
}