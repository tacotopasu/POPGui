using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public enum InputEventType
{
    KeyPressed,
    KeyReleased,
    MouseButtonPressed,
    MouseButtonReleased,
}

public enum MouseButtons
{
    Left,
    Right,
    Middle,
    XButton1,
    XButton2
}

public class InputEvent
{
    public InputEventType EventType { get; }
    public List<Keys>? Keys { get; }
    public List<MouseButtons>? MouseButtons { get; }

    public InputEvent(InputEventType eventType, List<Keys>? keys = null, List<MouseButtons>? mouseButtons = null)
    {
        EventType = eventType;
        Keys = keys;
        MouseButtons = mouseButtons;
    }
}
