namespace Invaders;

public delegate void PlaySoundEvent(SoundType soundType);
public class EventManager
{
    private SoundType? _soundType;
    
    public event PlaySoundEvent PlaySound;
    public void PublishPlaySound(SoundType soundType) => _soundType = soundType;

    public void Update()
    {
        if (_soundType != null) {
            PlaySound?.Invoke((SoundType)_soundType);
            _soundType = null;
        }
    }
}