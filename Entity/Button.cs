using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Button : Entity
{
    private SceneState _sceneState;
    private GUI _guiParent;
    private string _text;
    private Text _textInstance;
    
    public bool IsHovered;

    public Button(GUI guiParent, SceneState sceneState, string text, Vector2f position) : base("Button")
    { 
        _guiParent = guiParent;
        _sceneState = sceneState;
        Sprite.Position = position;
        _text = text;

        DontDestroyOnLoad = true;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        
        Entity.CenterOrigin(Sprite);
        
        _textInstance = new Text();
        _textInstance.Font = scene.AssetManager.LoadFont("pixel-font");
        _textInstance.CharacterSize = 16;
        _textInstance.DisplayedString = _text;
        
        Entity.CenterOrigin(_textInstance);
        
        _textInstance.Position = Position;
    }

    public override void Update(Scene scene, float deltaTime)
    {
        if (_guiParent.IsActive && IsHovered && scene.InputManager.MousePressed)
        {
            scene.SetSceneState(_sceneState);
        }
    }

    public override void Render(RenderTarget target)
    {
        if (!_guiParent.IsActive) return;
        
        Sprite.Color = new Color(255, 120, IsHovered ? (byte)80 : (byte)120, 255);
        base.Render(target);
        target.Draw(_textInstance);
    }
}