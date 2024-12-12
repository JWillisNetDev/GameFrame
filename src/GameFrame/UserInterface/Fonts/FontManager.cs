using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace GameFrame.UserInterface.Fonts;

public class FontManager
{
	public static FontManager Instance { get; } = new();

	public IReadOnlyDictionary<Font, SpriteFont> Fonts => _fonts;

	private readonly Dictionary<Font, SpriteFont> _fonts = new();

	public void LoadContent(ContentManager contentManager)
	{
		LoadContentToFontCache(contentManager);
	}

	public SpriteFont GetSpriteFont(Font font)
	{
		return _fonts.TryGetValue(font, out var spriteFont) ? spriteFont : null;
	}

	private void LoadContentToFontCache(ContentManager cm)
	{
		var fontType = typeof(Font);
		foreach (var field in fontType.GetFields())
		{
			if (field.GetCustomAttribute<DescriptionAttribute>(true) is { Description: { Length: > 0 } desc })
			{
				Font fValue = (Font)field.GetValue(null);
				_fonts[fValue] = cm.Load<SpriteFont>(desc);
			}
		}
	}
}
