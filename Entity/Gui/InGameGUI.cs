using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class InGameGUI : GUI
{
    private Text scoreText;
    
    private float lastSpawn;
    
    public InGameGUI() : base("PlayerShip")
    {
        SceneState = SceneState.IN_GAME;
        scoreText = new Text();
    }

    public override void Create(Scene scene)
    {
        scoreText.Font = scene.AssetManager.LoadFont("pixel-font");
        scoreText.DisplayedString = "Score: 0";
        scoreText.CharacterSize = 16;
        scoreText.Position = new Vector2f(20, Program.WindowSize.Y - scoreText.GetLocalBounds().Height - 20);
    }
    
    public override void JustLoaded(Scene scene)
    {
        scene.EnemySpawnSpeed = scene.BaseEnemySpawnSpeed;
        scene.QueueSpawn(new Player());
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;

        scene.Score = (int)Math.Floor(scene.Clock.ElapsedTime.AsSeconds()) * 10;
        scoreText.DisplayedString = $"Score: {scene.Score}";

        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        if (currentTime - lastSpawn > scene.EnemySpawnSpeed)
        {
            lastSpawn = currentTime;
            scene.EnemySpawnSpeed = Math.Max(scene.EnemySpawnSpeed - 50, 500); // Make enemies spawn faster
            scene.QueueSpawn(new Enemy());
        }
    }

    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        target.Draw(scoreText);
    }
}