using SFML.Window;

namespace Invaders;

public class InputManager
{
    public readonly Dictionary<Keyboard.Key, bool> KeysDown = new();
    public bool MousePressed;

    public InputManager()
    {
        Program.Window.KeyPressed += (sender, args) =>
        {
            KeysDown[args.Code] = true;
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
    
    public bool IsKeyDown(Keyboard.Key key)
    {
        return KeysDown.TryGetValue(key, out bool isDown) && isDown;
    }
}