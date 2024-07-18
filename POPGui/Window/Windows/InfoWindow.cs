using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class InfoWindow : Window
{
    private string linkString;

    public InfoWindow(GraphicsDevice graphicsDevice, EventHandler eventHandler, InputHandler inputHandler, Point position, string windowTitle, Style style) :
        base(graphicsDevice, eventHandler, inputHandler, position, windowTitle, style)
    {

        // Style.DefaultSize = new Point(100, 100);
        Style.BackgroundColor = new Color(0f, 0f, 0f, 0.25f);

        Bounds.Width = 300;
        Bounds.Height = 133;

        var titleBarStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.LightPink,
            TextColor = Color.Black
        };

        var headerStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Transparent,
            TextColor = Color.White
        };

        var linkStyle = new Style
        {
            Font = Style.Font,
            BackgroundColor = Color.Transparent,
            TextColor = Color.LightPink
        };


        TitleBar titleBar = new TitleBar(28, "Info", titleBarStyle, graphicsDevice);
        AddElement(titleBar);

        string introString = "Welcome to my engine test!\nIt's very basic for now but I've learned a lot and am very happy and proud with the current product! Check it out at :";
        Header intro = new Header(0, introString, headerStyle, graphicsDevice);
        AddElement(intro);

        linkString = "https://github.com/tacotopasu/POPGui";
        Button link = new Button(new Point(0, 0), linkString, linkStyle, graphicsDevice, OpenLink);
        AddElement(link);
    }

    private void OpenLink()
    {
        try
        {
            // Use ProcessStartInfo class to start browser
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = linkString,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening link: {ex.Message}");
        }
    }
}