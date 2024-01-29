namespace Alaveri.Windows.Forms
{
    /// <summary>
    /// The position of the popup panel.
    /// </summary>
    public enum PopupPanelPosition
    {
        /// <summary>
        /// Positions the panel in the center of the parent form.
        /// </summary>
        Center,
        /// <summary>
        /// Uses the panel's location.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Represents a popup panel.
    /// </summary>
    public class PopupPanel
    {
        /// <summary>
        /// The position of the panel when shown.
        /// </summary>
        public PopupPanelPosition Position { get; set; }

        /// <summary>
        /// The Panel to be managed by the PopupPanelManager.
        /// </summary>
        public Panel Panel { get; set; }

        /// <summary>
        /// Initializes a new instance of the PopupPanel class using the specifid Panel control.
        /// </summary>
        /// <param name="panel">The Panel to be managed by the PopupPanelManager.</param>
        public PopupPanel(Panel panel)
        {
            Panel = panel;
        }
    }

}
