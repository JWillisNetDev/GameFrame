using GameFrame.Drawing;
using GameFrame.UserInterface;
using GameFrame.UserInterface.Extensions;
using GameFrame.UserInterface.Fonts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameFrame;

public class GameManager
{
	public Game Game { get; }
	public GraphicsDeviceManager GraphicsDeviceManager { get; }
	public Canvas Canvas { get; }

	private GameTime _lastGameTime = new();
	private Sprite _testSprite;
	private Button _button;
	private Sprite _buttonSprite;

	private ContentManager Content => Game.Content;

	public GameManager(Game game, GraphicsDeviceManager gdm)
	{
		Game = game;
		GraphicsDeviceManager = gdm;
		Canvas = new Canvas(gdm.GraphicsDevice, 1920, 1080);
		SetResolution(1200, 800);
	}

	public void SetFullScreen()
	{
		GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
		GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		Game.Window.IsBorderless = true;
		GraphicsDeviceManager.ApplyChanges();
		Canvas.SetDestinationRectangle();
	}

	public void SetResolution(int width, int height)
	{
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0, nameof(width));
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0, nameof(height));

		GraphicsDeviceManager.PreferredBackBufferWidth = width;
		GraphicsDeviceManager.PreferredBackBufferHeight = height;
		GraphicsDeviceManager.ApplyChanges();
		Canvas.SetDestinationRectangle();
	}

	public void LoadContent()
	{
		FontManager.Instance.LoadContent(Content);
		_testSprite = CreateSprite("panels");

		var buttonTex = Content.Load<Texture2D>("test_button");
		_button = new Button(Canvas, buttonTex, new Vector2(100, 0));
		_button.Text(txt => txt.Text("Test Button").Font(Font.MenuFont));
	}

	public void Update(GameTime gameTime)
	{
		var inputManager = InputManager.Instance;
		inputManager.Update();
		if (inputManager.IsKeyPressed(Keys.F1))
		{
			SetResolution(400, 300);
		}
		if (inputManager.IsKeyPressed(Keys.F2))
		{
			SetResolution(1920, 1080);
		}
		if (inputManager.IsKeyPressed(Keys.F3))
		{
			SetResolution(640, 1080);
		}
		if (inputManager.IsKeyPressed(Keys.F4))
		{
			SetFullScreen();
		}

		_button.Update(gameTime);
	}

	public void Draw(GameTime gameTime)
	{
		using SpriteBatch spriteBatch = new(GraphicsDeviceManager.GraphicsDevice);
		using var _ = Canvas.BeginLifetime(spriteBatch);

		spriteBatch.Begin();

		_testSprite.Draw(gameTime, spriteBatch);
		_button.Draw(gameTime, spriteBatch);

		spriteBatch.End();
	}

	private Sprite CreateSprite(string assetName, Vector2 position = default)
	{
		Texture2D tex = Content.Load<Texture2D>(assetName);
		return new Sprite(tex, position);
	}
}
