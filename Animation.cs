using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Animation
{
    public readonly IntRect[] TextureInfos;
    public readonly float Speed;
    private Clock clock;
    private int currentFrame;

    public Animation(IntRect[] textureInfos, float speed)
    {
        TextureInfos = textureInfos;
        Speed = speed;
        clock = new Clock();
    }
    
    public int LoopedCount => (int)Math.Floor((float)(currentFrame / TextureInfos.Length));

    public IntRect GetCurrentTextureRect()
    {
        if (clock.ElapsedTime.AsMilliseconds() >= Speed)
        {
            currentFrame++;
            clock.Restart();
        }
        return TextureInfos[currentFrame % TextureInfos.Length];
    }
}