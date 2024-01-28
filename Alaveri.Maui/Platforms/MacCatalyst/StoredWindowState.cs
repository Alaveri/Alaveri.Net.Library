using System.Text.Json.Serialization;

namespace Alaveri.Maui
{
    public partial class StoredWindowState
    {
        /// <summary>
        /// Restores the state of the window from the configuration.
        /// </summary>
        public void RestoreWindowState()
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

        public void StoreWindowState()
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