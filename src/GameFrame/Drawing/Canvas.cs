using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFrame.Drawing;

public class Canvas
{
	private readonly RenderTarget2D _renderTarget;
	private readonly GraphicsDevice _graphicsDevice;
	private float _scale;
	private Vector2 _screenOffset;
	private Rectangle _destinationRectangle;

	public Canvas(GraphicsDevice graphicsDevice, int width, int height)
	{
		_graphicsDevice = graphicsDevice;
		_renderTarget = new RenderTarget2D(graphicsDevice, width, height);
	}

	public void SetDestinationRectangle()
	{
		var screenSize = _graphicsDevice.PresentationParameters.Bounds;

		float scaleX = (float)screenSize.Width / _renderTarget.Width, scaleY = (float)screenSize.Height / _renderTarget.Height;
		_scale = Math.Min(scaleX, scaleY);

		int viewportWidth = (int)(_renderTarget.Width * _scale), viewportHeight = (int)(_renderTarget.Height * _scale);

		int offsetX = (screenSize.Width - viewportWidth) / 2, offsetY = (screenSize.Height - viewportHeight) / 2;
		_screenOffset = new(offsetX, offsetY);
		_destinationRectangle = new Rectangle(offsetX, offsetY, viewportWidth, viewportHeight);
	}

	public Vector2 ToViewportPosition(Vector2 windowPosition)
	{
		float x = windowPosition.X - _screenOffset.X, y = windowPosition.Y - _screenOffset.Y;
		float vpX = Math.Clamp(x / _scale, 0, _renderTarget.Width), vpY = Math.Clamp(y / _scale, 0, _renderTarget.Height);
		return new Vector2(vpX, vpY);
	}

	public Point ToViewportPosition(Point point)
	{
		float x = point.X - _screenOffset.X, y = point.Y - _screenOffset.Y;
		int vpX = Util.ClampInt32((int)(x / _scale), 0, _renderTarget.Width), vpY = Util.ClampInt32((int)(y / _scale), 0, _renderTarget.Height);
		return new Point(vpX, vpY);
	}

	public IDisposable BeginLifetime(SpriteBatch spriteBatch)
	{
		_graphicsDevice.SetRenderTarget(_renderTarget);
		_graphicsDevice.Clear(Color.Black);
		return new CanvasLifetime(this, spriteBatch);
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		_graphicsDevice.SetRenderTarget(null);
		_graphicsDevice.Clear(Color.Black);
		spriteBatch.Begin();
		spriteBatch.Draw(_renderTarget, _destinationRectangle, Color.White);
		spriteBatch.End();
	}

	private sealed class CanvasLifetime(Canvas canvas, SpriteBatch spriteBatch) : IDisposable
	{
		public void Dispose()
		{
			canvas.Draw(spriteBatch);
		}
	}
}
