using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Header : GUIElement
{
    private string _text;
    private GraphicsDevice _graphicsDevice;
    private Color _backgroundColor;
    private SpriteFont _font;

    public Header(Rectangle bounds, string text, Style style, GraphicsDevice graphicsDevice) : base(bounds, style, graphicsDevice)
    {
        _text = text;
        _graphicsDevice = graphicsDevice;
        _backgroundColor = style.BackgroundColor ?? Color.Transparent;
        _font = style.Font;
    }

    public override void Update(InputHandler inputHandler, Window window)
    {
        // dumb C#, made me write this empty override to not cause myself more mental anguish...
        // this is VERY likely due to my lack of experience and not knowing any better :)
    }

    public override void Draw(SpriteBatch spriteBatch, Window window)
    {
        int x = window.Bounds.X + Bounds.X;
        int y = window.Bounds.Y + Bounds.Y;

        Rectangle rect = new Rectangle(new Point(x, y), new Point(Bounds.Width, Bounds.Height));
        spriteBatch.Draw(BackgroundTexture, rect, _backgroundColor);

        if (Style.Font != null)
        {
            Vector2 textSize = _font.MeasureString(_text);
            Vector2 textPosition = new Vector2(x + 7, y + 5);
            spriteBatch.DrawString(Style.Font, _text, textPosition, Style.TextColor ?? Color.White);
        }
    }
}
