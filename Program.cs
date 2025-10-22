using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Text;

namespace Invaders;

class Program {
    
    public static readonly Vector2u WindowSize = new Vector2u(550, 800);
    public static Window Window { get; private set; }
    
    static void Main(string[] args) 
    {
        using (var window = new RenderWindow(
                   new VideoMode(WindowSize.X, WindowSize.Y), "Invaders")) 
        {
            Window = window;
            
            Clock clock = new Clock();
            Scene scene = new Scene();
            
            window.Closed += (o, e) =>
            {
                scene.SoundManager.DisposeAll();
                scene.AssetManager.DisposeAll();
                window.Close();
            };
       
            while (window.IsOpen) {
                window.DispatchEvents();
                
                
                float deltaTime = clock.Restart().AsSeconds();
                deltaTime = MathF.Min(deltaTime, 0.01f);
                
                scene.UpdateAll(deltaTime);

                window.Clear(new Color(16, 2, 31));
                
                scene.RenderAll(window);

                window.Display();
            }
        }
    }

    private static void SaveHighscores(Dictionary<string, int> highscores)
    {
        string saveString = string.Join(";", highscores.Select(hvp => $"{hvp.Key},{hvp.Value}"));
        File.WriteAllText("Highscores.txt", saveString, Encoding.UTF8);
    }

    public static void SaveHighscore(string name, int score)
    {
        Dictionary<string, int> highscores = GetHighscores();
        highscores[name] = score;
        
        // Sort highscores
        highscores = highscores
            .OrderByDescending(h => h.Value)
            .ToDictionary(h => h.Key, h => h.Value);
        
        // Remove lowest highscore if full
        if (highscores.Count > 15)
        {
            var lowest = highscores.OrderBy(h => h.Value).First();
            highscores.Remove(lowest.Key);
        }
        
        SaveHighscores(highscores);
    }
    
    public static Dictionary<string, int> GetHighscores()
    {
        Dictionary<string, int> highscores = new();

        if (!File.Exists("Highscores.txt"))
        {
            return highscores;
        }
        
        string savedContent = File.ReadAllText("Highscores.txt", Encoding.UTF8);
        string[] nameScorePairs = savedContent.Split(';');

        foreach (string nameScorePair in nameScorePairs)
        {
            string[] splitPair = nameScorePair.Split(',');
            string name = splitPair[0];
            string score = splitPair[1];
            
            if (int.TryParse(score, out int scoreInt))
            {
                highscores.Add(name, scoreInt);
            }
        }
        
        return highscores; 
    }
}