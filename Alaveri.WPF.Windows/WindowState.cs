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
    /// <param name="window">The window to store.</param>
    public void StoreWindowState(Window window)
    {
        if (window.WindowState != WindowState.Minimized)
        {
            Position = new Point(window.Left, window.Top);
            Size = new Size(window.Width, window.Height);
            switch (window.WindowState)
            {
                case WindowState.Normal:
                    RestoredPosition = new Point(window.Left, window.Top);
                    RestoredSize = new Size(window.Width, window.Height);
                    WindowState = WindowState.Normal;
                    break;
                case WindowState.Maximized:
                    WindowState = WindowState.Maximized;
                    break;
                case WindowState.Minimized:
                    RestoredPosition = new Point(window.Left, window.Top);
                    RestoredSize = new Size(window.Width, window.Height);
                    WindowState = WindowState.Normal;
                    break;
            }
        }
    }

    /// <summary>
    /// Restrores the Window state.
    /// </summary>
    /// <param name="window">The window to restore.</param>
    public void RestoreWindowState(Window window)
    {
        if (Position.X != int.MaxValue && Position.Y != int.MaxValue)
        {
            window.Left = Position.X;
            window.Top = Position.Y;
        }
        window.Width = Size.Width;
        window.Height = Size.Height;
        if (WindowState == WindowState.Maximized)
            window.WindowState = WindowState.Maximized;
    }

    /// <summary>
    /// Initializes a new instance of the WindowState class using the specified Window and initial size.
    /// </summary>
    public StoredWindowState(Size initialSize = default)
    {
        Size = initialSize;
    }

    /// <summary>
    /// Initializes a new instance of the WindowState class.
    /// </summary>
    public StoredWindowState()
    {
    }
}
