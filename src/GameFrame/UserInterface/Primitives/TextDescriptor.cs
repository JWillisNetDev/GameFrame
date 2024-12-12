using GameFrame.UserInterface.Fonts;
using Microsoft.Xna.Framework;

namespace GameFrame.UserInterface.Primitives;

public class TextDescriptor
{
	public string Text { get; set; }
	public Vector2 Margins { get; set; } = Vector2.Zero;
	public Font Font { get; set; } = Font.Default;
}