using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Maui;

/// <summary>
/// Contains the stored state of a window.
/// </summary>
/// <param name="initialWidth">The initial width of the window.</param>
/// <param name="initialHeight">The initial height of the window.</param>
public abstract class BaseStoredWindowState(double initialWidth, double initialHeight)
{
    /// <summary>
    /// The window bound to this state.
    /// </summary>
    [JsonIgnore]
    public Window? Window { get; set; }

    /// <summary>
    /// Gets or sets the window's X position.
    /// </summary>
    public double? X { get; set; }

    /// <summary>
    /// Gets or sets the window's Y position.
    /// </summary>
    public double? Y { get; set; }

    /// <summary>
    /// Gets or sets the window's width.
    /// </summary>
    public double Width { get; set; } = initialWidth;

    /// <summary>
    /// Gets or sets the window's height.
    /// </summary>
    public double Height { get; set; } = initialHeight;

    /// <summary>
    /// Gets or sets the window's X position when not maximized.
    /// </summary>
    public double RestoredX { get; set; }

    /// <summary>
    /// Gets or sets the window's Y position when not maximized.
    /// </summary>
    public double RestoredY { get; set; }

    /// <summary>
    /// Gets or sets the window's width when not maximized.
    /// </summary>
    public double RestoredWidth { get; set; }

    /// <summary>
    /// Gets or sets the window's height when not maximized.
    /// </summary>
    public double RestoredHeight { get; set; }

    /// <summary>
    /// True if the window is maximized.
    /// </summary>
    public bool Maximized { get; set; } = false;

    /// <summary>
    /// Restores the state of the window.
    /// </summary>
    public abstract void RestoreWindowState();

    /// <summary>
    /// Stores the state of the window.
    /// </summary>
    public abstract void StoreWindowState();
}
