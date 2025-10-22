using SFML.Audio;
using SFML.Graphics;

namespace Invaders;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;
    private readonly Dictionary<string, SoundBuffer> sounds;
    
    public AssetManager() 
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
        sounds = new Dictionary<string, SoundBuffer>();
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

    public SoundBuffer LoadSound(string name)
    {
        if (sounds.TryGetValue(name, out var soundBuffer))
        {
            return soundBuffer;
        }
        
        string filePath = $"Assets/{name}.wav";
        SoundBuffer sound = new SoundBuffer(filePath);
        sounds.Add(name, sound);
        
        return sound;
    }
}