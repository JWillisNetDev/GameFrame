using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Drawing;

public class Sprite : IDrawable
{
	public Vector2 Position { get; set; }
	public Texture2D Texture { get; set; }

	public Sprite(Texture2D texture, Vector2 position)
	{
		Texture = texture;
		Position = position;
	}

	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Texture, Position, Color.White);
	}
}
