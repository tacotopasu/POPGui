using System;
using System.Collections.Generic;

public class EventHandler
{
    private Dictionary<InputEventType, Action<InputEvent>> _eventHandlers;

    public EventHandler()
    {
        _eventHandlers = new Dictionary<InputEventType, Action<InputEvent>>();
    }

    public void RegisterEvent(InputEventType eventType, Action<InputEvent> handler)
    {
        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = handler;
        }
        else
        {
            _eventHandlers[eventType] += handler;
        }
    }

    public void UnregisterEvent(InputEventType eventType, Action<InputEvent> handler)
    {
        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] -= handler;
        }
    }

    public void HandleEvent(InputEvent inputEvent)
    {
        if (_eventHandlers.ContainsKey(inputEvent.EventType))
        {
            _eventHandlers[inputEvent.EventType]?.Invoke(inputEvent);
        }
    }
}
