using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

public class Button : GUIElement
{
    public string Text { get; set; }
    public SpriteFont Font { get; set; }
    public Color? TextColor { get; set; }
    public Color? BackgroundColor { get; set; }
    public Color? HoverColor { get; set; }
    public Action OnClick { get; set; }
    private bool _isHovered;
    private bool _isPressed;
    public int padding = 6;

    public Button(Point size, string text, Style style, GraphicsDevice graphicsDevice, Action onClick)
        : base(new Rectangle(0, 0, size.X, size.Y), style, graphicsDevice)
    {
        Text = text;
        Font = Style.Font;
        TextColor = Style.TextColor;
        BackgroundColor = Style.BackgroundColor; // Background color, used for Button color
        var mult = 1.5f;
        var offset = 0.15f;
        HoverColor = new Color((BackgroundColor.Value.R + offset) * mult, (BackgroundColor.Value.G + offset) * mult, (BackgroundColor.Value.B + offset) * mult);
        OnClick = onClick;

        if(Bounds.Height < Font.MeasureString(Text).Y)
            Bounds = new Rectangle(Bounds.X, Bounds.Y, (int)Font.MeasureString(Text).X + 2, (int)Font.MeasureString(Text).Y + 4);
    }

    public override void Update(InputHandler inputHandler, Window window)
    {
        int x = window.Bounds.X + Bounds.X;
        int y = window.Bounds.Y + Bounds.Y;
        var textSize = Font.MeasureString(Text);

        var mousePosition = inputHandler.mousePosition;
        _isHovered = new Rectangle(x, y, (int)textSize.X + padding, Bounds.Height).Contains(mousePosition);

        if (_isHovered && inputHandler.IsMouseLeftPressed())
        {
            _isPressed = true;
        }
        else if (_isPressed && inputHandler.IsMouseLeftReleased())
        {
            _isPressed = false;
            OnClick.Invoke();
        }
    }

    public override void Draw(SpriteBatch spriteBatch, Window window)
    {
        int x = window.Bounds.X + Bounds.X;
        int y = window.Bounds.Y + Bounds.Y;

        var textSize = Font.MeasureString(Text);
        Rectangle rect = new Rectangle(x, y, (int)textSize.X + padding, Bounds.Height);
        var textPosition = new Vector2(x + padding / 2, y + Bounds.Height / 2 - (int)textSize.Y / 2);
        var buttonColor = _isHovered ? HoverColor : BackgroundColor;

        // Button
        spriteBatch.Draw(BackgroundTexture, rect, (Color)buttonColor);

        // Label
        spriteBatch.DrawString(Font, Text, textPosition, (Color)TextColor);
    }
}
