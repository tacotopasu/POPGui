using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

public class LayoutManager
{
    private List<GUIElement> _elements;
    private int _currentY;
    private int _offsetX;
    private int _yPadding;
    public List<GUIElement> Elements { get { return _elements; } }

    public LayoutManager()
    {
        _elements = new List<GUIElement>();
        _offsetX = 7;
        _yPadding = 5;
        _currentY = 0;
    }

    public LayoutManager(int xOffset, int yPadding)
    {
        _elements = new List<GUIElement>();
        _offsetX = xOffset;
        _yPadding = yPadding;
        _currentY = 0;
    }

    public void AddElement(GUIElement element)
    {
        // offsetting it horizontally here sucks, but it's the simplest solution for now
        if (element.GetType() != typeof(TitleBar))
            element.Bounds = new Rectangle(element.Bounds.X + _offsetX, _currentY, element.Bounds.Width, element.Bounds.Height);

        _elements.Add(element);
        UpdateLayout();
    }

    public void RemoveElement(GUIElement element)
    {
        _elements.Remove(element);
        UpdateLayout();
    }

    private void UpdateElementPosition(GUIElement element)
    {
        element.Bounds = new Rectangle(element.Bounds.X, _currentY, element.Bounds.Width, element.Bounds.Height);
        _currentY = _currentY + element.Bounds.Height + _yPadding;
    }

    public void Clear()
    {
        _elements.Clear();
        _currentY = 0;
    }

    public void UpdateLayout()
    {
        _currentY = 0;
        foreach (var element in _elements)
        {
                UpdateElementPosition(element);
        }
    }
}