using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
#if WINDOWS
using Windows.Graphics;
using Microsoft.UI.Windowing;
#endif

namespace ACL.Maui.Windows
{
    /// <summary>
    /// Represents the state of a Form, including position, location and WindowState.
    /// </summary>
    public class WindowState
    {
#if WINDOWS
        /// <summary>
        /// The window linked to this state.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public AppWindow Window { get; set; }

        /// <summary>
        /// The location of the window.
        /// </summary>
#pragma warning disable CA1416 // Validate platform compatibility
        public PointInt32 Position { get; set; } = new PointInt32(int.MaxValue, int.MaxValue);
#pragma warning restore CA1416 // Validate platform compatibility

        /// <summary>
        /// The size of the window.
        /// </summary>
        public SizeInt32 Size { get; set; }

        /// <summary>
        /// The location of the window when not maximized.
        /// </summary>
        public PointInt32 RestoredPosition { get; set; }

        /// <summary>
        /// The size of the window.
        /// </summary>
        public SizeInt32 RestoredSize { get; set; }

        /// <summary>
        /// The form's window state.
        /// </summary>
        public OverlappedPresenterState State { get; set; } = OverlappedPresenterState.Restored;
#endif

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
            var presenter = Window.Presenter as OverlappedPresenter;
            if (presenter == null)
                return;
            Position = Window.Position;
            Size = Window.Size;
            switch (presenter.State)
            {
                case OverlappedPresenterState.Restored:
                    RestoredPosition = Window.Position;
                    RestoredSize = Window.Size;
                    State = OverlappedPresenterState.Restored;
                    break;
                case OverlappedPresenterState.Maximized:
                    State = OverlappedPresenterState.Maximized;
                    break;
                case OverlappedPresenterState.Minimized:
                    RestoredPosition = Window.Position;
                    RestoredSize = Window.Size;
                    State = OverlappedPresenterState.Restored;
                    break;
            }
#endif
        }

        /// <summary>
        /// Restrores the Window state from the WindowState.
        /// </summary>
        public void RestoreWindowState()
        {
#if WINDOWS
            var presenter = Window.Presenter as OverlappedPresenter;
            if (presenter == null)
                return;
#pragma warning disable CA1416 // Validate platform compatibility
            if (Position.X != int.MaxValue && Position.Y != int.MaxValue)
                Window.Move(Position);
#pragma warning restore CA1416 // Validate platform compatibility
            Window.Resize(Size);
            if (State == OverlappedPresenterState.Maximized)
                presenter.Maximize();
#endif
        }

#if WINDOWS
        /// <summary>
        /// Initializes a new instance of the WindowState class using the specified AppWindow and initial size.
        /// </summary>
        /// <param name="window">The window to link to this state.</param>
        public WindowState(AppWindow window, SizeInt32 initialSize = default)
        {
            Window = window;
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
