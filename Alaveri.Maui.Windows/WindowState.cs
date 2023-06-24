using Newtonsoft.Json;
using System.Xml.Serialization;
using Microsoft.UI.Windowing;

namespace Alaveri.Maui.Windows
{
    /// <summary>
    /// Represents the state of a window, including position, location and state.
    /// </summary>
    public class WindowState
    {
        /// <summary>
        /// Gets or sets the window.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public Window Window { get; set; }

        /// <summary>
        /// Gets or sets the application window.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]        
        public AppWindow AppWindow { get; set; }

        /// <summary>
        /// Gets or sets the window's position.
        /// </summary>
        public (double X, double Y) Position { get; set; } = (int.MaxValue, int.MaxValue);

        /// <summary>
        /// The size of the window.
        /// </summary>
        public (double Width, double Height) Size { get; set; }

        /// <summary>
        /// The location of the window when not maximized.
        /// </summary>
        public (double X, double Y) RestoredPosition { get; set; }

        /// <summary>
        /// The size of the window.
        /// </summary>
        public (double Width, double Height) RestoredSize { get; set; }

        /// <summary>
        /// True if the window is maximized.
        /// </summary>
        public bool Maximized { get; set; } = false;

        /// <summary>
        /// True if this state has been initialized.
        /// </summary>
        public bool Initialized { get; set; }

        /// <summary>
        /// Stores the Window's state in the WindowState
        /// </summary>
        public void StoreWindowState()
        {
#if WINDOWS
#pragma warning disable CA1416 // Validate platform compatibility

            Position = (Window.X, Window.Y);
            Size = (Window.Width, Window.Height);
            var presenter = AppWindow.Presenter as OverlappedPresenter;
            if (presenter == null)
                return;
            Maximized = presenter.State == OverlappedPresenterState.Maximized;
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        }

        /// <summary>
        /// Restrores the Window state from the WindowState.
        /// </summary>
        public void RestoreWindowState()
        {
#if WINDOWS
#pragma warning disable CA1416 // Validate platform compatibility
            var presenter = AppWindow.Presenter as OverlappedPresenter;
            if (presenter == null)
                return;
            if (Position.X != int.MaxValue && Position.Y != int.MaxValue)
            {
                Window.X = Position.X;
                Window.Y = Position.Y;
            }
            Window.Width = Size.Width;
            Window.Height = Size.Height;

            if (Maximized)
                presenter.Maximize();
#pragma warning restore CA1416 // Validate platform compatibility
#endif
        }

#if WINDOWS
        /// <summary>
        /// Initializes a new instance of the WindowState class using the specified AppWindow and initial size.
        /// </summary>
        /// <param name="window">The window to link to this state.</param>
        public WindowState(Window window, AppWindow appWindow, (double X, double Y) initialSize = default)
        {
            Window = window;
            AppWindow = appWindow;
            Size = initialSize;
        }
#endif

        /// <summary>
        /// Initializes a new instance of the WindowState class.
        /// </summary>
        public WindowState()
        {
        }
    }
}
