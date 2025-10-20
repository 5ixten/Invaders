using SFML.Window;

namespace Invaders;

public class InputManager
{
    public readonly Dictionary<Keyboard.Key, bool> KeysDown = new();

    public InputManager(Window window)
    {
        window.KeyPressed += (sender, args) =>
        {
            KeysDown[args.Code] = true;
        };

        window.KeyReleased += (sender, args) =>
        {
            KeysDown[args.Code] = false;
        };
    }
    
    public bool IsKeyDown(Keyboard.Key key)
    {
        return KeysDown.TryGetValue(key, out bool isDown) && isDown;
    }
}