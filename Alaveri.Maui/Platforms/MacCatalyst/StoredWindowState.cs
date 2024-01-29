using System.Text.Json.Serialization;

namespace Alaveri.Maui
{
    /// <summary>
    /// Contains platform-specific stored window state information.
    /// </summary>
    /// <param name="initialWidth">The initial width of the window.</param>
    /// <param name="initialHeight">The initial height of the window.</param>
    public class StoredWindowState(double initialWidth, double initialHeight) : BaseStoredWindowState(initialWidth, initialHeight), IStoredWindowState
    {
        /// <summary>
        /// Restores the state of the window from the configuration.
        /// </summary>
        public override void RestoreWindowState()
        {
            if (Window == null)
                return;
        
            if (X != null)
                Window.X = X.Value;
            if (Y != null)
                Window.Y = Y.Value;
            Window.Width = Width;
            Window.Height = Height;
        }

        public override void StoreWindowState()
        {
            if (Window == null)
                return;
            Maximized = false;
            X = Window.X;
            Y = Window.Y;
            Width = Window.Width;
            Height = Window.Height;
        }

    }
}