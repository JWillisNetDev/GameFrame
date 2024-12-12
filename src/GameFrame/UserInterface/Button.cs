using GameFrame.Drawing;
using GameFrame.UserInterface.Extensions;
using GameFrame.UserInterface.Fonts;
using GameFrame.UserInterface.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace GameFrame.UserInterface;

public interface IUserInterfaceElement
{
	Vector2 Position { get; }
	Rectangle Rectangle { get; }

	void Update(GameTime gameTime);
	void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}

public class Button : IUserInterfaceElement
{
	public Vector2 Position => _position;
	public Rectangle Rectangle => _rect;

	public bool CenterText { get; set; }

	private TextDescriptor _textDescriptor;
	private readonly Canvas _canvas;
	private readonly Texture2D _texture;
	private Vector2 _position;
	private Rectangle _rect;
	private Color _shade = Color.White;
	private bool _isMouseOver;
	private bool _isMouseDown;
	private bool _isMousePressed;
	private bool _isMouseReleased;

	public Button(Canvas canvas, Texture2D texture, Vector2 position)
	{
		_canvas = canvas;
		_texture = texture;
		_position = position;
		_rect = new Rectangle((int)_position.X, (int)_position.Y, texture.Width, texture.Height);
	}

	public Button(Canvas canvas, Texture2D texture, Vector2 position, string text) : this(canvas, texture, position)
	{
		_textDescriptor.Text(text);
	}

	public void Text(Action<TextDescriptor> textDescriptorDefinition)
	{
		_textDescriptor ??= new TextDescriptor();
		textDescriptorDefinition(_textDescriptor);
	}

	public void Update(GameTime gameTime)
	{
		MouseState ms = Mouse.GetState();
		Point vpPoint = _canvas.ToViewportPosition(ms.Position);
		Rectangle cursor = new(vpPoint.X, vpPoint.Y, 1, 1);

		if (cursor.Intersects(_rect))
		{
			_shade = Color.DarkGray;
			Debug.WriteLine("HOVERING");
		}
		else
		{
			_shade = Color.White;
		}
	}

	public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(_texture, _position, _shade);

		// TODO: This shouldn't be calculated every frame. Make it state reactive.
		var fm = FontManager.Instance;
		if (_textDescriptor is { Text: { } txt } && fm.GetSpriteFont(_textDescriptor.Font) is { } spriteFont)
		{
			spriteBatch.DrawString(spriteFont, txt, _position, Color.White);
		}
	}
}
