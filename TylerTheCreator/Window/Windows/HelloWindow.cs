using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class HelloWindow : Window
{
    public HelloWindow(GraphicsDevice graphicsDevice, InputHandler inputHandler, Point position, string windowTitle, Style style) :
        base(graphicsDevice, inputHandler, position, windowTitle, style)
    {
        var titleBarStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.DarkBlue,
            TextColor = Color.White
        };

        var headerStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Transparent,
            TextColor = Color.White
        };

        TitleBar titleBar = new TitleBar(new Rectangle(0, 0, Bounds.Width, 28), "Titlebar", titleBarStyle, graphicsDevice);
        Header header = new Header(new Rectangle(0, 28, Bounds.Width, 50), "Header", headerStyle, graphicsDevice);

        AddElement(titleBar);
        AddElement(header);
    }
}