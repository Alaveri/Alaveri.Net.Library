using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Alaveri.Maui;

public partial class StoredWindowState
{
    /// <summary>
    /// The window bound to this state.
    /// </summary>
    [JsonIgnore]
    public Window? Window { get; set; }

    /// <summary>
    /// Gets or sets the window's X position.
    /// </summary>
    public double? X;

    /// <summary>
    /// Gets or sets the window's Y position.
    /// </summary>
    public double? Y;

    /// <summary>
    /// Gets or sets the window's width.
    /// </summary>
    public double Width;

    /// <summary>
    /// Gets or sets the window's height.
    /// </summary>
    public double Height;

    /// <summary>
    /// Gets or sets the window's X position when not maximized.
    /// </summary>
    public double RestoredX;

    /// <summary>
    /// Gets or sets the window's Y position when not maximized.
    /// </summary>
    public double RestoredY;

    /// <summary>
    /// Gets or sets the window's width when not maximized.
    /// </summary>
    public double RestoredWidth;

    /// <summary>
    /// Gets or sets the window's height when not maximized.
    /// </summary>
    public double RestoredHeight;

    /// <summary>
    /// True if the window is maximized.
    /// </summary>
    public bool Maximized { get; set; } = false;

    /// <summary>
    /// Initializes a new object of the StoredWindowState class using the specified initial width and height.
    /// </summary>
    /// <param name="initialWidth">The initial width of the window.</param>
    /// <param name="initialHeight">The initial height of the window.</param>
    public StoredWindowState(double initialWidth, double initialHeight)
    {
        Width = initialWidth;
        Height = initialHeight;
    }
}
