using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Text.Json.Serialization;

namespace Alaveri.Maui
{
    public partial class StoredWindowState
    {
        public AppWindow? AppWindow { get; set; }

        /// <summary>
        /// Restores the state of the window from the configuration.
        /// </summary>
        public void RestoreWindowState()
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

        public void StoreWindowState()
        {
            if (AppWindow == null || Window == null)
                return;
            Maximized = false;
            X = Window.X; 
            Y = Window.Y;
            Width = Window.Width; 
            Height = Window.Height;
            if (AppWindow?.Presenter != null && AppWindow.Presenter is OverlappedPresenter presenter)
            {
                if (presenter.State == OverlappedPresenterState.Maximized)
                    Maximized = true;
                else
                {
                    RestoredX = Window.X;
                    RestoredY = Window.Y;
                    RestoredWidth = Window.Width;
                    RestoredHeight = Window.Height;
                }
            }

        }
    }
}