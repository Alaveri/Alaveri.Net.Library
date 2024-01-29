
using Newtonsoft.Json;

namespace Alaveri.Maui
{
    public interface IStoredWindowState
    {
        /// <summary>
        /// The window bound to this state.
        /// </summary>
        [JsonIgnore]
        Window? Window { get; set; }

        /// <summary>
        /// Gets or sets the window's X position.
        /// </summary>
        double? X { get; set; }

        /// <summary>
        /// Gets or sets the window's Y position.
        /// </summary>
        double? Y { get; set; }

        /// <summary>
        /// Gets or sets the window's width.
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the window's height.
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// Gets or sets the window's X position when not maximized.
        /// </summary>
        double RestoredX { get; set; }

        /// <summary>
        /// Gets or sets the window's Y position when not maximized.
        /// </summary>
        double RestoredY { get; set; }

        /// <summary>
        /// Gets or sets the window's width when not maximized.
        /// </summary>
        double RestoredWidth { get; set; }

        /// <summary>
        /// Gets or sets the window's height when not maximized.
        /// </summary>
        double RestoredHeight { get; set; }

        /// <summary>
        /// True if the window is maximized.
        /// </summary>
        bool Maximized { get; set; }

        /// <summary>
        /// Restores the state of the window.
        /// </summary>
        void RestoreWindowState();

        /// <summary>
        /// Stores the state of the window.
        /// </summary>
        void StoreWindowState();
    }
}