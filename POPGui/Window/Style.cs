using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Style
{
    private static Style emptyStyle;
    public SpriteFont? Font { get; set; }
    public Point? DefaultSize { get; set; }
    public Color? BackgroundColor { get; set; }
    public Color? BorderColor { get; set; }
    public Color? TextColor { get; set; }
    public int? Padding { get; set; }
    public int? Margin { get; set; }

    public static Style Empty => emptyStyle;

    public Style(SpriteFont? font, Point? defaultSize, Color? backgroundColor, Color? borderColor, Color? textColor, int? padding, int? margin)
    {
        Font = font;
        DefaultSize = defaultSize;
        BackgroundColor = backgroundColor;
        BorderColor = borderColor;
        TextColor = textColor;
        Padding = padding;
        Margin = margin;
    }

    public Style()
    {
        Font = null;
        DefaultSize = null;
        BackgroundColor = Color.Transparent;
        BorderColor = null;
        TextColor = Color.White;
        Padding = null;
        Margin = null;
    }

    public Style(Color? backgroundColor)
    {
        BackgroundColor = backgroundColor;
    }

    public Style(Color? backgroundColor, Point border)
    {
        BackgroundColor = backgroundColor;
        DefaultSize = border;
    }
}
