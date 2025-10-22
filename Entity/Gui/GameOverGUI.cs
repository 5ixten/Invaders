using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invaders;

public class GameOverGUI : GUI
{
    private Dictionary<string, int> _highscores;
    private List<Text> _highscoresTexts;
    private Text _inputText;
    private Text _scoreText;
    private Text _promptText;
    
    private string _inputName = "";
    private bool _allowHighscore;
    private bool _canContinue = false;
    
    public GameOverGUI() : base("PlayerShip")
    {
        SceneState = SceneState.GAME_OVER;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        _highscoresTexts = new List<Text>();
        _inputText = new Text();
        _scoreText = new Text();
        _promptText = new Text();
        
        _promptText.Font = scene.AssetManager.LoadFont("pixel-font");
        _promptText.CharacterSize = 24;
        _promptText.Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-50);
        _promptText.DisplayedString = "Press ENTER to continue";
        
        CenterOrigin(_promptText);
        
        _inputText.Font = scene.AssetManager.LoadFont("pixel-font");
        _inputText.CharacterSize = 24;
        _inputText.Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-90);
        
        _scoreText.Font = scene.AssetManager.LoadFont("pixel-font");
        _scoreText.CharacterSize = 24;
        _scoreText.Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-130);
        
        for (int i = 0; i < 15; i++)
        {
            Text newText = new Text();
            
            newText.Font = scene.AssetManager.LoadFont("pixel-font");
            newText.CharacterSize = 24;
            newText.Position = new Vector2f(50, 50 + i*35);
            
            _highscoresTexts.Add(newText);
        }
    }
    
    protected override void JustLoaded(Scene scene)
    {
        _inputName = "";
        _highscores = Program.GetHighscores();
        
        scene.EventManager.PublishPlaySound(SoundType.GameOver);

        _scoreText.DisplayedString = $"Final score: {scene.Score}";
        CenterOrigin(_scoreText);
        
        if (_highscoresTexts == null) return;
        
        _allowHighscore = _highscores.Count < 15 || _highscores.FirstOrDefault(kvp => kvp.Value < scene.Score).Key != null;
        
        int i = 0;
        foreach (var kvp in _highscores)
        {
            _highscoresTexts[i].DisplayedString = $"{i+1}.     {kvp.Key}: {kvp.Value}";
            i++;
        }
    }
    
    public override void Update(Scene scene, float deltaTime)
    {
        base.Update(scene, deltaTime);
        if (!IsActive) return;
        
        _inputText.DisplayedString = $"Your name: {_inputName}";
        _canContinue = true;
        
        CenterOrigin(_inputText);

        if (!_allowHighscore)
        {
            _inputText.DisplayedString = "Too low score for leaderboard";
            if (scene.InputManager.IsKeyDown(Keyboard.Key.Enter))
                scene.SetSceneState(SceneState.MAIN_MENU);
            
            CenterOrigin(_inputText);;
            
            return;
        }

        // Remove letter with backspace
        if (_inputName != "" && scene.InputManager.IsKeyDown(Keyboard.Key.Backspace) && !scene.InputManager.WasKeyPressed)
        {
            _inputName = _inputName.Substring(0, _inputName.Length - 1);
        }

        // Continue with enter
        if (_inputName.Length >= 5 && scene.InputManager.IsKeyDown(Keyboard.Key.Enter))
        {
            _inputName = _inputName.Substring(0, 5);
            Program.SaveHighscore(_inputName, scene.Score);
            
            scene.SetSceneState(SceneState.MAIN_MENU);
            scene.Score = 0;
            return;
        }

        if (_inputName.Length < 5)
        {
            _inputName += scene.InputManager.LastLetterPressed;
            _canContinue = false;
        }
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        
        target.Draw(_inputText);
        target.Draw(_scoreText);
        
        if (_canContinue)
            target.Draw(_promptText);
        
        foreach (var text in _highscoresTexts)
        {
            target.Draw(text);
        }
    }
}