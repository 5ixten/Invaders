using SFML.Window;

namespace Invaders;

public class InputManager
{
    private string _alphabet = "abcdefghijklmnopqrstuvwxyzåäö";
    
    public readonly Dictionary<Keyboard.Key, bool> KeysDown = new();
    public bool WasKeyPressed;
    public string LastLetterPressed;
    
    public bool MousePressed;

    public InputManager()
    {
        Program.Window.KeyPressed += (sender, args) =>
        {
            KeysDown[args.Code] = true;
            WasKeyPressed = true;
            
            if (_alphabet.Contains(args.Code.ToString().ToLower()))
            {
                LastLetterPressed = args.Code.ToString();
            }
        };

        Program.Window.KeyReleased += (sender, args) =>
        {
            KeysDown[args.Code] = false;
        };
        
        Program.Window.MouseButtonPressed += (sender, e) =>
        {
            if (e.Button == Mouse.Button.Left)
                MousePressed = true;
        };
        
        Program.Window.MouseButtonReleased += (sender, e) =>
        {
            if (e.Button == Mouse.Button.Left)
                MousePressed = false;
        };
    }

    public void Update()
    {
        WasKeyPressed = false;
        LastLetterPressed = "";
    }
    
    public bool IsKeyDown(Keyboard.Key key)
    {
        return KeysDown.TryGetValue(key, out bool isDown) && isDown;
    }
}