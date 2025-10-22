using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Animation
{
    public readonly IntRect[] Frames;
    public readonly float Speed;
    private Clock _clock;
    private int _currentFrame;

    public Animation(IntRect[] frames, float speed)
    {
        Frames = frames;
        Speed = speed;
        _clock = new Clock();
    }
    
    public int LoopedCount => (int)Math.Floor((float)(_currentFrame / Frames.Length));

    public IntRect GetCurrentTextureRect()
    {
        if (_clock.ElapsedTime.AsMilliseconds() >= Speed)
        {
            _currentFrame++;
            _clock.Restart();
        }
        return Frames[_currentFrame % Frames.Length];
    }
}