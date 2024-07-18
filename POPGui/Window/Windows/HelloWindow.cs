using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class HelloWindow : Window
{
    private int posX = 100;
    private int posY = 100;
    public int moveSpeed = 5;
    private int count = 0;

    private Label posLabel;
    private Label countLabel;

    private List<Square> _squares;
    private Texture2D _squareTexture;
    private GraphicsDeviceManager _graphics;

    public HelloWindow(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, EventHandler eventHandler, InputHandler inputHandler, Point position, string windowTitle, Style style) :
        base(graphicsDevice, eventHandler, inputHandler, position, windowTitle, style)
    {
        _graphics = graphics;

        _squares = new List<Square>();
        InitializeSquares(graphicsDevice);

        _eventHandler = eventHandler;
        _eventHandler.RegisterEvent(InputEventType.KeyDown, OnKeyDown);

        // Style.DefaultSize = new Point(100, 100);
        Style.BackgroundColor = new Color(0f, 0f, 0f, 0.25f);

        Bounds.Width = 300;
        Bounds.Height = 300;

        var titleBarStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Purple,
            TextColor = Color.White
        };

        var headerStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Transparent,
            TextColor = Color.White
        };

        var buttonStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Purple,
            TextColor = Color.White
        };


        TitleBar titleBar = new TitleBar(28, "Debug", titleBarStyle, graphicsDevice);
        AddElement(titleBar);

        Header actions = new Header(28, "Actions", headerStyle, graphicsDevice);
        AddElement(actions);

        Button resetPos = new Button(new Point(40, 10), "Reset Pos", buttonStyle, graphicsDevice, ResetPos);
        AddElement(resetPos);

        Button addSquare = new Button(new Point(40, 10), "Add Square", buttonStyle, graphicsDevice, AddSquare);
        AddElement(addSquare);

        Button removeSquare = new Button(new Point(40, 10), "Remove Square", buttonStyle, graphicsDevice, RemoveSquare);
        AddElement(removeSquare);

        Button clearSquares = new Button(new Point(40, 10), "Clear Squares", buttonStyle, graphicsDevice, ClearSquares);
        AddElement(clearSquares);

        Header debug = new Header(40, "Debug", headerStyle, graphicsDevice);
        AddElement(debug);

        posLabel = new Label(20, "X : {x}\nY : {y}", headerStyle, graphicsDevice);
        AddElement(posLabel);
        
        countLabel = new Label(20, "Squares : {count}", headerStyle, graphicsDevice);
        AddElement(countLabel);

        posLabel.Values = new Dictionary<string, object>
        {
            { "x", posX },
            { "y", posY }
        };
        countLabel.Values = new Dictionary<string, object>
        {
            { "count", count }
        };

        
    }

    private void InitializeSquares(GraphicsDevice graphicsDevice)
    {
        _squareTexture = new Texture2D(graphicsDevice, 1, 1);
        _squareTexture.SetData(new[] { Color.White });

        Random random = new Random();
        for (int i = 0; i < random.Next(10); i++)
        {
            AddSquare();
        }
    }

    private void ResetPos() {
        posX = 100;
        posY = 100;
    }

    private void AddSquare()
    {
        Random random = new Random();
        int size = 40;
        int x = random.Next(_graphics.PreferredBackBufferWidth - size);
        int y = random.Next(_graphics.PreferredBackBufferHeight - size);
        int velocityX = moveSpeed * (random.Next(2) == 0 ? 1 : -1);
        int velocityY = moveSpeed * (random.Next(2) == 0 ? 1 : -1);
        Color color = new Color(random.Next(256), random.Next(256), random.Next(256));

        _squares.Add(new Square(new Rectangle(x, y, size, size), color, new Point(velocityX, velocityY)));
        count++;
    }

    private void RemoveSquare()
    {
        if (_squares.Count == 0) return;

        _squares.RemoveAt(0);
        count--;
    }

    private void ClearSquares()
    {
        _squares = new List<Square>();
        count = 0;
    }

    public override void Update(InputHandler inputHandler)
    {
        foreach (var square in _squares)
        {
            square.Update(new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
        }
        base.Update(inputHandler);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(ColorTexture, new Rectangle(posX, posY, 100, 100), Color.HotPink);

        posLabel.Values = new Dictionary<string, object>
        {
            { "x", posX },
            { "y", posY }
        };

        countLabel.Values = new Dictionary<string, object>
        {
            { "count", count }
        };

        foreach (var square in _squares)
        {
            square.Draw(spriteBatch, _squareTexture);
        }

        base.Draw(spriteBatch);
    }

    private void OnKeyDown(InputEvent inputEvent)
    {
        foreach(Keys key in inputEvent.Keys)
        {
            switch (key)
            {
                case Keys.W:
                    posY -= moveSpeed;
                    break;
                case Keys.S:
                    posY += moveSpeed;
                    break;
                case Keys.A:
                    posX -= moveSpeed;
                    break;
                case Keys.D:
                    posX += moveSpeed;
                    break;
                default:
                    // Handle other keys if needed
                    break;
            }
        }
        
    }
}

public class Square
{
    public Rectangle Bounds;
    public Color Color;
    public Point Velocity;

    public Square(Rectangle bounds, Color color, Point velocity)
    {
        Bounds = bounds;
        Color = color;
        Velocity = velocity;
    }

    public void Update(Rectangle windowBounds)
    {
        // Move the square
        Bounds = new Rectangle(Bounds.Location + Velocity, Bounds.Size);

        // Check for collisions with the window edges
        if (Bounds.Left <= windowBounds.Left || Bounds.Right >= windowBounds.Right)
        {
            Velocity = new Point(-Velocity.X, Velocity.Y);
        }
        if (Bounds.Top <= windowBounds.Top || Bounds.Bottom >= windowBounds.Bottom)
        {
            Velocity = new Point(Velocity.X, -Velocity.Y);
        }
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Bounds, Color);
    }
}