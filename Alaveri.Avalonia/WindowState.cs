using Avalonia;
using Avalonia.Controls;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Alaveri.Avalonia
{
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
        public PixelPoint Position { get; set; } = new PixelPoint(int.MaxValue, int.MaxValue);

        /// <summary>
        /// The size of the window.
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// The location of the window when not maximized.
        /// </summary>
        public PixelPoint RestoredPosition { get; set; }

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

        public void StoreWindowState(double width, double height, PixelPoint position)
        {
            if (Window.WindowState != WindowState.Minimized)
            {
                Position = position;
                Size = new Size(width, height);
                switch (Window.WindowState)
                {
                    case WindowState.Normal:
                        RestoredPosition = position;
                        RestoredSize = new Size(width, height);
                        WindowState = WindowState.Normal;
                        break;
                    case WindowState.Maximized:
                        WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Minimized:
                        RestoredPosition = position;
                        RestoredSize = new Size(width, height);
                        WindowState = WindowState.Normal;
                        break;
                }
            }
        }

        /// <summary>
        /// Stores the Window's state.
        /// </summary>
        public void StoreWindowState()
        {
            if (Window.WindowState != WindowState.Minimized)
            {
                Position = Window.Position;
                Size = new Size(Window.Width, Window.Height);
                switch (Window.WindowState)
                {
                    case WindowState.Normal:
                        RestoredPosition = Window.Position;
                        RestoredSize = new Size(Window.Width, Window.Height);
                        WindowState = WindowState.Normal;
                        break;
                    case WindowState.Maximized:
                        WindowState = WindowState.Maximized;
                        break;
                    case WindowState.Minimized:
                        RestoredPosition = Window.Position;
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
            if (WindowState == WindowState.Maximized)
            {
                Window.WindowState = WindowState.Maximized;
            }
            else
            {
                if (Position.X != int.MaxValue && Position.Y != int.MaxValue)
                    Window.Position = Position;
                Window.Width = Size.Width;
                Window.Height = Size.Height;
            }
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

}