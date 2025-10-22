using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class GameOverGUI : GUI
{
    private Dictionary<string, int> _highscores;
    private List<Text> _highscoresTexts;
    private Text _inputText;
    
    private string _inputName = "";
    
    public GameOverGUI() : base("PlayerShip")
    {
        SceneState = SceneState.GAME_OVER;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        _highscoresTexts = new List<Text>();
        _inputText = new Text();
        
        _inputText.Font = scene.AssetManager.LoadFont("pixel-font");
        _inputText.CharacterSize = 24;
        _inputText.Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-300);
        
        for (int i = 0; i < 5; i++)
        {
            Text newText = new Text();
            
            newText.Font = scene.AssetManager.LoadFont("pixel-font");
            newText.CharacterSize = 24;
            newText.Position = new Vector2f(50, 50 + i*35);
            
            _highscoresTexts.Add(newText);
        }
    }
    
    public override void JustLoaded(Scene scene)
    {
        _inputName = "";
        _highscores = Program.GetHighscores();
  
        _highscores.Add("Sixte", 330);
        _highscores.Add("Charl", 3030);
        _highscores.Add("OScar", 530);
        _highscores.Add("Labubu", 10);
        
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
        
        _inputText.DisplayedString = _inputName;
        
        FloatRect bounds = _inputText.GetLocalBounds();
        _inputText.Origin = new Vector2f(
            bounds.Width / 2f, bounds.Height / 2f);
        Console.WriteLine(_inputText.Position + " " + _inputText.DisplayedString);

        // Remove letter with backspace
        if (_inputName != "" && scene.InputManager.IsKeyDown(Keyboard.Key.Backspace) && !scene.InputManager.WasKeyPressed)
        {
            _inputName = _inputName.Substring(0, _inputName.Length - 1);
        }

        // Continue with enter
        if (_inputName.Length >= 5 && scene.InputManager.IsKeyDown(Keyboard.Key.Enter))
        {
            _inputName = _inputName.Substring(0, 5);
            scene.SetSceneState(SceneState.MAIN_MENU);
            return;
        }
        
        if (_inputName.Length < 5)
            _inputName += scene.InputManager.LastLetterPressed;
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        
        target.Draw(_inputText);
        
        foreach (var text in _highscoresTexts)
        {
            target.Draw(text);
        }
    }
}