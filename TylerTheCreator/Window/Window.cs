using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

public class Window
{
    private Texture2D _backgroundTexture;
    private bool _isDragging;
    private Point _dragOffset;

    private List<GUIElement> _elements;
    private InputHandler _inputHandler;
    private GraphicsDevice _graphicsDevice;


    public Rectangle Bounds;
    public Style Style { get; set; }
    public string Title { get; set; }
    public bool IsDraggable { get; set; }

    public bool HasTitleBar { get; set; }

    public Window(GraphicsDevice graphicsDevice, InputHandler inputHandler, Point position, string title, Style style)
    {
        Style = style;
        Title = title;
        IsDraggable = true;
        HasTitleBar = true;

        Bounds = new Rectangle(position, Style.DefaultSize ?? new Point(150, 150));

        _backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
        _backgroundTexture.SetData(new Color[] { Style.BackgroundColor ?? Color.DarkGray });

        _graphicsDevice = graphicsDevice;
        _inputHandler = inputHandler;
        _elements = new List<GUIElement>();
    }

    public void Update(InputHandler inputHandler)
    {
        HandleUpdates(this);
    }

    protected virtual void HandleUpdates(Window window)
    {
        // When handling window updates, it's expected that _inputHandler has already been updated this cycle
        foreach (var element in _elements)
        {
            element.Update(_inputHandler, this);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Window window)
    {
        spriteBatch.Draw(_backgroundTexture, Bounds, Style.BackgroundColor ?? Color.Transparent);

        foreach (var element in _elements)
        {
            element.Draw(spriteBatch, window);
        }
    }

    public void MoveTo(Point position)
    {
        Bounds.X = position.X;
        Bounds.Y = position.Y;
    }

    protected void AddElement(GUIElement element)
    {
        _elements.Add(element);
    }

    protected void RemoveElement(GUIElement element)
    {
        _elements.Remove(element);
    }
}