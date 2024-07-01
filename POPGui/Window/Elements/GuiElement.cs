using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GUIElement
{
    protected Style Style { get; set; }

    public Rectangle Bounds { get; set; }

    protected Texture2D BackgroundTexture;

    public GUIElement(Rectangle bounds, Style style, GraphicsDevice graphicsDevice)
    {
        Bounds = bounds;
        Style = style;
        BackgroundTexture = CreateBackgroundTexture(graphicsDevice);
    }

    public abstract void Update(InputHandler inputHandler, Window window);

    public abstract void Draw(SpriteBatch spriteBatch, Window window);

    protected Texture2D CreateBackgroundTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new Color[] { Color.White });
        return texture;
    }
}
