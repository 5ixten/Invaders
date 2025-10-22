using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class InGameGUI : GUI
{
    private Text scoreText;
    
    private float lastSpawn;
    private int playerHealth;
    
    public InGameGUI() : base("PlayerShip")
    {
        SceneState = SceneState.IN_GAME;
        scoreText = new Text();
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        Sprite.Scale = new Vector2f(0.35f, 0.35f);
        
        scoreText.Font = scene.AssetManager.LoadFont("pixel-font");
        scoreText.DisplayedString = "Score: 0";
        scoreText.CharacterSize = 16;
        scoreText.Position = new Vector2f(20, Program.WindowSize.Y - scoreText.GetLocalBounds().Height - 20);
    }
    
    public override void JustLoaded(Scene scene)
    {
        scene.Clock.Restart();
        lastSpawn = 0;
        scene.EnemySpawnSpeed = scene.BaseEnemySpawnSpeed;
        scene.QueueSpawn(new Player());
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;

        if (scene.FindByType<Player>(out Player player))
        {
            playerHealth = player.Health;
        }
        
        scene.Score = (int)Math.Floor(scene.Clock.ElapsedTime.AsSeconds()) * 10;
        scoreText.DisplayedString = $"Score: {scene.Score}";

        float currentTime = scene.Clock.ElapsedTime.AsMilliseconds();
        if (currentTime - lastSpawn > scene.EnemySpawnSpeed)
        {
            lastSpawn = currentTime;
            scene.EnemySpawnSpeed = Math.Max(scene.EnemySpawnSpeed - 100, 300); // Make enemies spawn faster
            scene.QueueSpawn(new Enemy());
        }
    }

    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        target.Draw(scoreText);

        RenderHealth(target);
    }
    
    private void RenderHealth(RenderTarget target)
    {
        for (int i = 0; i < playerHealth; i++) {
            Sprite.Position = new Vector2f(
                20 + i*20 + i * Sprite.GetGlobalBounds().Width,
                20);

            base.Render(target);
        }
    }
}