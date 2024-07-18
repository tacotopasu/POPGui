using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

public class Window
{
    private Texture2D BackgroundTexture;
    protected Texture2D ColorTexture;
    protected bool _isDragging;
    protected Point _dragOffset;

    public LayoutManager layoutManager;
    protected EventHandler _eventHandler;
    protected InputHandler _inputHandler;
    protected GraphicsDevice _graphicsDevice;


    public Rectangle Bounds;
    public Style Style { get; set; }
    public string Title { get; set; }
    public bool IsDraggable { get; set; }

    public bool HasTitleBar { get; set; }

    public Window(GraphicsDevice graphicsDevice, EventHandler eventHandler, InputHandler inputHandler, Point position, string title, Style style)
    {
        Style = style;
        Title = title;
        IsDraggable = true;
        HasTitleBar = true;

        Bounds = new Rectangle(position, Style.DefaultSize ?? new Point(150, 150));

        BackgroundTexture = new Texture2D(graphicsDevice, 1, 1);
        ColorTexture = new Texture2D(graphicsDevice, 1, 1);
        BackgroundTexture.SetData(new Color[] { Style.BackgroundColor ?? Color.White });
        ColorTexture.SetData(new Color[] { Color.White });

        _graphicsDevice = graphicsDevice;
        _eventHandler = eventHandler;
        _inputHandler = inputHandler;
        layoutManager = new LayoutManager();
    }

    public virtual void Update(InputHandler inputHandler)
    {
        HandleUpdates(inputHandler, this);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        // Drawing window background
        spriteBatch.Draw(BackgroundTexture, Bounds, Style.BackgroundColor ?? Color.Transparent);

        // Drawing elements
        foreach (var element in layoutManager.Elements)
        {
            element.Draw(spriteBatch, this);
        }
    }

    public void MoveTo(Point position)
    {
        Bounds.X = position.X;
        Bounds.Y = position.Y;
    }

    protected virtual void HandleUpdates(InputHandler inputHandler, Window window)
    {
        // When handling window updates, it's expected that _inputHandler has already been updated this cycle
        foreach (var element in layoutManager.Elements)
        {
            element.Update(inputHandler, this);
        }

        layoutManager.UpdateLayout();
    }

    protected void AddElement(GUIElement element)
    {
        layoutManager.AddElement(element);
    }

    protected void RemoveElement(GUIElement element)
    {
        layoutManager.RemoveElement(element);
    }
}