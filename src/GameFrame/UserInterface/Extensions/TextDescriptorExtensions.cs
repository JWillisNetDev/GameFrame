using GameFrame.UserInterface.Fonts;
using GameFrame.UserInterface.Primitives;
using System.Numerics;

namespace GameFrame.UserInterface.Extensions;

public static class TextDescriptorExtensions
{
	public static TextDescriptor Text(this TextDescriptor td, string text)
	{
		td.Text = text;
		return td;
	}

	public static TextDescriptor Margins(this TextDescriptor td, Vector2 margins)
	{
		td.Margins = margins;
		return td;
	}

	public static TextDescriptor Font(this TextDescriptor td, Font font)
	{
		td.Font = font;
		return td;
	}
}
