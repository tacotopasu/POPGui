﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public class Header : GUIElement
{
    private string _text;
    private GraphicsDevice _graphicsDevice;
    private Color _backgroundColor;
    private SpriteFont _font;

    public Header(int height, string text, Style style, GraphicsDevice graphicsDevice) : base(new Rectangle(0, 0, 0, height), style, graphicsDevice)
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

        string finaloutput;
        int lines;
        (finaloutput, lines) = WrapText(_font, _text, window.Bounds.Width);

        float lineHeight = _font.MeasureString("A").Y;
        float totalTextHeight = lineHeight * lines;

        if (totalTextHeight > Bounds.Height)
        {
            Rectangle bounds = Bounds;
            bounds.Height = (int)totalTextHeight;
            Bounds = bounds;
        }

        Rectangle rect = new Rectangle(x, y, Bounds.Width, Bounds.Height);
        spriteBatch.Draw(BackgroundTexture, rect, _backgroundColor);

        if (Style.Font != null)
        {
            Vector2 textPosition = new Vector2(x, y + (Bounds.Height / 2) - (totalTextHeight / 2));
            Debug.WriteLine($"{finaloutput} | {y}");
            spriteBatch.DrawString(Style.Font, finaloutput, textPosition, Style.TextColor ?? Color.White);
        }
    }

    private (string, int) WrapText(SpriteFont font, string text, float maxLineWidth)
    {
        var lines = text.Split('\n');
        var wrappedText = new StringBuilder();
        int totalLines = 0;

        foreach (var line in lines)
        {
            var words = line.Split(' ');
            var wrappedLine = new StringBuilder();
            float lineWidth = 0;

            foreach (var word in words)
            {
                Vector2 size = font.MeasureString(word + " ");
                if (lineWidth + size.X < maxLineWidth)
                {
                    wrappedLine.Append(word + " ");
                    lineWidth += size.X;
                }
                else
                {
                    wrappedText.AppendLine(wrappedLine.ToString());
                    wrappedLine.Clear();
                    wrappedLine.Append(word + " ");
                    lineWidth = size.X;
                    totalLines++;
                }
            }

            wrappedText.AppendLine(wrappedLine.ToString());
            totalLines++;
        }

        return (wrappedText.ToString(), totalLines);
    }
}
