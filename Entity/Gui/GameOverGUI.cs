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
    private Text _prompText;
    
    private string _inputName = "";
    private bool _allowHighscore;
    
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
        _prompText = new Text();
        
        _prompText.Font = scene.AssetManager.LoadFont("pixel-font");
        _prompText.CharacterSize = 24;
        _prompText.Position = new Vector2f(Program.WindowSize.X/2, Program.WindowSize.Y-50);
        _prompText.DisplayedString = "Press ENTER to continue";
        
        Entity.CenterOrigin(_prompText);
        
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
    
    public override void JustLoaded(Scene scene)
    {
        _inputName = "";
        _highscores = Program.GetHighscores();
        
        scene.EventManager.PublishPlaySound(SoundType.GameOver);

        _scoreText.DisplayedString = $"Final score: {scene.Score}";
        Entity.CenterOrigin(_scoreText);
        
        if (_highscoresTexts == null) return;
        
        _allowHighscore = _highscores.Count < 15 || _highscores.FirstOrDefault(kvp => kvp.Value < scene.Score).Key != null;
        
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
        
        _inputText.DisplayedString = $"Your name: {_inputName}";
        
        Entity.CenterOrigin(_inputText);

        if (!_allowHighscore)
        {
            _inputText.DisplayedString = "Too low score for leaderboard";
            if (scene.InputManager.IsKeyDown(Keyboard.Key.Enter))
                scene.SetSceneState(SceneState.MAIN_MENU);
            
            Entity.CenterOrigin(_inputText);;
            
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
            _inputName += scene.InputManager.LastLetterPressed;
    }
    
    public override void Render(RenderTarget target)
    {
        if (!IsActive) return;
        
        target.Draw(_inputText);
        target.Draw(_prompText);
        target.Draw(_scoreText);
        
        foreach (var text in _highscoresTexts)
        {
            target.Draw(text);
        }
    }
}