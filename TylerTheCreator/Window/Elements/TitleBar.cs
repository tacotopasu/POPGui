using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

public class TitleBar : GUIElement
{
    private string _title;
    private SpriteFont _font;
    private Color _textColor;
    private Color _backgroundColor;
    private bool _isDragging;
    private Point _nextWindowPos;

    public TitleBar(Rectangle bounds, string title, Style style, GraphicsDevice graphicsDevice)
        : base(bounds, style, graphicsDevice)
    {
        _title = title;
        _font = style.Font;
        _textColor = style.TextColor ?? Color.White;
        _backgroundColor = style.BackgroundColor ?? Color.Black;
    }

    public override void Update(InputHandler inputHandler, Window window)
    {
        var mousePosition = inputHandler.mousePosition;
        var titleBarBounds = new Rectangle(window.Bounds.X, window.Bounds.Y, Bounds.Width, Bounds.Height);

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

                // Bounds = new Rectangle(mousePosition - _dragOffset, Bounds.Size);
                // window.MoveTo(new Point(_dragOffset.X - window.Bounds.X, _dragOffset.Y - window.Bounds.Y));
                Debug.WriteLine("Mouse   >> X: " + mousePosition.X + " | Y: " + mousePosition.Y);
                Debug.WriteLine("Offset  >> X: " + _nextWindowPos.X + " | Y: " + _nextWindowPos.Y);
                Debug.WriteLine("NextPos >> X: " + xPos + " | Y: " + yPos);
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

        Rectangle rect = new Rectangle(new Point(x, y), new Point(Bounds.Width, Bounds.Height));
        spriteBatch.Draw(BackgroundTexture, rect, _backgroundColor);

        Vector2 textSize = _font.MeasureString(_title);
        Vector2 textPosition = new Vector2(x + 7, y + 5);

        spriteBatch.DrawString(_font, _title, textPosition, _textColor);
    }
}
