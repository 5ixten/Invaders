using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Text;

namespace Invaders;

class Program {
    
    public static readonly Vector2u WindowSize = new Vector2u(550, 800);
    
    static void Main(string[] args) {
        using (var window = new RenderWindow(
                   new VideoMode(WindowSize.X, WindowSize.Y), "Pacman")) {
            
            window.Closed += (o, e) => window.Close();

            Clock clock = new Clock();
            Scene scene = new Scene(window);
       
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

    public static void SaveHighScore(string name, int score)
    {
        File.WriteAllText("HighScore.txt", score.ToString(), Encoding.UTF8);
    }
    
    static int GetHighScore()
    {
        string savedContent = File.ReadAllText("HighScore.txt", Encoding.UTF8);
        if (int.TryParse(savedContent, out int score))
            return score;

        return 0; 
    }
}