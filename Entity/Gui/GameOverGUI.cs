using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GameOverGUI : GUI
{
    private Dictionary<string, int> _highscores;
    private Text _highscoresText;
    
    
    public GameOverGUI() : base("PlayerShip")
    {
        SceneState = SceneState.HIGHSCORE_MENU;
        _highscoresText = new Text();
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        _highscoresText.Font = scene.AssetManager.LoadFont("pixel-font");
        _highscoresText.CharacterSize = 24;
        _highscoresText.Position = new Vector2f(50, 50);

    }
    
    public override void JustLoaded(Scene scene)
    {
        _highscores = Program.GetHighscores();
        
        _highscores.Add("Sixte", 330);
        _highscores.Add("Cissy", 3030);
        _highscores.Add("OScar", 530);
        _highscores.Add("Loblo", 10);
        
        _highscoresText.DisplayedString =
            string.Join("\n", _highscores.Select(hvp => $"{hvp.Key}: {hvp.Value}"));
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        
        target.Draw(_highscoresText);
    }
}