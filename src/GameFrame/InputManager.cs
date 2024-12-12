using Microsoft.Xna.Framework.Input;

namespace GameFrame;

public class InputManager
{
	public static InputManager Instance { get; private set; } = new();

	private KeyboardState _lastState;
	private KeyboardState _currentState;

	public bool IsKeyDown(Keys key) => _currentState.IsKeyDown(key);

	public bool IsAnyKeyDown(params Keys[] keys)
	{
		foreach (var key in keys)
		{
			if (_currentState.IsKeyDown(key))
			{
				return true;
			}
		}

		return false;
	}

	public bool IsKeyPressed(Keys key) => _currentState.IsKeyDown(key) && _lastState.IsKeyUp(key);

	public bool IsKeyHeld(Keys key) => _currentState.IsKeyDown(key) && _lastState.IsKeyDown(key);

	public bool IsKeyReleased(Keys key) => _currentState.IsKeyUp(key) && _lastState.IsKeyDown(key);

	public void Update()
	{
		_lastState = _currentState;
		_currentState = Keyboard.GetState();
	}
}
