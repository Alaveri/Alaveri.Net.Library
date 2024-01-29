namespace Alaveri.Maui.Windows
{
    /// <summary>
    /// Represents the state of a window, including position, location and state.
    /// </summary>
    public class StoredWindowState
    {
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
        /// Initializes a new instance of the WindowState class.
        /// </summary>
        public StoredWindowState((double X, double Y) initialSize = default)
        {
            Size = initialSize;
        }
    }
}
