using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class HighScoreMenuGUI : GUI
{
    private Dictionary<string, int> _highscores;
    private List<Text> _highscoresTexts;
    
    public HighScoreMenuGUI() : base("PlayerShip")
    {
        SceneState = SceneState.HIGHSCORE_MENU;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        _highscoresTexts = new List<Text>();
        
        for (int i = 0; i < 15; i++)
        {
            Text newText = new Text();
            
            newText.Font = scene.AssetManager.LoadFont("pixel-font");
            newText.CharacterSize = 24;
            newText.Position = new Vector2f(50, 50 + i*35);
            
            _highscoresTexts.Add(newText);
        }
        
        Buttons.Add(new Button(this, SceneState.MAIN_MENU, "Back", 
            new Vector2f(275, 700)));
        scene.QueueSpawn(Buttons[^1]);
    }
    
    public override void JustLoaded(Scene scene)
    {
        _highscores = Program.GetHighscores();
        
        if (_highscoresTexts == null) return;
        int i = 0;
        foreach (var kvp in _highscores)
        {
            _highscoresTexts[i].DisplayedString = $"{kvp.Key}: {kvp.Value}";
            i++;
        }
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;

        foreach (var text in _highscoresTexts)
        {
            target.Draw(text);
        }
    }
}