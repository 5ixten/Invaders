using SFML.Audio;

namespace Invaders;

public class SoundManager
{
    private Dictionary<SoundType, Func<SoundBuffer>> loaders;
    private readonly List<Sound> activeSounds = new List<Sound>();

    public SoundManager(Scene scene)
    {
        scene.EventManager.PlaySound += PlaySound;
        
        loaders = new Dictionary<SoundType, Func<SoundBuffer>> {
            {SoundType.Explosion, () => scene.AssetManager.LoadSound("Explosion")},
            {SoundType.Shot, () => scene.AssetManager.LoadSound("Shot")},
            {SoundType.GameOver, () => scene.AssetManager.LoadSound("GameOver")}
        };

        // Preload all sounds
        foreach (var kvp in loaders)
        {
            kvp.Value();
        }
    }

    public void Update()
    {
        activeSounds.RemoveAll(s =>
        {
            if (s.Status != SoundStatus.Playing)
            {
                s.Stop();
                s.Dispose();
                return true;
            }
            return false;
        });
    }

    private void PlaySound(SoundType soundType)
    {
        if (Create(soundType, out SoundBuffer soundBuffer))
        {
            Sound newSound = new Sound(soundBuffer);
            newSound.Play();
            
            activeSounds.Add(newSound);
        }
    }

    private bool Create(SoundType soundType, out SoundBuffer created) 
    {
        if (loaders.TryGetValue(soundType, out Func<SoundBuffer> loader)) 
        {
            created = loader();
            return true;
        }
        
        created = null;
        return false;
    }
    
    public void DisposeAll()
    {
        for (int i = 0; i < activeSounds.Count; i++)
        {
            activeSounds[i].Stop();
            activeSounds[i].Dispose();
        }
    }
}