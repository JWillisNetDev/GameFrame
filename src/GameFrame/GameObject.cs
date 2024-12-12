using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame;

public abstract class GameObject : IUpdateable, IDrawable
{
	protected GameManager GameManager { get; }

	protected GameObject(GameManager gameManager)
	{
		GameManager = gameManager;
	}

	public virtual void LoadContent(ContentManager contentManager)
	{
		// Do nothing
	}

	public virtual void Initialize()
	{
		// Do nothing
	}

	public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
	{
		// Do nothing
	}

	public virtual void Update(GameTime gameTime)
	{
		// Do nothing
	}
}