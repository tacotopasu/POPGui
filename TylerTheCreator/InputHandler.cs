using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


public class InputHandler
{
    private KeyboardState _currentKeyboardState;
    private KeyboardState _previousKeyboardState;
    private MouseState _currentMouseState;
    private MouseState _previousMouseState;

    public MouseState mouseState;
    public Point mousePosition;

    public event Action<InputEvent> OnInputEvent;

    public void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();

        _previousMouseState = _currentMouseState;
        _currentMouseState = Mouse.GetState();

        mouseState = _currentMouseState;
        mousePosition = new Point(mouseState.X, mouseState.Y);

        CheckKeyboardEvents();
        CheckMouseEvents();
    }

    private void CheckKeyboardKeyState
        (KeyboardState currentState, KeyboardState previousState, Keys key, List<Keys> pressedKeys, List<Keys> releasedKeys)
    {
        if (currentState.IsKeyDown(key) && previousState.IsKeyUp(key))
        {
            pressedKeys.Add(key);
        }
        else if (currentState.IsKeyUp(key) && previousState.IsKeyDown(key))
        {
            releasedKeys.Add(key);
        }
    }

    private void CheckKeyboardEvents()
    {
        List<Keys> pressedKeys = new List<Keys>();
        List<Keys> releasedKeys = new List<Keys>();

        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            CheckKeyboardKeyState(_currentKeyboardState, _previousKeyboardState, key, pressedKeys, releasedKeys);
        }

        if (pressedKeys.Count > 0)
        {
            OnInputEvent?.Invoke(new InputEvent(InputEventType.KeyPressed, pressedKeys));
        }

        if (releasedKeys.Count > 0)
        {
            OnInputEvent?.Invoke(new InputEvent(InputEventType.KeyReleased, releasedKeys));
        }
    }

    private void CheckMouseButtonState
        (ButtonState currentState, ButtonState previousState, MouseButtons button, List<MouseButtons> pressedButtons, List<MouseButtons> releasedButtons)
    {
        if (currentState == ButtonState.Pressed && previousState == ButtonState.Released)
        {
            pressedButtons.Add(button);
        }
        else if (currentState == ButtonState.Released && previousState == ButtonState.Pressed)
        {
            releasedButtons.Add(button);
        }
    }

    private void CheckMouseEvents()
    {
        List<MouseButtons> pressedButtons = new List<MouseButtons>();
        List<MouseButtons> releasedButtons = new List<MouseButtons>();

        CheckMouseButtonState(_currentMouseState.LeftButton, _previousMouseState.LeftButton, MouseButtons.Left, pressedButtons, releasedButtons);
        CheckMouseButtonState(_currentMouseState.RightButton, _previousMouseState.RightButton, MouseButtons.Right, pressedButtons, releasedButtons);
        CheckMouseButtonState(_currentMouseState.MiddleButton, _previousMouseState.MiddleButton, MouseButtons.Middle, pressedButtons, releasedButtons);
        CheckMouseButtonState(_currentMouseState.XButton1, _previousMouseState.XButton1, MouseButtons.XButton1, pressedButtons, releasedButtons);
        CheckMouseButtonState(_currentMouseState.XButton2, _previousMouseState.XButton2, MouseButtons.XButton2, pressedButtons, releasedButtons);

        if (pressedButtons.Count > 0)
        {
            OnInputEvent?.Invoke(new InputEvent(InputEventType.MouseButtonPressed, null, pressedButtons));
        }

        if (releasedButtons.Count > 0)
        {
            OnInputEvent?.Invoke(new InputEvent(InputEventType.MouseButtonReleased, null, releasedButtons));
        }
    }

    public bool IsKeyDown(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return _currentKeyboardState.IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
    }


    public bool IsMouseLeftDown()
    {
        return _currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool IsMouseLeftPressed()
    {
        return _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool IsMouseLeftReleased()
    {
        return !IsMouseLeftDown() && _previousMouseState.LeftButton == ButtonState.Pressed;
    }


    public bool IsMouseRightDown()
    {
        return _currentMouseState.RightButton == ButtonState.Pressed;
    }

    public bool IsMouseRightPressed()
    {
        return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
    }

    public bool IsMouseRightReleased()
    {
        return !IsMouseRightDown() && _previousMouseState.RightButton == ButtonState.Pressed;
    }
}
