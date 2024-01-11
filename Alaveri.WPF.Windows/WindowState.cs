using Newtonsoft.Json;
using System.Windows;
using System.Xml.Serialization;

namespace Alaveri.Wpf.Windows;

/// <summary>
/// Represents the state of a Window, including position, location and WindowState.
/// </summary>
public class StoredWindowState
{
    /// <summary>
    /// The window linked to this state.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public Window Window { get; set; }

    /// <summary>
    /// The location of the window.
    /// </summary>
    public Point Position { get; set; } = new Point(int.MaxValue, int.MaxValue);

    /// <summary>
    /// The size of the window.
    /// </summary>
    public Size Size { get; set; }

    /// <summary>
    /// The location of the window when not maximized.
    /// </summary>
    public Point RestoredPosition { get; set; }

    /// <summary>
    /// The size of the window.
    /// </summary>
    public Size RestoredSize { get; set; }

    /// <summary>
    /// The window's state.
    /// </summary>
    public WindowState WindowState { get; set; }

    /// <summary>
    /// True if this state has been initialized.
    /// </summary>
    public bool Initialized { get; set; }

    /// <summary>
    /// Stores the Window's state.
    /// </summary>
    public void StoreWindowState()
    {
        if (Window.WindowState != WindowState.Minimized)
        {
            Position = new Point(Window.Left, Window.Top);
            Size = new Size(Window.Width, Window.Height);
            switch (Window.WindowState)
            {
                case WindowState.Normal:
                    RestoredPosition = new Point(Window.Left, Window.Top);
                    RestoredSize = new Size(Window.Width, Window.Height);
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Maximized;
                    break;
                case WindowState.Minimized:
                    RestoredPosition = new Point(Window.Left, Window.Top);
                    RestoredSize = new Size(Window.Width, Window.Height);
                    WindowState = WindowState.Normal;
                    break;
            }
        }
    }

    /// <summary>
    /// Restrores the Window state.
    /// </summary>
    public void RestoreWindowState()
    {
        if (Position.X != int.MaxValue && Position.Y != int.MaxValue)
        {
            Window.Left = Position.X;
            Window.Top = Position.Y;
        }
        Window.Width = Size.Width;
        Window.Height = Size.Height;
        if (WindowState == WindowState.Maximized)
            Window.WindowState = WindowState.Maximized;
    }

    /// <summary>
    /// Initializes a new instance of the WindowState class using the specified Window and initial size.
    /// </summary>
    /// <param name="window">The window to link to this state.</param>
    public StoredWindowState(Window window, Size initialSize = default)
    {
        Window = window;
        Size = initialSize;
    }

    /// <summary>
    /// Initializes a new instance of the WindowState class.
    /// </summary>
    public StoredWindowState()
    {
    }
}
