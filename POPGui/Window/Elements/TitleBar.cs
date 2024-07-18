using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class TitleBar : GUIElement
{
    public string Title;
    public Color BackgroundColor;
    public Color TextColor;
    private SpriteFont _font;
    private bool _isDragging;
    private Point _nextWindowPos;

    public TitleBar(int height, string title, Style style, GraphicsDevice graphicsDevice) : base(new Rectangle(0, 0, 0, height), style, graphicsDevice)
    {
        Title = title;
        TextColor = style.TextColor ?? Color.White;
        BackgroundColor = style.BackgroundColor ?? Color.Black;
        _font = style.Font;
    }

    public override void Update(InputHandler inputHandler, Window window)
    {
        var mousePosition = inputHandler.mousePosition;
        var titleBarBounds = new Rectangle(window.Bounds.X, window.Bounds.Y, window.Bounds.Width, Bounds.Height);

        if (_isDragging)
        {
            if (!inputHandler.IsMouseLeftDown())
            {
                _isDragging = false;
            }
            else
            {
                int xPos = mousePosition.X - _nextWindowPos.X;
                int yPos = mousePosition.Y - _nextWindowPos.Y;

                // Debug.WriteLine("Mouse   >> X: " + mousePosition.X + " | Y: " + mousePosition.Y);
                // Debug.WriteLine("Offset  >> X: " + _nextWindowPos.X + " | Y: " + _nextWindowPos.Y);
                // Debug.WriteLine("NextPos >> X: " + xPos + " | Y: " + yPos);
                window.MoveTo(new Point(xPos, yPos));
            }
        }

        if (window.IsDraggable && inputHandler.IsMouseLeftDown() && titleBarBounds.Contains(mousePosition))
        {
            _isDragging = true;
            _nextWindowPos = new Point(mousePosition.X - window.Bounds.X, mousePosition.Y - window.Bounds.Y);
        }

        if (window.IsDraggable && !inputHandler.IsMouseLeftDown())
        {
            if (window.Bounds.X < 0 || window.Bounds.Y < 0)
                window.MoveTo(new Point(0, 0));
        }

    }

    public override void Draw(SpriteBatch spriteBatch, Window window)
    {
        int x = window.Bounds.X + Bounds.X;
        int y = window.Bounds.Y + Bounds.Y;

        Rectangle rect = new Rectangle(new Point(x, y), new Point(window.Bounds.Width, Bounds.Height));
        spriteBatch.Draw(BackgroundTexture, rect, BackgroundColor);

        Vector2 textSize = _font.MeasureString(Title);
        Vector2 textPosition = new Vector2(x + 7 , y + (Bounds.Height / 2) - (textSize.Y / 2));

        spriteBatch.DrawString(_font, Title, textPosition, TextColor);
    }
}
