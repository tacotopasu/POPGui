﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace POP
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _rectangleTexture;
        private InputHandler _inputHandler;
        private EventHandler _eventHandler;

        private Window _mainWindow;
        private Window _infoWindow;
        private SpriteFont _defaultFont;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _inputHandler = new InputHandler();
            _eventHandler = new EventHandler();
        }

        protected override void Initialize()
        {
            _inputHandler.OnInputEvent += _eventHandler.HandleEvent;

            _eventHandler.RegisterEvent(InputEventType.KeyPressed, OnKeyPressed);
            _eventHandler.RegisterEvent(InputEventType.KeyReleased, OnKeyReleased);
            _eventHandler.RegisterEvent(InputEventType.MouseButtonPressed, OnMouseButtonPressed);
            _eventHandler.RegisterEvent(InputEventType.MouseButtonReleased, OnMouseButtonReleased);

            _defaultFont = Content.Load<SpriteFont>("Fonts\\Default");

            var mainWindowStyle = new Style
            {
                Font = _defaultFont,
                DefaultSize = new Point(200, 250),
                BackgroundColor = Color.Black,
                TextColor = Color.White
            };

            var infoWindowStyle = new Style
            {
                Font = _defaultFont,
                DefaultSize = new Point(200, 250),
                BackgroundColor = Color.Black,
                TextColor = Color.White
            };

            _mainWindow = new HelloWindow(GraphicsDevice, _graphics, _eventHandler, _inputHandler, new Point(675, 35), "Debug", mainWindowStyle);
            _infoWindow = new InfoWindow(GraphicsDevice, _eventHandler, _inputHandler, new Point(35, 625), "Info", infoWindowStyle);

            SetWindowSize(1000, 800);
            base.Initialize();
        }

        private void SetWindowSize(int width, int height)
        {
            _graphics.PreferredBackBufferWidth = width;
            _graphics.PreferredBackBufferHeight = height;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _rectangleTexture = FillTexture(25, 25, Color.HotPink);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _inputHandler.Update();
            _mainWindow.Update(_inputHandler);
            _infoWindow.Update(_inputHandler);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            // _spriteBatch.Draw(_rectangleTexture, new Rectangle(50, 50, 100, 100), Color.White);
            _mainWindow.Draw(_spriteBatch);
            _infoWindow.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Texture2D FillTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, width, height);
            int pixelCount = width * height;
            Color[] data = new Color[pixelCount];

            for (int i = 0; i < pixelCount; i++)
                data[i] = color;

            texture.SetData(data);

            return texture;
        }

        // Event Handlers

        private void OnKeyPressed(InputEvent inputEvent)
        {

        }

        private void OnKeyReleased(InputEvent inputEvent)
        {

        }

        private void OnMouseButtonPressed(InputEvent inputEvent)
        {
            
        }

        private void OnMouseButtonReleased(InputEvent inputEvent)
        {
            
        }
    }
}