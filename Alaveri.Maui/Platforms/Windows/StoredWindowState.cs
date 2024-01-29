using Microsoft.UI;
using Microsoft.UI.Windowing;
using Newtonsoft.Json;

namespace Alaveri.Maui
{ 
    /// <summary>
    /// Contains platform-specific stored window state information.
    /// </summary>
    /// <param name="initialWidth">The initial width of the window.</param>
    /// <param name="initialHeight">The initial height of the window.</param>
    public class StoredWindowState(double initialWidth, double initialHeight) : BaseStoredWindowState(initialWidth, initialHeight), IStoredWindowState
    {
        [JsonIgnore]
        public AppWindow? AppWindow { get; set; }

        /// <summary>
        /// Restores the state of the window.
        /// </summary>
        public override void RestoreWindowState()
        {
            if (Window == null)
                return;
            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow window = AppWindow.GetFromWindowId(windowId);
                AppWindow = window;
                if (Maximized && window?.Presenter != null && window.Presenter is OverlappedPresenter presenter)
                {
                    Window.X = RestoredX;
                    Window.Y = RestoredY;
                    Window.Width = RestoredWidth;
                    Window.Height = RestoredHeight;
                    presenter.Maximize();
                }
                else
                {
                    if (X != null)
                        Window.X = X.Value;
                    if (Y != null)
                        Window.Y = Y.Value;
                    Window.Width = Width;
                    Window.Height = Height;
                }
            });
        }

        /// <summary>
        /// Stores the state of the window.
        /// </summary>
        public override void StoreWindowState()
        {
            if (AppWindow == null || Window == null)
                return;
            Maximized = false;
            if (AppWindow?.Presenter != null && AppWindow.Presenter is OverlappedPresenter presenter)
            {
                switch (presenter.State)
                {
                    case OverlappedPresenterState.Minimized:
                        break;
                    case OverlappedPresenterState.Restored:
                        X = Window.X;
                        Y = Window.Y;
                        Width = Window.Width;
                        Height = Window.Height;
                        RestoredX = Window.X;
                        RestoredY = Window.Y;
                        RestoredWidth = Window.Width;
                        RestoredHeight = Window.Height;
                        break;
                    case OverlappedPresenterState.Maximized:
                        Maximized = true;
                        break;
                }
            }

        }
    }
}