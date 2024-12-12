using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame;

public interface IUpdateable
{
	void Draw(GameTime gameTime, SpriteBatch spriteBatch);
	void Update(GameTime gameTime);
}
