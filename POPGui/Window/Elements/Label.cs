using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

public class Label : GUIElement
{
    public string FormatString { get; set; }
    public Dictionary<string, object> Values { get; set; }
    public SpriteFont Font { get; set; }
    public Color? TextColor { get; set; }

    public Label(int height, string formatString, Style style, GraphicsDevice graphicsDevice)
        : base(new Rectangle(0, 0, 0, height), style, graphicsDevice)
    {
        FormatString = formatString;
        Font = Style.Font;
        TextColor = Style.TextColor;
    }

    public override void Update(InputHandler inputHandler, Window window)
    {
    }

    public override void Draw(SpriteBatch spriteBatch, Window window)
    {
        // Get the position of the label relative to the window
        int x = window.Bounds.X + Bounds.X;
        int y = window.Bounds.Y + Bounds.Y;

        string formattedText = FormatString;
        if (Values != null)
        {
            foreach (var kvp in Values)
            {
                formattedText = formattedText.Replace($"{{{kvp.Key}}}", kvp.Value?.ToString());
            }
        }

        string finaloutput;
        int lines;
        (finaloutput, lines) = WrapText(Font, formattedText, window.Bounds.Width);
        float lineHeight = Font.MeasureString("A").Y;
        float totalTextHeight = lineHeight * lines;

        if (totalTextHeight > Bounds.Height)
        {
            Rectangle bounds = Bounds;
            bounds.Height = (int)totalTextHeight;
            Bounds = bounds;
            window.layoutManager.UpdateLayout();
        }

        // Draw background
        Rectangle rect = new Rectangle(x, y, Bounds.Width, Bounds.Height);
        spriteBatch.Draw(BackgroundTexture, rect, Style.BackgroundColor ?? Color.Transparent);

        // Draw text
        if (Style.Font != null)
        {
            Vector2 textPosition = new Vector2(x, y);
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
