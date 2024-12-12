using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame;

public interface IDrawable
{
	void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}